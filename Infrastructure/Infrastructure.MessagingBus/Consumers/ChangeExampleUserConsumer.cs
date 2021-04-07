using System.Threading.Tasks;
using Adapters.Mediator.Actions.Example.Commands.Update;
using GreenPipes;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MessagingBus.Consumers
{
    internal static class ChangeExampleUserConsumer
    {
        private class Consumer : IConsumer<ChangeExampleUserCommand>
        {
            private readonly IMediator _mediator;
        
            public Consumer(IMediator mediator)
            {
                _mediator = mediator;
            }
        
            public async Task Consume(ConsumeContext<ChangeExampleUserCommand> context)
            {
                await _mediator.Send(context.Message, context.CancellationToken);
            }
        }

        private class Compensator : IConsumer<ChangeExampleUserCommand.ChangeExampleUserCommandCompensation>
        {
            private readonly IMediator _mediator;
        
            public Compensator(IMediator mediator)
            {
                _mediator = mediator;
            }
        
            public async Task Consume(ConsumeContext<ChangeExampleUserCommand.ChangeExampleUserCommandCompensation> context)
            {
                await _mediator.Send(context.Message, context.CancellationToken);
            }
        }

        internal static void AddChangeExampleUserAction(this IServiceCollectionBusConfigurator configurator)
        {
            configurator.AddConsumer<Consumer>();
            configurator.AddConsumer<Compensator>();
        }
        
        internal static void AddChangeExampleUserActionEndPoints(
            this IRabbitMqBusFactoryConfigurator configurator,
            IRegistration context,
            IConfiguration configuration)
        {
            configurator.ReceiveEndpoint(configuration["Messaging.Actions.ChangeExampleUserAction_Consumer_TopicName"],
                ec =>
            {
                ec.PrefetchCount = 16;
                ec.UseMessageRetry(r => r.Interval(2, 100));
                ec.ConfigureConsumer<Consumer>(context);
            });

            configurator.ReceiveEndpoint(configuration["Messaging.Actions.ChangeExampleUserAction_Compensator_TopicName"], 
                ec =>
            {
                ec.PrefetchCount = 16;
                ec.UseMessageRetry(r => r.Interval(2, 100));
                ec.ConfigureConsumer<Compensator>(context);
            });
        }
    }
}