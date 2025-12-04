namespace PhotoShop_Marijiya
{
    partial class ConvolutionForms
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvolutionForms));
            numSize = new NumericUpDown();
            btnGenerate = new Button();
            panelGrid = new FlowLayoutPanel();
            label1 = new Label();
            txtFactor = new TextBox();
            label2 = new Label();
            txtBias = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numSize).BeginInit();
            SuspendLayout();
            // 
            // numSize
            // 
            numSize.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            numSize.Location = new Point(3, 3);
            numSize.Maximum = new decimal(new int[] { 9, 0, 0, 0 });
            numSize.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            numSize.Name = "numSize";
            numSize.Size = new Size(114, 21);
            numSize.TabIndex = 0;
            numSize.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(15, 30);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(75, 23);
            btnGenerate.TabIndex = 1;
            btnGenerate.Text = "Buat Kernel";
            btnGenerate.UseVisualStyleBackColor = true;
            // 
            // panelGrid
            // 
            panelGrid.AutoScroll = true;
            panelGrid.Location = new Point(153, 90);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(433, 215);
            panelGrid.TabIndex = 2;
            panelGrid.Paint += this.flowLayoutPanel1_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(153, 311);
            label1.Name = "label1";
            label1.Size = new Size(92, 15);
            label1.TabIndex = 3;
            label1.Text = "Factor (Pembagi):";
            label1.Click += this.label1_Click;
            // 
            // txtFactor
            // 
            txtFactor.Location = new Point(243, 308);
            txtFactor.Name = "txtFactor";
            txtFactor.Size = new Size(100, 21);
            txtFactor.TabIndex = 4;
            txtFactor.Text = "1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(391, 311);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 5;
            label2.Text = "Bias (Offset):";
            // 
            // txtBias
            // 
            txtBias.Location = new Point(462, 309);
            txtBias.Name = "txtBias";
            txtBias.Size = new Size(100, 21);
            txtBias.TabIndex = 6;
            txtBias.Text = "0";
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(230, 364);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(113, 27);
            btnOK.TabIndex = 7;
            btnOK.Text = "Terapkan (Apply)";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += this.btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(391, 364);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 27);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Batal";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += this.btnCancel_Click;
            // 
            // ConvolutionForms
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(txtBias);
            Controls.Add(label2);
            Controls.Add(txtFactor);
            Controls.Add(label1);
            Controls.Add(btnGenerate);
            Controls.Add(numSize);
            Controls.Add(panelGrid);
            Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ConvolutionForms";
            Text = "Ukuran Matrix: ";
            ((System.ComponentModel.ISupportInitialize)numSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numSize;
        private Button btnGenerate;
        private FlowLayoutPanel panelGrid;
        private Label label1;
        private TextBox txtFactor;
        private Label label2;
        private TextBox txtBias;
        private Button btnOK;
        private Button btnCancel;
    }
}