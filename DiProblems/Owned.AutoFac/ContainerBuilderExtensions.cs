using Autofac;

namespace Pellared.Owned
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterOwned(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(AutofacOwned<>)).As(typeof(IOwned<>));
            builder.RegisterGeneric(typeof(AutofacFactory<>)).As(typeof(IFactory<>));
            builder.RegisterGeneric(typeof(AutofacFactory<,>)).As(typeof(IFactory<,>));
            return builder;
        }

        // code for IoC Container which does not provide Owned functionality
        private static ContainerBuilder RegisterFuncOwned(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Owned<>)).As(typeof(IOwned<>));
            builder.RegisterGeneric(typeof(FuncFactory<>)).As(typeof(IFactory<>));
            builder.RegisterGeneric(typeof(FuncFactory<,>)).As(typeof(IFactory<,>));
            return builder;
        }
    }
}
