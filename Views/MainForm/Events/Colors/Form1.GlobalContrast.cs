using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoShop_Marijiya.Logic.Colors;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        private void contrastGlobalStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Panggil Logic Global
                Bitmap result = GlobalContrast.ApplyGlobalStretch(pixelData);

                // Update UI
                pictureBox.Image = result;
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();

                MessageBox.Show("Global Contrast Stretching berhasil diterapkan!");
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
