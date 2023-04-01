using Microsoft.AspNetCore.Identity;
using System.Collections.Specialized;
using System.Web;

namespace SingleSignOn.Backend.Services
{
    public class SteamIdentityService
    {
        private readonly CustomUserManager user_namager_;
        private readonly JwtGeneratorService jwt_generator_;

        private readonly HttpClient http_client_;
        private NameValueCollection? token_collection_;

        public SteamIdentityService(
            CustomUserManager user_namager,
            JwtGeneratorService jwt_generator
        )
        {
            user_namager_ = user_namager;
            jwt_generator_ = jwt_generator;

            http_client_ = new HttpClient();
        }

        public async Task<string> GenerateJwt(string token)
        {
            token_collection_ = HttpUtility.ParseQueryString(token);

            return await GenerateJwt();
        }

        private async Task<string> GenerateJwt()
        {
            var is_token_valid = await VerifyToken();
            if (!is_token_valid)
            {
                throw new ExternalCredentialException();
            }

            var steam_id = GetSteamId();
            if (string.IsNullOrEmpty(steam_id))
            {
                throw new FormatException();
            }

            var user = await user_namager_.FindBySteamIdAsync(steam_id);
            if (user == null)
            {
                user = await CreateUserBySteamId(steam_id);
            }

            return jwt_generator_.GenerateJwt(user.Id);
        }

        public async Task<ApplicationUser> CreateUserBySteamId(string steam_id)
        {
            var new_user = new ApplicationUser() {
                UserName = steam_id,
                LinkedSteamAccounts = new List<LinkedSteamAccount>() {
                    new LinkedSteamAccount() {
                        steam_id = steam_id
                    }
                }
            };
            var create_user_result = await user_namager_.CreateAsync(new_user);
            if (!create_user_result.Succeeded)
            {
                throw new CreateUserException(create_user_result.Errors);
            }
            return new_user;
        }

        public string? GetSteamId(string token)
        {
            token_collection_ = HttpUtility.ParseQueryString(token);

            return GetSteamId();
        }

        private string? GetSteamId()
        {
            var steam_url = token_collection_!.Get("openid.claimed_id");
            var prefix = "https://steamcommunity.com/openid/id/";
            return steam_url?.Substring(steam_url.IndexOf(prefix) + prefix.Length);
        }

        public async Task<bool> VerifyToken(string token)
        {
            token_collection_ = HttpUtility.ParseQueryString(token);
            return await VerifyToken();
        }

        private async Task<bool> VerifyToken()
        {
            token_collection_!.Set("openid.mode", "check_authentication");
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "https://steamcommunity.com/openid/login" + "?" + token_collection_.ToString()
            );
            var response = await http_client_.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return content.Contains("is_valid:true");
        }
    }

    public class ExternalCredentialException : Exception
    {

    }

    public class CreateUserException : Exception
    {
        private readonly IEnumerable<IdentityError>? errors_;

        public CreateUserException(IEnumerable<IdentityError>? errors = null)
        {
            errors_ = errors;
        }

        public IEnumerable<IdentityError>? errors
        {
            get
            {
                return errors_;
            }
        }
    }
}
