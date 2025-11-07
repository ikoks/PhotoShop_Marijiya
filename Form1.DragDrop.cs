// Nama file: Form1.DragDrop.cs
namespace PhotoShop_Marijiya
{
    // Gunakan kata kunci 'partial'
    public partial class Form1
    {
        // List format file support
        private readonly List<string> supportedImageFormats = new()
        {
            ".bmp", ".jpg", ".jpeg", ".png", ".gif", ".tif", ".tiff"
        };

        // Method panel drag mouse enter
        private void panelPictureBox_DragEnter(object sender, DragEventArgs e)
        {
            // Cek apakah data yang di-drag adalah file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // Method mouse drop image
        private void panelPictureBox_DragDrop(object sender, DragEventArgs e)
        {
            // Ambil file yang di-drag
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                string filePath = files[0];
                string fileExtension = Path.GetExtension(filePath).ToLower();

                if (supportedImageFormats.Contains(fileExtension))
                {
                    if (pixelData == null)
                    {
                        // Skenario 1: Belum ada gambar. Load sebagai gambar dasar.
                        loadNewImage(filePath);
                    }
                    else
                    {
                        // Skenario 2: Sudah ada gambar dasar.
                        if (secondLayerImage != null)
                        {
                            secondLayerImage.Dispose();
                            secondLayerImage = null;
                        }

                        try
                        {
                            secondLayerImage = new Bitmap(filePath);
                            addImageLayer(filePath); // Tampilkan layer
                            MessageBox.Show("Gambar kedua ditambahkan sebagai layer." +
                                "\nSekarang masuk ke mode Plus (+) dan klik kanan 'Tambah' untuk menjumlahkan.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Gagal memuat atau menambahkan layer gambar: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Format file tidak didukung.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Method picturebox enter
        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            panelPictureBox_DragEnter(sender, e);
        }

        // Method picturebox Drop
        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            panelPictureBox_DragDrop(sender, e);
        }
    }
}