using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class ControllerDescription : BaseDescription
    {
        public List<MethodDescription> MethodDescription { get; set; }
    }
}
