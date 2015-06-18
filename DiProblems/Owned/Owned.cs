using System;

namespace Pellared.Owned
{
    public interface IOwned<out T> : IDisposable
        where T : class
    {
        T Value { get; }
    }

    public class Owned<T> : DisposableBase, IOwned<T>
        where T : class
    {
        private readonly T value;

        public Owned(T value)
        {
            Require.NotNull(value, "value");

            this.value = value;
        }

        public T Value
        {
            get
            {
                Require.NotDisposed(this);
                return value;
            }
        }

        protected override void DisposeManaged()
        {
            var disposable = value as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }

    public class OwnedWithDelegatedDispose<T> : DisposableBase, IOwned<T>
        where T : class
    {
        private readonly T value;
        private readonly IDisposable[] disposables;

        public OwnedWithDelegatedDispose(T value, params IDisposable[] disposables)
        {
            Require.NotNull(value, "value");
            Require.NotNull(disposables, "disposables");

            this.value = value;
            this.disposables = disposables;
        }

        public T Value
        {
            get
            {
                Require.NotDisposed(this);
                return value;
            }
        }

        protected override void DisposeManaged()
        {
            foreach (IDisposable disposable in disposables)
            {
                disposable.Dispose();
            }
        }
    }

    public static class Owned
    {
        public static Owned<T> Create<T>(T instance)
            where T : class
        {
            return new Owned<T>(instance);
        }

        public static OwnedWithDelegatedDispose<T> CreateWithDelegatedDispose<T>(T instance, params IDisposable[] disposables)
            where T : class
        {
            return new OwnedWithDelegatedDispose<T>(instance, disposables);
        }
    }
}
