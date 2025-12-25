using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Histogram
{
    public static class HistogramEqualization
    {
        public static Bitmap ApplyHistogramEqualization(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            int totalPixels = width * height;

            Bitmap bmp = new Bitmap(width, height);

            // --- TAHAP 1: Hitung Histogram (Frekuensi) ---
            int[] histR = new int[256];
            int[] histG = new int[256];
            int[] histB = new int[256];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    histR[pixelData[y, x, 0]]++;
                    histG[pixelData[y, x, 1]]++;
                    histB[pixelData[y, x, 2]]++;
                }
            }

            // --- TAHAP 2: Hitung CDF (Cumulative Distribution Function) ---
            // CDF adalah jumlah kumulatif dari histogram sebelumnya
            int[] cdfR = new int[256];
            int[] cdfG = new int[256];
            int[] cdfB = new int[256];

            cdfR[0] = histR[0];
            cdfG[0] = histG[0];
            cdfB[0] = histB[0];

            for (int i = 1; i < 256; i++)
            {
                cdfR[i] = cdfR[i - 1] + histR[i];
                cdfG[i] = cdfG[i - 1] + histG[i];
                cdfB[i] = cdfB[i - 1] + histB[i];
            }

            // --- TAHAP 3: Normalisasi CDF ke rentang 0-255 (Mapping) ---
            // Rumus: h(v) = round( (cdf(v) - cdfMin) / (TotalPixels - 1) * 255 )
            // Atau versi simpel: (cdf(v) / TotalPixels) * 255

            byte[] mapR = new byte[256];
            byte[] mapG = new byte[256];
            byte[] mapB = new byte[256];

            // Cari nilai CDF minimum (yang bukan nol) agar hasil lebih akurat
            int minCdfR = 0, minCdfG = 0, minCdfB = 0;
            for (int i = 0; i < 256; i++) { if (cdfR[i] > 0) { minCdfR = cdfR[i]; break; } }
            for (int i = 0; i < 256; i++) { if (cdfG[i] > 0) { minCdfG = cdfG[i]; break; } }
            for (int i = 0; i < 256; i++) { if (cdfB[i] > 0) { minCdfB = cdfB[i]; break; } }

            for (int i = 0; i < 256; i++)
            {
                mapR[i] = (byte)((float)(cdfR[i] - minCdfR) / (totalPixels - minCdfR) * 255);
                mapG[i] = (byte)((float)(cdfG[i] - minCdfG) / (totalPixels - minCdfG) * 255);
                mapB[i] = (byte)((float)(cdfB[i] - minCdfB) / (totalPixels - minCdfB) * 255);
            }

            // --- TAHAP 4: Terapkan Mapping ke Gambar ---
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte oldR = pixelData[y, x, 0];
                    byte oldG = pixelData[y, x, 1];
                    byte oldB = pixelData[y, x, 2];

                    byte newR = mapR[oldR];
                    byte newG = mapG[oldG];
                    byte newB = mapB[oldB];

                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }

            return bmp;
        }
    }
}
