using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Domains.Domains.Balance.Repository
{
    public interface IBalanceRepository
    {
        public void Save(string typeOperation, double operationValue, string operationId, string date);
        public void Delete(string typeOperation, string operationId, string date, double currentValue);
        public void Update(string typeOperation, double operationValue, double operationValueOld, string date, string operationId);
        public Balance GetBalance(Balance balance);
        Task<dynamic> GetCurrentBalance();
    }
}
