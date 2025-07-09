using SonicRetro.SonLVL.API;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public partial class ChunkBlockEditor : UserControl
	{
		public ChunkBlockEditor()
		{
			InitializeComponent();
		}

		private void ChunkBlockEditor_Load(object sender, EventArgs e)
		{
			Enabled = false;
		}

		public bool Hexadecimal
		{
			get => block.Hexadecimal;
			set => block.Hexadecimal = value;
		}

		public event EventHandler PropertyValueChanged = delegate { };

		bool initializing;

		private RSDKv3_4.Tiles128x128.Block.Tile[] selectedObjects;
		[Browsable(false)]
		public RSDKv3_4.Tiles128x128.Block.Tile[] SelectedObjects
		{
			get { return selectedObjects; }
			set
			{
				initializing = true;
				if (Enabled = (selectedObjects = value) != null)
				{
					int cnt = value.Count(a => a.direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX));
					if (cnt == value.Length)
						xFlip.CheckState = CheckState.Checked;
					else if (cnt == 0)
						xFlip.CheckState = CheckState.Unchecked;
					else
						xFlip.CheckState = CheckState.Indeterminate;
					cnt = value.Count(a => a.direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY));
					if (cnt == value.Length)
						yFlip.CheckState = CheckState.Checked;
					else if (cnt == 0)
						yFlip.CheckState = CheckState.Unchecked;
					else
						yFlip.CheckState = CheckState.Indeterminate;
					var first = value[0];
					if (value.All(a => a.solidityA == first.solidityA))
						solidity1.SelectedIndex = (int)first.solidityA;
					else
						solidity1.SelectedIndex = -1;
					if (value.All(a => a.solidityB == first.solidityB))
						solidity2.SelectedIndex = (int)first.solidityB;
					else
						solidity2.SelectedIndex = -1;
					block.Maximum = LevelData.NewTiles.Length - 1;
					if (value.All(a => a.tileIndex == first.tileIndex))
					{
						block.Minimum = 0;
						block.Value = first.tileIndex;
					}
					else
					{
						block.Minimum = -1;
						block.Value = -1;
					}
					cnt = value.Count(a => a.visualPlane == RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High);
					if (cnt == value.Length)
						highPlane.CheckState = CheckState.Checked;
					else if (cnt == 0)
						highPlane.CheckState = CheckState.Unchecked;
					else
						highPlane.CheckState = CheckState.Indeterminate;
				}
				initializing = false;
			}
		}

		private void SetDirection()
		{
			RSDKv3_4.Tiles128x128.Block.Tile.Directions dir = RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone;
			if (xFlip.Checked) dir = RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX;
			if (yFlip.Checked) dir ^= RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY;
			foreach (RSDKv3_4.Tiles128x128.Block.Tile item in selectedObjects)
				item.direction = dir;
		}

		private void xFlip_CheckedChanged(object sender, EventArgs e)
		{
			if (!initializing && xFlip.CheckState != CheckState.Indeterminate)
			{
				SetDirection();
				PropertyValueChanged(xFlip, EventArgs.Empty);
			}
		}

		private void yFlip_CheckedChanged(object sender, EventArgs e)
		{
			if (!initializing && yFlip.CheckState != CheckState.Indeterminate)
			{
				SetDirection();
				PropertyValueChanged(yFlip, EventArgs.Empty);
			}
		}

		private void solidity1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!initializing && solidity1.SelectedIndex > -1)
			{
				foreach (RSDKv3_4.Tiles128x128.Block.Tile item in selectedObjects)
					item.solidityA = (RSDKv3_4.Tiles128x128.Block.Tile.Solidities)solidity1.SelectedIndex;
				PropertyValueChanged(solidity1, EventArgs.Empty);
			}
		}

		private void solidity2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!initializing && solidity2.SelectedIndex > -1)
			{
				foreach (RSDKv3_4.Tiles128x128.Block.Tile item in selectedObjects)
					item.solidityB = (RSDKv3_4.Tiles128x128.Block.Tile.Solidities)solidity2.SelectedIndex;
				PropertyValueChanged(solidity2, EventArgs.Empty);
			}
		}

		private void block_ValueChanged(object sender, EventArgs e)
		{
			if (!initializing && block.Value > -1)
			{
				block.Minimum = 0;
				foreach (RSDKv3_4.Tiles128x128.Block.Tile item in selectedObjects)
					item.tileIndex = (ushort)block.Value;
				PropertyValueChanged(block, EventArgs.Empty);
			}
		}

		private void highPlane_CheckedChanged(object sender, EventArgs e)
		{
			if (!initializing && highPlane.CheckState != CheckState.Indeterminate)
			{
				foreach (RSDKv3_4.Tiles128x128.Block.Tile item in selectedObjects)
					item.visualPlane = highPlane.Checked ? RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High : RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.Low;
				PropertyValueChanged(highPlane, EventArgs.Empty);
			}
		}
	}
}
