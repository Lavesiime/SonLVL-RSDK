namespace SonicRetro.SonLVL
{
	partial class ImportPalette
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
            this.sourcePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.destinationPanel = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.copyAllButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sourcePanel
            // 
            this.sourcePanel.Location = new System.Drawing.Point(31, 61);
            this.sourcePanel.Name = "sourcePanel";
            this.sourcePanel.Size = new System.Drawing.Size(446, 429);
            this.sourcePanel.TabIndex = 0;
            this.sourcePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.sourcePanel_Paint);
            this.sourcePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sourcePanel_MouseDown);
            this.sourcePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sourcePanel_MouseMove);
            this.sourcePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sourcePanel_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Drag to Select..";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(483, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 46);
            this.label2.TabIndex = 2;
            this.label2.Text = "→";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(542, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Click to Place";
            // 
            // destinationPanel
            // 
            this.destinationPanel.Location = new System.Drawing.Point(547, 61);
            this.destinationPanel.Name = "destinationPanel";
            this.destinationPanel.Size = new System.Drawing.Size(446, 429);
            this.destinationPanel.TabIndex = 3;
            this.destinationPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.destinationPanel_Paint);
            this.destinationPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.destinationPanel_MouseClick);
            this.destinationPanel.MouseLeave += new System.EventHandler(this.destinationPanel_MouseLeave);
            this.destinationPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.destinationPanel_MouseMove);
            // 
            // okButton
            // 
            this.okButton.AutoSize = true;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(661, 535);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(163, 35);
            this.okButton.TabIndex = 21;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSize = true;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(830, 535);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(163, 35);
            this.cancelButton.TabIndex = 22;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(200, 530);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(163, 44);
            this.resetButton.TabIndex = 23;
            this.resetButton.Text = "&Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // copyAllButton
            // 
            this.copyAllButton.Location = new System.Drawing.Point(31, 530);
            this.copyAllButton.Name = "copyAllButton";
            this.copyAllButton.Size = new System.Drawing.Size(163, 44);
            this.copyAllButton.TabIndex = 24;
            this.copyAllButton.Text = "Copy &All";
            this.copyAllButton.UseVisualStyleBackColor = true;
            this.copyAllButton.Click += new System.EventHandler(this.copyAllButton_Click);
            // 
            // ImportPalette
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(1029, 633);
            this.ControlBox = false;
            this.Controls.Add(this.copyAllButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.destinationPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sourcePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ImportPalette";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Palette...";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel sourcePanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel destinationPanel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.Button copyAllButton;
	}
}