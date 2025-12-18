using System;
using System.Windows.Forms;
using System.Drawing;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
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