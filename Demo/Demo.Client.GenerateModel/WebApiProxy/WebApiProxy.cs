using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

// additional namespaces
using Demo.Model;

namespace Demo.Client.GenerateModel.WebApiProxy
{
	public  partial class User : LayerSupertype
	{
		private string _name;
		public virtual string Name 
		{
			get { return _name; }
			set
			{
				if (!_name.Equals(value))
				{
					_name = value;
					OnPropertyChanged("Name");
				}
			}
		}
	}
	/// <summary>
	/// Class documentation
	/// </summary>
	public abstract partial class LayerSupertype : INotifyPropertyChanged
	{
		private int _id;
		private string _code;
		/// <summary>
		/// Property documentation
		/// </summary>
		public virtual int Id 
		{
			get { return _id; }
			set
			{
				if (!_id.Equals(value))
				{
					_id = value;
					OnPropertyChanged("Id");
				}
			}
		}
		public virtual string Code 
		{
			get { return _code; }
			set
			{
				if (!_code.Equals(value))
				{
					_code = value;
					OnPropertyChanged("Code");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	public  partial class MetadataModel : INotifyPropertyChanged
	{
		private string _requred;
		private int _intRange;
		private int _doubleRange;
		private string _maxLength;
		private string _minLength;
		private string _stringLength;
		private string _stringLengthWithMinimum;
		[Required]
		public virtual string Requred 
		{
			get { return _requred; }
			set
			{
				if (!_requred.Equals(value))
				{
					_requred = value;
					OnPropertyChanged("Requred");
				}
			}
		}
		[Range(1, 100)]
		public virtual int IntRange 
		{
			get { return _intRange; }
			set
			{
				if (!_intRange.Equals(value))
				{
					_intRange = value;
					OnPropertyChanged("IntRange");
				}
			}
		}
		[Range(0.5, 1)]
		public virtual int DoubleRange 
		{
			get { return _doubleRange; }
			set
			{
				if (!_doubleRange.Equals(value))
				{
					_doubleRange = value;
					OnPropertyChanged("DoubleRange");
				}
			}
		}
		[MaxLength(1024)]
		public virtual string MaxLength 
		{
			get { return _maxLength; }
			set
			{
				if (!_maxLength.Equals(value))
				{
					_maxLength = value;
					OnPropertyChanged("MaxLength");
				}
			}
		}
		[MinLength(32)]
		public virtual string MinLength 
		{
			get { return _minLength; }
			set
			{
				if (!_minLength.Equals(value))
				{
					_minLength = value;
					OnPropertyChanged("MinLength");
				}
			}
		}
		[StringLength(1024)]
		public virtual string StringLength 
		{
			get { return _stringLength; }
			set
			{
				if (!_stringLength.Equals(value))
				{
					_stringLength = value;
					OnPropertyChanged("StringLength");
				}
			}
		}
		[StringLength(100, MinimumLength = 1)]
		public virtual string StringLengthWithMinimum 
		{
			get { return _stringLengthWithMinimum; }
			set
			{
				if (!_stringLengthWithMinimum.Equals(value))
				{
					_stringLengthWithMinimum = value;
					OnPropertyChanged("StringLengthWithMinimum");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	public  partial class ClientRenamedModel : INotifyPropertyChanged
	{
		private string _clientJsonName;
		private string _clientDataMemberName;
		public virtual string ClientJsonName 
		{
			get { return _clientJsonName; }
			set
			{
				if (!_clientJsonName.Equals(value))
				{
					_clientJsonName = value;
					OnPropertyChanged("ClientJsonName");
				}
			}
		}
		public virtual string ClientDataMemberName 
		{
			get { return _clientDataMemberName; }
			set
			{
				if (!_clientDataMemberName.Equals(value))
				{
					_clientDataMemberName = value;
					OnPropertyChanged("ClientDataMemberName");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	public  partial class ModelWithIgnoredProperties : INotifyPropertyChanged
	{
		private string _notIgnored;
		public virtual string NotIgnored 
		{
			get { return _notIgnored; }
			set
			{
				if (!_notIgnored.Equals(value))
				{
					_notIgnored = value;
					OnPropertyChanged("NotIgnored");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	public  partial class DataContractModel : INotifyPropertyChanged
	{
		private string _hasDataMember;
		public virtual string HasDataMember 
		{
			get { return _hasDataMember; }
			set
			{
				if (!_hasDataMember.Equals(value))
				{
					_hasDataMember = value;
					OnPropertyChanged("HasDataMember");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	public  partial class DataAnnotationsModel : INotifyPropertyChanged
	{
		private DateTime _dateTime;
		private DateTimeOffset _dateTimeOffset;
		public virtual DateTime DateTime 
		{
			get { return _dateTime; }
			set
			{
				if (!_dateTime.Equals(value))
				{
					_dateTime = value;
					OnPropertyChanged("DateTime");
				}
			}
		}
		public virtual DateTimeOffset DateTimeOffset 
		{
			get { return _dateTimeOffset; }
			set
			{
				if (!_dateTimeOffset.Equals(value))
				{
					_dateTimeOffset = value;
					OnPropertyChanged("DateTimeOffset");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	public  partial class AllDataTypesModel : INotifyPropertyChanged
	{
		private byte _byte;
		private sbyte _sbyte;
		private short _short;
		private ushort _ushort;
		private int _int;
		private uint _uint;
		private long _long;
		private ulong _ulong;
		private float _float;
		private double _double;
		private decimal _decimal;
		private char _char;
		private string _string;
		private bool _bool;
		private DateTime _dateTime;
		private TimeSpan _timeSpan;
		private DateTimeOffset _dateTimeOffset;
		private Guid _guid;
		private int? _nullableInt;
		private double? _nullableDouble;
		private DateTime? _nullableDateTime;
		private Guid? _nullableGuid;
		public virtual byte Byte 
		{
			get { return _byte; }
			set
			{
				if (!_byte.Equals(value))
				{
					_byte = value;
					OnPropertyChanged("Byte");
				}
			}
		}
		public virtual sbyte Sbyte 
		{
			get { return _sbyte; }
			set
			{
				if (!_sbyte.Equals(value))
				{
					_sbyte = value;
					OnPropertyChanged("Sbyte");
				}
			}
		}
		public virtual short Short 
		{
			get { return _short; }
			set
			{
				if (!_short.Equals(value))
				{
					_short = value;
					OnPropertyChanged("Short");
				}
			}
		}
		public virtual ushort Ushort 
		{
			get { return _ushort; }
			set
			{
				if (!_ushort.Equals(value))
				{
					_ushort = value;
					OnPropertyChanged("Ushort");
				}
			}
		}
		public virtual int Int 
		{
			get { return _int; }
			set
			{
				if (!_int.Equals(value))
				{
					_int = value;
					OnPropertyChanged("Int");
				}
			}
		}
		public virtual uint Uint 
		{
			get { return _uint; }
			set
			{
				if (!_uint.Equals(value))
				{
					_uint = value;
					OnPropertyChanged("Uint");
				}
			}
		}
		public virtual long Long 
		{
			get { return _long; }
			set
			{
				if (!_long.Equals(value))
				{
					_long = value;
					OnPropertyChanged("Long");
				}
			}
		}
		public virtual ulong Ulong 
		{
			get { return _ulong; }
			set
			{
				if (!_ulong.Equals(value))
				{
					_ulong = value;
					OnPropertyChanged("Ulong");
				}
			}
		}
		public virtual float Float 
		{
			get { return _float; }
			set
			{
				if (!_float.Equals(value))
				{
					_float = value;
					OnPropertyChanged("Float");
				}
			}
		}
		public virtual double Double 
		{
			get { return _double; }
			set
			{
				if (!_double.Equals(value))
				{
					_double = value;
					OnPropertyChanged("Double");
				}
			}
		}
		public virtual decimal Decimal 
		{
			get { return _decimal; }
			set
			{
				if (!_decimal.Equals(value))
				{
					_decimal = value;
					OnPropertyChanged("Decimal");
				}
			}
		}
		public virtual char Char 
		{
			get { return _char; }
			set
			{
				if (!_char.Equals(value))
				{
					_char = value;
					OnPropertyChanged("Char");
				}
			}
		}
		public virtual string String 
		{
			get { return _string; }
			set
			{
				if (!_string.Equals(value))
				{
					_string = value;
					OnPropertyChanged("String");
				}
			}
		}
		public virtual bool Bool 
		{
			get { return _bool; }
			set
			{
				if (!_bool.Equals(value))
				{
					_bool = value;
					OnPropertyChanged("Bool");
				}
			}
		}
		public virtual DateTime DateTime 
		{
			get { return _dateTime; }
			set
			{
				if (!_dateTime.Equals(value))
				{
					_dateTime = value;
					OnPropertyChanged("DateTime");
				}
			}
		}
		public virtual TimeSpan TimeSpan 
		{
			get { return _timeSpan; }
			set
			{
				if (!_timeSpan.Equals(value))
				{
					_timeSpan = value;
					OnPropertyChanged("TimeSpan");
				}
			}
		}
		public virtual DateTimeOffset DateTimeOffset 
		{
			get { return _dateTimeOffset; }
			set
			{
				if (!_dateTimeOffset.Equals(value))
				{
					_dateTimeOffset = value;
					OnPropertyChanged("DateTimeOffset");
				}
			}
		}
		public virtual Guid Guid 
		{
			get { return _guid; }
			set
			{
				if (!_guid.Equals(value))
				{
					_guid = value;
					OnPropertyChanged("Guid");
				}
			}
		}
		public virtual int? NullableInt 
		{
			get { return _nullableInt; }
			set
			{
				if (!_nullableInt.Equals(value))
				{
					_nullableInt = value;
					OnPropertyChanged("NullableInt");
				}
			}
		}
		public virtual double? NullableDouble 
		{
			get { return _nullableDouble; }
			set
			{
				if (!_nullableDouble.Equals(value))
				{
					_nullableDouble = value;
					OnPropertyChanged("NullableDouble");
				}
			}
		}
		public virtual DateTime? NullableDateTime 
		{
			get { return _nullableDateTime; }
			set
			{
				if (!_nullableDateTime.Equals(value))
				{
					_nullableDateTime = value;
					OnPropertyChanged("NullableDateTime");
				}
			}
		}
		public virtual Guid? NullableGuid 
		{
			get { return _nullableGuid; }
			set
			{
				if (!_nullableGuid.Equals(value))
				{
					_nullableGuid = value;
					OnPropertyChanged("NullableGuid");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	/// <summary>
	/// Struct documentation
	/// </summary>
	public  partial struct MyPointStruct : INotifyPropertyChanged
	{
		private int _x;
		public  int X 
		{
			get { return _x; }
			set
			{
				if (!_x.Equals(value))
				{
					_x = value;
					OnPropertyChanged("X");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	public  partial class MyTask : INotifyPropertyChanged
	{
		private string _name;
		private MyTask _parenTask;
		public virtual string Name 
		{
			get { return _name; }
			set
			{
				if (!_name.Equals(value))
				{
					_name = value;
					OnPropertyChanged("Name");
				}
			}
		}
		public virtual MyTask ParenTask 
		{
			get { return _parenTask; }
			set
			{
				if (!_parenTask.Equals(value))
				{
					_parenTask = value;
					OnPropertyChanged("ParenTask");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	public  partial class NestedArrayFilter : INotifyPropertyChanged
	{
		private string _data;
		private List<int> _intList;
		private EnumModel[] _enumArray;
		public virtual string Data 
		{
			get { return _data; }
			set
			{
				if (!_data.Equals(value))
				{
					_data = value;
					OnPropertyChanged("Data");
				}
			}
		}
		public virtual List<int> IntList 
		{
			get { return _intList; }
			set
			{
				if (!_intList.Equals(value))
				{
					_intList = value;
					OnPropertyChanged("IntList");
				}
			}
		}
		public virtual EnumModel[] EnumArray 
		{
			get { return _enumArray; }
			set
			{
				if (!_enumArray.Equals(value))
				{
					_enumArray = value;
					OnPropertyChanged("EnumArray");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	public enum DataContractEnumModel
	{
		HasEnumMember = 0,
	}
	/// <summary>
	/// Enum documentation
	/// </summary>
	public enum EnumModel
	{
		/// <summary>
		/// Enum value documentation
		/// </summary>
		Value1 = 1024,
		Value2 = 1025,
	}
	public interface ITestPutMethodClient : Demo.Client.GenerateModel.WebApiProxy.IControllerClient
	{
		Task<User> PutProxyParameterSourceAsync(User user);
		Task PutVoidAsync(User user);
		Task PutTaskAsync(User user);
		Task PutResponseTypeVoidAsync(int id, User user);
	}
	public class TestPutMethodClient : Demo.Client.GenerateModel.WebApiProxy.ControllerClient, ITestPutMethodClient
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
	public interface ITestMetadataClient : Demo.Client.GenerateModel.WebApiProxy.IControllerClient
	{
		Task<MetadataModel> GetMetadataModelAsync();
		Task<ClientRenamedModel> GetRenamedModelAsync();
	}
	public class TestMetadataClient : Demo.Client.GenerateModel.WebApiProxy.ControllerClient, ITestMetadataClient
	{
		public TestMetadataClient(Uri baseUrl) : base(baseUrl)
		{ }
		public Task<MetadataModel> GetMetadataModelAsync()
		{
			var url = "api/TestMetadata/GetMetadataModel";

			return GetAsync<MetadataModel>(url);
		}
		public Task<ClientRenamedModel> GetRenamedModelAsync()
		{
			var url = "api/TestMetadata/GetServerRenamedModel";

			return GetAsync<ClientRenamedModel>(url);
		}
	}
	public interface ITestPostMethodClient : Demo.Client.GenerateModel.WebApiProxy.IControllerClient
	{
		Task PostXAsync(int x);
		Task<int> PostIdAsync(int id, string str);
	}
	public class TestPostMethodClient : Demo.Client.GenerateModel.WebApiProxy.ControllerClient, ITestPostMethodClient
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
	public interface IIgnoreModelClient : Demo.Client.GenerateModel.WebApiProxy.IControllerClient
	{
		Task<IgnoredModel> GetIgnoredModelAsync();
		Task<ModelWithIgnoredProperties> GetModelWithIgnoredPropertiesAsync();
		Task<DataContractModel> GetDataContractModelAsync();
		Task<DataContractEnumModel> GetDataContractEnumModelAsync();
	}
	public class IgnoreModelClient : Demo.Client.GenerateModel.WebApiProxy.ControllerClient, IIgnoreModelClient
	{
		public IgnoreModelClient(Uri baseUrl) : base(baseUrl)
		{ }
		public Task<IgnoredModel> GetIgnoredModelAsync()
		{
			var url = "api/IgnoreModel/GetIgnoredModel";

			return GetAsync<IgnoredModel>(url);
		}
		public Task<ModelWithIgnoredProperties> GetModelWithIgnoredPropertiesAsync()
		{
			var url = "api/IgnoreModel/GetModelWithIgnoredProperties";

			return GetAsync<ModelWithIgnoredProperties>(url);
		}
		public Task<DataContractModel> GetDataContractModelAsync()
		{
			var url = "api/IgnoreModel/GetDataContractModel";

			return GetAsync<DataContractModel>(url);
		}
		public Task<DataContractEnumModel> GetDataContractEnumModelAsync()
		{
			var url = "api/IgnoreModel/GetDataContractEnumModel";

			return GetAsync<DataContractEnumModel>(url);
		}
	}
	/// <summary>
	/// Controller documentation
	/// multiline
	/// </summary>
	public interface ITestDocumentationClient : Demo.Client.GenerateModel.WebApiProxy.IControllerClient
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
	public class TestDocumentationClient : Demo.Client.GenerateModel.WebApiProxy.ControllerClient, ITestDocumentationClient
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
	public interface ITestDeleteMethodClient : Demo.Client.GenerateModel.WebApiProxy.IControllerClient
	{
		Task DeleteAsync(int id);
		Task<int> DeleteArrayAsync(string[] codes);
		Task<DateTime> DeleteDateTimeAsync(DateTime dateTime);
		Task<DateTimeOffset> DeleteDateTimeOffsetAsync(DateTimeOffset offset);
		Task<DataAnnotationsModel> DeleteDataAnnotationsModelAsync(DataAnnotationsModel model);
	}
	public class TestDeleteMethodClient : Demo.Client.GenerateModel.WebApiProxy.ControllerClient, ITestDeleteMethodClient
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

			url = AppendParameter(url, "DateTime", model.DateTime, "{0:o}");
			url = AppendParameter(url, "DateTimeOffset", model.DateTimeOffset, "{0:o}");

			return DeleteAsync<DataAnnotationsModel>(url);
		}
	}
	public interface ITestGetMethodClient : Demo.Client.GenerateModel.WebApiProxy.IControllerClient
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
	public class TestGetMethodClient : Demo.Client.GenerateModel.WebApiProxy.ControllerClient, ITestGetMethodClient
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

			url = AppendParameter(url, "Name", u.Name);
			url = AppendParameter(url, "Id", u.Id);
			url = AppendParameter(url, "Code", u.Code);

			return GetAsync<User>(url);
		}
		public Task<string> GetWithTwoDifferentParamAsync(string str, User u)
		{
			var url = "api/TestGetMethod/GetWithTwoDiffenentStringParam";

			url = AppendParameter(url, "str", str);

			url = AppendParameter(url, "Name", u.Name);
			url = AppendParameter(url, "Id", u.Id);
			url = AppendParameter(url, "Code", u.Code);

			return GetAsync<string>(url);
		}
		public Task<NestedArrayFilter> GetWithNestedArrayParamAsync(string str, NestedArrayFilter filter)
		{
			var url = "api/TestGetMethod/GetWithNestedArrayParam";

			url = AppendParameter(url, "str", str);

			url = AppendParameter(url, "Data", filter.Data);
			foreach (var item in filter.IntList)
			{
				url = AppendParameter(url, "IntList", item);
			}
			foreach (var item in filter.EnumArray)
			{
				url = AppendParameter(url, "EnumArray", item);
			}

			return GetAsync<NestedArrayFilter>(url);
		}
		public Task<string> GetWithTwoCustomParamAsync(AllDataTypesModel x, User u, int intParam)
		{
			var url = "api/TestGetMethod/GetWithTwoCustomParam";

			url = AppendParameter(url, "x.Byte", x.Byte);
			url = AppendParameter(url, "x.Sbyte", x.Sbyte);
			url = AppendParameter(url, "x.Short", x.Short);
			url = AppendParameter(url, "x.Ushort", x.Ushort);
			url = AppendParameter(url, "x.Int", x.Int);
			url = AppendParameter(url, "x.Uint", x.Uint);
			url = AppendParameter(url, "x.Long", x.Long);
			url = AppendParameter(url, "x.Ulong", x.Ulong);
			url = AppendParameter(url, "x.Float", x.Float);
			url = AppendParameter(url, "x.Double", x.Double);
			url = AppendParameter(url, "x.Decimal", x.Decimal);
			url = AppendParameter(url, "x.Char", x.Char);
			url = AppendParameter(url, "x.String", x.String);
			url = AppendParameter(url, "x.Bool", x.Bool);
			url = AppendParameter(url, "x.DateTime", x.DateTime);
			url = AppendParameter(url, "x.TimeSpan", x.TimeSpan);
			url = AppendParameter(url, "x.DateTimeOffset", x.DateTimeOffset);
			url = AppendParameter(url, "x.Guid", x.Guid);
			url = AppendParameter(url, "x.NullableInt", x.NullableInt);
			url = AppendParameter(url, "x.NullableDouble", x.NullableDouble);
			url = AppendParameter(url, "x.NullableDateTime", x.NullableDateTime);
			url = AppendParameter(url, "x.NullableGuid", x.NullableGuid);

			url = AppendParameter(url, "u.Name", u.Name);
			url = AppendParameter(url, "u.Id", u.Id);
			url = AppendParameter(url, "u.Code", u.Code);

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
