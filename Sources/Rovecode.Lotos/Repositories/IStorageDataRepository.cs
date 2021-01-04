﻿using System;
using Rovecode.Lotos.Enums;
using Rovecode.Lotos.Models;

namespace Rovecode.Lotos.Repositories
{
    /// <summary>
    /// Works with data object who contains in IStorage
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStorageDataRepository<T> : IDisposable where T : StorageData
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
        /// Load current data to db and after load data from db to this object.
        /// </summary>
        public void Exchange();

        /// <summary>
        /// Exchange by ExchangeMode.
        /// </summary>
        /// <param name="mode"></param>
        public void Exchange(ExchangeMode mode);

        /// <summary>
        /// Delete current data in related to this data storage.
        /// </summary>
        public void Burn();

        /// <summary>
        /// Check curretn data is exists in storage.
        /// </summary>
        /// <returns></returns>
        public bool Exist();
    }
}