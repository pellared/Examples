using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddSample
{
    public class LegacyPersonViewModel
    {
        private readonly PersonRepository repository;

        public LegacyPersonViewModel()
        {
            repository = new PersonRepository();
        }

        public string Status { get; private set; }

        public void Save(Person person)
        {
            if (PersonValidator.IsValid(person))
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

    public static class PersonValidator
    {
        public static bool IsValid(Person person)
        {
            return !string.IsNullOrWhiteSpace(person.Name);
        }
    }

    public class PersonRepository : IPersonRepository
    {
        public void Add(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
