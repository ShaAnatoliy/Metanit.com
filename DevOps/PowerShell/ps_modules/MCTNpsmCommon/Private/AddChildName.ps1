function AddChildName {
    param 
    (
        [Parameter(Position = 0)]
        [System.String] $NodeNm,
        [Parameter(Position = 1)]
        [System.Collections.Hashtable] $InObj
    )

    $AddKeyName = ""

    foreach ($name in $InObj.Keys) {

        if ($NodeNm -eq "?") {
            $AddKeyName = $name
        }
        else {
            $AddKeyName = $NodeNm + "." + $name
        }

        if ($null -eq $InObj[$name]) {
            $AllKeys.Add($AddKeyName, $null)
            continue
        }

        if (IsJsonEnData($InObj[$name].GetType().FullName)) {
            $AllKeys.Add($AddKeyName, $InObj[$name])
            continue
        }

        $temp1 = GetNewTable($InObj[$name])
        AddChildName -NodeNm $AddKeyName -InObj $temp1 -ErrorAction Continue
    }
}
