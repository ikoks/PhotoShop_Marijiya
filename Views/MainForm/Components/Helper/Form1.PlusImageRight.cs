using System;
using System.Windows.Forms;
using System.Drawing;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk menangani klik mouse pada PictureBox di mode PlusImage
        private void plusImagePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            // Hanya aktif jika mode PlusImage sedang berjalan
            if (currentMode == EditMode.PlusImage)
            {
                // Pastikan klik di dalam batas gambar
                if (e.Button == MouseButtons.Right)
                {
                    // Tampilkan context menu
                    plusImageContextMenuStrip.Show(panelPictureBox, e.Location);
                    plusImageContextMenuStrip.Show(pictureBox, e.Location);
                }
            }
        }
    }
}