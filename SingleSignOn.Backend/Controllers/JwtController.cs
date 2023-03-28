using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SingleSignOn.Backend.Services;

namespace SingleSignOn.Backend.Controllers
{
    [Route("jwts")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> sign_in_manager_;
        private readonly UserManager<ApplicationUser> user_namager_;
        private readonly JwtGeneratorService jwt_generator_;

        public JwtController(
            SignInManager<ApplicationUser> sign_in_manager,
            UserManager<ApplicationUser> user_namager,
            JwtGeneratorService jwt_generator
        )
        {
            sign_in_manager_ = sign_in_manager;
            user_namager_ = user_namager;
            jwt_generator_ = jwt_generator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string username)
        {
            var user = await user_namager_.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(jwt_generator_.GenerateJwt(username));
        }
    }
}
