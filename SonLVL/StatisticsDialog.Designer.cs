namespace SonicRetro.SonLVL
{
	partial class StatisticsDialog
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
            this.objectsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chunksListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tilesListView = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // objectsListView
            // 
            this.objectsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5});
            this.objectsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectsListView.FullRowSelect = true;
            this.objectsListView.GridLines = true;
            this.objectsListView.HideSelection = false;
            this.objectsListView.LabelWrap = false;
            this.objectsListView.Location = new System.Drawing.Point(6, 6);
            this.objectsListView.Margin = new System.Windows.Forms.Padding(6);
            this.objectsListView.Name = "objectsListView";
            this.objectsListView.Size = new System.Drawing.Size(562, 444);
            this.objectsListView.TabIndex = 0;
            this.objectsListView.UseCompatibleStateImageBehavior = false;
            this.objectsListView.View = System.Windows.Forms.View.Details;
            this.objectsListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.objectsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.objectsListView_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Count";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Folder Total";
            this.columnHeader5.Width = 80;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(590, 503);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.objectsListView);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage1.Size = new System.Drawing.Size(574, 456);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Objects";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chunksListView);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage2.Size = new System.Drawing.Size(574, 456);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chunks";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chunksListView
            // 
            this.chunksListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader9});
            this.chunksListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chunksListView.FullRowSelect = true;
            this.chunksListView.GridLines = true;
            this.chunksListView.HideSelection = false;
            this.chunksListView.LabelWrap = false;
            this.chunksListView.Location = new System.Drawing.Point(6, 6);
            this.chunksListView.Margin = new System.Windows.Forms.Padding(6);
            this.chunksListView.Name = "chunksListView";
            this.chunksListView.Size = new System.Drawing.Size(562, 444);
            this.chunksListView.TabIndex = 1;
            this.chunksListView.UseCompatibleStateImageBehavior = false;
            this.chunksListView.View = System.Windows.Forms.View.Details;
            this.chunksListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.chunksListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chunksListView_KeyDown);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ID";
            this.columnHeader3.Width = 45;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Foreground";
            this.columnHeader4.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Background";
            this.columnHeader6.Width = 70;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Folder Total";
            this.columnHeader9.Width = 75;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tilesListView);
            this.tabPage4.Location = new System.Drawing.Point(8, 39);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage4.Size = new System.Drawing.Size(574, 456);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tiles";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tilesListView
            // 
            this.tilesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.tilesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesListView.FullRowSelect = true;
            this.tilesListView.GridLines = true;
            this.tilesListView.HideSelection = false;
            this.tilesListView.LabelWrap = false;
            this.tilesListView.Location = new System.Drawing.Point(6, 6);
            this.tilesListView.Margin = new System.Windows.Forms.Padding(6);
            this.tilesListView.Name = "tilesListView";
            this.tilesListView.Size = new System.Drawing.Size(562, 444);
            this.tilesListView.TabIndex = 1;
            this.tilesListView.UseCompatibleStateImageBehavior = false;
            this.tilesListView.View = System.Windows.Forms.View.Details;
            this.tilesListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.tilesListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tilesListView_KeyDown);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "ID";
            this.columnHeader7.Width = 45;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Count";
            // 
            // StatisticsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 503);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimizeBox = false;
            this.Name = "StatisticsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Usage Counts";
            this.Load += new System.EventHandler(this.StatisticsDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView objectsListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView chunksListView;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.ListView tilesListView;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader9;
	}
}