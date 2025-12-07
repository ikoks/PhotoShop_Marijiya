using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoShop_Marijiya.Logic.Histogram;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method menampilkan histogram
        private void ShowHistogramPanel()
        {
            if (pictureBox.Image == null) return;

            // Panggil kelas HistogramManager untuk membuat panel
            histogramPanel = Histogram.CreateHistogramPanel(new Bitmap(pictureBox.Image));

            // Form1 tinggal menambahkannya
            this.Controls.Add(histogramPanel);
            histogramPanel.BringToFront();
        }
    }
}