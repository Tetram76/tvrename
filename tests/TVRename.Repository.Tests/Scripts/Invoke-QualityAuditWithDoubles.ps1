#Requires -Version 7.3
<#
.SYNOPSIS
    Runs scripts/Update-QualityAuditIssue.ps1 against doubles of SonarQube Cloud and of GitHub, for its tests.

.DESCRIPTION
    The fixture (JSON) gives what the doubles answer: the open issues of main and those of its new code, the hotspots,
    the quality gate, the tracking issue found by its labels, the style log and the mutation report. The doubles replace
    Invoke-RestMethod and gh, a function taking precedence over a command of the same name: nothing reaches the
    network. The result (JSON) gives the exit code of the script, the gh commands it ran and the body it wrote.
#>
param(
    [Parameter(Mandatory)] [string] $Fixture,
    [Parameter(Mandatory)] [string] $Result
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$global:audit = Get-Content -Raw $Fixture | ConvertFrom-Json
$global:ghCalls = [Collections.Generic.List[string]]::new()
$global:writtenBody = $null

function global:Invoke-RestMethod {
    param($Headers, $TimeoutSec, $Uri)
    $audit = $global:audit
    if ($Uri -like '*/qualitygates/project_status*') { return [pscustomobject]@{ projectStatus = $audit.gate } }
    $page = [int] ([regex]::Match($Uri, '[?&]p=(\d+)').Groups[1].Value)
    $items, $property = if ($Uri -like '*/hotspots/search*') { @($audit.hotspots), 'hotspots' }
    elseif ($Uri -like '*sinceLeakPeriod=true*') { @($audit.newIssues), 'issues' }
    else { @($audit.issues), 'issues' }
    $pageItems = @($items | Select-Object -Skip (($page - 1) * 500) -First 500)
    [pscustomobject]@{ paging = [pscustomobject]@{ total = $items.Count }; $property = $pageItems }
}

function global:gh {
    $global:LASTEXITCODE = 0
    $global:ghCalls.Add(($args[0..1] -join ' '))
    if ($args[1] -eq 'list') {
        $tracking = $global:audit.tracking
        return ConvertTo-Json -Compress -Depth 3 @(if ($tracking) { $tracking })
    }
    $bodyFile = [array]::IndexOf($args, '--body-file')
    if ($bodyFile -ge 0) { $global:writtenBody = Get-Content -Raw $args[$bodyFile + 1] }
}

$root = git rev-parse --show-toplevel
$styleLog = New-TemporaryFile
$report = New-TemporaryFile
try {
    Set-Content -LiteralPath $styleLog -Value @($audit.styleLog)
    $arguments = @{ StyleLog = $styleLog.FullName; StyleRules = @($audit.styleRules); Update = [bool] $audit.update }
    if ($audit.mutationReport) {
        Set-Content -LiteralPath $report -Value (ConvertTo-Json -Depth 10 $audit.mutationReport)
        $arguments.MutationReport = $report.FullName
    }
    $env:SONAR_TOKEN = 'test'
    $global:LASTEXITCODE = 0
    $output = & "$root/scripts/Update-QualityAuditIssue.ps1" @arguments
    $exitCode = $LASTEXITCODE
    [pscustomobject]@{ exitCode = $exitCode; ghCalls = @($ghCalls); body = $writtenBody; output = ($output -join "`n") } |
        ConvertTo-Json -Depth 3 | Set-Content -LiteralPath $Result
}
finally {
    Remove-Item -LiteralPath $styleLog, $report
}
