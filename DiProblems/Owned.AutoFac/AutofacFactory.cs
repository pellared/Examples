using System;

namespace Pellared.Owned
{
    public class AutofacFactory<T> : IFactory<T>
    {
        private readonly Func<IOwned<T>> ownedFactory;

        public AutofacFactory(Func<IOwned<T>> ownedFactory)
        {
            this.ownedFactory = ownedFactory;
        }

        public IOwned<T> Create()
        {
            IOwned<T> result = ownedFactory();
            return result;
        }
    }

    public class AutofacFactory<TIn, TOut> : IFactory<TIn, TOut>
    {
        private readonly Func<TIn, IOwned<TOut>> ownedFactory;

        public AutofacFactory(Func<TIn, IOwned<TOut>> ownedFactory)
        {
            this.ownedFactory = ownedFactory;
        }

        public IOwned<TOut> Create(TIn input)
        {
            IOwned<TOut> result = ownedFactory(input);
            return result;
        }
    }
}
