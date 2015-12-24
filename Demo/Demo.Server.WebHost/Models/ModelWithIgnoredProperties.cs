using System.Runtime.Serialization;
using System.Web.Http.Description;
using System.Xml.Serialization;
using Newtonsoft.Json;
using WebApiProxy.Common.DataAnnotations;

namespace Demo.Server.WebHost.Models
{
    public class ModelWithIgnoredProperties
    {
        [ProxyIgnore]
        public string ProxyIgnore { get; set; }
        [JsonIgnore]
        public string JsonIgnore { get; set; }
        [XmlIgnore]
        public string XmlIgnore { get; set; }
        [IgnoreDataMember]
        public string IgnoreDataMember { get; set; }
        [ApiExplorerSettings(IgnoreApi = true)]
        public string ApiExplorerSettings { get; set; }

        public string NotIgnored { get; set; }

    }
}
