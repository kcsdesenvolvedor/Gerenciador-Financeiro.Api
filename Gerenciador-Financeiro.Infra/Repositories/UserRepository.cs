using FireSharp;
using FireSharp.Response;
using Gerenciador_Financeiro.Domains.Domains.User;
using Gerenciador_Financeiro.Domains.Domains.User.Repository;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private FirestoreDb _dbContext = DataBaseContext.OpenConnectionDb();
        public async void Delete(Guid id)
        {
            DocumentReference docRef = _dbContext.Collection("Users").Document(id.ToString());
            await docRef.DeleteAsync();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> listUser = new List<User>();

            Query query = _dbContext.Collection("Users");
            QuerySnapshot snap = await query.GetSnapshotAsync();

            foreach (DocumentSnapshot item in snap)
            {
                User user = item.ConvertTo<User>();
                listUser.Add(user);
                
            }
            return listUser;
        }

        public async void Save(User user)
        {
            DocumentReference doc = _dbContext.Collection("Users").Document(user.Id);

            Dictionary<string, object> dic = new Dictionary<string, object>()
            {
                { "Id", user.Id},
                { "Name", user.Name},
                { "Password", user.Password}
            };
            
            await doc.SetAsync(dic);
        }

        public async void Update(User user)
        {
            DocumentReference docRef = _dbContext.Collection("Users").Document(user.Id);
            Dictionary<string, object> dic = new Dictionary<string, object>()
            {
                { "Name", user.Name},
                { "Password", user.Password}
            };

            DocumentSnapshot snap = await docRef.GetSnapshotAsync();
            if(snap.Exists)
            {
                await docRef.UpdateAsync(dic);
            }else
            {
                throw new Exception();
            }
        }
    }
}
