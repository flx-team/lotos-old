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
        private readonly IClientSessionHandle _clientSessionHandle;

        private readonly List<INotifier<object>> _notifiers;

        private readonly Queue<ICommand> _commands;

        public Container(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;

            _notifiers = new List<INotifier<object>>();
            _commands = new Queue<ICommand>();

            _clientSessionHandle = _mongoDatabase.Client.StartSession();
        }

        public IStorage<T> GetStorage<T>() where T : StorageData
        {
            return new Storage<T>(this, _mongoDatabase.GetCollection<T>(typeof(T).Name));
        }

        public void Init()
        {
            _clientSessionHandle.StartTransaction();
        }

        public void Sync()
        {
            foreach (var item in _commands)
            {
                item.Run();
            }
        }

        public void Fail()
        {
            _clientSessionHandle.AbortTransaction();
        }

        public void Ok()
        {
            _clientSessionHandle.CommitTransaction();
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

        public void PushCommand(ICommand command)
        {
            _commands.Enqueue(command);
        }

        public void Dispose()
        {
            _commands.Clear();
        }
    }
}
