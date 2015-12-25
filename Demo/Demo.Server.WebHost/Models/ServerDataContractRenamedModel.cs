using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace Demo.Server.WebHost.Models
{
    [DataContract(Name = "ClientDataContractRenamedModel")]
    public class ServerDataContractRenamedModel
    {
        [DataMember(Name = "ClientDataMemberName")]
        public string ServerDataMemberName { get; set; }
    }
}