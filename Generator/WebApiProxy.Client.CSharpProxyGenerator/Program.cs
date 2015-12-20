using System.IO;
using System.Net.Http;
using WebApiProxy.Common.Model;

namespace WebApiProxy.Client.CSharpProxyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var asm = typeof (Configuration).Assembly;

            var configuration = new Configuration
            {
                GenerateModel = true,
                Namespace = "Proxy",
                Ctor = "(Uri baseUrl) : base(baseUrl)"
            };

            var metadata = LoadMetadata("http://localhost:36761/api/meta"); //demo
            //var metadata = LoadMetadata("http://localhost:54186/api/meta"); //SCS

            //var metadata = LoadMetadata("http://localhost:2112/api/meta"); //prism

            var codeGenerator = new CSharpProxyGenerator();
            var code = codeGenerator.Generate(configuration, metadata);

            File.WriteAllText(@"d:\code.cs", code);
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
