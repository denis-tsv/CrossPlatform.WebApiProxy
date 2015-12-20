using System.Collections.Generic;

namespace WebApiProxy.Common.Model
{
    public class MethodDescription : BaseDescription
    {
        public string HttpMethod { get; set; }

        public string RelativePath { get; set; }

        //TODO Создавать в конструкторе, тогда на клиенте можно обойтись без истеричных проверок
        public List<ParameterDescription> UrlParameterDescriptions { get; set; }

        public ParameterDescription BodyParameterDescription { get; set; }

        public string ReturnType { get; set; }

        public string ReturnDocumentation { get; set; }
    }
}
