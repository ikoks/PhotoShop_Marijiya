using PhotoShop_Marijiya.Logic.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoShop_Marijiya
{
    public partial class ConvolutionForms : Form
    {
        // Properti untuk mengambil hasil input ke Form1
        public double[,] Kernel { get; private set; }
        public double Factor { get; private set; }
        public int Bias { get; private set; }

        public ConvolutionForms()
        {
            InitializeComponent();
        }

        private void ConvolutionForm_Load(object sender, EventArgs e)
        {
            // Generate default 3x3 saat load
            GenerateGrid(3);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int size = (int)numSize.Value;
            GenerateGrid(size);
        }

        private void GenerateGrid(int size)
        {
            panelGrid.Controls.Clear();

            // Atur ukuran textbox agar pas
            int boxSize = 40;
            int margin = 5;

            // Atur lebar panel flow agar kotak turun ke bawah dengan rapi
            panelGrid.Width = (boxSize + margin) * size + 30;

            for (int i = 0; i < size * size; i++)
            {
                TextBox tb = new TextBox();
                tb.Width = boxSize;
                tb.Text = "0"; // Default 0
                if (i == (size * size) / 2) tb.Text = "1"; // Titik tengah default 1

                panelGrid.Controls.Add(tb);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int size = (int)numSize.Value;
            Kernel = new double[size, size];

            try
            {
                // 1. Ambil data dari Grid TextBox
                int index = 0;
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        TextBox tb = (TextBox)panelGrid.Controls[index];
                        Kernel[y, x] = double.Parse(tb.Text);
                        index++;
                    }
                }

                // 2. Ambil Factor dan Bias
                Factor = double.Parse(txtFactor.Text);
                Bias = int.Parse(txtBias.Text);

                // Validasi Factor (tidak boleh 0)
                if (Factor == 0) Factor = 1;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Pastikan semua input adalah angka valid!");
                // Jangan tutup form agar user bisa perbaiki
                this.DialogResult = DialogResult.None;
            }
        }

        private void pilihKernelcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pilihan = pilihKernelcomboBox.SelectedItem.ToString();

            // 1. Set Factor & Bias default
            this.Factor = 1.0;
            this.Bias = 0;

            // 2. Isi Kernel langsung dari Logic (Bypassing Grid UI)
            switch (pilihan)
            {
                // --- SOBEL ---
                case "Sobel Horizontal":
                    this.Kernel = filterSobel.Gx;
                    break;

                case "Sobel Vertical":
                    this.Kernel = filterSobel.Gy;
                    break;

                // --- PREWITT ---
                case "Prewitt Horizontal":
                    this.Kernel = FilterPrewitt.Gx;
                    break;

                case "Prewitt Vertical":
                    this.Kernel = FilterPrewitt.Gy;
                    break;

                // --- ROBERT ---
                case "Robert Cross X":
                    this.Kernel = FilterRobert.Gx;
                    break;

                case "Robert Cross Y":
                    this.Kernel = FilterRobert.Gy;
                    break;

            }

            // 3. TUTUP FORM SECARA OTOMATIS (Auto-Submit)
            // Ini membuat Form1 mengira user baru saja menekan tombol OK
            this.DialogResult = DialogResult.OK;
            this.Close();


        }
    }
}
