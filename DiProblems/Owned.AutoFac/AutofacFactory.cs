using System;

namespace Pellared.Owned
{
    public class AutofacFactory<T> : IFactory<T>
        where T : class
    {
        private readonly Func<Autofac.Features.OwnedInstances.Owned<T>, IOwned<T>> ownedFactory;
        private readonly Func<Autofac.Features.OwnedInstances.Owned<T>> creator;

        public AutofacFactory(Func<Autofac.Features.OwnedInstances.Owned<T>, IOwned<T>> ownedFactory, Func<Autofac.Features.OwnedInstances.Owned<T>> creator)
        {
            this.ownedFactory = ownedFactory;
            this.creator = creator;
        }

        public IOwned<T> Create()
        {
            Autofac.Features.OwnedInstances.Owned<T> ownedInstance = creator();
            IOwned<T> result = ownedFactory(ownedInstance);
            return result;
        }
    }

    public class AutofacFactory<T, TOut> : IFactory<T, TOut>
        where TOut : class
    {
        private readonly Func<Autofac.Features.OwnedInstances.Owned<TOut>, IOwned<TOut>> ownedFactory;
        private readonly Func<T, Autofac.Features.OwnedInstances.Owned<TOut>> creator;

        public AutofacFactory(Func<Autofac.Features.OwnedInstances.Owned<TOut>, IOwned<TOut>> ownedFactory, Func<T, Autofac.Features.OwnedInstances.Owned<TOut>> creator)
        {
            this.ownedFactory = ownedFactory;
            this.creator = creator;
        }

        public IOwned<TOut> Create(T input)
        {
            Autofac.Features.OwnedInstances.Owned<TOut> ownedInstance = creator(input);
            IOwned<TOut> result = ownedFactory(ownedInstance);
            return result;
        }
    }
}
