using TVRename.App;

namespace TVRename.Tests.App;

/// <summary>
/// Pins the separate instance (fonctionnel/dossiers-de-donnees.md): instances of the same name form one instance,
/// those of other names run beside them and never receive their arguments.
/// </summary>
/// <remarks>
/// Each test names its instances on its own, so that neither a validation instance nor the TV Rename running on the
/// machine of whoever runs the tests meets them.
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
    public void MutexName_OfTheMachineWideInstance_IsTheOneOfTheExistingApplication()
        => Assert.Equal("TVRename", Create(null).MutexName);

    [Fact]
    public void MutexName_OfASeparateInstance_CarriesItsName()
        => Assert.Equal("TVRename-validation", Create(new SeparateInstance("validation", NewFolder())).MutexName);

    [Fact]
    public void IsFirstInstance_IsFalse_ForASecondInstanceOfTheSameName()
    {
        SeparateInstance instance = NewInstance();

        Assert.True(Create(instance).IsFirstInstance());
        Assert.False(Create(instance).IsFirstInstance());
    }

    [Fact]
    public void IsFirstInstance_IsTrue_ForInstancesOfDifferentNames()
    {
        Assert.True(Create(NewInstance()).IsFirstInstance());
        Assert.True(Create(NewInstance()).IsFirstInstance());
    }

    [Fact]
    public async Task SendArgumentsToExistingInstance_ReachesTheInstanceOfTheSameNameOnly()
    {
        SeparateInstance instance = NewInstance();
        TaskCompletionSource<string[]> sameName = new(TaskCreationOptions.RunContinuationsAsynchronously);
        TaskCompletionSource<string[]> otherName = new(TaskCreationOptions.RunContinuationsAsynchronously);
        Assert.True(Create(NewInstance(), args => otherName.TrySetResult(args)).IsFirstInstance());
        Assert.True(Create(instance, args => sameName.TrySetResult(args)).IsFirstInstance());
        SingleInstanceService second = Create(instance);
        Assert.False(second.IsFirstInstance());

        await second.SendArgumentsToExistingInstance();

        Assert.NotEmpty(await sameName.Task.WaitAsync(ArgumentsTimeout));
        Assert.False(otherName.Task.IsCompleted);
    }

    private SingleInstanceService Create(SeparateInstance? instance, Action<string[]>? onArgumentsReceived = null)
    {
        SingleInstanceService service = new(instance, onArgumentsReceived ?? (_ => { }));
        services.Add(service);
        return service;
    }

    private SeparateInstance NewInstance() => new($"tests-{Guid.NewGuid():N}", NewFolder());

    private string NewFolder()
    {
        string folder = Path.Combine(Path.GetTempPath(), $"tvrename-tests-{Guid.NewGuid():N}");
        Directory.CreateDirectory(folder);
        folders.Add(folder);
        return folder;
    }
}
