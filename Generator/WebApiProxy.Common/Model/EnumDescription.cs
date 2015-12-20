using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class EnumDescription : BaseDescription
    {
        public List<EnumPropertyDescription> PropertyDescriptions { get; set; }
    }
}
