function GetNewTable
{
    param 
    (
        [Parameter(Mandatory, Position=0)]
        [System.Collections.Hashtable]$InTable
    )

    $OutTable = New-Object System.Collections.Hashtable

    $ens = $InTable.GetEnumerator()

    foreach ($item in $ens) {
        $OutTable.Add($item.Key, $item.value)
    }

    return $OutTable
}
