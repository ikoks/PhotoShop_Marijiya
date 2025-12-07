using System;
using System.Windows.Forms;
using System.Drawing;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        //method delete image
        private void deleteImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;

            if (originalImage != null)
            {
                originalImage.Dispose();
                originalImage = null;
            }

            // --- TAMBAHKAN INI ---
            if (secondLayerImage != null)
            {
                secondLayerImage.Dispose();
                secondLayerImage = null;
            }
            // --- SELESAI ---

            UpdatePixelDataFromPictureBox();


            MessageBox.Show("Kanvas telah dibersihkan.");
        }
    }
}