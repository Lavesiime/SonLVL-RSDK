using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public partial class ImportArtDialog : Form
	{
		private string artPath;
		private bool tilesMode;
		private Size artSize;

		public Bitmap colABitmap;
		public Bitmap colBBitmap;
		public Bitmap priBitmap;

		public ImportArtDialog(string art, Size size, bool tileMode)
		{
			InitializeComponent();
			artPath = art;
			artSize = size;
			tilesMode = tileMode;

			if (tilesMode)
			{
				// If we're importing tiles, then hide the priority fields
				priBaseLabel.Visible = priLabel.Visible = priBrowseButton.Visible = false;

				if (artSize.Width <= 16 && artSize.Height <= 16)
				{
					// If we're just a single tile, then disable the extra tile options
					mergeDupesCheckBox.Enabled = concurrentTilesCheckBox.Enabled = false;
					mergeDupesCheckBox.Checked = false;
				}
			}
			else
			{
				// Alternatively, if we're importing chunks/level layout, hide the tile-specific options
				mergeDupesCheckBox.Visible = concurrentTilesCheckBox.Visible = false;
			}

			artFileLabel.Text = "Selected art file: " + Path.GetFileName(art);

			string fmt = Path.Combine(Path.GetDirectoryName(art), Path.GetFileNameWithoutExtension(art) + "_{0}" + Path.GetExtension(art));

			if (File.Exists(string.Format(fmt, "col1")))
				loadFile(ref colALabel, ref colABitmap, string.Format(fmt, "col1"), false);
			else if (File.Exists(string.Format(fmt, "col")))
				loadFile(ref colALabel, ref colABitmap, string.Format(fmt, "col"), false);

			if (File.Exists(string.Format(fmt, "col2")))
				loadFile(ref colBLabel, ref colBBitmap, string.Format(fmt, "col2"), false);

			if (!tilesMode && File.Exists(string.Format(fmt, "pri")))
				loadFile(ref priLabel, ref priBitmap, string.Format(fmt, "pri"), false);
		}

		private void colABrowseButton_Click(object sender, EventArgs e)
		{
			openExtraFile(ref colALabel, ref colABitmap);
			if (colABitmap != null)
				colBBaseLabel.Enabled = colBBrowseButton.Enabled = true;
		}

		private void colBBrowseButton_Click(object sender, EventArgs e)
		{
			openExtraFile(ref colBLabel, ref colBBitmap);
		}

		private void priBrowseButton_Click(object sender, EventArgs e)
		{
			openExtraFile(ref priLabel, ref priBitmap);
		}

		// Used by all three file boxes to select their respective file
		private void openExtraFile(ref Label label, ref Bitmap bmp)
		{
			using (OpenFileDialog opendlg = new OpenFileDialog())
			{
				opendlg.DefaultExt = "png";
				opendlg.Filter = "Image Files|*.gif;*.png;*.bmp;*.jpg";
				opendlg.RestoreDirectory = true;
				if (opendlg.ShowDialog(this) == DialogResult.OK)
					loadFile(ref label, ref bmp, opendlg.FileName);
			}
		}

		// (the only reason it's separated is so that we can load files on init and load 'em from normal file dialogs without repating too much code, sorry--)
		private void loadFile(ref Label label, ref Bitmap bmp, string path, bool showError = true)
		{
			var b = new Bitmap(path);
			if (!b.Size.Equals(artSize))
			{
				if (showError)
					MessageBox.Show(this, $"The image must be the same size as the art file!\n(Selected file is {b.Width}x{b.Height}, while the art file is {artSize.Width}x{artSize.Height})", "SonLVL-RSDK Art Importer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				b.Dispose();
				return;
			}

			// If we already have something loaded, dispose of it before loading the new Bitmap
			if (bmp != null)
				b.Dispose();

			// Make our own copy, so that we can release the original file
			// (Whether the user needs/wants to edit it more, or if they're selecting the same file for several fields)
			bmp = new Bitmap(b);
			b.Dispose();
			label.Text = "Loaded!";
			label.Enabled = true;
		}

		private void infoButton_Click(object sender, EventArgs e)
		{
			if (tilesMode)
				MessageBox.Show("When importing tile art, you can import collision for the tiles as well. For an example of what the collision images should look like, export a tile whlie the `Export art+collision+priority` setting is enabled.", "SonLVL-RSDK Art Importer", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
				MessageBox.Show("When importing chunks, you can import collision as well as priority data. For an example of what the images should look like, export a chunk whlie the `Export art+collision+priority` setting is enabled.", "SonLVL-RSDK Art Importer", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
