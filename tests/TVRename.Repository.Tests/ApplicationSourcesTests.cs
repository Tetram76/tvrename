using System.Text.RegularExpressions;

namespace TVRename.Repository.Tests;

/// <summary>
/// The application is for private use, without support, reporting or donation (fonctionnel/contexte.md), and shows
/// no licence (decision of the user): it sends its user neither to the sites, forum, issue tracker, manual or donation
/// page of the original project, which the fork does not control, nor to the text of a licence. Only what the user
/// sees counts, so comments (licence headers of the sources) are not checked.
/// </summary>
public sealed partial class ApplicationSourcesTests
{
    private static readonly string Application = Path.Combine(RepositoryFiles.Root, "TVRename");

    private static readonly string[] Extensions = [".cs", ".resx", ".config", ".json"];

    /// <summary>References to the original project that are not links shown to the user, or are removed by their own ticket.</summary>
    private static readonly (string File, string Line)[] Exceptions =
    [
        // Markers intercepted by the embedded browser, which never reaches the original site.
        ("Forms/UI.cs", "public const string EXPLORE_PROXY ="),
        ("Forms/UI.cs", "public const string WATCH_PROXY ="),
        // Update channel through the releases of the original project, removed by its own chantier (#7).
        ("Utility/VersionUpdater.cs", "GITHUB_RELEASES_API_URL"),
        ("Forms/Supporting/UpdateNotification.cs", "{\"context\", \"TV-Rename/tvrename\"}"),
    ];

    [Fact]
    public void Sources_DoNotLinkToTheOriginalProjectNorToALicence()
    {
        var violations = new List<string>();
        foreach (var file in Directory.EnumerateFiles(Application, "*", SearchOption.AllDirectories).Where(IsSource))
        {
            var relative = Path.GetRelativePath(Application, file).Replace('\\', '/');
            var lines = File.ReadAllLines(file);
            for (var index = 0; index < lines.Length; index++)
            {
                var line = lines[index].Trim();
                if (!line.StartsWith("//", StringComparison.Ordinal) && UpstreamOrLicence().IsMatch(line)
                    && !Exceptions.Any(exception => exception.File == relative && line.Contains(exception.Line, StringComparison.Ordinal)))
                    violations.Add($"{relative}:{index + 1}: {line}");
            }
        }

        Assert.Empty(violations);
    }

    private static bool IsSource(string file)
    {
        var relative = Path.GetRelativePath(Application, file).Replace('\\', '/');
        return Extensions.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase)
            && !relative.StartsWith("bin/", StringComparison.Ordinal) && !relative.StartsWith("obj/", StringComparison.Ordinal);
    }

    [GeneratedRegex(@"tvrename\.com|TV-Rename/tvrename|groups\.google\.com\S*tvrename|paypal\.(me|com)|LICENSE\.md|General Public License", RegexOptions.IgnoreCase)]
    private static partial Regex UpstreamOrLicence();
}
