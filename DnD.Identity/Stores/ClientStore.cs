using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.ObjectModel;

namespace DnD.Identity.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly IConfiguration _configuration;
        public ClientStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            if (!clientId.Equals(_configuration.GetValue<string>("ClientId")))
                return Task.FromResult(new Client());

            
             var identityClient = new Client
            {
                ClientId = clientId,
                ClientSecrets = new Collection<Secret>(new List<Secret> { new Secret(_configuration.GetValue<string>("ClientSecret").Sha256()) } ) ,
                Enabled = true,
                AllowedGrantTypes = new Collection<string>{ GrantType.ResourceOwnerPassword, GrantType.DeviceFlow},
                Claims = new Collection<ClientClaim> { new ClientClaim("role", "admin") },
                 AllowedScopes = 
                 {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.LocalApi.ScopeName
                 },
                AllowOfflineAccess = true,
                AccessTokenLifetime = 3600*24*365*10,
                DeviceCodeLifetime = 3600*24*365*10,
            };

            return Task.FromResult(identityClient);
        }
    }
}
