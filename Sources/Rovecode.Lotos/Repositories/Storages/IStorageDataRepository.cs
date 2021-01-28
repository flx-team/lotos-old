using System;
using Rovecode.Lotos.Enums;
using Rovecode.Lotos.Models;

namespace Rovecode.Lotos.Repositories.Storages
{
    /// <summary>
    /// Works with data object who contains in IStorage
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStorageDataRepository<T> where T : StorageData
    {
        /// <summary>
        /// Storage related to this type of data.
        /// </summary>
        public IStorage<T> Storage { get; }

        /// <summary>
        /// Current local data.
        /// </summary>
        public T Data { get; }

        /// <summary>
        /// Delete current data in related to this data storage.
        /// </summary>
        public void Burn();

        /// <summary>
        /// Check curretn data is exists in storage.
        /// </summary>
        /// <returns></returns>
        public bool Exists();
    }
}
