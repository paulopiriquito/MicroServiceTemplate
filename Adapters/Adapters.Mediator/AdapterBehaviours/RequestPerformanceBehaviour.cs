using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Domain.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Adapters.Mediator.ApplicationBehaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<RequestPerformanceBehaviour<TRequest, TResponse>> _logger;
        private readonly IUserService _currentUserService;
        private readonly IConfiguration _configuration;

        public RequestPerformanceBehaviour(ILogger<RequestPerformanceBehaviour<TRequest, TResponse>> logger, IUserService currentUserService, IConfiguration configuration)
        {
            _timer = new Stopwatch();

            _logger = logger;
            _currentUserService = currentUserService;
            _configuration = configuration;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > _configuration.GetValue<int>("LogRequestSlaMilliseconds"))
            {
                var name = typeof(TRequest).Name;

                _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) User:{@UserId} Request:{@Request}", 
                    name, _timer.ElapsedMilliseconds, _currentUserService.GetCurrentUser().Id, request);
            }

            return response;
        }
    }
}
