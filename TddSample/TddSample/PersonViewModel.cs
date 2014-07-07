using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddSample
{
    public interface IPersonValidator
    {
        bool IsValid(Person person);
    }

    public interface IPersonRepository
    {
        void Add(Person person);
    }

    public class PersonViewModel
    {
        private readonly IPersonValidator validator;
        private readonly IPersonRepository repository;

        public PersonViewModel(IPersonValidator personValidator, IPersonRepository personRepository)
        {
            validator = personValidator;
            repository = personRepository;
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
}
