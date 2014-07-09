using System;

namespace TddSample
{
    public class Person
    {
        public Person(string name, DateTime birthday)
        {
            Name = name;
            Birthday = birthday;
        }

        public string Name { get; private set; }

        public DateTime Birthday { get; private set; }

        public int Age
        {
            get
            {
                return CalculateAge(Birthday, DateTime.Now);
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
