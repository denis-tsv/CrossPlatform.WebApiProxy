using System.Collections.Generic;
using System.Web.Http;
using WebApiProxy.Common.DataAnnotations;

namespace Demo.Server.WebHost.Controllers
{
    [RoutePrefix("api/IgnoreMethod")]
    public class IgnoreMethodController : ApiController
    {
        [ProxyIgnore]
        [Route("Values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [NonAction]
        [Route("NoAction")]
        public string GetNoAction()
        {
            return "No action";
        }
    }
}
