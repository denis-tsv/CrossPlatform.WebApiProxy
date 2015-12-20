using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Demo.Model;

namespace Demo.Server.WebHost.Controllers
{
    [RoutePrefix("api/TestPostMethod")]
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
