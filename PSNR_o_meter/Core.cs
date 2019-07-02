using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace PSNR_o_meter
{
    class Core
    {
        public class PSNRmeter
        {
            PictureMetrics meter;
            byte[,] mapLeft, mapRight;

            public PSNRmeter(Bitmap left, Bitmap right)
            {
                mapLeft = Utils.RGBtoGrayscale(left);
                mapRight = Utils.RGBtoGrayscale(right);
            }

            /// <summary>
            /// Changes one of pictures in class instance. Use this method to save your time 
            /// not grayscaling second image one more time.
            /// </summary>
            /// <param name="bitmap">New image to be measured</param>
            /// <param name="isLeft">True if it is left image, false otherwise</param>
            public void ChangePicture(Bitmap bitmap, bool isLeft)
            {
                var map = Utils.RGBtoGrayscale(bitmap);
                if (isLeft)
                {
                    mapLeft = map;
                }
                else
                {
                    mapRight = map;
                }
            }

            /// <summary>
            /// Measures pictures PSNR
            /// </summary>
            /// <returns>Dictionary, which contains all obtained meashures in name-value relations</returns>
            public Dictionary<string, float> CalculatePSRN()
            {
                meter.MeterPictures(mapLeft, mapRight);
                return meter.metrics;
            }
        }
               
        public abstract class PictureMetrics
        {
            public Dictionary<string, float> metrics = new Dictionary<string, float>(2); //because it'll often have only two meashures

            public abstract void MeterPictures(byte[,] left, byte[,] right);
        }
        public class PictureMetricsFull : PictureMetrics
        {
            public override void MeterPictures(byte[,] left, byte[,] right)
            {
                if (left.GetLength(0) != right.GetLength(0) || left.GetLength(1) != right.GetLength(1))
                {
                    throw new Exception($"Wrong pictures size! {left.GetLength(0)}х{left.GetLength(0)} vs {right.GetLength(0)}х{right.GetLength(1)} px");
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
                this.metrics.Add("PSNR", psnr);
                this.metrics.Add("Standard deviation", msq);
            }
        }
        //    public class PictureMetricsBitwise : PictureMetrics
    }
}
