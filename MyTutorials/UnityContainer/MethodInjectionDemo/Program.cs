using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace MethodInjectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<ICar, BMW>();

            var driver = container.Resolve<Driver>();
            driver.RunCar();

            // Run-time Configuration
            var container2 = new UnityContainer();
            container2.RegisterType<Driver>(new InjectionMethod("UseCar", new Audi()));

            //to specify multiple parameters values
            container2.RegisterType<Driver>(new InjectionMethod("UseCar", new object[] { new Audi() }));

            var driver2 = container2.Resolve<Driver>();
            driver2.RunCar();

        }

    }
}
