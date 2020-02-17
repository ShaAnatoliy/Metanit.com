function CountKeysFromJson {
	[cmdletbinding()]
	param (
		[Parameter(Mandatory)]
		[string] $PathFile
	)
	
	$retunInt = 0

	if (Test-Path $PathFile) {
		$text = [IO.File]::ReadAllText($PathFile)
		$parser = New-Object Web.Script.Serialization.JavaScriptSerializer
		$parser.MaxJsonLength = $text.length
		$ht1 = $parser.Deserialize($text, [System.Collections.Hashtable])  # $ht -is [System.Collections.Hashtable] => True

		if ($ht1 -is [System.Collections.Hashtable] -and $ht1.Count -gt 0 ) {
			$temp1 = GetNewTable($ht1)
			$retunInt += CountNodesInTable -NodeNm "?" -InObj $temp1 -CountKeys $retunInt -ErrorAction Continue
		}
	}
	else {
		Write-Warning -Message "Не найден файл: «$PathFile»"
	}

	return $retunInt
}