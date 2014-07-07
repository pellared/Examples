using System;
using FluentAssertions;
using Xunit;

namespace TddSample.Tests
{
    class LegacyPersonViewModelWithExtractAndOverride
    {
        private readonly PersonRepository repository;

        public LegacyPersonViewModelWithExtractAndOverride()
        {
            repository = new PersonRepository();
        }

        public string Status { get; private set; }

        public void Save(Person person)
        {
            if (Add(person))
            {
                Status = person.Name + " saved";
            }
            else
            {
                Status = "Please check the input";
            }
        }

        virtual protected bool Add(Person person)
        {
           bool isValid = PersonValidator.IsValid(person);
            if (!isValid)
            {
                return false;
            }

            repository.Add(person);
            return true;
        }
    }

    public class LegacyPersonViewModelWithExtractAndOverrideTests
    {
        [Fact]
        public void Save_ValidUser_StatusWithName()
        {
            var person = PersonMother.Create(DateTime.Now);
            LegacyPersonViewModelWithExtractAndOverride sut = new LegacyPersonViewModelWithExtractAndOverrideUnderTest();

            sut.Save(person);

            sut.Status.Should().Be("John saved");
        }

        class LegacyPersonViewModelWithExtractAndOverrideUnderTest : LegacyPersonViewModelWithExtractAndOverride
        {
            protected override bool Add(Person person)
            {
                return true;
            }
        }
    }
}
