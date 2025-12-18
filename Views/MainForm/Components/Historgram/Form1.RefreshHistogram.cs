using System;
using System.Windows.Forms;
using System.Drawing;


namespace PhotoShop_Marijiya
{
    public partial class Form1
    {
        // Method refresh histogram
        private void RefreshHistogram()
        {
            if (histogramPanel != null && this.Controls.Contains(histogramPanel))
            {
                this.Controls.Remove(histogramPanel);
                histogramPanel.Dispose();
                histogramPanel = null;
                ShowHistogramPanel();
            }
        }
    }
}