using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Rovecode.Lotos.Entities;
using Rovecode.Lotos.Containers;

namespace Rovecode.Lotos.Repositories
{
    public class Storage<T> : IStorage<T> where T : StorageEntity
    {
        public IContainer Container { get; internal set; }

        private readonly IMongoCollection<T> _mongoCollection;

        public Storage(IContainer container, IMongoCollection<T> mongoCollection)
        {
            Container = container;
            _mongoCollection = mongoCollection;
        }

        public Container GetContainer()
        {
            return (Container as Container)!;
        }

        public IMongoCollection<T> GetMongoCollection()
        {
            return _mongoCollection;
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            var session = GetContainer().ClientSessionHandle;

            var filter = BuildWhereFilter(expression);

            return (int)_mongoCollection.CountDocuments(session, filter);
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return Count(expression) > 0;
        }

        public bool Exists(ObjectId id)
        {
            return Count(e => e.Id == id) > 0;
        }

        public bool Exists(IEnumerable<ObjectId> ids)
        {
            return Count(e => ids.Contains(e.Id)) > 0;
        }

        public bool Exists(params ObjectId[] ids)
        {
            return Count(e => ids.Contains(e.Id)) > 0;
        }

        public IStorageEntityProvider<T>? Pick(Expression<Func<T, bool>> expression, int offset = 0)
        {
            var result = PickMany(expression, offset, 1);

            return result.FirstOrDefault();
        }

        public IStorageEntityProvider<T>? Pick(ObjectId id)
        {
            return Pick(e => e.Id == id);
        }

        public Task<IStorageEntityProvider<T>?> PickAsync(Expression<Func<T, bool>> expression, int offset = 0)
        {
            return Task.Run(() => Pick(expression, offset));
        }

        public Task<IStorageEntityProvider<T>?> PickAsync(ObjectId id)
        {
            return Task.Run(() => Pick(id));
        }

        public IEnumerable<IStorageEntityProvider<T>> PickMany(Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue)
        {
            var session = GetContainer().ClientSessionHandle;

            var filter = BuildWhereFilter(expression);

            var result = _mongoCollection.Find(session, filter)
                .Skip(offset).Limit(count);

            var prividers = result.ToEnumerable()
                .Select(e => new StorageEntityProvider<T>(this, e));

            return prividers;
        }

        public IEnumerable<IStorageEntityProvider<T>> PickMany(IEnumerable<ObjectId> ids)
        {
            return PickMany(e => ids.Contains(e.Id));
        }

        public IEnumerable<IStorageEntityProvider<T>> PickMany(params ObjectId[] ids)
        {
            return PickMany(e => ids.Contains(e.Id));
        }

        public Task<IEnumerable<IStorageEntityProvider<T>>> PickManyAsync(Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue)
        {
            return Task.Run(() => PickMany(expression, offset, count));
        }

        public Task<IEnumerable<IStorageEntityProvider<T>>> PickManyAsync(IEnumerable<ObjectId> ids)
        {
            return Task.Run(() => PickMany(ids));
        }

        public Task<IEnumerable<IStorageEntityProvider<T>>> PickManyAsync(params ObjectId[] ids)
        {
            return Task.Run(() => PickMany(ids));
        }

        public IStorageEntityProvider<T> Put(T entity)
        {
            _mongoCollection.InsertOne(GetContainer().ClientSessionHandle, entity);

            return new StorageEntityProvider<T>(this, entity);
        }

        public void Remove(Expression<Func<T, bool>> expression)
        {
            var session = GetContainer().ClientSessionHandle;

            var filter = BuildWhereFilter(expression);

            _mongoCollection.DeleteOne(session, filter);
        }

        public void Remove(ObjectId id)
        {
            Remove(e => e.Id == id);
        }

        public void RemoveMany(Expression<Func<T, bool>> expression)
        {
            var session = GetContainer().ClientSessionHandle;

            var filter = BuildWhereFilter(expression);

            _mongoCollection.DeleteMany(session, filter);
        }

        public void RemoveMany(IEnumerable<ObjectId> ids)
        {
            RemoveMany(e => ids.Contains(e.Id));
        }

        public void RemoveMany(params ObjectId[] ids)
        {
            RemoveMany(e => ids.Contains(e.Id));
        }

        public FilterDefinition<T> BuildWhereFilter(Expression<Func<T, bool>> expression)
        {
            return Builders<T>.Filter.Where(expression);
        }
    }
}
