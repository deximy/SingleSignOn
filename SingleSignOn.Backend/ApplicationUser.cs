using Microsoft.AspNetCore.Identity;

namespace SingleSignOn.Backend
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<LinkedSteamAccount> LinkedSteamAccounts { get; set; } = new List<LinkedSteamAccount>();
    }
}
