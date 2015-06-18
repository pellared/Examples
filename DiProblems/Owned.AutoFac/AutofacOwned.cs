namespace Pellared.Owned
{
    public class AutofacOwned<T> : IOwned<T>
        where T : class
    {
        private readonly Autofac.Features.OwnedInstances.Owned<T> owned;

        public AutofacOwned(Autofac.Features.OwnedInstances.Owned<T> owned)
        {
            this.owned = owned;
        }

        public T Value
        {
            get { return owned.Value; }
        }

        public void Dispose()
        {
            owned.Dispose();
        }
    }
}
