namespace SonicRetro.SonLVL
{
	partial class FindChunksDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tileList1 = new SonicRetro.SonLVL.API.TileList();
            this.chunkSelect = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.countButton = new System.Windows.Forms.Button();
            this.findButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chunkSelect)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tileList1);
            this.panel1.Controls.Add(this.chunkSelect);
            this.panel1.Location = new System.Drawing.Point(78, 23);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 562);
            this.panel1.TabIndex = 13;
            // 
            // tileList1
            // 
            this.tileList1.BackColor = System.Drawing.SystemColors.Window;
            this.tileList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileList1.Location = new System.Drawing.Point(0, 0);
            this.tileList1.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.tileList1.Name = "tileList1";
            this.tileList1.ScrollValue = 0;
            this.tileList1.SelectedIndex = -1;
            this.tileList1.Size = new System.Drawing.Size(586, 531);
            this.tileList1.TabIndex = 6;
            this.tileList1.SelectedIndexChanged += new System.EventHandler(this.tileList1_SelectedIndexChanged);
            // 
            // chunkSelect
            // 
            this.chunkSelect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chunkSelect.Hexadecimal = true;
            this.chunkSelect.Location = new System.Drawing.Point(0, 531);
            this.chunkSelect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chunkSelect.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.chunkSelect.Name = "chunkSelect";
            this.chunkSelect.Size = new System.Drawing.Size(586, 31);
            this.chunkSelect.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "ID:";
            // 
            // countButton
            // 
            this.countButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.countButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.countButton.Location = new System.Drawing.Point(518, 621);
            this.countButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.countButton.Name = "countButton";
            this.countButton.Size = new System.Drawing.Size(150, 44);
            this.countButton.TabIndex = 9;
            this.countButton.Text = "Cou&nt";
            this.countButton.UseVisualStyleBackColor = true;
            // 
            // findButton
            // 
            this.findButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.findButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.findButton.Location = new System.Drawing.Point(356, 621);
            this.findButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(150, 44);
            this.findButton.TabIndex = 8;
            this.findButton.Text = "&Find";
            this.findButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(78, 621);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(6);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(150, 44);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // FindChunksDialog
            // 
            this.AcceptButton = this.findButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(726, 685);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.countButton);
            this.Controls.Add(this.findButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindChunksDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Find Chunks";
            this.VisibleChanged += new System.EventHandler(this.FindChunksDialog_VisibleChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chunkSelect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		internal System.Windows.Forms.NumericUpDown chunkSelect;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button countButton;
		private System.Windows.Forms.Button findButton;
		private API.TileList tileList1;
		private System.Windows.Forms.Button cancelButton;
	}
}

