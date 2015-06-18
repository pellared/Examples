using Moq;
using System;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class OwnedTests
    {
        [Fact]
        public void Has_Value()
        {
            string value = "hello";

            using (var cut = new Owned<string>(value))
            {
                Assert.Equal(value, cut.Value);
            }
        }

        [Fact]
        public void Dispose_does_nothing_when_Value_not_disposable()
        {
            string value = "hello";

            var cut = new Owned<string>(value);

            cut.Dispose();
        }

        [Fact]
        public void Dispose_disposes_when_Value_disposable()
        {
            var disposable = new Mock<IDisposable>();
            using (IOwned<IDisposable> cut = Owned.Create(disposable.Object))
            {
                disposable.Verify(x => x.Dispose(), Times.Never);
            }

            disposable.Verify(x => x.Dispose(), Times.Once);
        }
    }
}
