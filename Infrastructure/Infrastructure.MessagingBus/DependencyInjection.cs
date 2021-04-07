using GreenPipes;
using Infrastructure.MessagingBus.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MessagingBus
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(configurator =>
                {
                    configurator.AddChangeExampleUserAction();
                    
                    configurator.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.UseHealthCheck(context);

                        cfg.Host(configuration["Messaging.EventBusConnection"]);

                        cfg.AddChangeExampleUserActionEndPoints(context, configuration);
                    }));
                }
            );

            return services;
        }
    }
}