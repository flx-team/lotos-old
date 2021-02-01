using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiExample.Models;
using AspNetCoreApiExample.Storages;
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
        private readonly UserStorage _profileStorage;

        public ProfilesController(ILogger<ProfilesController> logger, UserStorage profileStorage)
        {
            _logger = logger;

            _profileStorage = profileStorage;
        }

        [HttpPost("add")]
        public async Task<IActionResult> PostAddProfile([FromBody] ProfileEntity profileData)
        {
            await _profileStorage.Put(profileData);

            return Ok();
        }

        [HttpGet("list")]
        public async Task<IEnumerable<ProfileEntity>> GetAllProfilesAsync()
        {
            var profile = new ProfileEntity
            {
                Name = "Roman",
                Email = "suslikov@gm.com",
                Phone = 434352,
            };

            await _profileStorage.Put(profile);

            profile.Name = "Roman S";

            await profile.Push();

            var profile2 = await profile.Pull();

            profile2.Name = "Hohol";

            await profile2.Push();

            var ents = await _profileStorage.PickMany();

            _logger.LogInformation((await _profileStorage.CustomCount()).ToString());

            return ents;
        }

        [HttpGet("listWhereName")]
        public IEnumerable<ProfileEntity> GetAllProfiles(string name)
        {
            return null!;
        }
    }
}
