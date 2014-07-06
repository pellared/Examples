using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Credit_WhenPostivie_ThenBalanceIsInreased();
            Debit_WhenLessThenBalance_ThenBalanceIsDecreased();
        }

        static void Credit_WhenPostivie_ThenBalanceIsInreased()
        {
            var bankAccount = new BankAccount(100);

            bankAccount.Credit(12.5);

            Assert(bankAccount.Balance == 112.5);
        }

        static void Debit_WhenLessThenBalance_ThenBalanceIsDecreased()
        {
            var bankAccount = new BankAccount(123);

            bankAccount.Debit(23);

            Assert(bankAccount.Balance == 100);
        }

        private static void Assert(bool condition)
        {
            var stackTrace = new StackTrace();
            string testName = stackTrace.GetFrame(1).GetMethod().Name;
            if (!condition)
            {
                Console.WriteLine("FAILED\t " + testName);
            }
        }
    }
}
