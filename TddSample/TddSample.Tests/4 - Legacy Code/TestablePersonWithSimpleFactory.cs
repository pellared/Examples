using System;
using FluentAssertions;
using Xunit;
using Utils;

namespace TddSample.Tests
{
    // http://ayende.com/blog/3408/dealing-with-time-in-tests
    // see also a popular alternative: http://martinfowler.com/bliki/ClockWrapper.html
    public static class Clock
    {
        private static DateTime? seam;

        public static IDisposable Seam(DateTime value)
        {
            seam = value;
            Action reset = () => seam = null;
            return new ReleaseAction(reset);
        }

        public static DateTime Now
        {
            get
            {
                if (seam != null)
                {
                    return seam.Value;
                }

                return DateTime.Now;
            }
        }
    }

    public class PersonTestable
    {
        public PersonTestable(string name, DateTime birthday)
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
                return CalculateAge(Birthday, Clock.Now);
            }
        }

        private int CalculateAge(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return age;
        }
    }

    public class PersonTestableTests
    {
        [Fact]
        public void Age_ForPastDate_ReturnsAlwaysGoodResult()
        {
            var sut = new PersonTestable("James Stone", new DateTime(2000, 2, 3));

            using (Clock.Seam(new DateTime(2014, 5, 12)))
            {
                sut.Age.Should().Be(14);
            }
        }
    }
}
