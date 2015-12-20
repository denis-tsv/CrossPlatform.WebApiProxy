using System;
using System.Reflection;

namespace WebApiProxy.Server.MetadataGenerator
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}