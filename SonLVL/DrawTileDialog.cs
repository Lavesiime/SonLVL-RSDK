using System;
using System.Drawing;
using System.Windows.Forms;
using SonicRetro.SonLVL.API;

namespace SonicRetro.SonLVL.GUI
{
	public partial class DrawTileDialog : Form
	{
		public BitmapBits tile;

		private UndoSystem undoSystem;
		private Point selectedColor;
		private Bitmap palette;

		public DrawTileDialog()
		{
			InitializeComponent();

			BitmapBits bitmap = new BitmapBits(256, 256);
			for (int y = 0; y < 16; y++)
				for (int x = 0; x < 16; x++)
					bitmap.FillRectangle((byte)((y * 16) + x), x * 16, y * 16, 16, 16);
			palette = bitmap.ToBitmap(LevelData.NewPalette);
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void SaveState(string name)
		{
			// yeah.. we don't even use name here, but let's keep it anyways (since the undo system uses 'em)
			if (undoSystem.CreateState(name))
			{
				undoToolStripButton.Enabled = true;
				redoToolStripButton.Enabled = false;
			}
		}

		private void Undo()
		{
			if (!undoSystem.CanUndo) return;

			undoSystem.Undo();
			undoToolStripButton.Enabled = undoSystem.CanUndo;
			redoToolStripButton.Enabled = true;

			DrawTile();
		}

		private void Redo()
		{
			if (!undoSystem.CanRedo) return;

			undoSystem.Redo();
			undoToolStripButton.Enabled = true;
			redoToolStripButton.Enabled = undoSystem.CanRedo;

			DrawTile();
		}

		
		private void PalettePanel_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
			e.Graphics.DrawImage(palette, 0, 0, 256, 256);
			e.Graphics.DrawRectangle(new Pen(Color.Yellow, 2), selectedColor.X * 16, selectedColor.Y * 16, 16, 16);
		}

		private void PalettePanel_MouseDown(object sender, MouseEventArgs e)
		{
			selectedColor = new Point(e.X / 16, e.Y / 16);
			PalettePanel.Invalidate();
		}

		private void TilePicture_Paint(object sender, PaintEventArgs e)
		{
			DrawTile();
		}

		private Tool tool;
		private void TilePicture_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				switch (tool)
				{
					case Tool.Pencil:
						tile[e.X / (int)numericUpDown1.Value, e.Y / (int)numericUpDown1.Value] = (byte)((selectedColor.Y * 16) + selectedColor.X);
						lastpoint = new Point(e.X / (int)numericUpDown1.Value, e.Y / (int)numericUpDown1.Value);
						DrawTile();
						break;
					case Tool.Fill:
						tile.FloodFill((byte)((selectedColor.Y * 16) + selectedColor.X), e.X / (int)numericUpDown1.Value, e.Y / (int)numericUpDown1.Value);
						SaveState("Fill Tool");
						DrawTile();
						break;
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				selectedColor.X = tile[e.X / (int)numericUpDown1.Value, e.Y / (int)numericUpDown1.Value] & 15;
				selectedColor.Y = tile[e.X / (int)numericUpDown1.Value, e.Y / (int)numericUpDown1.Value] / 16;
				PalettePanel.Invalidate();
			}

			TilePicture.Focus();
		}

		Point lastpoint;
		private void TilePicture_MouseMove(object sender, MouseEventArgs e)
		{
			if (tool == Tool.Pencil && e.Button == MouseButtons.Left)
			{
				if (new Rectangle(Point.Empty, TilePicture.Size).Contains(e.Location))
				{
					tile.DrawLine((byte)((selectedColor.Y * 16) + selectedColor.X), lastpoint, new Point(e.X / (int)numericUpDown1.Value, e.Y / (int)numericUpDown1.Value));
					tile[e.X / (int)numericUpDown1.Value, e.Y / (int)numericUpDown1.Value] = (byte)((selectedColor.Y * 16) + selectedColor.X);
				}
				lastpoint = new Point(e.X / (int)numericUpDown1.Value, e.Y / (int)numericUpDown1.Value);
				DrawTile();
			}
		}

		private void TilePicture_MouseUp(object sender, MouseEventArgs e)
		{
			if (tool == Tool.Pencil && e.Button == MouseButtons.Left)
				SaveState("Pencil Tool");
		}

		private Graphics tileGfx;
		private BufferedGraphics tileGfxBuffer;
		private void DrawTile()
		{
			tileGfxBuffer.Graphics.Clear(LevelData.NewPalette[0]);
			tileGfxBuffer.Graphics.DrawImage(tile.Scale((int)numericUpDown1.Value).ToBitmap(LevelData.BmpPal), 0, 0, tile.Width * (int)numericUpDown1.Value, tile.Height * (int)numericUpDown1.Value);
			tileGfxBuffer.Render(tileGfx);
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			TilePicture.Size = new Size(tile.Width * (int)numericUpDown1.Value, tile.Height * (int)numericUpDown1.Value);
		}

		Cursor pencilcur, fillcur;
		private void DrawTileDialog_Shown(object sender, EventArgs e)
		{
			undoSystem = new DrawTileUndoSystem(tile);
			undoSystem.Init();

			tileGfx = TilePicture.CreateGraphics();
			tileGfx.SetOptions();

			tileGfxBuffer = BufferedGraphicsManager.Current.Allocate(tileGfx, new Rectangle(0, 0, TilePicture.Width, TilePicture.Height));
			tileGfxBuffer.Graphics.SetOptions();

			using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Properties.Resources.pencilcur))
				pencilcur = new Cursor(ms);
			using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Properties.Resources.fillcur))
				fillcur = new Cursor(ms);
			TilePicture.Cursor = pencilcur;
			TilePicture.Size = new Size(tile.Width * (int)numericUpDown1.Value, tile.Height * (int)numericUpDown1.Value);
		}

		private void TilePicture_Resize(object sender, EventArgs e)
		{
			tileGfx = TilePicture.CreateGraphics();
			tileGfx.SetOptions();

			tileGfxBuffer = BufferedGraphicsManager.Current.Allocate(tileGfx, new Rectangle(0, 0, TilePicture.Width, TilePicture.Height));
			tileGfxBuffer.Graphics.SetOptions();

			DrawTile();
		}

		private void pencilToolStripButton_Click(object sender, EventArgs e)
		{
			pencilToolStripButton.Checked = true;
			fillToolStripButton.Checked = false;
			tool = Tool.Pencil;
			TilePicture.Cursor = pencilcur;
		}

		private void fillToolStripButton_Click(object sender, EventArgs e)
		{
			pencilToolStripButton.Checked = false;
			fillToolStripButton.Checked = true;
			tool = Tool.Fill;
			TilePicture.Cursor = fillcur;
		}

		private void DrawTileDialog_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.P:
					pencilToolStripButton_Click(this, EventArgs.Empty);
					break;

				case Keys.F:
					fillToolStripButton_Click(this, EventArgs.Empty);
					break;

				case Keys.Z:
					if (e.Control)
						Undo();
					break;

				case Keys.Y:
					if (e.Control)
						Redo();
					break;
			}
		}

		private void undoToolStripButton_Click(object sender, EventArgs e)
		{
			Undo();
		}

		private void redoToolStripButton_Click(object sender, EventArgs e)
		{
			Redo();
		}

		enum Tool { Pencil, Fill }
	}
}
