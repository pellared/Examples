using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Pellared.Owned
{
    public class Disposable : IDisposable
    {
        const int DisposedFlag = 1;
        int _isDisposed;

        private readonly Finalizer finalizer;

        public Disposable(bool withFinalizer = false)
        {
#if !DEBUG
            if (withFinalizer)
#endif
            {
                finalizer = new Finalizer(OnFinalize);
            }
        }

        private void OnFinalize()
        {
            DisposeUnmanaged();
            Debug.Fail(GetType() + " in not disposed");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Dispose is implemented correctly, FxCop just doesn't see it.")]
        public void Dispose()
        {
            int wasDisposed = Interlocked.Exchange(ref _isDisposed, DisposedFlag);
            EnsureNotDisposed(wasDisposed);

            DisposeManaged();
            DisposeUnmanaged();

            if (finalizer != null)
            {
                finalizer.SuppressFinalize();
            }
        }

        protected virtual void DisposeManaged()
        {
        }

        protected virtual void DisposeUnmanaged()
        {
        }

        public bool IsDisposed
        {
            get
            {
                Thread.MemoryBarrier();
                return _isDisposed == DisposedFlag;
            }
        }

        private void EnsureNotDisposed(int wasDisposed)
        {
            if (wasDisposed == DisposedFlag)
            {
                string typeName = GetType().FullName;
                string stackTrace = new StackTrace().ToString();
                throw new ObjectDisposedException(typeName, stackTrace);
            }
        }
    }
}
