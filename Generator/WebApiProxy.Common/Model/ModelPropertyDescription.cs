using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class ModelPropertyDescription : BaseDescription
    {
        public string Type { get; set; }

        public string ProxyFormat { get; set; }

        public List<AttributeDescription> AttributeDescriptions { get; set; } = new List<AttributeDescription>();
    }
}
