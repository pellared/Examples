using System;
using FluentAssertions;
using Moq;
using Utils;
using Xunit;
using Ploeh.AutoFixture;

namespace TddSample.Tests
{
    public class PersonViewModelTests
    {
        private readonly PersonViewModel sut;
        private readonly Mock<IPersonValidator> validatorMock;
        private readonly Mock<IPersonRepository> repositoryMock;

        public PersonViewModelTests()
        {
            validatorMock = new Mock<IPersonValidator>();
            repositoryMock = new Mock<IPersonRepository>();
            sut = new PersonViewModel(validatorMock.Object, repositoryMock.Object);
        }

        [Fact]
        public void Save_ValidUser_StatusWithName()
        {
            validatorMock.Setup(x => x.IsValid(It.IsAny<Person>())).Returns(true);
            var person = new Person("John", Build.Any<DateTime>());

            sut.Save(person);

            sut.Status.Should().Be("John saved");
        }

        [Fact]
        public void Save_InvalidUser_ErrorStatus()
        {
            validatorMock.Setup(x => x.IsValid(It.IsAny<Person>())).Returns(false);

            sut.Save(Build.Any<Person>());

            sut.Status.Should().Be("Please check the input");
        }
    }
}
