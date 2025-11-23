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

        //Fungsi tambah image vs image
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

        // Fungsi perkalian angka
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

        // Fungsi pembagian angka
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

        // Fungsi perkalian image vs image
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

        /// <summary>
        /// Mengubah ukuran gambar (Zoom In/Out) berdasarkan faktor skala.
        /// </summary>
        /// <param name="bmp">Gambar asli.</param>
        /// <param name="factor">Faktor skala (misal: 1.25 untuk Zoom In 25%, 0.8 untuk Zoom Out).</param>
        /// <returns>Bitmap baru dengan ukuran yang disesuaikan.</returns>
        public static Bitmap ScaleImage(Bitmap bmp, double factor)
        {
            int newWidth = (int)(bmp.Width * factor);
            int newHeight = (int)(bmp.Height * factor);

            // Pastikan ukuran minimal 1x1 piksel agar tidak error
            newWidth = Math.Max(1, newWidth);
            newHeight = Math.Max(1, newHeight);

            Bitmap newBmp = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(newBmp))
            {
                // Atur kualitas interpolasi agar gambar tidak pecah/buram saat di-zoom
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                // Gambar ulang dengan ukuran baru
                g.DrawImage(bmp, 0, 0, newWidth, newHeight);
            }

            return newBmp;
        }
    }
}
