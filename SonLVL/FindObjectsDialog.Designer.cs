namespace SonicRetro.SonLVL
{
	partial class FindObjectsDialog
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
            this.components = new System.ComponentModel.Container();
            this.findButton = new System.Windows.Forms.Button();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.findSubtype = new System.Windows.Forms.CheckBox();
            this.subtypeSelect = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.subtypeList = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.findID = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.idSelect = new SonicRetro.SonLVL.API.IDControl();
            ((System.ComponentModel.ISupportInitialize)(this.subtypeSelect)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // findButton
            // 
            this.findButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.findButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.findButton.Location = new System.Drawing.Point(231, 473);
            this.findButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(150, 44);
            this.findButton.TabIndex = 0;
            this.findButton.Text = "&Find";
            this.findButton.UseVisualStyleBackColor = true;
            // 
            // selectAllButton
            // 
            this.selectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectAllButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.selectAllButton.Location = new System.Drawing.Point(393, 473);
            this.selectAllButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(150, 44);
            this.selectAllButton.TabIndex = 1;
            this.selectAllButton.Text = "&Select All";
            this.selectAllButton.UseVisualStyleBackColor = true;
            // 
            // findSubtype
            // 
            this.findSubtype.AutoSize = true;
            this.findSubtype.Location = new System.Drawing.Point(24, 227);
            this.findSubtype.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.findSubtype.Name = "findSubtype";
            this.findSubtype.Size = new System.Drawing.Size(192, 29);
            this.findSubtype.TabIndex = 4;
            this.findSubtype.Text = "Property Value:";
            this.findSubtype.UseVisualStyleBackColor = true;
            this.findSubtype.CheckedChanged += new System.EventHandler(this.findSubtype_CheckedChanged);
            // 
            // subtypeSelect
            // 
            this.subtypeSelect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.subtypeSelect.Location = new System.Drawing.Point(0, 161);
            this.subtypeSelect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.subtypeSelect.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.subtypeSelect.Name = "subtypeSelect";
            this.subtypeSelect.Size = new System.Drawing.Size(300, 31);
            this.subtypeSelect.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.subtypeList);
            this.panel1.Controls.Add(this.subtypeSelect);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(239, 227);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 192);
            this.panel1.TabIndex = 5;
            // 
            // subtypeList
            // 
            this.subtypeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subtypeList.HideSelection = false;
            this.subtypeList.LargeImageList = this.imageList1;
            this.subtypeList.Location = new System.Drawing.Point(0, 0);
            this.subtypeList.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.subtypeList.MultiSelect = false;
            this.subtypeList.Name = "subtypeList";
            this.subtypeList.Size = new System.Drawing.Size(300, 161);
            this.subtypeList.TabIndex = 0;
            this.subtypeList.TileSize = new System.Drawing.Size(120, 48);
            this.subtypeList.UseCompatibleStateImageBehavior = false;
            this.subtypeList.View = System.Windows.Forms.View.Tile;
            this.subtypeList.SelectedIndexChanged += new System.EventHandler(this.subtypeList_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // findID
            // 
            this.findID.AutoSize = true;
            this.findID.Checked = true;
            this.findID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.findID.Location = new System.Drawing.Point(24, 23);
            this.findID.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.findID.Name = "findID";
            this.findID.Size = new System.Drawing.Size(166, 29);
            this.findID.TabIndex = 2;
            this.findID.Text = "Object Type:";
            this.findID.UseVisualStyleBackColor = true;
            this.findID.CheckedChanged += new System.EventHandler(this.findID_CheckedChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(24, 473);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(6);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(150, 44);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // idSelect
            // 
            this.idSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.idSelect.Location = new System.Drawing.Point(239, 23);
            this.idSelect.Margin = new System.Windows.Forms.Padding(6);
            this.idSelect.Name = "idSelect";
            this.idSelect.Size = new System.Drawing.Size(300, 192);
            this.idSelect.TabIndex = 3;
            this.idSelect.ValueChanged += new System.EventHandler(this.idSelect_ValueChanged);
            // 
            // FindObjectsDialog
            // 
            this.AcceptButton = this.findButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(567, 540);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.findID);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.findSubtype);
            this.Controls.Add(this.idSelect);
            this.Controls.Add(this.selectAllButton);
            this.Controls.Add(this.findButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindObjectsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Find Objects";
            ((System.ComponentModel.ISupportInitialize)(this.subtypeSelect)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button findButton;
		private System.Windows.Forms.Button selectAllButton;
		internal API.IDControl idSelect;
		internal System.Windows.Forms.CheckBox findSubtype;
		internal System.Windows.Forms.NumericUpDown subtypeSelect;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ListView subtypeList;
		internal System.Windows.Forms.CheckBox findID;
		private System.Windows.Forms.Button cancelButton;
	}
}

