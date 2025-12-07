using System;
using System.Windows.Forms;
using System.Drawing;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
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
    }
}