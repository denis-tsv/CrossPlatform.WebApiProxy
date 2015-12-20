using System.Web.Http;
using Demo.Server.WebHost.Areas.HelpPage.ModelDescriptions;
using Demo.Server.WebHost.Areas.HelpPage.Models;
using WebApiProxy.Common.DataAnnotations;

namespace Demo.Server.WebHost.Areas.HelpPage.Controllers
{
    [ProxyIgnore]
    [RoutePrefix("api/HelpApi")]
    public class HelpApiController : ApiController
    {
        [Route("GetApi")]
        public HelpPageApiModel GetApi(string apiId)
        {
            HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
            return apiModel;
        }

        [Route("GetResourceModel")]
        public ModelDescription GetResourceModel(string modelName)
        {
            ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
            ModelDescription modelDescription;
            var res = modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription);
            return modelDescription;
        }
    }
}
