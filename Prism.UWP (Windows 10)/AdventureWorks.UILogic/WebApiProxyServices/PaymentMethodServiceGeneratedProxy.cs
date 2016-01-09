using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.UILogic.Models;
using AdventureWorks.UILogic.Services;
using AdventureWorks.UILogic.WebApiProxy;

namespace AdventureWorks.UILogic.WebApiProxyServices
{
    public class PaymentMethodServiceGeneratedProxy : IPaymentMethodService
    {
        private readonly IWebApiProxyFactory _proxyFactory;

        public PaymentMethodServiceGeneratedProxy(IWebApiProxyFactory proxyFactory)
        {
            _proxyFactory = proxyFactory;
        }

        public async Task<ICollection<PaymentMethod>> GetPaymentMethodsAsync()
        {
            using (var client = _proxyFactory.GetPaymentMethodClient())
            {
                var result = await client.GetAllAsync();
                return result;
            }
                
        }

        public async Task SavePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            using (var client = _proxyFactory.GetPaymentMethodClient())
            {
                await client.PostAddressAsync(paymentMethod);
            }
        }

        public async Task SetDefault(string defaultPaymentMethodId)
        {
            using (var client = _proxyFactory.GetPaymentMethodClient())
            {
                await client.PutAsync(defaultPaymentMethodId);
            }
        }
    }
}
