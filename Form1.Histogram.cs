// Nama file: Form1.Histogram.cs
namespace PhotoShop_Marijiya
{
    // Gunakan kata kunci 'partial'
    public partial class Form1
    {

        // Method menampilkan histogram
        private void ShowHistogramPanel()
        {
            if (pictureBox.Image == null) return;

            // Panggil kelas HistogramManager untuk membuat panel
            histogramPanel = HistogramManager.CreateHistogramPanel(new Bitmap(pictureBox.Image));

            // Form1 tinggal menambahkannya
            this.Controls.Add(histogramPanel);
            histogramPanel.BringToFront();
        }

        // Method refresh histogram
        private void RefreshHistogram()
        {
            if (histogramPanel != null && this.Controls.Contains(histogramPanel))
            {
                this.Controls.Remove(histogramPanel);
                histogramPanel.Dispose();
                histogramPanel = null;
                ShowHistogramPanel();
            }
        }

        // Method button histogram
        private void histogramImageToolStripButton_Click(object sender, EventArgs e)
        {
            // Cek apakah ada gambar
            if (pictureBox.Image == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // Logika toggle tidak berubah
            if (histogramPanel != null && this.Controls.Contains(histogramPanel))
            {
                this.Controls.Remove(histogramPanel);
                histogramPanel.Dispose();
                histogramPanel = null;
                return;
            }

            // Panggil fungsi pembuat yang baru
            ShowHistogramPanel();
        }
    }
}