using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using WebApiProxy.Common.DataAnnotations;
using WebApiProxy.Common.Model;

namespace WebApiProxy.Server.MetadataGenerator
{
    public class WebApiDescriptionGenerator
    {
        private readonly HttpConfiguration _configuration;
        private readonly IDocumentationProvider _documentationProvider;
        private readonly IModelDocumentationProvider _modelDocumentationProvider;
        private readonly Dictionary<string, ModelDescription> _modelDescriptions = new Dictionary<string, ModelDescription>();
        private readonly Dictionary<string, EnumDescription> _enumDescriptions = new Dictionary<string, EnumDescription>();
        private readonly Dictionary<string, string> _knownTypes = new Dictionary<string, string>
        {
            ["System.Boolean"] = "bool",
            ["System.Byte"] = "byte",
            ["System.SByte"] = "sbyte",
            ["System.Char"] = "char",

            ["System.Decimal"] = "decimal",
            ["System.Double"] = "double",
            ["System.Single"] = "float",

            ["System.Int32"] = "int",
            ["System.UInt32"] = "uint",
            ["System.Int64"] = "long",
            ["System.UInt64"] = "ulong",
            ["System.Int16"] = "short",
            ["System.UInt16"] = "ushort",

            ["System.String"] = "string",
            ["System.Void"] = "void",
            
        };
        public WebApiDescriptionGenerator(HttpConfiguration configuration)
        {
            _configuration = configuration;

            var docProvider = new AggregateXmlDocumentationProvider();
            _documentationProvider = _configuration.Services.GetDocumentationProvider() ?? docProvider;
            _modelDocumentationProvider = docProvider;
        }

        public WebApiDescription GenerateMetadata(HttpRequestMessage request)
        {
            var descriptions = _configuration.Services.GetApiExplorer().ApiDescriptions;
            
            var apiGroups = descriptions.Where(a => 
                    !a.RelativePath.Contains("Swagger") && 
                    !a.RelativePath.Contains("docs") &&
                    !a.ActionDescriptor.GetCustomAttributes<ProxyIgnoreAttribute>().Any() && 
                    !a.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<ProxyIgnoreAttribute>().Any())
                .ToLookup(a => a.ActionDescriptor.ControllerDescriptor);

            var controllerDefinitions = new List<ControllerDescription>();
            foreach (var apiGroup in apiGroups)
            {
                var definition = new ControllerDescription
                {
                    Name = apiGroup.Key.ControllerName,
                    Documentation = _documentationProvider.GetDocumentation(apiGroup.Key),
                    MethodDescriptions = GetMethodDefinitions(apiGroup)
                };

                controllerDefinitions.Add(definition);
            }

            var metadata = new WebApiDescription
            {
                ControllerDescriptions = controllerDefinitions,
                ModelDescriptions = _modelDescriptions.Values.ToList(),
                EnumDescriptions = _enumDescriptions.Values.ToList(),
            };

            // TODO Metadata [Required], [MaxLength]

            return metadata;
        }

        private List<MethodDescription> GetMethodDefinitions(IGrouping<HttpControllerDescriptor, ApiDescription> apiGroup)
        {
            var result = new List<MethodDescription>();

            foreach (var apiDescription in apiGroup)
            {
                var methodDescription = new MethodDescription
                {
                    Name = apiDescription.ActionDescriptor.ActionName,
                    BodyParameterDescription = GetBodyParameter(apiDescription),
                    UrlParameterDescriptions = GetUrlParameters(apiDescription),
                    RelativePath = apiDescription.RelativePath,
                    Documentation = _documentationProvider.GetDocumentation(apiDescription.ActionDescriptor),
                    ReturnType = GetMethodReturnValue(apiDescription.ResponseDescription),
                    ReturnDocumentation = _documentationProvider.GetResponseDocumentation(apiDescription.ActionDescriptor),
                    HttpMethod = apiDescription.HttpMethod.Method,
                };

                result.Add(methodDescription);
            }

            return result;
        }

        private string GetMethodReturnValue(ResponseDescription responseDescription)
        {
            var returnType = responseDescription.ResponseType ?? responseDescription.DeclaredType;

            
            if (returnType == null) //returns void
            {
                return null;
            }

            if (returnType == typeof (void)) // [ResponseType(typeof(void))]
            {
                return null;
            }

            //if (returnType == typeof (HttpResponseMessage) || returnType == typeof (IHttpActionResult))
            //{
                
            //}

            return ParseTypeName(returnType);
        }

        private List<ParameterDescription> GetUrlParameters(ApiDescription apiDescription)
        {
            var result = new List<ParameterDescription>();

            foreach (var parameterDescription in apiDescription.ParameterDescriptions
                .Where(p => p.Source == ApiParameterSource.FromUri))
            {
                var description = new ParameterDescription
                {
                    Name = parameterDescription.ParameterDescriptor.ParameterName,
                    Type = ParseTypeName(parameterDescription.ParameterDescriptor.ParameterType),
                    Documentation = _documentationProvider.GetDocumentation(parameterDescription.ParameterDescriptor),
                    IsOptional = parameterDescription.ParameterDescriptor.IsOptional,
                };

                if (parameterDescription.ParameterDescriptor.IsOptional)
                {
                    description.DefaultValue = Convert.ToString(parameterDescription.ParameterDescriptor.DefaultValue, CultureInfo.InvariantCulture);
                }
                
                var proxySource = parameterDescription.ParameterDescriptor.GetCustomAttributes<ProxySourceAttribute>();
                if (proxySource.Any())
                {
                    description.ProxySource = proxySource.Single().SourceName;
                }

                var proxyFormat = parameterDescription.ParameterDescriptor.GetCustomAttributes<ProxyFormatAttribute>();
                if (proxyFormat.Any())
                {
                    description.ProxyFormat = proxyFormat.Single().StringFormat;
                }

                result.Add(description);
            }

            return result;
        }

        private ParameterDescription GetBodyParameter(ApiDescription apiDescription)
        {
            var bodyParameter = apiDescription.ParameterDescriptions
                .FirstOrDefault(p => p.Source == ApiParameterSource.FromBody);

            if (bodyParameter == null) return null;

            var result = new ParameterDescription
            {
                Name = bodyParameter.ParameterDescriptor.ParameterName,
                Type = ParseTypeName(bodyParameter.ParameterDescriptor.ParameterType),
                Documentation = _documentationProvider.GetDocumentation(bodyParameter.ParameterDescriptor),
            };

            return result;
        }

        private string ParseTypeName(Type type)
        {
            if (_knownTypes.ContainsKey(type.FullName)) return _knownTypes[type.FullName];

            string typeName;
            if (type.IsGenericType)
            {
                typeName = ParseGenericTypeName(type);
            }
            else if (type.IsArray)
            {
                var elementTypeName = ParseTypeName(type.GetElementType());
                typeName = elementTypeName + "[]";
            }
            else
            {
                typeName = type.Name;
                AddModelDescription(type);
            }
           
            _knownTypes[type.FullName] = typeName;
            return typeName;
        }

        private void AddModelDescription(Type sourceType)
        {
            var type = sourceType;

            if (sourceType.IsArray) type = type.GetElementType();

            if (type.FullName.StartsWith("System.")) return;
            if (_modelDescriptions.ContainsKey(type.FullName)) return;

            Debug.Assert(!type.IsGenericType);

            if (type.IsEnum)
            {
                AddEnumDescription(type);
                return;
            }

            var modelDescription = new ModelDescription
            {
                Name = type.Name,
                IsAbstract = type.IsAbstract,
                IsValueType = type.IsValueType,
                Documentation = _modelDocumentationProvider.GetDocumentation(type)
            };
            _modelDescriptions.Add(type.FullName, modelDescription);

            if (type.BaseType != typeof (object) && type.BaseType != typeof (ValueType) && type.BaseType != typeof (Enum))
            {
                ParseTypeName(type.BaseType);
                modelDescription.BaseModelName = type.BaseType.Name;
            }

            var publicProperties = type.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetMethod.IsPublic && 
                            p.CanWrite && p.SetMethod.IsPublic);
            var properties = publicProperties.Where(p => p.GetCustomAttribute<ProxyIgnoreAttribute>() == null);

            var modelProperties = new List<ModelPropertyDescription>();
            foreach (var property in properties)
            {
                var modelProperty = new ModelPropertyDescription
                {
                    Name = property.Name,
                    Documentation = _modelDocumentationProvider.GetDocumentation(property),
                    Type = ParseTypeName(property.PropertyType)
                };

                var formatAttribure = property.GetCustomAttribute<ProxyFormatAttribute>();
                if (formatAttribure != null)
                {
                    modelProperty.ProxyFormat = formatAttribure.StringFormat;
                }

                modelProperties.Add(modelProperty);
            }

            modelDescription.PropertyDescriptions = modelProperties;
        }

        private void AddEnumDescription(Type type)
        {
            if (_enumDescriptions.ContainsKey(type.FullName)) return;

            var enumDesc = new EnumDescription
            {
                Name = type.Name,
                Documentation = _modelDocumentationProvider.GetDocumentation(type)
            };

            var members = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            var enumProperties = new List<EnumPropertyDescription>();
            foreach (var fieldInfo in members)
            {
                var desc = new EnumPropertyDescription
                {
                    Name = fieldInfo.Name,
                    Value = fieldInfo.GetRawConstantValue().ToString(),
                    Documentation = _modelDocumentationProvider.GetDocumentation(fieldInfo),
                };
                enumProperties.Add(desc);
            }
            enumDesc.PropertyDescriptions = enumProperties;

            _enumDescriptions[type.FullName] = enumDesc;
        }

        private string ParseGenericTypeName(Type type)
        {
            if (type.Name.StartsWith("Nullable`"))
            {
                var arg = type.GetGenericArguments().Single();
                var argName = ParseTypeName(arg);
                return argName + "?"; //TODO IsNullable
            }

            var typeNameBuilder = new StringBuilder();

            int index = type.Name.IndexOf('`');
            typeNameBuilder.Append(type.Name.Substring(0, index));

            Type[] args = type.GetGenericArguments();

            typeNameBuilder.Append("<");

            for (int i = 0; i < args.Length; i++)
            {
                var argName = ParseTypeName(args[i]);
                typeNameBuilder.Append(argName);

                if (i != args.Length - 1) typeNameBuilder.Append(", ");
            }

            typeNameBuilder.Append(">");

            return typeNameBuilder.ToString();
        }
    }
}
