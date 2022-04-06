using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gerenciador_Financeiro.Domains.Domains.Revenue
{
    [FirestoreData]
    public class Revenue // Receita
    {
        public Revenue()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now.Date.ToString("dd-MM-yyyy");
        }
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty(Name ="Descrição")]
        public string Description { get; set; }

        [FirestoreProperty(Name ="Data")]
        public string Date { get; set; }

        [FirestoreProperty(Name ="Valor")]
        public double RevenueValue { get; set; }

    }
}
