using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AdventureWorks.UILogic.Services;

namespace AdventureWorks.UILogic.WebApiProxy
{
    public interface IControllerClient : IDisposable
    {
        HttpClient HttpClient { get; }
    }
    
    public abstract class ControllerClient : IControllerClient
    {
        private static readonly Dictionary<Type, PropertyInfo[]> PropertiesDictionary = new Dictionary<Type, PropertyInfo[]>();
        public HttpClient HttpClient { get; protected set; }

        protected ControllerClient(HttpClientHandler handler, Uri baseUri)
        {
            HttpClient = new HttpClient(handler)
            {
                BaseAddress = baseUri,
            };
        }

        #region Parameters

        protected string AppendParameter(string currentUrl, string paramName, object paramValue, string format = null)
        {
            var stringValue = GetParamString(paramName, paramValue, format);

            var separator = currentUrl.Contains("?") ? "&" : "?";
            return $"{currentUrl}{separator}{stringValue}";
        }

        public string AppendParametersFromPropertiesOnRuntime(string currentUrl, string paramName, object paramValue, bool includeParamName)
        {
            // It is possible to avoid reflection on runtime and use it during codegeneration. 
            // You can load assemblies during codegeneration and search for each unknown type (which not exists in model descriptions),
            // analyse them properties and generate the same code that we generate for known types.
            // This solution is appicable for Visual Studio plugin, bot not for command line tool. So we will use reflection on runtime :)

            var properties = GetPropertyInfos(paramValue.GetType());
            
            string paramNameStr = includeParamName
                ? $"{paramName}."
                : "";

            var propertyStrings = new List<string>();
            foreach (var property in properties)
            {
                var value = property.GetValue(paramValue);

                if (value is IEnumerable && !(value is string))
                {
                    var values = (IEnumerable) value;
                    foreach (var valueItem in values)
                    {
                        var propString = GetParamString($"{paramNameStr}{property.Name}", valueItem, null);
                        propertyStrings.Add(propString);
                    }
                }
                else
                {
                    var propString = GetParamString($"{paramNameStr}{property.Name}", value, null);
                    propertyStrings.Add(propString);
                }
            }

            var separator = currentUrl.Contains("?") ? "&" : "?";
            return $"{currentUrl}{separator}{string.Join("&", propertyStrings)}";
        }

        private PropertyInfo[] GetPropertyInfos(Type type)
        {
            
            if (PropertiesDictionary.ContainsKey(type))
            {
                return PropertiesDictionary[type];
            }
            
            var result = type.GetRuntimeProperties()
                .Where(p => p.CanRead && p.GetMethod.IsPublic &&
                            p.CanWrite && p.SetMethod.IsPublic && 
                            !p.GetMethod.IsStatic)
                .ToArray();
            PropertiesDictionary.Add(type, result);
            return result;
        }

        private string GetParamString(string name, object value, string format)
        {
            var stringValue = ConvertToString(value, format);

            return $"{name}={stringValue}";
        }

        protected string ConvertToString(object value, string format = null)
        {
           
            string stringValue;
            if (!string.IsNullOrEmpty(format))
            {
                stringValue = string.Format(format, value);
            }
            else
            {
                stringValue = Convert.ToString(value, CultureInfo.InvariantCulture);
            }

            return Uri.EscapeDataString(stringValue);
        }

        #endregion

        #region Http Methods

        protected async Task<T> GetAsync<T>(string relativeUri)
        {
            var response = await HttpClient.GetAsync(relativeUri).ConfigureAwait(false);

            //you can insert checking of HttpStatusCode.NotFound code here

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<T>().ConfigureAwait(false);
            return result;
        }

        protected async Task PostNoResultAsync(string relativeUri, object value = null)
        {
            var response = await HttpClient.PostAsJsonAsync(relativeUri, value).ConfigureAwait(false);

            //response.EnsureSuccessStatusCode();
            await response.EnsureSuccessWithValidationSupportAsync();
        }

        protected async Task<T> PostAsync<T>(string relativeUri, object value = null)
        {
            var response = await HttpClient.PostAsJsonAsync(relativeUri, value).ConfigureAwait(false);

            //response.EnsureSuccessStatusCode();
            await response.EnsureSuccessWithValidationSupportAsync();

            var result = await response.Content.ReadAsAsync<T>().ConfigureAwait(false);
            return result;
        }

        protected async Task PutNoResultAsync(string relativeUri, object value = null)
        {
            var response = await HttpClient.PutAsJsonAsync(relativeUri, value).ConfigureAwait(false);

            //response.EnsureSuccessStatusCode();
            await response.EnsureSuccessWithValidationSupportAsync();
        }

        protected async Task<T> PutAsync<T>(string relativeUri, object value = null)
        {
            var response = await HttpClient.PutAsJsonAsync(relativeUri, value).ConfigureAwait(false);

            //response.EnsureSuccessStatusCode();
            await response.EnsureSuccessWithValidationSupportAsync();

            var result = await response.Content.ReadAsAsync<T>().ConfigureAwait(false);
            return result;
        }


        protected async Task<T> DeleteAsync<T>(string relativeUri)
        {
            var response = await HttpClient.DeleteAsync(relativeUri).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<T>().ConfigureAwait(false);
            return result;
        }

        protected async Task DeleteNoResultAsync(string relativeUri)
        {
            var response = await HttpClient.DeleteAsync(relativeUri).ConfigureAwait(false);
            
            response.EnsureSuccessStatusCode();
        }

        #endregion

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }

}
