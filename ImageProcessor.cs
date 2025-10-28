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
                    byte gray =(byte) ((0.299 * R) + (0.587 * G) + (0.114 * B));

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

        //Fungsi Mengubah Foto Menjadi Negatif
        public static Bitmap ApplyNegative(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Ambil nilai piksel lama dari array
                    byte R = pixelData[y, x, 0];
                    byte G = pixelData[y, x, 1];
                    byte B = pixelData[y, x, 2];

                    // Terapkan rumus negatif
                    // Kita perlu cast (byte) karena 255 (int) - byte (byte) hasilnya int
                    byte newR = (byte)(255 - R);
                    byte newG = (byte)(255 - G);
                    byte newB = (byte)(255 - B);

                    // Set piksel di bitmap baru
                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }
            return bmp;
        }

        // Fungsi Mengubah Foto Menjadi Black and White
        public static Bitmap ApplyBlackAndWhite(byte[,,] pixelData, int threshold = 128)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap newBmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int gray = (pixelData[y, x, 0] + pixelData[y, x, 1] + pixelData[y, x, 2]) / 3;
                    Color newColor = (gray >= threshold) ? Color.White : Color.Black;
                    newBmp.SetPixel(x, y, newColor);
                }
            }

            return newBmp;
        }

        // Fungsi mengubah kecerahan gambar / foto
        public static Bitmap ApplyBrightness(byte[,,] pixelData, int value)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Ambil nilai piksel lama dari array
                    int R = pixelData[y, x, 0];
                    int G = pixelData[y, x, 1];
                    int B = pixelData[y, x, 2];

                    // Tambahkan rumus kecerahan dan clamping ke 0-255
                    byte newR = (byte)Math.Min(255, Math.Max(0, R + value));
                    byte newG = (byte)Math.Min(255, Math.Max(0, G + value));
                    byte newB = (byte)Math.Min(255, Math.Max(0, B + value));

                    // Set piksel di bitmap baru
                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }
            return bmp;
        }

    }
}
