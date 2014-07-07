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
    class LegacyPersonViewModelWithSimpleFactory
    {
        private readonly IPersonValidator validator;
        private readonly IPersonRepository repository;

        public LegacyPersonViewModelWithSimpleFactory()
        {
            validator = PersonValidatorFactory.Create();
            repository = PersonRepositoryFactory.Create();
        }

        public string Status { get; private set; }

        public void Save(Person person)
        {
            if (validator.IsValid(person))
            {
                repository.Add(person);
                Status = person.Name + " saved";
            }
            else
            {
                Status = "Please check the input";
            }
        }
    }

    class PersonValidatorImpl : IPersonValidator
    {

        public bool IsValid(Person person)
        {
            return !string.IsNullOrWhiteSpace(person.Name);
        }
    }

    static class PersonRepositoryFactory
    {
        private static IPersonRepository seam;

        public static IPersonRepository Create()
        {
            if (seam != null)
            {
                return seam;
            }

            return new PersonRepository();
        }

        public static void SetSeam(IPersonRepository seamed)
        {
            seam = seamed;
        }

        public static void Reset()
        {
            seam = null;
        }
    }

    static class PersonValidatorFactory
    {
        private static IPersonValidator seam;

        public static IPersonValidator Create()
        {
            if (seam != null)
            {
                return seam;
            }

            return new PersonValidatorImpl();
        }

        public static void SetSeam(IPersonValidator seamed)
        {
            seam = seamed;
        }

        public static void Reset()
        {
            seam = null;
        }
    }

    public class LegacyPersonViewModelWitFactoryTests
    {
        [Fact]
        public void Save_ValidUser_StatusWithName()
        {
            // arrange
            var validatorMock = new Mock<IPersonValidator>();
            validatorMock.Setup(x => x.IsValid(It.IsAny<Person>())).Returns(true);
            PersonValidatorFactory.SetSeam(validatorMock.Object);

            var repositoryMock = new Mock<IPersonRepository>();
            PersonRepositoryFactory.SetSeam(repositoryMock.Object);

            var person = new Person("John", DateTime.Now);
            var sut = new LegacyPersonViewModelWithSimpleFactory();

            // act
            sut.Save(person);

            // assert
            sut.Status.Should().Be("John saved");

            // cleanup
            PersonValidatorFactory.Reset();
            PersonRepositoryFactory.Reset();
        }
    }
}
