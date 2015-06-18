using Xunit;

namespace Pellared.Owned.Tests
{
    public class ReleaseActionTests
    {
        [Fact]
        public void Invokes_action_when_disposed()
        {
            bool isActionInvoked = false;

            using (new ReleaseAction(() => isActionInvoked = true))
            {
                Assert.False(isActionInvoked);
            }

            Assert.True(isActionInvoked);
        }
    }
}
