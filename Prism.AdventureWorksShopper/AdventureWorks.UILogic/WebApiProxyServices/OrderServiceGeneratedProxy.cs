using System.Threading.Tasks;
using AdventureWorks.UILogic.Models;
using AdventureWorks.UILogic.Services;
using AdventureWorks.UILogic.WebApiProxy;

namespace AdventureWorks.UILogic.WebApiProxyServices
{
    public class OrderServiceGeneratedProxy : IOrderService
    {
        private readonly IWebApiProxyFactory _proxyFactory;

        public OrderServiceGeneratedProxy(IWebApiProxyFactory proxyFactory)
        {
            _proxyFactory = proxyFactory;
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            using (var client = _proxyFactory.GetOrderClient())
            {
                client.HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var orderToSend = new Order()
                {
                    UserId = order.UserId,
                    ShippingMethod = order.ShippingMethod,
                    ShoppingCart = order.ShoppingCart,
                    BillingAddress = order.BillingAddress,
                    ShippingAddress = order.ShippingAddress,
                    PaymentMethod = new PaymentMethod()
                    {
                        CardNumber = "**** **** **** ****",
                        CardVerificationCode = "****",
                        CardholderName = order.PaymentMethod.CardholderName,
                        ExpirationMonth = order.PaymentMethod.ExpirationMonth,
                        ExpirationYear = order.PaymentMethod.ExpirationYear,
                        Phone = order.PaymentMethod.Phone
                    }
                };

                var result = await client.CreateOrderAsync(orderToSend);
                return result;
            }
        }

        public async Task ProcessOrderAsync(Order order)
        {
            using (var client = _proxyFactory.GetOrderClient())
            {
                client.HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var orderToSend = new Order()
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    ShippingMethod = order.ShippingMethod,
                    ShoppingCart = order.ShoppingCart,
                    BillingAddress = order.BillingAddress,
                    ShippingAddress = order.ShippingAddress,
                    PaymentMethod = new PaymentMethod()
                    {
                        CardNumber = "**** **** **** ****",
                        CardVerificationCode = "****",
                        CardholderName = order.PaymentMethod.CardholderName,
                        ExpirationMonth = order.PaymentMethod.ExpirationMonth,
                        ExpirationYear = order.PaymentMethod.ExpirationYear,
                        Phone = order.PaymentMethod.Phone
                    }
                };

                await client.ProcessOrderAsync(orderToSend);
            }
        }
    }
}
