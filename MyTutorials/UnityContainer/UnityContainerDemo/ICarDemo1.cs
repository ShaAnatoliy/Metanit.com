using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace UnityContainerDemo
{
    public interface ICarDemo1
    {
        int Run();
    }

    public class BMWd1 : ICarDemo1
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }
    }

    public class Fordd1 : ICarDemo1
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }
    }

    public class Audid1 : ICarDemo1
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }

    }

    public interface ICarKey
    {

    }

    public class BMWKey : ICarKey
    {

    }

    public class AudiKey : ICarKey
    {

    }

    public class FordKey : ICarKey
    {

    }

    /* Constructor Injection for Multiple Parameters */
    //public class Driver
    //{
    //    private ICar _car = null;
    //    private ICarKey _key = null;

    //    public Driver(ICar car, ICarKey key)
    //    {
    //        _car = car;
    //        _key = key;
    //    }

    //    public void RunCar()
    //    {
    //        Console.WriteLine("Running {0} with {1} - {2} mile ", _car.GetType().Name, _key.GetType().Name, _car.Run());
    //    }
    //}

    /* Multiple Constructors */
    /* Если класс включает несколько конструкторов, то используйте атрибут [InjectionConstructor], чтобы указать, какой конструктор использовать для внедрения конструкции. */
    //public class Driver
    //{
    //    private ICar _car = null;

    //    [InjectionConstructor]
    //    public Driver(ICar car)
    //    {
    //        _car = car;
    //    }

    //    public Driver(string name)
    //    {
    //    }

    //    public void RunCar()
    //    {
    //        Console.WriteLine("Running {0} - {1} mile ", _car.GetType().Name, _car.Run());
    //    }
    //}

    /* Primitive Type Parameter */
    public class Driverd1
    {
        private ICarDemo1 _car = null;
        private string _name = string.Empty;

        public Driverd1(ICarDemo1 car, string driverName)
        {
            _car = car;
            _name = driverName;
        }

        public void RunCar()
        {
            Console.WriteLine("{0} is running {1} - {2} mile ", _name, _car.GetType().Name, _car.Run());
        }
    }

}
