using Microsoft.VisualBasic;
using PhotoShop_Marijiya.Logic.Arithmetic;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
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
                pictureBox.Image = Divide.ApplyDivide(pixelData, value);

                // 4. Update state
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