using System;
using Rovecode.Lotos.Containers;
using Rovecode.Lotos.Repositories;

namespace Rovecode.Lotos.Contexts
{
    public interface IStorageContext
    {
        public IContainer CreateContainer();
    }
}
