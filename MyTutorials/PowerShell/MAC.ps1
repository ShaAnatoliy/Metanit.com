# https://habr.com/ru/post/242445/

$MAC = Get-WmiObject -Authority  -ComputerName beta-wfm02, test-wfm02 -Class Win32_NetworkAdapterConfiguration -Filter IPEnabled=True  | `
  Select-Object -Property * | SELECT PSComputerName, @{Name="IPAddress";Expression={$_.IPAddress.get(0)}}, MACAddress, Description
$MAC

for ($i = 0; $i -lt 10; $i++)
{ 
    $i
}