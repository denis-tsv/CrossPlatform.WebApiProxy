using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class ModelDescription : BaseDescription
    {
        public bool IsValueType { get; set; }

        public bool IsAbstract { get; set; }

        public string BaseModelName { get; set; }

        public List<ModelPropertyDescription> PropertyDescriptions { get; set; }
    }
}
