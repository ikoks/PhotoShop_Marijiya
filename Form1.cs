namespace PhotoShop_Marijiya
{
    using Microsoft.VisualBasic;
    using System;
    using System.Diagnostics.Metrics;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    public partial class Form1 : Form
    {

        private byte[,,] pixelData;
        private Bitmap originalImage;
        private Bitmap? secondLayerImage = null;
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
            // Cek apakah array pixelData sudah terisi (bukan lagi pictureBox.Image)
            if (pixelData == null)
            {
                MessageBox.Show("Tidak Ada Gambar yang diproses. Silakan Add Image terlebih dahulu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            if (pixelData == null)
            {
                MessageBox.Show("Silahkan tambahkan gambar dasar terlebih dahulu.");
                return;
            }

            // Logika Toggle Mode
            if (currentMode == EditMode.PlusImage)
            {
                currentMode = EditMode.None;
                pictureBox.Cursor = Cursors.Default;
                // plusImageToolStripButton.Checked = false; // Jika 'CheckOnClick' = true
            }
            else
            {
                currentMode = EditMode.PlusImage;
                pictureBox.Cursor = Cursors.Hand;
                // plusImageToolStripButton.Checked = true; // Jika 'CheckOnClick' = true
                MessageBox.Show("Mode Operasi Gambar Aktif. Klik kanan pada gambar untuk opsi.");
            }
        }


        // Method untuk menangani klik pada menu "Tambah" di context menu PlusImage
        private void tambahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Anda memerlukan gambar dasar terlebih dahulu.", "Error");
                return;
            }

            byte[,,] pixelData1; // Akan diisi dengan originalImage
            byte[,,]? pixelData2 = null; // Akan diisi dengan gambar kedua
            Bitmap? tempBitmap2 = null; // Penampung jika memilih manual

            try
            {
                // 1. Ambil array piksel dari gambar pertama (Original)
                pixelData1 = this.ConvertBitmapToPixelData(this.originalImage);

                // 2. CEK APAKAH ADA GAMBAR DARI DRAG & DROP
                if (secondLayerImage != null)
                {
                    // Skenario A: YA. Gunakan gambar dari Drag & Drop
                    pixelData2 = ConvertBitmapToPixelData(secondLayerImage);

                    // Hapus gambar penampung setelah dipakai
                    secondLayerImage.Dispose();
                    secondLayerImage = null;
                }
                else
                {
                    // Skenario B: TIDAK ADA. Minta pengguna memilih file
                    MessageBox.Show("Pilih gambar KEDUA (Gambar B) untuk dijumlahkan.");
                    string imagePath2 = SelectFileImage();
                    if (imagePath2 == null) return; // Pengguna membatalkan

                    tempBitmap2 = new Bitmap(imagePath2);
                    pixelData2 = ConvertBitmapToPixelData(tempBitmap2);
                }

                if (pixelData2 == null)
                {
                    throw new Exception("Gagal mengonversi gambar kedua.");
                }

                // 3. Panggil metode untuk menjumlahkan (Original + Gambar 2)
                Bitmap bmp = ImageProcessor.PlusArraysImage(pixelData1, pixelData2);

                // 4. Tampilkan hasilnya
                pictureBox.Image = bmp;

                // 5. Perbarui pixelData utama kita dengan gambar hasil penjumlahan
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
                MessageBox.Show("Gambar berhasil dijumlahkan!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat menjumlahkan gambar: " + ex.Message, "Error");
            }
            finally
            {
                // Bersihkan memori dari bitmap manual (jika ada)
                if (tempBitmap2 != null)
                {
                    tempBitmap2.Dispose();
                }
            }
        }

        // Method untuk menangani klik pada menu "Kurang" di context menu MinImage
        private void kurangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage == null) // Cek originalImage, bukan pixelData
            {
                MessageBox.Show("Anda memerlukan gambar dasar terlebih dahulu.", "Error");
                return;
            }

            byte[,,] pixelData1; // Akan diisi dengan originalImage
            byte[,,]? pixelData2 = null; // Akan diisi dengan gambar kedua
            Bitmap? tempBitmap2 = null; // Penampung jika memilih manual

            try
            {
                // 1. Ambil array piksel dari gambar pertama (Original)
                pixelData1 = this.ConvertBitmapToPixelData(this.originalImage);

                // 2. CEK APAKAH ADA GAMBAR DARI DRAG & DROP
                if (secondLayerImage != null)
                {
                    // Skenario A: YA. Gunakan gambar dari Drag & Drop
                    pixelData2 = ConvertBitmapToPixelData(secondLayerImage);

                    // Hapus gambar penampung setelah dipakai
                    secondLayerImage.Dispose();
                    secondLayerImage = null;
                }
                else
                {
                    // Skenario B: TIDAK ADA. Minta pengguna memilih file
                    MessageBox.Show("Pilih gambar KEDUA (Gambar B) untuk dikurangkan.");
                    string imagePath2 = SelectFileImage();
                    if (imagePath2 == null) return; // Pengguna membatalkan

                    tempBitmap2 = new Bitmap(imagePath2);
                    pixelData2 = ConvertBitmapToPixelData(tempBitmap2);
                }

                if (pixelData2 == null)
                {
                    throw new Exception("Gagal mengonversi gambar kedua.");
                }

                // 3. Panggil metode untuk mengurangkan (Original - Gambar 2)
                //    (Saya asumsikan Anda ingin menggunakan ApplyDifference)
                Bitmap bmp = ImageProcessor.MinArraysImage(pixelData1, pixelData2);

                // 4. Tampilkan hasilnya
                pictureBox.Image = bmp;

                // 5. Perbarui pixelData utama kita dengan gambar hasil pengurangan
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
                MessageBox.Show("Gambar berhasil dikurangkan!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat mengurangkan gambar: " + ex.Message, "Error");
            }
            finally
            {
                // Bersihkan memori dari bitmap manual (jika ada)
                if (tempBitmap2 != null)
                {
                    tempBitmap2.Dispose();
                }
            }
        }

        //method delete image
        private void deleteImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;

            if (originalImage != null)
            {
                originalImage.Dispose();
                originalImage = null;
            }

            // --- TAMBAHKAN INI ---
            if (secondLayerImage != null)
            {
                secondLayerImage.Dispose();
                secondLayerImage = null;
            }
            // --- SELESAI ---

            UpdatePixelDataFromPictureBox();


            MessageBox.Show("Kanvas telah dibersihkan.");
        }

        // method perkalian angka
        private void angkaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // 1. Minta input untuk "KALI"
            string input = Interaction.InputBox(
                "Masukkan angka PENGALI (misal: 2 atau 1.5):", // Prompt
                "Multiply", // Judul
                "2.0" // Nilai default
            );

            if (string.IsNullOrEmpty(input)) return; // Pengguna membatalkan

            double value;
            if (double.TryParse(input, out value))
            {
                // 2. Panggil fungsi 'ApplyMultiply'
                pictureBox.Image = ImageProcessor.ApplyMultiply(pixelData, value);

                // 3. Update state
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
            }
            else
            {
                MessageBox.Show("Input tidak valid. Harap masukkan angka.");
            }
        }

        //method pembagian angka
        private void angkaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // 1. Minta input untuk "BAGI"
            string input = Interaction.InputBox(
                "Masukkan angka PEMBAGI (misal: 2 atau 1.5):", // Prompt
                "Divide", // Judul
                "2.0" // Nilai default
            );

            if (string.IsNullOrEmpty(input)) return; // Pengguna membatalkan

            double value;
            if (double.TryParse(input, out value))
            {
                // 2. Cek Pembagian dengan Nol
                if (value == 0)
                {
                    MessageBox.Show("Tidak bisa membagi dengan nol.", "Error");
                    return;
                }

                // 3. Panggil fungsi 'ApplyDivide'
                pictureBox.Image = ImageProcessor.ApplyDivide(pixelData, value);

                // 4. Update state
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
            }
            else
            {
                MessageBox.Show("Input tidak valid. Harap masukkan angka.");
            }
        }

        //method perkalian iamge vs image
        private void imageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Anda memerlukan gambar dasar terlebih dahulu.", "Error");
                return;
            }

            byte[,,] pixelData1;
            byte[,,]? pixelData2 = null;
            Bitmap? tempBitmap2 = null;

            try
            {
                pixelData1 = this.ConvertBitmapToPixelData(this.originalImage);

                if (secondLayerImage != null)
                {
                    pixelData2 = ConvertBitmapToPixelData(secondLayerImage);
                    secondLayerImage.Dispose();
                    secondLayerImage = null;
                }
                else
                {
                    // --- UBAH PESAN ---
                    MessageBox.Show("Pilih gambar KEDUA (Gambar B) untuk dikalikan.");
                    string imagePath2 = SelectFileImage();
                    if (imagePath2 == null) return;

                    tempBitmap2 = new Bitmap(imagePath2);
                    pixelData2 = ConvertBitmapToPixelData(tempBitmap2);
                }

                if (pixelData2 == null)
                {
                    throw new Exception("Gagal mengonversi gambar kedua.");
                }

                // --- UBAH FUNGSI YANG DIPANGGIL ---
                Bitmap bmp = ImageProcessor.ApplyMultiplyImage(pixelData1, pixelData2);

                pictureBox.Image = bmp;
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();

                // --- UBAH PESAN ---
                MessageBox.Show("Gambar berhasil dikalikan!");
            }
            catch (Exception ex)
            {
                // --- UBAH PESAN ---
                MessageBox.Show("Error saat mengalikan gambar: " + ex.Message, "Error");
            }
            finally
            {
                if (tempBitmap2 != null)
                {
                    tempBitmap2.Dispose();
                }
            }
        }

        //method pembagian image vs image
        private void imageToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Anda memerlukan gambar dasar terlebih dahulu.", "Error");
                return;
            }

            byte[,,] pixelData1;
            byte[,,]? pixelData2 = null;
            Bitmap? tempBitmap2 = null;

            try
            {
                pixelData1 = this.ConvertBitmapToPixelData(this.originalImage);

                if (secondLayerImage != null)
                {
                    pixelData2 = ConvertBitmapToPixelData(secondLayerImage);
                    secondLayerImage.Dispose();
                    secondLayerImage = null;
                }
                else
                {
                    // --- UBAH PESAN ---
                    MessageBox.Show("Pilih gambar KEDUA (Gambar B) untuk pembagian.");
                    string imagePath2 = SelectFileImage();
                    if (imagePath2 == null) return;

                    tempBitmap2 = new Bitmap(imagePath2);
                    pixelData2 = ConvertBitmapToPixelData(tempBitmap2);
                }

                if (pixelData2 == null)
                {
                    throw new Exception("Gagal mengonversi gambar kedua.");
                }

                // --- UBAH FUNGSI YANG DIPANGGIL ---
                Bitmap bmp = ImageProcessor.ApplyDivideImage(pixelData1, pixelData2);

                pictureBox.Image = bmp;
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();

                // --- UBAH PESAN ---
                MessageBox.Show("Gambar berhasil dibagi!");
            }
            catch (Exception ex)
            {
                // --- UBAH PESAN ---
                MessageBox.Show("Error saat membagi gambar: " + ex.Message, "Error");
            }
            finally
            {
                if (tempBitmap2 != null)
                {
                    tempBitmap2.Dispose();
                }
            }
        }

        //method bitwise AND
        private void aNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Anda memerlukan gambar dasar terlebih dahulu.", "Error");
                return;
            }

            byte[,,] pixelData1;
            byte[,,]? pixelData2 = null;
            Bitmap? tempBitmap2 = null;

            try
            {
                pixelData1 = this.ConvertBitmapToPixelData(this.originalImage);

                if (secondLayerImage != null)
                {
                    pixelData2 = ConvertBitmapToPixelData(secondLayerImage);
                    secondLayerImage.Dispose();
                    secondLayerImage = null;
                }
                else
                {
                    MessageBox.Show("Pilih gambar KEDUA (Gambar B) untuk operasi AND.");
                    string imagePath2 = SelectFileImage();
                    if (imagePath2 == null) return;

                    tempBitmap2 = new Bitmap(imagePath2);
                    pixelData2 = ConvertBitmapToPixelData(tempBitmap2);
                }

                if (pixelData2 == null)
                {
                    throw new Exception("Gagal mengonversi gambar kedua.");
                }

                Bitmap bmp = ImageProcessor.ApplyBitwiseAND(pixelData1, pixelData2);

                pictureBox.Image = bmp;
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
                MessageBox.Show("Operasi AND berhasil!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat operasi AND: " + ex.Message, "Error");
            }
            finally
            {
                if (tempBitmap2 != null)
                {
                    tempBitmap2.Dispose();
                }
            }
        }

        //method bitwise OR
        private void oRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Anda memerlukan gambar dasar terlebih dahulu.", "Error");
                return;
            }

            byte[,,] pixelData1;
            byte[,,]? pixelData2 = null;
            Bitmap? tempBitmap2 = null;

            try
            {
                pixelData1 = this.ConvertBitmapToPixelData(this.originalImage);

                if (secondLayerImage != null)
                {
                    pixelData2 = ConvertBitmapToPixelData(secondLayerImage);
                    secondLayerImage.Dispose();
                    secondLayerImage = null;
                }
                else
                {
                    MessageBox.Show("Pilih gambar KEDUA (Gambar B) untuk operasi OR.");
                    string imagePath2 = SelectFileImage();
                    if (imagePath2 == null) return;

                    tempBitmap2 = new Bitmap(imagePath2);
                    pixelData2 = ConvertBitmapToPixelData(tempBitmap2);
                }

                if (pixelData2 == null)
                {
                    throw new Exception("Gagal mengonversi gambar kedua.");
                }

                Bitmap bmp = ImageProcessor.ApplyBitwiseOR(pixelData1, pixelData2);

                pictureBox.Image = bmp;
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
                MessageBox.Show("Operasi OR berhasil!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat operasi AND: " + ex.Message, "Error");
            }
            finally
            {
                if (tempBitmap2 != null)
                {
                    tempBitmap2.Dispose();
                }
            }

        }

        //method bitwise XOR
        private void xORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Anda memerlukan gambar dasar terlebih dahulu.", "Error");
                return;
            }

            byte[,,] pixelData1;
            byte[,,]? pixelData2 = null;
            Bitmap? tempBitmap2 = null;

            try
            {
                pixelData1 = this.ConvertBitmapToPixelData(this.originalImage);

                if (secondLayerImage != null)
                {
                    pixelData2 = ConvertBitmapToPixelData(secondLayerImage);
                    secondLayerImage.Dispose();
                    secondLayerImage = null;
                }
                else
                {
                    MessageBox.Show("Pilih gambar KEDUA (Gambar B) untuk operasi XOR.");
                    string imagePath2 = SelectFileImage();
                    if (imagePath2 == null) return;

                    tempBitmap2 = new Bitmap(imagePath2);
                    pixelData2 = ConvertBitmapToPixelData(tempBitmap2);
                }

                if (pixelData2 == null)
                {
                    throw new Exception("Gagal mengonversi gambar kedua.");
                }

                Bitmap bmp = ImageProcessor.ApplyBitwiseXOR(pixelData1, pixelData2);

                pictureBox.Image = bmp;
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
                MessageBox.Show("Operasi XOR berhasil!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat operasi AND: " + ex.Message, "Error");
            }
            finally
            {
                if (tempBitmap2 != null)
                {
                    tempBitmap2.Dispose();
                }
            }
        }
    }
}
