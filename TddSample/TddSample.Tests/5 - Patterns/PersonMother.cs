using System;

namespace TddSample.Tests
{
    // http://martinfowler.com/bliki/ObjectMother.html
    internal class PersonMother
    {
        private readonly static string DefaultName = "John";

        public static Person Create(DateTime birthday)
        {
            return new Person(DefaultName, birthday);
        }

        public static Person Create()
        {
            return new Person(DefaultName, DateTime.Now.AddYears(-20));
        }
    }
}
