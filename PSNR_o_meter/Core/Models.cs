using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSNR_o_meter
{
    public class PictureMetric
    {
        public string MetricsName { get; private set; }
        public double Value { get; private set; }

        public PictureMetric(string name, double value)
        {
            MetricsName = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{MetricsName} = {Value}";
        }
    }
}
