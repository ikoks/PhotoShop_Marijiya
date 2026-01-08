using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Colors
{
    internal class DetectionColor
    {
        // Fungsi Mendeteksi Warna Tertentu dalam Gambar
        public static Bitmap ApplyDetection(byte[,,] pixelData, Color targetColor, int tolerance)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte r = pixelData[y, x, 0];
                    byte g = pixelData[y, x, 1];
                    byte b = pixelData[y, x, 2];

                    // 1. Hitung Jarak Warna (Euclidean Distance)
                    // Rumus ini mengukur seberapa jauh perbedaan warna pixel ini dengan warna target
                    int diffR = r - targetColor.R;
                    int diffG = g - targetColor.G;
                    int diffB = b - targetColor.B;

                    // Jarak akar kuadrat (lebih akurat daripada sekadar R+G+B)
                    double distance = Math.Sqrt(diffR * diffR + diffG * diffG + diffB * diffB);

                    // 2. Cek apakah masuk dalam toleransi?
                    if (distance < tolerance)
                    {
                        // JIKA COCOK (Warna Terseleksi):
                        // Ubah jadi Abu-abu gelap (efek terarsir/hitam)
                        int gray = (int)((r * 0.3) + (g * 0.59) + (b * 0.11));

                        // Opsional: Bisa dibuat lebih gelap agar terlihat "terseleksi"
                        // gray = gray / 2; 

                        bmp.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                    }
                    else
                    {
                        // JIKA TIDAK COCOK:
                        // Biarkan warna asli
                        bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
            }
            return bmp;
        }
    }
}
