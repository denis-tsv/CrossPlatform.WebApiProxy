using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.UILogic.Models;
using AdventureWorks.UILogic.Services;
using AdventureWorks.UILogic.WebApiProxy;

namespace AdventureWorks.UILogic.WebApiProxyServices
{
    public class AddressServiceGeneratedProxy : IAddressService
    {
        private readonly IWebApiProxyFactory _proxyFactory;
        
        public AddressServiceGeneratedProxy(IWebApiProxyFactory proxyFactory)
        {
            _proxyFactory = proxyFactory;
        }

        public async Task<ICollection<Address>> GetAddressesAsync()
        {
            using (var client = _proxyFactory.GetAddressClient())
            {
                var result = await client.GetAllAsync();
                return result;
            }
        }

        public async Task SaveAddressAsync(Address address)
        {
            using (var client = _proxyFactory.GetAddressClient())
            {
                await client.PostAddressAsync(address);
            }
        }

        public async Task SetDefault(string defaultAddressId, AddressType addressType)
        {
            using (var client = _proxyFactory.GetAddressClient())
            {
                await client.PutAsync(defaultAddressId, addressType);
            }
        }
    }
}
