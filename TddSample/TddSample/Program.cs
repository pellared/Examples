using System;
using System.Diagnostics;

namespace TddSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            program.ExecuteUnitTests();
        }

        void ExecuteUnitTests()
        {
            Credit_WhenPostivie_ThenBalanceIsInreased();
            Debit_WhenLessThenBalance_ThenBalanceIsDecreased();
        }

        void Credit_WhenPostivie_ThenBalanceIsInreased()
        {
            var bankAccount = new BankAccount(100);

            bankAccount.Credit(12.5);

            Assert(bankAccount.Balance == 112.5);
        }

        void Debit_WhenLessThenBalance_ThenBalanceIsDecreased()
        {
            var bankAccount = new BankAccount(123);

            bankAccount.Debit(23);

            Assert(bankAccount.Balance == 100);
        }

        private void Assert(bool condition)
        {
            var stackTrace = new StackTrace();
            string testName = stackTrace.GetFrame(1).GetMethod().Name;
            if (condition)
            {
                Console.WriteLine("PASSED\t " + testName);
            }
            else
            {
                Console.WriteLine("FAILED\t " + testName);
            }
        }
    }
}
