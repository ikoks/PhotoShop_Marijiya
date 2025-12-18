using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Colors
{
    internal class BlueColor
    {
        public static Bitmap ApplyBlueColor(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte B = pixelData[y, x, 2];
                    bmp.SetPixel(x, y, Color.FromArgb(0, 0, B));
                }
            }
            return bmp;
        }
    }
}
