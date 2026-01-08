using System;
using System.Windows.Forms;
using PhotoShop_Marijiya.Logic.Colors;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk menangani perubahan nilai slider
        private void sliderBar_Scroll(object sender, EventArgs e)
        {
            if (pixelData == null) return;

            switch (currentMode)
            {
                case EditMode.Brightness:
                    pictureBox.Image = Brightness.ApplyBrightness(pixelData, sliderBar.Value);
                    break;

                case EditMode.BlackWhite:
                    pictureBox.Image = BWColor.ApplyBlackAndWhite(pixelData, sliderBar.Value);
                    break;

                case EditMode.ColorDetection:
                    // Panggil helper yang tadi kita buat di Form1.Detection.cs
                    ApplyDetectionUpdate();
                    break;

                case EditMode.None:
                default:
                    return;
            }

            // Perbarui pixelData dari gambar di PictureBox
            UpdatePixelDataFromPictureBox();

            // Perbarui pixelData dari gambar di PictureBox
            RefreshHistogram();
        }
    }
}