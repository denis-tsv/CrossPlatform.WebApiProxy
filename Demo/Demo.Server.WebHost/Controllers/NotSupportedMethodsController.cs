using System.Collections.Generic;
using System.Web.Http;
using Demo.Model;
using WebApiProxy.Common.DataAnnotations;

namespace Demo.Server.WebHost.Controllers
{
    [RoutePrefix("api/TestGetMethod")]
    [ProxyIgnore]
    public class NotSupportedMethodsController : TestBaseController
    {
        [Route("GetWithCustomArrayParam")]
        public string GetWithCustomArrayParam(User[] users)
        {
            return null;
        }

        [Route("GetWithTwoCustomArrayParam")]
        public string GetWithTwoCustomArrayParam(User[] users, List<User> otherUsers)
        {
            return null;
        }

        [Route("GetByDictionaryParam")]
        public string GetByDictionaryParam(Dictionary<string, User> users)
        {
            return null;
        }

    }
}
