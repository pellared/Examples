using System;

namespace Pellared.Owned
{
    public interface IShared<out T>
    {
        IOwned<T> Share();
    }

    public class Shared<T> : IShared<T>
    {
        private readonly object syncRoot = new object();
        private readonly IFactory<T> factory;
        private IOwned<T> instance;
        private int referenceCount;

        public Shared(IFactory<T> factory)
        {
            this.factory = factory;
        }

        public IOwned<T> Share()
        {
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

        private class ReferenceToShared : Disposer, IOwned<T>
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
