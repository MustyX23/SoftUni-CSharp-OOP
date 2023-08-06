using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankLoan.Models.Banks
{
    public abstract class Bank : IBank
    {
        private string name;
        private readonly List<ILoan> clients;
        private readonly List<IClient> loans;

        public Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            loans = new List<IClient>();
            clients = new List<ILoan>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Capacity { get; private set; }

        public IReadOnlyCollection<ILoan> Loans => clients.AsReadOnly();

        public IReadOnlyCollection<IClient> Clients => loans.AsReadOnly();

        public double SumRates()
        {
            return clients.Sum(loan => loan.InterestRate);
        }

        public void AddClient(IClient Client)
        {
            if (Clients.Count >= Capacity)
            {
                throw new ArgumentException("Not enough capacity for this client.");
            }

            loans.Add(Client);

        }
        public void RemoveClient(IClient client)
        {
            loans.Remove(client);
        }

        public void AddLoan(ILoan loan)
        {
            clients.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Name: {Name}, Type: {GetType().Name}");
            sb.Append(Environment.NewLine);
            if (loans.Count == 0)
            {
                sb.Append("Clients: none");
                sb.Append(Environment.NewLine);
            }
            else
            {
                sb.Append("Clients: ");
                sb.Append(string.Join(", ", loans.Select(c => c.Name)));
                sb.Append(Environment.NewLine);
            }
            sb.Append($"Loans: {clients.Count}, Sum of Rates: {SumRates()}");
            return sb.ToString().TrimEnd();
        }

    }
}
