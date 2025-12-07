using Microsoft.VisualBasic;
using PhotoShop_Marijiya.Logic.Distortion;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        private void waveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // Minta input amplitudo
            string inputAmp = Interaction.InputBox("Masukkan Amplitudo (Tinggi Gelombang, misal: 20):", "Wave Effect", "20");
            if (string.IsNullOrEmpty(inputAmp)) return;

            // Minta input frekuensi
            string inputFreq = Interaction.InputBox("Masukkan Frekuensi (Kerapatan, misal: 0.05):", "Wave Effect", "0.05");
            if (string.IsNullOrEmpty(inputFreq)) return;

            if (double.TryParse(inputAmp, out double amp) && double.TryParse(inputFreq, out double freq))
            {
                Bitmap current = new Bitmap(pictureBox.Image);

                Cursor = Cursors.WaitCursor;
                pictureBox.Image = WaveDistortion.ApplyWave(current, amp, freq);
                Cursor = Cursors.Default;

                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
            }
            else
            {
                MessageBox.Show("Input tidak valid.");
            }
        }
    }
}