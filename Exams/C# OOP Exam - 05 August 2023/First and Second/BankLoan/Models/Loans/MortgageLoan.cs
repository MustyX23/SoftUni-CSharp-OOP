using System;
using System.Collections.Generic;
using System.Text;

namespace BankLoan.Models.Loans
{
    public class MortgageLoan : Loan
    {
        public MortgageLoan()
            : base(3, 50000)
        {
        }
    }
}
