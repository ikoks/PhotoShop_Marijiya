using System;
using System.Windows.Forms;
using System.Drawing;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
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
    }
}