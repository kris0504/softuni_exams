using BankLoan.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Loan : ILoan
    {
        private double amount;
        private int interestRate;
        public Loan(double amount, int interestRate)
        {
            Amount = amount;
            InterestRate = interestRate;
            
        }

        public int InterestRate { get => interestRate; private set => interestRate = value; }

        public double Amount { get => amount; private set => amount = value; }
    }
}
