using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoShop_Marijiya.Logic.Arithmetic;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk menangani klik pada menu "Kurang" di context menu MinImage
        private void kurangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (originalImage == null) // Cek originalImage, bukan pixelData
            {
                MessageBox.Show("Anda memerlukan gambar dasar terlebih dahulu.", "Error");
                return;
            }

            byte[,,] pixelData1; // Akan diisi dengan originalImage
            byte[,,]? pixelData2 = null; // Akan diisi dengan gambar kedua
            Bitmap? tempBitmap2 = null; // Penampung jika memilih manual

            try
            {
                // 1. Ambil array piksel dari gambar pertama (Original)
                pixelData1 = this.ConvertBitmapToPixelData(this.originalImage);

                // 2. CEK APAKAH ADA GAMBAR DARI DRAG & DROP
                if (secondLayerImage != null)
                {
                    // Skenario A: YA. Gunakan gambar dari Drag & Drop
                    pixelData2 = ConvertBitmapToPixelData(secondLayerImage);

                    // Hapus gambar penampung setelah dipakai
                    secondLayerImage.Dispose();
                    secondLayerImage = null;
                }
                else
                {
                    // Skenario B: TIDAK ADA. Minta pengguna memilih file
                    MessageBox.Show("Pilih gambar KEDUA (Gambar B) untuk dikurangkan.");
                    string imagePath2 = SelectFileImage();
                    if (imagePath2 == null) return; // Pengguna membatalkan

                    tempBitmap2 = new Bitmap(imagePath2);
                    pixelData2 = ConvertBitmapToPixelData(tempBitmap2);
                }

                if (pixelData2 == null)
                {
                    throw new Exception("Gagal mengonversi gambar kedua.");
                }

                // 3. Panggil metode untuk mengurangkan (Original - Gambar 2)
                Bitmap bmp = MinImage.MinArraysImage(pixelData1, pixelData2);

                // 4. Tampilkan hasilnya
                pictureBox.Image = bmp;

                // 5. Perbarui pixelData utama kita dengan gambar hasil pengurangan
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
                MessageBox.Show("Gambar berhasil dikurangkan!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat mengurangkan gambar: " + ex.Message, "Error");
            }
            finally
            {
                // Bersihkan memori dari bitmap manual (jika ada)
                if (tempBitmap2 != null)
                {
                    tempBitmap2.Dispose();
                }
            }
        }
    }
}