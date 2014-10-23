using Pellared.Owned;
using TinyIoC;

namespace ApiLibrary
{
    public class ApiLibraryConfiguration
    {
        private static readonly TinyIoCContainer Container;

        static ApiLibraryConfiguration()
        {
            // using TinyIoC as internal in order that the clients could have any library they want :)
            Container = TinyIoCContainer.Current;

            Container.Register<IMotor, Motor>();
            Container.Register<ISpeed, Speed>();
        }

        // in order to change the default implementation - it can be reused in really a lot of places!
        public static void RegisterSpeed<T>()
            where T : class, ISpeed
        {
            Container.Register<ISpeed, T>();
        }

        internal static T Get<T>() where T : class
        {
            return Container.Resolve<T>();
        }
    }
}
