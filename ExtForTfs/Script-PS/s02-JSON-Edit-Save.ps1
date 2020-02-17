
Function IIf($If, $Right, $Wrong) {If ($If) {$Right} Else {$Wrong}}

function IsJsonEnData
{
    param ([string]$TypeName)

    Switch ($TypeName) 
    {
        "System.Boolean" {return $true}
        "System.Object[]" {return $true}
        "System.Int16" {return $true}
        "System.Int32" {return $true}
        "System.Int64" {return $true}
        "System.Decimal" {return $true}
        "System.String" {return $true}
        default {return $false}
    }
}

function GetNewTable 
{
    [OutputType([System.Collections.Hashtable])]
    param ([parameter(position=0)] [System.Collections.Hashtable]$InTable)

    $OutTable = New-Object System.Collections.Hashtable

    $InTable.GetEnumerator() | ForEach-Object {
        $OutTable.Add($_.Key, $_.value)
    }

    return $OutTable
}

function AddChildName
{
    param ([System.Collections.Hashtable]$InObj)

    $Level++

    foreach ($name in $InObj.Keys)
    {
        if ($InObj[$name] -eq $null)
        {
            $AllKeys.Add($name+"($Level)", $null )
            $Level = 0
            continue
        }
        if (IsJsonEnData($InObj[$name].GetType().FullName))
        {
            $AllKeys.Add($name+"($Level)", $InObj[$name])
            $Level = 0
            continue
        }

        #$AllNames += ((IIf $AllNames "." "") + $name)
        
        AddChildName(GetNewTable($InObj[$name]))
    }
}

function GetHashData
{
    [OutputType([System.Collections.Hashtable])]
    param ([parameter(position=0)][string]$PathFile)

    if (Test-Path $PathFile)
    {
        $text = [IO.File]::ReadAllText($PathFile)
        $parser = New-Object Web.Script.Serialization.JavaScriptSerializer
        $parser.MaxJsonLength = $text.length
        # $ht1 = $parser.DeserializeObject($text) # возвращает [System.Object], $ht1 -is [System.Object] => True
        $ht1 = $parser.Deserialize($text, [System.Collections.Hashtable])  # $ht2 -is [System.Collections.Hashtable] => True

        $AllKeys = New-Object System.Collections.Hashtable
        $AllNames = ""
        $Level = 0

        if ($ht1 -is [System.Collections.Hashtable] -and $ht1.Count -gt 0 )
        {
            AddChildName(GetNewTable($ht1))
        }
    
        return $AllKeys
    }
}

Add-Type -AssemblyName System.Web.Extensions

Clear-Host

$T1 = GetHashData("\\pc-018026\builds\appsettings.json")
# $T1 = GetHashData("C:\DEV\temp.json")
# $T2 = GetHashData("C:\DEV\appsettings-Update.json")
# $T2 = GetHashData("C:\DEV\s01Combo-Lists-Arrays.ps1.json")
# "C:\DEV\appsettings-types.json"

$T1
Write-Host "-".PadRight(30,"-")
$T1.Keys
Write-Host "-".PadRight(30,"-")
$T1.Values
Write-Host "-".PadRight(30,"-")
