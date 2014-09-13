using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class FuncFactory<T> : IFactory<T>
    {
        private readonly Func<T, IOwned<T>> ownedFactory;
        private readonly Func<T> creator;

        public FuncFactory(Func<T, IOwned<T>> ownedFactory, Func<T> creator)
        {
            this.ownedFactory = ownedFactory;
            this.creator = creator;
        }

        public FuncFactory(Func<T> creator)
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

    public class FuncFactory<T, TOut> : IFactory<T, TOut>
    {
        private readonly Func<TOut, IOwned<TOut>> ownedFactory;
        private readonly Func<T, TOut> creator;

        public FuncFactory(Func<TOut, IOwned<TOut>> ownedFactory, Func<T, TOut> creator)
        {
            this.ownedFactory = ownedFactory;
            this.creator = creator;
        }

        public FuncFactory(Func<T, TOut> creator)
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
