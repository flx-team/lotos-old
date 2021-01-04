using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Rovecode.Lotos.Models;

namespace Rovecode.Lotos.Repositories
{
    public class Storage<T> : IStorage<T> where T : StorageData
    {
        private readonly IMongoCollection<T> _mongoCollection;

        public Storage(IMongoCollection<T> mongoCollection)
        {
            _mongoCollection = mongoCollection;
        }

        public void Burn(Expression<Func<T, bool>> expression)
        {
            _mongoCollection.DeleteOne(BuildWhereFilter(expression));
        }

        public void BurnMany(Expression<Func<T, bool>> expression)
        {
            _mongoCollection.DeleteMany(BuildWhereFilter(expression));
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return (int)_mongoCollection
                .CountDocuments(BuildWhereFilter(expression));
        }

        public bool Exist(Expression<Func<T, bool>> expression)
        {
            return Count(expression) > 0;
        }

        public IStorageDataRepository<T> Keep(T model)
        {
            _mongoCollection.InsertOne(model);
            return new StorageDataRepository<T>(this, model);
        }

        public IStorageDataRepository<T>?
            Search(Expression<Func<T, bool>> expression, int offset = 0)
        {
            var findResult = SearchMany(expression, offset, 1);

            return findResult.FirstOrDefault();
        }

        public async Task<IStorageDataRepository<T>?> SearchAsync(
            Expression<Func<T, bool>> expression, int offset = 0)
        {
            var findResult = await SearchManyAsync(expression, offset, 1);

            return findResult.FirstOrDefault();
        }

        public IEnumerable<IStorageDataRepository<T>> SearchMany(
            Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue)
        {
            var findResult = _mongoCollection.Find(BuildWhereFilter(expression))
                .Skip(offset).Limit(count);

            var repositories = findResult.ToEnumerable()
                .Select(e => new StorageDataRepository<T>(this, e));

            return repositories;
        }

        public Task<IEnumerable<IStorageDataRepository<T>>> SearchManyAsync(
            Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue)
        {
            return Task.Run(() => SearchMany(expression, offset, count));
        }

        public FilterDefinition<T> BuildWhereFilter(Expression<Func<T, bool>> expression)
        {
            return Builders<T>.Filter.Where(expression);
        }

        public IMongoCollection<T> GetMongoCollection()
        {
            return _mongoCollection;
        }
    }
}
