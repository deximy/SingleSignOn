using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SingleSignOn.Backend.Services
{
    public class JwtGeneratorService
    {
        private readonly IConfiguration configuration_;

        public JwtGeneratorService(IConfiguration configuration)
        {
            configuration_ = configuration;
        }


        public string GenerateJwt(string user_name)
        {
            var token_handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration_["Jwt:Key"] ?? new string('a', 256));
            var token_descriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, user_name)
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = token_handler.CreateToken(token_descriptor);
            return token_handler.WriteToken(token);
        }
    }
}
