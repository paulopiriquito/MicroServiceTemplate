using System;
using System.Threading;
using System.Threading.Tasks;
using Adapters.Mediator.Models;
using Domain.Application.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Adapters.Mediator.ApplicationBehaviours
{
    public class RequestLogger<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : Request<TResponse>
    {
        private readonly ILogger<RequestLogger<TRequest, TResponse>> _logger;
        private readonly IUserService _currentUserService;

        public RequestLogger(ILogger<RequestLogger<TRequest, TResponse>> logger, IUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            var currentUserId = _currentUserService.GetCurrentUser().Id;

            _logger.LogInformation(
                "Handle: RequestType:{@Request} RequestId:{@RequestId} Time:{@Time} UserId:{@UserId}", 
                 requestName, request.RequestId, DateTime.Now, currentUserId
                );

            var response = await next();
            
            _logger.LogInformation(
                "Handled: RequestType:{@Request} RequestId:{@RequestId} Time:{@Time} UserId:{@UserId}", 
                requestName, request.RequestId, DateTime.Now, currentUserId
            );

            return response;
        }
    }
}
