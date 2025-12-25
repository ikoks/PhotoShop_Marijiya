using PhotoShop_Marijiya.Logic.Histogram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        private void histogramEqualizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            // Panggil Logic Histogram Equalization
            Bitmap result = HistogramEqualization.ApplyHistogramEqualization(pixelData);

            // Update Tampilan
            pictureBox.Image = result;

            // Update Pixel Data & Refresh Grafik Histogram
            UpdatePixelDataFromPictureBox();
            RefreshHistogram();

            this.Cursor = Cursors.Default;
            MessageBox.Show("Histogram Equalization berhasil diterapkan!");
        }
    }
}
