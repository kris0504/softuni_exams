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
            if (clientName==nameof(Student))
            {
                if (banks.FirstModel(bankName).GetType().Name==nameof(BranchBank))
                {
                    var client = new Student(clientName,id,income);
                    banks.FirstModel(bankName).AddClient(client);
                    return String.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
                }
                else
                {
                    return String.Format(OutputMessages.UnsuitableBank);
                }
            }
            else if (clientName == nameof(Adult))
            {
                if (banks.FirstModel(bankName).GetType().Name == nameof(CentralBank))
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
