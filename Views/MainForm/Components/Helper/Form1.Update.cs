using System;
using System.Windows.Forms;
using System.Drawing;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk memperbarui pixelData dari gambar di PictureBox
        private void UpdatePixelDataFromPictureBox()
        {
            // Cek apakah ada gambar di PictureBox
            if (pictureBox.Image == null)
            {
                pixelData = null; // Reset pixelData jika tidak ada gambar
                return;
            }

            // Konversi gambar ke array pixelData
            using (Bitmap bmp = new Bitmap(pictureBox.Image))
            {
                pixelData = ConvertBitmapToPixelData(bmp);
            }
        }
    }
}