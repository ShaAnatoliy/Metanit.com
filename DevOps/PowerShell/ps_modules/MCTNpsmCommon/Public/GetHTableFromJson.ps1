function GetHTableFromJson {
	[cmdletbinding()]
	param (
		[Parameter(Mandatory)]
		[string] $PathFile
	)
    
	Add-Type -AssemblyName System.Web.Extensions
	
	$AllKeys = New-Object System.Collections.Hashtable

	if (Test-Path $PathFile) {
		$text = [IO.File]::ReadAllText($PathFile)
		$parser = New-Object Web.Script.Serialization.JavaScriptSerializer
		$parser.MaxJsonLength = $text.length
		# $ht1 = $parser.DeserializeObject($text) # возвращает [System.Object], $ht -is [System.Object] => True
		$ht1 = $parser.Deserialize($text, [System.Collections.Hashtable])  # $ht -is [System.Collections.Hashtable] => True

		if ($ht1 -is [System.Collections.Hashtable] -and $ht1.Count -gt 0 ) {
			$temp1 = GetNewTable($ht1)
			AddChildName -NodeNm "?" -InObj $temp1 -ErrorAction Continue
		}
	}
	else {
		Write-Warning -Message "Не найден файл: «$PathFile»"
	}

	return $AllKeys
}
