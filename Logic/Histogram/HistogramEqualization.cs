using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Histogram
{
    public static class HistogramEqualization
    {
        public static Bitmap ApplyNormalization(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            // 1. Cari Nilai Min dan Max untuk setiap channel (R, G, B)
            byte minR = 255, maxR = 0;
            byte minG = 255, maxG = 0;
            byte minB = 255, maxB = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte r = pixelData[y, x, 0];
                    byte g = pixelData[y, x, 1];
                    byte b = pixelData[y, x, 2];

                    if (r < minR) minR = r;
                    if (r > maxR) maxR = r;

                    if (g < minG) minG = g;
                    if (g > maxG) maxG = g;

                    if (b < minB) minB = b;
                    if (b > maxB) maxB = b;
                }
            }

            // 2. Terapkan Rumus Stretching
            // Rumus: NewPixel = ((OldPixel - Min) * 255) / (Max - Min)

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte r = pixelData[y, x, 0];
                    byte g = pixelData[y, x, 1];
                    byte b = pixelData[y, x, 2];

                    // Cegah pembagian dengan nol jika gambar datar (min == max)
                    int newR = (maxR == minR) ? r : ((r - minR) * 255) / (maxR - minR);
                    int newG = (maxG == minG) ? g : ((g - minG) * 255) / (maxG - minG);
                    int newB = (maxB == minB) ? b : ((b - minB) * 255) / (maxB - minB);

                    // Clamp (Jaga-jaga)
                    newR = Math.Min(255, Math.Max(0, newR));
                    newG = Math.Min(255, Math.Max(0, newG));
                    newB = Math.Min(255, Math.Max(0, newB));

                    bmp.SetPixel(x, y, Color.FromArgb((byte)newR, (byte)newG, (byte)newB));
                }
            }

            return bmp;
        }
    }
}
