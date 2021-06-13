using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lotos.Contexts;
using Lotos.Entities;

namespace Lotos.Repositories
{
    /// <summary>
    /// Storage is thing that work with mongo db collections uthing for this mongo db driver for c#.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStorage<T> where T : StorageEntity<T>
    {
        public void UseSession(SessionContext sessionContext);

        /// <summary>
        /// Save new entity in mongo db.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<T> Save(T entity);

        /// <summary>
        /// Save entity changes in mongo db.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task Update(T entity);

        /// <summary>
        /// Pick first one entity by filter expression.
        /// If no entities by filter expression returned null.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<T?> Pick(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Pick first one entity by id of entity.
        /// If no entities by id of entity returned null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T?> Pick(Guid id);

        /// <summary>
        /// Pick many entities by filter expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> PickMany(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Pick many entities by guid params/array.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> PickMany(params Guid[] ids);

        /// <summary>
        /// Pick all entities from storage.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<T>> PickMany();

        /// <summary>
        /// Count entities in storage.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<long> Count(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returned true if entity exists in storage by filter expression.
        /// Else returned false.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<bool> Exists(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returned true if entity exists in storage by guid.
        /// Else returned false.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> Exists(Guid id);

        /// <summary>
        /// Returned true if entity exists in storage by guid params/array.
        /// Else returned false.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<bool> Exists(params Guid[] ids);

        /// <summary>
        /// Removed first entity from storage that finded by filter expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task Remove(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Removed first entity from storage that finded by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task Remove(Guid id);

        /// <summary>
        /// Removed entities from storage that finded by filter expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task RemoveMany(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Removed entities from storage that finded by ids params/array.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task RemoveMany(params Guid[] ids);
    }
}
