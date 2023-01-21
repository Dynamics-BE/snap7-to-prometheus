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
        public ICollection<Label> Labels { get; set; }
        public string FormattedMetricsNameWithLabels { 
            get
            {
                if (Labels == null)
                {
                    return MetricsName;
                }
                else
                {
                    return $"{MetricsName}{{{String.Join(", ", Labels.Select(l => l.FormattedMetric))}}}";
                }
            }
        }
        public string FormattedTagReference
        {
            get
            {
                if (!String.IsNullOrEmpty(Name))
                {
                    return Name;
                }
                else
                {
                    return MetricsName;
                }
            }
        }
    }
}
