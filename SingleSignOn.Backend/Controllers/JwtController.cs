using Microsoft.AspNetCore.Mvc;
using SingleSignOn.Backend.Services;
using SingleSignOn.Backend.ViewModels;

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
        public async Task<IActionResult> Post([FromBody] CreateJwtViewModel view_model)
        {
            if (view_model.provider == "steam")
            {
                if (view_model.token == null)
                {
                    return BadRequest($"token can't be empty when provider is \"steam\".");
                }

                var jwt = await steam_identity_service_.GenerateJwt(view_model.token);
                if (view_model.redirect != null)
                {
                    return Redirect($"{view_model.redirect}/?jwt={jwt}");
                }
                return Created("", jwt);
            }
            return BadRequest($"Unknown provider: {view_model.provider}");
        }
    }
}
