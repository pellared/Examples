using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ploeh.AutoFixture;
using FluentAssertions;
using Xunit;

namespace TddSample.Tests
{
    public class BankAccountTests
    {
        private readonly static Fixture Fixture = new Fixture();

        [Fact]
        public void Credit_WhenPostivie_ThenBalanceIsInreased()
        {
            var bankAccount = new BankAccount(100);

            bankAccount.Credit(12.5);

            bankAccount.Balance.Should().Be(112.5);
        }

        [Fact]
        public void Debit_MoreThanBalance_ThrowsArgumentException()
        {
            var sut = new BankAccount(12.5);

            Action act = () => sut.Debit(13);

            act.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Debit_Negative_ThrowsArgumentOutOfRangeException()
        {
            var sut = new BankAccount(Any<double>());

            Action act = () => sut.Debit(-1);

            act.ShouldThrow<ArgumentOutOfRangeException>();
        }

        // http://blog.ploeh.dk/2009/03/05/ConstrainedNon-Determinism/
        private T Any<T>()
        {
            return Fixture.Create<T>();
        }
    }
}
