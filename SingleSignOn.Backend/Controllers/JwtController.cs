using Microsoft.AspNetCore.Mvc;
using SingleSignOn.Backend.Services;

namespace SingleSignOn.Backend.Controllers
{
    [Route("jwts")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private readonly SteamIdentityService steam_identity_service_;

        public JwtController(
            SteamIdentityService steam_identity_service
        )
        {
            steam_identity_service_ = steam_identity_service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string data)
        {
            return Ok(await steam_identity_service_.GenerateJwt(data));
        }
    }
}
