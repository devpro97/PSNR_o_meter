using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace PSNR_o_meter.Core
{
    public abstract class PictureMeter
    {
        public virtual List<PictureMetric> Metrics { get; set; }
        public abstract void MeterPictures(byte[,] left, byte[,] right);
    }

    public class PSNRmetric : PictureMeter
    {
        public PSNRmetric() { }

        public override void MeterPictures(byte[,] left, byte[,] right)
        {
            if    (left.GetLength(0) != right.GetLength(0) 
                || left.GetLength(1) != right.GetLength(1))
            {
                throw new ArgumentException($"Wrong pictures size! {left.GetLength(0)}х{left.GetLength(1)} vs {right.GetLength(0)}х{right.GetLength(1)} px");
            }

            var summsquare = 0f;
            var max = 0f;

            for (int i = 0; i < left.GetLength(0); i++)
            {
                for (int j = 0; j < left.GetLength(1); j++)
                {
                    var x = left[i, j];
                    var y = right[i, j];
                    summsquare += (float)Math.Pow((x - y), 2);
                    if (x > max)
                        max = x;
                }
            }

            var msq = summsquare / left.Length;
            var psnr = 10 * (float)Math.Log10(max * max / msq);

            this.Metrics = new List<PictureMetric>(2);
            this.Metrics.Add(new PictureMetric("PSNR", psnr));
            this.Metrics.Add(new PictureMetric("Standard deviation", msq));
        }
    }
    //    public class PictureMetricsBitwise : PictureMetrics //todo
}
