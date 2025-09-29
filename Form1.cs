namespace PhotoShop_Marijiya
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void addImageToolStripButton_Click(object sender, EventArgs e)
        {
            // Membuat sebuah instance dari OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Mengatur filter untuk tipe file gambar yang didukung
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff|All Files (*.*)|*.*";
            openFileDialog.Title = "Select an Image File";

            // Menampilkan dialog dan memeriksa apakah pengguna menekan OK
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Memuat gambar yang dipilih ke dalam PictureBox
                    // Menggunakan Image.FromFile untuk membuat objek Image dari file
                    pictureBox.Image = Image.FromFile(openFileDialog.FileName);

                    // Mengatur SizeMode untuk memastikan gambar sesuai dengan PictureBox
                    // (Anda sudah mengatur ini ke Zoom di designer code, tapi ini untuk kepastian)
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                    // Opsional: Perbarui Text form dengan nama file yang dimuat
                    this.Text = "PhotoShop Mariji - " + Path.GetFileName(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    // Menampilkan pesan error jika ada masalah saat memuat gambar
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
