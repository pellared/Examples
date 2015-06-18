using System;

namespace Pellared.Owned
{
    public interface IShared<out T>
        where T : class
    {
        IOwned<T> Share();
    }

    // TODO: tests
    public class Shared<T> : DisposableBase, IShared<T>
        where T : class
    {
        private readonly object syncRoot = new object();
        private readonly IFactory<T> factory;
        private IOwned<T> instance;
        private int referenceCount;

        public Shared(IFactory<T> factory)
        {
            Require.NotNull(factory, "factory");

            this.factory = factory;
        }

        public IOwned<T> Share()
        {
            Require.NotDisposed(this);

            lock (syncRoot)
            {
                if (referenceCount == 0)
                {
                    instance = factory.Create();
                }

                referenceCount++;

                return new ReferenceToShared(instance.Value, ReleaseOne);
            }
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();

            lock (syncRoot)
            {
                if (instance != null)
                {
                    instance.Dispose();
                }
            }
        }

        private void ReleaseOne()
        {
            lock (syncRoot)
            {
                referenceCount--;

                if (referenceCount == 0)
                {
                    instance.Dispose();
                    instance = null;
                }
            }

        }

        private class ReferenceToShared : ReleaseAction, IOwned<T>
        {
            private readonly T value;

            public T Value { get { return value; } }

            public ReferenceToShared(T value, Action release)
                : base(release)
            {
                this.value = value;
            }
        }
    }
}
