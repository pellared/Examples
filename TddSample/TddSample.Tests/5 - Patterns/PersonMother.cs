using System;

namespace TddSample.Tests
{
    // http://martinfowler.com/bliki/ObjectMother.html
    internal class PersonMother
    {
        public static Person Create(DateTime birthday)
        {
            return new Person("John", birthday);
        }

        public static Person Create()
        {
            return new Person("John", DateTime.Now);
        }
    }
}
