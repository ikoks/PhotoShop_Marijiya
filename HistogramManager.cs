using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

namespace PhotoShop_Marijiya
{
    public static class HistogramManager
    {

        public static Panel CreateHistogramPanel(Bitmap bmp)
        {
            int height = bmp.Height;
            int width = bmp.Width;

            // --- 1. HITUNG DATA HISTOGRAM ---
            int[] histR = new int[256];
            int[] histG = new int[256];
            int[] histB = new int[256];

            // Looping gambar untuk menghitung frekuensi
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    histR[pixelColor.R]++;
                    histG[pixelColor.G]++;
                    histB[pixelColor.B]++;
                }
            }

            // --- 2. BUAT UI PANEL UTAMA ---
            Panel histogramPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 380,
                BackColor = Color.White,
                AutoScroll = true,
            };

            // --- 3. BUAT CONTAINER (FlowLayoutPanel) ---
            FlowLayoutPanel fl = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };

            // --- 4. BUAT 3 CHART-NYA ---
            // Kita panggil helper 'CreateHistogramChart' di bawah
            fl.Controls.Add(CreateHistogramChart("Histogram - Red", histR, Color.Red));
            fl.Controls.Add(CreateHistogramChart("Histogram - Green", histG, Color.Green));
            fl.Controls.Add(CreateHistogramChart("Histogram - Blue", histB, Color.Blue));

            // --- 5. BUAT LABEL JUDUL ---
            Label lbl = new Label
            {
                Text = $"Histogram (Ukuran: {width} x {height})",
                Height = 26,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Times New Roman", 9F, FontStyle.Regular)
            };

            // --- 6. SUSUN ELEMEN-ELEMEN UI ---
            histogramPanel.Controls.Add(lbl); // Label (Dock.Top) ditambahkan DULU
            histogramPanel.Controls.Add(fl);  // FlowPanel (Dock.Fill) mengisi sisanya

            // Kembalikan panel yang sudah jadi
            return histogramPanel;
        }


        private static Chart CreateHistogramChart(String title, int[] histogramData, Color color)
        {
            Chart chart = new Chart
            {
                Height = 180,
                Margin = new Padding(6)
            };

            // Area Chart
            ChartArea area = new ChartArea();
            area.AxisX.Title = "Intensity";
            area.AxisY.Title = "Count";
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = 255;
            area.AxisX.Interval = 32;
            area.AxisY.IsStartedFromZero = true;
            chart.ChartAreas.Add(area);

            // Data Series (Batang-batangnya)
            Series series = new Series
            {
                ChartType = SeriesChartType.Column,
                IsVisibleInLegend = false,
                ShadowOffset = 0
            };

            // Isi data ke series
            for (int i = 0; i < histogramData.Length; i++)
            {
                series.Points.Add(new DataPoint(i, histogramData[i]));
            }

            series.Color = color;
            chart.Series.Add(series);

            // Judul Chart
            chart.Titles.Add(new Title
            {
                Text = title,
                Docking = Docking.Top,
                Font = new Font("Times New Roman", 9F, FontStyle.Bold)
            });

            return chart;
        }

    }
}
