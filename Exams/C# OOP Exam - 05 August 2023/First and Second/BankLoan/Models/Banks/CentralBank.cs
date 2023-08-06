using System;
using System.Collections.Generic;
using System.Text;

namespace BankLoan.Models.Banks
{
    public class CentralBank : Bank
    {
        public CentralBank(string name)
            : base(name, 50)
        {
        }
    }
}
