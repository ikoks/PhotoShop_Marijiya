namespace PhotoShop_Marijiya
{
    public partial class Form1 : Form
    {
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

                    if (pictureBox.Image != null)
                    {
                        int imgHeight = pictureBox.Image.Height;
                        int imgWidth = pictureBox.Image.Width;

                        string message = $"Gambar berhasil dimuat!\n\n" +
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
            // Cek apakah ada gambar yang dimuat di PictureBox
            if (pictureBox.Image == null)
            {
                MessageBox.Show("Tidak Ada Gambar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mengubah gambar menjadi bitmap supaya bisa diakses pixel-nya
            Bitmap bmp = new Bitmap(pictureBox.Image);

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
                    using (StreamWriter sw = new StreamWriter(saveDialog.FileName))
                    {
                        // Menulis dimensi gambar di baris pertama
                        sw.WriteLine("Ekstraksi Gambar");
                        sw.WriteLine($"Ukuran Gambar: {bmp.Width} {bmp.Height}");
                        sw.WriteLine("Format Gambar: R, G, B");
                        sw.WriteLine("-----------------------------------------------------------------------------------------------------");
                        // Looping setiap pixel dan menulis nilai RGB-nya ke file
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            for (int x = 0; x < bmp.Width; x++)
                            {
                                Color pixelColor = bmp.GetPixel(x, y);
                                sw.WriteLine($"{pixelColor.R} {pixelColor.G} {pixelColor.B}");
                            }
                            sw.WriteLine(); // pindah baris tiap baris pixel
                        }
                    }
                    MessageBox.Show("File berhasil di simpan", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Menampilkan pesan error jika ada masalah saat menyimpan file
                    MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
