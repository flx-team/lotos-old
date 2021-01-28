using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Rovecode.Lotos.Containers;
using Rovecode.Lotos.Models;

namespace Rovecode.Lotos.Repositories.Storages
{
    public interface IStorage<T> where T : StorageData
    {
        public IContainer Container { get; }

        /// <summary>
        /// Saves the data to the database and returns a repository that works with it.
        /// </summary>
        /// <param name="model">Data model.</param>
        /// <returns>Repository that works with keep data.</returns>
        public IStorageDataRepository<T> Keep(T model);

        /// <summary>
        /// Search data in collection matching expression filter. If data not find, return null. 
        /// </summary>
        /// <param name="expression">Expression filter.</param>
        /// <param name="offset"></param>
        /// <returns>Repository that works with find data or null.</returns>
        public IStorageDataRepository<T>? Search(Expression<Func<T, bool>> expression, int offset = 0);

        /// <summary>
        /// Search datas in collection matching expression filter. If data not find, return IEnumerable with 0 items in it. 
        /// </summary>
        /// <param name="expression">Expression filter.</param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns>IEnumerable with repositories that works with find datas.</returns>
        public IEnumerable<IStorageDataRepository<T>> SearchMany(Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue);

        /// <summary>
        /// Search data in collection matching expression filter. If data not find, return null. 
        /// </summary>
        /// <param name="expression">Expression filter.</param>
        /// <param name="offset"></param>
        /// <returns>Repository that works with find data or null.</returns>
        public Task<IStorageDataRepository<T>?> SearchAsync(Expression<Func<T, bool>> expression, int offset = 0);

        /// <summary>
        /// Search datas in collection matching expression filter. If data not find, return IEnumerable with 0 items in it. 
        /// </summary>
        /// <param name="expression">Expression filter.</param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns>IEnumerable with repositories that works with find datas.</returns>
        public Task<IEnumerable<IStorageDataRepository<T>>> SearchManyAsync(Expression<Func<T, bool>> expression, int offset = 0, int count = int.MaxValue);

        /// <summary>
        /// Count datas in collection matching expression filter.
        /// </summary>
        /// <param name="expression">Expression filter.</param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Check data is exists or not matching expression filter.
        /// </summary>
        /// <param name="expression">Expression filter.</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Delete first find data matching expression filter.
        /// </summary>
        /// <param name="expression">Expression filter.</param>
        public void Burn(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Delete all datas matching expression filter.
        /// </summary>
        /// <param name="expression">Expression filter.</param>
        public void BurnMany(Expression<Func<T, bool>> expression);
    }
}
