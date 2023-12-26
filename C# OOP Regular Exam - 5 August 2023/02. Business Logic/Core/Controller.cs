using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Repositories;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private LoanRepository loans;
        private BankRepository banks;
        public Controller()
        {
            loans = new LoanRepository();
            banks = new BankRepository();
        }
        public string AddBank(string bankTypeName, string name)
        {
            if (bankTypeName==nameof(CentralBank))
            {
                banks.AddModel(new CentralBank(name));
                return String.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
            }
            else if (bankTypeName == nameof(BranchBank))
            {
                banks.AddModel(new BranchBank(name));
                return String.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
            }
            else
            {
                throw new ArgumentException(String.Format(ExceptionMessages.BankTypeInvalid));
            }
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            if (clientTypeName=="Student")
            {
                if (banks.FirstModel(bankName).GetType().Name=="BranchBank")
                {
                    Student client = new Student(clientName,id,income);
                    var bank = banks.FirstModel(bankName);
                    bank.AddClient(client);
                    return String.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
                }
                else
                {
                    return String.Format(OutputMessages.UnsuitableBank);
                }
            }
            else if (clientTypeName == "Adult")
            {
                if (banks.FirstModel(bankName).GetType().Name == "CentralBank")
                {
                    var client = new Adult(clientName, id, income);
                    banks.FirstModel(bankName).AddClient(client);
                    return String.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
                }
                else
                {
                    return String.Format(OutputMessages.UnsuitableBank);
                }
            }
            else
            {
                throw new ArgumentException(String.Format(ExceptionMessages.ClientTypeInvalid));
            }
        }

        public string AddLoan(string loanTypeName)
        {
            if (loanTypeName == nameof(MortgageLoan))
            {
                loans.AddModel(new MortgageLoan());
                return String.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
            }
            else if (loanTypeName == nameof(StudentLoan))
            {
                loans.AddModel(new StudentLoan());
                return String.Format(OutputMessages.LoanSuccessfullyAdded,loanTypeName);
            }
            else
            {
                throw new ArgumentException(String.Format(ExceptionMessages.LoanTypeInvalid));
            }
        }

        public string FinalCalculation(string bankName)
        {
            var bank = banks.FirstModel(bankName);
           double income= bank.Clients.Sum(x => x.Income);
            double am = bank.Loans.Sum(x => x.Amount);
            double funds = income + am;
            return $"The funds of bank {bankName} are {funds:f2}.";


        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            if (loans.FirstModel(loanTypeName) is not null)
            {
             var res = loans.FirstModel(loanTypeName);
                        banks.FirstModel(bankName).AddLoan(res);
                        loans.RemoveModel(res);
                
            }
            else
            {
                throw new ArgumentException(String.Format(ExceptionMessages.MissingLoanFromType,loanTypeName));
            }
            return String.Format(OutputMessages.LoanReturnedSuccessfully,loanTypeName,bankName);
        }

        public string Statistics()
        {
            StringBuilder sb= new StringBuilder();
            foreach (var item in banks.Models)
            {
                sb.AppendLine(item.GetStatistics());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
