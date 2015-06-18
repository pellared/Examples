using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class DisposableTests
    {
        [Fact]
        public void Dispose_DisposesObjectsAddedToDisposer()
        {
            var disposable = new Mock<IDisposable>();
            var actionVerifier = new ActionVerifer();
            using (var cut = new DisposableUnderTest())
            {
                cut.AddForDisposalToTheDisposer(disposable.Object, actionVerifier.Action);

                disposable.Verify(x => x.Dispose(), Times.Never);
                actionVerifier.VerifyNeverCalled();
            }

            disposable.Verify(x => x.Dispose(), Times.Once);
            actionVerifier.VerifyCalledOnce();
        }

        [Fact]
        public void HasTheSamePropertiesAs_DiposableBase()
        {
            using (var cut = new DisposableUnderTest())
            {
                Assert.IsAssignableFrom<DisposableBase>(cut);
            }
        }

        private class DisposableUnderTest : Disposable
        {
            public void AddForDisposalToTheDisposer(IDisposable disposable, Action action)
            {
                Disposer.AddForDisposal(disposable);
                Disposer.AddOnDisposeAction(action);
            }
        }
    }
}
