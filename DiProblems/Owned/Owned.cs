using System;

namespace Pellared.Owned
{
    public interface IOwned<out T> : IDisposable
    {
        T Value { get; }
    }

    public class Owned<T> : IOwned<T>
    {
        public Owned(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public void Dispose()
        {
            var disposable = Value as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }

    public static class Owned
    {
        public static IOwned<T> Create<T>(T instance)
        {
            return new Owned<T>(instance);
        }
    }
}
