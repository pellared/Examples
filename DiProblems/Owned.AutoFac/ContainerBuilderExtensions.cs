using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // for other IoC Continers (such as unity)
        private static ContainerBuilder RegisterFuncOwned(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Owned<>)).As(typeof(IOwned<>));
            builder.RegisterGeneric(typeof(FuncFactory<>)).As(typeof(IFactory<>));
            builder.RegisterGeneric(typeof(FuncFactory<,>)).As(typeof(IFactory<,>));
            return builder;
        }
    }
}
