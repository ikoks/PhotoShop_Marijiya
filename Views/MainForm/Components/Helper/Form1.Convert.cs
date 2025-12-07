using System;
using System.Windows.Forms;
using System.Drawing;



namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method untuk mengonversi Bitmap ke array 3D pixelData
        private byte[,,] ConvertBitmapToPixelData(Bitmap bmp)
        {
            int height = bmp.Height; // Mendapatkan tinggi gambar
            int width = bmp.Width; // Mendapatkan lebar gambar

            // Inisialisasi array 3D untuk menyimpan data pixel
            byte[,,] data = new byte[height, width, 3];

            // Gunakan Bitmap sementara untuk membaca pixel
            using (Bitmap tempBmp = new Bitmap(bmp))
            {
                // Salin data pixel ke array
                for (int y = 0; y < height; y++)
                {
                    // Looping setiap kolom
                    for (int x = 0; x < width; x++)
                    {
                        Color pixelColor = tempBmp.GetPixel(x, y); // Mendapatkan warna pixel
                        data[y, x, 0] = pixelColor.R; // Simpan nilai Red
                        data[y, x, 1] = pixelColor.G; // Simpan nilai Green
                        data[y, x, 2] = pixelColor.B; // Simpan nilai Blue
                    }
                }
            }
            return data;
        }
    }
}