using Gerenciador_Financeiro.Domains.Domains.Demostrative;
using Gerenciador_Financeiro.Domains.Domains.Demostrative.Repository;
using Gerenciador_Financeiro.Domains.Domains.Demostrative.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Business.Services.Service
{
    public class DemonstrativeService : IDemonstrativeService
    {
        private IDemonstrativeRepository _demonstrativeRepository;

        public DemonstrativeService(IDemonstrativeRepository demonstrativeRepository)
        {
            _demonstrativeRepository = demonstrativeRepository;
        }
        public void Delete(string date, string operationId)
        {
            _demonstrativeRepository.Delete(date, operationId);
        }

        public Task<List<Demonstrative>> GetAllsDemonstrative()
        {
            return _demonstrativeRepository.GetAllsDemonstrative();
        }

        public Task<List<Demonstrative>> GetDemonstrativeByInterval(string interval1, string interval2)
        {
            return _demonstrativeRepository.GetDemonstrativeByInterval(interval1, interval2);
        }

        public void Save(string typeOperation, double operationValue, double value, string operationId, string date)
        {
            _demonstrativeRepository.Save(typeOperation, operationValue, value, operationId, date);
        }

        public void Update(double value, string date, string operationId)
        {
            _demonstrativeRepository.Update(value, date, operationId);
        }
    }
}
