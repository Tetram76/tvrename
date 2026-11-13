using TVRename.App;

namespace TVRename.Tests.App;

/// <summary>
/// Pins the single instance per folder of runtime data (fonctionnel/dossiers-de-donnees.md): instances started with
/// the same /appdatapath: folder form one instance, those of other folders run beside them and never receive their
/// arguments.
/// </summary>
/// <remarks>
/// Only instances with a folder of their own are exercised: the machine-wide instance would meet a TV Rename running
/// on the machine of whoever runs the tests.
/// </remarks>
public sealed class SingleInstanceServiceTests : IDisposable
{
    private static readonly TimeSpan ArgumentsTimeout = TimeSpan.FromSeconds(10);

    private readonly List<string> folders = [];
    private readonly List<SingleInstanceService> services = [];

    public void Dispose()
    {
        foreach (SingleInstanceService service in services)
        {
            service.Dispose();
        }
        foreach (string folder in folders.Where(Directory.Exists))
        {
            Directory.Delete(folder, recursive: true);
        }
    }

    [Fact]
    public void MutexName_WithoutFolder_IsTheMachineWideOne()
        => Assert.Equal("TVRename", Create(null).MutexName);

    [Fact]
    public void MutexName_DependsOnTheFolderOnly()
    {
        string folder = NewFolder();

        Assert.Equal(Create(folder).MutexName, Create(folder + Path.DirectorySeparatorChar).MutexName);
        Assert.Equal(Create(folder).MutexName, Create(folder.ToUpperInvariant()).MutexName);
        Assert.NotEqual(Create(folder).MutexName, Create(NewFolder()).MutexName);
        Assert.NotEqual("TVRename", Create(folder).MutexName);
    }

    [Fact]
    public void IsFirstInstance_IsFalse_ForASecondInstanceOfTheSameFolder()
    {
        string folder = NewFolder();

        Assert.True(Create(folder).IsFirstInstance());
        Assert.False(Create(folder).IsFirstInstance());
    }

    [Fact]
    public void IsFirstInstance_IsTrue_ForInstancesOfDifferentFolders()
    {
        Assert.True(Create(NewFolder()).IsFirstInstance());
        Assert.True(Create(NewFolder()).IsFirstInstance());
    }

    [Fact]
    public async Task SendArgumentsToExistingInstance_ReachesTheInstanceOfTheSameFolderOnly()
    {
        string folder = NewFolder();
        TaskCompletionSource<string[]> sameFolder = new(TaskCreationOptions.RunContinuationsAsynchronously);
        TaskCompletionSource<string[]> otherFolder = new(TaskCreationOptions.RunContinuationsAsynchronously);
        Assert.True(Create(NewFolder(), args => otherFolder.TrySetResult(args)).IsFirstInstance());
        Assert.True(Create(folder, args => sameFolder.TrySetResult(args)).IsFirstInstance());
        SingleInstanceService second = Create(folder);
        Assert.False(second.IsFirstInstance());

        await second.SendArgumentsToExistingInstance();

        Assert.NotEmpty(await sameFolder.Task.WaitAsync(ArgumentsTimeout));
        Assert.False(otherFolder.Task.IsCompleted);
    }

    private SingleInstanceService Create(string? folder, Action<string[]>? onArgumentsReceived = null)
    {
        SingleInstanceService service = new(folder, onArgumentsReceived ?? (_ => { }));
        services.Add(service);
        return service;
    }

    private string NewFolder()
    {
        string folder = Path.Combine(Path.GetTempPath(), $"tvrename-tests-{Guid.NewGuid():N}");
        Directory.CreateDirectory(folder);
        folders.Add(folder);
        return folder;
    }
}
