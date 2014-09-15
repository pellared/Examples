using ApiLibrary;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using Pellared.Owned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiProblemInApiLibrary
{
    class Program
    {
        private static IContainer container;

        class SuperSpeed : ISpeed
        {
            public int KilometeresPerHour
            {
                get
                {
                    return 1000;
                }
                set { }
            }
        }

        static void Main(string[] args)
        {
            Bootstrap();

            Console.WriteLine("Resolving from ApiLibrary:");
            var drive = new Drive { MotorSpeed = 100 };
            drive.StartMotor();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Resolving from Application:");
            container.Resolve<OwnedSample>(); // from this application
        }

        private static void Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterOwned();
            RegisterApplicationTypes(builder);

            Bootstrapper.RegisterSpeed<SuperSpeed>();

            container = builder.Build();
            
            //ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container)); // if using the Current Locator
            //Bootstrapper.Locator = new AutofacServiceLocator(container); // we could also make a collection of Locators if it would be neeeded
        }

        private static void RegisterApplicationTypes(ContainerBuilder builder)
        {
            builder.Register(_ => new Some()).As<ISome>();
            builder.RegisterType<Some>().As<IWithArg>();
            builder.RegisterType<OwnedSample>();
        }
    }
}
