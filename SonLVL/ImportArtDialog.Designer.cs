namespace SonicRetro.SonLVL
{
	partial class ImportArtDialog
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
            this.artFileLabel = new System.Windows.Forms.Label();
            this.colABrowseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.colBBaseLabel = new System.Windows.Forms.Label();
            this.colBBrowseButton = new System.Windows.Forms.Button();
            this.priBaseLabel = new System.Windows.Forms.Label();
            this.colALabel = new System.Windows.Forms.Label();
            this.colBLabel = new System.Windows.Forms.Label();
            this.priBrowseButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.infoButton = new System.Windows.Forms.Button();
            this.mergeDupesCheckBox = new System.Windows.Forms.CheckBox();
            this.concurrentTilesCheckBox = new System.Windows.Forms.CheckBox();
            this.priLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // artFileLabel
            // 
            this.artFileLabel.AutoSize = true;
            this.artFileLabel.Location = new System.Drawing.Point(21, 22);
            this.artFileLabel.Name = "artFileLabel";
            this.artFileLabel.Size = new System.Drawing.Size(167, 25);
            this.artFileLabel.TabIndex = 0;
            this.artFileLabel.Text = "Selected art file:";
            // 
            // colABrowseButton
            // 
            this.colABrowseButton.Location = new System.Drawing.Point(127, 98);
            this.colABrowseButton.Name = "colABrowseButton";
            this.colABrowseButton.Size = new System.Drawing.Size(122, 45);
            this.colABrowseButton.TabIndex = 4;
            this.colABrowseButton.Text = "Browse...";
            this.toolTip.SetToolTip(this.colABrowseButton, "Select the path to the collision image for Path 1..");
            this.colABrowseButton.UseVisualStyleBackColor = true;
            this.colABrowseButton.Click += new System.EventHandler(this.colABrowseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Collision: (Optional)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Path 1:";
            // 
            // colBBaseLabel
            // 
            this.colBBaseLabel.AutoSize = true;
            this.colBBaseLabel.Enabled = false;
            this.colBBaseLabel.Location = new System.Drawing.Point(41, 154);
            this.colBBaseLabel.Name = "colBBaseLabel";
            this.colBBaseLabel.Size = new System.Drawing.Size(80, 25);
            this.colBBaseLabel.TabIndex = 8;
            this.colBBaseLabel.Text = "Path 2:";
            // 
            // colBBrowseButton
            // 
            this.colBBrowseButton.Enabled = false;
            this.colBBrowseButton.Location = new System.Drawing.Point(127, 144);
            this.colBBrowseButton.Name = "colBBrowseButton";
            this.colBBrowseButton.Size = new System.Drawing.Size(122, 45);
            this.colBBrowseButton.TabIndex = 5;
            this.colBBrowseButton.Text = "Browse...";
            this.colBBrowseButton.UseVisualStyleBackColor = true;
            this.colBBrowseButton.Click += new System.EventHandler(this.colBBrowseButton_Click);
            // 
            // priBaseLabel
            // 
            this.priBaseLabel.AutoSize = true;
            this.priBaseLabel.Location = new System.Drawing.Point(21, 209);
            this.priBaseLabel.Name = "priBaseLabel";
            this.priBaseLabel.Size = new System.Drawing.Size(185, 25);
            this.priBaseLabel.TabIndex = 11;
            this.priBaseLabel.Text = "Priority: (Optional)";
            // 
            // colALabel
            // 
            this.colALabel.AutoSize = true;
            this.colALabel.Enabled = false;
            this.colALabel.Location = new System.Drawing.Point(254, 108);
            this.colALabel.Name = "colALabel";
            this.colALabel.Size = new System.Drawing.Size(137, 25);
            this.colALabel.TabIndex = 12;
            this.colALabel.Text = "(Not Loaded)";
            // 
            // colBLabel
            // 
            this.colBLabel.AutoSize = true;
            this.colBLabel.Enabled = false;
            this.colBLabel.Location = new System.Drawing.Point(254, 154);
            this.colBLabel.Name = "colBLabel";
            this.colBLabel.Size = new System.Drawing.Size(137, 25);
            this.colBLabel.TabIndex = 13;
            this.colBLabel.Text = "(Not Loaded)";
            this.toolTip.SetToolTip(this.colBLabel, "Select the path to the collision image for Path 2..");
            // 
            // priBrowseButton
            // 
            this.priBrowseButton.Location = new System.Drawing.Point(127, 241);
            this.priBrowseButton.Name = "priBrowseButton";
            this.priBrowseButton.Size = new System.Drawing.Size(122, 45);
            this.priBrowseButton.TabIndex = 6;
            this.priBrowseButton.Text = "Browse...";
            this.toolTip.SetToolTip(this.priBrowseButton, "Select the path to the priority image map..");
            this.priBrowseButton.UseVisualStyleBackColor = true;
            this.priBrowseButton.Click += new System.EventHandler(this.priBrowseButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.nextButton.Location = new System.Drawing.Point(406, 307);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(100, 45);
            this.nextButton.TabIndex = 21;
            this.nextButton.Text = "&Next";
            this.nextButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(295, 307);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 45);
            this.cancelButton.TabIndex = 20;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // infoButton
            // 
            this.infoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.infoButton.Location = new System.Drawing.Point(12, 307);
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size(80, 45);
            this.infoButton.TabIndex = 0;
            this.infoButton.Text = "&Help";
            this.infoButton.UseVisualStyleBackColor = true;
            this.infoButton.Click += new System.EventHandler(this.infoButton_Click);
            // 
            // mergeDupesCheckBox
            // 
            this.mergeDupesCheckBox.AutoSize = true;
            this.mergeDupesCheckBox.Checked = true;
            this.mergeDupesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mergeDupesCheckBox.Location = new System.Drawing.Point(26, 211);
            this.mergeDupesCheckBox.Name = "mergeDupesCheckBox";
            this.mergeDupesCheckBox.Size = new System.Drawing.Size(209, 29);
            this.mergeDupesCheckBox.TabIndex = 10;
            this.mergeDupesCheckBox.Text = "&Merge duplicates";
            this.toolTip.SetToolTip(this.mergeDupesCheckBox, "Merge all tiles with identical art together");
            this.mergeDupesCheckBox.UseVisualStyleBackColor = true;
            // 
            // concurrentTilesCheckBox
            // 
            this.concurrentTilesCheckBox.AutoSize = true;
            this.concurrentTilesCheckBox.Location = new System.Drawing.Point(26, 246);
            this.concurrentTilesCheckBox.Name = "concurrentTilesCheckBox";
            this.concurrentTilesCheckBox.Size = new System.Drawing.Size(322, 29);
            this.concurrentTilesCheckBox.TabIndex = 15;
            this.concurrentTilesCheckBox.Text = "&Group imported tiles together";
            this.toolTip.SetToolTip(this.concurrentTilesCheckBox, "If all imported tiles should be kept right next to each other on the tile list in" +
        "stead of being spread around.\r\nLimits the maximum number of tiles you can insert" +
        " at once.");
            this.concurrentTilesCheckBox.UseVisualStyleBackColor = true;
            // 
            // priLabel
            // 
            this.priLabel.AutoSize = true;
            this.priLabel.Enabled = false;
            this.priLabel.Location = new System.Drawing.Point(254, 251);
            this.priLabel.Name = "priLabel";
            this.priLabel.Size = new System.Drawing.Size(137, 25);
            this.priLabel.TabIndex = 22;
            this.priLabel.Text = "(Not Loaded)";
            // 
            // ImportArtDialog
            // 
            this.AcceptButton = this.nextButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(518, 364);
            this.Controls.Add(this.priLabel);
            this.Controls.Add(this.concurrentTilesCheckBox);
            this.Controls.Add(this.mergeDupesCheckBox);
            this.Controls.Add(this.infoButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.priBrowseButton);
            this.Controls.Add(this.colBLabel);
            this.Controls.Add(this.colALabel);
            this.Controls.Add(this.priBaseLabel);
            this.Controls.Add(this.colBBrowseButton);
            this.Controls.Add(this.colBBaseLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colABrowseButton);
            this.Controls.Add(this.artFileLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportArtDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Import...";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label artFileLabel;
		private System.Windows.Forms.Button colABrowseButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label colBBaseLabel;
		private System.Windows.Forms.Button colBBrowseButton;
		private System.Windows.Forms.Label priBaseLabel;
		private System.Windows.Forms.Label colALabel;
		private System.Windows.Forms.Label colBLabel;
		private System.Windows.Forms.Button priBrowseButton;
		private System.Windows.Forms.Button nextButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button infoButton;
		internal System.Windows.Forms.CheckBox mergeDupesCheckBox;
		internal System.Windows.Forms.CheckBox concurrentTilesCheckBox;
		private System.Windows.Forms.Label priLabel;
		private System.Windows.Forms.ToolTip toolTip;
	}
}