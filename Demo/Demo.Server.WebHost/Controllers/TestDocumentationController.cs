using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Demo.Server.WebHost.Controllers
{
    /// <summary>
    /// Controller documentation
    /// multiline
    /// </summary>
    [RoutePrefix("api/TestDocumentation")]
    public class TestDocumentationController : ApiController
    {
        /// <summary>
        /// Method documentation
        /// multiline
        /// </summary>
        /// <param name="urlParam">Url parameter documentation</param>
        /// <param name="bodyParam">Body parameter documentation</param>
        /// <returns>Return value documentation</returns>
        [Route("PostWithDocumentation")]
        public string PostWithDocumentation(string urlParam, [FromBody]string bodyParam)
        {
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        [Route("PostWithEmptyDocumentation")]
        public string PostWithEmptyDocumentation([FromBody]string doc)
        {
            return "";
        }
    }
}
