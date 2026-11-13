#Requires -Version 7.3
<#
.SYNOPSIS
    Builds the version of a pull request and starts it on this machine, for its functional validation.

.DESCRIPTION
    Validation environment of choix-implementation/environnement-de-validation-fonctionnelle.md: the application,
    built from the sources of the commit alone, started as a validation instance (/validation), whose data folder is
    %TEMP%\TVRename: it runs beside a TV Rename already started, and never reads nor writes its files. Each start
    begins from an empty data folder, so that nothing is left from one validation to the next. A validation version
    still running is stopped first. Stopped by Stop-Validation.ps1.

    Requires git and the .NET SDK of the application.

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

    # Keeps the data folder of the previous validation, rather than starting from an empty one.
    [switch] $KeepData
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

$repository = Split-Path $PSScriptRoot -Parent
$validation = Join-Path ([IO.Path]::GetTempPath()) 'tvrename-validation'
$sources = Join-Path $validation 'sources'
$application = Join-Path $validation 'application'

# The validation version holds its files: it is stopped before they are replaced.
& (Join-Path $PSScriptRoot 'Stop-Validation.ps1') -KeepData:$KeepData

if ($PullRequest) {
    git -C $repository fetch --quiet origin "pull/$PullRequest/head"
    $Ref = 'FETCH_HEAD'
}
$commit = git -C $repository rev-parse --verify "$Ref^{commit}"

# The sources of that commit alone, away from the working copy: its uncommitted changes, its branch and its other
# files play no part in what is validated.
New-Item -ItemType Directory $sources | Out-Null
$archive = Join-Path $validation 'sources.tar'
git -C $repository archive --format=tar --output $archive $commit
tar -xf $archive -C $sources
Remove-Item $archive

# Built like the CI builds it: without TVRename/msbuild.rsp, which MSBuild reads next to the project and which turns
# the build into the publication of the installer.
dotnet build (Join-Path $sources 'TVRename/TVRename.csproj') -noAutoResponse --configuration Release --output $application --nologo --verbosity quiet

Start-Process -FilePath (Join-Path $application 'TVRename.exe') -WorkingDirectory $application -ArgumentList '/validation'

Write-Host ''
# The pull request validated, if any: the commit alone does not tell which one is on screen.
if ($PullRequest) {
    Write-Host "Pull request:      #$PullRequest"
}
Write-Host "Version validated: $commit"
Write-Host "Data folder:       $(Join-Path ([IO.Path]::GetTempPath()) 'TVRename')"
Write-Host 'Stop: ./scripts/Stop-Validation.ps1'
