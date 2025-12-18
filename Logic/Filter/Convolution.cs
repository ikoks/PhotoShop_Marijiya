using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Filter
{
    internal class Convolution
    {
        public static Bitmap ApplyConvolution(byte[,,] pixelData, double[,] kernel, double factor = 1.0, int bias = 0)
        {
            int width = pixelData.GetLength(1);
            int height = pixelData.GetLength(0);
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Tentukan ukuran kernel
            int kernelWidth = kernel.GetLength(1);
            int kernelHeight = kernel.GetLength(0);

            // Cari titik tengah kernel (anchor)
            int filterOffsetW = (kernelWidth - 1) / 2;
            int filterOffsetH = (kernelHeight - 1) / 2;

            // Loop setiap piksel gambar
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double rSum = 0, gSum = 0, bSum = 0;

                    // Loop Kernel
                    for (int filterY = 0; filterY < kernelHeight; filterY++)
                    {
                        for (int filterX = 0; filterX < kernelWidth; filterX++)
                        {
                            // Cari piksel tetangga
                            int imageX = (x - filterOffsetW + filterX);
                            int imageY = (y - filterOffsetH + filterY);

                            // Cek batas gambar (Edge Handling: Skip jika di luar)
                            if (imageX >= 0 && imageX < width && imageY >= 0 && imageY < height)
                            {
                                byte r = pixelData[imageY, imageX, 0];
                                byte g = pixelData[imageY, imageX, 1];
                                byte b = pixelData[imageY, imageX, 2];

                                rSum += r * kernel[filterY, filterX];
                                gSum += g * kernel[filterY, filterX];
                                bSum += b * kernel[filterY, filterX];
                            }
                        }
                    }

                    // Terapkan Faktor dan Bias
                    int newR = (int)(rSum * factor) + bias;
                    int newG = (int)(gSum * factor) + bias;
                    int newB = (int)(bSum * factor) + bias;

                    // Clamp ke 0-255
                    newR = Math.Min(255, Math.Max(0, newR));
                    newG = Math.Min(255, Math.Max(0, newG));
                    newB = Math.Min(255, Math.Max(0, newB));

                    bmp.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }
            return bmp;
        }
    }
}
