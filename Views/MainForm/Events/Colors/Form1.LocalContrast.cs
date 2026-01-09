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
        private void contrastLokalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // Minta Ukuran Kernel
            string input = Interaction.InputBox(
                "Masukkan Ukuran Kernel (Ganjil, misal: 3, 5, 7, 15).\nSemakin besar nilai, semakin luas area perhitungannya.",
                "Local Contrast Stretching",
                "3"
            );

            if (int.TryParse(input, out int kernelSize))
            {
                // Validasi harus ganjil dan > 1
                if (kernelSize < 3 || kernelSize % 2 == 0)
                {
                    MessageBox.Show("Ukuran kernel harus bilangan ganjil minimal 3 (contoh: 3, 5, 7).");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                try
                {
                    // Panggil Logic
                    Bitmap result = LocalContrast.ApplyLocalStatistics(pixelData, kernelSize);

                    // Update UI
                    pictureBox.Image = result;
                    UpdatePixelDataFromPictureBox();
                    RefreshHistogram();

                    MessageBox.Show($"Local Contrast Stretching (Kernel {kernelSize}x{kernelSize}) berhasil!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
    }
}
