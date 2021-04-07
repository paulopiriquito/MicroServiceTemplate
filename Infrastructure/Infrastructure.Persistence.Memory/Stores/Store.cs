using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Application.Abstractions.Repositories;
using Domain.Entities.Abstractions;

namespace Infrastructure.Persistence.Memory.Stores
{
    internal abstract class Store<T> : IRepository<T> where T : class, IPersistedEntity
    {
        private static readonly Dictionary<Guid, T> OrganisationsCommitted = new Dictionary<Guid, T>();
        private readonly Dictionary<Guid, StateTracker<T>> _organisations = new Dictionary<Guid, StateTracker<T>>();
        
        public virtual IEnumerable<T> GetAll()
        {
            return OrganisationsCommitted.Values;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(GetAll);
        }

        public virtual IEnumerable<T> Where(Predicate<T> expression)
        {
            return OrganisationsCommitted.Values.Where(expression.Invoke);
        }

        public virtual T FirstOrDefault(Predicate<T> expression)
        {
            return OrganisationsCommitted.Values.FirstOrDefault(expression.Invoke);
        }

        public async Task<T> FirstOrDefaultAsync(Predicate<T> expression)
        {
            return await Task.Run(() => FirstOrDefault(expression));
        }

        public virtual T FindByGuid(Guid id)
        {
            OrganisationsCommitted.TryGetValue(id, out var value);
            return value;
        }

        public async Task<T> FindByGuidAsync(Guid id)
        {
            return await Task.Run(() => FindByGuid(id));
        }

        public virtual void Add(T input)
        {
            _organisations.Add(input.Id, new StateTracker<T>(input){Added = true});
        }

        public virtual T Update(T updatedData)
        {
            _organisations[updatedData.Id] = new StateTracker<T>(updatedData){Updated = true};
            
            return updatedData;
        }

        public IEnumerable<T> UpdateAll(IEnumerable<T> updatedData)
        {
            return updatedData.Select(Update);
        }

        public virtual void Delete(T input)
        {
            var (key, value) = _organisations.FirstOrDefault(x => x.Value.Value.Id == input.Id);
            value.Deleted = true;
            _organisations[key] = value;
        }

        public virtual void Delete(Guid id)
        {
            var (key, value) = _organisations.FirstOrDefault(x => x.Value.Value.Id == id);
            value.Deleted = true;
            _organisations[key] = value;
        }

        public virtual void Commit()
        {
            lock (OrganisationsCommitted)
            {
                foreach (var (key, tracker) in _organisations)
                {
                    if (tracker.Updated)
                    {
                        OrganisationsCommitted[key] = tracker.Value;
                        break;
                    }
                    if (tracker.Added)
                    {
                        OrganisationsCommitted.Add(key, tracker.Value);
                        break;
                    }
                    if (tracker.Deleted)
                    {
                        OrganisationsCommitted.Remove(key);
                        break;
                    }
                }
                _organisations.Clear();
            }
        }

        public async Task CommitAsync()
        {
            await Task.Run(Commit);
        }

        public virtual void Clear()
        {
            _organisations.Clear();
        }

        public async Task ClearAsync()
        {
            await Task.Run(Clear);
        }

        private class StateTracker<TK>
        {
            public StateTracker(TK value)
            {
                Value = value;
            }
        
            public TK Value { get; }
            public bool Added { get; set; }
            public bool Deleted { get; set; }
            public bool Updated { get; set; }
        }
    }
}