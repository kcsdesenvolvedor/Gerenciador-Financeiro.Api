using Gerenciador_Financeiro.Domains.Domains.User;
using Gerenciador_Financeiro.Domains.Domains.User.Repository;
using Gerenciador_Financeiro.Domains.Domains.User.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Business.Services.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }

        public User GetByName(string name)
        {
            return _repository.GetByName(name);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _repository.GetUsers();
        }

        public void Save(User user)
        {
            _repository.Save(user);
        }

        public void Update(User user)
        {
            _repository.Update(user);
        }
    }
}
