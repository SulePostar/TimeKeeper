using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TimeKeeper.DAL;

namespace TimeKeeper.IDP
{
    public static class Config
    {
        static List<TestUser> users;

        public static List<TestUser> GetUsers()
        {
            if (users == null)
            {
                users = new List<TestUser>();
                using (TimeContext context = new TimeContext())
                {
                    foreach (var user in context.Users)
                    {
                        users.Add(new TestUser
                        {
                            SubjectId = user.Id.ToString(),
                            Username = user.Username,
                            Password = "$ch00l",
                            Claims = new List<Claim>
                            {
                                new Claim("name", user.Name),
                                new Claim("role", user.Role)
                            }
                        });
                    }
                }
            }
            return users;
        }

        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("names", "Your names", new List<string> { "name" }),
                new IdentityResource("roles", "Your roles", new List<string> { "role" })
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("timekeeper", "Time Keeper API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientId = "tk2019",
                    ClientName = "TimeKeeper",
                    ClientSecrets = { new Secret("mistral_talents".Sha256()) },
                    
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,

                    //RedirectUris = { "https://localhost:44350/signin-oidc" },
                    RedirectUris = { "http://localhost:3300/auth-callback" },
                    //PostLogoutRedirectUris = { "https://localhost:44350/signout-callback-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:3300/" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "names",
                        "timekeeper"
                    },
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedCorsOrigins = { "http://localhost:3300", "http://localhost:3000", "https://localhost:44350" },
                    AccessTokenLifetime = 3600
                }
            };
        }
    }
}
