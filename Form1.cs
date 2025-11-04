namespace PhotoShop_Marijiya
{
    using System;
    using System.Diagnostics.Metrics;
    using System.Windows.Forms.DataVisualization.Charting;
    public partial class Form1 : Form
    {

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

        // Enum untuk mode edit
        private enum EditMode
        {
            None,
            Brightness,
            BlackWhite,
            ColorDetection,
            PlusImage
        }

        // Variabel untuk menyimpan mode edit saat ini
        private EditMode currentMode = EditMode.None;


        // Method untuk menambahkan gambar
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

        // Method untuk menyimpan data gambar ke file .txt
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

        // Method untuk menerapkan efek warna merah
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

        // Method untuk menerapkan efek warna hijau
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

        // Method untuk menerapkan efek warna biru
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

        // Method untuk menerapkan efek grayscale
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

        // Helper untuk menreset histogram
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

        // Method untuk menampilkan histogram
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
            pictureBox.Image = ImageProcessor.ApplyNegative(pixelData);

            MessageBox.Show("Efek negatif diterapkan!");

            RefreshHistogram();
        }

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
            pictureBox.Image = ImageProcessor.ApplyBlackAndWhite(pixelData, sliderBar.Value);
            RefreshHistogram();

            MessageBox.Show("Geser slider untuk mengatur ambang batas Black & White.");
        }

        // Method untuk mengaktifkan mode brightness
        private void brightnestoolStripButton_Click(object sender, EventArgs e)
        {
            // Cek apakah ada gambar
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            //Kondisional mematikan trackbar
            if (currentMode == EditMode.Brightness)
            {
                sliderBar.Visible = false;      // Sembunyikan slider
                currentMode = EditMode.None;    // Reset mode

                return; // Selesai
            }

            currentMode = EditMode.Brightness;

            // Tampilkan slider dan atur propertinya
            sliderBar.Visible = true;
            sliderBar.Minimum = -255;   // Atur rentang
            sliderBar.Maximum = 255;
            sliderBar.Value = 0;        // Atur nilai default
            sliderBar.TickFrequency = 10;

            pictureBox.Image = ImageProcessor.ApplyBrightness(pixelData, sliderBar.Value);

        }

        private void sliderBar_Scroll(object sender, EventArgs e)
        {
            if (pixelData == null) return;

            int value = sliderBar.Value;

            switch (currentMode)
            {
                case EditMode.Brightness:
                    pictureBox.Image = ImageProcessor.ApplyBrightness(pixelData, value);
                    break;

                case EditMode.BlackWhite:
                    pictureBox.Image = ImageProcessor.ApplyBlackAndWhite(pixelData, value);
                    break;

                case EditMode.None:
                default:
                    return;
            }

            RefreshHistogram();
        }

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
                pictureBox.Image = ImageProcessor.ApplyColorDetection(pixelData, pickedColor);
                RefreshHistogram();

                //mematikan fungsi color detection
                currentMode = EditMode.None;
                pictureBox.Cursor = Cursors.Default;

                MessageBox.Show($"Warna '{pickedColor.ToString()}' terdeteksi dan diterapkan.");
            }
        }

        private void detectionColorToolStripButton_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silahkan tambahkan gambar terlebih dahulu");
            }

            currentMode = EditMode.ColorDetection;
            pictureBox.Cursor = Cursors.Cross;
        }

        private void plusImagePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            // Hanya aktif jika mode PlusImage sedang berjalan
            if (currentMode == EditMode.PlusImage)
            {
                // Pastikan klik di dalam batas gambar
                if (e.Button == MouseButtons.Right)
                {
                    // Tampilkan context menu
                    plusImageContextMenuStrip.Show(pictureBox, e.Location);
                }
            }
        }

        private void plusImageToolStripButton_Click(object sender, EventArgs e)
        {
            // Cek apakah ada gambar
            if (pixelData == null)
            {
                MessageBox.Show("Silahkan tambahkan gambar terlebih dahulu");
            }

            currentMode = EditMode.PlusImage;
            pictureBox.Cursor = Cursors.Default;
        }
    }
}
