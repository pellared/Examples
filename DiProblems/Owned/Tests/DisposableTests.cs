using System;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class DisposableTests
    {
        [Fact]
        public void Should_invoke_dispose_resources_when_Dispose()
        {
            var cut = new DisposableSpy();

            cut.Dispose();

            Assert.True(cut.IsDisposeManagedCalled);
            Assert.True(cut.IsDisposeUnmanagedCalled);
        }

        [Fact]
        public void Should_invoke_dispose_resources_once_when_Dispose_multiple_times()
        {
            var cut = new DisposableSpy();

            cut.Dispose();
            cut.Dispose();

            Assert.Equal(1, cut.DisposeResourcesCount);
        }

        [Fact]
        public void Should_throw_exception_when_disposed_and_RequireNotDisposed_called()
        {
            var cut = new DisposableSpy();
            cut.Dispose();

            Assert.Throws<ObjectDisposedException>(() => cut.CallRequireNotDisposed());
        }

        [Fact]
        public void Should_not_throw_exception_when_not_disposed_and_RequireNotDisposed_called()
        {
            using (var cut = new DisposableSpy())
            {
                Assert.DoesNotThrow(cut.CallRequireNotDisposed);
            }
        }

        [Fact]
        public void Should_be_IsDisposed_when_disposed()
        {
            var cut = new DisposableSpy();

            cut.Dispose();

            Assert.True(cut.IsDisposed);
        }

        [Fact]
        public void Should_not_be_IsDisposed_when_not_disposed()
        {
            using (var cut = new DisposableSpy())
            {
                Assert.False(cut.IsDisposed);
            }
        }

        private class DisposableSpy : Disposable
        {
            public int DisposeResourcesCount { get; private set; }
            public bool IsDisposeManagedCalled { get; private set; }
            public bool IsDisposeUnmanagedCalled { get; private set; }

            public void CallRequireNotDisposed()
            {
                RequireNotDisposed();
            }
            
            protected override void DisposeManaged()
            {
                IsDisposeManagedCalled = true;
                DisposeResourcesCount++;
            }

            protected override void DisposeUnmanaged()
            {
                IsDisposeUnmanagedCalled = true;
            }
        }
    }
}
