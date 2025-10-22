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

            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte R = pixelData[y, x, 0];
                    // Komponen G dan B dibuat 0 agar hasilnya hanya merah
                    bmp.SetPixel(x, y, Color.FromArgb(R, 0, 0));
                }
            }

            pictureBox.Image = bmp;
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

            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte G = pixelData[y, x, 1];
                    bmp.SetPixel(x, y, Color.FromArgb(0, G, 0));
                }
            }

            pictureBox.Image = bmp;
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

            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte B = pixelData[y, x, 2];
                    bmp.SetPixel(x, y, Color.FromArgb(0, 0, B));
                }
            }

            pictureBox.Image = bmp;
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

            int height = pixelData.GetLength(0);
            int width = pixelData.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte R = pixelData[y, x, 0];
                    byte G = pixelData[y, x, 1];
                    byte B = pixelData[y, x, 2];

                    // Rumus grayscale sederhana (rata-rata)
                    byte gray = (byte)((R + G + B) / 3);

                    bmp.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }

            pictureBox.Image = bmp;
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
        private Chart CreateHistogramChart(String title, int[] histogramData, Color color)
        {
            Chart chart = new Chart
            {
                Height = 180, // tinggi tiap chart (sesuaikan)
                Margin = new Padding(6)
            };

            ChartArea area = new ChartArea();
            // atur sedikit margin supaya label terlihat
            area.AxisX.Title = "Intensity";
            area.AxisY.Title = "Count";
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = 255;
            area.AxisX.Interval = 32; // tampil interval sumbu X
            area.AxisY.IsStartedFromZero = true;
            chart.ChartAreas.Add(area);

            Series series = new Series
            {
                ChartType = SeriesChartType.Column,
                IsVisibleInLegend = false,
                ShadowOffset = 0
            };

            // tambahkan data bins (0..255)
            for (int i = 0; i < histogramData.Length; i++)
            {
                DataPoint p = new DataPoint(i, histogramData[i]);
                series.Points.Add(p);
            }

            // styling series (warna)
            series.Color = color;

            chart.Series.Add(series);

            // judul chart
            Title t = new Title
            {
                Text = title,
                Docking = Docking.Top,
                Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold)
            };
            chart.Titles.Add(t);

            return chart;
        }

        private void ShowHistogramPanel()
        {
            // Cek apakah ada gambar
            if (pictureBox.Image == null) return;

            // Ambil Bitmap dan ukurannya langsung dari pictureBox
            Bitmap bmp = new Bitmap(pictureBox.Image);
            int height = bmp.Height;
            int width = bmp.Width;

            // inisialisasi data histogram (0..255)
            int[] histR = new int[256];
            int[] histG = new int[256];
            int[] histB = new int[256];

            // Hitung histogram dari Bitmap (pictureBox.Image) menggunakan GetPixel
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    histR[pixelColor.R]++;
                    histG[pixelColor.G]++;
                    histB[pixelColor.B]++;
                }
            }

            // Buat panel baru di sisi kanan
            histogramPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 380,
                BackColor = Color.White,
                AutoScroll = true,
            };

            // Buat container (FlowLayoutPanel) untuk menumpuk chart secara vertikal
            FlowLayoutPanel fl = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };

            // buat chart R, G, B
            Chart chartR = CreateHistogramChart("Histogram - Red", histR, Color.Red);
            Chart chartG = CreateHistogramChart("Histogram - Green", histG, Color.Green);
            Chart chartB = CreateHistogramChart("Histogram - Blue", histB, Color.Blue);

            // tambahkan ke container
            fl.Controls.Add(chartR);
            fl.Controls.Add(chartG);
            fl.Controls.Add(chartB);

            // Buat Label untuk judul panel
            Label lbl = new Label
            {
                Text = $"Histogram (Ukuran: {width} x {height})",
                Height = 26,
                Dock = DockStyle.Top,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular)
            };

            // Tambahkan elemen ke panel
            // Label
            histogramPanel.Controls.Add(lbl);
            // FlowLayoutPanel yang berisi chart
            histogramPanel.Controls.Add(fl);

            // Tambahkan panel ke Form
            this.Controls.Add(histogramPanel);
            // Bawa panel ke depan
            histogramPanel.BringToFront();
        }

        private void RefreshHistogram()
        {
            // Cek apakah panelnya sedang terbuka
            if (histogramPanel != null && this.Controls.Contains(histogramPanel))
            {
                // 1. Hapus panel yang lama
                this.Controls.Remove(histogramPanel);
                histogramPanel.Dispose();
                histogramPanel = null;

                // 2. Tampilkan panel yang baru dengan data ter-update
                ShowHistogramPanel();
            }
            // Jika panel tidak terbuka, tidak melakukan apa-apa
        }

        private void histogramImageToolStripButton_Click(object sender, EventArgs e)
        {
            // Cek apakah ada gambar
            if (pictureBox.Image == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // Jika panel sudah ada (sudah terbuka), tutup (toggle)
            if (histogramPanel != null && this.Controls.Contains(histogramPanel))
            {
                this.Controls.Remove(histogramPanel);
                histogramPanel.Dispose();
                histogramPanel = null;
                return;
            }

            // Jika panel BELUM ada, panggil fungsi pembuatnya
            ShowHistogramPanel();
        }
    }
}
