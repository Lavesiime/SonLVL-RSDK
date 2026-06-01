using System;
using System.Windows.Forms;

namespace SonicRetro.SonLVL.GUI
{
	public partial class ResizeLevelDialog : Form
	{
		public ResizeLevelDialog()
		{
			InitializeComponent();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void levelWidth_ValueChanged(object sender, EventArgs e)
		{
			widthLabel.Text = $"chunks ({levelWidth.Value * 128} pixels)";
		}

		private void levelHeight_ValueChanged(object sender, EventArgs e)
		{
			heightLabel.Text = $"chunks ({levelHeight.Value * 128} pixels)";
		}
	}
}
