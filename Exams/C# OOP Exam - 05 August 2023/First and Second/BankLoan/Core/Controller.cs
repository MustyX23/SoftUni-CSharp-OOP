using BankLoan.Core.Contracts;
using BankLoan.Models.Banks;
using BankLoan.Models.Clients;
using BankLoan.Models.Contracts;
using BankLoan.Models.Loans;
using BankLoan.Repositories;
using BankLoan.Repositories.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private IRepository<ILoan> loans;
        private IRepository<IBank> banks;

        public Controller()
        {
            loans = new LoanRepository();
            banks = new BankRepository();
        }
        public string AddBank(string bankTypeName, string name)
        {
            if (bankTypeName != nameof(BranchBank) && bankTypeName != nameof(CentralBank))
            {
                throw new ArgumentException(ExceptionMessages.BankTypeInvalid);
            }

            IBank bank = null;

            if (bankTypeName == nameof(BranchBank))
            {
                bank = new BranchBank(name);
            }
            else if (bankTypeName == nameof(CentralBank))
            {
                bank = new CentralBank(name);
            }

            banks.AddModel(bank);
            return String.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
        }
        public string AddLoan(string loanTypeName)
        {
            if (loanTypeName != nameof(StudentLoan) && loanTypeName != nameof(MortgageLoan))
            {
                throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);
            }

            ILoan loan = null;

            if (loanTypeName == nameof(StudentLoan))
            {
                loan = new StudentLoan();
            }
            else if (loanTypeName == nameof(MortgageLoan))
            {
                loan = new MortgageLoan();
            }

            loans.AddModel(loan);
            return String.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
        }
        public string ReturnLoan(string bankName, string loanTypeName)
        {
            ILoan loan = loans.FirstModel(loanTypeName);
            IBank bank = banks.FirstModel(bankName);

            if (loan == null)
            {
                throw new ArgumentException($"Loan of type {loanTypeName} is missing.");
            }

            bank.AddLoan(loan);


            loans.RemoveModel(loan);
            return String.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
        }
        
        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            if (clientTypeName != nameof(Student) && clientTypeName != nameof(Adult))
            {
                throw new ArgumentException(ExceptionMessages.ClientTypeInvalid);
            }

            IBank bank = banks.FirstModel(bankName);

            IClient client = null;

            if (clientTypeName == nameof(Student))
            {
                if (bank is BranchBank)
                {
                    client = new Student(clientName, id, income);
                }
                else
                {
                    return "Unsuitable bank.";
                }                
            }
            else if (clientTypeName == nameof(Adult))
            {
                if (bank is CentralBank)
                {
                    client = new Adult(clientName, id, income);
                }
                else
                {
                    return "Unsuitable bank.";
                }               
            }

            bank.AddClient(client);
            return String.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
        }


        public string FinalCalculation(string bankName)
        {
            IBank bank = banks.FirstModel(bankName);

            double totalIncomeOfClients = bank.Clients.Sum(client => client.Income);
            double totalLoansAmount = bank.Loans.Sum(loan => loan.Amount);

            double funds = totalIncomeOfClients + totalLoansAmount;

            return $"The funds of bank {bankName} are {funds:F2}.";
        }
        public string Statistics()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var bank in banks.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }

            return sb.ToString().TrimEnd();
        }
    }
}