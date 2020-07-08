using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSNR_o_meter.Core
{
    public class Calculator
    {
        byte[,] mapLeft, mapRight;

        public Calculator() { }
        public Calculator(Bitmap left, Bitmap right)
        {
            mapLeft = Utils.RGBtoGrayscale(left);
            mapRight = Utils.RGBtoGrayscale(right);
        }

        /// <summary>
        /// Changes one of pictures in class instance. Use this method to save your time 
        /// not grayscaling unchanged image one more time.
        /// </summary>
        /// <param name="bitmap">New image to be measured</param>
        /// <param name="isLeft"><see langword="true""/> if it is left image, <see langword="false""/> otherwise</param>
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
        /// <returns>
        /// Dictionary, which contains all obtained measures in name-value relations
        /// </returns>
        public List<PictureMetric> CalculatePSRN(PictureMeter meter)
        {
            meter.MeterPictures(mapLeft, mapRight);
            return meter.Metrics;
        }
    }
}
