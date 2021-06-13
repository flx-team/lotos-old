using System;
using System.Threading.Tasks;
using Lotos.Entities;

namespace Lotos.Repositories
{
    public interface IStorageEntityRepository<T> where T : StorageEntity<T>
    {
        /// <summary>
        /// Storage of current entity.
        /// </summary>
        public IStorage<T> Storage { get; }

        /// <summary>
        /// Remove current entity from mongo db.
        /// </summary>
        /// <returns></returns>
        public Task Remove();

        /// <summary>
        /// Return true if entity exists in db.
        /// </summary>
        /// <returns></returns>
        public Task<bool> Exists();

        /// <summary>
        /// Save changes of this entity in mongo db.
        /// </summary>
        /// <returns></returns>
        public Task Update();

        /// <summary>
        /// Get new entity by current entity id.
        /// </summary>
        /// <returns></returns>
        public Task<T> CopyActual();
    }
}
