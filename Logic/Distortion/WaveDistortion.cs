using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Distortion
{
    internal class WaveDistortion
    {
        /// <summary>
        /// Menerapkan efek Wave (Gelombang) Sinusoidal.
        /// </summary>
        /// <param name="bmp">Gambar asli.</param>
        /// <param name="amplitude">Tinggi gelombang (misal: 20).</param>
        /// <param name="frequency">Kerapatan gelombang (misal: 0.05).</param>
        /// <returns>Bitmap hasil distorsi.</returns>
        public static Bitmap ApplyWave(Bitmap bmp, double amplitude, double frequency)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            Bitmap newBmp = new Bitmap(w, h);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    // Rumus Gelombang Horizontal: geser X berdasarkan fungsi Sinus dari Y
                    int srcX = (int)(x + amplitude * Math.Sin(y * frequency));
                    int srcY = y; // Y tetap (bisa juga dibalik untuk gelombang vertikal)

                    // Cek batas gambar
                    if (srcX >= 0 && srcX < w && srcY >= 0 && srcY < h)
                    {
                        newBmp.SetPixel(x, y, bmp.GetPixel(srcX, srcY));
                    }
                    else
                    {
                        newBmp.SetPixel(x, y, Color.Black);
                    }
                }
            }
            return newBmp;
        }
    }
}
