using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class FuncFactoryTests
    {
        [Fact]
        public void Should_create_owned_from_created_instace()
        {
            bool isCreatingUsingValue = false;
            string value = "message";
            Func<string> creator = () => value;
            Func<string, IOwned<string>> ownedFactory = instance =>
            {
                isCreatingUsingValue = instance == value;
                return Mock.Of<IOwned<string>>();
            };
            var cut = new FuncFactory<string>(ownedFactory, creator);

            cut.Create();

            Assert.True(isCreatingUsingValue);
        }

        [Fact]
        public void Should_return_created_instace()
        {
            IOwned<string> createdObject = Mock.Of<IOwned<string>>();
            Func<string> creator = () => "any message";
            Func<string, IOwned<string>> ownedFactory = _ => createdObject;
            var cut = new FuncFactory<string>(ownedFactory, creator);

            IOwned<string> result = cut.Create();

            Assert.Equal(createdObject, result);
        }

        [Fact]
        public void Should_create_owned_with_good_value()
        {
            string value = "message";
            Func<string> creator = () => value;
            var cut = new FuncFactory<string>(creator);

            IOwned<string> result = cut.Create();

            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void Should_create_owned()
        {
            Func<string> creator = () => "any message";
            var cut = new FuncFactory<string>(creator);

            IOwned<string> result = cut.Create();

            Assert.IsType<Owned<string>>(result);
        }

        [Fact]
        public void Should_provide_argument_to_creator()
        {
            bool isProvidingArgumentToCreator = false;
            int argument = 3;
            Func<int, string> creator = arg =>
            {
                isProvidingArgumentToCreator = arg == argument;
                return "any message";
            };

            var cut = new FuncFactory<int, string>(creator);
            cut.Create(argument);

            Assert.True(isProvidingArgumentToCreator);
        }
    }
}
