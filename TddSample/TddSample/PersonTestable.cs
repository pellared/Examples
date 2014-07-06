using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddSample
{
    public interface IClock
    {
        DateTime Now { get; }
    }

    public class PersonTestable
    {
        private readonly IClock clock;

        public PersonTestable(string name, DateTime birthday, IClock clock)
        {
            Name = name;
            Birthday = birthday;
            this.clock = clock;
        }

        public string Name { get; private set; }

        public DateTime Birthday { get; private set; }

        public int Age
        {
            get
            {
                return CalculateAge(Birthday, clock.Now);
            }
        }

        private int CalculateAge(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return age;
        }
    }
}
