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
                    byte gray = (byte)((0.299 * R) + (0.587 * G) + (0.114 * B));

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
                    int gray = (int)((0.299 * pixelData[y, x, 0]) + (0.587 * pixelData[y, x, 1]) + (0.114 * pixelData[y, x, 2]));
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


        // Fungsi Mendeteksi Warna Tertentu dalam Gambar
        public static Bitmap ApplyColorDetection(byte[,,] pixelData, Color targetColor)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte R = pixelData[y, x, 0];
                    byte G = pixelData[y, x, 1];
                    byte B = pixelData[y, x, 2];

                    Color currentColor = Color.FromArgb(R, G, B);

                    if (currentColor == targetColor)
                    {
                        bmp.SetPixel(x, y, currentColor); // Pertahankan warna asli
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.Black); // Ubah jadi hitam
                    }
                }
            }
            return bmp;
        }

        public static Bitmap PlusArraysImage(byte[ , , ] pixelData1, byte[,,] pixelData2)
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

        public static Bitmap ApplyDivide(byte[,,] pixelData, double value)
        {
            // PENTING: Mencegah error pembagian dengan nol
            if (value == 0)
            {
                // Kembalikan gambar asli jika pembagi 0
                return ApplyMultiply(pixelData, 1.0); // Trik: 1.0x = salinan
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
    }
}
