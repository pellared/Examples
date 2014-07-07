using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;
using Moq;
using Xunit;
using Ploeh.AutoFixture;

namespace TddSample.Tests
{
    public class PersonViewModelWithoutMocksTests
    {
        private readonly static Fixture Fixture = new Fixture();

        [Fact]
        public void Save_ValidUser_StatusWithName()
        {
            var person = new Person("John", Any<DateTime>());
            var validator = new PersonValidatorStub();
            var respository = new PersonRepositoryDummy();
            var sut = new PersonViewModel(validator, respository);

            sut.Save(person);

            sut.Status.Should().Be("John saved");
        }

        class PersonValidatorStub : IPersonValidator
        {
            public bool IsValid(Person person)
            {
                return true;
            }
        }
        
        class PersonRepositoryDummy : IPersonRepository
        {
            public void Add(Person person)
            {
            }
        }

        private T Any<T>()
        {
            return Fixture.Create<T>();
        }
    }
}
