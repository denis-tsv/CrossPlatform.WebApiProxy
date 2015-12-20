using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class MethodDescription
    {
        public string HttpMethod { get; set; }

        public string Name { get; set; }

        public string RelativePath { get; set; }

        public List<ParameterDescription> UrlParameterDescriptions { get; set; }

        public ParameterDescription BodyParameterDescription { get; set; }

        public string Documentation { get; set; }

        public string ReturnType { get; set; }
    }
}
