using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Demo.Server.WebHost.Models
{
    [DataContract]
    public enum DataContractEnumModel
    {
        [EnumMember]
        HasEnumMember,

        NoEnumMember

    }
}