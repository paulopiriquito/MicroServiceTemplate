using System;
using MediatR;

namespace Adapters.Mediator.Models
{
    public abstract class Request<TResult> : IRequest<TResult>
    {
        public Guid RequestId { get; set; } = Guid.NewGuid();
    }
    
    public abstract class Request : IRequest
    {
        public Guid RequestId { get; set; } = Guid.NewGuid();
    }
}