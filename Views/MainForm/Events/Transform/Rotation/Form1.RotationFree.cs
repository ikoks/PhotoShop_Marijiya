using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoShop_Marijiya.Logic.Transform;
using Microsoft.VisualBasic;

namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        //rotation free degree
        private void freeDegreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null) return;

            string input = Interaction.InputBox(
                "Masukkan sudut rotasi (derajat, misal: 15, -30):",
                "Free Rotation",
                "0"
            );

            if (string.IsNullOrEmpty(input)) return; // Batal

            float angle;
            if (float.TryParse(input, out angle))
            {
                Bitmap current = new Bitmap(pictureBox.Image);
                pictureBox.Image = Rotation.rotateFreeDegree(current, angle);

                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
            }
            else
            {
                MessageBox.Show("Masukkan angka yang valid.");
            }
        }
    }
}