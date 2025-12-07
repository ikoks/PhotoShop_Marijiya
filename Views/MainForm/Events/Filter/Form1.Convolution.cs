using PhotoShop_Marijiya.Logic.Filter;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk menangani klik pada tombol "Filtering" di ToolStrip
        private void filteringToolStripButton_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Silakan tambahkan gambar terlebih dahulu.");
                return;
            }

            // Buka Form Dialog Konvolusi
            using (ConvolutionForms form = new ConvolutionForms())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 3. JIKA USER KLIK "OK" -> AMBIL DATA DARI FORM
                    double[,] kernel = form.Kernel; // Ambil matriks angka
                    double factor = form.Factor;    // Ambil pembagi
                    int bias = form.Bias;           // Ambil bias

                    // Ubah kursor jadi loading (karena konvolusi itu berat)
                    this.Cursor = Cursors.WaitCursor;

                    try
                    {
                        // 4. KIRIM DATA KE IMAGEPROCESSOR
                        Bitmap result = Convolution.ApplyConvolution(pixelData, kernel, factor, bias);

                        // 5. TAMPILKAN HASILNYA
                        pictureBox.Image = result;

                        // 6. UPDATE DATA UTAMA (PENTING!)
                        UpdatePixelDataFromPictureBox();

                        // 7. Refresh Histogram
                        RefreshHistogram();

                        MessageBox.Show("Filter Konvolusi berhasil diterapkan!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                    }
                    finally
                    {
                        // Kembalikan kursor jadi normal
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }
    }
}