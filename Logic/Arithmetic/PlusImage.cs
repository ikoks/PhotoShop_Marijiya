using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Arithmetic
{
    internal class PlusImage
    {
        //Fungsi tambah image vs image
        public static Bitmap PlusArraysImage(byte[,,] pixelData1, byte[,,] pixelData2)
        {
            // Dapatkan dimensi dari kedua array
            int height1 = pixelData1.GetLength(0);
            int width1 = pixelData1.GetLength(1);
            int height2 = pixelData2.GetLength(0);
            int width2 = pixelData2.GetLength(1);

            // Tentukan dimensi maksimum untuk bitmap hasil
            int height = Math.Max(height1, height2);
            int width = Math.Max(width1, width2);

            // Buat bitmap baru dengan dimensi maksimum
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Iterasi melalui setiap piksel
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

                    // Tambahkan nilai piksel dan lakukan clamping ke 0-255
                    byte newR = (byte)Math.Min(255, R1 + R2);
                    byte newG = (byte)Math.Min(255, G1 + G2);
                    byte newB = (byte)Math.Min(255, B1 + B2);

                    // Set piksel di bitmap baru
                    bmp.SetPixel(x, y, Color.FromArgb((byte)newR, (byte)newG, (byte)newB));
                }
            }
            return bmp;
        }
    }
}
