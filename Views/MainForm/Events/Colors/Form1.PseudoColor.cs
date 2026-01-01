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
        private void semuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 1. Panggil Logic Pseudocolor
                Bitmap result = PseudoColor.ApplyPseudocolor(pixelData);

                // 2. Update Tampilan
                pictureBox.Image = result;

                // 3. Update Data Pixel & Histogram
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();

                MessageBox.Show("Pewarnaan Semu (Pseudocolor) berhasil diterapkan!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menerapkan Pseudocolor: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
