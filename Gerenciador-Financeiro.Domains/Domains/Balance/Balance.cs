using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gerenciador_Financeiro.Domains.Domains.Balance
{
    [FirestoreData]
    public class Balance
    {
        private string secretId = "110ca1fd-2364-436a-83bc-721dbc455408";
        public Balance()
        {
            Id = secretId;
        }
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty(Name ="Valor Atual")]
        public double BalanceValue { get; set; }
    }
}
