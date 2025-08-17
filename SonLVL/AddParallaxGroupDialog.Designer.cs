namespace SonicRetro.SonLVL.GUI
{
	partial class AddParallaxGroupDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.parallaxFactorLabel = new System.Windows.Forms.Label();
            this.parallaxFactorIncreaseValue = new System.Windows.Forms.NumericUpDown();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.spacingNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.scrollSpeedIncreaseValue = new System.Windows.Forms.NumericUpDown();
            this.scrollSpeedLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.deformCheckBox = new System.Windows.Forms.CheckBox();
            this.parallaxFactorStartingValue = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.scrollSpeedStartingValue = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.parallaxFactorIncreaseValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spacingNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrollSpeedIncreaseValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parallaxFactorStartingValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrollSpeedStartingValue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parallax Factor:";
            // 
            // parallaxFactorLabel
            // 
            this.parallaxFactorLabel.AutoSize = true;
            this.parallaxFactorLabel.Location = new System.Drawing.Point(169, 186);
            this.parallaxFactorLabel.Name = "parallaxFactorLabel";
            this.parallaxFactorLabel.Size = new System.Drawing.Size(216, 25);
            this.parallaxFactorLabel.TabIndex = 1;
            this.parallaxFactorLabel.Text = " / 256 = +0.00390625";
            // 
            // parallaxFactorIncreaseValue
            // 
            this.parallaxFactorIncreaseValue.Location = new System.Drawing.Point(57, 184);
            this.parallaxFactorIncreaseValue.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.parallaxFactorIncreaseValue.Minimum = new decimal(new int[] {
            65535,
            0,
            0,
            -2147483648});
            this.parallaxFactorIncreaseValue.Name = "parallaxFactorIncreaseValue";
            this.parallaxFactorIncreaseValue.Size = new System.Drawing.Size(106, 31);
            this.parallaxFactorIncreaseValue.TabIndex = 3;
            this.parallaxFactorIncreaseValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.parallaxFactorIncreaseValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.parallaxFactorIncreaseValue.ValueChanged += new System.EventHandler(this.parallaxFactorNumericUpDown_ValueChanged);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(31, 462);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(132, 40);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(215, 462);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(132, 40);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Spacing:";
            // 
            // spacingNumericUpDown
            // 
            this.spacingNumericUpDown.Location = new System.Drawing.Point(153, 25);
            this.spacingNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spacingNumericUpDown.Name = "spacingNumericUpDown";
            this.spacingNumericUpDown.Size = new System.Drawing.Size(120, 31);
            this.spacingNumericUpDown.TabIndex = 1;
            this.spacingNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spacingNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spacingNumericUpDown.ValueChanged += new System.EventHandler(this.spacingNumericUpDown_ValueChanged);
            // 
            // scrollSpeedIncreaseValue
            // 
            this.scrollSpeedIncreaseValue.Location = new System.Drawing.Point(57, 352);
            this.scrollSpeedIncreaseValue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.scrollSpeedIncreaseValue.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.scrollSpeedIncreaseValue.Name = "scrollSpeedIncreaseValue";
            this.scrollSpeedIncreaseValue.Size = new System.Drawing.Size(120, 31);
            this.scrollSpeedIncreaseValue.TabIndex = 5;
            this.scrollSpeedIncreaseValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.scrollSpeedIncreaseValue.ValueChanged += new System.EventHandler(this.scrollSpeedNumericUpDown_ValueChanged);
            // 
            // scrollSpeedLabel
            // 
            this.scrollSpeedLabel.AutoSize = true;
            this.scrollSpeedLabel.Location = new System.Drawing.Point(183, 354);
            this.scrollSpeedLabel.Name = "scrollSpeedLabel";
            this.scrollSpeedLabel.Size = new System.Drawing.Size(90, 25);
            this.scrollSpeedLabel.TabIndex = 8;
            this.scrollSpeedLabel.Text = " / 64 = 0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(241, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Automatic Scroll Speed:";
            // 
            // deformCheckBox
            // 
            this.deformCheckBox.AutoSize = true;
            this.deformCheckBox.Location = new System.Drawing.Point(31, 411);
            this.deformCheckBox.Name = "deformCheckBox";
            this.deformCheckBox.Size = new System.Drawing.Size(113, 29);
            this.deformCheckBox.TabIndex = 6;
            this.deformCheckBox.Text = "Deform";
            this.deformCheckBox.UseVisualStyleBackColor = true;
            // 
            // parallaxFactorStartingValue
            // 
            this.parallaxFactorStartingValue.DecimalPlaces = 5;
            this.parallaxFactorStartingValue.Increment = new decimal(new int[] {
            78125,
            0,
            0,
            458752});
            this.parallaxFactorStartingValue.Location = new System.Drawing.Point(227, 115);
            this.parallaxFactorStartingValue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.parallaxFactorStartingValue.Name = "parallaxFactorStartingValue";
            this.parallaxFactorStartingValue.Size = new System.Drawing.Size(120, 31);
            this.parallaxFactorStartingValue.TabIndex = 2;
            this.parallaxFactorStartingValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.parallaxFactorStartingValue.ValueChanged += new System.EventHandler(this.parallaxFactorStartingValue_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Starting Value:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Increase Per Line:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 285);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "Starting Value:";
            // 
            // scrollSpeedStartingValue
            // 
            this.scrollSpeedStartingValue.DecimalPlaces = 5;
            this.scrollSpeedStartingValue.Increment = new decimal(new int[] {
            3125,
            0,
            0,
            327680});
            this.scrollSpeedStartingValue.Location = new System.Drawing.Point(227, 283);
            this.scrollSpeedStartingValue.Maximum = new decimal(new int[] {
            3984375,
            0,
            0,
            393216});
            this.scrollSpeedStartingValue.Name = "scrollSpeedStartingValue";
            this.scrollSpeedStartingValue.Size = new System.Drawing.Size(120, 31);
            this.scrollSpeedStartingValue.TabIndex = 4;
            this.scrollSpeedStartingValue.ValueChanged += new System.EventHandler(this.scrollSpeedStartingValue_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(52, 324);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 25);
            this.label7.TabIndex = 16;
            this.label7.Text = "Increase Per Line:";
            // 
            // AddParallaxGroupDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(402, 545);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.scrollSpeedStartingValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.parallaxFactorStartingValue);
            this.Controls.Add(this.deformCheckBox);
            this.Controls.Add(this.scrollSpeedIncreaseValue);
            this.Controls.Add(this.scrollSpeedLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.spacingNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.parallaxFactorIncreaseValue);
            this.Controls.Add(this.parallaxFactorLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddParallaxGroupDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Add Parallax Series...";
            ((System.ComponentModel.ISupportInitialize)(this.parallaxFactorIncreaseValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spacingNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrollSpeedIncreaseValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parallaxFactorStartingValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrollSpeedStartingValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label parallaxFactorLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label scrollSpeedLabel;
		private System.Windows.Forms.Label label5;
		internal System.Windows.Forms.NumericUpDown parallaxFactorIncreaseValue;
		internal System.Windows.Forms.NumericUpDown spacingNumericUpDown;
		internal System.Windows.Forms.NumericUpDown scrollSpeedIncreaseValue;
		internal System.Windows.Forms.CheckBox deformCheckBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		internal System.Windows.Forms.NumericUpDown parallaxFactorStartingValue;
		internal System.Windows.Forms.NumericUpDown scrollSpeedStartingValue;
	}
}