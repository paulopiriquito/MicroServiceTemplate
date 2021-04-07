using System;
using Adapters.Mediator.Mappings;
using Adapters.Mediator.Models;
using Domain.Entities.Enterprise.Concepts;
using FluentValidation;

namespace Adapters.Mediator.Actions.Example.Commands.Update
{
    public class ChangeExampleUserCommand : Request<Result>, IMapFrom<Domain.Entities.Enterprise.Concepts.Example>
    {
        public Guid Id { get; set; }
        
        public User User { get; set; }
        

        public class ChangeExampleUserCommandValidation : AbstractValidator<ChangeExampleUserCommand>
        {
            public ChangeExampleUserCommandValidation()
            {
                RuleFor(x => x.User.IsValid());
            }
        }
        
        public class ChangeExampleUserCommandCompensation : ChangeExampleUserCommand, ICompensationRequest<Result>
        {
            public ChangeExampleUserCommandCompensation(Guid requestToCompensateId)
            {
                RequestToCompensateId = requestToCompensateId;
            }
            
            public Guid RequestToCompensateId { get; }
        }
    }
}