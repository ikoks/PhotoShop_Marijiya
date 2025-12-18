using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Arithmetic
{
    internal class Multiply
    {
        public static Bitmap ApplyMultiplyImage(byte[,,] pixelData1, byte[,,] pixelData2)
        {
            // Tentukan dimensi maksimum
            int height1 = pixelData1.GetLength(0);
            int width1 = pixelData1.GetLength(1);
            int height2 = pixelData2.GetLength(0);
            int width2 = pixelData2.GetLength(1);

            int height = Math.Max(height1, height2);
            int width = Math.Max(width1, width2);

            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Ambil piksel dari array 1. 
                    // Jika di luar batas, anggap nilainya 1.
                    // Gunakan 'long' agar (255*255) tidak overflow.
                    long R1 = (x < width1 && y < height1) ? pixelData1[y, x, 0] : 1;
                    long G1 = (x < width1 && y < height1) ? pixelData1[y, x, 1] : 1;
                    long B1 = (x < width1 && y < height1) ? pixelData1[y, x, 2] : 1;

                    // Ambil piksel dari array 2.
                    // Jika di luar batas, anggap nilainya 1.
                    long R2 = (x < width2 && y < height2) ? pixelData2[y, x, 0] : 1;
                    long G2 = (x < width2 && y < height2) ? pixelData2[y, x, 1] : 1;
                    long B2 = (x < width2 && y < height2) ? pixelData2[y, x, 2] : 1;

                    // --- INI LOGIKA BARU ANDA ---
                    // Rumus: R1 * R2, lalu jepit ke 255
                    long newR = R1 * R2;
                    long newG = G1 * G2;
                    long newB = B1 * B2;

                    byte finalR = (byte)Math.Min(255, newR);
                    byte finalG = (byte)Math.Min(255, newG);
                    byte finalB = (byte)Math.Min(255, newB);
                    // --- SELESAI ---

                    bmp.SetPixel(x, y, Color.FromArgb(finalR, finalG, finalB));
                }
            }
            return bmp;
        }

        public static Bitmap ApplyMultiply(byte[,,] pixelData, double value)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Ambil nilai piksel lama
                    double R = pixelData[y, x, 0];
                    double G = pixelData[y, x, 1];
                    double B = pixelData[y, x, 2];

                    // Terapkan perkalian
                    double newR = R * value;
                    double newG = G * value;
                    double newB = B * value;

                    // "Jepit" (Clamp) hasilnya agar tetap di antara 0 dan 255
                    byte finalR = (byte)Math.Min(255, Math.Max(0, newR));
                    byte finalG = (byte)Math.Min(255, Math.Max(0, newG));
                    byte finalB = (byte)Math.Min(255, Math.Max(0, newB));

                    bmp.SetPixel(x, y, Color.FromArgb(finalR, finalG, finalB));
                }
            }
            return bmp;
        }
    }
}
