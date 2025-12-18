using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Arithmetic
{
    internal class Divide
    {
        // Fungsi pembagian angka
        public static Bitmap ApplyDivide(byte[,,] pixelData, double value)
        {
            // PENTING: Mencegah error pembagian dengan nol
            if (value == 0)
            {
                // Kembalikan gambar asli jika pembagi 0
                return Multiply.ApplyMultiply(pixelData, 1.0); 
            }

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

                    // Terapkan pembagian
                    double newR = R / value;
                    double newG = G / value;
                    double newB = B / value;

                    // "Jepit" (Clamp) hasilnya (meskipun pembagian jarang > 255)
                    byte finalR = (byte)Math.Min(255, Math.Max(0, newR));
                    byte finalG = (byte)Math.Min(255, Math.Max(0, newG));
                    byte finalB = (byte)Math.Min(255, Math.Max(0, newB));

                    bmp.SetPixel(x, y, Color.FromArgb(finalR, finalG, finalB));
                }
            }
            return bmp;
        }

        // Fungsi pembagian image vs image
        public static Bitmap ApplyDivideImage(byte[,,] pixelData1, byte[,,] pixelData2)
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
                    // Ambil piksel dari array 1 (Gunakan 'double' untuk presisi)
                    double R1 = (x < width1 && y < height1) ? pixelData1[y, x, 0] : 0;
                    double G1 = (x < width1 && y < height1) ? pixelData1[y, x, 1] : 0;
                    double B1 = (x < width1 && y < height1) ? pixelData1[y, x, 2] : 0;

                    // Ambil piksel dari array 2
                    // Jika di luar batas, anggap '1' (agar R1 / 1 = R1)
                    double R2 = (x < width2 && y < height2) ? pixelData2[y, x, 0] : 1;
                    double G2 = (x < width2 && y < height2) ? pixelData2[y, x, 1] : 1;
                    double B2 = (x < width2 && y < height2) ? pixelData2[y, x, 2] : 1;

                    // --- PENCEGAHAN DIVIDE BY ZERO ---
                    // Jika piksel BUKAN di luar batas TAPI warnanya 0 (hitam)
                    if (R2 == 0) R2 = 1.0; // Anggap 1 untuk mencegah crash
                    if (G2 == 0) G2 = 1.0;
                    if (B2 == 0) B2 = 1.0;
                    // --- SELESAI ---

                    // Terapkan pembagian
                    double newR = R1 / R2;
                    double newG = G1 / G2;
                    double newB = B1 / B2;

                    // "Jepit" (Clamp) hasilnya (meskipun jarang terjadi di pembagian)
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
