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
    }
}
