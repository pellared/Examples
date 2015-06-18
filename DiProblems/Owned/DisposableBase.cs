using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Pellared.Owned
{
    public class DisposableBase : IDisposable
    {
        public enum Option
        {
            NoFinalizer,
            WithFinalizer
        }

        private const int DisposedFlag = 1;

        private readonly StackTrace creationStackTrace;

        private int _isDisposed;

        public DisposableBase(Option option = Option.NoFinalizer)
        {
#if DEBUG
            creationStackTrace = new StackTrace();
#else
            if (option == Option.WithFinalizer)
            {
                GC.SuppressFinalize(this);
            }
#endif
        }

        ~DisposableBase()
        {
            DisposeUnmanaged();
            Debug.Fail(GetType() + " was not disposed" + Environment.NewLine + creationStackTrace);
        }

        public bool IsDisposed
        {
            get
            {
                Thread.MemoryBarrier();
                return _isDisposed == DisposedFlag;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Dispose is implemented correctly")]
        public void Dispose()
        {
            int wasDisposed = Interlocked.Exchange(ref _isDisposed, DisposedFlag);
            if (wasDisposed != DisposedFlag)
            {
                DisposeResources();
            }
        }

        protected virtual void DisposeManaged()
        {
        }

        protected virtual void DisposeUnmanaged()
        {
        }

        private void DisposeResources()
        {
            DisposeManaged();
            DisposeUnmanaged();
            GC.SuppressFinalize(this);
        }
    }
}
