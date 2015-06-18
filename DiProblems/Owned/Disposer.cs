using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Owned
{
    public interface IDisposer
    {
        void AddForDisposal(IDisposable disposable);
    }

    public class Disposer : DisposableBase, IDisposer
    {
        private Stack<IDisposable> disposables = new Stack<IDisposable>();

        public void AddForDisposal(IDisposable disposable)
        {
            Require.NotDisposed(this);
            Require.NotNull(disposable, "disposableValue");

            disposables.Push(disposable);
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            while (disposables.Any())
            {
                IDisposable disposable = disposables.Pop();
                disposable.Dispose();
            }
        }
    }

    public static class DisposerExtensions
    {

        public static void AddOnDisposeAction(this IDisposer disposer, Action action)
        {
            Require.NotNull(action, "action");

            var releaseAction = new ReleaseAction(action);
            disposer.AddForDisposal(releaseAction);
        }
    }
}
