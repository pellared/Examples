using System;
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
