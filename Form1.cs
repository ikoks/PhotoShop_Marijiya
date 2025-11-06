namespace PhotoShop_Marijiya
{
    using System;
    using System.Diagnostics.Metrics;
    using System.Windows.Forms;
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

        // Method untuk mengonversi Bitmap ke array 3D pixelData
        private byte[,,] ConvertBitmapToPixelData(Bitmap bmp)
        {
            int height = bmp.Height; // Mendapatkan tinggi gambar
            int width = bmp.Width; // Mendapatkan lebar gambar

            // Inisialisasi array 3D untuk menyimpan data pixel
            byte[,,] data = new byte[height, width, 3];

            // Gunakan Bitmap sementara untuk membaca pixel
            using (Bitmap tempBmp = new Bitmap(bmp))
            {
                // Salin data pixel ke array
                for (int y = 0; y < height; y++)
                {
                    // Looping setiap kolom
                    for (int x = 0; x < width; x++)
                    {
                        Color pixelColor = tempBmp.GetPixel(x, y); // Mendapatkan warna pixel
                        data[y, x, 0] = pixelColor.R; // Simpan nilai Red
                        data[y, x, 1] = pixelColor.G; // Simpan nilai Green
                        data[y, x, 2] = pixelColor.B; // Simpan nilai Blue
                    }
                }
            }
            return data;
        }
 
        // Method untuk memperbarui pixelData dari gambar di PictureBox
        private void UpdatePixelDataFromPictureBox()
        {
            // Cek apakah ada gambar di PictureBox
            if (pictureBox.Image == null)
            {
                pixelData = null; // Reset pixelData jika tidak ada gambar
                return;
            }

            // Konversi gambar ke array pixelData
            using (Bitmap bmp = new Bitmap(pictureBox.Image))
            {
                pixelData = ConvertBitmapToPixelData(bmp);
            }
        }

        // Method untuk menampilkan dialog pemilihan file gambar
        private string SelectFileImage()
        {
            // Membuat sebuah instance dari OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Mengatur filter untuk tipe file gambar yang didukung
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff|All Files (*.*)|*.*";
            openFileDialog.Title = "Select an Image File";

            // Menampilkan dialog dan memeriksa apakah pengguna menekan OK
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        // Method untuk memuat gambar baru
        private void loadNewImage(string imagePath)
        {
            try
            {
                // Memuat gambar yang dipilih ke dalam PictureBox
                pictureBox.Image = new System.Drawing.Bitmap(imagePath);

                if (originalImage != null)
                {
                    originalImage.Dispose(); // Dispose gambar lama jika ada
                }

                originalImage = new Bitmap(pictureBox.Image); // Simpan salinan gambar asli

                // Panggil helper baru untuk update originalImage dan pixelData
                UpdatePixelDataFromPictureBox();

                // Tampilkan pesan sukses
                if (pixelData != null)
                {
                    string message = $"Gambar berhasil dimuat dan diproses ke array!\n\n" +
                                     $"File: {System.IO.Path.GetFileName(imagePath)}\n" +
                                     $"Ukuran: {pixelData.GetLength(1)} x {pixelData.GetLength(0)} pixels";

                    MessageBox.Show(message);
                }
                else
                {
                    MessageBox.Show("gagal memuat objek");
                }

                // Opsional: Perbarui Text form dengan nama file yang dimuat
                this.Text = "PhotoShop Mariji - " + Path.GetFileName(imagePath);
            }
            catch (Exception ex)
            {
                // Menampilkan pesan error jika ada masalah saat memuat gambar
                MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method untuk menambahkan gambar sebagai layer di atas gambar yang ada
        private void addImageLayer(string imagePath)
        {
            try
            {
                // dapatkan salinan gambar lama
                using (Bitmap oldImage = new Bitmap(pictureBox.Image))

                // Memuat gambar yang dipilih ke dalam PictureBox
                using (Bitmap newImage = new Bitmap(imagePath))
                {
                    // Gabungkan gambar lama dan baru
                    int combinedWidth = Math.Max(oldImage.Width, newImage.Width);
                    int combinedHeight = Math.Max(oldImage.Height, newImage.Height);

                    // Membuat bitmap baru untuk menampung gabungan
                    Bitmap combinedImage = new Bitmap(combinedWidth, combinedHeight);

                    using (Graphics g = Graphics.FromImage(combinedImage))
                    {
                        // Menggambar gambar lama di posisi (0,0)
                        g.DrawImage(oldImage, 0, 0, oldImage.Width, oldImage.Height);
                        // Menggambar gambar baru di posisi (0,0)
                        g.DrawImage(newImage, 0, 0, newImage.Width, newImage.Height);
                    }

                    pictureBox.Image = combinedImage;
                }
                // oldImage dan newImage otomatis di-dispose karena menggunakan 'using'

                // Panggil helper baru untuk update originalImage dan pixelData
                UpdatePixelDataFromPictureBox();

                if (pixelData != null)
                {
                    // Tampilkan pesan sukses
                    string message = $"Gambar baru berhasil ditambahkan!\n\n" +
                                     $"File: {System.IO.Path.GetFileName(imagePath)}\n" +
                                     $"Ukuran: {pixelData.GetLength(1)} x {pixelData.GetLength(0)} pixels";
                    MessageBox.Show(message);
                }
                else
                {
                    // Jika gagal menggabungkan, tampilkan pesan error
                    MessageBox.Show("gagal menggabungkan objek");
                }

                // Perbarui Text form dengan nama file yang dimuat
                this.Text = "PhotoShop Mariji - " + Path.GetFileName(imagePath);
            }
            catch (Exception ex)
            {
                // Menampilkan pesan error jika ada masalah saat memuat gambar
                MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method untuk menambahkan gambar
        private void addImageToolStripButton_Click(object sender, EventArgs e)
        {
            // Membuat sebuah instance dari OpenFileDialog
            string imagePath = SelectFileImage();

            // Menampilkan dialog dan memeriksa apakah pengguna menekan OK
            if (imagePath != null)
            {
                // Panggil method loadNewImage untuk memuat dan memproses gambar
                loadNewImage(imagePath);
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

            // Perbarui pixelData dari gambar di PictureBox
            UpdatePixelDataFromPictureBox();

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

            // Perbarui pixelData dari gambar di PictureBox
            UpdatePixelDataFromPictureBox();

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

            // Perbarui pixelData dari gambar di PictureBox
            UpdatePixelDataFromPictureBox();

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

            // Perbarui pixelData dari gambar di PictureBox
            UpdatePixelDataFromPictureBox();

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

            // Perbarui pixelData dari gambar di PictureBox
            UpdatePixelDataFromPictureBox();

            // Refresh histogram
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

            // Refresh histogram
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

        // Method untuk menangani perubahan nilai slider
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

            // Perbarui pixelData dari gambar di PictureBox
            UpdatePixelDataFromPictureBox();

            // Perbarui pixelData dari gambar di PictureBox
            RefreshHistogram();
        }

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
                pictureBox.Image = ImageProcessor.ApplyColorDetection(pixelData, pickedColor);

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

        // Method untuk menangani klik mouse pada PictureBox di mode PlusImage
        private void plusImagePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            // Hanya aktif jika mode PlusImage sedang berjalan
            if (currentMode == EditMode.PlusImage)
            {
                // Pastikan klik di dalam batas gambar
                if (e.Button == MouseButtons.Right)
                {
                    // Tampilkan context menu
                    plusImageContextMenuStrip.Show(panelPictureBox, e.Location);
                    plusImageContextMenuStrip.Show(pictureBox, e.Location);
                }
            }
        }

        // Method untuk mengaktifkan mode plus image
        private void plusImageToolStripButton_Click(object sender, EventArgs e)
        {
            // Cek apakah ada gambar
            if (pixelData == null)
            {
                MessageBox.Show("Silahkan tambahkan gambar terlebih dahulu");
            }
            else
            {
                string imagePath = SelectFileImage();

                // Menampilkan dialog dan memeriksa apakah pengguna menekan OK
                if (imagePath != null)
                {
                    // Panggil method addImageLayer untuk menambahkan gambar sebagai layer
                    addImageLayer(imagePath);
                }

                // Cek apakah sudah dalam mode PlusImage
                if (currentMode == EditMode.PlusImage)
                {
                    // Jika sudah dalam mode PlusImage, matikan mode
                    currentMode = EditMode.None;
                    pictureBox.Cursor = Cursors.Default;

                    return;
                }

                // Aktifkan mode PlusImage
                currentMode = EditMode.PlusImage;
            }
        }

        // Daftar format gambar yang didukung
        private readonly List<string> supportedImageFormats = new()
        {
            ".bmp", ".jpg", ".jpeg", ".png", ".gif", ".tif", ".tiff"
        };

        // Event handler untuk drag enter pada panelPictureBox
        private void panelPictureBox_DragEnter(object sender, DragEventArgs e)
        {
            // Cek apakah data yang di-drag adalah file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // Event handler untuk drag drop pada panelPictureBox
        private void panelPictureBox_DragDrop(object sender, DragEventArgs e)
        {
            // Ambil file yang di-drag
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                // Ambil path file pertama
                string filePath = files[0];

                // Cek ekstensi file
                string fileExtension = Path.GetExtension(filePath).ToLower();

                // Cek apakah format file didukung
                if (supportedImageFormats.Contains(fileExtension))
                {
                    // Cek apakah pixelData sudah ada atau belum
                    if (pixelData == null)
                    {
                        // Jika belum ada gambar, load sebagai gambar baru
                        loadNewImage(filePath);
                    }
                    else
                    {
                        // Jika sudah ada gambar, tambahkan sebagai layer
                        addImageLayer(filePath);

                        // Cek apakah sudah dalam mode PlusImage
                        if (currentMode == EditMode.PlusImage)
                        {
                            // Jika sudah dalam mode PlusImage, matikan mode
                            currentMode = EditMode.None;
                            pictureBox.Cursor = Cursors.Default;

                            return;
                        }

                        // Aktifkan mode PlusImage
                        currentMode = EditMode.PlusImage;
                    }
                }
                else
                {
                    MessageBox.Show("Format file tidak didukung. Silakan pilih file gambar yang valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Event handler untuk drag enter pada pictureBox
        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            panelPictureBox_DragEnter(sender, e);
        }

        // Event handler untuk drag drop pada pictureBox
        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            panelPictureBox_DragDrop(sender, e);
        }

        // Method untuk menangani klik pada menu "Tambah" di context menu PlusImage
        private void tambahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cek apakah kita punya dua gambar untuk dijumlahkan
            if (pixelData == null || originalImage == null)
            {
                MessageBox.Show("Anda memerlukan setidaknya satu gambar dasar untuk melakukan penjumlahan", "Error");
                return;
            }

            if (this.ConvertBitmapToPixelData(this.originalImage) == null)
            {
                MessageBox.Show("Gagal mengonversi gambar asli", "Error");
            }

            try
            {
                // Ambil array pixelData dari gambar pertama
                byte[,,] pixelData1 = this.ConvertBitmapToPixelData(this.originalImage);

                // Ambil array pixelData dari gambar kedua
                byte[,,] pixelData2 = this.pixelData;

                // Panggil metode AddArraysImage untuk menjumlahkan kedua array
                Bitmap bmp = ImageProcessor.PlusArraysImage(pixelData1, pixelData2);

                // Tampilkan hasil penjumlahan di pictureBox
                pictureBox.Image = bmp;

                // Memperbarui pixelData dari gambar hasil penjumlahan
                UpdatePixelDataFromPictureBox();

                // Refresh histogram
                RefreshHistogram();

                MessageBox.Show("Gambar berhasil dijumlahkan!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat menjumlahkan gambar: " + ex.Message, "Error");
            }
        }

        // Method untuk menangani klik pada menu "Kurang" di context menu MinImage
        private void kurangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cek apakah kita punya dua gambar untuk dijumlahkan
            if (pixelData == null || originalImage == null)
            {
                MessageBox.Show("Anda memerlukan setidaknya satu gambar dasar untuk melakukan penjumlahan", "Error");
                return;
            }

            if (this.ConvertBitmapToPixelData(this.originalImage) == null)
            {
                MessageBox.Show("Gagal mengonversi gambar asli", "Error");
            }

            try
            {
                // Ambil array pixelData dari gambar pertama
                byte[,,] pixelData1 = this.ConvertBitmapToPixelData(this.originalImage);

                // Ambil array pixelData dari gambar kedua
                byte[,,] pixelData2 = this.pixelData;

                // Panggil metode AddArraysImage untuk menjumlahkan kedua array
                Bitmap bmp = ImageProcessor.MinArraysImage(pixelData1, pixelData2);

                // Tampilkan hasil penjumlahan di pictureBox
                pictureBox.Image = bmp;

                // Memperbarui pixelData dari gambar hasil penjumlahan
                UpdatePixelDataFromPictureBox();

                // Refresh histogram
                RefreshHistogram();

                MessageBox.Show("Gambar berhasil dikurangkan!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat mengurangkan gambar: " + ex.Message, "Error");
            }
        }
    }
}
