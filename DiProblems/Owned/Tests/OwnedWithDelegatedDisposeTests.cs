using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class OwnedWithDelegatedDisposeTests
    {
        [Fact]
        public void Dispose_disposes_only_provided_disposables()
        {
            var disposableArgument1 = new Mock<IDisposable>();
            var disposableArgument2 = new Mock<IDisposable>();

            var disposableValue = new Mock<IDisposable>();
            using (IOwned<IDisposable> cut = Owned.CreateWithDelegatedDispose(disposableValue.Object, disposableArgument1.Object, disposableArgument2.Object))
            {
                disposableArgument1.Verify(x => x.Dispose(), Times.Never);
                disposableArgument2.Verify(x => x.Dispose(), Times.Never);
            }

            disposableValue.Verify(x => x.Dispose(), Times.Never);
            disposableArgument1.Verify(x => x.Dispose(), Times.Once);
            disposableArgument2.Verify(x => x.Dispose(), Times.Once);
        }
    }
}
