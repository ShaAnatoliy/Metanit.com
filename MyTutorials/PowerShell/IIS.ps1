chcp 65001
iisreset /start

Stop-WebSite -Name "Default Web Site"
Start-WebSite -Name "Default Web Site"
