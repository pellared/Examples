using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;
using Moq;
using Xunit;

namespace TddSample.Tests
{
    public class PersonViewModelWithoutMocksTests
    {
        [Fact]
        public void FirstPerson_PersonListReturnsAList_ReturnsFirstPersonFromTheList()
        {
            var personListStub = new PersonListLoaderStub();
            PersonViewModel sut = new PersonViewModel(personListStub);

            Person result = sut.FirstPerson;

            result.Should().Be(personListStub.FirstPerson);
        }

        class PersonListLoaderStub : IPersonListLoader
        {
            public Person FirstPerson = new Person("John", new DateTime(1987, 12, 1));

            public IEnumerable<Person> Load()
            {
                yield return FirstPerson;
                yield return new Person("Bob", new DateTime(1977, 1, 12));
                yield return new Person("Dave", new DateTime(2001, 3, 5)); 
            }
        }
    }
}
