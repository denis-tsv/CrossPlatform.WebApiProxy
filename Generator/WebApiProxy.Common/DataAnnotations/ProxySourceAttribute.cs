using System;

namespace WebApiProxy.Common.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ProxySourceAttribute : Attribute
    {
        public ProxySourceAttribute(string sourceName)
        {
            SourceName = sourceName;
        }

        public string SourceName { get; }
    }
}
