using System;
using System.Windows.Forms;
using System.Drawing;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method button histogram
        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cek apakah ada gambar
            if (pictureBox.Image == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // Logika toggle tidak berubah
            if (histogramPanel != null && this.Controls.Contains(histogramPanel))
            {
                this.Controls.Remove(histogramPanel);
                histogramPanel.Dispose();
                histogramPanel = null;
                return;
            }

            // Panggil fungsi pembuat yang baru
            ShowHistogramPanel();
        }
    }
}