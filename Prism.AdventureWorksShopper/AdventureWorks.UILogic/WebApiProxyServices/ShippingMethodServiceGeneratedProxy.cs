using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.UILogic.Models;
using AdventureWorks.UILogic.Services;
using AdventureWorks.UILogic.WebApiProxy;

namespace AdventureWorks.UILogic.WebApiProxyServices
{
    public class ShippingMethodServiceGeneratedProxy : IShippingMethodService
    {
        private readonly IWebApiProxyFactory _proxyFactory;

        public ShippingMethodServiceGeneratedProxy(IWebApiProxyFactory proxyFactory)
        {
            _proxyFactory = proxyFactory;
        }

        public async Task<IEnumerable<ShippingMethod>> GetShippingMethodsAsync()
        {
            using (var client = _proxyFactory.GetShippingMethodClient())
            {
                var result = await client.defaultActionAsync();
                return result;
            }
        }

        public async Task<ShippingMethod> GetBasicShippingMethodAsync()
        {
            using (var client = _proxyFactory.GetShippingMethodClient())
            {
                var result = await client.basicAsync();
                return result;
            }
        }
    }
}
