using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Arithmetic
{
    internal class Bitwise
    {
        // Fungsi Bitwise AND
        public static Bitmap ApplyBitwiseAND(byte[,,] pixelData1, byte[,,] pixelData2)
        {
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
                    byte R1 = (x < width1 && y < height1) ? pixelData1[y, x, 0] : (byte)0;
                    byte G1 = (x < width1 && y < height1) ? pixelData1[y, x, 1] : (byte)0;
                    byte B1 = (x < width1 && y < height1) ? pixelData1[y, x, 2] : (byte)0;

                    byte R2 = (x < width2 && y < height2) ? pixelData2[y, x, 0] : (byte)0;
                    byte G2 = (x < width2 && y < height2) ? pixelData2[y, x, 1] : (byte)0;
                    byte B2 = (x < width2 && y < height2) ? pixelData2[y, x, 2] : (byte)0;

                    // --- INI LOGIKA BITWISE AND ---
                    byte newR = (byte)(R1 & R2);
                    byte newG = (byte)(G1 & G2);
                    byte newB = (byte)(B1 & B2);
                    // --- SELESAI ---

                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }
            return bmp;
        }

        // Fungsi bitwise OR
        public static Bitmap ApplyBitwiseOR(byte[,,] pixelData1, byte[,,] pixelData2)
        {
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
                    byte R1 = (x < width1 && y < height1) ? pixelData1[y, x, 0] : (byte)0;
                    byte G1 = (x < width1 && y < height1) ? pixelData1[y, x, 1] : (byte)0;
                    byte B1 = (x < width1 && y < height1) ? pixelData1[y, x, 2] : (byte)0;

                    byte R2 = (x < width2 && y < height2) ? pixelData2[y, x, 0] : (byte)0;
                    byte G2 = (x < width2 && y < height2) ? pixelData2[y, x, 1] : (byte)0;
                    byte B2 = (x < width2 && y < height2) ? pixelData2[y, x, 2] : (byte)0;

                    // --- INI LOGIKA BITWISE OR ---
                    byte newR = (byte)(R1 | R2);
                    byte newG = (byte)(G1 | G2);
                    byte newB = (byte)(B1 | B2);
                    // --- SELESAI ---

                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }
            return bmp;
        }

        // Fungsi bitwise XOR
        public static Bitmap ApplyBitwiseXOR(byte[,,] pixelData1, byte[,,] pixelData2)
        {
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
                    byte R1 = (x < width1 && y < height1) ? pixelData1[y, x, 0] : (byte)0;
                    byte G1 = (x < width1 && y < height1) ? pixelData1[y, x, 1] : (byte)0;
                    byte B1 = (x < width1 && y < height1) ? pixelData1[y, x, 2] : (byte)0;

                    byte R2 = (x < width2 && y < height2) ? pixelData2[y, x, 0] : (byte)0;
                    byte G2 = (x < width2 && y < height2) ? pixelData2[y, x, 1] : (byte)0;
                    byte B2 = (x < width2 && y < height2) ? pixelData2[y, x, 2] : (byte)0;

                    // --- INI LOGIKA BITWISE XOR ---
                    byte newR = (byte)(R1 ^ R2);
                    byte newG = (byte)(G1 ^ G2);
                    byte newB = (byte)(B1 ^ B2);
                    // --- SELESAI ---

                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }
            return bmp;
        }
    }
}
