using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace TVRename.Repository.Tests.Scripts;

/// <summary>
/// The tracking issue of the weekly quality audit (gestion-projet/depot/controle-qualite-hebdomadaire.md) decides when
/// the user is notified: a wrong urgency, or a previous list misread, would silence the critical debt or notify the
/// known debt every week. The script runs for real, against doubles of SonarQube Cloud and of GitHub
/// (Invoke-QualityAuditWithDoubles.ps1), so that nothing reaches the network.
/// </summary>
public sealed partial class UpdateQualityAuditIssueTests
{
    private const string AsItComes = "Au fil de l'eau";

    private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);

    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(60);

    [Fact]
    public async Task FirstRun_SetsTheReference_AndFailsForTheCriticalDebtOnly()
    {
        var run = await RunAsync(new Fixture { Issues = [Issue("a", "csharpsquid:S1", "A.cs", "LOW"), Issue("b", "csharpsquid:S2", "B.cs", "HIGH")] });

        Assert.Equal(1, run.ExitCode);
        Assert.Contains("issue create", run.GhCalls);
        Assert.Equal(["- `B.cs` : `sonar` `csharpsquid:S2`"], Section(run.Body, "Critique"));
        Assert.Empty(Section(run.Body, "Prioritaire"));
        Assert.Equal(["- `A.cs` : `sonar` `csharpsquid:S1`"], Section(run.Body, AsItComes));
    }

    [Fact]
    public async Task FirstRun_WithoutCriticalDebt_Succeeds()
    {
        var run = await RunAsync(new Fixture { Issues = [Issue("a", "csharpsquid:S1", "A.cs", "LOW")] });

        Assert.Equal(0, run.ExitCode);
        Assert.Contains("issue create", run.GhCalls);
    }

    [Fact]
    public async Task NextRun_ReadsItsOwnBodyBack_AndFindsNothingNew()
    {
        SonarIssue[] issues = [Issue("a", "csharpsquid:S1", "A.cs", "LOW"), Issue("c", "csharpsquid:S3", "A.cs", "MEDIUM")];
        var first = await RunAsync(new Fixture { Issues = issues });

        var next = await RunAsync(new Fixture { Issues = issues, Tracking = Tracking(first.Body) });

        Assert.Equal(0, next.ExitCode);
        Assert.Contains("issue edit", next.GhCalls);
        Assert.Equal(WithoutDate(first.Body), WithoutDate(next.Body));
    }

    [Fact]
    public async Task IssueRaisedAfterTheReference_IsPriority_AndNotifiesOnce()
    {
        var reference = await RunAsync(new Fixture { Issues = [Issue("a", "csharpsquid:S1", "A.cs", "LOW")] });
        SonarIssue[] issues = [Issue("a", "csharpsquid:S1", "A.cs", "LOW"), Issue("n", "csharpsquid:S9", "C.cs", "LOW")];

        var raised = await RunAsync(new Fixture { Issues = issues, Tracking = Tracking(reference.Body) });
        var known = await RunAsync(new Fixture { Issues = issues, Tracking = Tracking(raised.Body) });

        Assert.Equal(1, raised.ExitCode);
        Assert.Equal(["- `C.cs` : `sonar` `csharpsquid:S9`"], Section(raised.Body, "Prioritaire"));
        Assert.Equal(["- `A.cs` : `sonar` `csharpsquid:S1`"], Section(raised.Body, AsItComes));
        Assert.Equal(0, known.ExitCode);
        Assert.Equal(["- `C.cs` : `sonar` `csharpsquid:S9`"], Section(known.Body, "Prioritaire"));
    }

    [Fact]
    public async Task EntryRemovedBeforeItsHandlingIsAcquired_ComesBackAsNewPriority()
    {
        var reference = await RunAsync(new Fixture { Issues = [Issue("a", "csharpsquid:S1", "A.cs", "LOW")] });
        Assert.NotNull(reference.Body);
        var removed = reference.Body.Replace("- `A.cs` : `sonar` `csharpsquid:S1`\n", "", StringComparison.Ordinal);

        var run = await RunAsync(new Fixture { Issues = [Issue("a", "csharpsquid:S1", "A.cs", "LOW")], Tracking = Tracking(removed) });

        Assert.Equal(1, run.ExitCode);
        Assert.Equal(["- `A.cs` : `sonar` `csharpsquid:S1`"], Section(run.Body, "Prioritaire"));
    }

    [Fact]
    public async Task CriticalIssueThatCeasesToBeCritical_IsPriority_WithoutNotifying()
    {
        var critical = await RunAsync(new Fixture { Issues = [Issue("b", "csharpsquid:S2", "B.cs", "HIGH")] });

        var run = await RunAsync(new Fixture { Issues = [Issue("b", "csharpsquid:S2", "B.cs", "LOW")], Tracking = Tracking(critical.Body) });

        Assert.Equal(0, run.ExitCode);
        Assert.Empty(Section(run.Body, "Critique"));
        Assert.Equal(["- `B.cs` : `sonar` `csharpsquid:S2`"], Section(run.Body, "Prioritaire"));
    }

    [Fact]
    public async Task FailedGateCondition_MakesItsIssuesCritical_OrIsItselfAnEntry()
    {
        var newCode = Issue("n", "csharpsquid:S100", "A.cs", "LOW");
        var run = await RunAsync(new Fixture
        {
            Issues = [newCode, Issue("o", "csharpsquid:S200", "B.cs", "LOW")],
            NewIssues = [newCode],
            Gate = new Gate("ERROR", [new Condition("new_maintainability_rating", "ERROR"), new Condition("new_coverage", "ERROR"), new Condition("new_reliability_rating", "OK")]),
        });

        Assert.Equal(1, run.ExitCode);
        Assert.Equal(["- `gate` `new_coverage`", "- `A.cs` : `sonar` `csharpsquid:S100`"], Section(run.Body, "Critique"));
        Assert.Equal(["- `B.cs` : `sonar` `csharpsquid:S200`"], Section(run.Body, AsItComes));
    }

    [Fact]
    public async Task StyleWarningsAndMutants_AreAsItComes_ByRuleAndFile()
    {
        var root = RepositoryFiles.Root;
        var run = await RunAsync(new Fixture
        {
            StyleLog =
            [
                $@"  4:7>{root}\TVRename\X.cs(12,5): warning IDE0005: Using directive is unnecessary. [{root}\TVRename\TVRename.csproj]",
                $@"{root}\TVRename\X.cs(20,1,20,9): warning IDE0005: Using directive is unnecessary. [{root}\TVRename\TVRename.csproj]",
                $@"{root}\TVRename\X.cs(30,1): warning IDE0001: Name can be simplified. [{root}\TVRename\TVRename.csproj]",
            ],
            StyleRules = ["IDE0005"],
            MutationReport = new Dictionary<string, object>
            {
                ["files"] = new Dictionary<string, object>
                {
                    [Path.Combine(root, "TVRename", "Y.cs")] = new
                    {
                        mutants = new[]
                        {
                            new { mutatorName = "Equality mutation", status = "Survived" },
                            new { mutatorName = "Boolean mutation", status = "Killed" },
                            new { mutatorName = "Block removal mutation", status = "NoCoverage" },
                        },
                    },
                },
            },
        });

        Assert.Equal(0, run.ExitCode);
        Assert.Equal(
            ["- `TVRename/X.cs` : `style` `IDE0005`", "- `TVRename/Y.cs` : `mutant` `Block removal mutation`, `Equality mutation`"],
            Section(run.Body, AsItComes));
    }

    [Fact]
    public async Task EmptyList_ClosesTheOpenIssue()
    {
        var reference = await RunAsync(new Fixture { Issues = [Issue("a", "csharpsquid:S1", "A.cs", "LOW")] });

        var run = await RunAsync(new Fixture { Tracking = Tracking(reference.Body) });

        Assert.Equal(0, run.ExitCode);
        Assert.Equal(["issue list", "issue edit", "issue close"], run.GhCalls);
    }

    [Fact]
    public async Task RunOutsideMain_WritesNothing()
    {
        var run = await RunAsync(new Fixture { Issues = [Issue("b", "csharpsquid:S2", "B.cs", "HIGH")], Update = false });

        Assert.Equal(1, run.ExitCode);
        Assert.Equal(["issue list"], run.GhCalls);
        Assert.Null(run.Body);
    }

    private static SonarIssue Issue(string key, string rule, string file, string severity, string quality = "MAINTAINABILITY") =>
        new(key, rule, $"Tetram76_tvrename:{file}", [new Impact(quality, severity)]);

    private static TrackingIssue Tracking(string? body) => new(42, "OPEN", body ?? throw new ArgumentNullException(nameof(body)));

    /// <summary>The lines of a section of the body, blank lines left out.</summary>
    private static string[] Section(string? body, string name) =>
        (body ?? throw new ArgumentNullException(nameof(body))).Split('\n')
            .SkipWhile(line => line != $"### {name}").Skip(1)
            .TakeWhile(line => !line.StartsWith("### ", StringComparison.Ordinal))
            .Where(line => line.Length > 0).ToArray();

    private static string? WithoutDate(string? body) => body is null ? null : DateLine().Replace(body, "");

    private static async Task<Run> RunAsync(Fixture fixture)
    {
        var directory = Directory.CreateTempSubdirectory("quality-audit-");
        try
        {
            var fixturePath = Path.Combine(directory.FullName, "fixture.json");
            var resultPath = Path.Combine(directory.FullName, "result.json");
            await File.WriteAllTextAsync(fixturePath, JsonSerializer.Serialize(fixture, Json));

            var start = new ProcessStartInfo("pwsh") { WorkingDirectory = RepositoryFiles.Root, RedirectStandardOutput = true, RedirectStandardError = true };
            foreach (var argument in new[] { "-NoProfile", "-NonInteractive", "-File", Path.Combine(RepositoryFiles.Root, "tests", "TVRename.Repository.Tests", "Scripts", "Invoke-QualityAuditWithDoubles.ps1"), "-Fixture", fixturePath, "-Result", resultPath })
                start.ArgumentList.Add(argument);

            using var process = Process.Start(start) ?? throw new InvalidOperationException("pwsh could not be started.");
            var output = process.StandardOutput.ReadToEndAsync();
            var error = process.StandardError.ReadToEndAsync();
            using var timeout = new CancellationTokenSource(Timeout);
            try
            {
                await process.WaitForExitAsync(timeout.Token);
            }
            catch (OperationCanceledException)
            {
                process.Kill(entireProcessTree: true);
                throw new TimeoutException($"The quality audit did not end within {Timeout}.");
            }

            Assert.True(process.ExitCode == 0, $"The doubles failed ({process.ExitCode}): {await error}{await output}");
            return JsonSerializer.Deserialize<Run>(await File.ReadAllTextAsync(resultPath), Json)
                ?? throw new InvalidOperationException("No result.");
        }
        finally
        {
            directory.Delete(recursive: true);
        }
    }

    [GeneratedRegex(@"^Dernier contrôle : \d{4}-\d{2}-\d{2}$", RegexOptions.Multiline)]
    private static partial Regex DateLine();

    private sealed record Impact(string SoftwareQuality, string Severity);

    private sealed record SonarIssue(string Key, string Rule, string Component, Impact[] Impacts);

    private sealed record Condition(string MetricKey, string Status);

    private sealed record Gate(string Status, Condition[] Conditions);

    private sealed record TrackingIssue(int Number, string State, string Body);

    private sealed record Fixture
    {
        public SonarIssue[] Issues { get; init; } = [];

        public SonarIssue[] NewIssues { get; init; } = [];

        public object[] Hotspots { get; init; } = [];

        public Gate Gate { get; init; } = new("OK", []);

        public TrackingIssue? Tracking { get; init; }

        public string[] StyleLog { get; init; } = [];

        public string[] StyleRules { get; init; } = ["IDE0005"];

        public object? MutationReport { get; init; }

        public bool Update { get; init; } = true;
    }

    private sealed record Run(int ExitCode, string[] GhCalls, string? Body, string Output);
}
