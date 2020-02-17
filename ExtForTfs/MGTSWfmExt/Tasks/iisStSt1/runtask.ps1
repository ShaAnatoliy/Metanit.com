
Import-Module (Resolve-Path('ps_modules\VstsTaskSdk'))

$dirS = Get-VstsTaskVariable -Name "System.ArtifactsDirectory"
$WebSite = Get-VstsInput -Name nameWebSite
$CmdA = Get-VstsInput -Name performAnAction
$UserAdminID = Get-VstsInput -Name userAdmin
$RMhost = Get-VstsInput -Name remoteHost
$secret = Get-VstsInput -Name pswAdmin
$Port = Get-VstsInput -Name Port

$A1 = Get-VstsInput -Name typesOfAuthentication
switch ($A1) {
  "0" {$Authent = "Default"}
  "1" {$Authent = "Basic"}
  "2" {$Authent = "Negotiate"}
  "3" {$Authent = "NegotiateWithImplicitCredential"}
  "4" {$Authent = "Credssp"}
  "5" {$Authent = "Digest"}
  "6" {$Authent = "Kerberos"}
}

$P1 = Get-VstsInput -Name Proto
switch ($P1) {
  "1" {$Proto = "HTTP"}
  "2" {$Proto = "HTTPS"}
}

Write-Host iisStSt 1.0.4.17
Write-Host System.ArtifactsDirectory: $dirS
Write-Host WebSite: $WebSite
Write-Host Command: $CmdA
Write-Host Authent: $Authent';' Port: $Port';' Proto: $Proto

<# 
  if ("$input_targetType".ToUpperInvariant() -eq 'FILEPATH') {
        $contents += ". '$("$input_filePath".Replace("'", "''"))' $input_arguments".Trim()
        Write-Host (Get-VstsLocString -Key 'PS_FormattedCommand' -ArgumentList ($contents[-1]))
    } else {
        $contents += "$input_script".Replace("`r`n", "`n").Replace("`n", "`r`n")
    }
#>

#Stop-WebSite -Name $WebSite
# Restart-Service WinRM
# Test-WsMan COMPUTER
# C:\PS> $SessionOption = New-PSSessionOption -ProxyAccessType IEConfig -ProxyAuthentication Negotiate -ProxyCredential mgts\ashelganov
# New-PSSession -ComputerName beta-wfm02 -SessionOption $SessionOption
# Enter-PSSession -ComputerName beta-wfm02 -Credential "mgts\ashelganov"
# TNC beta-wfm02 –port 5985 #HTTP 5986-HTTPS
# Set-ExecutionPolicy RemoteSigned

if ($UserAdminID -and $RMhost -and $WebSite) {
  
  $secpasswd = ConvertTo-SecureString $secret -AsPlainText -Force
  $mycreds = New-Object System.Management.Automation.PSCredential($UserAdminID, $secpasswd)
   
  if ($Proto -eq "HTTP") {
    $remoteSession = New-PSSession $RMhost -Credential $mycreds -Authentication $Authent -Port $Port
  } else {
    $remoteSession = New-PSSession $RMhost -Credential $mycreds -Authentication $Authent -Port $Port -UseSSL
  }

  # -Authentication Basic | Credssp | Digest | Kerberos | Negotiate | NegotiateWithImplicitCredential
  # Set-Variable -Name "VSTS_HTTP_PROXY_USERNAME" -Value $UserAdminID
  # Set-Variable -Name "VSTS_HTTP_PROXY_PASSWORD" -Value $secret

  Enter-PSSession -Session $remoteSession
  [Console]::OutputEncoding = [System.Text.Encoding]::GetEncoding("cp866")
  try {
    if ( $CmdA -eq "StartSite") {
      Invoke-Command -ScriptBlock { Start-WebSite -Name $WebSite} -Session $remoteSession 
    } else {
      Invoke-Command -ScriptBlock { Stop-WebSite -Name $WebSite} -Session $remoteSession 
    }
  }
  finally {
    Exit-PSSession -Session $remoteSession
    [Console]::OutputEncoding = [System.Text.Encoding]::Default  
  }
  
  
  
  
}
