using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AdventureWorks.UILogic.Services;
using AdventureWorks.UILogic.WebApiProxy;

namespace AdventureWorks.UILogic.WebApiProxyServices
{
    public class LocationServiceGeneratedProxy : ILocationService
    {
        private readonly IWebApiProxyFactory _proxyFactory;

        public LocationServiceGeneratedProxy(IWebApiProxyFactory proxyFactory)
        {
            _proxyFactory = proxyFactory;
        }

        public async Task<ReadOnlyCollection<string>> GetStatesAsync()
        {
            using (var client = _proxyFactory.GetLocationClient())
            {
                var result = await client.GetStatesAsync();
                return result;
            }
        }
    }
}
