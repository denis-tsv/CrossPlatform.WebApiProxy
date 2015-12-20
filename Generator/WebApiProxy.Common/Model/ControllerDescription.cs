using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class ControllerDescription
    {
        public string Name { get; set; }

        public string Documentation { get; set; }

        public List<MethodDescription> MethodDescription { get; set; }
    }
}
