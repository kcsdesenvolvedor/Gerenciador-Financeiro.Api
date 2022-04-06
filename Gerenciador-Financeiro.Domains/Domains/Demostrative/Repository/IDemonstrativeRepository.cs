using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Domains.Domains.Demostrative.Repository
{
    public interface IDemonstrativeRepository
    {
        void Save(string typeOperation, double operationValue, double value, string operationId, string date);
        void Update(double value, string date, string operationId);
        void Delete(string date, string operationId );
        Task<List<Demonstrative>> GetAllsDemonstrative();
        Task<List<Demonstrative>> GetDemonstrativeByInterval(string interval1, string interval2);
    }
}
