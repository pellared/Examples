using System;

namespace Pellared.Owned
{
    public class Disposer : Disposable
    {
        private readonly Action action;

        public Disposer(Action action)
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
