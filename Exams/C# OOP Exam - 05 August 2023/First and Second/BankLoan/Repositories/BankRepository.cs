using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankLoan.Repositories
{
    public class BankRepository : IRepository<IBank>
    {
        //Ela tuka smeni HashSeta ako neshto
        private readonly HashSet<IBank> banks;

        public BankRepository()
        {
            banks = new HashSet<IBank>();
        }

        public IReadOnlyCollection<IBank> Models => banks;

        public void AddModel(IBank model)
        {
            banks.Add(model);
        }
        public bool RemoveModel(IBank model)
        {
            return banks.Remove(model);
        }

        public IBank FirstModel(string name)
        {
            return banks.FirstOrDefault(bank => bank.Name == name);
        }

    }
}
