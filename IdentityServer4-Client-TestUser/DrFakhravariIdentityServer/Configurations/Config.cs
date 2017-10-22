using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace DrFakhravariIdentityServer.IdentityServerConfiguration
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("PolymerApi", "Dr Fakhravari Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "DrFakhravari_Himself",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials, 

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("Resherper!".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = {"PolymerApi"}
                },
                new Client
                {
                    ClientId = "ro.DrFakhravari_Himself",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("Resherper!".Sha256())
                    },
                    AllowedScopes = {"PolymerApi"}
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",
                    Claims = new[]
                    {
                        new Claim("Employee", "Mosalla"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",
                    Claims = new[]
                    {
                        new Claim("Employee", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                }
            };
        }
    }
}