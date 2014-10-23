using ApiLibrary;
using Autofac;
using Pellared.Owned;
using System;

namespace DiProblemInApiLibrary
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Using ApiLibrary:");
            ApiLibraryConfiguration.RegisterSpeed<SuperSpeed>();
            var drive = new Drive { MotorSpeed = 100 };
            drive.StartMotor();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Resolving Application's CompostionRoot:");
           
            using (var compositionRoot = new CompostionRoot())
            {
                var rootObject = compositionRoot.Create();
            }
        }
        
        private class SuperSpeed : ISpeed
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
        

        private class CompostionRoot : Disposable
        {
            private IContainer container;

            public CompostionRoot()
            {
                var builder = new ContainerBuilder();
                builder.RegisterCustomOwned();
                RegisterApplicationTypes(builder);
                container = builder.Build();
            }

            public OwnedSample Create()
            {
                return container.Resolve<OwnedSample>();
            }

            protected override void DisposeManaged()
            {
                container.Dispose();
            }

            private static void RegisterApplicationTypes(ContainerBuilder builder)
            {
                builder.Register(_ => new Some()).As<ISome>();
                builder.RegisterType<Some>().As<IWithArg>();
                builder.RegisterType<OwnedSample>();
            }
        }
    }
}