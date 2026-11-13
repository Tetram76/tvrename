using NLog;
using NLog.Config;
using NLog.Targets;

namespace TVRename.Tests.TVRename;

/// <summary>
/// Pins where the runtime data of the application go once /appdatapath: has moved them
/// (fonctionnel/dossiers-de-donnees.md): logs, cache and log of the embedded browser, audit of TheTVDB.
/// </summary>
/// <remarks>
/// <see cref="PathManager"/> holds the folder for the whole process, as the application sets it once at start-up: no
/// other test reads it.
/// </remarks>
public sealed class PathManagerTests : IDisposable
{
    private readonly string appDataFolder = Path.Combine(Path.GetTempPath(), $"tvrename-tests-{Guid.NewGuid():N}");

    public PathManagerTests() => PathManager.SetUserDefinedAppDataPath(appDataFolder);

    public void Dispose()
    {
        if (Directory.Exists(appDataFolder))
        {
            Directory.Delete(appDataFolder, recursive: true);
        }
    }

    [Fact]
    public void SetUserDefinedAppDataPath_CreatesTheFolder()
        => Assert.True(Directory.Exists(appDataFolder));

    [Fact]
    public void CefCachePath_IsInTheFolder()
        => Assert.Equal(Path.Combine(appDataFolder, "cache"), PathManager.CefCachePath());

    [Fact]
    public void CefLogFile_IsInTheFolder()
        => Assert.Equal(Path.Combine(appDataFolder, "log", "cef-debug.log"), PathManager.CefLogFile());

    [Fact]
    public void AuditLogFile_IsInTheFolder()
        => Assert.Equal(Path.Combine(appDataFolder, "audit", "Updates-1.json"), PathManager.AuditLogFile("-1"));

    [Fact]
    public void LogFile_OfTheApplicationConfiguration_IsInTheFolder()
    {
        FileTarget logFile = LoadApplicationLogFileTarget();

        Assert.Equal(
            Path.GetFullPath(Path.Combine(appDataFolder, "log", "TVRename.log")),
            Path.GetFullPath(logFile.FileName.Render(LogEventInfo.CreateNullEvent())));
    }

    [Fact]
    public void LogFile_OfTheApplicationConfiguration_WithoutTheOption_IsInTheDefaultFolder()
    {
        FileTarget logFile = LoadApplicationLogFileTarget();
        GlobalDiagnosticsContext.Remove("AppDataFolder");
        try
        {
            Assert.Equal(
                Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TVRename", "log", "TVRename.log")),
                Path.GetFullPath(logFile.FileName.Render(LogEventInfo.CreateNullEvent())));
        }
        finally
        {
            PathManager.SetUserDefinedAppDataPath(appDataFolder);
        }
    }

    [Fact]
    public void ArchiveOfTheLogFile_OfTheApplicationConfiguration_IsInTheFolder()
    {
        FileTarget logFile = LoadApplicationLogFileTarget();

        string archive = Path.GetFullPath(logFile.ArchiveFileName!.Render(LogEventInfo.CreateNullEvent()));

        Assert.StartsWith(Path.Combine(appDataFolder, "log", "archive") + Path.DirectorySeparatorChar, archive, StringComparison.OrdinalIgnoreCase);
    }

    // The configuration the application ships, copied next to it and, through its reference, next to these tests.
    private static FileTarget LoadApplicationLogFileTarget()
        => Assert.IsType<FileTarget>(new XmlLoggingConfiguration(Path.Combine(AppContext.BaseDirectory, "NLog.config")).FindTargetByName("logfile"));
}
