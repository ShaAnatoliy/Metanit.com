cls
chcp 65001

Stop-Service -Name 'SQLWriter'
Stop-Service -Name 'SQLBrowser'

Stop-Service -Name 'ReportServer$LOCALDB'
Stop-Service -Name 'MSSQLFDLauncher$LOCALDB'
Stop-Service -Name 'SQLAgent$LOCALDB'
Stop-Service -Name 'MSSQL$LOCALDB'

Stop-Service -Name 'ReportServer$SQLEXPRESS'
Stop-Service -Name 'MSSQLFDLauncher$SQLEXPRESS'
Stop-Service -Name 'SQLAgent$SQLEXPRESS'
Stop-Service -Name 'MSSQL$SQLEXPRESS'

Stop-Service -Name 'MSSQLFDLauncher$LOCALDB2017'
Stop-Service -Name 'SSASTELEMETRY$LOCALDB2017'
Stop-Service -Name 'SQLTELEMETRY$LOCALDB2017'
Stop-Service -Name 'SSISTELEMETRY140'
Stop-Service -Name 'SQLAgent$LOCALDB2017'
Stop-Service -Name 'MSOLAP$LOCALDB2017'
Stop-Service -Name 'MsDtsServer140'
Stop-Service -Name 'MSSQL$LOCALDB2017'
