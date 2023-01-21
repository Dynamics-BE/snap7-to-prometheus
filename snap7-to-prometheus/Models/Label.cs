﻿namespace snap7_to_prometheus.Models
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
                    return $"{Name}=\"{ValueTag.Data}\"";
                }

            }
        }

    }
}
