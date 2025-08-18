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
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.solidity2 = new System.Windows.Forms.ComboBox();
            this.solidity1 = new System.Windows.Forms.ComboBox();
            this.xFlip = new System.Windows.Forms.CheckBox();
            this.yFlip = new System.Windows.Forms.CheckBox();
            this.highPlane = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.block = new SonicRetro.SonLVL.NumericUpDownMulti();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.block)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            groupBox1.Location = new System.Drawing.Point(6, 6);
            groupBox1.Margin = new System.Windows.Forms.Padding(6);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 0);
            groupBox1.Size = new System.Drawing.Size(12, 30);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Solidity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Plane B Solidity:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Plane A Solidity:";
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
            this.solidity2.Location = new System.Drawing.Point(253, 176);
            this.solidity2.Margin = new System.Windows.Forms.Padding(6);
            this.solidity2.Name = "solidity2";
            this.solidity2.Size = new System.Drawing.Size(238, 33);
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
            this.solidity1.Location = new System.Drawing.Point(253, 123);
            this.solidity1.Margin = new System.Windows.Forms.Padding(6);
            this.solidity1.Name = "solidity1";
            this.solidity1.Size = new System.Drawing.Size(238, 33);
            this.solidity1.TabIndex = 0;
            this.solidity1.SelectedIndexChanged += new System.EventHandler(this.solidity1_SelectedIndexChanged);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(19, 42);
            label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 25);
            label2.TabIndex = 9;
            label2.Text = "Index:";
            // 
            // xFlip
            // 
            this.xFlip.AutoSize = true;
            this.xFlip.Location = new System.Drawing.Point(253, 42);
            this.xFlip.Margin = new System.Windows.Forms.Padding(6);
            this.xFlip.Name = "xFlip";
            this.xFlip.Size = new System.Drawing.Size(99, 29);
            this.xFlip.TabIndex = 0;
            this.xFlip.Text = "X Flip";
            this.xFlip.UseVisualStyleBackColor = true;
            this.xFlip.CheckedChanged += new System.EventHandler(this.xFlip_CheckedChanged);
            // 
            // yFlip
            // 
            this.yFlip.AutoSize = true;
            this.yFlip.Location = new System.Drawing.Point(364, 42);
            this.yFlip.Margin = new System.Windows.Forms.Padding(6);
            this.yFlip.Name = "yFlip";
            this.yFlip.Size = new System.Drawing.Size(100, 29);
            this.yFlip.TabIndex = 1;
            this.yFlip.Text = "Y Flip";
            this.yFlip.UseVisualStyleBackColor = true;
            this.yFlip.CheckedChanged += new System.EventHandler(this.yFlip_CheckedChanged);
            // 
            // highPlane
            // 
            this.highPlane.AutoSize = true;
            this.highPlane.Location = new System.Drawing.Point(253, 83);
            this.highPlane.Margin = new System.Windows.Forms.Padding(6);
            this.highPlane.Name = "highPlane";
            this.highPlane.Size = new System.Drawing.Size(149, 29);
            this.highPlane.TabIndex = 11;
            this.highPlane.Text = "High Plane";
            this.highPlane.UseVisualStyleBackColor = true;
            this.highPlane.CheckedChanged += new System.EventHandler(this.highPlane_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.highPlane);
            this.groupBox2.Controls.Add(this.solidity2);
            this.groupBox2.Controls.Add(this.solidity1);
            this.groupBox2.Controls.Add(this.yFlip);
            this.groupBox2.Controls.Add(this.block);
            this.groupBox2.Controls.Add(this.xFlip);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 242);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Chunk Tile";
            // 
            // block
            // 
            this.block.Hexadecimal = true;
            this.block.Location = new System.Drawing.Point(101, 40);
            this.block.Margin = new System.Windows.Forms.Padding(6);
            this.block.Maximum = new decimal(new int[] {
            2047,
            0,
            0,
            0});
            this.block.Name = "block";
            this.block.Size = new System.Drawing.Size(106, 31);
            this.block.TabIndex = 10;
            this.block.ValueChanged += new System.EventHandler(this.block_ValueChanged);
            // 
            // ChunkBlockEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(groupBox1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ChunkBlockEditor";
            this.Size = new System.Drawing.Size(506, 248);
            this.Load += new System.EventHandler(this.ChunkBlockEditor_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
	}
}
