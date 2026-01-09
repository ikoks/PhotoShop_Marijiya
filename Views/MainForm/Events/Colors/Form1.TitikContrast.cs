using Microsoft.VisualBasic;
using PhotoShop_Marijiya.Logic.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        private void contrastTitikStripTextBox1_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // Input Nilai Batas Bawah (rMin)
            string inputMin = Interaction.InputBox(
                "Masukkan batas bawah intensitas (rMin).\nPixel di bawah nilai ini akan menjadi hitam (0).",
                "Contrast Stretching",
                "50" // Default value
            );
            if (string.IsNullOrEmpty(inputMin)) return;

            // Input Nilai Batas Atas (rMax)
            string inputMax = Interaction.InputBox(
                "Masukkan batas atas intensitas (rMax).\nPixel di atas nilai ini akan menjadi putih (255).",
                "Contrast Stretching",
                "200" // Default value
            );
            if (string.IsNullOrEmpty(inputMax)) return;

            // Validasi & Eksekusi
            if (int.TryParse(inputMin, out int rMin) && int.TryParse(inputMax, out int rMax))
            {
                if (rMin >= rMax)
                {
                    MessageBox.Show("Nilai rMin harus lebih kecil dari rMax.", "Error");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                try
                {
                    // Panggil Logic
                    Bitmap result = TitikContrast.ApplyContrastTitik(pixelData, rMin, rMax);

                    // Update UI
                    pictureBox.Image = result;
                    UpdatePixelDataFromPictureBox();
                    RefreshHistogram();

                    MessageBox.Show("Contrast Stretching berhasil diterapkan!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                MessageBox.Show("Input harus berupa angka bulat.");
            }
        }
    }
}
