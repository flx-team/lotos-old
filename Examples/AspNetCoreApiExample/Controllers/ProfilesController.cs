using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rovecode.Lotos.Containers;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Repositories;

namespace AspNetCoreApiExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ILogger<ProfilesController> _logger;
        private readonly IStorage<ProfileEntity> _profileStorage;

        public ProfilesController(ILogger<ProfilesController> logger, IContainer storageContext)
        {
            _logger = logger;

            _profileStorage = storageContext.GetStorage<ProfileEntity>();
        }

        [HttpPost("add")]
        public IActionResult PostAddProfile([FromBody] ProfileEntity profileData)
        {
            _profileStorage.Put(profileData);

            return Ok();
        }

        [HttpGet("list")]
        public IEnumerable<ProfileEntity> GetAllProfiles()
        {
            return _profileStorage.PickMany(e => true)
                .Select(e => e.Value);
        }

        [HttpGet("listWhereName")]
        public IEnumerable<ProfileEntity> GetAllProfiles(string name)
        {
            return _profileStorage.PickMany(e => e.Name == name)
                .Select(e => e.Value);
        }
    }
}
