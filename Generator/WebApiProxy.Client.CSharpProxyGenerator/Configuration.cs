namespace WebApiProxy.Client.CSharpProxyGenerator
{
    public class Configuration
    {
        public string BaseAddress { get; set; }

        //TODO StringArray
        public string AdditionalNamespaces { get; set; }
        public bool GenerateModel { get; set; }
        public string Namespace { get; set; }
        public string Ctor { get; set; }
        //TODO stringArray
        public string AdditionalAssemblies { get; set; }

    }
}
