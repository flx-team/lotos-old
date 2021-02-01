using System;
using System.Threading.Tasks;
using AspNetCoreApiExample.Models;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Repositories;

namespace AspNetCoreApiExample.Storages
{
    public sealed class UserStorage : Storage<ProfileEntity>
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
