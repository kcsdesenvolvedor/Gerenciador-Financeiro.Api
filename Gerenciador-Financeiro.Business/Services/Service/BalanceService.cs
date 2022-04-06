using Gerenciador_Financeiro.Domains.Domains.Balance;
using Gerenciador_Financeiro.Domains.Domains.Balance.Repository;
using Gerenciador_Financeiro.Domains.Domains.Balance.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Business.Services.Service
{
    public class BalanceService : IBalanceService
    {
        private IBalanceRepository _balanceRepository;

        public BalanceService(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        public void Delete(string typeOperation, string operationId, string date, double currentValue)
        {
            _balanceRepository.Delete(typeOperation, operationId, date, currentValue);
        }

        public Balance GetBalance(Balance balance)
        {
            return _balanceRepository.GetBalance(balance);
        }

        public Task<dynamic> GetCurrentBalance()
        {
            return _balanceRepository.GetCurrentBalance();
        }

        public void Save(string typeOperation, double operationValue, string operationId, string date)
        {
            _balanceRepository.Save(typeOperation, operationValue, operationId, date);
        }

        public void Update(string typeOperation, double operationValue, double operationValueOld, string date, string operationId)
        {
            _balanceRepository.Update(typeOperation, operationValue, operationValueOld, date, operationId);
        }
    }
}
