using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoShop_Marijiya.Logic.Arithmetic;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk mengaktifkan mode plus image
        private void plusImageToolStripButton_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silahkan tambahkan gambar dasar terlebih dahulu.");
                return;
            }

            // Logika Toggle Mode
            if (currentMode == EditMode.PlusImage)
            {
                currentMode = EditMode.None;
                pictureBox.Cursor = Cursors.Default;
                // plusImageToolStripButton.Checked = false; // Jika 'CheckOnClick' = true
            }
            else
            {
                currentMode = EditMode.PlusImage;
                pictureBox.Cursor = Cursors.Hand;
                // plusImageToolStripButton.Checked = true; // Jika 'CheckOnClick' = true
                MessageBox.Show("Mode Operasi Gambar Aktif. Klik kanan pada gambar untuk opsi.");
            }
        }
    }
}