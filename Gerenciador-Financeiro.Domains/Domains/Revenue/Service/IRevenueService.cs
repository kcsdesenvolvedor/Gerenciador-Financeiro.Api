using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Domains.Domains.Revenue.Service
{
    public interface IRevenueService
    {
        void Save(Revenue revenue);
        void Update(Revenue revenue);
        void Delete(string id);
        Task<List<Revenue>> GetAllsRevenue();
        Task<Revenue> GetRevenueById(string id);
    }
}
