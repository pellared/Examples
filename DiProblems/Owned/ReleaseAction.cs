using System;

namespace Pellared.Owned
{
    public class ReleaseAction : DisposableBase
    {
        private readonly Action action;

        public ReleaseAction(Action action)
        {
            Require.NotNull(action, "action");

            this.action = action;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            action();
        }
    }
}
