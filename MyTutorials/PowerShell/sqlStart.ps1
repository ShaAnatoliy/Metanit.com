cls
chcp 65001

Start-Service -Name 'MSSQL$SQLEXPRESS'
Start-Service -Name 'ReportServer$SQLEXPRESS'
Start-Service -Name 'MSSQLFDLauncher$SQLEXPRESS'
Start-Service -Name 'SQLAgent$SQLEXPRESS'

Start-Service -Name 'MSSQL$LOCALDB'
Start-Service -Name 'MSSQLFDLauncher$LOCALDB'
Start-Service -Name 'SQLAgent$LOCALDB'
Start-Service -Name 'ReportServer$LOCALDB'

Start-Service -Name 'MSSQLFDLauncher$LOCALDB2017'
Start-Service -Name 'SSASTELEMETRY$LOCALDB2017'
Start-Service -Name 'SQLTELEMETRY$LOCALDB2017'
Start-Service -Name 'SSISTELEMETRY140'
Start-Service -Name 'SQLAgent$LOCALDB2017'
Start-Service -Name 'MSOLAP$LOCALDB2017'
Start-Service -Name 'MsDtsServer140'
Start-Service -Name 'MSSQL$LOCALDB2017'

Start-Service -Name 'SQLWriter'
Start-Service -Name 'SQLBrowser'
