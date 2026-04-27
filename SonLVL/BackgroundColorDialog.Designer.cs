namespace SonicRetro.SonLVL
{
	partial class BackgroundColorDialog
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
            this.useLevelColor = new System.Windows.Forms.RadioButton();
            this.useConstantColor = new System.Windows.Forms.RadioButton();
            this.palettePanel = new System.Windows.Forms.Panel();
            this.indexLabel = new System.Windows.Forms.Label();
            this.index = new System.Windows.Forms.NumericUpDown();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.constantColorBox = new System.Windows.Forms.PictureBox();
            this.colorChange = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.constantColorOverlay = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.index)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.constantColorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.constantColorOverlay)).BeginInit();
            this.SuspendLayout();
            // 
            // useLevelColor
            // 
            this.useLevelColor.AutoSize = true;
            this.useLevelColor.Location = new System.Drawing.Point(29, 39);
            this.useLevelColor.Name = "useLevelColor";
            this.useLevelColor.Size = new System.Drawing.Size(214, 29);
            this.useLevelColor.TabIndex = 3;
            this.useLevelColor.Text = "Use Level Color...";
            this.useLevelColor.UseVisualStyleBackColor = true;
            this.useLevelColor.CheckedChanged += new System.EventHandler(this.useLevelColor_CheckedChanged);
            // 
            // useConstantColor
            // 
            this.useConstantColor.AutoSize = true;
            this.useConstantColor.Location = new System.Drawing.Point(29, 620);
            this.useConstantColor.Name = "useConstantColor";
            this.useConstantColor.Size = new System.Drawing.Size(248, 29);
            this.useConstantColor.TabIndex = 4;
            this.useConstantColor.Text = "Use Constant Color...";
            this.useConstantColor.UseVisualStyleBackColor = true;
            this.useConstantColor.CheckedChanged += new System.EventHandler(this.useConstantColor_CheckedChanged);
            // 
            // palettePanel
            // 
            this.palettePanel.Location = new System.Drawing.Point(29, 95);
            this.palettePanel.Name = "palettePanel";
            this.palettePanel.Size = new System.Drawing.Size(512, 492);
            this.palettePanel.TabIndex = 5;
            this.palettePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.palettePanel_Paint);
            this.palettePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.palettePanel_MouseDown);
            // 
            // indexLabel
            // 
            this.indexLabel.AutoSize = true;
            this.indexLabel.Location = new System.Drawing.Point(323, 39);
            this.indexLabel.Name = "indexLabel";
            this.indexLabel.Size = new System.Drawing.Size(76, 25);
            this.indexLabel.TabIndex = 6;
            this.indexLabel.Text = "Index: ";
            // 
            // index
            // 
            this.index.Location = new System.Drawing.Point(396, 37);
            this.index.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.index.Name = "index";
            this.index.Size = new System.Drawing.Size(120, 31);
            this.index.TabIndex = 7;
            this.index.ValueChanged += new System.EventHandler(this.index_ValueChanged);
            // 
            // okButton
            // 
            this.okButton.AutoSize = true;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(203, 731);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(163, 35);
            this.okButton.TabIndex = 20;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSize = true;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(372, 731);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(163, 35);
            this.cancelButton.TabIndex = 21;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // constantColorBox
            // 
            this.constantColorBox.BackColor = System.Drawing.Color.MediumTurquoise;
            this.constantColorBox.Location = new System.Drawing.Point(29, 655);
            this.constantColorBox.Name = "constantColorBox";
            this.constantColorBox.Size = new System.Drawing.Size(340, 35);
            this.constantColorBox.TabIndex = 10;
            this.constantColorBox.TabStop = false;
            this.constantColorBox.Click += new System.EventHandler(this.constantColor_Click);
            // 
            // colorChange
            // 
            this.colorChange.AutoSize = true;
            this.colorChange.Location = new System.Drawing.Point(388, 651);
            this.colorChange.Name = "colorChange";
            this.colorChange.Size = new System.Drawing.Size(147, 35);
            this.colorChange.TabIndex = 11;
            this.colorChange.Text = "&Change...";
            this.colorChange.UseVisualStyleBackColor = true;
            this.colorChange.Click += new System.EventHandler(this.constantColor_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(33, 606);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(500, 2);
            this.label1.TabIndex = 12;
            // 
            // constantColorOverlay
            // 
            this.constantColorOverlay.BackColor = System.Drawing.Color.Gray;
            this.constantColorOverlay.Location = new System.Drawing.Point(29, 655);
            this.constantColorOverlay.Name = "constantColorOverlay";
            this.constantColorOverlay.Size = new System.Drawing.Size(340, 35);
            this.constantColorOverlay.TabIndex = 13;
            this.constantColorOverlay.TabStop = false;
            this.constantColorOverlay.Visible = false;
            // 
            // BackgroundColorDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(566, 792);
            this.Controls.Add(this.constantColorOverlay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorChange);
            this.Controls.Add(this.constantColorBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.index);
            this.Controls.Add(this.indexLabel);
            this.Controls.Add(this.palettePanel);
            this.Controls.Add(this.useConstantColor);
            this.Controls.Add(this.useLevelColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackgroundColorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Background Color...";
            ((System.ComponentModel.ISupportInitialize)(this.index)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.constantColorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.constantColorOverlay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		internal System.Windows.Forms.RadioButton useLevelColor;
		internal System.Windows.Forms.RadioButton useConstantColor;
		private System.Windows.Forms.Panel palettePanel;
		private System.Windows.Forms.Label indexLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		internal System.Windows.Forms.NumericUpDown index;
		internal System.Windows.Forms.PictureBox constantColorBox;
		private System.Windows.Forms.Button colorChange;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.PictureBox constantColorOverlay;
	}
}