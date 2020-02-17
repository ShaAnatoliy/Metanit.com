using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace ResolverOverrideDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer().RegisterType<ICar, BMW>();

            var driver1 = container.Resolve<Driver>(); // Injects registered ICar type
            driver1.RunCar();
            Console.WriteLine();

            // Overrides the registered ICar type 
            var driver2 = container.Resolve<Driver>(new ParameterOverride("car", new Ford()));
            driver2.RunCar();
            Console.WriteLine();

            // Override Multiple Parameters
            var driver3 = container.Resolve<Driver>(new ResolverOverride[] {
                new ParameterOverride("car1", new Ford()),
                new ParameterOverride("car2", new BMW()),
                new ParameterOverride("car3", new Audi()) });
            driver3.RunCar();
            Console.WriteLine();

            // PropertyOverride
            //var container = new UnityContainer();
            ////Configure the default value of the Car property
            //container.RegisterType<Driver>(new InjectionProperty("Car", new BMW()));
            //var driver1 = container.Resolve<Driver>();
            //driver1.RunCar();
            ////Override the default value of the Car property
            //var driver2 = container.Resolve<Driver>( new PropertyOverride("Car", new Audi() ));
            //driver2.RunCar();

            // DependencyOverride
            //var container = new UnityContainer().RegisterType<ICar, BMW>();
            //var driver1 = container.Resolve<Driver>();
            //driver1.RunCar();
            ////Override the dependency
            //var driver2 = container.Resolve<Driver>(new DependencyOverride<ICar>(new Audi()));
            //driver2.RunCar();



        }
    }
}
