using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace snap7_to_prometheus.Models
{
    public class Tag
    {
        public string Name { get; set; }
        public string MetricsName { get; set; }
        public int OffsetByte { get; set; }
        public int OffsetBit { get; set; }
        public int StringLength { get; set; }
        public string Type { get; set; }
        public object Data { get; set; }
    }
}
