﻿CREATE TABLE [dbo].[Books]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL,
	[Author] NVARCHAR(50) NOT NULL,
	[Price] INT NOT NULL
)
