using System.Collections.Generic;
using System.Web.Http;
using WebApiProxy.Common.DataAnnotations;

namespace Demo.Server.WebHost.Controllers
{
    [ProxyIgnore]
    public class IgnoreController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}