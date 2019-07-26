--  <!-- Если ошибка создания экземпляра LocalDB - 50
-- C:\>sqllocaldb stop MSSQLLocalDB
-- C:\>sqllocaldb delete MSSQLLocalDB
-- C:\>sqllocaldb create MSSQLLocalDB
-- C:\>sqllocaldb start MSSQLLocalDB<br>
-- .. LocalDB instance "MSSQLLocalDB" started.
 
-- C:\>sqllocaldb i //информация
-- C:\>sqlcmd -S(LocalDB)\MSSQLLocalDB
-- C:\>sqlcmd -S myServer\instanceName -i C:\myScript.sql  // Выполнить скрипт

USE [master]
GO

-- DECLARE @dbname nvarchar;
-- select @dbname = 'что-то'

CREATE DATABASE Bookstore
    ON ( 
	  NAME = Bookstore, 
	  FileName = 'C:\DEV\MYPRJ\METANIT.COM\BOOKSTORE\APP_DATA\BOOKSTORE.MDF',
	  SIZE = 5 MB)
 COLLATE Cyrillic_General_CI_AS
;
GO

USE [Bookstore]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Books]
(
	Id int NOT NULL IDENTITY(1,1),
	Name nvarchar(50) NOT NULL,
	Author nvarchar(50) NOT NULL,
	Price int NOT NULL,
	CONSTRAINT PK_BookID PRIMARY KEY (Id)
)
GO

CREATE TABLE [dbo].[Purchases]
(
	PurchaseId int NOT NULL IDENTITY(1,1),
	Person nvarchar NOT NULL,
	Address nvarchar NOT NULL,
	BookId int NOT NULL,
	Date date NOT NULL,
	CONSTRAINT PK_PurchaseId PRIMARY KEY (PurchaseId),
	CONSTRAINT FK_onBookID FOREIGN KEY (BookId)
        REFERENCES dbo.Books(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE 
)
GO

insert into dbo.Books (Name, Author, Price) values 
( 'Война и мир', 'Л. Толстой', 220 ), 
('Отцы и дети','И. Тургенев', 180), 
('Чайка','А. Чехов', 150)
GO
