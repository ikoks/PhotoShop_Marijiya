using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoShop_Marijiya.Logic.Arithmetic;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        //method pembagian image vs image
        private void imageToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Anda memerlukan gambar dasar terlebih dahulu.", "Error");
                return;
            }

            byte[,,] pixelData1;
            byte[,,]? pixelData2 = null;
            Bitmap? tempBitmap2 = null;

            try
            {
                pixelData1 = this.ConvertBitmapToPixelData(this.originalImage);

                if (secondLayerImage != null)
                {
                    pixelData2 = ConvertBitmapToPixelData(secondLayerImage);
                    secondLayerImage.Dispose();
                    secondLayerImage = null;
                }
                else
                {
                    // --- UBAH PESAN ---
                    MessageBox.Show("Pilih gambar KEDUA (Gambar B) untuk pembagian.");
                    string imagePath2 = SelectFileImage();
                    if (imagePath2 == null) return;

                    tempBitmap2 = new Bitmap(imagePath2);
                    pixelData2 = ConvertBitmapToPixelData(tempBitmap2);
                }

                if (pixelData2 == null)
                {
                    throw new Exception("Gagal mengonversi gambar kedua.");
                }

                // --- UBAH FUNGSI YANG DIPANGGIL ---
                Bitmap bmp = Divide.ApplyDivideImage(pixelData1, pixelData2);

                pictureBox.Image = bmp;
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();

                // --- UBAH PESAN ---
                MessageBox.Show("Gambar berhasil dibagi!");
            }
            catch (Exception ex)
            {
                // --- UBAH PESAN ---
                MessageBox.Show("Error saat membagi gambar: " + ex.Message, "Error");
            }
            finally
            {
                if (tempBitmap2 != null)
                {
                    tempBitmap2.Dispose();
                }
            }
        }
    }
}