using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace TddSample.Tests
{
    public class ListTests
    {
        [Fact]
        public void Add_OneElement_CountReturnsOne()
        {
            // Arrange
            var list = new List<int>();

            // Act
            list.Add(3);

            // Assert
            list.Count.Should().Be(1);
        }

        [Fact]
        public void AddTests()
        {
            // COMMON ANTI-PATTERN: The One
            var list = new List<int>();

            list.Add(3);
            list.Count.Should().Be(2);

            const int addedElement = 2;
            list.Add(addedElement);
            list.Last().Should().Be(addedElement);
            list.Should().BeSameAs(new[] { 3, 2 });
              
            // there should be on unit test for each concept; not for each method!

            // tests should be trustworthy, maintainable and readable!
            // if there are not then something is wrong with the tests or the code under test

            // anti-patterns: http://blog.james-carr.org/2006/11/03/tdd-anti-patterns/
        }
    }
}
