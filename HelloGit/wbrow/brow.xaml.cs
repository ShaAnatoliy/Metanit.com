using System;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
//using System.Net.Http;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace wbrow
{

    public partial class wndbrow : Window
    {
        public WebBrowser WB = null;
        private DispatcherTimer timer = null;

        public wndbrow()
        {
            InitializeComponent();
            WB = wb;
            DataContext = new WinBrowVM(this);
        }

        private void WinSizeChanged(object sender, SizeChangedEventArgs e)
        {
            brdTop1.Width = this.ActualWidth - 15;
            brdTop2.Width = this.ActualWidth - 15;
            tbr.Width = this.ActualWidth - 18;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var dc = (DataContext as WinBrowVM);
            if (dc.RunSrv)
            {
                dc.RunSrv = false;
                TimerStart();
                e.Cancel = true;
            }
        }

        private void TimerStart()
        {
            timer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 700);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            timer.IsEnabled = false;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Stop();

            // Прекратить работу HTTP сервера
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(App.LocalUrl + App.port.ToString() + App.CmdShutdown);
            req.Method = "GET";
            req.KeepAlive = false;
            req.AllowAutoRedirect = false;
            req.ContentType = "message/http";
            try { HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); } catch { }
            System.Threading.Thread.Sleep(700);
            Close();
        }

        private void AddrBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var dc = (DataContext as WinBrowVM);
                dc.ViewFile();
            }
        }
    }

    /// <summary>
    /// ViewModel основного окна
    /// </summary>
    public class WinBrowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        internal void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool runSrv = false;
        public bool RunSrv { get { return runSrv; } set { runSrv = value; } }

        wndbrow ThisWindow = null;
        public WinBrowVM(wndbrow pThisWindow)
        {
            ThisWindow = pThisWindow;
        }

        /// <summary>
        /// Если updateProcess = true, то идёт процесс обновления файла
        /// </summary>
        bool updateProcess = false;
        public bool UpdateProcess { get { return updateProcess; } set { updateProcess = value; } }

        Visibility openDB = Visibility.Hidden;
        public Visibility OpenDB { get { return openDB; } set { openDB = value; } }

        Visibility updatePanel = Visibility.Collapsed;
        public Visibility UpdatePanel { get { return updatePanel; } set { updatePanel = value; } }

        Visibility wbPanelVisi = Visibility.Visible;
        public Visibility WBPanelVisi { get { return wbPanelVisi; } set { wbPanelVisi = value; } }

        string upFile = "";
        public string UpFile { get { return upFile; } set { upFile = value; } }
        string upRefUrl = "";
        public string UpRefUrl
        {
            get { return upRefUrl; }
            set
            {
                upRefUrl = value;
                addrUrlDb = upRefUrl;
                OnPropertyChanged("AddrUrlDb");
            }
        }
        string addrUrlDb = "";
        public string AddrUrlDb
        {
            get { return addrUrlDb; }
            set
            {
                addrUrlDb = value;
                upRefUrl = addrUrlDb;
                OnPropertyChanged("UpRefUrl");
            }
        }


        string upDescriptFile = "";
        public string UpDescriptFile { get { return upDescriptFile; } set { upDescriptFile = value; } }
        string upMimeText = "";
        public string UpMimeText { get { return upMimeText; } set { upMimeText = value; } }

        public ICommand OkUpFileCmd { get { return new RelayCommand((obj) => OkUpFile()); } }
        void OkUpFile()
        {
            // Записать/Обновить
            string upCmd = App.LocalUrl + App.port.ToString() + "/upfile?";
            upCmd += "file=" + upFile;
            upCmd += "&path=" + upRefUrl;
            upCmd += "&title=" + upDescriptFile;
            upCmd += "&mtype=" + upMimeText;
            ThisWindow.WB.Navigate(upCmd);

            wbPanelVisi = Visibility.Visible;
            OnPropertyChanged("WBPanelVisi");
            updatePanel = Visibility.Collapsed;
            OnPropertyChanged("UpdatePanel");

            updateProcess = false;
            OnPropertyChanged("UpdateProcess");
            addrUrlDb = upRefUrl;
            OnPropertyChanged("AddrUrlDb");
        }

        public ICommand NoUpFileCmd { get { return new RelayCommand((obj) => NoUpFile()); } }
        internal void NoUpFile()
        {
            wbPanelVisi = Visibility.Visible;
            OnPropertyChanged("WBPanelVisi");
            updatePanel = Visibility.Collapsed;
            OnPropertyChanged("UpdatePanel");
            updateProcess = false;
            OnPropertyChanged("UpdateProcess");
        }

        public ICommand ViewFileCmd { get { return new RelayCommand((obj) => ViewFile()); } }
        public void ViewFile()
        {
            if (!runSrv || updateProcess)
                return;

            if (string.IsNullOrEmpty(addrUrlDb))
            {
                MessageBox.Show("Не указан адрес файла в БД!");
                return;
            }
            string getHtml = App.LocalUrl + App.port.ToString() + upRefUrl;
            ThisWindow.WB.Navigate(getHtml);

        }

        public ICommand UpdateFileCmd { get { return new RelayCommand((obj) => UpdateFile()); } }
        public void UpdateFile()
        {
            if (!runSrv || updateProcess) return;

            updateProcess = true;
            OnPropertyChanged("UpdateProcess");

            string filename = "";
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
			{
				CheckFileExists = false,
				CheckPathExists = true,
				Multiselect = false,
				Title = "Выбрать файл для добавления/обновления"
			};
			// dlg.FileName = "sqls"; // Default file name
			// dlg.DefaultExt = ".db3"; // Default file extension
			// dlg.Filter = "SQLite DB (.db3)|*.db3"; // Filter files by extension
			bool? result = dlg.ShowDialog(ThisWindow);
            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                updateProcess = false;
                OnPropertyChanged("UpdateProcess");
                return;
            }

            upFile = filename;
            OnPropertyChanged("UpFile");
            upRefUrl = filename.Replace('\\', '/');
            OnPropertyChanged("UpRefUrl");
            upDescriptFile = System.IO.Path.GetFileName(filename);
            OnPropertyChanged("UpDescriptFile");

            wbPanelVisi = Visibility.Collapsed;
            OnPropertyChanged("WBPanelVisi");
            updatePanel = Visibility.Visible;
            OnPropertyChanged("UpdatePanel");
        }

        public ICommand OnOffSrvCmd { get { return new RelayCommand((obj) => OnOffSrv()); } }
        internal void OnOffSrv()
        {
            if (updateProcess)
            {
                NoUpFile(); // Прекратить редактировать данные по файлу 
                return;
            }

            if (runSrv)
            {
                runSrv = false;
                OnPropertyChanged("RunSrv");
                ThisWindow.WB.Navigate(App.LocalUrl + App.port.ToString() + App.CmdShutdown);
                openDB = Visibility.Hidden;
                OnPropertyChanged("OpenDB");
                return;
            }

            // свободный порт
            bool isAvailable = true;
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            while (isAvailable)
            {
                foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
                {
                    if (tcpi.LocalEndPoint.Port == App.port)
                    {
                        isAvailable = false;
                        break;
                    }
                }
                if (!isAvailable)
                {
                    ++App.port;
                }
                else
                {
                    break;
                }
            }
            if (App.port > 65535)
            {
                MessageBox.Show("На ПК не найден свободный порт!", "Фигасе!", MessageBoxButton.OK, MessageBoxImage.Stop);
                runSrv = false;
                OnPropertyChanged("RunSrv");
                openDB = Visibility.Hidden;
                OnPropertyChanged("OpenDB");
                return;
            }

            // Открыть файл БД
            string filename = "";
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
			{
				CheckFileExists = false,
				CheckPathExists = true,
				Multiselect = false,
				Title = "Создать/Открыть SQLite DB",
				FileName = "sqls", // Default file name
				DefaultExt = ".db3", // Default file extension
				Filter = "SQLite DB (.db3)|*.db3" // Filter files by extension
			};
			bool? result = dlg.ShowDialog(ThisWindow);
            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                return;
            }

            Process myProcess = new Process();
            try
            {
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = "sqls.exe";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.Arguments = "-port=\"" + App.port.ToString() + "\" -db3=\"" + filename + "\"";
                myProcess.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + (char)10 + "sqls.exe", "Произошла ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                runSrv = false;
                OnPropertyChanged("RunSrv");
                openDB = Visibility.Hidden;
                OnPropertyChanged("OpenDB");
                return;
            }

            runSrv = true;
            OnPropertyChanged("RunSrv");
            openDB = Visibility.Visible;
            OnPropertyChanged("OpenDB");

            System.Threading.Thread.Sleep(1000);
            OpenSQLiteDB(filename);
        }

        List<string> mimeList = new List<string>();
        public List<string> MimeList { get { return mimeList; } private set { mimeList = value; } }

        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;

        private void OpenSQLiteDB(string filename)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();
            DataTable dTable = new DataTable();

            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                /*
                if (m_dbConn.State != ConnectionState.Open)
                {
                    MessageBox.Show("БД не открыта");
                    return;
                }
                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT, author TEXT, book TEXT)";
                m_sqlCmd.ExecuteNonQuery();
                */

                SQLiteDataAdapter adapter = new SQLiteDataAdapter("select mtypenm from mtypes", m_dbConn);
                adapter.Fill(dTable);

                mimeList.Clear();

                if (dTable.Rows.Count > 0)
                {
                    foreach (var itm in dTable.Rows)
                    {
                        mimeList.Add((itm as DataRow)[0].ToString());
                    }
                    OnPropertyChanged("MimeList");
                    // for (int i = 0; i < dTable.Rows.Count; i++)
                    //    dgvViewer.Rows.Add(dTable.Rows[i].ItemArray);
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Ошибка получения данных из БД: " + (char)10 + ex.Message);
            }
            finally
            {
                if (m_dbConn != null && m_dbConn.State == ConnectionState.Open)
                {
                    m_dbConn.Close();
                }
            }
        }

        /*
         Добавление данных в БД
        private void btAdd_Click(object sender, EventArgs e)
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }

            AddDataToDb addData = new AddDataToDb();
            if (addData.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_sqlCmd.CommandText = "INSERT INTO Catalog ('author', 'book') values ('" +
                        addData.Author + "' , '" +
                        addData.Book + "')";

                    m_sqlCmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        } */

    }

    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> action, Func<object, bool> canExecute = null)
        {
            execute = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

    }

}
