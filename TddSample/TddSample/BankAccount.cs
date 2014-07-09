using System;

namespace TddSample
{
    public class BankAccount
    {
        public BankAccount(double balance)
        {
            Balance = balance;
        }

        public double Balance { get; private set; }

        public void Debit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            if (amount > Balance)
            {
                throw new ArgumentException("amount is greater then current balance");
            }

            Balance += amount;
        }

        public void Credit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            Balance += amount;
        }
    }
}
