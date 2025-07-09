namespace SonicRetro.SonLVL.GUI
{
	partial class ObjectList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectList));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.typeLabel = new System.Windows.Forms.Label();
            this.typeValue = new System.Windows.Forms.NumericUpDown();
            this.propertyValueLabel = new System.Windows.Forms.Label();
            this.propertyValue = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listView2 = new System.Windows.Forms.ListView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.typeValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyValue)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "UnknownImg.png");
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(6, 6);
            this.listView1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(472, 822);
            this.listView1.TabIndex = 0;
            this.listView1.TileSize = new System.Drawing.Size(180, 64);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            // 
            // typeLabel
            // 
            this.typeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(6, 17);
            this.typeLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(72, 25);
            this.typeLabel.TabIndex = 0;
            this.typeLabel.Text = "Type: ";
            // 
            // typeValue
            // 
            this.typeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.typeValue.Location = new System.Drawing.Point(84, 13);
            this.typeValue.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.typeValue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.typeValue.Name = "typeValue";
            this.typeValue.Size = new System.Drawing.Size(76, 31);
            this.typeValue.TabIndex = 2;
            // 
            // propertyValueLabel
            // 
            this.propertyValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.propertyValueLabel.AutoSize = true;
            this.propertyValueLabel.Location = new System.Drawing.Point(234, 17);
            this.propertyValueLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.propertyValueLabel.Name = "propertyValueLabel";
            this.propertyValueLabel.Size = new System.Drawing.Size(166, 25);
            this.propertyValueLabel.TabIndex = 3;
            this.propertyValueLabel.Text = "Property Value: ";
            // 
            // propertyValue
            // 
            this.propertyValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.propertyValue.Location = new System.Drawing.Point(404, 13);
            this.propertyValue.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.propertyValue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.propertyValue.Name = "propertyValue";
            this.propertyValue.Size = new System.Drawing.Size(76, 31);
            this.propertyValue.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(650, 12);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 44);
            this.button1.TabIndex = 4;
            this.button1.Text = "&OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(812, 12);
            this.button2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 44);
            this.button2.TabIndex = 5;
            this.button2.Text = "&Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.listView2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.listView1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(968, 834);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // listView2
            // 
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.HideSelection = false;
            this.listView2.LargeImageList = this.imageList2;
            this.listView2.Location = new System.Drawing.Point(490, 6);
            this.listView2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(472, 822);
            this.listView2.TabIndex = 1;
            this.listView2.TileSize = new System.Drawing.Size(180, 64);
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Tile;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "UnknownImg.png");
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.typeLabel);
            this.panel1.Controls.Add(this.typeValue);
            this.panel1.Controls.Add(this.propertyValueLabel);
            this.panel1.Controls.Add(this.propertyValue);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 834);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(968, 62);
            this.panel1.TabIndex = 7;
            // 
            // ObjectList
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(968, 896);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "ObjectList";
            this.ShowInTaskbar = false;
            this.Text = "Add Object...";
            ((System.ComponentModel.ISupportInitialize)(this.typeValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyValue)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		internal System.Windows.Forms.ListView listView1;
		internal System.Windows.Forms.Label typeLabel;
		internal System.Windows.Forms.NumericUpDown typeValue;
		internal System.Windows.Forms.Label propertyValueLabel;
		internal System.Windows.Forms.NumericUpDown propertyValue;
		internal System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		internal System.Windows.Forms.ListView listView2;
		internal System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.Panel panel1;
	}
}
