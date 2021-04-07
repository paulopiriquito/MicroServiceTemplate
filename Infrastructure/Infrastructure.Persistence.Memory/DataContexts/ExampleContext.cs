using Domain.Application.Abstractions.Repositories;
using Domain.Entities.Enterprise.Concepts;
using Infrastructure.Persistence.Memory.Stores;

namespace Infrastructure.Persistence.Memory.DataContexts
{
    internal class ExampleContext : ExampleContextBase
    {
        public override IRepository<Example> Examples { get; } = new ExampleStore();
        public override IRepository<Example> ExamplesCompensations { get; } = new ExampleStore.ExampleCompensationStore();
    }
}