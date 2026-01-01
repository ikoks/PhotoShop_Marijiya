using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Filter
{
    internal class SharpenFilter
    {
        // Kernel Sharpen Standar (3x3)
        // Logikanya: Piksel tengah diperkuat (5), tetangganya dikurangi (-1) untuk menonjolkan perbedaan.
        private static readonly double[,] Kernel = {
            {  0, -1,  0 },
            { -1,  5, -1 },
            {  0, -1,  0 }
        };

        public static Bitmap Apply(byte[,,] pixelData)
        {
            // Kita menggunakan mesin 'Convolution' yang sudah Anda buat sebelumnya.
            // Factor = 1.0 (karena total kernel 5-1-1-1-1 = 1)
            // Bias = 0
            return Convolution.ApplyConvolution(pixelData, Kernel, 1.0, 0);
        }
    }
}
