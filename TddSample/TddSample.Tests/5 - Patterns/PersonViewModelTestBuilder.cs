using Moq;

namespace TddSample.Tests
{
    // http://www.natpryce.com/articles/000714.html
    public class PersonViewModelTestBuilder
    {
        private IPersonValidator validator;
        private IPersonRepository repository;

        public PersonViewModelTestBuilder WithValidator(IPersonValidator personValidator)
        {
            validator = personValidator;
            return this;
        }

        public PersonViewModelTestBuilder WithValidator(IPersonRepository personRepository)
        {
            repository = personRepository;
            return this;
        }

        public PersonViewModel Build()
        {
            // default data/implementations for tests. these can be real implementations, fakes, dummies etc.
            if (validator == null)
            {
                validator = new PersonValidatorImpl();
            }

            if (repository == null)
            {
                repository =  new Mock<IPersonRepository>().Object;
            }

            return new PersonViewModel(validator, repository);
        }
    }
}