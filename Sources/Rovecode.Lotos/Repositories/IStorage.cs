using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Rovecode.Lotos.Entities;

namespace Rovecode.Lotos.Repositories
{
    public interface IStorage<T> where T : StorageEntity<T>
    {
        public Task Put(T entity);

        public Task Push(T entity);

        public Task<T?> Pick(Expression<Func<T, bool>> expression);
        public Task<T?> Pick(Guid id);

        public Task<IEnumerable<T>> PickMany(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> PickMany(params Guid[] ids);
        public Task<IEnumerable<T>> PickMany();

        public Task<long> Count(Expression<Func<T, bool>> expression);

        public Task<bool> Exists(Expression<Func<T, bool>> expression);
        public Task<bool> Exists(Guid id);
        public Task<bool> Exists(params Guid[] ids);

        public Task Remove(Expression<Func<T, bool>> expression);
        public Task Remove(Guid id);

        public Task RemoveMany(Expression<Func<T, bool>> expression);
        public Task RemoveMany(params Guid[] ids);
    }
}
