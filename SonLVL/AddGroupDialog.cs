using System;
using System.Windows.Forms;

namespace SonicRetro.SonLVL.GUI
{
	public partial class AddGroupDialog : Form
	{
		public AddGroupDialog()
		{
			InitializeComponent();
			value_ValueChanged(this, EventArgs.Empty);
		}

		private void value_ValueChanged(object sender, EventArgs e)
		{
			MainForm.Instance.AddObjectsPreview = new System.Drawing.Rectangle(
				(int)XDist.Value, (int)YDist.Value, (int)Rows.Value, (int)Columns.Value);
			MainForm.Instance.DrawLevel();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
