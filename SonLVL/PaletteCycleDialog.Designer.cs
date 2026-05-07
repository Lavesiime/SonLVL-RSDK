namespace SonicRetro.SonLVL
{
	partial class PaletteCycleDialog
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
            this.palettePanel = new SonicRetro.SonLVL.DoubleBufferedPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.frameNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.colorRed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.colorGreen = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.colorBlue = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.importButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.frameMaxLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.previewChunkPanel = new SonicRetro.SonLVL.DoubleBufferedPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tileList = new SonicRetro.SonLVL.API.TileList();
            this.chunkNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colorHex = new SonicRetro.SonLVL.NumericUpDownPadded();
            this.copyStageColorsbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.frameNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBlue)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chunkNumericUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorHex)).BeginInit();
            this.SuspendLayout();
            // 
            // palettePanel
            // 
            this.palettePanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.palettePanel.Location = new System.Drawing.Point(35, 80);
            this.palettePanel.Name = "palettePanel";
            this.palettePanel.Size = new System.Drawing.Size(512, 200);
            this.palettePanel.TabIndex = 2;
            this.palettePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.palettePanel_Paint);
            this.palettePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.palettePanel_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Frame:";
            // 
            // frameNumericUpDown
            // 
            this.frameNumericUpDown.Location = new System.Drawing.Point(127, 28);
            this.frameNumericUpDown.Name = "frameNumericUpDown";
            this.frameNumericUpDown.Size = new System.Drawing.Size(120, 31);
            this.frameNumericUpDown.TabIndex = 0;
            this.frameNumericUpDown.ValueChanged += new System.EventHandler(this.frameNumericUpDown_ValueChanged);
            // 
            // colorRed
            // 
            this.colorRed.Location = new System.Drawing.Point(644, 86);
            this.colorRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorRed.Name = "colorRed";
            this.colorRed.Size = new System.Drawing.Size(98, 31);
            this.colorRed.TabIndex = 15;
            this.colorRed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colorRed.ValueChanged += new System.EventHandler(this.color_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(581, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Red:";
            // 
            // colorGreen
            // 
            this.colorGreen.Location = new System.Drawing.Point(844, 86);
            this.colorGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorGreen.Name = "colorGreen";
            this.colorGreen.Size = new System.Drawing.Size(98, 31);
            this.colorGreen.TabIndex = 20;
            this.colorGreen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colorGreen.ValueChanged += new System.EventHandler(this.color_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(761, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Green:";
            // 
            // colorBlue
            // 
            this.colorBlue.Location = new System.Drawing.Point(1031, 86);
            this.colorBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorBlue.Name = "colorBlue";
            this.colorBlue.Size = new System.Drawing.Size(98, 31);
            this.colorBlue.TabIndex = 25;
            this.colorBlue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colorBlue.ValueChanged += new System.EventHandler(this.color_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(964, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "Blue:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(582, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "Hex:";
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(879, 27);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(250, 42);
            this.importButton.TabIndex = 10;
            this.importButton.Text = "Import Palette Frame...";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(571, 28);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(250, 42);
            this.exportButton.TabIndex = 5;
            this.exportButton.Text = "Export Palette Frame...";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // frameMaxLabel
            // 
            this.frameMaxLabel.AutoSize = true;
            this.frameMaxLabel.Location = new System.Drawing.Point(253, 30);
            this.frameMaxLabel.Name = "frameMaxLabel";
            this.frameMaxLabel.Size = new System.Drawing.Size(36, 25);
            this.frameMaxLabel.TabIndex = 18;
            this.frameMaxLabel.Text = "/ 1";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(586, 242);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(131, 42);
            this.okButton.TabIndex = 35;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(998, 242);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(131, 42);
            this.cancelButton.TabIndex = 40;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // previewChunkPanel
            // 
            this.previewChunkPanel.Location = new System.Drawing.Point(23, 79);
            this.previewChunkPanel.Name = "previewChunkPanel";
            this.previewChunkPanel.Size = new System.Drawing.Size(512, 512);
            this.previewChunkPanel.TabIndex = 5;
            this.previewChunkPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.previewChunkPanel_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tileList);
            this.panel1.Controls.Add(this.chunkNumericUpDown);
            this.panel1.Location = new System.Drawing.Point(559, 79);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 516);
            this.panel1.TabIndex = 14;
            // 
            // tileList
            // 
            this.tileList.BackColor = System.Drawing.SystemColors.Window;
            this.tileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileList.ImageHeight = 128;
            this.tileList.ImageSize = 128;
            this.tileList.ImageWidth = 128;
            this.tileList.Location = new System.Drawing.Point(0, 0);
            this.tileList.Margin = new System.Windows.Forms.Padding(12);
            this.tileList.Name = "tileList";
            this.tileList.ScrollValue = 0;
            this.tileList.SelectedIndex = -1;
            this.tileList.Size = new System.Drawing.Size(586, 485);
            this.tileList.TabIndex = 45;
            this.tileList.SelectedIndexChanged += new System.EventHandler(this.tileList_SelectedIndexChanged);
            // 
            // chunkNumericUpDown
            // 
            this.chunkNumericUpDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chunkNumericUpDown.Hexadecimal = true;
            this.chunkNumericUpDown.Location = new System.Drawing.Point(0, 485);
            this.chunkNumericUpDown.Margin = new System.Windows.Forms.Padding(6);
            this.chunkNumericUpDown.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.chunkNumericUpDown.Name = "chunkNumericUpDown";
            this.chunkNumericUpDown.Size = new System.Drawing.Size(586, 31);
            this.chunkNumericUpDown.TabIndex = 46;
            this.chunkNumericUpDown.ValueChanged += new System.EventHandler(this.chunkNumericUpDown_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.previewChunkPanel);
            this.groupBox1.Location = new System.Drawing.Point(12, 324);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1162, 628);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 25);
            this.label2.TabIndex = 15;
            this.label2.Text = "Chunk:";
            // 
            // colorHex
            // 
            this.colorHex.Hexadecimal = true;
            this.colorHex.Location = new System.Drawing.Point(644, 170);
            this.colorHex.Maximum = new decimal(new int[] {
            16777215,
            0,
            0,
            0});
            this.colorHex.Name = "colorHex";
            this.colorHex.Size = new System.Drawing.Size(120, 31);
            this.colorHex.TabIndex = 30;
            this.colorHex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colorHex.ValueChanged += new System.EventHandler(this.colorHex_ValueChanged);
            // 
            // copyStageColorsbutton
            // 
            this.copyStageColorsbutton.Location = new System.Drawing.Point(787, 163);
            this.copyStageColorsbutton.Name = "copyStageColorsbutton";
            this.copyStageColorsbutton.Size = new System.Drawing.Size(342, 42);
            this.copyStageColorsbutton.TabIndex = 43;
            this.copyStageColorsbutton.Text = "Copy Unchanged Stage Colors...";
            this.copyStageColorsbutton.UseVisualStyleBackColor = true;
            this.copyStageColorsbutton.Click += new System.EventHandler(this.copyStageColorsbutton_Click);
            // 
            // PaletteCycleDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(1217, 976);
            this.Controls.Add(this.copyStageColorsbutton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.frameMaxLabel);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.colorHex);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.colorBlue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.colorGreen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.colorRed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.frameNumericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.palettePanel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteCycleDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Edit Palette Cycle...";
            ((System.ComponentModel.ISupportInitialize)(this.frameNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBlue)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chunkNumericUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorHex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private SonicRetro.SonLVL.DoubleBufferedPanel palettePanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown frameNumericUpDown;
		private System.Windows.Forms.NumericUpDown colorRed;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown colorGreen;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown colorBlue;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private NumericUpDownPadded colorHex;
		private System.Windows.Forms.Button importButton;
		private System.Windows.Forms.Button exportButton;
		private System.Windows.Forms.Label frameMaxLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private SonicRetro.SonLVL.DoubleBufferedPanel previewChunkPanel;
		private System.Windows.Forms.Panel panel1;
		private API.TileList tileList;
		internal System.Windows.Forms.NumericUpDown chunkNumericUpDown;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button copyStageColorsbutton;
	}
}