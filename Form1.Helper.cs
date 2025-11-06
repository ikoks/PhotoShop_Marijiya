namespace PhotoShop_Marijiya
{
    public partial class Form1
    {


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
                    Bitmap combinedImage = new Bitmap(combinedWidth, combinedHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

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



    }
}