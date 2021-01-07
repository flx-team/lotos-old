using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Rovecode.Lotos.Repositories.Mongo
{
    public interface IMongoRepository<T>
    {
        public IMongoCollection<T> Collection { get; }

        public IClientSessionHandle ClientSession { get; }

        public T? Search(FilterDefinition<T> filter, int offset = 0);
        public IEnumerable<T> SearchMany(FilterDefinition<T> filter, int offset = 0, int count = int.MaxValue);

        public Task<T?> SearchAsync(FilterDefinition<T> filter, int offset = 0);
        public Task<IEnumerable<T>> SearchManyAsync(FilterDefinition<T> filter, int offset = 0, int count = int.MaxValue);

        public int Count(FilterDefinition<T> filter);

        public bool Exists(FilterDefinition<T> filter);

        public void DeleteOne(FilterDefinition<T> filter);
        public void DeleteMany(FilterDefinition<T> filter);

        public void InsertOne(T model);
    }
}
