using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Histogram
{
    public static class LinearStretch
    {
             // ==================================================
            // FUNGSI UTAMA (INPUT pixelData, OUTPUT Bitmap)
            // ==================================================
        public static Bitmap ApplyLinearStretch(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);

            // 1. Histogram
            int[] histR = new int[256];
            int[] histG = new int[256];
            int[] histB = new int[256];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    histR[pixelData[y, x, 0]]++;
                    histG[pixelData[y, x, 1]]++;
                    histB[pixelData[y, x, 2]]++;
                }
            }

            // 2. Min Max
            GetMinMax(histR, out int rMin, out int rMax);
            GetMinMax(histG, out int gMin, out int gMax);
            GetMinMax(histB, out int bMin, out int bMax);

            // 3. Buat Bitmap hasil
            Bitmap result = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int r = Stretch(pixelData[y, x, 0], rMin, rMax);
                    int g = Stretch(pixelData[y, x, 1], gMin, gMax);
                    int b = Stretch(pixelData[y, x, 2], bMin, bMax);

                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return result;
        }

        // ==================================================
        // HELPER MIN MAX
        // ==================================================
        private static void GetMinMax(int[] hist, out int min, out int max)
        {
            min = 0;
            max = 255;

            for (int i = 0; i < 256; i++)
            {
                if (hist[i] > 0)
                {
                    min = i;
                    break;
                }
            }

            for (int i = 255; i >= 0; i--)
            {
                if (hist[i] > 0)
                {
                    max = i;
                    break;
                }
            }
        }

        // ==================================================
        // HELPER STRETCH
        // ==================================================
        private static int Stretch(int value, int min, int max)
        {
            if (max == min) return value;

            int result = (value - min) * 255 / (max - min);

            if (result < 0) result = 0;
            if (result > 255) result = 255;

            return result;
        }
    }
}


