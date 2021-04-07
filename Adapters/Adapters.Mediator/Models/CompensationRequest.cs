using System;

namespace Adapters.Mediator.Models
{
    public interface ICompensationRequest<TResponse>
    {
        public Guid RequestToCompensateId { get; }
    }
}