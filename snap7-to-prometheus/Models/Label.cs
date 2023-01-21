using System.Text.RegularExpressions;

namespace snap7_to_prometheus.Models
{
    public class Label
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string ValueTagName { get; set; }
        public Tag ValueTag { get; set; }

        public string FormattedMetric
        {
            get
            {
                if (ValueTag == null)
                {
                    return $"{Name}=\"{Value}\"";
                }
                else
                {
                    var dataString = Regex.Replace(ValueTag.Data.ToString(), @"[^\u0000-\u007F]+", string.Empty);
                    dataString = dataString.Replace("\0", string.Empty);
                    return $"{Name}=\"{dataString}\"";
                }

            }
        }

    }
}
