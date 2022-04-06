using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gerenciador_Financeiro.Domains.Domains.Expense
{
    [FirestoreData]
    public class Expense // gastos
    {
        public Expense()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now.Date.ToString("dd-MM-yyyy");
        }
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string Date { get; set; }
        [FirestoreProperty]
        public double Price { get; set; }
        [FirestoreProperty]
        public string[] Tag { get; set; }
        [FirestoreProperty]
        public string Image { get; set; }
        [FirestoreProperty]
        public string Annotation { get; set; }

    }
}
