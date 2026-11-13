#Requires -Version 7.3
<#
.SYNOPSIS
    Gathers the entries of the weekly quality audit of main and keeps them in the tracking issue of the harness.

.DESCRIPTION
    Run by .github/workflows/quality-audit.yml (gestion-projet/depot/controle-qualite-hebdomadaire.md), after the build
    that reports the style rules and the mutation tests. Gathers the entries of the audit: the open issues, the security
    hotspots to review and the failed conditions of the quality gate of main in SonarQube Cloud; the style warnings of
    the build log; the surviving or uncovered mutants of the Stryker.NET report. Each entry is its source, its rule
    (condition, mutator) and its file, never its line, so that it stays the same from one week to the next.

    The debt is split by urgency: critical, the issues that fail a condition of the quality gate and those of blocker or
    high severity; priority, any other issue of SonarQube Cloud; as it comes, everything else. The first run sets the
    reference: what is not critical goes as it comes. Afterwards, an issue of SonarQube Cloud that the previous list
    held as it comes stays there; any other one is priority.

    The tracking issue (labels quality-audit and harness, gestion-projet/revue-tickets/issues.md) is a state, not a
    history: its body is rewritten in full, open while it lists an entry. Without -Update, nothing is written: the body
    is only printed, as for a run on a branch other than main.

    The script fails, which notifies the user who created the workflow, when the audit finds a critical entry, or a
    priority entry absent from the previous list, and only then.

.EXAMPLE
    ./scripts/Update-QualityAuditIssue.ps1 -StyleLog style.log -StyleRules IDE0005, IDE0300 -MutationReport mutation-report.json
#>
[CmdletBinding()]
param(
    # Warnings-only log of the build that reports the style rules.
    [Parameter(Mandatory)] [string] $StyleLog,

    # Style rules the audit reports; any other warning of the log is ignored.
    [Parameter(Mandatory)] [string[]] $StyleRules,

    # JSON report of Stryker.NET; none when no code is mutated.
    [string] $MutationReport,

    # Rewrites the tracking issue; a run on a branch other than main only prints its findings.
    [switch] $Update
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$sonarProject = 'Tetram76_tvrename'
$sonarApi = 'https://sonarcloud.io/api'
$root = (git rev-parse --show-toplevel) -replace '\\', '/'
# Above this size, GitHub refuses the body of an issue.
$maxBodyLength = 65536
$sections = 'Critique', 'Prioritaire', "Au fil de l'eau"

function ConvertTo-Entry([string] $section, [string] $source, [string] $ident, [string] $file = '') {
    [pscustomobject]@{ Section = $section; Source = $source; Ident = $ident; File = $file; Key = "$source|$ident|$file" }
}

function Get-RelativePath([string] $path) {
    [IO.Path]::GetRelativePath($root, $path).Replace('\', '/')
}

#region SonarQube Cloud

function Invoke-SonarApi([string] $path) {
    if (-not $env:SONAR_TOKEN) { throw 'SONAR_TOKEN is not set.' }
    Invoke-RestMethod -Headers @{ Authorization = "Bearer $env:SONAR_TOKEN" } -TimeoutSec 60 -Uri "$sonarApi/$path"
}

function Invoke-Sonar([string] $path, [string] $property) {
    # 500 per page, the maximum of the API, which serves no more than 10,000 results in all.
    for ($page = 1; ; $page++) {
        $response = Invoke-SonarApi "$path&ps=500&p=$page"
        $items = @($response.$property)
        $items
        if ($items.Count -lt 500 -or $page * 500 -ge $response.paging.total) { return }
    }
}

function Get-SonarFile([string] $component) {
    # A component is <project>:<path>; one without a path is the project itself.
    if ($component.Contains(':')) { $component.Substring($component.IndexOf(':') + 1) } else { '/' }
}

$issues = @(Invoke-Sonar "issues/search?componentKeys=$sonarProject&branch=main&resolved=false" issues)
$hotspots = @(Invoke-Sonar "hotspots/search?projectKey=$sonarProject&status=TO_REVIEW" hotspots)
$gate = (Invoke-SonarApi "qualitygates/project_status?projectKey=$sonarProject&branch=main").projectStatus
Write-Output "SonarQube Cloud: $($issues.Count) open issue(s), $($hotspots.Count) hotspot(s) to review, quality gate $($gate.status)."

# Every issue of the new code that fails a condition of the quality gate is critical, even if none would fail it alone;
# so is a failed condition that no issue designates (gestion-projet/depot/controle-qualite-hebdomadaire.md).
$criticalIssues = [Collections.Generic.HashSet[string]]::new()
$criticalHotspots = [Collections.Generic.HashSet[string]]::new()
$failedConditions = @($gate.conditions | Where-Object status -EQ 'ERROR')
$newCodeIssues, $newCodeHotspots = if ($failedConditions | Where-Object metricKey -Like 'new_*') {
    , @(Invoke-Sonar "issues/search?componentKeys=$sonarProject&branch=main&resolved=false&sinceLeakPeriod=true" issues)
    , @(Invoke-Sonar "hotspots/search?projectKey=$sonarProject&status=TO_REVIEW&sinceLeakPeriod=true" hotspots)
}
$gateEntries = foreach ($condition in $failedConditions) {
    $metric = $condition.metricKey
    $newCode = $metric.StartsWith('new_')
    $designated = @(switch -Regex ($metric) {
            'hotspot|security_review' { $newCode ? $newCodeHotspots : $hotspots; break }
            'reliability|bug|security|vulnerabilit|maintainability|sqale|code_smell' {
                $quality = switch -Regex ($metric) {
                    'reliability|bug' { 'RELIABILITY'; break }
                    'security|vulnerabilit' { 'SECURITY'; break }
                    default { 'MAINTAINABILITY' }
                }
                ($newCode ? $newCodeIssues : $issues) | Where-Object { $_.impacts.softwareQuality -contains $quality }
            }
        })
    foreach ($item in $designated) {
        if ($item.PSObject.Properties['ruleKey']) { [void] $criticalHotspots.Add($item.key) } else { [void] $criticalIssues.Add($item.key) }
    }
    if (-not $designated) { ConvertTo-Entry Critique gate $metric }
}

$sonarEntries = @(
    foreach ($sonarIssue in $issues) {
        $isCritical = $criticalIssues.Contains($sonarIssue.key) -or ($sonarIssue.impacts.severity | Where-Object { $_ -in 'BLOCKER', 'HIGH' })
        ConvertTo-Entry ($isCritical ? 'Critique' : '') sonar $sonarIssue.rule (Get-SonarFile $sonarIssue.component)
    }
    foreach ($hotspot in $hotspots) {
        ConvertTo-Entry ($criticalHotspots.Contains($hotspot.key) ? 'Critique' : '') hotspot $hotspot.ruleKey (Get-SonarFile $hotspot.component)
    }
)

#endregion

#region Style rules and mutation tests

# A parallel build prefixes each line of its log with the node that wrote it (" 4:7>").
$styleEntries = @(
    foreach ($line in Get-Content $StyleLog) {
        if ($line -match '^(?:\s*\d+(?::\d+)?>)?(?<file>.+?)\(\d+,\d+(?:,\d+,\d+)?\): warning (?<rule>IDE\d{4}):' -and $Matches.rule -in $StyleRules) {
            ConvertTo-Entry "Au fil de l'eau" style $Matches.rule (Get-RelativePath $Matches.file)
        }
    }
)

# A mutant is its mutator and its file, like any other entry: one entry per mutant, with its code and its replacement,
# would exceed the size of an issue body as soon as a few files are mutated. The report of the run, kept as its
# artifact, details each mutant.
$mutantEntries = @(
    if ($MutationReport) {
        $report = Get-Content -Raw $MutationReport | ConvertFrom-Json
        foreach ($file in $report.files.PSObject.Properties) {
            foreach ($mutant in @($file.Value.mutants | Where-Object status -In 'Survived', 'NoCoverage')) {
                ConvertTo-Entry "Au fil de l'eau" mutant $mutant.mutatorName.Replace('`', "'") (Get-RelativePath $file.Name)
            }
        }
    }
)

#endregion

#region Tracking issue

$tracking = @(gh issue list --label quality-audit --label harness --state all --limit 1 --json number,state,body | ConvertFrom-Json)
if ($LASTEXITCODE) { throw 'The lookup of the tracking issue failed.' }
$issue = $tracking ? $tracking[0] : $null

# The previous list, entry by entry, with its section. The first run, without any section, sets the reference.
function Read-Entry([string] $body) {
    $section = $null
    foreach ($line in $body -split '\r?\n') {
        if ($line -match '^### (?<name>.+)$') { $section = $Matches.name; continue }
        if (-not $section) { continue }
        if ($line -match '^- `gate` `(?<metric>[^`]+)`$') { ConvertTo-Entry $section gate $Matches.metric; continue }
        if ($line -match '^- `(?<file>[^`]+)` : (?<groups>.+)$') {
            $file = $Matches.file
            foreach ($group in $Matches.groups -split ' ; ') {
                if ($group -match '^`(?<source>\w+)` (?<rules>.+)$') {
                    $groupSource = $Matches.source
                    foreach ($rule in [regex]::Matches($Matches.rules, '`([^`]+)`')) { ConvertTo-Entry $section $groupSource $rule.Groups[1].Value $file }
                }
            }
        }
    }
}

$previous = @(if ($issue) { Read-Entry $issue.body })
$reference = -not ($issue -and $issue.body -match '(?m)^### ')
$previousKeys = [Collections.Generic.HashSet[string]]::new([string[]] @($previous | ForEach-Object Key))
$asItComes = [Collections.Generic.HashSet[string]]::new([string[]] @($previous | Where-Object Section -EQ "Au fil de l'eau" | ForEach-Object Key))

# An issue of SonarQube Cloud that is not critical is priority, unless the reference or the previous list keeps it as
# it comes; a critical one that ceases to be so without having been handled is therefore priority again.
foreach ($entry in $sonarEntries | Where-Object Section -EQ '') {
    $entry.Section = ($reference -or $asItComes.Contains($entry.Key)) ? "Au fil de l'eau" : 'Prioritaire'
}

# Each entry appears once: the same rule on the same file is one entry, whatever the number of its occurrences.
$entries = @(@($gateEntries) + $sonarEntries + $styleEntries + $mutantEntries | Where-Object { $_ } |
    Sort-Object { $sections.IndexOf($_.Section) } | Group-Object Key | ForEach-Object { $_.Group[0] })

# One line per file, its entries grouped by source. Every name goes into a code span, where GitHub neither notifies a
# mention nor links a reference to an issue.
function Format-Section([string] $name) {
    "### $name"
    ''
    $inSection = @($entries | Where-Object Section -EQ $name)
    foreach ($entry in $inSection | Where-Object Source -EQ gate | Sort-Object Ident) { "- ``gate`` ``$($entry.Ident)``" }
    foreach ($file in $inSection | Where-Object File | Group-Object File | Sort-Object Name) {
        $groups = foreach ($source in 'sonar', 'hotspot', 'style', 'mutant') {
            $rules = @($file.Group | Where-Object Source -EQ $source | ForEach-Object Ident | Sort-Object -Unique)
            if ($rules) { "``$source`` " + (($rules | ForEach-Object { "``$_``" }) -join ', ') }
        }
        "- ``$($file.Name)`` : $($groups -join ' ; ')"
    }
    ''
}

# The three parts of a tracking issue, and nothing else (gestion-projet/revue-tickets/issues.md).
$body = (@(
        "Dette qualité du code de ``main`` que relève le contrôle qualité hebdomadaire (``.github/workflows/quality-audit.yml``), répartie par urgence. Une entrée est sa source, sa règle (condition, mutateur) et son fichier ; le rapport des tests de mutation, joint à l'exécution du contrôle, détaille chaque mutant. Chaque entrée est retirée dès que son traitement est acquis (``gestion-projet/depot/controle-qualite-hebdomadaire.md``)."
        ''
        "Dernier contrôle : $([DateTime]::UtcNow.ToString('yyyy-MM-dd'))"
        ''
        foreach ($section in $sections) { Format-Section $section }
    ) -join "`n").TrimEnd() + "`n"
Write-Output $body

if ($body.Length -gt $maxBodyLength) {
    throw "The body of the tracking issue ($($body.Length) characters) exceeds the $maxBodyLength that GitHub accepts."
}

if ($Update) {
    $bodyFile = New-TemporaryFile
    try {
        Set-Content -LiteralPath $bodyFile -Value $body -NoNewline
        if (-not $issue) {
            # Without any entry, there is nothing to follow, hence no issue to create.
            if ($entries) {
                # Only the initial title: the user may change it, the issue being found by its labels alone.
                gh issue create --title 'Harnais qualité : Dette qualité du code de main' --label quality-audit --label harness --type Task --body-file $bodyFile
                if ($LASTEXITCODE) { throw 'The creation of the tracking issue failed.' }
            }
        }
        else {
            gh issue edit $issue.number --body-file $bodyFile
            if ($LASTEXITCODE) { throw 'The update of the tracking issue failed.' }
            if ($entries -and $issue.state -ne 'OPEN') { gh issue reopen $issue.number }
            elseif (-not $entries -and $issue.state -eq 'OPEN') { gh issue close $issue.number --reason completed }
            if ($LASTEXITCODE) { throw 'The change of state of the tracking issue failed.' }
        }
    }
    finally {
        Remove-Item -LiteralPath $bodyFile
    }
}

#endregion

$critical = @($entries | Where-Object Section -EQ Critique)
$newPriority = @($entries | Where-Object { $_.Section -eq 'Prioritaire' -and -not $previousKeys.Contains($_.Key) })
if ($critical -or $newPriority) {
    Write-Output "::error::The quality audit found $($critical.Count) critical entry(ies) and $($newPriority.Count) new priority entry(ies)."
    exit 1
}
