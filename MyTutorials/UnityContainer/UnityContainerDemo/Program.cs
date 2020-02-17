using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace UnityContainerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();

            /* Multiple Registration */
            // MultipleRegistration(container);

            /* Register Named Type */
            // RegisterNamedType(container);

            /* Register Instance */
            // RegisterInstance(container);

            /* Constructor Injection for Multiple Parameters */
            // MultipleParameters(container);

            /* Multiple Constructors */
            // MultipleConstructors(container);

            /* Primitive Type Parameter */
            // PrimitiveTypeParameter(container);

            /* Lifetime Managers */
            LifetimeManagers(container);

            // https://www.tutorialsteacher.com/ioc/dependency-injection
            // Шаблон DI (внедрения зависимостей) включает в себя 3 типа классов:
            // 
        }

        /* Контейнер Unity управляет временем жизни объектов всех зависимостей, которые он разрешает с помощью менеджеров времени жизни. */
        private static void LifetimeManagers(IUnityContainer container)
        {
            /*    The following table lists all the lifetime managers:
             * TransientLifetimeManager - Создает новый объект запрошенного типа каждый раз, когда вы вызываете метод Resolve или ResolveAll.
             * ContainerControlledLifetimeManager - Создает одноэлементный(Singleton) объект при первом вызове метода Resolve или ResolveAll, 
             *                                      а затем возвращает тот же объект при последующих вызовах Resolve или ResolveAll.
             * HierarchicalLifetimeManager - То же, что и ContainerControlledLifetimeManager, единственное отличие состоит в том, 
             *                               что дочерний контейнер может создавать свой собственный одноэлементный объект.
             *                               Родительский и дочерний контейнеры не используют один и тот же одноэлементный объект.
             * PerResolveLifetimeManager - Аналогичен TransientLifetimeManager, но он повторно использует тот же объект зарегистрированного 
             *                             типа в графе рекурсивных объектов.
             * PerThreadLifetimeManager - Создает одноэлементный(Singleton) объект на поток. Он возвращает разные объекты из контейнера в разных потоках.
             * ExternallyControlledLifetimeManager - Поддерживает слабую ссылку на объекты, которые он создает при вызове метода Resolve или ResolveAll. 
             *                                    Не поддерживает время жизни созданных сильных объектов и позволяет вам или сборщику мусора 
             *                                    контролировать время жизни объектов. Это позволяет вам создать свой собственный менеджер жизни.
             */

            // TransientLifetimeManager является менеджером времени жизни по умолчанию.

            container.RegisterType<ICar, BMW>(new HierarchicalLifetimeManager());

            var childContainer = container.CreateChildContainer();

            var driver1 = container.Resolve<Driver>();
            driver1.RunCar();

            var driver2 = container.Resolve<Driver>();
            driver2.RunCar();

            var driver3 = childContainer.Resolve<Driver>();
            driver3.RunCar();

            var driver4 = childContainer.Resolve<Driver>();
            driver4.RunCar();

        }

        private static void PrimitiveTypeParameter(IUnityContainer container)
        {
            container.RegisterType<Driverd1>(new InjectionConstructor(new object[] { new Audid1(), "Steve" }));
            var driver = container.Resolve<Driverd1>(); // Injects Audi and Steve
            driver.RunCar();
        }

        private static void MultipleConstructors(IUnityContainer container)
        {
            /* Вместо применения атрибута [InjectionConstructor] можно передавать объект InjectionConstructor в метод RegisterType () */
            container.RegisterType<Driverd1>(new InjectionConstructor(new Fordd1()));
            // or 
            container.RegisterType<ICarDemo1, Fordd1>();
            container.RegisterType<Driverd1>(new InjectionConstructor(container.Resolve<ICarDemo1>()));
        }

        private static void MultipleParameters(IUnityContainer container)
        {
            container.RegisterType<ICarDemo1, Audid1>();
            container.RegisterType<ICarKey, AudiKey>();
            var driver = container.Resolve<Driverd1>();
            driver.RunCar();
        }

        private static void RegisterInstance(IUnityContainer container)
        {
            ICarDemo1 audi = new Audid1();
            container.RegisterInstance<ICarDemo1>(audi);
            Driverd1 driver1 = container.Resolve<Driverd1>();
            driver1.RunCar();
            driver1.RunCar();
            Driverd1 driver2 = container.Resolve<Driverd1>();
            driver2.RunCar();
        }

        private static void RegisterNamedType(IUnityContainer container)
        {
            // Теперь метод Resolve() вернет объект Audi, если мы укажем ИМЯ отображения.
            container.RegisterType<ICarDemo1, BMWd1>();
            container.RegisterType<ICarDemo1, Audid1>("LuxuryCar");
            // Registers Driver type            
            container.RegisterType<Driverd1>("LuxuryCarDriver", new InjectionConstructor(container.Resolve<ICarDemo1>("LuxuryCar")));
            Driverd1 driver1 = container.Resolve<Driverd1>();// injects BMW
            driver1.RunCar();
            Driverd1 driver2 = container.Resolve<Driverd1>("LuxuryCarDriver");// injects Audi
            driver2.RunCar();
            Console.WriteLine();
        }

        private static void MultipleRegistration(IUnityContainer container)
        {
            container.RegisterType<ICarDemo1, BMWd1>();
            container.RegisterType<ICarDemo1, Audid1>(); // Контейнер внедрит последний зарегистрированный тип, если вы зарегистрируете несколько отображений одного типа
            Driverd1 driver = container.Resolve<Driverd1>();
            driver.RunCar(); // Running Audi
            Console.WriteLine();
        }
    }
}
