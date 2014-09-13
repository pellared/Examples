using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class OwnedTests
    {
        [Fact]
        public void Should_have_provided_value()
        {
            string value = "hello";
            
            var cut = new Owned<string>(value);

            Assert.Equal(value, cut.Value);
        }

        [Fact]
        public void Should_work_if_not_disposable()
        {
            string value = "hello";

            var cut = new Owned<string>(value);

            cut.Dispose();
        }

        [Fact]
        public void Should_dispose_the_value()
        {
            var disposable = new Mock<IDisposable>();
            IOwned<IDisposable> cut = Owned.Create(disposable.Object);

            cut.Dispose();

            disposable.Verify(x => x.Dispose());
        }
    }
}
