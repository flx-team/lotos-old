using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Rovecode.Lotos.Containers;
using Rovecode.Lotos.Models;
using Rovecode.Lotos.Repositories.Storages;

namespace Rovecode.Lotos.Repositories.Storages
{
    public class Storage<T> : IStorage<T> where T : StorageData
    {
        public IContainer Container { get; set; }

        public IMongoCollection<T> MongoCollection { get; }

        public Storage(IContainer сontainer, IMongoCollection<T> mongoCollection)
        {
            MongoCollection = mongoCollection;
            Container = сontainer;
        }

        public IStorageDataRepository<T> Keep(T model)
        {
            MongoCollection.InsertOne(Container.ClientSession, model);
            return new StorageDataRepository<T>(this, model);
        }

        public IStorageDataRepository<T>? Search(Expression<Func<T, bool>> expression, int offset = 0)
        {
            var findResult = SearchMany(expression, offset, 1);

            return findResult.FirstOrDefault();
        }

        public IEnumerable<IStorageDataRepository<T>> SearchMany(Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue)
        {
            var findResult = MongoCollection.Find(Container.ClientSession, BuildWhereFilter(expression))
                .Skip(offset).Limit(count);

            var repositories = findResult.ToEnumerable()
                .Select(e => new StorageDataRepository<T>(this, e));

            return repositories;
        }

        public async Task<IStorageDataRepository<T>?> SearchAsync(Expression<Func<T, bool>> expression, int offset = 0)
        {
            var findResult = await SearchManyAsync(expression, offset, 1);

            return findResult.FirstOrDefault();
        }

        public Task<IEnumerable<IStorageDataRepository<T>>> SearchManyAsync(Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue)
        {
            return Task.Run(() => SearchMany(expression, offset, count));
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return (int)MongoCollection
                .CountDocuments(Container.ClientSession, BuildWhereFilter(expression));
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return Count(expression) > 0;
        }

        public void Burn(Expression<Func<T, bool>> expression)
        {
            MongoCollection.DeleteOne(Container.ClientSession, BuildWhereFilter(expression));
        }

        public void BurnMany(Expression<Func<T, bool>> expression)
        {
            MongoCollection.DeleteMany(Container.ClientSession, BuildWhereFilter(expression));
        }

        public FilterDefinition<T> BuildWhereFilter(Expression<Func<T, bool>> expression)
        {
            return Builders<T>.Filter.Where(expression);
        }
    }
}
