using System.Collections.Generic;
using Domain.Application.Abstractions.Repositories;
using Domain.Application.Abstractions.Repositories.DataContexts;
using Domain.Entities.Enterprise.Concepts;

namespace Infrastructure.Persistence.Memory.DataContexts
{
    internal abstract class ExampleContextBase : IExampleContext
    {
        public abstract IRepository<Example> Examples { get; }
        public abstract IRepository<Example> ExamplesCompensations { get; }

        public IEnumerable<Example> UserExamples(User user)
        {
            var userExamples = Examples
                .Where(
                    example => 
                        example.User.Id == user.Id
                );
            return userExamples;
        }

        public void Commit()
        {
            Examples.Commit();
        }

        public void Clear()
        {
            Examples.Clear();
        }
    }
}