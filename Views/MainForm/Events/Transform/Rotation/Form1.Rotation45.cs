using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoShop_Marijiya.Logic.Transform;
using Microsoft.VisualBasic;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        //rotation 45 derajat
        private void derajatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (pixelData == null) return;

            Bitmap currentImg = new Bitmap(pictureBox.Image);

            pictureBox.Image = Rotation.rotateFreeDegree(currentImg, 45);

            UpdatePixelDataFromPictureBox();
            RefreshHistogram();
        }
    }
}
