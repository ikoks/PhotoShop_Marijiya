using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Distortion
{
    internal class SwirlDistortion
    {
        /// <summary>
        /// Menerapkan efek Swirl (Pusaran) pada gambar.
        /// </summary>
        /// <param name="bmp">Gambar asli.</param>
        /// <param name="degree">Kekuatan pusaran (misal: 0.05).</param>
        /// <returns>Bitmap hasil distorsi.</returns>
        public static Bitmap ApplySwirl(Bitmap bmp, double degree)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            Bitmap newBmp = new Bitmap(w, h);

            // Titik pusat gambar
            double cX = w / 2.0;
            double cY = h / 2.0;

            // Akses piksel langsung (perhatikan: ini lambat untuk gambar besar jika tanpa LockBits, 
            // tapi untuk kemudahan pemahaman kita pakai Get/SetPixel dulu).
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    // Hitung offset relatif terhadap pusat
                    double relX = x - cX;
                    double relY = y - cY;

                    // Konversi ke koordinat polar
                    double distance = Math.Sqrt(relX * relX + relY * relY);
                    double angle = Math.Atan2(relY, relX);

                    // Hitung sudut baru (distorsi)
                    // Semakin jauh dari pusat, semakin kecil putarannya (atau sebaliknya)
                    double newAngle = angle + (distance * degree);

                    // Konversi kembali ke koordinat kartesius
                    int srcX = (int)(cX + distance * Math.Cos(newAngle));
                    int srcY = (int)(cY + distance * Math.Sin(newAngle));

                    // Cek batas gambar (Clamping)
                    if (srcX >= 0 && srcX < w && srcY >= 0 && srcY < h)
                    {
                        newBmp.SetPixel(x, y, bmp.GetPixel(srcX, srcY));
                    }
                    else
                    {
                        // Area kosong diisi hitam (atau transparan)
                        newBmp.SetPixel(x, y, Color.Black);
                    }
                }
            }
            return newBmp;
        }
    }
}
