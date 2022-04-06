using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Domains.Domains.User.Service
{
    public interface IUserService
    {
        void Save(User user);
        void Update(User user); 
        void Delete(Guid id);
        Task<List<User>> GetUsers();
        User GetById(int id);
        User GetByName(string name);
    }
}
