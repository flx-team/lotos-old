using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using Rovecode.Lotos.Containers;
using Rovecode.Lotos.Entities;

namespace Rovecode.Lotos.Repositories
{
    public interface IStorage<T> where T : StorageEntity
    {
        public IContainer Container { get; }

        public IStorageEntityProvider<T> Put(T entity);

        public IStorageEntityProvider<T>? Pick(Expression<Func<T, bool>> expression, int offset = 0);
        public IStorageEntityProvider<T>? Pick(ObjectId id);

        public Task<IStorageEntityProvider<T>?> PickAsync(Expression<Func<T, bool>> expression, int offset = 0);
        public Task<IStorageEntityProvider<T>?> PickAsync(ObjectId id);

        public IEnumerable<IStorageEntityProvider<T>> PickMany(Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue);
        public IEnumerable<IStorageEntityProvider<T>> PickMany(IEnumerable<ObjectId> ids);
        public IEnumerable<IStorageEntityProvider<T>> PickMany(params ObjectId[] ids);

        public Task<IEnumerable<IStorageEntityProvider<T>>> PickManyAsync(Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue);
        public Task<IEnumerable<IStorageEntityProvider<T>>> PickManyAsync(IEnumerable<ObjectId> ids);
        public Task<IEnumerable<IStorageEntityProvider<T>>> PickManyAsync(params ObjectId[] ids);

        public int Count(Expression<Func<T, bool>> expression);

        public bool Exists(Expression<Func<T, bool>> expression);
        public bool Exists(ObjectId id);
        public bool Exists(IEnumerable<ObjectId> ids);
        public bool Exists(params ObjectId[] ids);

        public void Remove(Expression<Func<T, bool>> expression);
        public void Remove(ObjectId id);

        public void RemoveMany(Expression<Func<T, bool>> expression);
        public void RemoveMany(IEnumerable<ObjectId> ids);
        public void RemoveMany(params ObjectId[] ids);
    }
}
