#Requires -Version 7.3
<#
.SYNOPSIS
    Stops the validation version started by Start-Validation.ps1, and removes what it created.

.DESCRIPTION
    Validation environment of choix-implementation/environnement-de-validation-fonctionnelle.md: stops the processes
    started from the validation version (the application and the processes of its embedded browser), never another
    TV Rename, then removes its sources, its build and its folders of data.
#>
[CmdletBinding()]
param(
    # Keeps the folders of data, for the next start with -KeepData.
    [switch] $KeepData
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$validation = Join-Path ([IO.Path]::GetTempPath()) 'tvrename-validation'
$application = Join-Path $validation 'application'

# Found by the folder they run from, which tells them from the TV Rename of the machine.
$processes = @(Get-Process | Where-Object { $_.Path -and $_.Path.StartsWith("$application\", [StringComparison]::OrdinalIgnoreCase) })
foreach ($process in $processes) {
    # The window is asked to close first, so that the application saves what it would on a normal exit.
    if ($process.MainWindowHandle -ne [IntPtr]::Zero -and $process.CloseMainWindow() -and $process.WaitForExit(10000)) {
        continue
    }
    if (-not $process.HasExited) {
        $process.Kill($true)
        $process.WaitForExit(10000) | Out-Null
    }
}
foreach ($process in $processes) {
    $process.Dispose()
}

if (Test-Path $validation) {
    $kept = if ($KeepData) { @('userfiles', 'appdata') } else { @() }
    Get-ChildItem $validation | Where-Object Name -NotIn $kept | Remove-Item -Recurse -Force
    if (-not $KeepData) {
        Remove-Item $validation -Recurse -Force
    }
}
