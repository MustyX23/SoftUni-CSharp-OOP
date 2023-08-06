using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private readonly List<ILoan> loans;

        public LoanRepository()
        {
            loans = new List<ILoan>();
        }
        public IReadOnlyCollection<ILoan> Models => loans.AsReadOnly();
        public bool RemoveModel(ILoan model)
        {
            return loans.Remove(model);
        }

        public void AddModel(ILoan model)
        {
            loans.Add(model);
        }

        public ILoan FirstModel(string name)
        {
            return loans.FirstOrDefault(loan => loan.GetType().Name == name);
        }

    }
}
