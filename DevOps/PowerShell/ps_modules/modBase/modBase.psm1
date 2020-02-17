function Base64Encode {
    param(
       [Parameter(Mandatory)][string] $text,
       [bool] $decode = $false
    )
 
    if ($decode) {
       $bytes = [Convert]::FromBase64String($text)
       $data = [System.Text.Encoding]::Unicode.GetString($bytes)
    }
    else {
       $bytes = [System.Text.Encoding]::Unicode.GetBytes($text)
       $data = [Convert]::ToBase64String($bytes)    
    }
 
    return $data
 }

 