using System;
using System.Windows.Forms;
using PhotoShop_Marijiya.Logic.Colors;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk menerapkan efek black & white
        private void blackAndWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            //Kondisional mematikan trackbar
            if (currentMode == EditMode.BlackWhite)
            {
                sliderBar.Visible = false;      // Sembunyikan slider
                currentMode = EditMode.None;    // Reset mode

                return; // Selesai
            }

            // Set mode edit ke Black & White
            currentMode = EditMode.BlackWhite;

            // Tampilkan slider
            sliderBar.Visible = true;
            sliderBar.Minimum = 0;
            sliderBar.Maximum = 256;
            sliderBar.Value = 128; // Nilai tengah default
            sliderBar.TickFrequency = 10;

            // Tampilkan gambar awal
            pictureBox.Image = BWColor.ApplyBlackAndWhite(pixelData, sliderBar.Value);

            // Refresh histogram
            RefreshHistogram();

            MessageBox.Show("Geser slider untuk mengatur ambang batas Black & White.");
        }
    }
}