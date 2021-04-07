namespace Domain.Application.Abstractions.Repositories.DataContexts
{
    public interface IEntityContext
    {
        public void Commit();
        public void Clear();
    }
}