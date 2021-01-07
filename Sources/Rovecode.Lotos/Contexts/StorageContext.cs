using System;
using MongoDB.Driver;
using Rovecode.Lotos.Models;
using Rovecode.Lotos.Repositories;

namespace Rovecode.Lotos.Contexts
{
    internal class StorageContext : IStorageContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public StorageContext(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IStorage<T> Get<T>() where T : StorageData
        {
            return new Storage<T>(_mongoDatabase.GetCollection<T>(typeof(T).Name));
        }
    }
}
