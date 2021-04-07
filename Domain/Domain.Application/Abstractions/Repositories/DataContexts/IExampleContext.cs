using System.Collections.Generic;
using Domain.Entities.Enterprise.Concepts;

namespace Domain.Application.Abstractions.Repositories.DataContexts
{
    public interface IExampleContext : IEntityContext
    {
        public IRepository<Example> Examples { get; }
        
        public IRepository<Example> ExamplesCompensations { get; }

        public IEnumerable<Example> UserExamples(User user);
    }
}