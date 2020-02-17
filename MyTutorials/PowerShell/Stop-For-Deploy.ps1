function StopProcess
{
  param ([Parameter (Mandatory = $true)] [string] $ProcName)

    Write-Host 'Попытка выгрузки процесса',$ProcName,'из памяти!'

    Stop-Process -Name $ProcName -Force -ErrorAction Ignore 

    $getproc = Get-Process -Name $ProcName -ErrorAction SilentlyContinue

    if ($getproc) # Если процес в памяти висит > Задержка для выгрузки до 1 минуты
    {
        for ( $n = 0; $n -le 20; $n++ ) # меньше или равно 20
        {
            Start-Sleep -Seconds 3 # Время на выгрузку
            $getproc = Get-Process -Name $ProcName -ErrorAction SilentlyContinue
            if ($getproc -eq $null) 
            { 
                $n = 21
            }
        }
    }

    if ($getproc)
    {
        Write-Error 'Процесс',$ProcName,' НЕ ВЫГРУЖЕН из памяти!'
        return -1
    }
    else
    {
        Write-Host 'Процесс',$ProcName,'выгружен из памяти.'
        return 0
    }
}

chcp 65001

iisreset /stop

$ret = StopProcess("MobilityStudio")
if ($ret -ne 0)
{
  Exit -1
}

$ret = StopProcess("mmc")
if ($ret -ne 0)
{
  Exit -1
}

Exit 0
