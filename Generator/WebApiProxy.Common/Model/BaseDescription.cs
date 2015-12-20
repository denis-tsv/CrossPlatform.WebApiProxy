using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProxy.Common.Model
{
    public abstract class BaseDescription
    {
        public string Name { get; set; }

        public string Documentation { get; set; }
    }
}
