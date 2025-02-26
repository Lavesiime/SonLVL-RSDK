using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public partial class InsertDeleteDialog : Form
	{
		int layerType;

		public InsertDeleteDialog(int t)
		{
			InitializeComponent();
			layerType = t;
			if (t != 0)
			{
				moveObjects.Text = "Move Parallax";
				moveObjects.Enabled = false;
			}
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void entireRow_CheckedChanged(object sender, EventArgs e)
		{
			if (layerType == 1) // Layer has horizontal parallax
				moveObjects.Enabled = entireRow.Checked;
		}

		private void entireColumn_CheckedChanged(object sender, EventArgs e)
		{
			if (layerType == 2) // Layer has vertical parallax
				moveObjects.Enabled = entireColumn.Checked;
		}
	}
}
