using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoShop_Marijiya.Logic.Transform;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        //rotation 90 derajat
        private void derajatToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (pixelData == null) return;

            Bitmap currentImg = new Bitmap(pictureBox.Image);

            pictureBox.Image = Rotation.rotate90(currentImg);

            UpdatePixelDataFromPictureBox();
            RefreshHistogram();
        }
    }
}