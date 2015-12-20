using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiProxy.Server.MetadataGenerator
{
    public class WebApiDescriptionHandler : DelegatingHandler
    {
        private readonly HttpConfiguration _configuration;

        public WebApiDescriptionHandler(HttpConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var metadataGenerator = new WebApiDescriptionGenerator(_configuration);
            var metadata = metadataGenerator.GenerateMetadata(request);
            var responce = request.CreateResponse(System.Net.HttpStatusCode.OK, metadata);
            return Task.FromResult(responce);
        }
    }

   
}
