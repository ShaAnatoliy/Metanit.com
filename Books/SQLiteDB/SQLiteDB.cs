using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using Books.Models;

namespace Books
{
    public static class SQLiteDB
    {
        public static bool CreateTables()
        {
            // string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

            // строка запроса, который надо будет выполнить на базе
            string commandText = "CREATE TABLE IF NOT EXISTS Books ( [id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [image] BLOB, [image_format] VARCHAR(10), " +
                "[image_name] NVARCHAR(128), [file] BINARY, [file_format] VARCHAR(10), [file_name] NVARCHAR(128))"; // создать таблицу, если её нет

            bool result = false;

            BookContext db = new BookContext();
            SQLiteConnection Connect = db.Database.Connection as SQLiteConnection;
            
            try
            {
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();

                // string p = Connect.FileName;

                result = Command.ExecuteNonQuery() == 0; 

            }
            finally
            {
                Connect.Close();
                db.Dispose();
            }

            return result; // true - Таблица создана
        }
    }
}