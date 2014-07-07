using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace TddSample.Tests
{
    public class PersonTestableTests
    {
        [Fact]
        public void Age_ForPastDate_ReturnsAlwaysGoodResult()
        {
            var clockStub = new Mock<IClock>();
            clockStub.Setup(x => x.Now).Returns(new DateTime(2014, 5, 12));
            var sut = new PersonTestable("James Stone", new DateTime(2000, 2, 3), clockStub.Object);

            sut.Age.Should().Be(14);
        }
    }
}
