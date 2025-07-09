using System;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public class ToolStripCheckBoxButton : ToolStripControlHost
	{
		private CheckBox checkBox;
		
		public event EventHandler CheckedChanged;
		public override string Text => checkBox.Text;

		public bool Checked
		{
			get => checkBox.Checked;
			set => checkBox.Checked = value;
		}

		public ToolStripCheckBoxButton() : base(new CheckBox())
		{
			checkBox = (CheckBox)this.Control;
			checkBox.CheckedChanged += (sender, e) => CheckedChanged(sender, e);
			Margin = new Padding(5, 0, 0, 0);
		}
	}
}
