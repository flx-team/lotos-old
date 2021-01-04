using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rovecode.Lotos.Contexts;
using Rovecode.Lotos.Repositories;

namespace AspNetCoreApiExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ILogger<ProfilesController> _logger;
        private readonly IStorage<ProfileData> _profileStorage;

        public ProfilesController(ILogger<ProfilesController> logger, IStorageContext storageContext)
        {
            _logger = logger;

            _profileStorage = storageContext.Get<ProfileData>();
        }

        [HttpPost("add")]
        public IActionResult PostAddProfile([FromBody] ProfileData profileData)
        {
            _profileStorage.Keep(profileData);

            return Ok();
        }

        [HttpGet("list")]
        public IEnumerable<ProfileData> GetAllProfiles()
        {
            return _profileStorage.SearchMany(e => true)
                .Select(e => e.Data);
        }

        [HttpGet("listWhereName")]
        public IEnumerable<ProfileData> GetAllProfiles(string name)
        {
            return _profileStorage.SearchMany(e => e.Name == name)
                .Select(e => e.Data);
        }
    }
}
