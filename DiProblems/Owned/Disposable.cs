using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pellared.Owned
{
    public class Disposable : DisposableBase
    {
        private readonly Disposer disposer = new Disposer();

        protected IDisposer Disposer { get { return disposer; } }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            disposer.Dispose();
        }
    }
}
