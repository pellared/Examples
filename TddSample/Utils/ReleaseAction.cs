using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class ReleaseAction : Disposable
    {
        private readonly Action action;

        public ReleaseAction(Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            this.action = action;
        }

        protected override void DisposeManaged()
        {
            action();
        }
    }
}
