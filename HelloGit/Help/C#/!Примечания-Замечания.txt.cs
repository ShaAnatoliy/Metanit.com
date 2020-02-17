// u8Fh@Fqq
/// <summary>
///	 Проверяет существование в таблице PRINT_PIN записи у которой:
/// -поле PHONE равно номеру телефона абонента
/// -поле ORDER_ID равно id наряда
/// </summary>
/// <param name="phone">Номер телефона абонента.</param>
/// <param name="taskKey">Номер наряда в Wfm.</param>
/// <returns></returns>
private bool CheckPinRequest(string phone, string taskKey)
{
	using (var db = new SqlDb(System.Configuration.ConfigurationManager.AppSettings["soDbConnection"]))
	{
		var dt = db.ExecuteTable("SELECT * FROM WFMG.dbo.PIN_PRINT WHERE Phone = @phone and TaskKey=@taskKey",
			new Dictionary<string, object>() { { "@phone", phone }, { "@taskKey", taskKey } });
		if (dt.Rows.Count > 0)
			return true;
	}
	return false;
}

/*
Решение проблемы состоит в том, чтобы синхронизировать потоки и ограничить доступ 
к разделяемым ресурсам на время их использования каким-нибудь потоком. 
Для этого используется ключевое слово lock. Оператор lock определяет блок кода, 
внутри которого весь код блокируется и становится недоступным для других потоков 
до завершения работы текущего потока. И мы можем переделать предыдущий пример следующим образом:
*/
class Program
{
    static int x=0;
    static object locker = new object();
    static void Main(string[] args)
    {
        for (int i = 0; i < 5; i++)
        {
            Thread myThread = new Thread(Count);
            myThread.Name = "Поток " + i.ToString();
            myThread.Start();
        }
 
        Console.ReadLine();
    }
    public static void Count()
    {
        lock (locker)
        {
            x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                x++;
                Thread.Sleep(100);
            }
        }
    }
}
/* 
Для блокировки с ключевым словом lock используется объект-заглушка, 
в данном случае это переменная locker. Когда выполнение доходит до оператора lock, 
объект locker блокируется, и на время его блокировки монопольный доступ к блоку кода 
имеет только один поток. После окончания работы блока кода, 
объект locker освобождается и становится доступным для других потоков.
*/
