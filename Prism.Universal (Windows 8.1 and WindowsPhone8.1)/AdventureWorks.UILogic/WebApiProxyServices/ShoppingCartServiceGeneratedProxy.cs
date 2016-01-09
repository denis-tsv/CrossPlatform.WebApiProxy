using System.Threading.Tasks;
using AdventureWorks.UILogic.Models;
using AdventureWorks.UILogic.Services;
using AdventureWorks.UILogic.WebApiProxy;

namespace AdventureWorks.UILogic.WebApiProxyServices
{
    public class ShoppingCartServiceGeneratedProxy : IShoppingCartService
    {
        private readonly IWebApiProxyFactory _proxyFactory;

        public ShoppingCartServiceGeneratedProxy(IWebApiProxyFactory proxyFactory)
        {
            _proxyFactory = proxyFactory;
        }
        
        public async Task<ShoppingCart> GetShoppingCartAsync(string shoppingCartId)
        {
            using (var client = _proxyFactory.GetShoppingCartClient())
            {
                var result = await client.GetAsync(shoppingCartId);
                return result;
            }
        }

        public async Task AddProductToShoppingCartAsync(string shoppingCartId, string productIdToIncrement)
        {
            using (var client = _proxyFactory.GetShoppingCartClient())
            {
                await client.AddProductToShoppingCartAsync(shoppingCartId, productIdToIncrement);
            }
        }

        public async Task RemoveProductFromShoppingCartAsync(string shoppingCartId, string productIdToDecrement)
        {
            using (var client = _proxyFactory.GetShoppingCartClient())
            {
                await client.RemoveProductFromShoppingCartAsync(shoppingCartId, productIdToDecrement);
            }
        }

        public async Task RemoveShoppingCartItemAsync(string shoppingCartId, string itemIdToRemove)
        {
            using (var client = _proxyFactory.GetShoppingCartClient())
            {
                await client.RemoveShoppingCartItemAsync(shoppingCartId, itemIdToRemove);
            }
        }

        public async Task DeleteShoppingCartAsync(string shoppingCartId)
        {
            using (var client = _proxyFactory.GetShoppingCartClient())
            {
                await client.DeleteShoppingCartAsync(shoppingCartId);
            }
        }

        public async Task<bool> MergeShoppingCartsAsync(string anonymousShoppingCartId, string authenticatedShoppingCartId)
        {
            using (var client = _proxyFactory.GetShoppingCartClient())
            {
                var result = await client.MergeShoppingCartsAsync(authenticatedShoppingCartId, anonymousShoppingCartId);
                return result;
            }
        }
    }
}
