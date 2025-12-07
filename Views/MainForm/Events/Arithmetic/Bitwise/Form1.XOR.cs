using System;
using System.Windows.Forms;
using System.Drawing;
using PhotoShop_Marijiya.Logic.Arithmetic;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        //method bitwise XOR
        private void xORToolStripMenuItem_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Pilih gambar KEDUA (Gambar B) untuk operasi XOR.");
                    string imagePath2 = SelectFileImage();
                    if (imagePath2 == null) return;

                    tempBitmap2 = new Bitmap(imagePath2);
                    pixelData2 = ConvertBitmapToPixelData(tempBitmap2);
                }

                if (pixelData2 == null)
                {
                    throw new Exception("Gagal mengonversi gambar kedua.");
                }

                Bitmap bmp = Bitwise.ApplyBitwiseXOR(pixelData1, pixelData2);

                pictureBox.Image = bmp;
                UpdatePixelDataFromPictureBox();
                RefreshHistogram();
                MessageBox.Show("Operasi XOR berhasil!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat operasi AND: " + ex.Message, "Error");
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