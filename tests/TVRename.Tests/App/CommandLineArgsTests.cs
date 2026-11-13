namespace TVRename.Tests.App;

/// <summary>
/// Pins the reading of the option of the validation instance (fonctionnel/dossiers-de-donnees.md).
/// </summary>
public class CommandLineArgsTests
{
    [Theory]
    [InlineData("/validation")]
    [InlineData("/Validation")]
    public void Validation_IsSetByTheOption(string option)
        => Assert.True(new CommandLineArgs([option]).Validation);

    [Fact]
    public void Validation_IsNotSetWithoutTheOption()
        => Assert.False(new CommandLineArgs(["/userfilepath:C:\\Validation\\data"]).Validation);

    [Fact]
    public void Validation_LeavesUserFilePathToItsOwnOption()
    {
        CommandLineArgs args = new(["/userfilepath:C:\\Validation\\data", "/validation"]);

        Assert.Equal("C:\\Validation\\data", args.UserFilePath);
        Assert.True(args.Validation);
    }

    [Fact]
    public void Helptext_DescribesTheOption()
        => Assert.Contains("/validation", CommandLineArgs.Helptext(), StringComparison.Ordinal);
}
