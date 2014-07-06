using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;
using Xunit;

namespace TddSample.Tests
{
    public class PersonTests
    {
        [Fact]
        public void Age_ForPastDate_ReturnsGoodResult()
        {
            var sut = new Person("James Stone", new DateTime(2000, 2, 3));

            sut.Age.Should().Be(14);
        }
    }
}
