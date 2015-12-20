using System.Collections.Generic;

namespace Demo.Model
{
    public class NestedArrayFilter
    {
        public string Data { get; set; }

        public List<int> IntList { get; set; }

        public EnumModel[] EnumArray { get; set; }
    }
}
