using System;
using System.Windows.Forms;
using PhotoShop_Marijiya.Logic.Colors;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk menangani klik mouse pada PictureBox
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            Color pickedColor;

            // Hanya aktif jika mode ColorDetection sedang berjalan
            if (currentMode == EditMode.ColorDetection)
            {
                if (pixelData == null) return;

                // Pastikan klik di dalam batas gambar
                if (e.X < 0 || e.X >= pictureBox.Image.Width ||
                    e.Y < 0 || e.Y >= pictureBox.Image.Height)
                {
                    return;
                }

                // Ambil warna dari posisi piksel yang diklik
                // Penting: Ambil dari data piksel ASLI (pixelData) agar efek tidak menumpuk.
                byte R = pixelData[e.Y, e.X, 0];
                byte G = pixelData[e.Y, e.X, 1];
                byte B = pixelData[e.Y, e.X, 2];

                pickedColor = Color.FromArgb(R, G, B);

                // Terapkan deteksi warna dengan warna yang baru dipilih
                pictureBox.Image = DetectionColor.ApplyColorDetection(pixelData, pickedColor);

                // Perbarui pixelData dari gambar di PictureBox
                UpdatePixelDataFromPictureBox();

                // Refresh histogram
                RefreshHistogram();

                //mematikan fungsi color detection
                currentMode = EditMode.None;
                pictureBox.Cursor = Cursors.Default;

                MessageBox.Show($"Warna '{pickedColor.ToString()}' terdeteksi dan diterapkan.");
            }
        }

        // Method untuk mengaktifkan mode color detection
        private void detectionColorToolStripButton_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silahkan tambahkan gambar terlebih dahulu");
            }

            currentMode = EditMode.ColorDetection;
            pictureBox.Cursor = Cursors.Cross;
        }
    }
}