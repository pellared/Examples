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
    public class PersonViewModelTests
    {
        private readonly PersonViewModel sut;
        private readonly Mock<IPersonListLoader> personListLoaderMock;

        public PersonViewModelTests()
        {
            personListLoaderMock = new Mock<IPersonListLoader>();
            sut = new PersonViewModel(personListLoaderMock.Object);
        }

        [Fact]
        public void FirstPerson_PersonListReturnsAList_ReturnsFirstPersonFromTheList()
        {
            Person firstPerson = new Person("John", new DateTime(1987, 12, 1));
            personListLoaderMock
                .Setup(x => x.Load())
                .Returns(new [] 
                {
                    firstPerson,
                    new Person("Bob", new DateTime(1977, 1, 12)),
                    new Person("Dave", new DateTime(2001, 3, 5))
                });

            Person result = sut.FirstPerson;

            result.Should().Be(firstPerson);
        }

        [Fact]
        public void FirstPerson_InvokedManyTimes_LoadsTheListOnlyOnce()
        {
            Person result = sut.FirstPerson;
            result = sut.FirstPerson;

            personListLoaderMock.Verify(x => x.Load(), Times.Once);
        }
    }
}
