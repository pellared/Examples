using Xunit;

namespace Pellared.Owned.Tests
{
    public class DisposerTests
    {
        [Fact]
        public void Should_invoke_action_when_disposed()
        {
            bool isActionInvoked = false;

            using (new Disposer(() => isActionInvoked = true))
            {
                Assert.False(isActionInvoked);
            }

            Assert.True(isActionInvoked);
        }
    }
}
