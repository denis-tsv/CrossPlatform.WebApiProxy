using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.UILogic.WebApiProxy;

namespace AdventureWorks.UILogic.Tests.Mocks
{
    public class MockWebApiProxyFactory : IWebApiProxyFactory
    {
        private string _cookie;

        public void ClearCookies()
        {
            _cookie = null;
        }

        public void SaveCookies()
        {
            _cookie = "_cookie";
        }

        public IShippingMethodClient GetShippingMethodClient()
        {
            throw new NotImplementedException();
        }

        public ILocationClient GetLocationClient()
        {
            throw new NotImplementedException();
        }

        public IIdentityClient GetIdentityClient()
        {
            throw new NotImplementedException();
        }

        public ICategoryClient GetCategoryClient()
        {
            throw new NotImplementedException();
        }

        public IShoppingCartClient GetShoppingCartClient()
        {
            throw new NotImplementedException();
        }

        public IOrderClient GetOrderClient()
        {
            throw new NotImplementedException();
        }

        public IAddressClient GetAddressClient()
        {
            throw new NotImplementedException();
        }

        public IProductClient GetProductClient()
        {
            throw new NotImplementedException();
        }

        public ISearchSuggestionClient GetSearchSuggestionClient()
        {
            throw new NotImplementedException();
        }

        public IPaymentMethodClient GetPaymentMethodClient()
        {
            throw new NotImplementedException();
        }
    }
}
