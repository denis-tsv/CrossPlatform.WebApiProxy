using System;
using WebApiProxy.Common.DataAnnotations;

namespace Demo.Model
{
    public class DataAnnotationsModel
    {
        [ProxyFormat("{0:o}")]
        public DateTime DateTime { get; set; }

        [ProxyFormat("{0:o}")]
        public DateTimeOffset DateTimeOffset { get; set; }

        [ProxyIgnore]
        public string IgnoredProterty { get; set; }
    }
}
