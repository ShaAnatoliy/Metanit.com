Import-Module (Resolve-Path('ps_modules\modBase'))
$expected = "Hello World"
$result = Base64Encode $expected
$actual = Base64Encode $result -decode $true
Write-Output $result, ">", $actual
