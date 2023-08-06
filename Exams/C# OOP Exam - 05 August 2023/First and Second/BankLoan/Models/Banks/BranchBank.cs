using System;
using System.Collections.Generic;
using System.Text;

namespace BankLoan.Models.Banks
{
    public class BranchBank : Bank
    {
        public BranchBank(string name)
            : base(name, 25)
        {
        }
    }
}
