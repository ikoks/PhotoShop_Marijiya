using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoShop_Marijiya.Logic.Transform;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            // 1. Ambil gambar saat ini
            Bitmap current = new Bitmap(pictureBox.Image);

            // 2. Panggil ImageProcessor dengan faktor 0.8 (80%)
            pictureBox.Image = ScaleImage.ApplyScaleImage(current, 0.8);

            // 3. Update array pixelData
            UpdatePixelDataFromPictureBox();

            // 4. Refresh Histogram jika panelnya terbuka
            RefreshHistogram();
        }
    }
}