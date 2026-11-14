using System.Text.RegularExpressions;

namespace TVRename.Repository.Tests;

/// <summary>
/// The application is for private use (fonctionnel/contexte.md) and carries no licence, near or far (decision of the
/// user): outside the application too, neither its installer nor the documentation of the repository shows one. The
/// licence headers of the sources are checked with them (<see cref="ApplicationSourcesTests"/>).
/// </summary>
public sealed partial class LicenceTests
{
    [Fact]
    public void Repository_HasNoLicenceFile() =>
        Assert.False(File.Exists(Path.Combine(RepositoryFiles.Root, "Licence.rtf")));

    [Theory]
    [InlineData("Installer.nsi")]
    [InlineData("README.md")]
    public void File_ShowsNoLicence(string relativePath) =>
        Assert.DoesNotMatch(Licence(), RepositoryFiles.Read(relativePath));

    // Broader than the check of the sources, where the word also names the terms of a data source (TVmaze).
    [GeneratedRegex(@"licen[cs]e|General Public License|\bGPL(v\d+)?\b", RegexOptions.IgnoreCase)]
    private static partial Regex Licence();
}
