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
        private readonly IUserStorage _profileStorage;
        private readonly SessionContext _sessionCtx;

        public ProfilesController(ILogger<ProfilesController> logger, IUserStorage profileStorage, SessionContext sessionContext)
        {
            _logger = logger;

            _sessionCtx = sessionContext;

            _profileStorage = profileStorage;
        }

        [HttpPost("add")]
        public async Task<IActionResult> PostAddProfile([FromBody] ProfileEntity profileData)
        {
            _profileStorage.UseSession(_sessionCtx);

            await _sessionCtx.Sandbox(async (ctx) =>
            {
                await _profileStorage.Put(profileData);

                ctx.CloseWithError();
            });

            return Ok();
        }

        [HttpGet("list")]
        public Task<IEnumerable<ProfileEntity>> GetAllProfilesAsync()
        {
            return null!;
        }

        [HttpGet("listWhereName")]
        public IEnumerable<ProfileEntity> GetAllProfiles(string name)
        {
            return null!;
        }
    }
}
