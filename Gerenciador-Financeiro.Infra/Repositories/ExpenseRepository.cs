
using Gerenciador_Financeiro.Domains.Domains.Balance;
using Gerenciador_Financeiro.Domains.Domains.Balance.Service;
using Gerenciador_Financeiro.Domains.Domains.Demostrative;
using Gerenciador_Financeiro.Domains.Domains.Demostrative.Service;
using Gerenciador_Financeiro.Domains.Domains.Expense;
using Gerenciador_Financeiro.Domains.Domains.Expense.Repository;
using Google.Cloud.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Infra.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private FirestoreDb _dbContext = DataBaseContext.OpenConnectionDb();
        private IDemonstrativeService _demonstrativeService;
        private IBalanceService _balanceService;

        public ExpenseRepository(IDemonstrativeService demonstrativeService, IBalanceService balanceService)
        {
            _demonstrativeService = demonstrativeService;
            _balanceService = balanceService;
        }
        public async void Delete(string id)
        {
            DocumentReference docRef = _dbContext.Collection("Despesa").Document(id);
            DocumentSnapshot snap = await docRef.GetSnapshotAsync();
            if(snap.Exists)
            {
                Expense expense = snap.ConvertTo<Expense>();

                await docRef.DeleteAsync();
                _balanceService.Delete("Debito", expense.Id, expense.Date, expense.Price);
            }
        }

        public async Task<List<Expense>> GetAllsExpense()
        {
            List<Expense> expenseList = new List<Expense>();

            Query query = _dbContext.Collection("Despesa");
            QuerySnapshot snap = await query.GetSnapshotAsync();

            foreach (DocumentSnapshot item in snap)
            {
                Expense expense = item.ConvertTo<Expense>();
                expenseList.Add(expense);
            }
            expenseList.Reverse();
            return expenseList;
        }

        public async Task<dynamic> GetExpenseById(string id)
        {
            Expense expense = new Expense();
            Query query = _dbContext.Collection("Despesa");
            QuerySnapshot snap = await query.GetSnapshotAsync();

            foreach (DocumentSnapshot item in snap)
            {
                if(item.Id == id)
                    expense = item.ConvertTo<Expense>();
            }
            if (expense.Name == null)
            {
                return new { message = "Despesa não encontrada" };
            }
            return expense;
        }

        public async Task<List<Expense>> GetExpenseByName(string name)
        {
            List<Expense> expenseList = new List<Expense>();

            Query query = _dbContext.Collection("Despesa");
            QuerySnapshot snap = await query.GetSnapshotAsync();

            foreach (DocumentSnapshot item in snap)
            {
                Expense expenseVerification = item.ConvertTo<Expense>();
                if(expenseVerification.Name.ToUpper().Contains(name.ToUpper()))
                    expenseList.Add(expenseVerification);
            }

            return expenseList;
        }

        public async void Save(Expense expense)
        {

            DocumentReference docRef = _dbContext.Collection("Despesa").Document(expense.Id);
            Dictionary<string, object> dic = new Dictionary<string, object>()
            {
                { "Id", expense.Id},
                { "Name", expense.Name},
                { "Date", expense.Date},
                { "Price", expense.Price},
                { "Image", expense.Image},
                { "Annotation", expense.Annotation}
            };

            ArrayList tag = new ArrayList();
            foreach (var item in expense.Tag)
            {
                tag.Add(item);
            }

            dic.Add("Tag", tag);

            await docRef.SetAsync(dic);
            _balanceService.Save("Debito", expense.Price, expense.Id, expense.Date);
        }

        public async void Update(Expense expense)
        {
            Expense exp = await GetExpenseById(expense.Id);
            DocumentReference docRef = _dbContext.Collection("Despesa").Document(expense.Id);
            Dictionary<string, object> dic = new Dictionary<string, object>()
            {
                { "Name", expense.Name},
                { "Date", exp.Date},
                { "Price", expense.Price},
                { "Image", expense.Image},
                { "Annotation", expense.Annotation}
            };

            ArrayList tag = new ArrayList();
            foreach(var item in expense.Tag)
            {
                tag.Add(item);
            }

            dic.Add("Tag", tag);

            await docRef.UpdateAsync(dic);
            _balanceService.Update("Debito", expense.Price, exp.Price, exp.Date, expense.Id);
        }
    }
}
