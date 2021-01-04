using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Rovecode.Lotos.Models;

namespace Rovecode.Lotos.Repositories
{
    public interface IStorage<T> where T : StorageData
    {
        public IStorageDataRepository<T> Keep(T model);

        public IStorageDataRepository<T>? Search(Expression<Func<T, bool>> expression, int offset = 0);
        public IEnumerable<IStorageDataRepository<T>> SearchMany(Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue);

        public Task<IStorageDataRepository<T>?> SearchAsync(Expression<Func<T, bool>> expression, int offset = 0);
        public Task<IEnumerable<IStorageDataRepository<T>>> SearchManyAsync(Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue);

        public int Count(Expression<Func<T, bool>> expression);
        public bool Exist(Expression<Func<T, bool>> expression);

        public void Burn(Expression<Func<T, bool>> expression);
        public void BurnMany(Expression<Func<T, bool>> expression);
    }
}
