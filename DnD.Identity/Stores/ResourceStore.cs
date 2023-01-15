using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.ObjectModel;

namespace DnD.Identity.Stores
{
    public class ResourceStore : IResourceStore
    {
        private List<ApiScope> _apiScopes;
        private List<IdentityResource> _identityResources;

        public ResourceStore()
        {
            _apiScopes = new List<ApiScope>
            {
                new ApiScope("DnDReniaApi", "Api Renia"),
            };

            _identityResources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
     
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            return Task.FromResult(new List<ApiResource>().AsEnumerable());
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(new List<ApiResource>().AsEnumerable());
        }

        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var scopes = _apiScopes.AsEnumerable();
            return Task.FromResult(scopes.Where(scope => scopeNames.Contains(scope.Name)));
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var result = _identityResources.Where(r => scopeNames.Contains(r.Name));
            return Task.FromResult(result);
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(new Resources
            {
                ApiResources = new Collection<ApiResource>(new List<ApiResource>()),
                ApiScopes = new Collection<ApiScope>(_apiScopes),
                IdentityResources = new Collection<IdentityResource>(_identityResources)
            });
        }
    }
}
