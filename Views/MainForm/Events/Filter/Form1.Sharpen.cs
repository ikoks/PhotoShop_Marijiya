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
        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 1. Panggil Logic Sharpen
                Bitmap result = SharpenFilter.Apply(pixelData);

                // 2. Update Tampilan
                pictureBox.Image = result;
                UpdatePixelDataFromPictureBox();
                RefreshHistogram(); // Histogram pasti berubah karena kontras tepi meningkat

                MessageBox.Show("Efek Sharpen berhasil diterapkan!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menerapkan Sharpen: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
