using NLog.Config;
using NLog.Targets;
using TVRename.App;

namespace TVRename.Tests.App;

/// <summary>
/// Pins that the application keeps its log local: it sends no log, nor any diagnostic data, to a remote service
/// (fonctionnel/journal-de-l-application.md).
/// </summary>
/// <remarks>
/// The log has two possible sources: the targets that <see cref="ApplicationBase"/> adds in code at start-up, and the
/// <c>NLog.config</c> file shipped with the application. Each test closes one of them.
/// </remarks>
public sealed class ApplicationBaseTests
{
    /// <summary>The target types that write on the machine itself: log file, debugger output and console.</summary>
    private static readonly Type[] LocalTargetTypes =
    [
        typeof(FileTarget),
        typeof(OutputDebugStringTarget),
        typeof(ColoredConsoleTarget),
    ];

    [Fact]
    public void TheApplication_ReferencesNoSyslogSender()
        => Assert.DoesNotContain(
            typeof(ApplicationBase).Assembly.GetReferencedAssemblies(),
            reference => reference.Name == "NLog.Targets.Syslog");

    [Fact]
    public void TheShippedLogConfiguration_HasLocalTargetsOnly()
    {
        XmlLoggingConfiguration configuration = new(Path.Combine(AppContext.BaseDirectory, "NLog.config"));

        Assert.NotEmpty(configuration.AllTargets);
        Assert.All(configuration.AllTargets, target => Assert.Contains(target.GetType(), LocalTargetTypes));
    }
}
