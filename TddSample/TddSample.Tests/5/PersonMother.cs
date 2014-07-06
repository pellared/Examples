using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddSample.Tests
{
    internal class PersonMother
    {
        public static Person Create(DateTime birthday)
        {
            return new Person("John", birthday);
        }
    }
}
