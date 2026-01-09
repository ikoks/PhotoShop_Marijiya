using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Colors
{
    internal class TitikContrast
    {
        /// <summary>
        /// Menerapkan Contrast Stretching (Peregangan Kontras) berbasis operasi titik.
        /// Nilai pixel < min akan jadi 0 (Hitam).
        /// Nilai pixel > max akan jadi 255 (Putih).
        /// Nilai di antaranya akan direnggangkan (Stretched).
        /// </summary>
        public static Bitmap ApplyContrastTitik(byte[,,] pixelData, int rMin, int rMax)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            // Validasi agar tidak bagi nol
            if (rMax <= rMin) rMax = rMin + 1;

            // Konstanta pengali agar perhitungan di dalam loop lebih cepat
            // Rumus: s = (r - rMin) * (255 / (rMax - rMin))
            double scale = 255.0 / (rMax - rMin);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte r = pixelData[y, x, 0];
                    byte g = pixelData[y, x, 1];
                    byte b = pixelData[y, x, 2];

                    // Proses Channel Red
                    int newR = (int)((r - rMin) * scale);

                    // Proses Channel Green
                    int newG = (int)((g - rMin) * scale);

                    // Proses Channel Blue
                    int newB = (int)((b - rMin) * scale);

                    // CLAMPING (Operasi Titik Wajib di-clamp ke 0-255)
                    // Jika hasil < 0, jadikan 0. Jika > 255, jadikan 255.
                    newR = Math.Max(0, Math.Min(255, newR));
                    newG = Math.Max(0, Math.Min(255, newG));
                    newB = Math.Max(0, Math.Min(255, newB));

                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }
            return bmp;
        }
    }
}
