using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Transform
{
    internal class ScaleImage
    {
        public static Bitmap ApplyScaleImage(Bitmap bmp, double factor)
        {
            int newWidth = (int)(bmp.Width * factor);
            int newHeight = (int)(bmp.Height * factor);

            // Pastikan ukuran minimal 1x1 piksel agar tidak error
            newWidth = Math.Max(1, newWidth);
            newHeight = Math.Max(1, newHeight);

            Bitmap newBmp = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(newBmp))
            {
                // Atur kualitas interpolasi agar gambar tidak pecah/buram saat di-zoom
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                // Gambar ulang dengan ukuran baru
                g.DrawImage(bmp, 0, 0, newWidth, newHeight);
            }

            return newBmp;
        }

    }
}
