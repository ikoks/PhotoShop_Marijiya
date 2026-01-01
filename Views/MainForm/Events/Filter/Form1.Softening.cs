using PhotoShop_Marijiya.Logic.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        private void softToolStripButton_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 1. Panggil Logic Softening (Gaussian Blur)
                Bitmap result = SofteningFilter.Apply(pixelData);

                // 2. Update Tampilan
                pictureBox.Image = result;

                // 3. Update Data Pixel & Histogram
                // Penting di-update agar jika di-blur lagi, efeknya bertumpuk (makin blur)
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();

                MessageBox.Show("Efek Softening berhasil diterapkan!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menerapkan Softening: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
