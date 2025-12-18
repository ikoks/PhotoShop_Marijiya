using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya.Logic.Colors
{
    internal class RedColor
    {
        public static Bitmap ApplyRed(byte[,,] pixelData)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte R = pixelData[y, x, 0];
                    bmp.SetPixel(x, y, Color.FromArgb(R, 0, 0));
                }
            }
            MessageBox.Show("Efek warna merah diterapkan!");
            return bmp;
        }
    }
}
