using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace snap7_to_prometheus.Models
{
    public class Configuration
    {
        public string PLCAddress { get; set; }
        public int PLCRack { get; set; }
        public int PLCSlot { get; set; }
        public int ReadRateMs { get; set; }
        public List<DBRead> DBReads { get; set; }
    }
}
