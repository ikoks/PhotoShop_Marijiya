using Microsoft.VisualBasic;
using PhotoShop_Marijiya.Logic.Distortion;
using System;
using System.Windows.Forms;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        private void swirlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // Minta input kekuatan
            string input = Interaction.InputBox(
                "Masukkan kekuatan pusaran (Saran: 0.01 sampai 0.05):",
                "Swirl Effect",
                "0.02"
            );

            if (string.IsNullOrEmpty(input)) return;

            if (double.TryParse(input, out double degree))
            {
                // Ambil gambar saat ini
                // (Gunakan masterBitmap jika Anda sudah menerapkan sistem Zoom untuk kualitas terbaik)
                Bitmap current = new Bitmap(pictureBox.Image);

                // Proses
                Cursor = Cursors.WaitCursor; // Ubah kursor jadi loading karena ini agak berat
                pictureBox.Image = SwirlDistortion.ApplySwirl(current, degree);
                Cursor = Cursors.Default;

                // Update Data
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
            }
            else
            {
                MessageBox.Show("Input tidak valid.");
            }
        }
    }
}