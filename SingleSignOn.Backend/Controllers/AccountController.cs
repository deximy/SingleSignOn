using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SingleSignOn.Backend.Controllers
{
    [Route("accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> user_namager_;

        public AccountController(UserManager<ApplicationUser> user_namager)
        {
            user_namager_ = user_namager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string username)
        {
            var user = new ApplicationUser() {
                UserName = username,
            };
            var result = await user_namager_.CreateAsync(user);
            if (result.Succeeded)
            {
                return Created(user.Id, null);
            }

            return BadRequest();
        }
    }
}
