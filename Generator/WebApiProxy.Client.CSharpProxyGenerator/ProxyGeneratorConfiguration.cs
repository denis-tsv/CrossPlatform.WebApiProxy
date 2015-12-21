﻿namespace WebApiProxy.Client.CSharpProxyGenerator
{
    public class ProxyGeneratorConfiguration
    {
        public string DescriptionUrl { get; set; }
        public string[] AdditionalNamespaces { get; set; }
        public bool GenerateModel { get; set; }
        public string Namespace { get; set; }
        public string BaseProxyClass { get; set; }
        public string ProxyConstructor { get; set; }
        
        //public string AdditionalAssemblies { get; set; }

    }
}
