using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class DisposerTests
    {
        [Fact]
        public void Dispose_DisposesAddedDisposables()
        {
            var disposable1 = new Mock<IDisposable>();
            var disposable2 = new Mock<IDisposable>();

            using (var cut = new Disposer())
            {
                cut.AddForDisposal(disposable1.Object);
                cut.AddForDisposal(disposable2.Object);

                disposable1.Verify(x => x.Dispose(), Times.Never);
                disposable2.Verify(x => x.Dispose(), Times.Never);
            }

            disposable1.Verify(x => x.Dispose(), Times.Once);
            disposable2.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public void Dispose_InvokesAddedActions()
        {
            var actionVerifier1 = new ActionVerifer();
            var actionVerifier2 = new ActionVerifer();

            using (var cut = new Disposer())
            {
                cut.AddOnDisposeAction(actionVerifier1.Action);
                cut.AddOnDisposeAction(actionVerifier2.Action);

                actionVerifier1.VerifyNeverCalled();
                actionVerifier2.VerifyNeverCalled();
            }

            actionVerifier1.VerifyCalledOnce();
            actionVerifier2.VerifyCalledOnce();
        }

        [Fact]
        public void HasTheSamePropertiesAs_DiposableBase()
        {
            using (var cut = new Disposer())
            {
                Assert.IsAssignableFrom<DisposableBase>(cut);
            }
        }
    }
}
