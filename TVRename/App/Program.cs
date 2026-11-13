//
// Main website for TVRename is http://tvrename.com
//
// Source code available at https://github.com/TV-Rename/tvrename
//
// Copyright (c) TV Rename. This code is released under GPLv3 https://github.com/TV-Rename/tvrename/blob/master/LICENSE.md
//

using System.Net.Http;
using System.Reflection;

namespace TVRename.App;

/// <summary>
/// Application entry point.
/// </summary>
public static class Program
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    private static ApplicationBase? TvRename;

    /// <summary>
    /// Defines the entry point of the application.
    /// Checks if the application is already running and if so, performs actions via IPC and exits.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    [STAThread]
    private static void Main(string[] args)
    {
        // Before the first log, which goes to the data folder.
        SeparateInstance? separateInstance = SeparateInstance();
        if (separateInstance is not null && !SetupSeparateAppDataFolder(separateInstance))
        {
            Environment.Exit(1);
        }

        Logger.Info($"TV Rename {Helpers.DisplayVersion} started with args: {string.Join(" ", args)}");
        Logger.Info($"Copyright (C) {TimeHelpers.LocalNow().Year} TV Rename");
        Logger.Info("This program comes with ABSOLUTELY NO WARRANTY; This is free software, and you are welcome to redistribute it under certain conditions");

        Application.EnableVisualStyles();
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetColorMode(TVSettings.Instance.ColorModeActual);

        try
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
            AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;
            Application.ThreadException += delegate (object _, ThreadExceptionEventArgs eventArgs)
            {
                Exception e = eventArgs.Exception;
                if (e is HttpRequestException hre)
                {
                    Logger.Fatal(e, $"UNHANDLED ERROR - Application.ThreadException ({hre.TargetSite}-{hre.InnerException})");
                    if (hre.InnerException is { } ie)
                    {
                        Logger.Fatal(ie, $"UNHANDLED ERROR - Application.ThreadException ({ie.TargetSite}-{ie.InnerException})");
                    }
                }
                else
                {
                    Logger.Fatal(e, "UNHANDLED ERROR - Application.ThreadException");
                }

                Environment.Exit(1);
            };
        }
        catch (Exception e)
        {
            Logger.Fatal(e);
        }

        if (args.Contains("/?", StringComparer.OrdinalIgnoreCase))
        {
            Logger.Info(CommandLineArgs.Helptext());

            //redirect console output to parent process
            //must be before any calls to Console.WriteLine()
            //MS: Never got this to work quite right - seems there is no simple way to output
            //to the command line console if you are a winforms app
            if (NativeMethods.AttachParentConsole())
            {
                Console.WriteLine(CommandLineArgs.Helptext());
            }
            else
            {
                Logger.Info("Could not attach to console");
            }
            return;
        }

        Mutex? mutex = null;
        try
        {
            using SingleInstanceService singleInstanceService = new(separateInstance, OnArgumentsReceived);

            // Check if an application instance is already running
            mutex = new Mutex(true, singleInstanceService.MutexName, out bool newInstance);

            if (!singleInstanceService.IsFirstInstance() || !newInstance)
            {
                // Already running
                Logger.Warn("An instance is already running, exiting");
                _ = singleInstanceService.SendArgumentsToExistingInstance();
                Environment.Exit(1);
            }

            Logger.Info("Starting new instance");

            TvRename = new ApplicationBase();

            TvRename.Run(args);

            GC.KeepAlive(mutex);
        }
        catch (TVRenameOperationInterruptedException)
        {
            Logger.Warn("USER REQUESTED End: Application exiting with error");
            Environment.Exit(1);
        }
        catch (ObjectDisposedException)
        {
            Logger.Warn("ObjectDisposedError: Application exiting with error");
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            Logger.Fatal(ex, "Application exiting with error");

            new ShowException(ex).ShowDialog();

            Environment.Exit(1);
        }
        finally
        {
            mutex?.Dispose();
            Logger.Info("Application exiting");
        }
    }

    /// <summary>
    /// The instance kept apart from the TV Rename of the machine: the validation build only (VALIDATION, set by
    /// scripts/Start-Validation.ps1), so that the application users get has no such mode.
    /// </summary>
    private static SeparateInstance? SeparateInstance()
#if VALIDATION
        => new("validation", PathManager.ValidationAppDataPath);
#else
        => null;
#endif

    /// <summary>
    /// Moves the data folder of the application to the one of the separate instance.
    /// </summary>
    /// <returns>false when the folder cannot be used: the application then stops, rather than writing to the default
    /// folder that a separate instance leaves untouched.</returns>
    private static bool SetupSeparateAppDataFolder(SeparateInstance separateInstance)
    {
        try
        {
            PathManager.SetUserDefinedAppDataPath(separateInstance.Folder);
            return true;
        }
        catch (Exception ex) when (ex is ArgumentException or System.IO.IOException or UnauthorizedAccessException or NotSupportedException)
        {
            MessageBox.Show($"Error while setting the data folder {separateInstance.Folder}:{Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    private static void OnArgumentsReceived(string[] args)
    {
        if (TvRename is null)
        {
            Logger.Warn($"Cannot pass {args.ToCsv()} to running instance ApplicationBase 's' is not created yet.");
        }
        else
        {
            Logger.Info($"Received {args.ToCsv()}, sending to the application.");
            TvRename.ProcessReceivedArgs(args);
        }
    }

    private static Assembly? OnAssemblyResolve(object? sender, ResolveEventArgs args)
    {
        if (args.Name.StartsWith("CefSharp", StringComparison.Ordinal))
        {
            string assemblyName = args.Name.Split([','], 2)[0] + ".dll";
            string architectureSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                Environment.Is64BitProcess ? "x64" : "x86",
                assemblyName);

            if (File.Exists(architectureSpecificPath))
            {
                Logger.Warn($"Updated path for Assembly: {architectureSpecificPath}");
                return Assembly.LoadFile(architectureSpecificPath);
            }
        }

        return null;
    }
    private static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs args)
    {
        Exception e = (Exception)args.ExceptionObject;
        Logger.Fatal(e, "UNHANDLED ERROR - GlobalExceptionHandler");
        Environment.Exit(1);
    }
}
