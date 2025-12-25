using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Histogram
{
    public static class AdaptiveHistogramEqualization
    {
        // ==================================================
        // INPUT  : byte[,,] pixelData
        // OUTPUT : Bitmap
        // ==================================================
        public static Bitmap ApplyAHE(byte[,,] pixelData, int tileSize = 32)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);

            Bitmap result = new Bitmap(width, height);

            // Proses per channel
            for (int channel = 0; channel < 3; channel++)
            {
                ProcessChannel(pixelData, result, channel, tileSize);
            }

            return result;
        }

        // ==================================================
        // PROSES 1 CHANNEL
        // ==================================================
        private static void ProcessChannel(
            byte[,,] pixelData,
            Bitmap result,
            int channel,
            int tileSize)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);

            for (int ty = 0; ty < height; ty += tileSize)
            {
                for (int tx = 0; tx < width; tx += tileSize)
                {
                    int yEnd = Math.Min(ty + tileSize, height);
                    int xEnd = Math.Min(tx + tileSize, width);

                    // 1. Histogram lokal
                    int[] hist = new int[256];

                    for (int y = ty; y < yEnd; y++)
                    {
                        for (int x = tx; x < xEnd; x++)
                        {
                            hist[pixelData[y, x, channel]]++;
                        }
                    }

                    // 2. CDF
                    int totalPixel = (yEnd - ty) * (xEnd - tx);
                    int[] cdf = new int[256];
                    cdf[0] = hist[0];

                    for (int i = 1; i < 256; i++)
                        cdf[i] = cdf[i - 1] + hist[i];

                    // 3. Equalization lokal
                    for (int y = ty; y < yEnd; y++)
                    {
                        for (int x = tx; x < xEnd; x++)
                        {
                            int value = pixelData[y, x, channel];
                            int newValue = (cdf[value] * 255) / totalPixel;

                            Color oldColor = result.GetPixel(x, y);
                            int r = oldColor.R;
                            int g = oldColor.G;
                            int b = oldColor.B;

                            if (channel == 0) r = newValue;
                            if (channel == 1) g = newValue;
                            if (channel == 2) b = newValue;

                            result.SetPixel(x, y, Color.FromArgb(r, g, b));
                        }
                    }
                }
            }
        }
    }
}
