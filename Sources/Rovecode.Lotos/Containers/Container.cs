using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Rovecode.Lotos.Common;
using Rovecode.Lotos.Models;
using Rovecode.Lotos.Repositories.Storages;

namespace Rovecode.Lotos.Containers
{
    public class Container : IContainer
    {
        private readonly IMongoDatabase _mongoDatabase;

        private readonly List<INotifier<object>> _notifiers;

        public IClientSessionHandle ClientSession { get; }

        public Container(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;

            _notifiers = new List<INotifier<object>>();

            ClientSession = _mongoDatabase.Client.StartSession();
        }

        public IStorage<T> GetStorage<T>() where T : StorageData
        {
            return new Storage<T>(this, _mongoDatabase.GetCollection<T>(typeof(T).Name));
        }

        public void Init()
        {
            ClientSession.StartTransaction();
        }

        public void Sync()
        {
            // TODO
        }

        public void Fail()
        {
            ClientSession.AbortTransaction();
        }

        public void Ok()
        {
            ClientSession.CommitTransaction();
        }

        public void Sandbox(Action action)
        {
            try
            {
                Init();
                action.Invoke();
                Sync();
                Ok();
            }
            catch (Exception)
            {
                Fail();
                throw;
            }

            Dispose();
        }

        public void Dispose()
        {

        }
    }
}
