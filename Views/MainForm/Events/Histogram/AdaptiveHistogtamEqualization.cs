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
        private void adaptiveHistogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Masukkan gambar terlebih dahulu");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            Bitmap result = AdaptiveHistogramEqualization.ApplyAHE(pixelData,tileSize: 32 );

            pictureBox.Image = result;

            RefreshHistogram();

            this.Cursor = Cursors.Default;
            MessageBox.Show("Adaptive Histogram Equalization berhasil!");
        }
    }

}
