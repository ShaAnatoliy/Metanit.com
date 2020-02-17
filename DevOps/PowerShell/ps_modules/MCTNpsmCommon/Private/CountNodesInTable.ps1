# CountNodesInTable
function CountNodesInTable {
    param 
    (
        [Parameter(Position = 0)]
        [System.String] $NodeNm,
        [Parameter(Position = 1)]
        [System.Collections.Hashtable] $InObj,
        [Parameter(Position = 2)]
        [int] $CountKeys
    )

    $AddKeyName = ""
    $cnt = 0

    foreach ($name in $InObj.Keys) {

        if ($NodeNm -eq "?") {
            $AddKeyName = $name
        }
        else {
            $AddKeyName = $NodeNm + "." + $name
        }

        if (($null -eq $InObj[$name]) -or (IsJsonEnData($InObj[$name].GetType().FullName))) {
            $cnt++
        }
        else {
            $temp1 = GetNewTable($InObj[$name])
            $cnt = CountNodesInTable -NodeNm $AddKeyName -InObj $temp1 -CountKeys $cnt  -ErrorAction Continue
        }
    }
    return $CountKeys + $cnt
}
