using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddSample.Tests
{
    internal class PersonTestBuilder
    {
        private string name = "John";
        private DateTime birthday = new DateTime(1987, 1, 5);

        public void WithName(string name)
        {
            this.name = name;
        }

        public void WithName(DateTime birthday)
        {
            this.birthday = birthday;
        }
  
        public Person Build()
        {
            return new Person(name, birthday);
        }
    }
}
