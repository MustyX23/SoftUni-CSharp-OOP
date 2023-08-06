using System;
using System.Collections.Generic;
using System.Text;

namespace BankLoan.Models.Clients
{
    public class Adult : Client
    {
        //Has an initial interest of 4 percent.

        public Adult(string name, string id, double income)
            : base(name, id, 4, income)
        {

        }

        public override void IncreaseInterest()
        {
            Interest += 2;
        }
    }
}
