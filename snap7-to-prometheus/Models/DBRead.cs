using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace snap7_to_prometheus.Models
{
    public class DBRead
    {
        public int DBNumber { get; set; }
        public int DBOffsetByte { get; set; }
        public int DBLengthByte { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
