using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class WebApiDescription
    {
        public List<ControllerDescription> ControllerDescriptions { get; set; }

        public List<ModelDescription> ModelDescriptions { get; set; }

        public List<EnumDescription> EnumDescriptions { get; set; }
    }
}
