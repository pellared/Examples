using Moq;
using System;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class FactoryTests
    {
        [Fact]
        public void Creates_Owned()
        {
            var injected = "asd";
            var cut = new Factory<string>(() => injected);

            using (IOwned<string> result = cut.Create())
            {
                Assert.IsType<Owned<string>>(result);
                Assert.Equal(injected, result.Value);
            }
        }


        [Fact]
        public void Create_Owned_with_argument()
        {
            bool isProvidingArgumentToCreator = false;
            int argument = 3;
            var injected = "asd";
            Func<int, string> creator = arg =>
            {
                isProvidingArgumentToCreator = arg == argument;
                return injected;
            };
            var cut = new Factory<int, string>(creator);

            using (IOwned<string> result = cut.Create(argument))
            {

                Assert.IsType<Owned<string>>(result);
                Assert.Equal(injected, result.Value);
                Assert.True(isProvidingArgumentToCreator);
            }
        }
    }
}
