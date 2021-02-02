using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Entities;
using Rovecode.Lotos.Repositories;

namespace AspNetCore.Controllers
{
    public sealed class UserEntity : StorageEntity<UserEntity>
    {
        public string Name { get; set; }

        public int Counter { get; set; }

        public DateTime DateTime { get; set; }

        public async Task Increment()
        {
            Counter++;

            await Push();
        }
    }

    public sealed class SaverEntity : StorageEntity<SaverEntity>
    {
        public Guid UserId { get; set; }
    }

    public sealed class UserStorage : Storage<UserEntity>
    {
        public UserStorage(StorageContext<UserEntity> storageContext) : base(storageContext)
        {

        }

        public async Task<IEnumerable<UserEntity>> PickViaName(string name)
        {
            return await PickMany(e => e.Name == name);
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly UserStorage _storage;
        private readonly IStorage<SaverEntity> _idStorage;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, UserStorage storage, IStorage<SaverEntity> idStorage)
        {
            _logger = logger;
            _storage = storage;
            _idStorage = idStorage;
        }

        [HttpGet]
        [Route("get_user")]
        public async Task<IEnumerable<UserEntity>> GetUser(string name)
        {
            if (await _storage.Count(e => e.Name == name) < 2)
            {
                var user = await _storage.Put(new ()
                {
                    Name = name,
                    DateTime = DateTime.Now,
                });

                await _idStorage.Put(new ()
                {
                    UserId = user.Id
                });
            }

            var users = await _storage.PickViaName(name);

            foreach (var user in users)
            {
                await user.Increment();

                var saver = await _idStorage.Pick(e => e.UserId == user.Id)!;

                _logger.LogInformation(saver.Id.ToString());
                _logger.LogInformation(saver.UserId.ToString());
                _logger.LogInformation(user.Id.ToString());

                var userX = _storage.Pick(saver.UserId);
            }
            Guid guid = Guid.NewGuid();

            UserEntity us1 = await _storage.Pick(guid);
            UserEntity us2 = await _storage.Pick(guid);

            us1.Name = "Roman";
            await us1.Push();

            us2 = await us2.Pull();

            return await _storage.PickViaName(name);
        }
    }
}
