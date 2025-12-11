using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Filter
{
    internal class ApplyEdgeDetection
    {
        public static Bitmap Apply(byte[,,] pixelData, int[,] gx, int[,] gy)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            // Offset 1 untuk kernel 3x3 (agar tidak keluar batas)
            int offset = 1;

            for (int y = offset; y < height - offset; y++)
            {
                for (int x = offset; x < width - offset; x++)
                {
                    int sumX = 0;
                    int sumY = 0;

                    // Konvolusi 3x3
                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            // Kita gunakan channel Green (index 1) sebagai representasi intensitas/gray
                            int val = pixelData[y + ky, x + kx, 1];

                            // Ambil bobot dari kernel (perhatikan indeks array kernel)
                            // gx[ky + 1, kx + 1] mengubah indeks -1..1 menjadi 0..2
                            sumX += val * gx[ky + 1, kx + 1];
                            sumY += val * gy[ky + 1, kx + 1];
                        }
                    }

                    // Rumus Magnitude (Gradient): Akar(X^2 + Y^2)
                    int magnitude = (int)Math.Sqrt((sumX * sumX) + (sumY * sumY));

                    // Batasi nilai agar tetap 0-255 (Clamping)
                    magnitude = Math.Min(255, Math.Max(0, magnitude));

                    // Set pixel hasil (Grayscale)
                    bmp.SetPixel(x, y, Color.FromArgb(magnitude, magnitude, magnitude));
                }
            }
            return bmp;
        }
    }
}
