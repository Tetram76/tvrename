#Requires -Version 7.3
<#
.SYNOPSIS
    Stops the validation version started by Start-Validation.ps1, and removes what it created.

.DESCRIPTION
    Validation environment of choix-implementation/environnement-de-validation-fonctionnelle.md: stops the processes
    started from the validation version (the application and the processes of its embedded browser), never another
    TV Rename, then removes its sources, its build and the data folder of the validation instance (%TEMP%\TVRename).
#>
[CmdletBinding()]
param(
    # Keeps the data folder, for the next start with -KeepData.
    [switch] $KeepData
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$validation = Join-Path ([IO.Path]::GetTempPath()) 'tvrename-validation'
$application = Join-Path $validation 'application'
# Set by the validation build itself (PathManager.ValidationAppDataPath).
$dataFolder = Join-Path ([IO.Path]::GetTempPath()) 'TVRename'

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

foreach ($folder in @($validation) + @(if (-not $KeepData) { $dataFolder })) {
    if (Test-Path $folder) {
        Remove-Item $folder -Recurse -Force
    }
}
