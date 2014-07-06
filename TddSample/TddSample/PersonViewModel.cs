using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddSample
{
    public interface IPersonListLoader
    {
        IEnumerable<Person> Load();
    }

    public class PersonViewModel
    {
        private IPersonListLoader personListLoader;
        private Lazy<List<Person>> personList;

        public PersonViewModel(IPersonListLoader personListLoader)
        {
            this.personListLoader = personListLoader;
            personList = new Lazy<List<Person>>(() => personListLoader.Load().ToList());
        }

        public Person FirstPerson
        {
            get
            {
                return personList.Value.FirstOrDefault();
            }
        }

    }
}
