using System.Web.Http;
using WebApiProxy.Common.DataAnnotations;

namespace Demo.Server.WebHost.Controllers
{
    [RoutePrefix("api/TestPostMethod")]
    //[ProxyIgnore]
    public class TestPostMethodController : ApiController
    {
        [Route("PostX")]
        public void PostX([FromBody]int x)
        {
        }

        [Route("{id}/PostId")]
        public int PostId(int id, [FromBody] string str)
        {
            return id;
        }
        
    }
}
