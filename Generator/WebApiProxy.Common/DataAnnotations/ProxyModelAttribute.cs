using System;

namespace WebApiProxy.Common.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
    public class ProxyModelAttribute : Attribute
    {
        public ProxyModelAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
