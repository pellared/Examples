using System;

namespace TddSample.Tests
{
    // http://www.natpryce.com/articles/000714.html
    public class PersonBuilder
    {
        private string name = Utils.Build.Any<string>();
        private DateTime birthDate = Utils.Build.Any<DateTime>();

        public PersonBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public PersonBuilder WithBirthDate(DateTime birthDate)
        {
            this.birthDate = birthDate;
            return this;
        }

        public Person Build()
        {
            return new Person(name, birthDate);
        }
    }
}