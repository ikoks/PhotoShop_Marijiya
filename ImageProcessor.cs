using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PhotoShop_Marijiya
{
    
    public static class ImageProcessor
    {
        // Fungsi Mengubah Foto Menjadi GrayScale
        public static Bitmap ApplyGrayscale(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Ambil data dari array asli
                    byte R = pixelData[y, x, 0];
                    byte G = pixelData[y, x, 1];
                    byte B = pixelData[y, x, 2];

                    // Hitung nilai grayscale
                    byte gray = (byte)((R + G + B) / 3);

                    // Set piksel di bitmap baru
                    bmp.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }
            return bmp;
        }

        //Fungsi mengubah foto menjadi merah
        public static Bitmap ApplyRedChannel(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte R = pixelData[y, x, 0];
                    bmp.SetPixel(x, y, Color.FromArgb(R, 0, 0));
                }
            }
            return bmp;
        }

        //Fungsi Mengubah Foto Menjadi Hijau
        public static Bitmap ApplyGreenChannel(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte G = pixelData[y, x, 1];
                    bmp.SetPixel(x, y, Color.FromArgb(0, G, 0));
                }
            }
            return bmp;
        }

       //Fungsi Mengubah Foto Menjadi Biru
        public static Bitmap ApplyBlueChannel(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte B = pixelData[y, x, 2];
                    bmp.SetPixel(x, y, Color.FromArgb(0, 0, B));
                }
            }
            return bmp;
        }
    }
}
