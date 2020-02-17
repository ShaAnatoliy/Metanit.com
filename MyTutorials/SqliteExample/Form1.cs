using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;

namespace SQLite_Project
{
    public partial class Form1 : Form
    {
        /// <summary> Путь к рабочему каталогу, относительно исполняемого файла </summary>
        public string dirWork = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        /// <summary> Путь к файлу базы данных </summary>
        public string dbPath = null;
        /// <summary> Имя таблицы базы данных </summary>
        public string dbTableName = null;
        /// <summary> Путь к изображению </summary>
        public string imgPath = null;
        /// <summary> Имя файла  </summary>
        public string imgName = null;
        /// <summary> Расширение изображения </summary>
        public string imgFormat = null;
        /// <summary> Путь к файлу </summary>
        public string filePath = null;
        /// <summary> Имя файла  </summary>
        public string fileName = null;
        /// <summary> Расширение файла  </summary>
        public string fileFormat = null;
        

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод инициализации переменных
        /// </summary>
        private void InitVariable ()
        {
            dbPath = dirWork + @"\" + textBoxNameDB.Text + ".db"; // задаём путь и мя базы данных
            dbTableName = textBoxNameTable.Text; // задаём имя таблицы, с которой будем работать
        }

        private void buttonCreateDB_Click(object sender, EventArgs e) // кнопка "Создать базу данных"
        {
            InitVariable();
            if (!File.Exists(dbPath) & dbPath != null) // если базы данных нету, то...
            {
                SQLiteConnection.CreateFile(dbPath); // создать базу данных
                MessageBox.Show("База данных создана");
            }
            else
            {
                MessageBox.Show("База данных уже существует");
            }
        }

        private void buttonCreateTable_Click(object sender, EventArgs e) // кнопка "Создать таблицу"
        {
            InitVariable();
            if (!File.Exists(dbPath)) // если нет базы данных, то...
            {
                MessageBox.Show("Создайте базу данных"); // вывести сообщение
                return; // прекратить выполнение
            }
                
            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;")) // в строке указывается к какой базе подключаемся
            {
                // строка запроса, который надо будет выполнить на базе
                string commandText = "CREATE TABLE IF NOT EXISTS " + dbTableName + "( [id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, [image] BLOB, [image_format] VARCHAR(10), " +
                    "[image_name] NVARCHAR(128), [file] BINARY, [file_format] VARCHAR(10), [file_name] NVARCHAR(128))"; // создать таблицу, если её нет
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                int _result = Command.ExecuteNonQuery(); // _result для if-else
                // или этот код: Command.ExecuteNonQuery();
                Connect.Close();

                // ниже необязательный код if-else
                if (_result == 0) // если таблица создана, то...
                {
                    MessageBox.Show("Таблица создана");
                }
                else
                {
                    MessageBox.Show("Таблица '"+ dbTableName + "' в базе данных уже существует");
                }
            }
        }

        private void buttonBrowseImg_Click(object sender, EventArgs e) // кнопка "Выбрать" (изображение)
        {
            // допустимые расширения у диалогового окна выбора файла
            openFileDialogImg.Filter = "Изображения|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.gif;*.ico";

            // Если нажата любая кнопка кроме "ОК", то ничего не произойдёт
            if (openFileDialogImg.ShowDialog() != DialogResult.OK)
                return; // ничего, иначе...

            imgPath = openFileDialogImg.FileName; // записываем в переменную путь к изображению

            textBoxImg.Text = openFileDialogImg.FileName; // выводим в текстовом поле путь к изображению (для наглядности)

        }

        private void buttonBrowseFile_Click(object sender, EventArgs e) // кнопка "Выбрать" (файл)
        {
            // выход, если была нажата кнопка Отмена и прочие (кроме ОК)
            if (openFileDialogFile.ShowDialog() != DialogResult.OK)
                return;
            
            filePath = openFileDialogFile.FileName; // записываем в переменную путь к файлу
            fileFormat = Path.GetExtension(openFileDialogFile.FileName).ToLower(); // запишем в переменную расширение файла в нижнем регистре
            fileName = Path.GetFileName(openFileDialogFile.FileName); // запишем в переменную имя файла

            textBoxFile.Text = openFileDialogFile.FileName; // выводим в текстовом поле путь к файлу (для наглядности)
        }

        private void buttonAddImg_Click(object sender, EventArgs e) // кнопка "Добавить в базу" (изображение)
        {
            InitVariable();
            if (textBoxImg.Text == "") // если не указан путь к изображению, то...
                return; // прекратить выполнение

            // Конвертируем изображение в байты byte[]
            byte[] _imageBytes = null;
            FileInfo _imgInfo = new FileInfo(imgPath); // загрузим изображение
            long _numBytes = _imgInfo.Length; // вычислим длину
            FileStream _fileStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read); // откроем изображение на чтение
            BinaryReader _binReader = new BinaryReader(_fileStream);
            _imageBytes = _binReader.ReadBytes((int)_numBytes); // изображение в байтах

            imgFormat = Path.GetExtension(imgPath).Replace(".", "").ToLower(); // запишем в переменную расширение изображения в нижнем регистре, не забыв удалить (Replace) точку перед расширением
            imgName = Path.GetFileName(openFileDialogImg.FileName).Replace(Path.GetExtension(imgPath), ""); // запишем в переменную имя файла, не забыв удалить (Replace) расширение с точкой

            // записываем информацию в базу данных
            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
            {
                string commandText = "INSERT INTO " + dbTableName + " ([image], [image_format], [image_name]) VALUES(@image, @format, @name)";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Command.Parameters.AddWithValue("@image", _imageBytes);
                Command.Parameters.AddWithValue("@format", imgFormat);
                Command.Parameters.AddWithValue("@name", imgName);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
                MessageBox.Show("Изображение добавлено в базу данных");
            }
        }

        private void buttonViewImg_Click(object sender, EventArgs e) // кнопка "Посмотреть" (изображение)
        {
            InitVariable();
            if (!File.Exists(dbPath)) // если нет базы данных, то...
            {
                MessageBox.Show("Создайте базу данных"); // вывести сообщение
                return; // прекратить выполнение
            }

            // получаем данные их БД
            List<byte[]> _imageList = new List<byte[]>(); // сделав запрос к БД мы получим множество строк в ответе, поэтому мы их сможем загнать в массив/List
            List<string> _imgFormatList = new List<string>();

            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
            {
                Connect.Open();
                SQLiteCommand Command = new SQLiteCommand
                {
                    Connection = Connect,
                    CommandText = @"SELECT * FROM " + dbTableName + " WHERE [image_format] NOT NULL" // выборка записей с заполненной ячейкой формата изображения
                };
                SQLiteDataReader sqlReader = Command.ExecuteReader();
                byte[] _dbImageByte = null;
                string _dbImageFormat = null;
                while (sqlReader.Read()) // считываем и вносим в лист все параметры
                {
                    _dbImageByte = (byte[])sqlReader["image"]; // читаем строки с изображениями, которые хранятся в байтовом формате
                    _imageList.Add(_dbImageByte); // добавляем в List
                    _dbImageFormat = sqlReader["image_format"].ToString().TrimStart().TrimEnd(); // читаем строки с форматом изображений
                    _imgFormatList.Add(_dbImageFormat); // добавляем в List
                }
                Connect.Close();
            }

            if (_imageList.Count == 0) // если в базе нет записей с изображениями (пустой список), то...
            {
                MessageBox.Show("В базе данных нет изображений"); 
                return; // завершить работу метода
            }

            // конвертируем бинарные данные в изображение
            byte[] _imageBytes = _imageList[0]; // так как SQLite вернёт список изображений из БД, то из листа берём первое с индексом '0'
            MemoryStream ms = new MemoryStream(_imageBytes);
            Image _newImg = Image.FromStream(ms);

            // сохраняем изоражение на диск
            string _imgFormat = _imgFormatList[0]; // получаем расширение текущего изображения хранящееся в БД
            string _newImageSaved = dirWork + @"\" + String.Format("{0:yyMMddHHmmss}", DateTime.Now) + "." + _imgFormat; // задаём путь сохранения и имя нового изображения
            if (_imgFormat == "jpeg" || _imgFormat == "jpg") // если расширение равно указанному, то...
                _newImg.Save(_newImageSaved, System.Drawing.Imaging.ImageFormat.Jpeg); // задаём указанный формат: ImageFormat
            else if (_imgFormat == "png")
                _newImg.Save(_newImageSaved, System.Drawing.Imaging.ImageFormat.Png);
            else if (_imgFormat == "bmp")
                _newImg.Save(_newImageSaved, System.Drawing.Imaging.ImageFormat.Bmp);
            else if (_imgFormat == "gif")
                _newImg.Save(_newImageSaved, System.Drawing.Imaging.ImageFormat.Gif);
            else if (_imgFormat == "ico")
                _newImg.Save(_newImageSaved, System.Drawing.Imaging.ImageFormat.Icon);
            else if (_imgFormat == "tiff" || _imgFormat == "tif")
                _newImg.Save(_newImageSaved, System.Drawing.Imaging.ImageFormat.Tiff);
            // и т.д., можно все if заменить на одну строку "_newImg.Save(_newImageSaved)", насколько это правильно сказать не могу, но работает

            // открыть изображение на просмотр программой по умолчанию
            Process.Start(_newImageSaved);
      
        }

        private void buttonAddFile_Click(object sender, EventArgs e) // кнопка "Добавить в базу" (файл)
        {
            InitVariable();
            if (textBoxFile.Text == "") // если не указан путь к изображению, то...
                return; // прекратить выполнение

            // Конвертируем файл в байты byte[]
            byte[] _fileBytes = null;
            FileInfo _fileInfo = new FileInfo(filePath); // загрузим файл
            long _numBytes = _fileInfo.Length; // вычислим длину
            FileStream _fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read); // откроем файл на чтение
            BinaryReader _binReader = new BinaryReader(_fileStream);
            _fileBytes = _binReader.ReadBytes((int)_numBytes); // файл в байтах

            fileFormat = Path.GetExtension(filePath).Replace(".", "").ToLower(); // запишем в переменную расширение файла в нижнем регистре, не забыв удалить (Replace) точку перед расширением
            fileName = Path.GetFileName(openFileDialogFile.FileName).Replace(Path.GetExtension(filePath), ""); // запишем в переменную имя файла, не забыв удалить (Replace) расширение с точкой

            // записываем информацию в базу данных
            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
            {
                string commandText = "INSERT INTO " + dbTableName + " ([file], [file_format], [file_name]) VALUES(@file, @format, @name)";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Command.Parameters.AddWithValue("@file", _fileBytes);
                Command.Parameters.AddWithValue("@format", fileFormat);
                Command.Parameters.AddWithValue("@name", fileName);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
                MessageBox.Show("Файл добавлен в базу данных");
            }
        }

        private void buttonViewFile_Click(object sender, EventArgs e) // кнопка "Посмотреть" (файл)
        {
            InitVariable();
            if (!File.Exists(dbPath)) // если нет базы данных, то...
            {
                MessageBox.Show("Создайте базу данных"); // вывести сообщение
                return; // прекратить выполнение
            }

            // получаем данные их БД
            List<byte[]> _fileList = new List<byte[]>(); // сделав запрос к БД мы получим множество строк в ответе, поэтому мы их сможем загнать в массив/List
            List<string> _fileFormatList = new List<string>();

            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
            {
                Connect.Open();
                SQLiteCommand Command = new SQLiteCommand
                {
                    Connection = Connect,
                    CommandText = @"SELECT * FROM " + dbTableName + " WHERE [file_format] NOT NULL" // выборка записей с заполненной ячейкой формата файла
                };
                SQLiteDataReader sqlReader = Command.ExecuteReader();
                byte[] _dbFileByte = null;
                string _dbFileFormat = null;
                while (sqlReader.Read()) // считываем и вносим в лист все строки полученных данных из БД
                {
                    _dbFileByte = (byte[])sqlReader["file"]; // читаем строки с файлами, которые хранятся в байтовом формате
                    _fileList.Add(_dbFileByte); // добавляем в List
                    _dbFileFormat = sqlReader["file_format"].ToString().TrimStart().TrimEnd(); // читаем строки с форматом файлов
                    _fileFormatList.Add(_dbFileFormat); // добавляем в List
                }
                Connect.Close();
            }

            if (_fileList.Count == 0) // если в базе нет записей с файлами (пустой список), то...
            {
                MessageBox.Show("В базе данных нет файлов");
                return; // завершить работу метода
            }

            // сохранить файл на диск
            byte[] _fileBytes = _fileList[0]; // получаем массив байтов файла, который в БД (первый из списка)
            string _fileFormat = _fileFormatList[0]; // получаем расширение файла (первый из списка)
            string _newFileSaved = dirWork + @"\" + String.Format("{0:yyMMddHHmmss}", DateTime.Now) + "." + _fileFormat; // задаём путь сохранения файла с именем и расширение
            FileStream fileStream = new FileStream(_newFileSaved, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter binWriter = new BinaryWriter(fileStream);
            binWriter.Write(_fileBytes);
            binWriter.Close();

            // открыть файл на просмотр программой по умолчанию
            Process.Start(_newFileSaved);
        }

        private void buttonUpdateData_Click(object sender, EventArgs e) // кнопка "Обновить данные"
        {
            InitVariable();

            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
            {
                string commandText = "UPDATE [" + dbTableName + "] SET [file_name] = @value WHERE [id] = @id";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Command.Parameters.AddWithValue("@value", "Имя файла " + String.Format("{0:yyMMddHHmmss}", DateTime.Now));
                Command.Parameters.AddWithValue("@id", textBoxUpdateData.Text);
                Connect.Open();
                // Command.ExecuteNonQuery(); // можно эту строку вместо двух последующих строк
                Int32 _rowsUpdate = Command.ExecuteNonQuery(); // sql возвращает сколько строк обработано
                MessageBox.Show("Обновлено строк: " + _rowsUpdate);
                Connect.Close();
            }
        }

        private void buttonDeleteData_Click(object sender, EventArgs e) // кнопка "Удалить данные"
        {
            InitVariable();

            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;"))
            {
                string commandText = "DELETE FROM [" + dbTableName + "] WHERE [id] = @id LIMIT 1"; // LIMIT в SQLite аналог TOP в MS SQL
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Command.Parameters.AddWithValue("@id", textBoxDeleteData.Text);
                Connect.Open();
                // Command.ExecuteNonQuery(); // можно эту строку вместо двух последующих строк
                Int32 _rowsUpdate = Command.ExecuteNonQuery(); // sql возвращает сколько строк обработано
                MessageBox.Show("Удалено строк: " + _rowsUpdate);
                Connect.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (textBoxNameDB.Text == "" || textBoxNameTable.Text == "" || textBoxImg.Text == "" || textBoxFile.Text == "" || textBoxDeleteData.Text == ""
                || textBoxUpdateData.Text == "") // если хоть одно поле не заполнено, то...
            {
                buttonCreateDB.Enabled = buttonCreateTable.Enabled = buttonAddImg.Enabled = buttonAddFile.Enabled = buttonViewImg.Enabled = buttonViewFile.Enabled =
                    buttonUpdateData.Enabled = buttonDeleteData.Enabled = false; // отключить все кнопки
                label6.Visible = true; // отобразить предупреждение о заполнение полей
            }
            else // иначе...
            {
                buttonCreateDB.Enabled = buttonCreateTable.Enabled = buttonAddImg.Enabled = buttonAddFile.Enabled = buttonViewImg.Enabled = buttonViewFile.Enabled =
                    buttonUpdateData.Enabled = buttonDeleteData.Enabled = true; // включить все кнопки
                label6.Visible = false; // скрыть предупреждение о заполнение полей
            }

        }
    }
}
