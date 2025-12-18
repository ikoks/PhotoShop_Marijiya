using PhotoShop_Marijiya.Logic.Histogram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop_Marijiya {
    public partial class Form1
    {
        private void linearStretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Masukkan gambar terlebih dahulu");

                return;
            }

            this.Cursor = Cursors.WaitCursor;

            // Panggil Logic Histogram Equalization
            Bitmap result = LinearStretch.ApplyLinearStretch(pixelData);

            // Update Tampilan
            pictureBox.Image = result;

            // Update Pixel Data & Refresh Grafik Histogram
            UpdatePixelDataFromPictureBox();
            RefreshHistogram();

            this.Cursor = Cursors.Default;
            MessageBox.Show("Linear Stretch berhasil diterapkan!");
        }
    }
}
