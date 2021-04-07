using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Dapper.Contrib.Extensions;
using Adapters.Persistence.Configuration;
using Domain.Application.Abstractions.Repositories;
using Domain.Entities.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Adapters.Persistence.Repositories
{
    public class DapperRepository<T> : IRepository<T> where T : class, IPersistedEntity
    {
        protected readonly IConfiguration Configuration;

        private readonly TableConfig _tableName;

        public DapperRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            _tableName = typeof(T).GetTableName();
        }

        internal IDbConnection Connection => new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Where(Predicate<T> expression)
        {
            throw new NotImplementedException();
        }

        public T FirstOrDefault(Predicate<T> expression)
        {
            throw new NotImplementedException();
        }

        public Task<T> FirstOrDefaultAsync(Predicate<T> expression)
        {
            throw new NotImplementedException();
        }

        public T FindByGuid(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByGuidAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(T input)
        {
            throw new NotImplementedException();
        }

        public T Update(T updatedData)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> UpdateAll(IEnumerable<T> updatedData)
        {
            throw new NotImplementedException();
        }

        public void Delete(T input)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<object> AddAsync(T entity)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var result = await conn.InsertAsync(entity);
                conn.Close();
                return result;
            }
        }

        public async Task<bool> AddListAsync(IEnumerable<T> entities)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    foreach (var entity in entities) await conn.InsertAsync(entity);

                    scope.Complete();
                    conn.Close();

                    return true;
                }
            }
        }

        //TODO A intenção não está clara, obfuscação do comportamento
        public async Task<bool> DeleteAsync(object id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var tableName = _tableName;
                var query = $"UPDATE [{tableName.Schema}].[{tableName.Name}] SET [IsActive] = 0 WHERE [ID] = @Id";

                var result = await conn.ExecuteAsync(query,
                    new
                    {
                        Id = id
                    }) > 0;

                conn.Close();

                return result;
            }
        }
    }
}