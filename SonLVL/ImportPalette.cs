using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SonicRetro.SonLVL.API;

namespace SonicRetro.SonLVL
{
	public partial class ImportPalette : Form
	{
		public Color[] sourcePalette, destinationPalette, backupPalette;
		Point selection = new Point(-1, -1); // X: start index, Y: last index
		int mouseindex = -1;

		Graphics srcGfx, dstGfx;
		BufferedGraphics srcBuffer, dstBuffer;

		public ImportPalette(Color[] srcPal, Color[] dstPal)
		{
			InitializeComponent();

			srcGfx = sourcePanel.CreateGraphics();
			srcGfx.SetOptions();
			srcBuffer = BufferedGraphicsManager.Current.Allocate(srcGfx, new Rectangle(0, 0, sourcePanel.Width, sourcePanel.Height));
			srcBuffer.Graphics.SetOptions();

			dstGfx = destinationPanel.CreateGraphics();
			dstGfx.SetOptions();
			dstBuffer = BufferedGraphicsManager.Current.Allocate(dstGfx, new Rectangle(0, 0, destinationPanel.Width, destinationPanel.Height));
			dstBuffer.Graphics.SetOptions();

			sourcePalette = srcPal;
			destinationPalette = (Color[])dstPal.Clone();
			backupPalette = dstPal;
		}

		private void sourcePanel_Paint(object sender, PaintEventArgs e)
		{
			srcBuffer.Graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, sourcePanel.Width, sourcePanel.Height);

			for (int i = 0; i < sourcePalette.Length; i++)
				srcBuffer.Graphics.FillRectangle(new SolidBrush(sourcePalette[i]), (i & 15) * 14, (i / 16) * 14, 13, 13);

			if (selection.X > -1)
			{
				for (int i = Math.Min(selection.X, selection.Y); i <= Math.Max(selection.X, selection.Y); i++)
					srcBuffer.Graphics.DrawRectangle(new Pen(Color.White), (i & 15) * 14, (i / 16) * 14, 13, 13);
			}

			srcBuffer.Render(srcGfx);
		}

		private void sourcePanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (selection.X > -1 && selection.Y < selection.X)
				selection = new Point(selection.Y, selection.X);
		}

		private void sourcePanel_MouseDown(object sender, MouseEventArgs e)
		{
			selection.X = selection.Y = Math.Min((e.X / 14) + ((e.Y / 14) * 16), sourcePalette.Length - 1);
			sourcePanel.Invalidate();
		}

		private void sourcePanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			int prevSel = selection.Y;
			selection.Y = Math.Min((e.X / 14) + ((e.Y / 14) * 16), sourcePalette.Length - 1);
			if (prevSel != selection.Y)
				sourcePanel.Invalidate();
		}

		private void destinationPanel_MouseLeave(object sender, EventArgs e)
		{
			mouseindex = -1;
			destinationPanel.Invalidate();
		}

		private void destinationPanel_MouseMove(object sender, MouseEventArgs e)
		{
			int previndex = mouseindex;
			mouseindex = (e.X / 14) + ((e.Y / 14) * 16);
			if (selection.X > -1 && mouseindex != previndex)
				destinationPanel.Invalidate();
		}

		private void destinationPanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (selection.X > -1 && mouseindex > -1)
			{
				for (int i = 0; i <= selection.Y - selection.X; i++)
				{
					if (mouseindex + i >= destinationPalette.Length) break;
					destinationPalette[mouseindex + i] = sourcePalette[selection.X + i];
				}
				mouseindex = -1;
				selection = new Point(-1, -1);
				sourcePanel.Invalidate();
			}
		}

		private void destinationPanel_Paint(object sender, PaintEventArgs e)
		{
			dstBuffer.Graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, destinationPanel.Width, destinationPanel.Height);
			for (int i = 0; i < destinationPalette.Length; i++)
				dstBuffer.Graphics.FillRectangle(new SolidBrush(destinationPalette[i]), (i & 15) * 14, (i / 16) * 14, 13, 13);
			if (selection.X > -1 && mouseindex > -1)
			{
				for (int i = 0; i <= selection.Y - selection.X; i++)
				{
					if (mouseindex + i >= destinationPalette.Length) break;
					dstBuffer.Graphics.FillRectangle(new SolidBrush(sourcePalette[selection.X + i]), ((i + mouseindex) & 15) * 14, ((i + mouseindex) / 16) * 14, 13, 13);
				}
			}
			dstBuffer.Render(dstGfx);
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			destinationPalette = (Color[])backupPalette.Clone();
			selection = new Point(-1, -1);
			sourcePanel.Invalidate();
			destinationPanel.Invalidate();
		}

		private void copyAllButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < Math.Min(sourcePalette.Length, destinationPalette.Length); i++)
			{
				destinationPalette[i] = sourcePalette[i];
				selection = new Point(-1, -1);
				sourcePanel.Invalidate();
				destinationPanel.Invalidate();
			}
		}
	}
}
