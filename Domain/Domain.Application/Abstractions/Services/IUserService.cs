using Domain.Entities.Enterprise.Concepts;

namespace Domain.Application.Abstractions.Services
{
    public interface IUserService
    {
        User GetCurrentUser();
    }
}