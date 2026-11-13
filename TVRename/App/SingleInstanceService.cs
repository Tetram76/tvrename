//
// Main website for TVRename is http://tvrename.com
//
// Source code available at https://github.com/TV-Rename/tvrename
//
// Copyright (c) TV Rename. This code is released under GPLv3 https://github.com/TV-Rename/tvrename/blob/master/LICENSE.md
//

using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using File = System.IO.File;
using Path = System.IO.Path;

namespace TVRename.App;

/// <summary>
/// An instance kept apart from the machine-wide one: the one of the validation build (VALIDATION).
/// </summary>
/// <param name="Name">Name of the instance, which its mutex and semaphore carry.</param>
/// <param name="Folder">Its data folder, where it writes the port it listens on.</param>
internal sealed record SeparateInstance(string Name, string Folder);

/// <summary>
/// Keeps a single instance of the application, and passes the arguments of the next instances to it.
/// </summary>
/// <remarks>
/// The machine-wide instance keeps the names and the port of the existing application. A separate instance forms a
/// single instance with the instances of the same name only
/// (choix-implementation/environnement-de-validation-fonctionnelle.md): its names carry its name, and it listens on a
/// port the system assigns, written in its folder for the next ones.
/// </remarks>
/// <param name="separateInstance">The separate instance, or null for the machine-wide instance.</param>
/// <param name="onArgumentsReceived">Receives the arguments passed by a next instance.</param>
internal sealed class SingleInstanceService(SeparateInstance? separateInstance, Action<string[]> onArgumentsReceived) : IDisposable
{
    private const string LOCAL_HOST = "127.0.0.1";
    private const int MACHINE_WIDE_PORT = 19191;
    private const string PORT_FILE_NAME = "instance.port";
    private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

    private readonly string scopeSuffix = separateInstance is null ? string.Empty : $"-{separateInstance.Name}";
    private Semaphore? semaphore;
    private TcpListener? tcpListener;

    /// <summary>
    /// Name of the mutex that the application holds while it runs.
    /// </summary>
    internal string MutexName => $"TVRename{scopeSuffix}";

    private string SemaphoreName => $"Global\\{Environment.MachineName}-TVRename-{Assembly.GetExecutingAssembly().GetName().Version}{scopeSuffix}.SingleInstanceLimiter";

    private string? PortFile => separateInstance is null ? null : Path.Combine(separateInstance.Folder, PORT_FILE_NAME);

    internal bool IsFirstInstance()
    {
        try
        {
            if (Semaphore.TryOpenExisting(SemaphoreName, out semaphore))
            {
                return false;
            }

            semaphore = CreateSemaphore();
            StartListening();
            return true;
        }
        catch (IOException ex)
        {
            Log.Error(ex);
            return false;
        }
        catch (UnauthorizedAccessException ex)
        {
            Log.Error(ex);
            return false;
        }
        catch (ArgumentException ex)
        {
            Log.Error(ex);
            return false;
        }
        catch (WaitHandleCannotBeOpenedException ex)
        {
            Log.Error(ex);
            return false;
        }
    }

    public void Dispose()
    {
        tcpListener?.Stop();
        semaphore?.Dispose();
    }

    private Semaphore CreateSemaphore()
    {
        // 1. Create a security descriptor
        var semaphoreSecurity = new SemaphoreSecurity();

        // 2. Define a rule that allows "Everyone" (WorldSid) to enter and release the semaphore
        var everyoneSid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
        var accessRule = new SemaphoreAccessRule(
            everyoneSid,
            SemaphoreRights.Synchronize | SemaphoreRights.Modify, // Rights to enter/release
            AccessControlType.Allow);

        semaphoreSecurity.AddAccessRule(accessRule);

        // 3. Create the semaphore safely with the attached Access Control List (ACL)
        return SemaphoreAcl.Create(
            initialCount: 0,
            maximumCount: 1,
            name: SemaphoreName,
            createdNew: out bool _,
            semaphoreSecurity: semaphoreSecurity);
    }

    /// <summary>
    /// Listens before returning, so that the port of a separate instance is written before a next instance can look
    /// for it.
    /// </summary>
    private void StartListening()
    {
        tcpListener = new TcpListener(IPAddress.Parse(LOCAL_HOST), PortFile is null ? MACHINE_WIDE_PORT : 0);
        try
        {
            tcpListener.Start();
            if (PortFile is not null)
            {
                File.WriteAllText(PortFile, ((IPEndPoint)tcpListener.LocalEndpoint).Port.ToString(CultureInfo.InvariantCulture));
            }
        }
        catch (SocketException ex)
        {
            Log.Error(ex);
            tcpListener.Stop();
            return;
        }

        TcpListener listener = tcpListener;
        Task.Run(() => ListenForArguments(listener));
    }

    private void ListenForArguments(TcpListener listener)
    {
        try
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => ReceivedMessage(client));
            }
        }
        catch (SocketException ex)
        {
            // Also the way out once the listener is stopped.
            Log.Debug(ex);
        }
        catch (ObjectDisposedException ex)
        {
            Log.Debug(ex);
        }
    }

    private void ReceivedMessage(TcpClient tcpClient)
    {
        try
        {
            using (tcpClient)
            {
                using NetworkStream networkStream = tcpClient.GetStream();
                string data = string.Empty;
                byte[] bytes = new byte[256];
                int bytesCount;
                while ((bytesCount = networkStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data += Encoding.UTF8.GetString(bytes, 0, bytesCount);
                }
                onArgumentsReceived(data.Split(' '));
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    public Task SendArgumentsToExistingInstance() => Task.Run(SendArguments);

    private void SendArguments()
    {
        try
        {
            using TcpClient tcpClient = new(LOCAL_HOST, ExistingInstancePort());
            using NetworkStream networkStream = tcpClient.GetStream();
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            string message = commandLineArgs.Length > 1
                ? string.Join(" ", commandLineArgs)
                : "/focus";
            byte[] data = Encoding.UTF8.GetBytes(message);
            networkStream.Write(data, 0, data.Length);
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    private int ExistingInstancePort()
        => PortFile is null
            ? MACHINE_WIDE_PORT
            : int.Parse(File.ReadAllText(PortFile), NumberStyles.None, CultureInfo.InvariantCulture);
}
