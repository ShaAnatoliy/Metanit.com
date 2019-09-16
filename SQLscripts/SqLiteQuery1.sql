-- for SQLite

-- CreateTableBook
CREATE TABLE IF NOT EXISTS Books ( 
[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
[image] BLOB, 
[image_format] VARCHAR(10), 
[image_name] NVARCHAR(128), [file] BINARY, [file_format] VARCHAR(10), [file_name] NVARCHAR(128))