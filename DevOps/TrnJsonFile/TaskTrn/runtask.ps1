[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

Import-Module .\ps_modules\VstsTaskSdk

$dirS = Get-VstsTaskVariable -Name "Build.SourcesDirectory" -Default "defaultA"
$TF = Get-VstsInput -Name "targetFiles" -Default "Не определено: targetFiles"
$RD = Get-VstsInput -Name "rootDirectory" -Default "Не определено: rootDirectory"

Write-Host Revision 1.0.4.8
Write-Host Build.SourcesDirectory: $dirS
Write-Host targetFiles: $TF
Write-Host rootDirectory: $RD
