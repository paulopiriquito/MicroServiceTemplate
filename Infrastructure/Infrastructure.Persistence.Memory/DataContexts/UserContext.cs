#nullable enable
using Domain.Application.Abstractions.Repositories;
using Domain.Application.Abstractions.Repositories.DataContexts;
using Domain.Entities.Enterprise.Concepts;
using Infrastructure.Persistence.Memory.Stores;

namespace Infrastructure.Persistence.Memory.DataContexts
{
    public class UserContext : IUserContext
    {
        public IRepository<User> Users { get; } = new UserStore();
        
        public User? FindByEmail(string email)
        {
            return Users.FirstOrDefault(x => x.Email == email);
        }

        public void Commit()
        {
            Users.Commit();
        }

        public void Clear()
        {
            Users.Clear();
        }
    }
}