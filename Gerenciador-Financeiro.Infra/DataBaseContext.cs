using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.Firestore;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_Financeiro.Infra
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
        }

        public static FirestoreDb OpenConnectionDb()
        {
            FirestoreDb db = FirestoreDb.Create("gerenciador-financeiro-d0bd6");


            return db;
        }
    }
}
