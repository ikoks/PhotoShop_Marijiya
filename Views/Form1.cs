namespace PhotoShop_Marijiya
{
    using Microsoft.VisualBasic;
    using PhotoShop_Marijiya.Logic.Arithmetic;
    using PhotoShop_Marijiya.Logic.Colors;
    using PhotoShop_Marijiya.Logic.Distortion;
    using PhotoShop_Marijiya.Logic.Filter;
    using PhotoShop_Marijiya.Logic.Histogram;
    using PhotoShop_Marijiya.Logic.Transform;
    using System;
    using System.Diagnostics.Metrics;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    public partial class Form1 : Form
    {

        private byte[,,] pixelData;
        private Bitmap originalImage;
        private Bitmap? secondLayerImage = null;
        private Panel histogramPanel;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Enum untuk mode edit
        private enum EditMode
        {
            None,
            Brightness,
            BlackWhite,
            ColorDetection,
            PlusImage
        }

        // Variabel untuk menyimpan mode edit saat ini
        private EditMode currentMode = EditMode.None;

        private void histogramEqualizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pixelData == null)
            {
                MessageBox.Show("Masukkan gambar terlebih dahulu");
                
                return;
            }

            pictureBox.Image = HistogramEqualization.ApplyNormalization(pixelData);

        }
    }
}
