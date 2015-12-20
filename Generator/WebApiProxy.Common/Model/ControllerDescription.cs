using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class ControllerDescription : BaseDescription
    {
        public List<MethodDescription> MethodDescriptions { get; set; } = new List<MethodDescription>();
    }
}
