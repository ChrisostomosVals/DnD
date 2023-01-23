using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace DnD.Identity.Stores
{
    public class DeviceFlowStore : IDeviceFlowStore
    {
        public Task<DeviceCode> FindByDeviceCodeAsync(string deviceCode)
        {
            throw new NotImplementedException();
        }

        public Task<DeviceCode> FindByUserCodeAsync(string userCode)
        {
            throw new NotImplementedException();
        }

        public Task RemoveByDeviceCodeAsync(string deviceCode)
        {
            throw new NotImplementedException();
        }

        public Task StoreDeviceAuthorizationAsync(string deviceCode, string userCode, DeviceCode data)
        {
            throw new NotImplementedException();
        }

        public Task UpdateByUserCodeAsync(string userCode, DeviceCode data)
        {
            throw new NotImplementedException();
        }
    }
}
