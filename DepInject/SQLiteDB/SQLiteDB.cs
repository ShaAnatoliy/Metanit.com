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
        public static string CreateTables()
        {
            // string path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

            string connectFileName = "";

            BookContext db = new BookContext();
            SQLiteConnection Connect = db.Database.Connection as SQLiteConnection;
            
            try
            {
                SQLiteCommand Command = new SQLiteCommand(Properties.Resources.CreateTableBook, Connect);
                Connect.Open();

                connectFileName = Connect.FileName; // Для глобального параметра: HttpContext.Current.Application[Properties.Resources.Db3FilePathName1]

                Command.ExecuteNonQuery();

                Command = new SQLiteCommand
                {
                    Connection = Connect,
                    CommandText = @"select count(Id) kzp from books"
                };

                SQLiteDataReader dtr = Command.ExecuteReader();
                dtr.Read();
                if (dtr.GetInt32(0) == 0) // Если в таблице нет записей -> Добавить
                {
                    Command = new SQLiteCommand
                    {
                        Connection = Connect,
                        CommandText = Properties.Resources.InsertToBooks
                    };
                    Command.ExecuteNonQuery();
                }
            }
            finally
            {
                Connect.Close();
                db.Dispose();
            }

            return connectFileName; 
        }
    }


}