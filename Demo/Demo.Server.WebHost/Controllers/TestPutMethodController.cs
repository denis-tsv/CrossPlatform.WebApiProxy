using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Demo.Model;
using WebApiProxy.Common.DataAnnotations;
using WebApiProxy.Server.MetadataGenerator;

namespace Demo.Server.WebHost.Controllers
{
    [RoutePrefix("api/TestPutMethod")]
    public class TestPutMethodController : ApiController
    {
        private PropertiesComparer<User> _userComparer = new PropertiesComparer<User>();

        #region ProxyParameterSource

        [Route("{id}/PutProxyParameterSource")]
        public User PutProxyParameterSource([ProxySource("user.Id")]int id, [FromBody]User user)
        {
            return user;
        }

        #endregion

        #region Return void

        [Route("{id}/PutVoid")]
        public void PutVoid([ProxySource("user.Id")]int id, [FromBody]User user)
        {
            var expected = new User
            {
                Id = 1,
                Name = "NAme",
                Code = "123"
            };

            if (!_userComparer.Equals(expected, user)) throw new Exception();
        }

        
        [Route("{id}/PutTask")]
        public async Task PutTask([ProxySource("user.Id")]int id, [FromBody]User user)
        {
            await Task.Delay(100);

            var expected = new User
            {
                Id = 1,
                Name = "NAme",
                Code = "123"
            };

            if (!_userComparer.Equals(expected, user)) throw new Exception();
        }

        [ResponseType(typeof(void))]
        [Route("{id}/PutResponseTypeVoid")]
        public HttpResponseMessage PutResponseTypeVoid(int id, [FromBody]User user)
        {
            var expected = new User
            {
                Id = 1,
                Name = "NAme",
                Code = "123"
            };

            if (!_userComparer.Equals(expected, user)) throw new Exception();

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        #endregion

        
        

    }
}
