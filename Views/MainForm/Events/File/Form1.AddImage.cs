using System;
using System.Windows.Forms;
using System.Drawing;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
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
    }
}