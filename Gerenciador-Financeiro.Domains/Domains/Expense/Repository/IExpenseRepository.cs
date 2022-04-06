using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Domains.Domains.Expense.Repository
{
    public interface IExpenseRepository
    {
        void Save(Expense expense);
        void Delete(string id);
        void Update(Expense expense);
        Task<List<Expense>> GetAllsExpense();
        Task<dynamic> GetExpenseById(string id);
        Task<List<Expense>> GetExpenseByName(string name);
    }
}
