namespace Boovey.Web
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using IdentityServer4.Models;
    using Constants;

    public class IdentityServerConfig
    {
        public static IEnumerable<Client> Clients =>
           new List<Client>
           {
                new Client
                {
                    ClientId = IdentityServerConfigValues.ClientId,
                    AllowOfflineAccess = IdentityServerConfigValues.AllowOfflineAccess,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret(IdentityServerConfigValues.ClientId.Sha256())
                    },
                    AllowedScopes = 
                    { 
                        "users", "offline_access", IdentityServerConfigValues.ClientId, "roles" 
                    }
                }
           };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResource("roles", new[] { "role" })
            };

        public static IEnumerable<ApiScope> ApiScopes => 
            new List<ApiScope>
            {
                    new ApiScope("users", "My API", new string[]{ ClaimTypes.Name, ClaimTypes.Role }),
                    new ApiScope("offline_access", "RefereshToken"),
                    new ApiScope(IdentityServerConfigValues.ClientId, "app")
            };
    }
}
