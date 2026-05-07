using SonicRetro.SonLVL.API;
using SonicRetro.SonLVL.GUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public partial class PaletteCycleDialog : Form
	{
		private Color[,] frames;
		private int startIndex;

		private BitmapBits chunkBitmap;
		private Color[] chunkPalette;

		private List<int> unchangedCols;

		private int frameCount => frames.GetLength(0);
		private int colorCount => frames.GetLength(1);

		public bool Hexadecimal
		{
			set => chunkNumericUpDown.Hexadecimal = value;
		}

		public PaletteCycleDialog(Color[] basePalette, Color[,] frames, int startIndex)
		{
			InitializeComponent();
			this.frames = frames;
			this.startIndex = startIndex;
			frameNumericUpDown.Maximum = frameCount - 1;
			frameMaxLabel.Text = $"/ {frameCount - 1}";

			chunkPalette = (Color[])basePalette.Clone();
			for (int i = 0; i < colorCount; i++)
				chunkPalette[startIndex + i] = frames[0, i];

			SelectColor(0);
			DrawChunk();

			tileList.Images = LevelData.CompChunkBmps;
			chunkNumericUpDown.Maximum = LevelData.CompChunkBmps.Length - 1;
			tileList.SelectedIndex = (int)chunkNumericUpDown.Value;

			// Now, let's get a list of all indexes that stay constant throughout the entire animation
			// (Given that palette cycle files typically contain the entire palette row, instead of only what's different)
			bool[] used = new bool[colorCount];
			for (int frame = 1; frame < frameCount; frame++)
			{
				for (int index = 0; index < colorCount; index++)
				{
					if (used[index] || frames[frame, index] != frames[frame - 1, index])
						used[index] = true;
				}
			}

			unchangedCols = new List<int>();
			for (int i = 0; i < used.Length; i++)
				if (!used[i])
					unchangedCols.Add(i);
		}

		private void palettePanel_Paint(object sender, PaintEventArgs e)
		{
			int frame = (int)frameNumericUpDown.Value;

			for (int i = 0; i < colorCount; i++)
			{
				e.Graphics.FillRectangle(new SolidBrush(frames[frame, i]), (i & 0x0f) * 16, (i >> 4) * 16, 16, 16);

				if (i == selectedIndex)
					e.Graphics.DrawRectangle(new Pen(Color.Magenta, 2), (i & 0x0f) * 16, (i >> 4) * 16 + 1, 15, 15);
			}
		}

		private void previewChunkPanel_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SetOptions();
			e.Graphics.DrawImage(chunkBitmap.ToBitmap(chunkPalette), 0, 0, 256, 256);
		}

		private void frameNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int frame = (int)frameNumericUpDown.Value;
			for (int i = 0; i < colorCount; i++)
				chunkPalette[startIndex + i] = frames[frame, i];

			loaded = false;
			Color color = frames[frame, selectedIndex];
			colorRed.Value = color.R;
			colorGreen.Value = color.G;
			colorBlue.Value = color.B;
			colorHex.Value = color.ToArgb() & 0xFFFFFF;
			loaded = true;

			palettePanel.Invalidate();
			previewChunkPanel.Invalidate();
		}

		private void chunkNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			DrawChunk();

			previewChunkPanel.Invalidate();

			tileList.SelectedIndex = (int)chunkNumericUpDown.Value;
		}

		private void tileList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tileList.SelectedIndex != -1)
				chunkNumericUpDown.Value = tileList.SelectedIndex;
		}

		int selectedIndex = -1;
		private bool loaded = true;

		private bool SelectColor(int index)
		{
			if (index >= colorCount || index == selectedIndex)
				return false;

			selectedIndex = index;

			int frame = (int)frameNumericUpDown.Value;

			Color color = frames[frame, selectedIndex];

			loaded = false;
			colorRed.Value = color.R;
			colorGreen.Value = color.G;
			colorBlue.Value = color.B;
			colorHex.Value = color.ToArgb() & 0xFFFFFF;
			loaded = true;

			return true;
		}

		private void DrawChunk()
		{
			chunkBitmap = new BitmapBits(128, 128);
			chunkBitmap.DrawSprite(LevelData.ChunkSprites[(int)chunkNumericUpDown.Value]);
		}

		private void palettePanel_MouseDown(object sender, MouseEventArgs e)
		{
			int index = (e.Location.X >> 4) + (e.Location.Y & 0xf0);

			if (SelectColor(index))
				palettePanel.Invalidate();
		}

		private void color_ValueChanged(object sender, EventArgs e)
		{
			if (!loaded) return;

			int frame = (int)frameNumericUpDown.Value;
			frames[frame, selectedIndex] = Color.FromArgb((byte)colorRed.Value, (byte)colorGreen.Value, (byte)colorBlue.Value);
			chunkPalette[startIndex + selectedIndex] = frames[frame, selectedIndex];

			loaded = false;
			colorHex.Value = frames[frame, selectedIndex].ToArgb() & 0xFFFFFF;
			loaded = true;

			palettePanel.Invalidate();
			previewChunkPanel.Invalidate();
		}

		private void colorHex_ValueChanged(object sender, EventArgs e)
		{
			if (!loaded) return;

			int frame = (int)frameNumericUpDown.Value;
			frames[frame, selectedIndex] = Color.FromArgb((int)((int)colorHex.Value | 0xFF000000));
			chunkPalette[startIndex + selectedIndex] = frames[frame, selectedIndex];

			loaded = false;
			Color color = frames[frame, selectedIndex];
			colorRed.Value = color.R;
			colorGreen.Value = color.G;
			colorBlue.Value = color.B;
			loaded = true;

			palettePanel.Invalidate();
			previewChunkPanel.Invalidate();
		}

		private void exportButton_Click(object sender, EventArgs e)
		{
			int frame = (int)frameNumericUpDown.Value;

			using (SaveFileDialog a = new SaveFileDialog() { DefaultExt = "act", Filter = "Palette Files|*.act|PNG Files|*.png|Replace GIF Palette|*.gif|JASC-PAL Files|*.pal;*.PspPalette", RestoreDirectory = true })
				if (a.ShowDialog(this) == DialogResult.OK)
				{
					switch (Path.GetExtension(a.FileName).ToLower())
					{
						default:
						case ".png":
							BitmapBits bmp = new BitmapBits(Math.Min(16, colorCount) * 8, ((colorCount + 15) / 16) * 8);
							for (int i = 0; i < colorCount; i++)
								bmp.FillRectangle((byte)i, (i & 0x0f) * 8, (i >> 4) * 8, 8, 8);
							bmp.ToBitmap(chunkPalette).Save(a.FileName);
							break;

						case ".gif":
							if (File.Exists(a.FileName))
							{
								var gif = new RSDKv3_4.Gif(a.FileName);
								for (int i = 0; i < colorCount; i++)
									gif.palette[startIndex + i] = new RSDKv3_4.Palette.Color(frames[frame, i].R, frames[frame, i].G, frames[frame, i].B);
								gif.Write(a.FileName);
							}
							else
								MessageBox.Show(this, $"\"{a.FileName}\" not found, please select the existing GIF spritesheet you wish to save the palette to!", "SonLVL-RSDK", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							break;

						case ".act":
							using (FileStream str = File.Create(a.FileName))
							using (BinaryWriter bw = new BinaryWriter(str))
								for (int i = 0; i < colorCount; i++)
								{
									bw.Write(frames[frame, i].R);
									bw.Write(frames[frame, i].G);
									bw.Write(frames[frame, i].B);
								}
							break;

						case ".pal":
						case ".psppalette":
							using (StreamWriter writer = File.CreateText(a.FileName))
							{
								writer.WriteLine("JASC-PAL");
								writer.WriteLine("0100");
								writer.WriteLine($"{colorCount}");
								for (int i = 0; i < colorCount; i++)
									writer.WriteLine($"{frames[frame, i].R}, {frames[frame, i].G}, {frames[frame, i].B}");
								writer.Close();
							}
							break;
					}
				}
		}

		private void importButton_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog a = new OpenFileDialog())
			{
				a.DefaultExt = "";
				a.Filter = "All|*.act;*.bmp;*.png;*.gif|Palette Files|*.act|Image Files|*.bmp;*.png;*.gif";
				a.RestoreDirectory = true;
				if (a.ShowDialog(this) == DialogResult.OK)
				{
					List<Color> colors = new List<Color>();
					switch (Path.GetExtension(a.FileName))
					{
						case ".act":
							byte[] palette = File.ReadAllBytes(a.FileName);
							for (int i = 0; i < Math.Min(palette.Length / 3, 256); i++)
								colors.Add(Color.FromArgb(palette[i * 3], palette[i * 3 + 1], palette[i * 3 + 2]));
							break;

						case ".bmp":
						case ".png":
						case ".gif":
							using (Bitmap bmp = new Bitmap(a.FileName))
							{
								if ((bmp.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
									colors.AddRange(bmp.Palette.Entries);
								else
								{
									MessageBox.Show(this, "No palette found in image!", "SonLVL-RSDK", MessageBoxButtons.OK, MessageBoxIcon.Warning);
									return;
								}
							}
							break;

						default:
							return;
					}

					int frame = (int)frameNumericUpDown.Value;
					Color[] framePal = new Color[colorCount];
					for (int i = 0; i < colorCount; i++)
						framePal[i] = frames[frame, i];

					using (ImportPalette dialog = new ImportPalette(colors.ToArray(), framePal))
					{
						if (dialog.ShowDialog(this) == DialogResult.OK)
						{
							for (int i = 0; i < colorCount; i++)
								frames[frame, i] = dialog.destinationPalette[i];
						}
					}
				}
			}
		}

		private void copyStageColorsbutton_Click(object sender, EventArgs e)
		{
			// yeah
		}
	}
}
