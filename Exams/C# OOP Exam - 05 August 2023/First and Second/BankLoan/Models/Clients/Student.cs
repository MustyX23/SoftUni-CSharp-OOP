using System;
using System.Collections.Generic;
using System.Text;

namespace BankLoan.Models.Clients
{
    public class Student : Client
    {
        //Has an initial interest of 2 percent.
        public Student(string name, string id, double income)
            : base(name, id, 2, income)
        {
        }

        public override void IncreaseInterest()
        {
            Interest += 1;
        }
    }
}
