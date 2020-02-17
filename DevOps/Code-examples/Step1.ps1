function StopProcess([string]$ProcName)
{
    $msg = 'Попытка выгрузки процесса ' + $ProcName + ' из памяти!'
    Write-Output $msg

    Stop-Process -Name $ProcName -Force -ErrorAction SilentlyContinue # PowerShell v2 не понимает: -ErrorAction Ignore

    $getproc = Get-Process -Name $ProcName -ErrorAction SilentlyContinue

    if ($getproc) # Если процес в памяти висит
    {
        while ($getproc)
        {
            Wait-Process -Name $ProcName -Timeout 60 -ErrorAction SilentlyContinue
            $getproc = Get-Process -Name $ProcName -ErrorAction SilentlyContinue
        }
    }

    if ($getproc)
    {
        $msg = 'Процесс ' + $ProcName + ' НЕ ВЫГРУЖЕН из памяти.'
        Write-Error $msg

        return -1
    }

    $msg = 'Процесс ' + $ProcName + ' выгружен из памяти.'
	Write-Output $msg

    return 0
}

chcp 65001

iisreset /stop

StopProcess -ProcName "MobilityStudio"
if ($LASTEXITCODE -ne 0)
{
  Exit $LASTEXITCODE
}

StopProcess -ProcName "mmc"
if ($LASTEXITCODE -ne 0)
{
  Exit $LASTEXITCODE
}

