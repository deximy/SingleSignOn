using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using IdentityModel;

namespace SingleSignOn.Backend
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource() {
                    Name = "verification",
                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Email,
                        JwtClaimTypes.EmailVerified
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope("api1", "MyAPI")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource> {
            };

        public static IEnumerable<Client> Clients =>
            new List<Client> {
                new Client {
                    ClientId = "web",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect after login
                    RedirectUris = { "http://localhost:7000/signin-oidc" },

                    // where to redirect after logout
                    PostLogoutRedirectUris = { "http://localhost:7000/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowedScopes = new List<string> {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                }
            };
    }
}
