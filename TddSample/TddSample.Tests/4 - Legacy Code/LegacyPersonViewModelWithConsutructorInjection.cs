using FluentAssertions;
using Moq;
using Xunit;

namespace TddSample.Tests

{
    class TestableLegacyPersonViewModel 
    {
        private readonly IPersonValidator validator;
        private readonly IPersonRepository repository;

        internal TestableLegacyPersonViewModel(IPersonValidator personValidator, IPersonRepository personRepository)
        {
            validator = personValidator;
            repository = personRepository;
        }

        public TestableLegacyPersonViewModel()
        {
            validator = new PersonValidatorImpl();
            repository = new PersonRepository();
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

    public class TestableLegacyPersonViewModelTests
    {
        [Fact]
        public void Save_ValidUser_StatusWithName()
        {
            // arrange
            var validatorMock = new Mock<IPersonValidator>();
            validatorMock.Setup(x => x.IsValid(It.IsAny<Person>())).Returns(true);

            var repositoryMock = new Mock<IPersonRepository>();

            var person = PersonMother.Create();
            var sut = new TestableLegacyPersonViewModel(validatorMock.Object, repositoryMock.Object);

            // act
            sut.Save(person);

            // assert
            sut.Status.Should().Be("John saved");
        }
    }
}
