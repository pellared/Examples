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
        public void Should_invoke_action_when_disposed()
        {
            bool isActionInvoked = false;

            using (new Disposer(() => isActionInvoked = true))
            { }

            Assert.True(isActionInvoked);
        }

        [Fact]
        public void Should_action_not_invoed_before_disposal()
        {
            bool isActionInvoked = false;

            using (new Disposer(() => isActionInvoked = true))
            {
                Assert.False(isActionInvoked);
            }
        }
    }
}
