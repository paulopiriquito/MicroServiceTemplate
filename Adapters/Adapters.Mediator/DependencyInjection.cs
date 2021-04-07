using System.Reflection;
using Adapters.Mediator.ApplicationBehaviours;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Mediator
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            AddApplicationBehaviours(services, configuration);

            return services;
        }

        private static void AddApplicationBehaviours(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));

            if (configuration.GetValue<bool>("UseInternalLogging"))
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLogger<,>));
            }

            if (configuration.GetValue<bool>("UseActionValidation"))
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            }
        }
    }
}