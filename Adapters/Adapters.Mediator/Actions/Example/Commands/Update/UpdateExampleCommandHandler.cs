using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Adapters.Mediator.Models;
using AutoMapper;
using Domain.Application.Abstractions.Repositories.DataContexts;
using MediatR;

namespace Adapters.Mediator.Actions.Example.Commands.Update
{
    public class UpdateExampleCommandHandler : 
        IRequestHandler<ChangeExampleUserCommand, Result>, 
        IRequestHandler<ChangeExampleUserCommand.ChangeExampleUserCommandCompensation, Result>
    {
        private readonly IExampleContext _exampleContext;
        private readonly IMapper _mapper;
        
        public UpdateExampleCommandHandler(IExampleContext exampleContext, IMapper mapper)
        {
            _exampleContext = exampleContext;
            _mapper = mapper;
        }
        
        public async Task<Result> Handle(ChangeExampleUserCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();
            var handleSuccess = true;
            
            try
            {
                var currentExample = await _exampleContext.Examples.FindByGuidAsync(request.Id);
                currentExample.Id = request.Id;
                _exampleContext.ExamplesCompensations.Update(currentExample);

                currentExample = _mapper.Map<Domain.Entities.Enterprise.Concepts.Example>(request);
                _exampleContext.Examples.Update(currentExample);

                await _exampleContext.ExamplesCompensations.CommitAsync();
                await _exampleContext.Examples.CommitAsync();
            }
            catch (Exception e)
            {
                handleSuccess = false;
                errors.Add(e.ToString());
            }

            return new Result(handleSuccess, errors);
        }

        public async Task<Result> Handle(ChangeExampleUserCommand.ChangeExampleUserCommandCompensation request, CancellationToken cancellationToken)
        {
            var errors = new List<string>();
            var handleSuccess = true;
            
            try
            {
                var compensationData = await _exampleContext.ExamplesCompensations.FindByGuidAsync(request.RequestToCompensateId);

                compensationData.Id = request.Id;

                _exampleContext.Examples.Update(compensationData);

                await _exampleContext.Examples.CommitAsync();
            }
            catch (Exception e)
            {
                handleSuccess = false;
                errors.Add(e.ToString());
            }

            return new Result(handleSuccess, errors);
        }
    }
}