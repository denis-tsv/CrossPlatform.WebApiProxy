using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace Demo.Server.WebHost.Models
{
    [DataContract(Name = "ClientRenamedModel")]
    public class ServerRenamedModel
    {
        [JsonProperty("ClientJsonName")]
        [DataMember]//DataMember used only for generating on client
        public string ServerJsonName { get; set; }

        [DataMember(Name = "ClientDataMemberName")]
        public string ServerDataMemberName { get; set; }
    }
}