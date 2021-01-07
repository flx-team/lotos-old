using System;
using Rovecode.Lotos.Common;
using Rovecode.Lotos.Models;
using Rovecode.Lotos.Repositories.Storages;

namespace Rovecode.Lotos.Containers
{
    public interface IContainer : IDisposable
    {
        public IStorage<T> GetStorage<T>() where T : StorageData;

        public void Init();
        public void Sync();
        public void Fail();
        public void Ok();

        public void PushCommand(ICommand command);

        public void Sandbox(Action action);
    }
}
