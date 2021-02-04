using System;
using System.Threading.Tasks;
using AspNetCoreApiExample.Models;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Repositories;

namespace AspNetCoreApiExample.Storages
{
    public interface IUserStorage : IStorage<ProfileEntity>
    {
        public Task<long> CustomCount();
    }

    public sealed class UserStorage : Storage<ProfileEntity>, IUserStorage
    {
        public UserStorage(StorageContext<ProfileEntity> storageContext) : base(storageContext)
        {

        }

        public Task<long> CustomCount()
        {
            return Count(e => e.Name == "Hohol");
        }
    }
}
