# Get-Service –name B* | ForEach { $_.Pause() }

$services = Get-Service –name B*
ForEach ($service in $services) 
{
    Write-Output $service.DisplayName
}
# То же
foreach ($service in (Get-Service –name B*)) 
{
  $service.pause()
}

# ! СОВЕТ — не используйте перебор если есть возможность этого не делать. Для примера перепишем наш код приведенный ранее по другому:
# Get-Service –name B* | Suspend-Service
Get-Service –name B* | ForEach { Write-Output $_.DisplayName }

# и пример сортировки будет намного лучше выглядеть если написать его так:
Get-Service b* | Sort Status | select Name,DisplayName,Status

# Список свойств
Get-Command -Verb Format | Format-Wide -Column 1
Get-Command -Verb Format | Format-List
Get-Process -Name mmc | Format-List
Get-Process -Name chr* | Format-List -Property ProcessName,FileVersion,StartTime,Id
Get-Process -Name chr* | Format-Table -Property ProcessName,FileVersion,StartTime,Id