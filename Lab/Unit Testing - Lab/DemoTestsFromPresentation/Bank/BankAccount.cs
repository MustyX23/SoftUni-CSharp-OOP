using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class BankAccount
    {
        public BankAccount(decimal amount)
        {
            Amount = amount;
        }

        public decimal Account { get; set; }
        public decimal Amount { get; set; }
    }
}
