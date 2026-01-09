using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Colors
{
    internal class GlobalContrast
    {
        /// <summary>
        /// Peregangan Kontras Aras Global (Global Contrast Stretching).
        /// Mencari Min/Max dari SELURUH gambar, lalu menariknya ke 0-255.
        /// </summary>
        public static Bitmap ApplyGlobalStretch(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            // 1. Cari Min dan Max Global (Per Channel)
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

            // 2. Hitung Faktor Skala (Pre-calculation biar cepat)
            // Rumus: scale = 255 / (Max - Min)
            double scaleR = (maxR == minR) ? 0 : 255.0 / (maxR - minR);
            double scaleG = (maxG == minG) ? 0 : 255.0 / (maxG - minG);
            double scaleB = (maxB == minB) ? 0 : 255.0 / (maxB - minB);

            // 3. Terapkan Peregangan ke Seluruh Gambar
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte r = pixelData[y, x, 0];
                    byte g = pixelData[y, x, 1];
                    byte b = pixelData[y, x, 2];

                    // Rumus: (Val - Min) * scale
                    int newR = (int)((r - minR) * scaleR);
                    int newG = (int)((g - minG) * scaleG);
                    int newB = (int)((b - minB) * scaleB);

                    // Clamp (Jaga-jaga)
                    newR = Math.Min(255, Math.Max(0, newR));
                    newG = Math.Min(255, Math.Max(0, newG));
                    newB = Math.Min(255, Math.Max(0, newB));

                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }

            return bmp;
        }
    }
}
