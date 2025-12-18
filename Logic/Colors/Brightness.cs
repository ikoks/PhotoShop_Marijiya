using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Colors
{
    internal class Brightness
    {
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
