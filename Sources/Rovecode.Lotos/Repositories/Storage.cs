using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Rovecode.Lotos.Entities;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Utils;
using Rovecode.Lotos.Repositories;

namespace Rovecode.Lotos.Repositories
{
    public class Storage<T> : IStorage<T> where T : StorageEntity<T>
    {
        private readonly StorageContext<T> _context;

        public Storage(StorageContext<T> storageContext)
        {
            _context = storageContext;
        }

        public async Task<long> Count(Expression<Func<T, bool>> expression)
        {
            long count = await _context.Collection.CountDocumentsAsync(expression);

            return count;
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> expression)
        {
            var options = new CountOptions { Limit = 1 };

            long count = await _context.Collection.CountDocumentsAsync(expression, options);

            return count != 0;
        }

        public async Task<bool> Exists(Guid id)
        {
            var filter = StorageUtils.BuildIdFilter<T>(id);
            var options = new CountOptions { Limit = 1 };

            long count = await _context.Collection.CountDocumentsAsync(filter, options);

            return count != 0;
        }

        public async Task<bool> Exists(params Guid[] ids)
        {
            var filter = StorageUtils.BuildIdsFilter<T>(ids);
            var options = new CountOptions { Limit = 1 };

            long count = await _context.Collection.CountDocumentsAsync(filter, options);

            return count != 0;
        }

        public async Task<T?> Pick(Expression<Func<T, bool>> expression)
        {
            var result = await _context.Collection.Find(expression)
                .FirstOrDefaultAsync();

            if (result is null) return null;

            var repository = new StorageEntityRepository<T>(this, result);
            result.Storage = this;

            return result;
        }

        public async Task<T?> Pick(Guid id)
        {
            var filter = StorageUtils.BuildIdFilter<T>(id);

            var result = await _context.Collection.Find(StorageUtils.BuildIdFilter<T>(id))
                .FirstOrDefaultAsync();

            if (result is null) return null;

            var repository = new StorageEntityRepository<T>(this, result);
            result.Storage = this;

            return result;
        }

        public async Task<IEnumerable<T>> PickMany(Expression<Func<T, bool>> expression)
        {
            var result = await _context.Collection.Find(expression)
                .ToListAsync();

            result.ForEach(e =>
            {
                var repository = new StorageEntityRepository<T>(this, e);
                e.Storage = this;
            });

            return result;
        }

        public async Task<IEnumerable<T>> PickMany(params Guid[] ids)
        {
            var result = await _context.Collection.Find(StorageUtils.BuildIdsFilter<T>(ids))
                .ToListAsync();

            result.ForEach(e =>
            {
                var repository = new StorageEntityRepository<T>(this, e);
                e.Storage = this;
            });

            return result;
        }

        public Task<IEnumerable<T>> PickMany()
        {
            return PickMany(e => true);
        }

        public async Task Push(T entity)
        {
            await _context.Collection.ReplaceOneAsync(StorageUtils.BuildIdFilter<T>(entity.Id), entity);
        }

        public async Task<T> Put(T entity)
        {
            entity.Id = Guid.NewGuid();

            await _context.Collection.InsertOneAsync(entity);

            var repository = new StorageEntityRepository<T>(this, entity);

            entity.Storage = this;

            return entity;
        }

        public async Task Remove(Expression<Func<T, bool>> expression)
        {
            await _context.Collection.DeleteOneAsync(expression);
        }

        public async Task Remove(Guid id)
        {
            await _context.Collection.DeleteOneAsync(StorageUtils.BuildIdFilter<T>(id));
        }

        public async Task RemoveMany(Expression<Func<T, bool>> expression)
        {
            await _context.Collection.DeleteManyAsync(expression);
        }

        public async Task RemoveMany(params Guid[] ids)
        {
            await _context.Collection.DeleteOneAsync(StorageUtils.BuildIdsFilter<T>(ids));
        }
    }
}
