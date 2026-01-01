using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Colors
{
    internal class PseudoColor
    {
        /// <summary>
        /// Menerapkan Pewarnaan Semu (Pseudocolor) dengan gaya "Thermal Heat Map".
        /// Grayscale -> (Biru -> Cyan -> Hijau -> Kuning -> Merah).
        /// </summary>
        public static Bitmap ApplyPseudocolor(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 1. Ambil intensitas rata-rata (Grayscale)
                    // Jika gambar sudah grayscale, ambil salah satu channel saja (misal R) biar cepat
                    byte r = pixelData[y, x, 0];
                    byte g = pixelData[y, x, 1];
                    byte b = pixelData[y, x, 2];

                    int intensity = (r + g + b) / 3;

                    // 2. Petakan Intensitas ke Warna (Heat Map Logic)
                    byte newR = 0, newG = 0, newB = 0;

                    if (intensity < 128)
                    {
                        // Bagian Gelap (0-127): Transisi Biru ke Hijau
                        // Intensitas 0   -> R:0,   G:0,   B:255 (Biru)
                        // Intensitas 127 -> R:0,   G:255, B:0   (Hijau)
                        newR = 0;
                        newG = (byte)(2 * intensity);
                        newB = (byte)(255 - 2 * intensity);
                    }
                    else
                    {
                        // Bagian Terang (128-255): Transisi Hijau ke Merah
                        // Intensitas 128 -> R:0,   G:255, B:0   (Hijau)
                        // Intensitas 255 -> R:255, G:0,   B:0   (Merah)
                        newR = (byte)(2 * (intensity - 128));
                        newG = (byte)(255 - 2 * (intensity - 128));
                        newB = 0;
                    }

                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }

            return bmp;
        }
    }
}
