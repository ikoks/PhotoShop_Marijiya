using System;
using System.Windows.Forms;
using PhotoShop_Marijiya.Logic.Colors;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk menerapkan efek negatif
        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cek apakah ada gambar
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // 2. Panggil kelas ImageProcessor untuk melakukan pekerjaan
            pictureBox.Image = NegativeColor.ApplyNegative(pixelData);

            MessageBox.Show("Efek negatif diterapkan!");

            // Perbarui pixelData dari gambar di PictureBox
            UpdatePixelDataFromPictureBox();

            // Refresh histogram
            RefreshHistogram();
        }
    }
}