using Pellared.Owned;
using System;

namespace DiProblemInApiLibrary
{
    interface ISome
    {
        void Do();
    }

    interface IWithArg : ISome
    {
    }

    public class Some : ISome, IWithArg, IDisposable
    {
        private readonly string text;

        public Some(string text)
        {
            this.text = text;
        }

        public Some()
            : this("Do")
        {
        }

        public void Do()
        {
            Console.WriteLine(text);
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose");
        }
    }

    class OwnedSample
    {
        public OwnedSample(ISome some, IOwned<ISome> ownedSome, IFactory<ISome> someFactory, IFactory<string, IWithArg> ownedArgFactory)
        {
            Console.WriteLine("ISome");
            some.Do();
            Console.WriteLine();

            Console.WriteLine("IOwned<ISome>");
            using (ownedSome)
            {
                ownedSome.Value.Do();
            }
            Console.WriteLine();

            Console.WriteLine("IFactory<ISome>");
            using (IOwned<ISome> created = someFactory.Create())
            {
                created.Value.Do();
            }
            Console.WriteLine();

            Console.WriteLine("IFactory<string, IWithArg>");
            using (var nestedCreated = ownedArgFactory.Create("Do with arg:"))
            {
                nestedCreated.Value.Do();
            }
            Console.WriteLine();
        }
    }
}
