using System;
using System.Collections.Generic;
using System.Text;

namespace BankLoan.Models.Loans
{
    public class StudentLoan : Loan
    {
        public StudentLoan()
            : base(1, 10000)
        {
        }
    }
}
