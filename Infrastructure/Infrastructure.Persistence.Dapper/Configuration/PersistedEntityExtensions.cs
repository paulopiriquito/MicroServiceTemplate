using System;

namespace Adapters.Persistence.Configuration
{
    internal static class PersistedEntityExtensions
    {
        internal static TableConfig GetTableName(this Type persistedEntity)
        {
            return new TableConfig();
        }
    }
}