using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// additional namespaces
using AdventureWorks.UILogic.Models;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace AdventureWorks.UILogic.WebApiProxy
{
	public interface IShippingMethodClient : IControllerClient
	{
		Task<IEnumerable<ShippingMethod>> defaultActionAsync();
		Task<ShippingMethod> basicAsync();
	}
	public class ShippingMethodClient : ControllerClient, IShippingMethodClient
	{
		public ShippingMethodClient(HttpClientHandler handler, Uri baseUri) : base(handler, baseUri)
		{ }
		public Task<IEnumerable<ShippingMethod>> defaultActionAsync()
		{
			var url = "api/shippingmethod/defaultAction";

			return GetAsync<IEnumerable<ShippingMethod>>(url);
		}
		public Task<ShippingMethod> basicAsync()
		{
			var url = "api/shippingmethod/basic";

			return GetAsync<ShippingMethod>(url);
		}
	}
	public interface ILocationClient : IControllerClient
	{
		Task<ReadOnlyCollection<string>> GetStatesAsync();
	}
	public class LocationClient : ControllerClient, ILocationClient
	{
		public LocationClient(HttpClientHandler handler, Uri baseUri) : base(handler, baseUri)
		{ }
		public Task<ReadOnlyCollection<string>> GetStatesAsync()
		{
			var url = "api/Location";

			return GetAsync<ReadOnlyCollection<string>>(url);
		}
	}
	public interface IIdentityClient : IControllerClient
	{
		Task<string> GetPasswordChallengeAsync(string requestId);
		Task<UserInfo> GetIsValidAsync(string id, string requestId, string passwordHash);
		Task<bool> GetIsValidSessionAsync();
	}
	public class IdentityClient : ControllerClient, IIdentityClient
	{
		public IdentityClient(HttpClientHandler handler, Uri baseUri) : base(handler, baseUri)
		{ }
		public Task<string> GetPasswordChallengeAsync(string requestId)
		{
			var url = "api/Identity";

			url = AppendParameter(url, "requestId", requestId);

			return GetAsync<string>(url);
		}
		public Task<UserInfo> GetIsValidAsync(string id, string requestId, string passwordHash)
		{
			var url = "api/Identity/{id}";

			url = url.Replace("{id}", ConvertToString(id) );

			url = AppendParameter(url, "requestId", requestId);

			url = AppendParameter(url, "passwordHash", passwordHash);

			return GetAsync<UserInfo>(url);
		}
		public Task<bool> GetIsValidSessionAsync()
		{
			var url = "api/Identity";

			return GetAsync<bool>(url);
		}
	}
	public interface ICategoryClient : IControllerClient
	{
		Task<ICollection<Category>> GetCategoriesAsync(int parentId, int maxAmountOfProducts);
		Task<Category> GetCategoryAsync(int id);
	}
	public class CategoryClient : ControllerClient, ICategoryClient
	{
		public CategoryClient(HttpClientHandler handler, Uri baseUri) : base(handler, baseUri)
		{ }
		public Task<ICollection<Category>> GetCategoriesAsync(int parentId, int maxAmountOfProducts)
		{
			var url = "api/Category";

			url = AppendParameter(url, "parentId", parentId);

			url = AppendParameter(url, "maxAmountOfProducts", maxAmountOfProducts);

			return GetAsync<ICollection<Category>>(url);
		}
		public Task<Category> GetCategoryAsync(int id)
		{
			var url = "api/Category/{id}";

			url = url.Replace("{id}", ConvertToString(id) );

			return GetAsync<Category>(url);
		}
	}
	public interface IShoppingCartClient : IControllerClient
	{
		Task<ShoppingCart> GetAsync(string id);
		Task AddProductToShoppingCartAsync(string id, string productIdToIncrement);
		Task RemoveProductFromShoppingCartAsync(string id, string productIdToDecrement);
		Task DeleteShoppingCartAsync(string id);
		Task RemoveShoppingCartItemAsync(string id, string itemIdToRemove);
		Task<bool> MergeShoppingCartsAsync(string id, string anonymousShoppingCartId);
		Task ResetAsync(bool resetData);
	}
	public class ShoppingCartClient : ControllerClient, IShoppingCartClient
	{
		public ShoppingCartClient(HttpClientHandler handler, Uri baseUri) : base(handler, baseUri)
		{ }
		public Task<ShoppingCart> GetAsync(string id)
		{
			var url = "api/ShoppingCart/{id}";

			url = url.Replace("{id}", ConvertToString(id) );

			return GetAsync<ShoppingCart>(url);
		}
		public Task AddProductToShoppingCartAsync(string id, string productIdToIncrement)
		{
			var url = "api/ShoppingCart/{id}";

			url = url.Replace("{id}", ConvertToString(id) );

			url = AppendParameter(url, "productIdToIncrement", productIdToIncrement);

			return PostNoResultAsync(url);
		}
		public Task RemoveProductFromShoppingCartAsync(string id, string productIdToDecrement)
		{
			var url = "api/ShoppingCart/{id}";

			url = url.Replace("{id}", ConvertToString(id) );

			url = AppendParameter(url, "productIdToDecrement", productIdToDecrement);

			return PostNoResultAsync(url);
		}
		public Task DeleteShoppingCartAsync(string id)
		{
			var url = "api/ShoppingCart/{id}";

			url = url.Replace("{id}", ConvertToString(id) );

			return DeleteNoResultAsync(url);
		}
		public Task RemoveShoppingCartItemAsync(string id, string itemIdToRemove)
		{
			var url = "api/ShoppingCart/{id}";

			url = url.Replace("{id}", ConvertToString(id) );

			url = AppendParameter(url, "itemIdToRemove", itemIdToRemove);

			return PutNoResultAsync(url);
		}
		public Task<bool> MergeShoppingCartsAsync(string id, string anonymousShoppingCartId)
		{
			var url = "api/ShoppingCart/{id}";

			url = url.Replace("{id}", ConvertToString(id) );

			url = AppendParameter(url, "anonymousShoppingCartId", anonymousShoppingCartId);

			return PostAsync<bool>(url);
		}
		public Task ResetAsync(bool resetData)
		{
			var url = "api/ShoppingCart";

			url = AppendParameter(url, "resetData", resetData);

			return PostNoResultAsync(url);
		}
	}
	public interface IOrderClient : IControllerClient
	{
		Task<Order> GetOrderAsync(int id);
		Task<int> CreateOrderAsync(Order order);
		Task ProcessOrderAsync(Order order);
		Task ResetAsync(bool resetData);
	}
	public class OrderClient : ControllerClient, IOrderClient
	{
		public OrderClient(HttpClientHandler handler, Uri baseUri) : base(handler, baseUri)
		{ }
		public Task<Order> GetOrderAsync(int id)
		{
			var url = "api/Order/{id}";

			url = url.Replace("{id}", ConvertToString(id) );

			return GetAsync<Order>(url);
		}
		public Task<int> CreateOrderAsync(Order order)
		{
			var url = "api/Order";

			return PostAsync<int>(url, order);
		}
		public Task ProcessOrderAsync(Order order)
		{
			var url = "api/Order/{id}";

			var id = order.Id;
			url = url.Replace("{id}", ConvertToString(id) );

			return PutNoResultAsync(url, order);
		}
		public Task ResetAsync(bool resetData)
		{
			var url = "api/Order";

			url = AppendParameter(url, "resetData", resetData);

			return PostNoResultAsync(url);
		}
	}
	public interface IAddressClient : IControllerClient
	{
		Task<ICollection<Address>> GetAllAsync();
		Task<bool> PostAddressAsync(Address address);
		Task<bool> PutAsync(string defaultAddressId, AddressType addressType);
	}
	public class AddressClient : ControllerClient, IAddressClient
	{
		public AddressClient(HttpClientHandler handler, Uri baseUri) : base(handler, baseUri)
		{ }
		public Task<ICollection<Address>> GetAllAsync()
		{
			var url = "api/Address";

			return GetAsync<ICollection<Address>>(url);
		}
		public Task<bool> PostAddressAsync(Address address)
		{
			var url = "api/Address";

			return PostAsync<bool>(url, address);
		}
		public Task<bool> PutAsync(string defaultAddressId, AddressType addressType)
		{
			var url = "api/Address";

			url = AppendParameter(url, "defaultAddressId", defaultAddressId);

			url = AppendParameter(url, "addressType", addressType);

			return PutAsync<bool>(url);
		}
	}
	public interface IProductClient : IControllerClient
	{
		Task<IEnumerable<Product>> GetProductsAsync();
		Task<ICollection<Product>> GetProductsAsync(int categoryId);
		Task<Product> GetProductAsync(string id);
		Task<SearchResult> GetSearchResultsAsync(string queryString, int maxResults);
	}
	public class ProductClient : ControllerClient, IProductClient
	{
		public ProductClient(HttpClientHandler handler, Uri baseUri) : base(handler, baseUri)
		{ }
		public Task<IEnumerable<Product>> GetProductsAsync()
		{
			var url = "api/Product";

			return GetAsync<IEnumerable<Product>>(url);
		}
		public Task<ICollection<Product>> GetProductsAsync(int categoryId)
		{
			var url = "api/Product";

			url = AppendParameter(url, "categoryId", categoryId);

			return GetAsync<ICollection<Product>>(url);
		}
		public Task<Product> GetProductAsync(string id)
		{
			var url = "api/Product/{id}";

			url = url.Replace("{id}", ConvertToString(id) );

			return GetAsync<Product>(url);
		}
		public Task<SearchResult> GetSearchResultsAsync(string queryString, int maxResults)
		{
			var url = "api/Product";

			url = AppendParameter(url, "queryString", queryString);

			url = AppendParameter(url, "maxResults", maxResults);

			return GetAsync<SearchResult>(url);
		}
	}
	public interface ISearchSuggestionClient : IControllerClient
	{
		Task<IEnumerable<string>> GetSearchSuggestionsAsync();
		Task<ReadOnlyCollection<string>> GetSearchSuggestionsAsync(string searchTerm);
	}
	public class SearchSuggestionClient : ControllerClient, ISearchSuggestionClient
	{
		public SearchSuggestionClient(HttpClientHandler handler, Uri baseUri) : base(handler, baseUri)
		{ }
		public Task<IEnumerable<string>> GetSearchSuggestionsAsync()
		{
			var url = "api/SearchSuggestion";

			return GetAsync<IEnumerable<string>>(url);
		}
		public Task<ReadOnlyCollection<string>> GetSearchSuggestionsAsync(string searchTerm)
		{
			var url = "api/SearchSuggestion";

			url = AppendParameter(url, "searchTerm", searchTerm);

			return GetAsync<ReadOnlyCollection<string>>(url);
		}
	}
	public interface IPaymentMethodClient : IControllerClient
	{
		Task<ICollection<PaymentMethod>> GetAllAsync();
		Task<bool> PostAddressAsync(PaymentMethod paymentMethod);
		Task<bool> PutAsync(string defaultPaymentMethodId);
	}
	public class PaymentMethodClient : ControllerClient, IPaymentMethodClient
	{
		public PaymentMethodClient(HttpClientHandler handler, Uri baseUri) : base(handler, baseUri)
		{ }
		public Task<ICollection<PaymentMethod>> GetAllAsync()
		{
			var url = "api/PaymentMethod";

			return GetAsync<ICollection<PaymentMethod>>(url);
		}
		public Task<bool> PostAddressAsync(PaymentMethod paymentMethod)
		{
			var url = "api/PaymentMethod";

			return PostAsync<bool>(url, paymentMethod);
		}
		public Task<bool> PutAsync(string defaultPaymentMethodId)
		{
			var url = "api/PaymentMethod";

			url = AppendParameter(url, "defaultPaymentMethodId", defaultPaymentMethodId);

			return PutAsync<bool>(url);
		}
	}
}
