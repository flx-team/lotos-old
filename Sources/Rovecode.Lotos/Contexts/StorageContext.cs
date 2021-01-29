using System;
using MongoDB.Driver;
using Rovecode.Lotos;
using Rovecode.Lotos.Containers;
using Rovecode.Lotos.Repositories;

namespace Rovecode.Lotos.Contexts
{
    internal class StorageContext : IStorageContext
    {
        public IMongoDatabase MongoDatabase { get; }

        public StorageContext(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
        }

        public IContainer CreateContainer()
        {
            return new Container(this);
        }
    }
}
