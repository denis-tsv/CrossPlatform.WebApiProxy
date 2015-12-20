using System;

namespace WebApiProxy.Common.DataAnnotations
{
    public class ProxyFormatAttribute : Attribute
    {
        public ProxyFormatAttribute(string stringFormat)
        {
            StringFormat = stringFormat;
        }

        public string StringFormat { get; }
    }
}
