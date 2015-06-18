using System;
using Xunit;

namespace Pellared.Owned.Tests
{
    public class DisposableBaseTests
    {
        [Fact]
        public void Should_invoke_dispose_resources_when_Dispose()
        {
            var cut = new DisposableUnderTest();

            Assert.False(cut.IsDisposeManagedCalled);
            Assert.False(cut.IsDisposeUnmanagedCalled);
            cut.Dispose();

            Assert.True(cut.IsDisposeManagedCalled);
            Assert.True(cut.IsDisposeUnmanagedCalled);
        }

        [Fact]
        public void Should_invoke_dispose_resources_once_when_Dispose_multiple_times()
        {
            var cut = new DisposableUnderTest();

            cut.Dispose();
            cut.Dispose();

            Assert.Equal(1, cut.DisposeCalledCount);
        }

        [Fact]
        public void IsNotDisposed_AfterDisposedCalled()
        {
            var cut = new DisposableUnderTest();
            cut.Dispose();

            Assert.True(cut.IsDisposed);
        }

        [Fact]
        public void IsNotDisposed_BeforeDisposedCalled()
        {
            using (var cut = new DisposableUnderTest())
            {
                Assert.False(cut.IsDisposed);
            }
        }

        [Fact]
        public void Should_be_IsDisposed_when_disposed()
        {
            var cut = new DisposableUnderTest();

            cut.Dispose();

            Assert.True(cut.IsDisposed);
        }

        [Fact]
        public void Should_not_be_IsDisposed_when_not_disposed()
        {
            using (var cut = new DisposableUnderTest())
            {
                Assert.False(cut.IsDisposed);
            }
        }

        private class DisposableUnderTest : DisposableBase
        {
            public int DisposeCalledCount { get; private set; }
            public bool IsDisposeManagedCalled { get; private set; }
            public bool IsDisposeUnmanagedCalled { get; private set; }
            
            protected override void DisposeManaged()
            {
                IsDisposeManagedCalled = true;
                DisposeCalledCount++;
            }

            protected override void DisposeUnmanaged()
            {
                IsDisposeUnmanagedCalled = true;
            }
        }
    }
}
