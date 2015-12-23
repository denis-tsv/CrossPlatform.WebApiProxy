using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// additional namespaces
using Demo.Model;

namespace Demo.Client.UseSharedModel.WebApiProxy
{
	public interface ITestPostMethodClient : Demo.Client.UseSharedModel.WebApiProxy.IControllerClient
	{
		Task PostXAsync(int x);
		Task<int> PostIdAsync(int id, string str);
	}
	public class TestPostMethodClient : Demo.Client.UseSharedModel.WebApiProxy.ControllerClient, ITestPostMethodClient
	{
		public TestPostMethodClient(Uri baseUrl) : base(baseUrl)
		{ }
		public Task PostXAsync(int x)
		{
			var url = "api/TestPostMethod/PostX";

			return PostNoResultAsync(url, x);
		}
		public Task<int> PostIdAsync(int id, string str)
		{
			var url = "api/TestPostMethod/{id}/PostId";

			url = url.Replace("{id}", ConvertToString(id) );

			return PostAsync<int>(url, str);
		}
	}
	public interface ITestPutMethodClient : Demo.Client.UseSharedModel.WebApiProxy.IControllerClient
	{
		Task<User> PutProxyParameterSourceAsync(User user);
		Task PutVoidAsync(User user);
		Task PutTaskAsync(User user);
		Task PutResponseTypeVoidAsync(int id, User user);
	}
	public class TestPutMethodClient : Demo.Client.UseSharedModel.WebApiProxy.ControllerClient, ITestPutMethodClient
	{
		public TestPutMethodClient(Uri baseUrl) : base(baseUrl)
		{ }
		public Task<User> PutProxyParameterSourceAsync(User user)
		{
			var url = "api/TestPutMethod/{id}/PutProxyParameterSource";

			var id = user.Id;
			url = url.Replace("{id}", ConvertToString(id) );

			return PutAsync<User>(url, user);
		}
		public Task PutVoidAsync(User user)
		{
			var url = "api/TestPutMethod/{id}/PutVoid";

			var id = user.Id;
			url = url.Replace("{id}", ConvertToString(id) );

			return PutNoResultAsync(url, user);
		}
		public Task PutTaskAsync(User user)
		{
			var url = "api/TestPutMethod/{id}/PutTask";

			var id = user.Id;
			url = url.Replace("{id}", ConvertToString(id) );

			return PutNoResultAsync(url, user);
		}
		public Task PutResponseTypeVoidAsync(int id, User user)
		{
			var url = "api/TestPutMethod/{id}/PutResponseTypeVoid";

			url = url.Replace("{id}", ConvertToString(id) );

			return PutNoResultAsync(url, user);
		}
	}
	/// <summary>
	/// Controller documentation
	/// multiline
	/// </summary>
	public interface ITestDocumentationClient : Demo.Client.UseSharedModel.WebApiProxy.IControllerClient
	{
		/// <summary>
		/// Method documentation
		/// multiline
		/// </summary>
		/// <param name="urlParam">Url parameter documentation</param>
		/// <param name="bodyParam">Body parameter documentation</param>
		/// <returns>Return value documentation</returns>
		Task<string> PostWithDocumentationAsync(string urlParam, string bodyParam);
		Task<string> PostWithEmptyDocumentationAsync(string doc);
	}
	public class TestDocumentationClient : Demo.Client.UseSharedModel.WebApiProxy.ControllerClient, ITestDocumentationClient
	{
		public TestDocumentationClient(Uri baseUrl) : base(baseUrl)
		{ }
		public Task<string> PostWithDocumentationAsync(string urlParam, string bodyParam)
		{
			var url = "api/TestDocumentation/PostWithDocumentation";

			url = AppendParameter(url, "urlParam", urlParam);

			return PostAsync<string>(url, bodyParam);
		}
		public Task<string> PostWithEmptyDocumentationAsync(string doc)
		{
			var url = "api/TestDocumentation/PostWithEmptyDocumentation";

			return PostAsync<string>(url, doc);
		}
	}
	public interface ITestDeleteMethodClient : Demo.Client.UseSharedModel.WebApiProxy.IControllerClient
	{
		Task DeleteAsync(int id);
		Task<int> DeleteArrayAsync(string[] codes);
		Task<DateTime> DeleteDateTimeAsync(DateTime dateTime);
		Task<DateTimeOffset> DeleteDateTimeOffsetAsync(DateTimeOffset offset);
		Task<DataAnnotationsModel> DeleteDataAnnotationsModelAsync(DataAnnotationsModel model);
	}
	public class TestDeleteMethodClient : Demo.Client.UseSharedModel.WebApiProxy.ControllerClient, ITestDeleteMethodClient
	{
		public TestDeleteMethodClient(Uri baseUrl) : base(baseUrl)
		{ }
		public Task DeleteAsync(int id)
		{
			var url = "api/TestDeleteMethod/DeleteSimple";

			url = AppendParameter(url, "id", id);

			return DeleteNoResultAsync(url);
		}
		public Task<int> DeleteArrayAsync(string[] codes)
		{
			var url = "api/TestDeleteMethod/DeleteArray";

			foreach (var item in codes)
			{
				url = AppendParameter(url, "codes", item);
			}

			return DeleteAsync<int>(url);
		}
		public Task<DateTime> DeleteDateTimeAsync(DateTime dateTime)
		{
			var url = "api/TestDeleteMethod/DeleteDateTime";

			url = AppendParameter(url, "dateTime", dateTime, "{0:o}");

			return DeleteAsync<DateTime>(url);
		}
		public Task<DateTimeOffset> DeleteDateTimeOffsetAsync(DateTimeOffset offset)
		{
			var url = "api/TestDeleteMethod/DeleteDateTimeOffset";

			url = AppendParameter(url, "offset", offset, "{0:u}");

			return DeleteAsync<DateTimeOffset>(url);
		}
		public Task<DataAnnotationsModel> DeleteDataAnnotationsModelAsync(DataAnnotationsModel model)
		{
			var url = "api/TestDeleteMethod/DeleteFormatPropertyModel";

			url = AppendParametersFromPropertiesOnRuntime(url, "model", model, false);

			return DeleteAsync<DataAnnotationsModel>(url);
		}
	}
	public interface ITestGetMethodClient : Demo.Client.UseSharedModel.WebApiProxy.IControllerClient
	{
		Task<int> GetSimpleAsync();
		Task<AllDataTypesModel> GetClassAsync();
		Task<MyPointStruct> GetStructureAsync();
		Task<EnumModel> GetEnumAsync();
		Task<User> GetInretitanceHierarchyAsync();
		Task<MyTask> GetCycleAsync();
		Task<int[]> GetIntArrayAsync();
		Task<List<string>> GetStringListAsync();
		Task<IEnumerable<AllDataTypesModel>> GetCustomIEnumerableAsync();
		Task<AllDataTypesModel[]> GetCustomArrayAsync();
		Task<Dictionary<int, string>> GetSimpleDictionaryAsync();
		Task<Dictionary<long, AllDataTypesModel>> GetCustomDictionaryAsync();
		Task<int> GetWithOneSimpleParamAsync(int x);
		Task<double[]> GetWithTwoSimpleParamAsync(int x, double y);
		Task<User> GetWithOneCustomParamAsync(User u);
		Task<string> GetWithTwoDifferentParamAsync(string str, User u);
		Task<NestedArrayFilter> GetWithNestedArrayParamAsync(string str, NestedArrayFilter filter);
		Task<string> GetWithTwoCustomParamAsync(AllDataTypesModel x, User u, int intParam);
		Task<List<int>> GetWithSimpleArrayParamAsync(int[] numbers);
		Task<List<string>> GetWithTwoIEnumerableParamAsync(IEnumerable<int> numbers, List<string> names);
		Task<int> GetOneSimpleWithDefaultAsync(int x = 1024);
		Task<string> GetManySimpleWithDefaultAsync(int x, string str = "132", double d = 123.456);
		// Unable to generate proxy method for GetIHttpActionResult. Please specify the return value by ResponseType attribute
		// Unable to generate proxy method for GetHttpResponseMessage. Please specify the return value by ResponseType attribute
		Task<int> GetIHttpActionResultWithResponseTypeAsync();
		Task<AllDataTypesModel> GetHttpResponseMessageWithResponseTypeAsync();
		Task<int> GetTaskSimpleAsync();
		// Unable to generate proxy method for GetTaskHttpResponseMessage. Please specify the return value by ResponseType attribute
		Task<AllDataTypesModel> GetTaskIHttpActionResultWithResponseTypeAsync();
	}
	public class TestGetMethodClient : Demo.Client.UseSharedModel.WebApiProxy.ControllerClient, ITestGetMethodClient
	{
		public TestGetMethodClient(Uri baseUrl) : base(baseUrl)
		{ }
		public Task<int> GetSimpleAsync()
		{
			var url = "api/TestGetMethod/GetSimple";

			return GetAsync<int>(url);
		}
		public Task<AllDataTypesModel> GetClassAsync()
		{
			var url = "api/TestGetMethod/GetClass";

			return GetAsync<AllDataTypesModel>(url);
		}
		public Task<MyPointStruct> GetStructureAsync()
		{
			var url = "api/TestGetMethod/GetStructure";

			return GetAsync<MyPointStruct>(url);
		}
		public Task<EnumModel> GetEnumAsync()
		{
			var url = "api/TestGetMethod/GetEnum";

			return GetAsync<EnumModel>(url);
		}
		public Task<User> GetInretitanceHierarchyAsync()
		{
			var url = "api/TestGetMethod/GetInretitanceHierarchy";

			return GetAsync<User>(url);
		}
		public Task<MyTask> GetCycleAsync()
		{
			var url = "api/TestGetMethod/GetCycle";

			return GetAsync<MyTask>(url);
		}
		public Task<int[]> GetIntArrayAsync()
		{
			var url = "api/TestGetMethod/GetIntArray";

			return GetAsync<int[]>(url);
		}
		public Task<List<string>> GetStringListAsync()
		{
			var url = "api/TestGetMethod/GetStringList";

			return GetAsync<List<string>>(url);
		}
		public Task<IEnumerable<AllDataTypesModel>> GetCustomIEnumerableAsync()
		{
			var url = "api/TestGetMethod/GetCustomIEnumerable";

			return GetAsync<IEnumerable<AllDataTypesModel>>(url);
		}
		public Task<AllDataTypesModel[]> GetCustomArrayAsync()
		{
			var url = "api/TestGetMethod/GetCustomArray";

			return GetAsync<AllDataTypesModel[]>(url);
		}
		public Task<Dictionary<int, string>> GetSimpleDictionaryAsync()
		{
			var url = "api/TestGetMethod/GetSimpleDictionary";

			return GetAsync<Dictionary<int, string>>(url);
		}
		public Task<Dictionary<long, AllDataTypesModel>> GetCustomDictionaryAsync()
		{
			var url = "api/TestGetMethod/GetCustomDictionary";

			return GetAsync<Dictionary<long, AllDataTypesModel>>(url);
		}
		public Task<int> GetWithOneSimpleParamAsync(int x)
		{
			var url = "api/TestGetMethod/GetWithOneSimpleParam";

			url = AppendParameter(url, "x", x);

			return GetAsync<int>(url);
		}
		public Task<double[]> GetWithTwoSimpleParamAsync(int x, double y)
		{
			var url = "api/TestGetMethod/GetWithTwoSimpleParam";

			url = AppendParameter(url, "x", x);

			url = AppendParameter(url, "y", y);

			return GetAsync<double[]>(url);
		}
		public Task<User> GetWithOneCustomParamAsync(User u)
		{
			var url = "api/TestGetMethod/GetWithOneCustomParam";

			url = AppendParametersFromPropertiesOnRuntime(url, "u", u, false);

			return GetAsync<User>(url);
		}
		public Task<string> GetWithTwoDifferentParamAsync(string str, User u)
		{
			var url = "api/TestGetMethod/GetWithTwoDiffenentStringParam";

			url = AppendParameter(url, "str", str);

			url = AppendParametersFromPropertiesOnRuntime(url, "u", u, false);

			return GetAsync<string>(url);
		}
		public Task<NestedArrayFilter> GetWithNestedArrayParamAsync(string str, NestedArrayFilter filter)
		{
			var url = "api/TestGetMethod/GetWithNestedArrayParam";

			url = AppendParameter(url, "str", str);

			url = AppendParametersFromPropertiesOnRuntime(url, "filter", filter, false);

			return GetAsync<NestedArrayFilter>(url);
		}
		public Task<string> GetWithTwoCustomParamAsync(AllDataTypesModel x, User u, int intParam)
		{
			var url = "api/TestGetMethod/GetWithTwoCustomParam";

			url = AppendParametersFromPropertiesOnRuntime(url, "x", x, true);

			url = AppendParametersFromPropertiesOnRuntime(url, "u", u, true);

			url = AppendParameter(url, "intParam", intParam);

			return GetAsync<string>(url);
		}
		public Task<List<int>> GetWithSimpleArrayParamAsync(int[] numbers)
		{
			var url = "api/TestGetMethod/GetWithSimpleArrayParam";

			foreach (var item in numbers)
			{
				url = AppendParameter(url, "numbers", item);
			}

			return GetAsync<List<int>>(url);
		}
		public Task<List<string>> GetWithTwoIEnumerableParamAsync(IEnumerable<int> numbers, List<string> names)
		{
			var url = "api/TestGetMethod/GetWithTwoSimpleArrayParam";

			foreach (var item in numbers)
			{
				url = AppendParameter(url, "numbers", item);
			}

			foreach (var item in names)
			{
				url = AppendParameter(url, "names", item);
			}

			return GetAsync<List<string>>(url);
		}
		public Task<int> GetOneSimpleWithDefaultAsync(int x = 1024)
		{
			var url = "api/TestGetMethod/GetOneSimpleWithDefault";

			url = AppendParameter(url, "x", x);

			return GetAsync<int>(url);
		}
		public Task<string> GetManySimpleWithDefaultAsync(int x, string str = "132", double d = 123.456)
		{
			var url = "api/TestGetMethod/GetManySimpleWithDefault";

			url = AppendParameter(url, "x", x);

			url = AppendParameter(url, "str", str);

			url = AppendParameter(url, "d", d);

			return GetAsync<string>(url);
		}
		// Unable to generate proxy method for GetIHttpActionResult. Please specify the return value by ResponseType attribute
		// Unable to generate proxy method for GetHttpResponseMessage. Please specify the return value by ResponseType attribute
		public Task<int> GetIHttpActionResultWithResponseTypeAsync()
		{
			var url = "api/TestGetMethod/GetIHttpActionResultWithResponseType";

			return GetAsync<int>(url);
		}
		public Task<AllDataTypesModel> GetHttpResponseMessageWithResponseTypeAsync()
		{
			var url = "api/TestGetMethod/GetHttpResponseMessageWithResponseType";

			return GetAsync<AllDataTypesModel>(url);
		}
		public Task<int> GetTaskSimpleAsync()
		{
			var url = "api/TestGetMethod/GetTaskSimple";

			return GetAsync<int>(url);
		}
		// Unable to generate proxy method for GetTaskHttpResponseMessage. Please specify the return value by ResponseType attribute
		public Task<AllDataTypesModel> GetTaskIHttpActionResultWithResponseTypeAsync()
		{
			var url = "api/TestGetMethod/GetTaskIHttpActionResultWithResponseType";

			return GetAsync<AllDataTypesModel>(url);
		}
	}
}
