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
            if (currentMode == EditMode.ColorDetection)
            {
                if (pixelData == null || pictureBox.Image == null) return;

                // Validasi koordinat agar tidak error jika klik di luar gambar (jika SizeMode AutoSize/Center)
                // Jika PictureBox SizeMode = Zoom, koordinat harus dikalibrasi. 
                // Untuk simplifikasi (StretchImage/AutoSize), kita pakai e.X e.Y langsung.

                if (e.X >= 0 && e.X < pictureBox.Image.Width && e.Y >= 0 && e.Y < pictureBox.Image.Height)
                {
                    // 1. AMBIL WARNA DARI ARRAY ASLI (Bukan dari PictureBox yang mungkin sudah diedit)
                    // Mengambil dari pixelData menjamin kita mengambil warna murni
                    byte r = pixelData[e.Y, e.X, 0];
                    byte g = pixelData[e.Y, e.X, 1];
                    byte b = pixelData[e.Y, e.X, 2];

                    this.selectedDetectionColor = Color.FromArgb(r, g, b);

                    // 2. Jalankan Logika Deteksi
                    ApplyDetectionUpdate();
                }
            }
        }

        // Method untuk mengaktifkan mode color detection
        private void detectionColorToolStripButton_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silahkan tambahkan gambar terlebih dahulu");
                return;
            }

            // Jika tombol ditekan lagi, matikan mode
            if (currentMode == EditMode.ColorDetection)
            {
                currentMode = EditMode.None;
                sliderBar.Visible = false;
                pictureBox.Cursor = Cursors.Default;
                MessageBox.Show("Mode Deteksi Warna Dinonaktifkan.");
            }
            else
            {
                currentMode = EditMode.ColorDetection;
                pictureBox.Cursor = Cursors.Cross; // Ubah kursor jadi tanda tambah

                // Reset Slider
                sliderBar.Visible = true;
                sliderBar.Minimum = 0;
                sliderBar.Maximum = 200; // Toleransi jarak warna (0-200 cukup)
                sliderBar.Value = 50;    // Nilai default toleransi

                MessageBox.Show("Mode Deteksi Warna Aktif.\n\n1. KLIK warna pada gambar.\n2. GESER slider untuk mengatur toleransi.");
            }
        }

        //helper
        private void ApplyDetectionUpdate()
        {
            // Cek apakah sudah ada warna yang dipilih
            if (selectedDetectionColor == Color.Empty) return;

            this.Cursor = Cursors.WaitCursor;

            // Ambil nilai toleransi dari Slider
            int tolerance = sliderBar.Value;

            // Panggil Logic
            Bitmap result = DetectionColor.ApplyDetection(pixelData, selectedDetectionColor, tolerance);

            pictureBox.Image = result;
        
            this.Cursor = Cursors.Default;
        }
    }
}