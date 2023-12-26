using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private int capacity;
        private List<ILoan> loans;
        private List<IClient> clients;
        public Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            loans =new List<ILoan>();
            clients = new List<IClient>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(String.Format(ExceptionMessages.BankNameNullOrWhiteSpace));
                }
                name = value;
            }
        }

        public int Capacity { get => capacity; private set => capacity = value; }

        public IReadOnlyCollection<ILoan> Loans { get => loans; }

        public IReadOnlyCollection<IClient> Clients { get => clients; }

        public void AddClient(IClient Client)
        {
            if (clients.Count>=capacity)
            {
                throw new ArgumentException("Not enough capacity for this client.");
            }
            clients.Add(Client);
        }

        public void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {name}, Type: {this.GetType().Name}");
            if (clients.Count==0)
            {
                sb.AppendLine("Clients: none");
            }
            else
            {
                string a = "";
                char[] b = { ' ', ',' };
                foreach (var client in clients)
                {
                    a += client.Name + ", ";
                }
                sb.AppendLine($"Clients: {a.Trim(b)}");
            }
            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {SumRates()}");
            return sb.ToString().TrimEnd();
        }

        public void RemoveClient(IClient Client)
        {
            clients.Remove(Client);
        }

        public double SumRates()
        {
            return loans.Sum(x => x.InterestRate);
        }
    }
}
