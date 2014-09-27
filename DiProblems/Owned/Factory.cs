using System;

namespace Pellared.Owned
{
    public interface IFactory<out T>
    {
        IOwned<T> Create();
    }

    public interface IFactory<in T, out TOut>
    {
        IOwned<TOut> Create(T input);
    }

    public class Factory<T> : IFactory<T>
    {
        private readonly Func<T, IOwned<T>> ownedFactory;
        private readonly Func<T> creator;

        public Factory(Func<T, IOwned<T>> ownedFactory, Func<T> creator)
        {
            this.ownedFactory = ownedFactory;
            this.creator = creator;
        }

        public Factory(Func<T> creator)
            : this(Owned.Create, creator)
        {
        }

        public IOwned<T> Create()
        {
            T ownedInstance = creator();
            IOwned<T> result = ownedFactory(ownedInstance);
            return result;
        }
    }

    public class Factory<T, TOut> : IFactory<T, TOut>
    {
        private readonly Func<TOut, IOwned<TOut>> ownedFactory;
        private readonly Func<T, TOut> creator;

        public Factory(Func<TOut, IOwned<TOut>> ownedFactory, Func<T, TOut> creator)
        {
            this.ownedFactory = ownedFactory;
            this.creator = creator;
        }

        public Factory(Func<T, TOut> creator)
            : this(Owned.Create, creator)
        {
        }

        public IOwned<TOut> Create(T input)
        {
            TOut ownedInstance = creator(input);
            IOwned<TOut> result = ownedFactory(ownedInstance);
            return result;
        }
    }
}
