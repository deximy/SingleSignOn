using Microsoft.AspNetCore.Mvc;
using SingleSignOn.Backend.Services;
using System.Data;
using System.Web;

namespace SingleSignOn.Backend.Controllers
{
    [Route("oidc")]
    [ApiController]
    public class OidcCallbackController : ControllerBase
    {
        private readonly SteamVerifyService steam_verify_service_;
        private readonly CustomUserManager user_namager_;
        private readonly JwtGeneratorService jwt_generator_;
        private readonly ApplicationDbContext db_context_;

        public OidcCallbackController(
            SteamVerifyService steam_verify_service,
            CustomUserManager user_namager,
            JwtGeneratorService jwt_generator,
            ApplicationDbContext db_context
        )
        {
            steam_verify_service_ = steam_verify_service;
            user_namager_ = user_namager;
            jwt_generator_ = jwt_generator;
            db_context_ = db_context;
        }

        [HttpGet("steam")]
        public async Task<IActionResult> Get()
        {
            var verify_result = await steam_verify_service_.Verify(HttpContext.Request.QueryString.Value ?? throw new NoNullAllowedException());
            if (!verify_result)
            {
                return UnprocessableEntity($"Invalid authentication.");
            }

            var steam_id = GetSteamIdFromQuery(HttpContext.Request.QueryString);
            if (string.IsNullOrEmpty(steam_id))
            {
                return StatusCode(500, $"An internal error occurred. Invalid steam id.");
            }

            var user = await user_namager_.FindBySteamIdAsync(steam_id);
            if (user == null)
            {
                var create_user_result = await user_namager_.CreateAsync(
                    new ApplicationUser() {
                        UserName = steam_id
                    }
                );
                if (create_user_result == null)
                {
                    return UnprocessableEntity($"Failed to create a user with steam id {steam_id}.");
                }
                if (!create_user_result.Succeeded)
                {
                    return UnprocessableEntity(
                        create_user_result.Errors.Select(
                            error => {
                                return new {
                                    code = error.Code,
                                    description = error.Description
                                };
                            }
                        )
                    );
                }

                user = await user_namager_.FindByNameAsync(steam_id);
                if (user == null)
                {
                    return StatusCode(500, $"An internal error occurred. User created but not found.");
                }
                user.LinkedSteamAccounts.Add(new LinkedSteamAccount() { steam_id = steam_id });
                await db_context_.SaveChangesAsync();

                return Ok(jwt_generator_.GenerateJwt(steam_id));
            }

            return Ok(jwt_generator_.GenerateJwt(steam_id));
        }

        string? GetSteamIdFromQuery(QueryString query)
        {
            var steam_url = HttpUtility.ParseQueryString(query.Value ?? throw new NoNullAllowedException()).Get("openid.claimed_id");
            var prefix = "https://steamcommunity.com/openid/id/";
            return steam_url?.Substring(steam_url.IndexOf(prefix) + prefix.Length);
        }
    }
}
