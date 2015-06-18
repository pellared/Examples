using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Pellared.Owned.Tests
{
    public class SampleUsage
    {
        private readonly ITestOutputHelper output;

        public SampleUsage(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void InOnePlace()
        {
            output.WriteLine("Initializing");
            using (var objectsToDispose = new Disposer())
            {
                objectsToDispose.AddOnDisposeAction(() => output.WriteLine("Closed"));

                var repository = new WithSomeManagedResource(output);
                objectsToDispose.AddForDisposal(repository);

                var anythingFactory = new Factory<IAnything>(() => new WithUnmanagedResource(output));
                var maker = new WithUnitOfWork(anythingFactory, output);

                var engine = new Engine(repository, maker);

                objectsToDispose.AddOnDisposeAction(() => output.WriteLine("Closing"));

                output.WriteLine("Staring");
                engine.Main();
            }
        }

        [Fact]
        public void CompositonExtracted()
        {
            output.WriteLine("Initializing");
            using (IOwned<Engine> engine = ComposeTheRoot())
            {
                output.WriteLine("Staring");
                engine.Value.Main();
            }
        }

        private IOwned<Engine> ComposeTheRoot()
        {
            var objectsToDispose = new Disposer();

            objectsToDispose.AddOnDisposeAction(() => output.WriteLine("Closed"));

            var repository = new WithSomeManagedResource(output);
            objectsToDispose.AddForDisposal(repository);

            var anythingFactory = new Factory<IAnything>(() => new WithUnmanagedResource(output));
            var maker = new WithUnitOfWork(anythingFactory, output);

            var engine = new Engine(repository, maker);

            objectsToDispose.AddOnDisposeAction(() => output.WriteLine("Closing"));
            
            return Owned.CreateWithDelegatedDispose(engine, objectsToDispose);
        }

        class Engine
        {
            IMaker maker;

            public Engine(IRepository repository, IMaker maker)
            {
                this.maker = maker;
            }

            public void Main()
            {
                using (maker.MakeAnything())
                { }

                using (maker.MakeAnything())
                { }

                using (maker.MakeAnything())
                { }
            }
        }

        interface IRepository { }

        class WithSomeManagedResource : Disposable, IRepository
        {
            public WithSomeManagedResource(ITestOutputHelper output)
            {
                var stream = new MemoryStream();
                Disposer.AddForDisposal(stream);
                Disposer.AddOnDisposeAction(() => output.WriteLine("IRepository disposed"));

                output.WriteLine("IRepository created");
            }
        }

        interface IMaker
        {
            IOwned<IAnything> MakeAnything();
        }

        interface IAnything { }

        class WithUnmanagedResource : DisposableBase, IAnything
        {
            ITestOutputHelper output;

            public WithUnmanagedResource(ITestOutputHelper output)
                : base(Option.WithFinalizer)
            {
                this.output = output;
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();
                output.WriteLine("IAnything disposed");
            }
        }

        class WithUnitOfWork : IMaker
        {
            ITestOutputHelper output;
            IFactory<IAnything> factory;

            public WithUnitOfWork(IFactory<IAnything> factory, ITestOutputHelper output)
            {
                this.factory = factory;
                this.output = output;
            }

            public IOwned<IAnything> MakeAnything()
            {
                output.WriteLine("IAnything created");
                return factory.Create();
            }
        }

    }
}
