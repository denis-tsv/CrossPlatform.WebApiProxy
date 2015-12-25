using Newtonsoft.Json;
using WebApiProxy.Common.DataAnnotations;

namespace Demo.Server.WebHost.Models
{
    [ProxyModel("ClientJsonRenamedModel")]
    public class ServerJsonRenamedModel
    {
        [JsonProperty("ClientJsonName")]
        public string ServerJsonName { get; set; }
    }
}