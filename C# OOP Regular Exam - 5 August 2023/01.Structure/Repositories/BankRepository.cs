﻿using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class BankRepository : IRepository<IBank>
    {
        private List<IBank> models;
        public BankRepository()
        {
            models = new List<IBank>();
        }
        public IReadOnlyCollection<IBank> Models => models.AsReadOnly();
        public void AddModel(IBank model)
        {
            models.Add(model);
        }

        public IBank FirstModel(string name)
        {
           return models.FirstOrDefault(x => x.Name == name);
        }

        public bool RemoveModel(IBank model)
        {
            return models.Remove(model);
        }
    }
}
