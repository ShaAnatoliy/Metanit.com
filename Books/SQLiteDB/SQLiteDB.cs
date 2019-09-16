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

            bool result = false;

            BookContext db = new BookContext();
            SQLiteConnection Connect = db.Database.Connection as SQLiteConnection;
            
            try
            {
                SQLiteCommand Command = new SQLiteCommand(Properties.Resources.CreateTableBook, Connect);
                Connect.Open();
                // string p = Connect.FileName;
                result = Command.ExecuteNonQuery() == 0;

                Command = new SQLiteCommand
                {
                    Connection = Connect,
                    CommandText = @"select count(Id) kzp from books"
                };
                var sqlReader = Command.ExecuteScalar();
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