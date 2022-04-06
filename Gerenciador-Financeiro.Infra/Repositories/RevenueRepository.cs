using Gerenciador_Financeiro.Domains.Domains.Balance;
using Gerenciador_Financeiro.Domains.Domains.Balance.Service;
using Gerenciador_Financeiro.Domains.Domains.Demostrative;
using Gerenciador_Financeiro.Domains.Domains.Demostrative.Service;
using Gerenciador_Financeiro.Domains.Domains.Revenue;
using Gerenciador_Financeiro.Domains.Domains.Revenue.Repository;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Infra.Repositories
{
    public class RevenueRepository : IRevenueRepository
    {
        private FirestoreDb _dbContext = DataBaseContext.OpenConnectionDb();
        private IDemonstrativeService _demonstrativeService;
        private IBalanceService _balanceService;

        public RevenueRepository(IDemonstrativeService demonstrativeService, IBalanceService balanceService)
        {
            _demonstrativeService = demonstrativeService;
            _balanceService = balanceService;
        }
        public async void Delete(string id)
        {
            DocumentReference docRef = _dbContext.Collection("Receita").Document(id);
            DocumentSnapshot snap = await docRef.GetSnapshotAsync();

            if(snap.Exists)
            {
                Revenue revenue = snap.ConvertTo<Revenue>();

                await docRef.DeleteAsync();
                _balanceService.Delete("Credito", revenue.Id, revenue.Date, revenue.RevenueValue);
            }
        }

        public async Task<List<Revenue>> GetAllsRevenue()
        {
            List<Revenue> listRevenue = new List<Revenue>();
            Query query = _dbContext.Collection("Receita");
            QuerySnapshot snap = await query.GetSnapshotAsync();

            foreach (var item in snap)
            {
                Revenue revenue = item.ConvertTo<Revenue>();
                listRevenue.Add(revenue);
            }
            listRevenue.Reverse();
            return listRevenue;
        }

        public async Task<Revenue> GetRevenueById(string id)
        {
            DocumentReference docRef = _dbContext.Collection("Receita").Document(id);
            DocumentSnapshot snap = await docRef.GetSnapshotAsync();

            if(snap.Exists)
            {
                Revenue revenue = snap.ConvertTo<Revenue>();
                return revenue;
            }else
            {
                return null;
            }
        }

        public async void Save(Revenue revenue)
        {
            DocumentReference docRef = _dbContext.Collection("Receita").Document(revenue.Id);
            Dictionary<string, object> dic = new Dictionary<string, object>()
            {
                { "Id", revenue.Id},
                { "Descrição", revenue.Description},
                { "Data", revenue.Date},
                { "Valor", revenue.RevenueValue}
            };

            await docRef.SetAsync(dic);
            _balanceService.Save("Credito", revenue.RevenueValue, revenue.Id, revenue.Date);

        }

        public async void Update(Revenue revenue)
        {
            Revenue rev = GetRevenueById(revenue.Id).Result;

            DocumentReference docRef = _dbContext.Collection("Receita").Document(revenue.Id);
            Dictionary<string, object> dic = new Dictionary<string, object>()
            {
                { "Id", revenue.Id},
                { "Descrição", revenue.Description},
                { "Data", rev.Date},
                { "Valor", revenue.RevenueValue}
            };

            await docRef.UpdateAsync(dic);
            _balanceService.Update("Credito", revenue.RevenueValue, rev.RevenueValue, revenue.Date, revenue.Id);
        }
    }
}
