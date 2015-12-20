using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class MethodDescription : BaseDescription
    {
        public string HttpMethod { get; set; }

        public string RelativePath { get; set; }

        public List<ParameterDescription> UrlParameterDescriptions { get; set; } = new List<ParameterDescription>();

        public ParameterDescription BodyParameterDescription { get; set; }

        public string ReturnType { get; set; }

        public string ReturnDocumentation { get; set; }
    }
}
