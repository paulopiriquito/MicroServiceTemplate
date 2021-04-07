using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Logging
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLoggingService(this IServiceCollection services)
        {
            return services;
        }
    }
}