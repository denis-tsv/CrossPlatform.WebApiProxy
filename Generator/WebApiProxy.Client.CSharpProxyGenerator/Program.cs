using System;
using System.IO;
using System.Net.Http;
using System.Xml.Serialization;
using WebApiProxy.Common.Model;

namespace WebApiProxy.Client.CSharpProxyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
                throw new Exception("Generator requires 2 arguments: path to config and path to source code");

            var configurationPath = args[0];
            var srcPath = args[1];

            var xmlSerializer = new XmlSerializer(typeof (ProxyGeneratorConfiguration));
            var stream = File.OpenRead(configurationPath);
            var configuration = (ProxyGeneratorConfiguration)xmlSerializer.Deserialize(stream);
            stream.Close();

            var metadata = LoadMetadata(configuration.DescriptionUrl); 
            
            var codeGenerator = new CSharpProxyGenerator(configuration, metadata);
            var code = codeGenerator.Generate();

            File.WriteAllText(srcPath, code);
        }

        private static WebApiDescription LoadMetadata(string apiMetadataUrl)
        {   
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(apiMetadataUrl).Result;
                response.EnsureSuccessStatusCode();
                var metadata = response.Content.ReadAsAsync<WebApiDescription>().Result;
                return metadata;
            }
        }
    }
}
