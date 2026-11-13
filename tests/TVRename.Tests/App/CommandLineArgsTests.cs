namespace TVRename.Tests.App;

/// <summary>
/// Pins the reading of the options that move the folders of the application (fonctionnel/dossiers-de-donnees.md).
/// </summary>
public class CommandLineArgsTests
{
    [Theory]
    [InlineData("/appdatapath:C:\\Validation\\appdata", "C:\\Validation\\appdata")]
    [InlineData("/AppDataPath:C:\\Validation\\appdata", "C:\\Validation\\appdata")]
    [InlineData("/appdatapath:relative", "relative")]
    public void AppDataPath_IsTheFolderGivenByTheOption(string option, string expected)
        => Assert.Equal(expected, new CommandLineArgs([option]).AppDataPath);

    [Fact]
    public void AppDataPath_IsNullWithoutTheOption()
        => Assert.Null(new CommandLineArgs(["/userfilepath:C:\\Validation\\data"]).AppDataPath);

    [Fact]
    public void AppDataPath_AndUserFilePath_AreReadIndependently()
    {
        CommandLineArgs args = new(["/userfilepath:C:\\Validation\\data", "/appdatapath:C:\\Validation\\appdata"]);

        Assert.Equal("C:\\Validation\\data", args.UserFilePath);
        Assert.Equal("C:\\Validation\\appdata", args.AppDataPath);
    }

    [Fact]
    public void Helptext_DescribesTheOption()
        => Assert.Contains("/appdatapath:", CommandLineArgs.Helptext(), StringComparison.Ordinal);
}
