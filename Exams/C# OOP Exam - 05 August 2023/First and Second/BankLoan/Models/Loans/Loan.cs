using BankLoan.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankLoan.Models.Loans
{
    public abstract class Loan : ILoan
    {
        protected Loan(int interestRate, double amount)
        {
            InterestRate = interestRate;
            Amount = amount;
        }

        public int InterestRate { get; private set; }

        public double Amount { get; private set; }
    }
}
