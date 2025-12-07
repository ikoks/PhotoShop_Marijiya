using Microsoft.VisualBasic;
using PhotoShop_Marijiya.Logic.Arithmetic;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
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
                pictureBox.Image = Multiply.ApplyMultiply(pixelData, value);

                // 3. Update state
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
            }
            else
            {
                MessageBox.Show("Input tidak valid. Harap masukkan angka.");
            }
        }
    }
}