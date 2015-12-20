using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Model
{
    public class NestedArrayFilter
    {
        public string Data { get; set; }

        public List<int> IntList { get; set; }

        public EnumModel[] EnumArray { get; set; }
    }
}
