using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class WebApiDescription
    {
        public List<ControllerDescription> ControllerDescriptions { get; set; } = new List<ControllerDescription>();

        public List<ModelDescription> ModelDescriptions { get; set; } = new List<ModelDescription>();

        public List<EnumDescription> EnumDescriptions { get; set; } = new List<EnumDescription>();
    }
}
