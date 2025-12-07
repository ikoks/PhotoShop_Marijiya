using System;
using System.Windows.Forms;
using PhotoShop_Marijiya.Logic.Colors;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk menerapkan efek warna hijau
        private void greenColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // Panggil kelas ImageProcessor
            pictureBox.Image = GreenColor.ApplyGreenChannel(pixelData);
            MessageBox.Show("Efek warna hijau diterapkan!");

            // Perbarui pixelData dari gambar di PictureBox
            UpdatePixelDataFromPictureBox();

            // Refresh histogram
            RefreshHistogram();
        }
    }
}