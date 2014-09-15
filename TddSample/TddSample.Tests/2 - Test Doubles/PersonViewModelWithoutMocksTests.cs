using System;
using System.Collections.Generic;
using FluentAssertions;
using Utils;
using Xunit;

namespace TddSample.Tests
{
    public class PersonViewModelWithoutMocksTests
    {
        class PersonRepositoryDummy : IPersonRepository
        {
            public void Add(Person person)
            {
            }
        }

        class PersonValidatorStub : IPersonValidator
        {
            public bool IsValid(Person person)
            {
                return true;
            }
        }

        private class PersonRepositoryFake : IPersonRepository
        {
            private readonly List<Person> collection = new List<Person>();

            public void Add(Person person)
            {
                collection.Add(person);
            }
        }

        [Fact]
        public void Save_ValidUser_StatusWithName()
        {
            var person = new Person("John", Build.Any<DateTime>());
            var validator = new PersonValidatorStub();
            var respository = new PersonRepositoryDummy();
            var sut = new PersonViewModel(validator, respository);

            sut.Save(person);

            sut.Status.Should().Be("John saved");
        }
    }
}
