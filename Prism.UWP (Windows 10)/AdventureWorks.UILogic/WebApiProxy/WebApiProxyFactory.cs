using System;
using System.Net;
using System.Net.Http;

namespace AdventureWorks.UILogic.WebApiProxy
{
    public interface IWebApiProxyFactory
    {
        void ClearCookies();
        void SaveCookies();

        IShippingMethodClient GetShippingMethodClient();
        ILocationClient GetLocationClient();
        IIdentityClient GetIdentityClient();
        ICategoryClient GetCategoryClient();
        IShoppingCartClient GetShoppingCartClient();
        IOrderClient GetOrderClient();
        IAddressClient GetAddressClient();
        IProductClient GetProductClient();
        ISearchSuggestionClient GetSearchSuggestionClient();
        IPaymentMethodClient GetPaymentMethodClient();
    }

    public class WebApiProxyFactory : IWebApiProxyFactory
    {
        private HttpClientHandler _handler;

        private readonly Uri _baseUri = new Uri(Constants.ServerAddress);
        private CookieCollection _cookie;

        private HttpClientHandler GetHttpClientHandler()
        {
            _handler = new HttpClientHandler
            {
                UseCookies = true,
            };

            if (_cookie != null)
            {
                foreach (var c in _cookie)
                {
                    _handler.CookieContainer.Add(_baseUri, (Cookie)c);
                }
            }

            return _handler;
        }

        public void ClearCookies()
        {
            _cookie = null;
        }

        public void SaveCookies()
        {
            _cookie = _handler.CookieContainer.GetCookies(_baseUri);
        }

        public IShippingMethodClient GetShippingMethodClient()
        {
            return new ShippingMethodClient(GetHttpClientHandler(), _baseUri);
        }
        public ILocationClient GetLocationClient()
        {
            return new LocationClient(GetHttpClientHandler(), _baseUri);
        }
        public IIdentityClient GetIdentityClient()
        {
            return new IdentityClient(GetHttpClientHandler(), _baseUri);
        }
        public ICategoryClient GetCategoryClient()
        {
            return new CategoryClient(GetHttpClientHandler(), _baseUri);
        }
        public IShoppingCartClient GetShoppingCartClient()
        {
            return new ShoppingCartClient(GetHttpClientHandler(), _baseUri);
        }
        public IOrderClient GetOrderClient()
        {
            return new OrderClient(GetHttpClientHandler(), _baseUri);
        }
        public IAddressClient GetAddressClient()
        {
            return new AddressClient(GetHttpClientHandler(), _baseUri);
        }
        public IProductClient GetProductClient()
        {
            return new ProductClient(GetHttpClientHandler(), _baseUri);
        }
        public ISearchSuggestionClient GetSearchSuggestionClient()
        {
            return new SearchSuggestionClient(GetHttpClientHandler(), _baseUri);
        }
        public IPaymentMethodClient GetPaymentMethodClient()
        {
            return new PaymentMethodClient(GetHttpClientHandler(), _baseUri);
        }
    }
}
