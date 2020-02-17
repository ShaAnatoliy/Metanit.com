using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace PropertyInjectionDemo
{
    public interface ICar
    {
        int Run();
    }

    public class BMW : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }
    }

    public class Ford : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }
    }

    public class Audi : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }

    }
    public class Driver
    {
        private ICar _car = null;

        public Driver()
        {
        }

        [Dependency]
        public ICar Car { get; set; }

        /* Можно указать имя в атрибуте [Dependency("name")], который затем можно использовать для установки значения свойства. */
        [Dependency("LuxCar")]
        public ICar LuxCar { get; set; }

        public void RunCar()
        {
            Console.WriteLine("Run {0} - {1} mile ", this.Car.GetType().Name, this.Car.Run());
            Console.WriteLine("Run Lux car {0} - {1} mile ", this.LuxCar.GetType().Name, this.LuxCar.Run());
        }
    }

}
