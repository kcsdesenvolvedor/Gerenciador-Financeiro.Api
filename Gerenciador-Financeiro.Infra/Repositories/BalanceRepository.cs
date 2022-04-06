using Gerenciador_Financeiro.Domains.Domains.Balance;
using Gerenciador_Financeiro.Domains.Domains.Balance.Repository;
using Gerenciador_Financeiro.Domains.Domains.Demostrative.Service;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Infra.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private FirestoreDb _dbContex = DataBaseContext.OpenConnectionDb();
        private IDemonstrativeService _demonstrativeService;

        public BalanceRepository(IDemonstrativeService demonstrativeService)
        {
            _demonstrativeService = demonstrativeService;
        }
        public Balance GetBalance(Balance balance)
        {
            throw new NotImplementedException();
        }

        public async void Save(string typeOperation, double operationValue, string operationId, string date)
        {
            Balance balance = new Balance();
            Balance balanceExist = GetCurrentBalance().Result;
            if(typeOperation == "Credito")
            {
                var value = balanceExist.BalanceValue + operationValue;
                AddBalance(typeOperation, balance.Id, value, balanceExist.BalanceValue);
                _demonstrativeService.Save(typeOperation, operationValue, value, operationId, date);
            }
            else
            {
                var value = balanceExist.BalanceValue - operationValue;
                AddBalance(typeOperation, balance.Id, value, balanceExist.BalanceValue);
                _demonstrativeService.Save(typeOperation, operationValue, value, operationId, date);
            }
            
        }

        public void Update(string typeOperation, double newValue, double currentValue, string date, string operationId)
        {
            Balance balance = new Balance();
            Balance balanceExist = GetCurrentBalance().Result;

            if(typeOperation == "Credito")
            {
                var value = (balanceExist.BalanceValue - currentValue) + newValue;
                UpdateBalance(balance.Id, value);
                _demonstrativeService.Update(newValue, date, operationId);
            }else
            {
                var value = (balanceExist.BalanceValue + currentValue) - newValue;
                UpdateBalance(balance.Id, value);
                _demonstrativeService.Update(newValue, date, operationId);
            }
        }

        public void Delete(string typeOperation, string operationId, string date, double currentValue)
        {
            Balance balance = new Balance();
            Balance balanceExist = GetCurrentBalance().Result;

            if (typeOperation == "Credito")
            {
                var value = balanceExist.BalanceValue - currentValue;
                UpdateBalance(balance.Id, value);
                _demonstrativeService.Delete(date, operationId);
            }
            else
            {
                var value = balanceExist.BalanceValue + currentValue;
                UpdateBalance(balance.Id, value);
                _demonstrativeService.Delete(date, operationId);
            }
        }
        async void AddBalance(string typeOperation, string id, double value, double valueOld)
        {
            DocumentReference docRef = _dbContex.Collection("Saldo").Document(id);
            Dictionary<string, object> dic = new Dictionary<string, object>()
                {
                    { "Id", id},
                    { "Valor Atual", value}
                };

            await docRef.SetAsync(dic);
        }

        async void UpdateBalance(string id, double value)
        {
            DocumentReference docRef = _dbContex.Collection("Saldo").Document(id);
            Dictionary<string, object> dic = new Dictionary<string, object>()
                {
                    { "Id", id},
                    { "Valor Atual", value}
                };

            await docRef.UpdateAsync(dic);
        }


        public async Task<dynamic> GetCurrentBalance()
        {
            //DocumentReference docRef = _dbContex.Collection("Balance").Document(id);
            //DocumentSnapshot snap = await docRef.GetSnapshotAsync();
            Balance balance = new Balance();
            Query query = _dbContex.Collection("Saldo");
            QuerySnapshot snap = await query.GetSnapshotAsync();

            foreach (var item in snap)
            {
                if (item.Exists)
                {
                    balance = item.ConvertTo<Balance>();
                    return balance;
                }
                else
                {
                    balance = new Balance { BalanceValue = 0};
                    return balance;
                }
            }

            balance = new Balance { BalanceValue = 0};
            return balance;
        }
    }
}
