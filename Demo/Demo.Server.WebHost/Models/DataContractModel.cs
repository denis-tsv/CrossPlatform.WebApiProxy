using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Demo.Server.WebHost.Models
{
    [DataContract]
    public class DataContractModel
    {
        [DataMember]
        public string HasDataMember { get; set; }

        public string NoDataMember { get; set; }
    }
}