#nullable enable
using Domain.Entities.Enterprise.Concepts;

namespace Domain.Application.Abstractions.Repositories.DataContexts
{
    public interface IUserContext : IEntityContext
    {
        public IRepository<User> Users { get; }

        public User? FindByEmail(string email);
    }
}