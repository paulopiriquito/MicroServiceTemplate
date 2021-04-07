using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Abstractions;

namespace Domain.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : class, IPersistedEntity
    {
        public IEnumerable<T> GetAll();
        public Task<IEnumerable<T>> GetAllAsync(); 
        public IEnumerable<T> Where(Predicate<T> expression);
        public T FirstOrDefault(Predicate<T> expression);
        public Task<T> FirstOrDefaultAsync(Predicate<T> expression);
        public T FindByGuid(Guid id);
        public Task<T> FindByGuidAsync(Guid id);
        public void Add(T input);
        public T Update(T updatedData);
        public IEnumerable<T> UpdateAll(IEnumerable<T> updatedData);
        public void Delete(T input);
        public void Delete(Guid id);
        public void Commit();
        public Task CommitAsync();
        public void Clear();
        public Task ClearAsync();
    }
}