using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class RegisterOwnedTests
    {
        private readonly IContainer container;

        public RegisterOwnedTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterOwned();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsSelf();

            container = builder.Build();
        }

        [Fact]
        public void Should_resolve_AutofacOwned()
        {
            var instance = container.Resolve<ClassWithOwned>();

            Assert.IsType<AutofacOwned<Normal>>(instance.Owned);
        }

        private class ClassWithOwned
        {
            public IOwned<Normal> Owned { get; private set; }

            public ClassWithOwned(IOwned<Normal> owned)
            {
                Owned = owned;
            }
        }

        [Fact]
        public void Should_resolve_AutofacFactory_Normal()
        {
            var instance = container.Resolve<ClassWithFactory>();

            Assert.IsType<AutofacFactory<Normal>>(instance.Factory);
        }

        private class ClassWithFactory
        {
            public IFactory<Normal> Factory { get; private set; }

            public ClassWithFactory(IFactory<Normal> factory)
            {
                Factory = factory;
            }
        }

        [Fact]
        public void Should_resolve_AutofacFactory_WithArg()
        {
            var instance = container.Resolve<ClassWithFactoryWithArg>();

            Assert.IsType<AutofacFactory<Normal, WithArg>>(instance.Factory);
        }

        private class ClassWithFactoryWithArg
        {
            public IFactory<Normal, WithArg> Factory { get; private set; }

            public ClassWithFactoryWithArg(IFactory<Normal, WithArg> factory)
            {
                Factory = factory;
            }
        }

        [Fact]
        public void Should_resolve_AutofacFactory_WithArgOnly()
        {
            var instance = container.Resolve<ClassWithFactoryWithArgOnly>();

            Assert.IsType<AutofacFactory<WithArg>>(instance.Factory);
        }

        private class ClassWithFactoryWithArgOnly
        {
            public IFactory<WithArg> Factory { get; private set; }

            public ClassWithFactoryWithArgOnly(IFactory<WithArg> factory)
            {
                Factory = factory;
            }
        }

        private class Normal
        {
        }

        private class WithArg
        {
            public string Argument { get; private set; }

            public WithArg(string argument)
            {
                Argument = argument;
            }
        }
    }
}
