using Moq;
using System;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class FactoryTests
    {
        [Fact]
        public void Should_create()
        {
            var injected = "asd";
            var cut = new Factory<string>(() => injected);

            var result = cut.Create();

            Assert.Equal(injected, result.Value);
        }


        [Fact]
        public void Should_create_with_argument()
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

            var result = cut.Create(argument);

            Assert.Equal(injected, result.Value);
            Assert.True(isProvidingArgumentToCreator);
        }
    }
}
