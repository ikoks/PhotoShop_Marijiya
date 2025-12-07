using System;
using System.Windows.Forms;
using PhotoShop_Marijiya.Logic.Colors;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk menerapkan efek warna biru
        private void blueColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            pictureBox.Image = BlueColor.ApplyBlueColor(pixelData);
            MessageBox.Show("Efek warna biru diterapkan!");

            // Perbarui pixelData dari gambar di PictureBox
            UpdatePixelDataFromPictureBox();

            // Refresh histogram
            RefreshHistogram();
        }
    }
}