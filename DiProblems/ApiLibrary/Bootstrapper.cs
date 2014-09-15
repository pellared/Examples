using Pellared.Owned;
using TinyIoC;

namespace ApiLibrary
{
    public class Bootstrapper
    {
        private static readonly TinyIoCContainer Container;

        static Bootstrapper()
        {
            //if (ServiceLocator.IsLocationProviderSet)
            //{
            //    Locator = ServiceLocator.Current;
            //}

            // using TinyIoC as internal in order that the clients could have any library they want
            Container = TinyIoCContainer.Current;


            Container.Register(typeof(IOwned<>), typeof(Owned<>));
            Container.Register(typeof(IFactory<>), typeof(FuncFactory<>));
            Container.Register(typeof(IFactory<,>), typeof(FuncFactory<,>));

            Container.Register<IMotor, Motor>();
            Container.Register<ISpeed, Speed>();
        }

        //public static IServiceLocator Locator { get; set; }

        internal static T Get<T>() where T : class
        {
            // client could also provide custom registration using Common Service Locator
            // alternatives: assembly scanning, MEF (MAF)

            // it is a total failure to mix like this! do not do it at home!
            //if (Locator != null)
            //{
            //    try
            //    {
            //        return Locator.GetInstance<T>();
            //    }
            //    catch (Exception)
            //    {
            //        // if not registered by the client
            //    }
            //}

            return Container.Resolve<T>();
        }

        public static void RegisterSpeed<T>()
            where T : class, ISpeed
        {
            Container.Register<ISpeed, T>();
        }
    }
}
