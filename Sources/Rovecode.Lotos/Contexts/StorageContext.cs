using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Rovecode.Lotos.Entities;

namespace Rovecode.Lotos.Contexts
{
    public class StorageContext<T> where T : StorageEntity<T>
    {
        internal IMongoCollection<T> Collection { get; }

        public StorageContext(IMongoDatabase mongoDatabase)
        {
            Collection = mongoDatabase.GetCollection<T>(typeof(T).Name);
        }
    }
}
