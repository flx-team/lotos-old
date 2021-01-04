using System;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Repositories;
using Rovecode.Lotos.Tests.Models;
using Xunit;

namespace Rovecode.Lotos.Tests
{
    public class StorageTest
    {
        private readonly IStorage<UserData> _userStorage;

        public StorageTest(IStorageContext storageContext)
        {
            _userStorage = storageContext.Get<UserData>();
        }

        [Fact]
        public void ExistsCheck_CreateUserData_And_BurnCreatedUserData_Test()
        {
            // create model
            UserData userData = new ()
            {
                FirstName = "TestName",
                LastName = "LastName",
                Email = "test@email.com",
                Phone = 9563224563
            };

            // keep
            var user = _userStorage.Keep(userData);

            // check
            Assert.True(user.Exist());

            // delete
            user.Burn();

            // check
            Assert.True(!user.Exist());
        }
    }
}
