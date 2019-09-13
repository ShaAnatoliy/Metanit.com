using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Books
{
    public static class SQLiteDB
    {
        /// <summary> Путь к рабочему каталогу, относительно исполняемого файла </summary>
        static string dirWork { get; set; }
        /// <summary> Путь к файлу базы данных </summary>
        static string dbPath { get; set; }
        /// <summary> Имя таблицы базы данных </summary>
        static string dbTableName { get; set; }


    }
}