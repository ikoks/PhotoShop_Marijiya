using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Arithmetic
{
    internal class MinImage
    {
        // Fungsi kurang image vs image
        public static Bitmap MinArraysImage(byte[,,] pixelData1, byte[,,] pixelData2)
        {
            // Dapatkan dimensi dari kedua array
            int height1 = pixelData1.GetLength(0);
            int width1 = pixelData1.GetLength(1);
            int height2 = pixelData2.GetLength(0);
            int width2 = pixelData2.GetLength(1);

            // 1. Tentukan dimensi MAKSIMUM (Perbaikan #1)
            int height = Math.Max(height1, height2);
            int width = Math.Max(width1, width2);

            // Buat bitmap baru dengan dimensi maksimum
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // 2. Iterasi melalui setiap piksel di kanvas terbesar
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Ambil nilai piksel dari kedua array
                    // Jika di luar batas, anggap nilainya 0
                    int R1 = (x < width1 && y < height1) ? pixelData1[y, x, 0] : 0;
                    int G1 = (x < width1 && y < height1) ? pixelData1[y, x, 1] : 0;
                    int B1 = (x < width1 && y < height1) ? pixelData1[y, x, 2] : 0;

                    int R2 = (x < width2 && y < height2) ? pixelData2[y, x, 0] : 0;
                    int G2 = (x < width2 && y < height2) ? pixelData2[y, x, 1] : 0;
                    int B2 = (x < width2 && y < height2) ? pixelData2[y, x, 2] : 0;

                    // 3. Gunakan LOGIKA DIFFERENCE (Absolut) (Perbaikan #2)
                    byte newR = (byte)Math.Abs(R1 - R2);
                    byte newG = (byte)Math.Abs(G1 - G2);
                    byte newB = (byte)Math.Abs(B1 - B2);

                    // Set piksel di bitmap baru
                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }
            return bmp;
        }
    }
}
