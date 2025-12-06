using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoShop_Marijiya.Logic.Colors
{
    internal class BWColor
    {
        public static Bitmap ApplyBlackAndWhite(byte[,,] pixelData, int threshold = 128)
        {
            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap newBmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int gray = (int)((0.299 * pixelData[y, x, 0]) + (0.587 * pixelData[y, x, 1]) + (0.114 * pixelData[y, x, 2]));
                    Color newColor = (gray >= threshold) ? Color.White : Color.Black;
                    newBmp.SetPixel(x, y, newColor);
                }
            }
            MessageBox.Show("Efek warna BW diterapkan!");
            return newBmp;
        }
    }
}
