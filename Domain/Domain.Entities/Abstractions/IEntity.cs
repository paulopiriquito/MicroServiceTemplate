using System;

namespace Domain.Entities.Abstractions
{
    public interface IPersistedEntity
    {
        public Guid Id { get; set; }
    }
}