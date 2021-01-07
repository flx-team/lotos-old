using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Rovecode.Lotos.Repositories.Mongo
{
    public class MongoRepository<T> : IMongoRepository<T>
    {
        public IMongoCollection<T> Collection { get; }

        public IClientSessionHandle ClientSession { get; }

        public MongoRepository(IMongoCollection<T> collection, IClientSessionHandle clientSession)
        {
            Collection = collection;
            ClientSession = clientSession;
        }

        public int Count(FilterDefinition<T> filter)
        {
            return (int)Collection
                .CountDocuments(ClientSession, filter);
        }

        public void DeleteMany(FilterDefinition<T> filter)
        {
            Collection.DeleteMany(ClientSession, filter);
        }

        public void DeleteOne(FilterDefinition<T> filter)
        {
            Collection.DeleteOne(ClientSession, filter);
        }

        public bool Exists(FilterDefinition<T> filter)
        {
            return Count(filter) > 0;
        }

        public T? Search(FilterDefinition<T> filter, int offset = 0)
        {
            var findResult = SearchMany(filter, offset, 1);

            return findResult.FirstOrDefault();
        }

        public async Task<T?> SearchAsync(FilterDefinition<T> filter, int offset = 0)
        {
            var findResult = await SearchManyAsync(filter, offset, 1);

            return findResult.FirstOrDefault();
        }

        public IEnumerable<T> SearchMany(FilterDefinition<T> filter, int offset = 0, int count = int.MaxValue)
        {
            var findResult = Collection.Find(ClientSession, filter)
                .Skip(offset).Limit(count);

            return findResult.ToEnumerable();
        }

        public Task<IEnumerable<T>> SearchManyAsync(FilterDefinition<T> filter, int offset = 0, int count = int.MaxValue)
        {
            return Task.Run(() => SearchMany(filter, offset, count));
        }

        public void InsertOne(T model)
        {
            Collection.InsertOne(ClientSession, model);
        }
    }
}
