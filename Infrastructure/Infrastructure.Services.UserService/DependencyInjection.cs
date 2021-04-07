using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.UserService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserService(this IServiceCollection services)
        {
            return services;
        }
    }
}