namespace SonicRetro.SonLVL
{
	partial class CopyCollisionDialog
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
            this.planeAOverBRadioButton = new System.Windows.Forms.RadioButton();
            this.planeBOverARadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tileCheckBox = new System.Windows.Forms.CheckBox();
            this.chunkCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // planeAOverBRadioButton
            // 
            this.planeAOverBRadioButton.AutoSize = true;
            this.planeAOverBRadioButton.Location = new System.Drawing.Point(17, 61);
            this.planeAOverBRadioButton.Name = "planeAOverBRadioButton";
            this.planeAOverBRadioButton.Size = new System.Drawing.Size(226, 29);
            this.planeAOverBRadioButton.TabIndex = 0;
            this.planeAOverBRadioButton.TabStop = true;
            this.planeAOverBRadioButton.Text = "Plane &A → Plane B";
            this.planeAOverBRadioButton.UseVisualStyleBackColor = true;
            // 
            // planeBOverARadioButton
            // 
            this.planeBOverARadioButton.AutoSize = true;
            this.planeBOverARadioButton.Location = new System.Drawing.Point(17, 107);
            this.planeBOverARadioButton.Name = "planeBOverARadioButton";
            this.planeBOverARadioButton.Size = new System.Drawing.Size(226, 29);
            this.planeBOverARadioButton.TabIndex = 1;
            this.planeBOverARadioButton.TabStop = true;
            this.planeBOverARadioButton.Text = "Plane &B → Plane A";
            this.planeBOverARadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Copy Collision From...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(345, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Copy...";
            // 
            // tileCheckBox
            // 
            this.tileCheckBox.AutoSize = true;
            this.tileCheckBox.Checked = true;
            this.tileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tileCheckBox.Location = new System.Drawing.Point(350, 61);
            this.tileCheckBox.Name = "tileCheckBox";
            this.tileCheckBox.Size = new System.Drawing.Size(167, 29);
            this.tileCheckBox.TabIndex = 4;
            this.tileCheckBox.Text = "&Tile Collision";
            this.tileCheckBox.UseVisualStyleBackColor = true;
            // 
            // chunkCheckBox
            // 
            this.chunkCheckBox.AutoSize = true;
            this.chunkCheckBox.Checked = true;
            this.chunkCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chunkCheckBox.Location = new System.Drawing.Point(350, 107);
            this.chunkCheckBox.Name = "chunkCheckBox";
            this.chunkCheckBox.Size = new System.Drawing.Size(182, 29);
            this.chunkCheckBox.TabIndex = 5;
            this.chunkCheckBox.Text = "&Chunk Solidity";
            this.chunkCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(91, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 40);
            this.button1.TabIndex = 6;
            this.button1.Text = "&OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(333, 170);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 40);
            this.button2.TabIndex = 7;
            this.button2.Text = "&Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // CopyCollisionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(570, 238);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chunkCheckBox);
            this.Controls.Add(this.tileCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.planeBOverARadioButton);
            this.Controls.Add(this.planeAOverBRadioButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CopyCollisionDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Copy Collision...";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		internal System.Windows.Forms.RadioButton planeAOverBRadioButton;
		internal System.Windows.Forms.RadioButton planeBOverARadioButton;
		internal System.Windows.Forms.CheckBox tileCheckBox;
		internal System.Windows.Forms.CheckBox chunkCheckBox;
	}
}