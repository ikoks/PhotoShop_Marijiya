using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Filter
{
    internal class SofteningFilter
    {
        // Kernel Gaussian Blur 3x3
        // Logikanya: Piksel tengah memiliki bobot terbesar (4), piksel sudut terkecil (1).
        // Total semua angka = 1 + 2 + 1 + 2 + 4 + 2 + 1 + 2 + 1 = 16.
        // Maka Factor (Pembagi) harus 16 agar gambar tidak jadi terlalu terang.
        private static readonly double[,] Kernel = {
            { 1, 2, 1 },
            { 2, 4, 2 },
            { 1, 2, 1 }
        };

        public static Bitmap Apply(byte[,,] pixelData)
        {
            // Factor = 1/16 (karena total bobot kernel adalah 16)
            double factor = 1.0 / 16.0;
            int bias = 0;

            return Convolution.ApplyConvolution(pixelData, Kernel, factor, bias);
        }
    }
}
