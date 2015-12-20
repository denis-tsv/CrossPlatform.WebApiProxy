using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiProxy.Server.MetadataGenerator
{
    public class MetadataHandler : DelegatingHandler
    {
        private readonly HttpConfiguration _configuration;

        public MetadataHandler(HttpConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var metadataGenerator = new MetadataGenerator(_configuration);
            var metadata = metadataGenerator.GenerateMetadata(request);
            var responce = request.CreateResponse(System.Net.HttpStatusCode.OK, metadata);
            return Task.FromResult(responce);
        }
    }

   
}
