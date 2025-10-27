namespace PhotoShop_Marijiya
{
    using System;
    using System.Diagnostics.Metrics;
    using System.Windows.Forms.DataVisualization.Charting;
    public partial class Form1 : Form
    {

        // --- TAMBAHAN ---
        // Deklarasi array 3D sebagai field.
        // Ini bisa diakses oleh SEMUA method di dalam class Form1.
        // Format: [tinggi, lebar, 3]
        // [y, x, 0] = Red
        // [y, x, 1] = Green
        // [y, x, 2] = Blue
        private byte[,,] pixelData;
        private Bitmap originalImage;
        private Panel histogramPanel;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void addImageToolStripButton_Click(object sender, EventArgs e)
        {
            // Membuat sebuah instance dari OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Mengatur filter untuk tipe file gambar yang didukung
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff|All Files (*.*)|*.*";
            openFileDialog.Title = "Select an Image File";

            // Menampilkan dialog dan memeriksa apakah pengguna menekan OK
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Memuat gambar yang dipilih ke dalam PictureBox
                    string selectedImage = openFileDialog.FileName;
                    pictureBox.Image = new System.Drawing.Bitmap(selectedImage);

                    // Simpan salinan gambar asli
                    originalImage = new Bitmap(pictureBox.Image);

                    if (pictureBox.Image != null)
                    {
                        //Memasukan width dan height pada variabel
                        int imgHeight = pictureBox.Image.Height;
                        int imgWidth = pictureBox.Image.Width;

                        // --- TAMBAHAN DIMULAI ---
                        // 1. Inisialisasi array 3D sesuai ukuran gambar
                        pixelData = new byte[imgHeight, imgWidth, 3];

                        // 2. Konversi gambar ke Bitmap agar bisa dibaca pixelnya
                        Bitmap bmp = new Bitmap(pictureBox.Image);

                        // 3. Looping untuk mengisi array pixelData
                        for (int y = 0; y < imgHeight; y++)
                        {
                            for (int x = 0; x < imgWidth; x++)
                            {
                                Color pixelColor = bmp.GetPixel(x, y);
                                pixelData[y, x, 0] = pixelColor.R; // Simpan nilai Red
                                pixelData[y, x, 1] = pixelColor.G; // Simpan nilai Green
                                pixelData[y, x, 2] = pixelColor.B; // Simpan nilai Blue
                            }
                        }
                        // --- TAMBAHAN SELESAI ---

                        string message = $"Gambar berhasil dimuat DAN diproses ke array!\n\n" +
                                         $"File: {System.IO.Path.GetFileName(selectedImage)}\n" +
                                         $"Ukuran: {imgWidth} x {imgHeight} pixels";

                        MessageBox.Show(message);
                    }
                    else
                    {
                        MessageBox.Show("gagal memuat objek");
                    }

                    // Opsional: Perbarui Text form dengan nama file yang dimuat
                    this.Text = "PhotoShop Mariji - " + Path.GetFileName(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    // Menampilkan pesan error jika ada masalah saat memuat gambar
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveImageTxtToolStripButton_Click(object sender, EventArgs e)
        {
            // --- MODIFIKASI ---
            // Cek apakah array pixelData sudah terisi (bukan lagi pictureBox.Image)
            if (pixelData == null)
            {
                MessageBox.Show("Tidak Ada Gambar yang diproses. Silakan Add Image terlebih dahulu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // --- SELESAI MODIFIKASI ---

            // --- DIHAPUS ---
            // Bitmap bmp = new Bitmap(pictureBox.Image); // Tidak perlu lagi, data sudah di array

            // Membuat dialog untuk menyimpan file .txt
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt",
                Title = "Save Image as Text File",
                FileName = "Ekstraksi_Gambar.txt"
            };

            // Memeriksa apakah pengguna meklik save
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // --- MODIFIKASI ---
                    // Ambil ukuran dari array
                    int height = pixelData.GetLength(0); // Dimensi pertama (tinggi)
                    int width = pixelData.GetLength(1);  // Dimensi kedua (lebar)

                    using (StreamWriter sw = new StreamWriter(saveDialog.FileName))
                    {
                        sw.WriteLine("Ekstraksi Gambar (dari Array)");
                        sw.WriteLine($"Ukuran Gambar: {width} {height}");
                        sw.WriteLine("Format Gambar: R, G, B");
                        sw.WriteLine("-----------------------------------------------------------------------------------------------------");

                        // Looping setiap pixel DARI ARRAY
                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < width; x++)
                            {
                                // Ambil nilai R, G, B dari array
                                byte R = pixelData[y, x, 0];
                                byte G = pixelData[y, x, 1];
                                byte B = pixelData[y, x, 2];

                                sw.Write($"[{R},{G},{B}]");
                            }
                            sw.WriteLine(); // pindah baris tiap baris pixel
                        }
                    }
                    // --- SELESAI MODIFIKASI ---
                    MessageBox.Show("File berhasil di simpan dari array", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    // Menampilkan pesan error jika ada masalah saat menyimpan file
                    MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void redColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // Panggil kelas ImageProcessor
            pictureBox.Image = ImageProcessor.ApplyRedChannel(pixelData);
            MessageBox.Show("Efek warna merah diterapkan!");

            // Refresh histogram
            RefreshHistogram();
        }

        private void greenColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // Panggil kelas ImageProcessor
            pictureBox.Image = ImageProcessor.ApplyGreenChannel(pixelData);
            MessageBox.Show("Efek warna hijau diterapkan!");

            // Refresh histogram
            RefreshHistogram();
        }

        private void blueColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            pictureBox.Image = ImageProcessor.ApplyBlueChannel(pixelData);
            MessageBox.Show("Efek warna biru diterapkan!");

            // Refresh histogram
            RefreshHistogram();
        }

        private void grayscaleColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            pictureBox.Image = ImageProcessor.ApplyGrayscale(pixelData);
            MessageBox.Show("Efek grayscale diterapkan!");

            // Refresh histogram
            RefreshHistogram();
        }

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

            // Refresh histogram
            RefreshHistogram();
        }

        // Helper untuk membuat chart histogram
        private void ShowHistogramPanel()
        {
            if (pictureBox.Image == null) return;

            // Panggil kelas HistogramManager untuk membuat panel
            histogramPanel = HistogramManager.CreateHistogramPanel(new Bitmap(pictureBox.Image));

            // Form1 tinggal menambahkannya
            this.Controls.Add(histogramPanel);
            histogramPanel.BringToFront();
        }


        private void RefreshHistogram()
        {
            if (histogramPanel != null && this.Controls.Contains(histogramPanel))
            {
                this.Controls.Remove(histogramPanel);
                histogramPanel.Dispose();
                histogramPanel = null;
                ShowHistogramPanel();
            }
        }

        private void histogramImageToolStripButton_Click(object sender, EventArgs e)
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

        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cek apakah ada gambar
            if (pictureBox.Image == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // 2. Panggil kelas ImageProcessor untuk melakukan pekerjaan
            pictureBox.Image = ImageProcessor.ApplyNegative(pixelData);

            MessageBox.Show("Efek negatif diterapkan!");

            RefreshHistogram();
        }
    }
}
