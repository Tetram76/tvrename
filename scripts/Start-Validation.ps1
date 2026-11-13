#Requires -Version 7.3
<#
.SYNOPSIS
    Builds the version of a pull request and starts it on this machine, for its functional validation.

.DESCRIPTION
    Validation environment of choix-implementation/environnement-de-validation-fonctionnelle.md: the application,
    built from the sources of the commit alone, started with folders of data of its own (/userfilepath: and
    /appdatapath:), beside a TV Rename already running, whose settings, caches and logs it never reads nor writes.
    Each start begins from empty folders, so that nothing is left from one validation to the next. A validation
    version still running is stopped first. Stopped by Stop-Validation.ps1.

    Requires git and the .NET SDK of the application (global.json, or the target framework of TVRename.csproj).

.EXAMPLE
    ./scripts/Start-Validation.ps1 -PullRequest 18

.EXAMPLE
    ./scripts/Start-Validation.ps1 -PullRequest 18 -KeepData
    With the settings and caches the previous validation left.
#>
[CmdletBinding(DefaultParameterSetName = 'Ref')]
param(
    # The pull request whose head commit is validated.
    [Parameter(Mandatory, ParameterSetName = 'PullRequest', Position = 0)]
    [int] $PullRequest,

    # Or any commit of the repository (branch, tag, hash): the current one by default.
    [Parameter(ParameterSetName = 'Ref')]
    [string] $Ref = 'HEAD',

    # Keeps the folders of data of the previous validation, rather than starting from empty ones.
    [switch] $KeepData
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

$repository = Split-Path $PSScriptRoot -Parent
$validation = Join-Path ([IO.Path]::GetTempPath()) 'tvrename-validation'
$sources = Join-Path $validation 'sources'
$application = Join-Path $validation 'application'
$userFiles = Join-Path $validation 'userfiles'
$appData = Join-Path $validation 'appdata'

# The validation version holds its files: it is stopped before they are replaced.
& (Join-Path $PSScriptRoot 'Stop-Validation.ps1') -KeepData

if ($PullRequest) {
    git -C $repository fetch --quiet origin "pull/$PullRequest/head"
    $Ref = 'FETCH_HEAD'
}
$commit = git -C $repository rev-parse --verify "$Ref^{commit}"

# The sources of that commit alone, away from the working copy: its uncommitted changes, its branch and its other
# files play no part in what is validated.
foreach ($folder in @($sources, $application) + @(if (-not $KeepData) { $userFiles, $appData })) {
    if (Test-Path $folder) {
        Remove-Item $folder -Recurse -Force
    }
}
New-Item -ItemType Directory $sources | Out-Null
$archive = Join-Path $validation 'sources.tar'
git -C $repository archive --format=tar --output $archive $commit
tar -xf $archive -C $sources
Remove-Item $archive

# Built like the CI builds it: without TVRename/msbuild.rsp, which MSBuild reads next to the project and which turns
# the build into the publication of the installer.
dotnet build (Join-Path $sources 'TVRename/TVRename.csproj') -noAutoResponse --configuration Release --output $application --nologo --verbosity quiet

Start-Process -FilePath (Join-Path $application 'TVRename.exe') -WorkingDirectory $application `
    -ArgumentList "`"/userfilepath:$userFiles`"", "`"/appdatapath:$appData`""

Write-Host ''
# The pull request validated, if any: the commit alone does not tell which one is on screen.
if ($PullRequest) {
    Write-Host "Pull request:         #$PullRequest"
}
Write-Host "Version validated:    $commit"
Write-Host "Settings and caches:  $userFiles"
Write-Host "Logs, browser cache:  $appData"
Write-Host 'Stop: ./scripts/Stop-Validation.ps1'
