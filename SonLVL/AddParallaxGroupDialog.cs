using System;
using System.Windows.Forms;

namespace SonicRetro.SonLVL.GUI
{
	public partial class AddParallaxGroupDialog : Form
	{
		public AddParallaxGroupDialog()
		{
			InitializeComponent();
		}

		private void spacingNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			MainForm.Instance.PreviewLineSpacing = (int)spacingNumericUpDown.Value;
			MainForm.Instance.DrawLevel();
		}

		private void parallaxFactorNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			parallaxFactorLabel.Text = $" / 256 = {(parallaxFactorIncreaseValue.Value > 0 ? "+" : "")}{parallaxFactorIncreaseValue.Value / 256}";
		}

		private void scrollSpeedNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			scrollSpeedLabel.Text = $" / 64 = {(scrollSpeedIncreaseValue.Value > 0 ? "+" : "")}{scrollSpeedIncreaseValue.Value / 64}";
		}

		const decimal parallaxFactorMultiple = 1 / 256m;
		private void parallaxFactorStartingValue_ValueChanged(object sender, EventArgs e)
		{
			// Round the number as needed, limited precision and all
			parallaxFactorStartingValue.Value = Math.Round(parallaxFactorStartingValue.Value / parallaxFactorMultiple, MidpointRounding.AwayFromZero) * parallaxFactorMultiple;
		}

		const decimal speedFactorMultiple = 1 / 64m;
		private void scrollSpeedStartingValue_ValueChanged(object sender, EventArgs e)
		{
			// See above, same thing here
			scrollSpeedStartingValue.Value = Math.Round(scrollSpeedStartingValue.Value / speedFactorMultiple, MidpointRounding.AwayFromZero) * speedFactorMultiple;
		}
	}
}
