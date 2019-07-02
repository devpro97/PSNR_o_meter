using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace PSNR_o_meter
{
    class Utils
    {
        public static unsafe Bitmap RGBtoGrayscaleBitmap(Bitmap bitmap)
        {
            var format = bitmap.PixelFormat;
            if (format != PixelFormat.Format32bppArgb)
            {
                throw new BadImageFormatException($"{format} is not supported by this prog!");
            }
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, format);
            byte* picStart = (byte*)data.Scan0.ToPointer();
            byte bytesPerPix = (byte)(data.Stride / data.Width);

            for (short x = 0; x < data.Width; ++x)
                for (short y = 0; y < data.Height; ++y)
                {
                    byte* pix = picStart + x * bytesPerPix + y * data.Stride;
                    byte gray = RGBToGrayscalePixel(pix);
                    pix[2] = gray;
                    pix[1] = gray;
                    pix[0] = gray;
                }
            bitmap.UnlockBits(data);

            return bitmap;
        }
        public static unsafe byte[,] RGBtoGrayscale(Bitmap bitmap)
        {
            byte[,] mapOfValues = new byte[bitmap.Width, bitmap.Height];

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            byte* picStart = (byte*)data.Scan0.ToPointer();
            byte bytesPerPix = (byte)(data.Stride / data.Width);

            for (short x = 0; x < data.Width; ++x)
            {
                for (short y = 0; y < data.Height; ++y)
                {
                    byte* pix = picStart + x * bytesPerPix + y * data.Stride;
                    mapOfValues[x, y] = RGBToGrayscalePixel(pix);
                }
            }
            bitmap.UnlockBits(data);

            return mapOfValues;
        }

        static Color RGBtoGrayscalePixel(Color col)
        {
            byte b = RGBToGrayscalePixel(col);
            return Color.FromArgb(col.A, b, b, b);
        }
        static byte RGBToGrayscalePixel(Color col)
        {
            return (byte)(0.3 * col.R + 0.59 * col.G + 0.11 * col.B);
        }
        static unsafe byte RGBToGrayscalePixel(byte* pix)
        {
            return (byte)(0.3 * pix[1] + 0.59 * pix[2] + 0.11 * pix[3]);
        }
        static unsafe byte RGBToGrayscalePixel(byte* pix, byte throwedBits)
        {
            byte r = (byte)(pix[1] >> throwedBits);
            byte g = (byte)(pix[2] >> throwedBits);
            byte b = (byte)(pix[3] >> throwedBits);
            return (byte)(0.3 * r + 0.59 * g + 0.11 * b);
        }
    }
}
