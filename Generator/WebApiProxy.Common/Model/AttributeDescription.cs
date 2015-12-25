using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class NameValue
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class AttributeDescription
    {
        public string Name { get; set; }
        public List<string> ConstructorParameters { get; set; } = new List<string>();
        public List<NameValue> Properties { get; set; } = new List<NameValue>();
    }
}
