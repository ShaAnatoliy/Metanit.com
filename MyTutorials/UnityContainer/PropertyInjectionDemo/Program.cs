using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace PropertyInjectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<ICar, BMW>();
            container.RegisterType<ICar, Audi>("LuxCar");

            var driver = container.Resolve<Driver>();
            driver.RunCar();
            Console.WriteLine();

            var container2 = new UnityContainer();
            // Run-time Configuration
            container2.RegisterType<Driver>(new InjectionProperty("Car", new BMW()));

            var driver2 = container.Resolve<Driver>();
            Console.WriteLine("Run-time Configuration");
            driver2.RunCar();


        }


    }
}
