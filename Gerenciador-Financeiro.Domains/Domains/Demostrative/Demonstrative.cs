using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gerenciador_Financeiro.Domains.Domains.Demostrative
{
    [FirestoreData]
    public class Demonstrative
    {
        public Demonstrative()
        {
        }
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty(Name ="Operação")]
        public dynamic[] Operation { get; set; }
    }
}
