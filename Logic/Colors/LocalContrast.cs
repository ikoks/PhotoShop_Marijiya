using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Colors
{
    internal class LocalContrast
    {
        /// <summary>
        /// Peregangan Kontras Lokal (Local Histogram Stretching).
        /// Menggunakan window/kernel size untuk mencari Min/Max lokal.
        /// </summary>
        public static Bitmap ApplyLocalStatistics(byte[,,] pixelData, int kernelSize)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            int offset = kernelSize / 2;

            // Loop setiap piksel gambar
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // --- 1. Cari Min dan Max di area Kernel ---
                    byte localMinR = 255, localMaxR = 0;
                    byte localMinG = 255, localMaxG = 0;
                    byte localMinB = 255, localMaxB = 0;

                    for (int ky = -offset; ky <= offset; ky++)
                    {
                        for (int kx = -offset; kx <= offset; kx++)
                        {
                            // Pastikan tidak keluar batas gambar
                            int ny = Math.Min(Math.Max(y + ky, 0), height - 1);
                            int nx = Math.Min(Math.Max(x + kx, 0), width - 1);

                            byte r = pixelData[ny, nx, 0];
                            byte g = pixelData[ny, nx, 1];
                            byte b = pixelData[ny, nx, 2];

                            if (r < localMinR) localMinR = r;
                            if (r > localMaxR) localMaxR = r;

                            if (g < localMinG) localMinG = g;
                            if (g > localMaxG) localMaxG = g;

                            if (b < localMinB) localMinB = b;
                            if (b > localMaxB) localMaxB = b;
                        }
                    }

                    // --- 2. Terapkan Rumus Stretching pada Piksel Tengah ---
                    byte currentR = pixelData[y, x, 0];
                    byte currentG = pixelData[y, x, 1];
                    byte currentB = pixelData[y, x, 2];

                    int newR = Stretch(currentR, localMinR, localMaxR);
                    int newG = Stretch(currentG, localMinG, localMaxG);
                    int newB = Stretch(currentB, localMinB, localMaxB);

                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }

            return bmp;
        }

        // Helper Rumus
        private static int Stretch(byte val, byte min, byte max)
        {
            if (max <= min) return val; // Hindari bagi nol jika area datar (warna sama semua)

            // Rumus: (Val - Min) * 255 / (Max - Min)
            int result = ((val - min) * 255) / (max - min);

            return Math.Min(255, Math.Max(0, result));
        }
    }
}
