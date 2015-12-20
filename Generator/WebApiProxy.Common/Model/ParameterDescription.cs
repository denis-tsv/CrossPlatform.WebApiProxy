namespace WebApiProxy.Common.Model
{
    public class ParameterDescription
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Documentation { get; set; }

        public bool IsOptional { get; set; }

        public string DefaultValue { get; set; }

        public string ProxySource { get; set; }

        public string ProxyFormat { get; set; }
    }
}
