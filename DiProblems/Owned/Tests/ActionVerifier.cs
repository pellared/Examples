using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pellared.Owned.Tests
{
    internal class ActionVerifer
    {
        private int calledCount;

        public void Action()
        {
            calledCount++;
        }

        public void VerifyCalledOnce()
        {
            Assert.Equal(1, calledCount);
        }

        public void VerifyNeverCalled()
        {
            Assert.Equal(0, calledCount);
        }
    }
}
