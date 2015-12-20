using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace WebApiProxy.Server.MetadataGenerator
{
    public class AggregateXmlDocumentationProvider : IDocumentationProvider, IModelDocumentationProvider
    {
        private readonly Dictionary<string, XmlDocumentationProvider> _documentationProviders = new Dictionary<string, XmlDocumentationProvider>();

        private XmlDocumentationProvider GetDocumentationProvider(Type type)
        {
            var path = HttpContext.Current.Server.MapPath($"~/bin/{type.Assembly.GetName().Name}.xml");
            XmlDocumentationProvider result;
            if (_documentationProviders.TryGetValue(path, out result))
            {
                return result;
            }

            if (File.Exists(path))
            {
                result = new XmlDocumentationProvider(path);
            }

            _documentationProviders.Add(path, result);
            return result;
        }

        public string GetDocumentation(HttpControllerDescriptor controllerDescriptor)
        {
            var provider = GetDocumentationProvider(controllerDescriptor.ControllerType);
            return provider?.GetDocumentation(controllerDescriptor);
        }

        public string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var provider = GetDocumentationProvider(actionDescriptor.ControllerDescriptor.ControllerType);
            return provider?.GetDocumentation(actionDescriptor);
        }

        public string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            var provider = GetDocumentationProvider(parameterDescriptor.ActionDescriptor.ControllerDescriptor.ControllerType);
            return provider?.GetDocumentation(parameterDescriptor);
        }

        public string GetResponseDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var provider = GetDocumentationProvider(actionDescriptor.ControllerDescriptor.ControllerType);
            return provider?.GetResponseDocumentation(actionDescriptor);
        }

        public string GetDocumentation(MemberInfo member)
        {
            var provider = GetDocumentationProvider(member.DeclaringType);
            return provider?.GetDocumentation(member);
        }

        public string GetDocumentation(Type type)
        {
            var provider = GetDocumentationProvider(type);
            return provider?.GetDocumentation(type);
        }
    }
}
