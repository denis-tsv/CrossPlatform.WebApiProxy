namespace WebApiProxy.Common.Model
{
    public class ParameterDescription : BaseDescription
    {
        public string Type { get; set; }
        
        public bool IsOptional { get; set; }

        public string DefaultValue { get; set; }

        public string ProxySource { get; set; }

        public string ProxyFormat { get; set; }
    }
}
