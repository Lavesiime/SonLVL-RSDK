namespace SonicRetro.SonLVL.GUI
{
	partial class NewModDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textModName = new System.Windows.Forms.TextBox();
            this.textModAuthor = new System.Windows.Forms.TextBox();
            this.checkOpenFolder = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textModDescription = new System.Windows.Forms.TextBox();
            this.textVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.loadTextScripts = new System.Windows.Forms.CheckBox();
            this.disablePauseFocus = new System.Windows.Forms.CheckBox();
            this.redirectSave = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.useGameXml = new System.Windows.Forms.CheckBox();
            this.forceSonic1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mod Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 117);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Author:";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOK.Location = new System.Drawing.Point(288, 267);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 28);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCancel.Location = new System.Drawing.Point(396, 267);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textModName
            // 
            this.textModName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textModName.Location = new System.Drawing.Point(108, 15);
            this.textModName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textModName.Name = "textModName";
            this.textModName.Size = new System.Drawing.Size(340, 22);
            this.textModName.TabIndex = 1;
            // 
            // textModAuthor
            // 
            this.textModAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textModAuthor.Location = new System.Drawing.Point(108, 49);
            this.textModAuthor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textModAuthor.Name = "textModAuthor";
            this.textModAuthor.Size = new System.Drawing.Size(340, 22);
            this.textModAuthor.TabIndex = 3;
            // 
            // checkOpenFolder
            // 
            this.checkOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkOpenFolder.AutoSize = true;
            this.checkOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkOpenFolder.Location = new System.Drawing.Point(16, 274);
            this.checkOpenFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkOpenFolder.Name = "checkOpenFolder";
            this.checkOpenFolder.Size = new System.Drawing.Size(108, 21);
            this.checkOpenFolder.TabIndex = 11;
            this.checkOpenFolder.Text = "Open folder";
            this.toolTip1.SetToolTip(this.checkOpenFolder, "Open the newly created mod\'s folder upon completion.");
            this.checkOpenFolder.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // textModDescription
            // 
            this.textModDescription.AcceptsReturn = true;
            this.textModDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textModDescription.Location = new System.Drawing.Point(108, 113);
            this.textModDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textModDescription.Multiline = true;
            this.textModDescription.Name = "textModDescription";
            this.textModDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textModDescription.Size = new System.Drawing.Size(340, 78);
            this.textModDescription.TabIndex = 7;
            // 
            // textVersion
            // 
            this.textVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textVersion.Location = new System.Drawing.Point(108, 81);
            this.textVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textVersion.Name = "textVersion";
            this.textVersion.Size = new System.Drawing.Size(340, 22);
            this.textVersion.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 85);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Version:";
            // 
            // loadTextScripts
            // 
            this.loadTextScripts.AutoSize = true;
            this.loadTextScripts.Location = new System.Drawing.Point(16, 199);
            this.loadTextScripts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loadTextScripts.Name = "loadTextScripts";
            this.loadTextScripts.Size = new System.Drawing.Size(133, 20);
            this.loadTextScripts.TabIndex = 14;
            this.loadTextScripts.Text = "Load Text Scripts";
            this.loadTextScripts.UseVisualStyleBackColor = true;
            // 
            // disablePauseFocus
            // 
            this.disablePauseFocus.AutoSize = true;
            this.disablePauseFocus.Location = new System.Drawing.Point(169, 199);
            this.disablePauseFocus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.disablePauseFocus.Name = "disablePauseFocus";
            this.disablePauseFocus.Size = new System.Drawing.Size(158, 20);
            this.disablePauseFocus.TabIndex = 15;
            this.disablePauseFocus.Text = "Disable Pause Focus";
            this.disablePauseFocus.UseVisualStyleBackColor = true;
            // 
            // redirectSave
            // 
            this.redirectSave.AutoSize = true;
            this.redirectSave.Location = new System.Drawing.Point(345, 199);
            this.redirectSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.redirectSave.Name = "redirectSave";
            this.redirectSave.Size = new System.Drawing.Size(115, 20);
            this.redirectSave.TabIndex = 16;
            this.redirectSave.Text = "Redirect Save";
            this.redirectSave.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 228);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(95, 20);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // useGameXml
            // 
            this.useGameXml.AutoSize = true;
            this.useGameXml.Location = new System.Drawing.Point(345, 228);
            this.useGameXml.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.useGameXml.Name = "useGameXml";
            this.useGameXml.Size = new System.Drawing.Size(115, 20);
            this.useGameXml.TabIndex = 18;
            this.useGameXml.Text = "Use game.xml";
            this.useGameXml.UseVisualStyleBackColor = true;
            // 
            // forceSonic1
            // 
            this.forceSonic1.AutoSize = true;
            this.forceSonic1.Location = new System.Drawing.Point(169, 228);
            this.forceSonic1.Margin = new System.Windows.Forms.Padding(4);
            this.forceSonic1.Name = "forceSonic1";
            this.forceSonic1.Size = new System.Drawing.Size(111, 20);
            this.forceSonic1.TabIndex = 19;
            this.forceSonic1.Text = "Force Sonic 1";
            this.forceSonic1.UseVisualStyleBackColor = true;
            // 
            // NewModDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(512, 310);
            this.Controls.Add(this.forceSonic1);
            this.Controls.Add(this.useGameXml);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.redirectSave);
            this.Controls.Add(this.disablePauseFocus);
            this.Controls.Add(this.loadTextScripts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkOpenFolder);
            this.Controls.Add(this.textModName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textModAuthor);
            this.Controls.Add(this.textModDescription);
            this.Controls.Add(this.textVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewModDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Mod";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textModName;
		private System.Windows.Forms.TextBox textModAuthor;
		private System.Windows.Forms.CheckBox checkOpenFolder;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TextBox textModDescription;
        private System.Windows.Forms.TextBox textVersion;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox loadTextScripts;
		private System.Windows.Forms.CheckBox disablePauseFocus;
		private System.Windows.Forms.CheckBox redirectSave;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox useGameXml;
		private System.Windows.Forms.CheckBox forceSonic1;
	}
}