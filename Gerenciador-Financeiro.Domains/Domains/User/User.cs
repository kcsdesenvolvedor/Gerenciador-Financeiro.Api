using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.Firestore;

namespace Gerenciador_Financeiro.Domains.Domains.User
{
    [FirestoreData]
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string Password { get; set; }

    }
}
