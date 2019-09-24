using Autofac;
using IoCAutofacConsoleSample.SampleImp;
using IoCAutofacInterfacesSample.SampleInterfaces;
using IoCAutofacLibSample;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IoCAutofacConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterWithCtorInjection();

            Console.ReadKey();
        }
        static void PrintMethodName()
        {
            StackFrame frame = new StackFrame(1);
            Console.WriteLine($"--- {frame.GetMethod().Name} ---");
        }

        static void BasicRegisterTest()
        {
            PrintMethodName();
            ///Tworzymy builder
            var builder = new ContainerBuilder();
            ///Rejestrujemy typ Foo jako IFoo
            builder.RegisterType<Foo>().As<IFoo>();
            ///Budujemy builder do kontenera
            var container = builder.Build();
            ///Testujemy rozwiązanie typu IFoo na Foo
            Console.WriteLine(container.Resolve<IFoo>().GetHello());
        }
        static void RegisterWithCtor()
        {
            PrintMethodName();
            ///Tworzymy builder
            var builder = new ContainerBuilder();
            ///Rejestrujemy typ Foo2 jako IFoo. W trakcie rejestracji wykonujemy kontruktor
            builder.Register(x=> new Foo2("Rafał")).As<IFoo>();
            ///Budujemy builder do kontenera
            var container = builder.Build();
            ///Testujemy rozwiązanie typu IFoo na Foo
            Console.WriteLine(container.Resolve<IFoo>().GetHello());
        }
        static void RegisterWithParameter()
        {
            PrintMethodName();
            var builder = new ContainerBuilder();
            builder.Register<IFoo>((x, p) =>
            {
                var paramValue = p.TypedAs<string>();
                if (String.IsNullOrEmpty(paramValue))
                {
                    return new Foo();
                }
                else
                {
                    return new Foo2(paramValue);
                }
            });
            var container = builder.Build();
            Console.WriteLine(container.Resolve<IFoo>(new TypedParameter(typeof(string),null)).GetHello());
            Console.WriteLine(container.Resolve<IFoo>(new TypedParameter(typeof(string),"Rafał")).GetHello());
        }
        static void RegisterWithoutScope()
        {
            PrintMethodName();
            var builder = new ContainerBuilder();
            builder.Register(x => new Counter()).AsSelf().AsImplementedInterfaces();
            var container = builder.Build();
            Console.WriteLine(container.Resolve<Counter>().GetNext());
            Console.WriteLine(container.Resolve<Counter>().GetNext());
            Console.WriteLine(container.Resolve<ICounter>().GetNext());
        }
        static void RegisterWithScope()
        {
            PrintMethodName();
            var builder = new ContainerBuilder();
            builder.Register(x => new Counter()).AsSelf().InstancePerMatchingLifetimeScope();
            var container = builder.Build();
            Console.WriteLine(container.Resolve<Counter>().GetNext());
            using (var scope = container.BeginLifetimeScope("test"))
            {
                Console.WriteLine(scope.Resolve<Counter>().GetNext());
                Console.WriteLine(scope.Resolve<Counter>().GetNext());
            }
            Console.WriteLine(container.Resolve<Counter>().GetNext());
            Console.WriteLine(container.Resolve<Counter>().GetNext());
        }
        static void RegisterSignleInstance()
        {
            PrintMethodName();
            var builder = new ContainerBuilder();
            builder.Register(x => new Counter()).AsSelf().SingleInstance();
            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope("scope1"))
            {
                Console.WriteLine(scope.Resolve<Counter>().GetNext());
                Console.WriteLine(scope.Resolve<Counter>().GetNext());
            }
            Console.WriteLine(container.Resolve<Counter>().GetNext());
        }
        static void RegisterWithCtorInjection()
        {
            PrintMethodName();
            var builder = new ContainerBuilder();
            builder.RegisterType<Foo>().As<IFoo>();
            builder.RegisterType<ConsolePrinter>().AsSelf();
            var container = builder.Build();
            container.Resolve<ConsolePrinter>().Print();
        }
        static void RegisterWithAssemblyScaning()
        {
            PrintMethodName();
            var builder = new ContainerBuilder();
            builder.Register(x=> new Foo2("Rafał")).As<IFoo>();
          
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(x=>x.GetInterfaces().Contains(typeof(IPrinter))).AsImplementedInterfaces();

            var container = builder.Build();
            container.Resolve<IPrinter>().Print();
        }

        static void RegisterMultiply()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Foo>().As<IFoo>();
            builder.Register(c=>new Foo2("Rafał")).As<IFoo>();
            var container = builder.Build();
            Console.WriteLine(container.Resolve<IFoo>().GetHello());
            foreach (var services in container.Resolve<IEnumerable<IFoo>>())
            {
                Console.WriteLine(services.GetHello());
            }
            
        }
    }
}
