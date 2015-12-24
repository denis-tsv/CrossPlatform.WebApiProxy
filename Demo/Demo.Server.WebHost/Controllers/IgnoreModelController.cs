using System.Web.Http;
using Demo.Model;
using Demo.Server.WebHost.Models;
using IgnoredModel = Demo.Server.WebHost.Models.IgnoredModel;

namespace Demo.Server.WebHost.Controllers
{
    [RoutePrefix("api/IgnoreModel")]
    public class IgnoreModelController : ApiController
    {
        [Route("GetIgnoredModel")]
        public IgnoredModel GetIgnoredModel()
        {
            return new IgnoredModel { SomeProperty = "Value"};
        }

        [Route("GetModelWithIgnoredProperties")]
        public ModelWithIgnoredProperties GetModelWithIgnoredProperties()
        {
            return new ModelWithIgnoredProperties
            {
                NotIgnored = "NotIgnored"
            };
        }

        [Route("GetDataContractModel")]
        public DataContractModel GetDataContractModel()
        {
            return new DataContractModel
            {
                HasDataMember = "1",
                NoDataMember = "2"
            };
        }
        [Route("GetDataContractEnumModel")]
        public DataContractEnumModel GetDataContractEnumModel()
        {
            return DataContractEnumModel.HasEnumMember;
        }
    }
}