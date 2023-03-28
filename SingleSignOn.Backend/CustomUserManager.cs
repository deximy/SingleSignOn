using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SingleSignOn.Backend
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {
        public CustomUserManager(
            IUserStore<ApplicationUser> user_store,
            IOptions<IdentityOptions> options_accessor,
            IPasswordHasher<ApplicationUser> password_hasher,
            IEnumerable<IUserValidator<ApplicationUser>> user_validators,
            IEnumerable<IPasswordValidator<ApplicationUser>> password_validators,
            ILookupNormalizer lookup_normalizer,
            IdentityErrorDescriber identity_error_describer,
            IServiceProvider service_provider,
            ILogger<UserManager<ApplicationUser>> logger
        ) : base(
            user_store,
            options_accessor,
            password_hasher,
            user_validators,
            password_validators,
            lookup_normalizer,
            identity_error_describer,
            service_provider,
            logger
        )
        {
        }

        public async Task<ApplicationUser?> FindBySteamIdAsync(string steam_id)
        {
            return await Users.FirstOrDefaultAsync(
                user => user.LinkedSteamAccounts.Any(linked_steam_account => linked_steam_account.steam_id == steam_id)
            );
        }
    }
}
