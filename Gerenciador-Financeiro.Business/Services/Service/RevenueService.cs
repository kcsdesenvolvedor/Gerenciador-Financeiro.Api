using Gerenciador_Financeiro.Domains.Domains.Revenue;
using Gerenciador_Financeiro.Domains.Domains.Revenue.Repository;
using Gerenciador_Financeiro.Domains.Domains.Revenue.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Business.Services.Service
{
    public class RevenueService : IRevenueService
    {
        private IRevenueRepository _repository;

        public RevenueService(IRevenueRepository repository)
        {
            _repository = repository;
        }
        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public Task<List<Revenue>> GetAllsRevenue()
        {
            return _repository.GetAllsRevenue();
        }

        public Task<Revenue> GetRevenueById(string id)
        {
            return _repository.GetRevenueById(id);
        }

        public void Save(Revenue revenue)
        {
            _repository.Save(revenue);
        }

        public void Update(Revenue revenue)
        {
            _repository.Update(revenue);
        }
    }
}
