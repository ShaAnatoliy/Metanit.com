using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
 
namespace Serialization
{
    [Serializable]
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
 
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("Tom", 29);
            Person person2 = new Person("Bill", 25);
            Person[] people = new Person[] { person, person2 };
 
            // ������� ������ SoapFormatter
            SoapFormatter formatter = new SoapFormatter();
            // �������� �����, ���� ����� ���������� ��������������� ������
            using (FileStream fs = new FileStream("people.soap", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, people);
 
                Console.WriteLine("������ ������������");
            }
 
            // ��������������
            using (FileStream fs = new FileStream("people.soap", FileMode.OpenOrCreate))
            {
                Person[] newPeople = (Person[])formatter.Deserialize(fs);
 
                Console.WriteLine("������ ��������������");
                foreach (Person p in newPeople)
                {
                    Console.WriteLine("���: {0} --- �������: {1}", p.Name, p.Age);
                }
            }
            Console.ReadLine();
        }
    }
}

