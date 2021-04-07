using Domain.Application.Abstractions.Repositories;
using Domain.Application.Abstractions.Repositories.DataContexts;
using Domain.Entities.Enterprise.Concepts;
using Infrastructure.Persistence.Memory.DataContexts;
using Infrastructure.Persistence.Memory.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Memory
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMemoryPersistence(this IServiceCollection services)
        {
            services.AddScoped<IExampleContext, ExampleContext>();
            
            services.AddScoped<IRepository<User>, Store<User>>();

            return services;
        }
    }
}