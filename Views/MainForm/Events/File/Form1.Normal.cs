using System;
using System.Windows.Forms;
using PhotoShop_Marijiya.Logic.Colors;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk mengembalikan gambar ke kondisi normal
        private void normalImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Belum ada gambar yang dimuat.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tampilkan kembali gambar asli ke PictureBox
            pictureBox.Image = new Bitmap(originalImage);
            MessageBox.Show("Gambar dikembalikan ke kondisi normal!");

            // Perbarui pixelData dari gambar di PictureBox
            UpdatePixelDataFromPictureBox();

            // Refresh histogram
            RefreshHistogram();

            // Reset mode edit dan sembunyikan slider jika aktif
            sliderBar.Visible = false;
            currentMode = EditMode.None;
            pictureBox.Cursor = Cursors.Default;
            detectionColorToolStripButton.Checked = false;
        }
    }
}