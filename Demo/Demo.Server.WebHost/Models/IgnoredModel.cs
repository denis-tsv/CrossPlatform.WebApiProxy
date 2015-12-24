using WebApiProxy.Common.DataAnnotations;

namespace Demo.Server.WebHost.Models
{
    [ProxyIgnore]
    public class IgnoredModel
    {
        public string SomeProperty { get; set; }
    }
}
