﻿using System.Web.Http;
using Demo.Server.WebHost.Models;

namespace Demo.Server.WebHost.Controllers
{
    [RoutePrefix("api/TestMetadata")]
    public class TestMetadataController : ApiController
    {
        [Route("GetMetadataModel")]
        public MetadataModel GetMetadataModel()
        {
            return new MetadataModel();
        }

        [Route("GetDataContractRenamedModel")]
        public ServerDataContractRenamedModel GetDataContractRenamedModel()
        {
            return new ServerDataContractRenamedModel();
        }

        [Route("GetJsonRenamedModel")]
        public ServerJsonRenamedModel GetJsonRenamedModel()
        {
            return new ServerJsonRenamedModel();
        }
    }
}