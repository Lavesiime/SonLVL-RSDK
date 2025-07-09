using System;
using System.Windows.Forms;

namespace SonicRetro.SonLVL.GUI
{
	public partial class LoadErrorDialog : Form
	{
		public LoadErrorDialog(bool level, string error)
		{
			InitializeComponent();
			if (!level)
				label1.Text = label1.Text.Replace("level", "game");
			label1.Text += error;
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void reportButton_Click(object sender, EventArgs e)
		{
			using (API.BugReportDialog dlg = new API.BugReportDialog("SonLVL-RSDK", string.Join(Environment.NewLine, MainForm.Instance.LogFile.ToArray())))
				dlg.ShowDialog(this);
			Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("https://lavesiime.github.io/guides/sonlvl/install/");
			Close();
		}
	}
}
