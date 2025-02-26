namespace SonicRetro.SonLVL
{
	partial class InsertDeleteDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.shiftH = new System.Windows.Forms.RadioButton();
            this.shiftV = new System.Windows.Forms.RadioButton();
            this.entireRow = new System.Windows.Forms.RadioButton();
            this.entireColumn = new System.Windows.Forms.RadioButton();
            this.moveObjects = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(14, 312);
            this.okButton.Margin = new System.Windows.Forms.Padding(6);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(150, 44);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(176, 312);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(6);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(150, 44);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // shiftH
            // 
            this.shiftH.AutoSize = true;
            this.shiftH.Checked = true;
            this.shiftH.Location = new System.Drawing.Point(24, 23);
            this.shiftH.Margin = new System.Windows.Forms.Padding(6);
            this.shiftH.Name = "shiftH";
            this.shiftH.Size = new System.Drawing.Size(184, 29);
            this.shiftH.TabIndex = 2;
            this.shiftH.TabStop = true;
            this.shiftH.Text = "Shift chunks right";
            this.shiftH.UseVisualStyleBackColor = true;
            // 
            // shiftV
            // 
            this.shiftV.AutoSize = true;
            this.shiftV.Location = new System.Drawing.Point(24, 67);
            this.shiftV.Margin = new System.Windows.Forms.Padding(6);
            this.shiftV.Name = "shiftV";
            this.shiftV.Size = new System.Drawing.Size(193, 29);
            this.shiftV.TabIndex = 3;
            this.shiftV.Text = "Shift chunks down";
            this.shiftV.UseVisualStyleBackColor = true;
            // 
            // entireRow
            // 
            this.entireRow.AutoSize = true;
            this.entireRow.Location = new System.Drawing.Point(24, 140);
            this.entireRow.Margin = new System.Windows.Forms.Padding(6);
            this.entireRow.Name = "entireRow";
            this.entireRow.Size = new System.Drawing.Size(139, 29);
            this.entireRow.TabIndex = 4;
            this.entireRow.Text = "Entire row";
            this.entireRow.UseVisualStyleBackColor = true;
            this.entireRow.CheckedChanged += new System.EventHandler(this.entireRow_CheckedChanged);
            // 
            // entireColumn
            // 
            this.entireColumn.AutoSize = true;
            this.entireColumn.Location = new System.Drawing.Point(24, 185);
            this.entireColumn.Margin = new System.Windows.Forms.Padding(6);
            this.entireColumn.Name = "entireColumn";
            this.entireColumn.Size = new System.Drawing.Size(174, 29);
            this.entireColumn.TabIndex = 5;
            this.entireColumn.Text = "Entire column";
            this.entireColumn.UseVisualStyleBackColor = true;
            this.entireColumn.CheckedChanged += new System.EventHandler(this.entireColumn_CheckedChanged);
            // 
            // moveObjects
            // 
            this.moveObjects.AutoSize = true;
            this.moveObjects.Checked = true;
            this.moveObjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.moveObjects.Location = new System.Drawing.Point(24, 252);
            this.moveObjects.Margin = new System.Windows.Forms.Padding(6);
            this.moveObjects.Name = "moveObjects";
            this.moveObjects.Size = new System.Drawing.Size(176, 29);
            this.moveObjects.TabIndex = 6;
            this.moveObjects.Text = "Move Objects";
            this.moveObjects.UseVisualStyleBackColor = true;
            // 
            // InsertDeleteDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(350, 379);
            this.Controls.Add(this.moveObjects);
            this.Controls.Add(this.entireColumn);
            this.Controls.Add(this.entireRow);
            this.Controls.Add(this.shiftV);
            this.Controls.Add(this.shiftH);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertDeleteDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		internal System.Windows.Forms.RadioButton shiftH;
		internal System.Windows.Forms.RadioButton shiftV;
		internal System.Windows.Forms.RadioButton entireRow;
		internal System.Windows.Forms.RadioButton entireColumn;
		internal System.Windows.Forms.CheckBox moveObjects;
	}
}

