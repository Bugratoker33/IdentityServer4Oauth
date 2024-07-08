using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace AuthApi.Class
{
    public class Config
    {
        #region Scopes
        //API'larda kullanılacak izinleri tanımlar.
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
                new ApiScope("Garanti.Write","Garanti bankası yazma izni"),
                new ApiScope("Garanti.Read","Garanti bankası okuma izni"),
                new ApiScope("HalkBank.Write","HalkBank bankası yazma izni"),
                new ApiScope("HalkBank.Read","HalkBank bankası okuma izni"),
            };
        }
        #endregion
        #region Resources
        //API'lar tanımlanır.
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
                new ApiResource("Garanti"){
                    ApiSecrets = {
                        new Secret("garanti".Sha256())
                    },
                    Scopes = {
                        "Garanti.Write",
                        "Garanti.Read"
                    }
                },
                new ApiResource("HalkBank"){
                    ApiSecrets = {
                        new Secret("halkbank".Sha256())
                    },
                    Scopes = {
                        "HalkBank.Write",
                        "HalkBank.Read"
                    }
                }
            };
        }
        #endregion
        #region Clients
        //API'ları kullanacak client'lar tanımlanır.
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                        {
                            ClientId = "GarantiBankasi",
                            ClientName = "GarantiBankasi",
                            ClientSecrets = { new Secret("garanti".Sha256()) },
                            AllowedGrantTypes = { GrantType.ClientCredentials },
                            AllowedScopes = { "Garanti.Write", "Garanti.Read" , IdentityServerConstants.LocalApi.ScopeName }
                        },
                new Client
                        {
                            ClientId = "HalkBankasi",
                            ClientName = "HalkBankasi",
                            ClientSecrets = { new Secret("halkbank".Sha256()) },
                            AllowedGrantTypes = { GrantType.ClientCredentials },
                            AllowedScopes = { "HalkBank.Write", "HalkBank.Read" }
                        }
            };
        }

        #endregion
        public static IEnumerable<TestUser> GetTestUsers()
        {
            return new List<TestUser> {
        new TestUser {
            SubjectId = "test-user1",
            Username = "test-user1",
            Password = "12345",
            Claims = {
                new Claim("name","test user1"),
                new Claim("website","https://wwww.testuser1.com"),
                new Claim("gender","1")
            }
        },
        new TestUser {
            SubjectId = "test-user2",
            Username = "test-user2",
            Password = "12345",
            Claims = {
                new Claim("name","test user2"),
                new Claim("website","https://wwww.testuser2.com"),
                new Claim("gender","0")
            }
        }
      };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
              //  new IdentityResource(){Name="roles",DisplayName = "Roles", Description="Kullanıcı rolleri",UserClaims=new[]{"role"}}
             };
        }
    }
}

