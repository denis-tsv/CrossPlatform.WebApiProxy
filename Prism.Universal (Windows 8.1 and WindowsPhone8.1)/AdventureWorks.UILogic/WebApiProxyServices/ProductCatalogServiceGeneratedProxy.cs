using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AdventureWorks.UILogic.Models;
using AdventureWorks.UILogic.Services;
using AdventureWorks.UILogic.WebApiProxy;

namespace AdventureWorks.UILogic.WebApiProxyServices
{
    public class ProductCatalogServiceGeneratedProxy : IProductCatalogService
    {
        private readonly IWebApiProxyFactory _proxyFactory;

        public ProductCatalogServiceGeneratedProxy(IWebApiProxyFactory proxyFactory)
        {
            _proxyFactory = proxyFactory;
        }
        
        public async Task<ICollection<Category>> GetCategoriesAsync(int parentId, int maxAmountOfProducts)
        {
            using (var client = _proxyFactory.GetCategoryClient())
            {
                var result = await client.GetCategoriesAsync(parentId, maxAmountOfProducts);
                return result;
            }
        }

        public async Task<SearchResult> GetFilteredProductsAsync(string productsQueryString, int maxResults)
        {
            using (var client = _proxyFactory.GetProductClient())
            {
                var result = await client.GetSearchResultsAsync(productsQueryString, maxResults);
                return result;
            }
        }

        public async Task<ReadOnlyCollection<string>> GetSearchSuggestionsAsync(string searchTerm)
        {
            using (var client = _proxyFactory.GetSearchSuggestionClient())
            {
                var result = await client.GetSearchSuggestionsAsync(searchTerm);
                return result;
            }
        }

        public async Task<ICollection<Product>> GetProductsAsync(int categoryId)
        {
            using (var client = _proxyFactory.GetProductClient())
            {
                var result = await client.GetProductsAsync(categoryId);
                return result;
            }
        }

        public async Task<Category> GetCategoryAsync(int categoryId)
        {
            using (var client = _proxyFactory.GetCategoryClient())
            {
                var result = await client.GetCategoryAsync(categoryId);
                return result;
            }
        }

        public async Task<Product> GetProductAsync(string productNumber)
        {
            using (var client = _proxyFactory.GetProductClient())
            {
                var result = await client.GetProductAsync(productNumber);
                return result;
            }
        }
    }
}
