using System;

namespace Pellared.Owned
{
    public interface IFactory<out T>
        where T : class
    {
        IOwned<T> Create();
    }

    public interface IFactory<in T, out TOut>
        where TOut : class
    {
        IOwned<TOut> Create(T input);
    }

    public class Factory<T> : IFactory<T>
        where T : class
    {
        private readonly Func<T> creator;

        public Factory(Func<T> creator)
        {
            Require.NotNull(creator, "creator");
            this.creator = creator;
        }

        public IOwned<T> Create()
        {
            T ownedInstance = creator();
            IOwned<T> result = Owned.Create(ownedInstance);
            return result;
        }
    }

    public class Factory<T, TOut> : IFactory<T, TOut>
        where TOut : class
    {
        private readonly Func<T, TOut> creator;

        public Factory(Func<T, TOut> creator)
        {
            Require.NotNull(creator, "creator");
            this.creator = creator;
        }

        public IOwned<TOut> Create(T input)
        {
            TOut ownedInstance = creator(input);
            IOwned<TOut> result = Owned.Create(ownedInstance);
            return result;
        }
    }
}
