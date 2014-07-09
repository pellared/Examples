using System;

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
