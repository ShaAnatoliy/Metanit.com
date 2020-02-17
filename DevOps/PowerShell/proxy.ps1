netsh winhttp set proxy "http://proxysg02.***.corp.net:8080"
$Wcl=New-Object System.Net.WebClient
$Creds=Get-Credential
$Wcl.Proxy.Credentials=$Creds
