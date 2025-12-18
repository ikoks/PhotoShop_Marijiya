using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Transform
{
    internal class Rotation
    {
        public static Bitmap rotate90(Bitmap bmp)
        {
            Bitmap newBmp = (Bitmap)bmp.Clone();
            newBmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            return newBmp;
        }

        public static Bitmap rotate180(Bitmap bmp)
        {
            Bitmap newBmp = (Bitmap)bmp.Clone();
            newBmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
            return newBmp;
        }

        public static Bitmap rotate270(Bitmap bmp)
        {
            Bitmap newBmp = (Bitmap)bmp.Clone();
            newBmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return newBmp;
        }

        public static Bitmap rotateFreeDegree(Bitmap bmp, float degree)
        {
            // menghitung radians
            double radians = degree * Math.PI / 180.0;
            double cos = Math.Abs(Math.Cos(radians));
            double sin = Math.Abs(Math.Sin(radians));

            //kanvas baru
            int newWidth = (int)Math.Round(bmp.Width * cos + bmp.Height * sin);
            int newHeigth = (int)Math.Round(bmp.Height * cos + bmp.Width * sin);

            Bitmap newBmp = new Bitmap(newWidth, newHeigth, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(newBmp))
            {
                //kualitas tinggi
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // Pindahkan titik pusat ke tengah kanvas baru
                g.TranslateTransform(newWidth / 2.0f, newHeigth / 2.0f);

                // Putar
                g.RotateTransform(degree);

                // Geser balik agar menggambar dari posisi yang benar
                g.TranslateTransform(-bmp.Width / 2.0f, -bmp.Height / 2.0f);

                g.DrawImage(bmp, new Point(0, 0));
            }

            return newBmp;

        }
    }
}
