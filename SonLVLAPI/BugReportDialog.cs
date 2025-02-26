using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace SonicRetro.SonLVL.API
{
	public partial class BugReportDialog : Form
	{
		private string programName, log;

		public BugReportDialog(string programName, string log)
		{
			InitializeComponent();
			this.programName = programName;
			this.log = log;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("https://github.com/Lavesiime/SonLVL-RSDK/issues");
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void copyButton_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(log);
		}

		private void ErrorReportDialog_Load(object sender, EventArgs e)
		{
			StringBuilder text = new StringBuilder();
			text.AppendLine($"Program: {programName}");
			text.AppendLine($"Build Date: {File.GetLastWriteTimeUtc(Application.ExecutablePath).ToString(CultureInfo.InvariantCulture)}");
			text.AppendLine($"OS Version: {Environment.OSVersion}");
			text.AppendLine("Log:");
			text.AppendLine(log);
			textBox1.Text = text.ToString();
		}
	}
}
