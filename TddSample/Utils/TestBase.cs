using System;

namespace Utils
{
    // http://martinfowler.com/bliki/GivenWhenThen.html
    public class TestBase : IDisposable
    {
        // do not use the constructor in derived classes
        public TestBase()
        {
// ReSharper disable DoNotCallOverridableMethodsInConstructor
            Setup();
            Given();
            When();
// ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public void Dispose()
        {
            Cleanup();
        }

        protected virtual void Setup() { }
        protected virtual void Given() { }
        protected virtual void When() { }
        protected virtual void Cleanup() { } 
    }
}