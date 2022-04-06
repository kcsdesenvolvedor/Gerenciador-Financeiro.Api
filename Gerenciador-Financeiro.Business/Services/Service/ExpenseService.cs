
using Gerenciador_Financeiro.Domains.Domains.Expense;
using Gerenciador_Financeiro.Domains.Domains.Expense.Repository;
using Gerenciador_Financeiro.Domains.Domains.Expense.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Business.Services.Service
{
    public class ExpenseService : IExpenseService
    {
        private IExpenseRepository _repository;
        public ExpenseService(IExpenseRepository repository)
        {
            _repository = repository;
        }
        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public Task<List<Expense>> GetAllsExpense()
        {
            return _repository.GetAllsExpense();
        }

        public Task<dynamic> GetExpenseById(string id)
        {
            return _repository.GetExpenseById(id);
        }

        public Task<List<Expense>> GetExpenseByName(string name)
        {
            return _repository.GetExpenseByName(name);
        }

        public void Save(Expense expense)
        {
            _repository.Save(expense);
        }

        public void Update(Expense expense)
        {
            _repository.Update(expense);
        }
    }
}
