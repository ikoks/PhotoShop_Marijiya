using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoShop_Marijiya.Logic.Transform;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {

        //rotation 270 derajat
        private void derajatToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (pixelData == null) return;

            Bitmap currentImg = new Bitmap(pictureBox.Image);

            pictureBox.Image = Rotation.rotate270(currentImg);

            UpdatePixelDataFromPictureBox();
            RefreshHistogram();
        }
    }
}