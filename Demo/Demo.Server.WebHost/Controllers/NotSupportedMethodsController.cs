using System.Collections.Generic;
using System.Web.Http;
using Demo.Model;
using WebApiProxy.Common.DataAnnotations;

namespace Demo.Server.WebHost.Controllers
{
    [RoutePrefix("api/TestGetMethod")]
    [ProxyIgnore]
    public class NotSupportedMethodsController : ApiController
    {
        [Route("GetWithCustomArrayParam")]
        public string GetWithCustomArrayParam([FromUri]User[] users)
        {
            return null;
        }

        [Route("GetWithTwoCustomArrayParam")]
        public string GetWithTwoCustomArrayParam([FromUri]User[] users, [FromUri]List<User> otherUsers)
        {
            return null;
        }

        [Route("GetByDictionaryParam")]
        public string GetByDictionaryParam([FromUri]Dictionary<string, User> users)
        {
            return null;
        }

    }
}
