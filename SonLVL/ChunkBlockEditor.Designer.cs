namespace SonicRetro.SonLVL
{
	partial class ChunkBlockEditor
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label label2;
            this.solidity2 = new System.Windows.Forms.ComboBox();
            this.solidity1 = new System.Windows.Forms.ComboBox();
            this.xFlip = new System.Windows.Forms.CheckBox();
            this.yFlip = new System.Windows.Forms.CheckBox();
            this.block = new SonicRetro.SonLVL.NumericUpDownMulti();
            this.highPlane = new System.Windows.Forms.CheckBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.block)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(this.solidity2);
            groupBox1.Controls.Add(this.solidity1);
            groupBox1.Location = new System.Drawing.Point(4, 32);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            groupBox1.Size = new System.Drawing.Size(176, 100);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Solidity";
            // 
            // solidity2
            // 
            this.solidity2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidity2.FormattingEnabled = true;
            this.solidity2.Items.AddRange(new object[] {
            "All Solid",
            "Top Solid",
            "Left/Right/Bottom Solid",
            "Not Solid",
            "Top Solid (No Grip)"});
            this.solidity2.Location = new System.Drawing.Point(8, 57);
            this.solidity2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.solidity2.Name = "solidity2";
            this.solidity2.Size = new System.Drawing.Size(160, 24);
            this.solidity2.TabIndex = 1;
            this.solidity2.SelectedIndexChanged += new System.EventHandler(this.solidity2_SelectedIndexChanged);
            // 
            // solidity1
            // 
            this.solidity1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solidity1.FormattingEnabled = true;
            this.solidity1.Items.AddRange(new object[] {
            "All Solid",
            "Top Solid",
            "Left/Right/Bottom Solid",
            "Not Solid",
            "Top Solid (No Grip)"});
            this.solidity1.Location = new System.Drawing.Point(8, 23);
            this.solidity1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.solidity1.Name = "solidity1";
            this.solidity1.Size = new System.Drawing.Size(160, 24);
            this.solidity1.TabIndex = 0;
            this.solidity1.SelectedIndexChanged += new System.EventHandler(this.solidity1_SelectedIndexChanged);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(4, 144);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(44, 16);
            label2.TabIndex = 9;
            label2.Text = "Tile:";
            // 
            // xFlip
            // 
            this.xFlip.AutoSize = true;
            this.xFlip.Location = new System.Drawing.Point(4, 4);
            this.xFlip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xFlip.Name = "xFlip";
            this.xFlip.Size = new System.Drawing.Size(62, 20);
            this.xFlip.TabIndex = 0;
            this.xFlip.Text = "X Flip";
            this.xFlip.UseVisualStyleBackColor = true;
            this.xFlip.CheckedChanged += new System.EventHandler(this.xFlip_CheckedChanged);
            // 
            // yFlip
            // 
            this.yFlip.AutoSize = true;
            this.yFlip.Location = new System.Drawing.Point(81, 4);
            this.yFlip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.yFlip.Name = "yFlip";
            this.yFlip.Size = new System.Drawing.Size(63, 20);
            this.yFlip.TabIndex = 1;
            this.yFlip.Text = "Y Flip";
            this.yFlip.UseVisualStyleBackColor = true;
            this.yFlip.CheckedChanged += new System.EventHandler(this.yFlip_CheckedChanged);
            // 
            // block
            // 
            this.block.Hexadecimal = true;
            this.block.Location = new System.Drawing.Point(61, 142);
            this.block.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.block.Maximum = new decimal(new int[] {
            2047,
            0,
            0,
            0});
            this.block.Name = "block";
            this.block.Size = new System.Drawing.Size(71, 22);
            this.block.TabIndex = 10;
            this.block.ValueChanged += new System.EventHandler(this.block_ValueChanged);
            // 
            // highPlane
            // 
            this.highPlane.AutoSize = true;
            this.highPlane.Location = new System.Drawing.Point(4, 174);
            this.highPlane.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.highPlane.Name = "highPlane";
            this.highPlane.Size = new System.Drawing.Size(95, 20);
            this.highPlane.TabIndex = 11;
            this.highPlane.Text = "High Plane";
            this.highPlane.UseVisualStyleBackColor = true;
            this.highPlane.CheckedChanged += new System.EventHandler(this.highPlane_CheckedChanged);
            // 
            // ChunkBlockEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.highPlane);
            this.Controls.Add(this.block);
            this.Controls.Add(label2);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.yFlip);
            this.Controls.Add(this.xFlip);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ChunkBlockEditor";
            this.Size = new System.Drawing.Size(184, 198);
            this.Load += new System.EventHandler(this.ChunkBlockEditor_Load);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.block)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox xFlip;
		private System.Windows.Forms.CheckBox yFlip;
		private System.Windows.Forms.ComboBox solidity1;
		private System.Windows.Forms.ComboBox solidity2;
		private NumericUpDownMulti block;
		private System.Windows.Forms.CheckBox highPlane;
	}
}
