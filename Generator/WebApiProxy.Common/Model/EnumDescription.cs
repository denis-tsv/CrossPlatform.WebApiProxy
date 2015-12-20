using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class EnumDescription
    {
        public string Name { get; set; }

        public List<EnumPropertyDescription> PropertyDescriptions { get; set; }
    }

    public class EnumPropertyDescription
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
