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
            var drive = new Drive();
            drive.MotorSpeed = 100;
            drive.StartMotor();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Resolving from Application:");
            ServiceLocator.Current.GetInstance<OwnedSample>(); // from this application
        }

        private static void Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterOwned();
            RegisterApplicationTypes(builder);
            OverrideApiLibraryRegistration(builder);

            var container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container)); // if using the Current Locator
            //Bootstrapper.Locator = new AutofacServiceLocator(container); // we could also make a collection of Locators if it would be neeeded
        }

        private static void OverrideApiLibraryRegistration(ContainerBuilder builder)
        {
            // changing the ISpeed implementation provided by ApiLibrary
            builder.RegisterType<SuperSpeed>().As<ISpeed>();
        }

        private static void RegisterApplicationTypes(ContainerBuilder builder)
        {
            builder.Register(_ => new Some()).As<ISome>();
            builder.RegisterType<Some>().As<IWithArg>();
            builder.RegisterType<OwnedSample>();
        }
    }
}
