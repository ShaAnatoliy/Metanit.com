## ShAℰ
Import-Module (Resolve-Path('ps_modules\MCTNpsmCommon')) -Force

<# 
$ErroActionPreference = "Stop"
SilentlyContinue - не отображать сообщение об ошибке, продолжайте выполнять последующие команды.
Continue - показать любое сообщение об ошибке и попытаться продолжить выполнение команд подпоследовательности.
Inquire - предлагает пользователю продолжить или прекратить действие
Stop - завершить действие с ошибкой.
-----
$PSDefaultParameterValues += @{'*:ErrorAction' = 'Stop'}
#>

# @("C:\DEV\appsettings-types.json", "C:\DEV\s01Combo-Lists-Arrays.ps1.json", "C:\DEV\temp.json" )
$PathFile1 = "\\pc-018026\builds\appsettings.json"
$PathFile2 = "C:\DEV\appsettings-Update.json"

$Tsource = [IO.File]::ReadAllText($PathFile1) | ConvertFrom-Json
#$Tupdate = [IO.File]::ReadAllText($PathFile1) | ConvertFrom-Json

$ct1 = CountKeysFromJson($PathFile1)
$ct2 = CountKeysFromJson($PathFile2)

if ($ct1 -gt 0 -and $ct2 -gt 0) {
    # Можно пытаться обработать JSON файл   $Tsource = GetHTableFromJson($PathFile1)

    $Tupdate = GetHTableFromJson($PathFile2)

    # UPDATE
    # $o0 = $Tsource | ForEach-Object -MemberName "Logging" | ForEach-Object -MemberName "LogLevel"
    foreach ($KeyUpdt in $Tupdate.Keys) {
        $o0 = $null
        $flds = $KeyUpdt.Split('.')
        if ($flds.Count -eq 1) {
            $o0 = $Tsource # root
        }
        else {
            for ($i = 0; $i -lt $flds.Count - 1; $i++) {
                $keyval = $flds[$i]
                if ($null -eq $o0) {
                    $o0 = $Tsource.$keyval
                }
                else {
                    $o0 = $o0.$keyval
                }
            }
        }
        $keyval = $flds[$flds.Count - 1]
        $val = $Tupdate[$KeyUpdt]
        if ($null -ne $o0) {
            if ($null -ne $o0.$keyval) {
                $o0.$keyval = $val # Тут замена
            }
        }
    }

    # ADD ??
    <# foreach ($KeyUpdt in $T2.Keys) {
        if (!$Tsource.GetEnumerator().Where( { $_.Key -contains $KeyUpdt })) {
            # НЕТ > добавить
            $Tsource.Add($KeyUpdt, $T2[$KeyUpdt])
        }
    } #>

    $Tsource | ConvertTo-Json -depth 10 | Out-File -Encoding utf8 "C:\DEV\_out_file.json"

}
