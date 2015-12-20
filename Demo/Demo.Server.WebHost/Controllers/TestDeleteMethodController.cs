using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Demo.Model;
using WebApiProxy.Common.DataAnnotations;

namespace Demo.Server.WebHost.Controllers
{
    

    [RoutePrefix("api/TestDeleteMethod")]
    public class TestDeleteMethodController : ApiController
    {
        [Route("DeleteSimple")]
        public void Delete(int id)
        {
            if (id != 5) throw new Exception();
        }

        [Route("DeleteArray")]
        public int DeleteArray([ModelBinder]string[] codes)
        {
            return codes.Length;
        }

        [Route("DeleteDateTime")]
        public DateTime DeleteDateTime([ProxyFormat("{0:o}")]DateTime dateTime)
        {
            return dateTime;
        }

        [Route("DeleteDateTimeOffset")]
        public DateTimeOffset DeleteDateTimeOffset([ProxyFormat("{0:u}")]DateTimeOffset offset)
        {
            return offset;
        }

        [Route("DeleteFormatPropertyModel")]
        public DataAnnotationsModel DeleteDataAnnotationsModel([FromUri]DataAnnotationsModel model)
        {
            return model;
        }

    }
}
