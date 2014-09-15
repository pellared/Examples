using System;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class DisposableTests
    {
        [Fact]
        public void Should_invoke_DisposeManaged_and_DisposeUnmanaged_when_disposed()
        {
            var cut = new DisposableSpy();

            cut.Dispose();

            Assert.True(cut.IsDisposeManagedCalled);
            Assert.True(cut.IsDisposeUnmanagedCalled);
        }

        [Fact]
        public void Should_throw_exception_when_multiple_disposal()
        {
            var cut = new DisposableSpy();
            Assert.ThrowsDelegate dispose = () => cut.Dispose();
            dispose();

            Assert.Throws<ObjectDisposedException>(dispose);
        }

        [Fact]
        public void Should_be_IsDisposed_when_disposed()
        {
            var cut = new DisposableSpy();

            cut.Dispose();

            Assert.True(cut.Disposed);
        }

        [Fact]
        public void Should_not_be_IsDisposed_when_not_disposed()
        {
            using (var cut = new DisposableSpy())
            {
                Assert.False(cut.Disposed);
            }
        }

        private class DisposableSpy : Disposable
        {
            public bool IsDisposeManagedCalled { get; private set; }
            public bool IsDisposeUnmanagedCalled { get; private set; }

            public bool Disposed { get { return IsDisposed; } }

            protected override void DisposeManaged()
            {
                IsDisposeManagedCalled = true;
            }

            protected override void DisposeUnmanaged()
            {
                IsDisposeUnmanagedCalled = true;
            }
        }
    }
}
