using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public partial class StatisticsDialog : Form
	{
		bool Hexadecimal { get; set; }

		public StatisticsDialog(bool useHexadecimal)
		{
			InitializeComponent();
			objectsListView.ListViewItemSorter = new ListViewColumnSorter();
			chunksListView.ListViewItemSorter = new ListViewColumnSorter();
			tilesListView.ListViewItemSorter = new ListViewColumnSorter();

			Hexadecimal = useHexadecimal;
		}

		private void StatisticsDialog_Load(object sender, EventArgs e)
		{
			Dictionary<int, int[]> counts = new Dictionary<int, int[]>();

			// First, let's do the objects tab
			// Index 0 in the array is the currently opened stage, while index 1 is the folder total
			for (int i = 0; i < LevelData.ObjTypes.Count; i++)
				counts.Add(i, new int[] { 0, 0 });

			// Go through all objects in the scene, add 'em to the first column
			foreach (ObjectEntry item in LevelData.Objects)
				if (counts.ContainsKey(item.Type))
					counts[item.Type][0]++;
				else
					counts.Add(item.Type, new int[] { 1, 0 });

			// Set the starting values for the folder total column
			foreach (KeyValuePair<int, int[]> item in counts)
				item.Value[1] = item.Value[0];
			
			// Now, go through all additional scenes and add their counts into the folder total column
			foreach (var scene in LevelData.AdditionalScenes)
				foreach (var entity in scene.Scene.entities)
					if (counts.ContainsKey(entity.type))
						counts[entity.type][1]++;
					else
						counts.Add(entity.type, new int[] { 0, 1 });

			objectsListView.BeginUpdate();

			foreach (KeyValuePair<int, int[]> item in counts)
			{
				ListViewItem lvi = new ListViewItem((item.Key == 0) ? "Blank Object" : LevelData.GetObjectDefinition((byte)item.Key).Name);
				lvi.SubItems[0].Tag = item.Key;
				lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, item.Value[0].ToString()) { Tag = item.Value[0] });
				lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, item.Value[1].ToString()) { Tag = item.Value[1] });
				objectsListView.Items.Add(lvi);
			}
			objectsListView.Sort();
			objectsListView.EndUpdate();
			counts.Clear();

			// Now, the Chunks tab
			// Index 0 in the array is Foreground, Index 1 is Background, and Index 2 is folder total
			for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
				counts.Add(i, new int[] { 0, 0, 0 });
			
			// Go through the Foreground..
			for (int y = 0; y < LevelData.FGHeight; y++)
				for (int x = 0; x < LevelData.FGWidth; x++)
					if (counts.ContainsKey(LevelData.Scene.layout[y][x]))
						counts[LevelData.Scene.layout[y][x]][0]++;
					else
						counts.Add(LevelData.Scene.layout[y][x], new int[] { 1, 0, 0 });

			// And then the background..
			for (int layer = 0; layer < 8; layer++)
				for (int y = 0; y < LevelData.BGHeight[layer]; y++)
					for (int x = 0; x < LevelData.BGWidth[layer]; x++)
						if (counts.ContainsKey(LevelData.Background.layers[layer].layout[y][x]))
							counts[LevelData.Background.layers[layer].layout[y][x]][1]++;
						else
							counts.Add(LevelData.Background.layers[layer].layout[y][x], new int[] { 0, 1, 0 });

			// Add the two together to get the starting value for the folder total
			foreach (KeyValuePair<int, int[]> item in counts)
				item.Value[2] = item.Value[0] + item.Value[1];

			// And now, let's go through the rest of the stage folder
			foreach (var scene in LevelData.AdditionalScenes)
				foreach (ushort[] row in scene.Scene.layout)
					foreach (ushort chunk in row)
						if (counts.ContainsKey(chunk))
							counts[chunk][2]++;
						else
							counts.Add(chunk, new int[] { 0, 0, 1 });

			chunksListView.BeginUpdate();

			foreach (KeyValuePair<int, int[]> item in counts)
			{
				ListViewItem lvi = new ListViewItem(item.Key.ToString(Hexadecimal ? "X3" : "D3"));
				lvi.SubItems[0].Tag = item.Key;
				lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, item.Value[0].ToString()) { Tag = item.Value[0] });
				lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, item.Value[1].ToString()) { Tag = item.Value[1] });
				lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, item.Value[2].ToString()) { Tag = item.Value[2] });
				chunksListView.Items.Add(lvi);
			}
			chunksListView.Sort();
			chunksListView.EndUpdate();
			counts.Clear();

			// Now, tiles!
			// We're simply tallying up how many times the tiles are used in chunks, not how often they're used in the stage
			for (int i = 0; i < LevelData.NewTiles.Length; i++)
				counts.Add(i, new int[] { 0 });

			for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
				for (int y = 0; y < 8; y++)
					for (int x = 0; x < 8; x++)
						if (counts.ContainsKey(LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex))
							counts[LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex][0]++;
						else
							counts.Add(LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex, new int[] { 1 });
			
			tilesListView.BeginUpdate();
			foreach (KeyValuePair<int, int[]> item in counts)
			{
				ListViewItem lvi = new ListViewItem(item.Key.ToString(Hexadecimal ? "X3" : "D3"));
				lvi.SubItems[0].Tag = item.Key;
				lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, item.Value[0].ToString()) { Tag = item.Value[0] });
				tilesListView.Items.Add(lvi);
			}
			tilesListView.Sort();
			tilesListView.EndUpdate();
		}

		private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			ListView list = (ListView)sender;
			ListViewColumnSorter sort = (ListViewColumnSorter)list.ListViewItemSorter;
			if (sort.Column == e.Column)
				sort.Order = sort.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
			else
			{
				sort.Column = e.Column;
				sort.Order = e.Column == 0 ? SortOrder.Ascending : SortOrder.Descending;
			}
			list.Sort();
		}

		private void objectsListView_KeyDown(object sender, KeyEventArgs e)
		{
			if (objectsListView.SelectedItems.Count == 0) return;
			if (e.KeyCode == Keys.C && e.Control)
			{
				// Let's copy the selected row(s) in csv format
				StringBuilder sb = new StringBuilder("Name, Count, Folder Total\n");
				foreach (ListViewItem item in objectsListView.SelectedItems)
					sb.AppendLine($"{item.SubItems[0].Text}, {item.SubItems[1].Text}, {item.SubItems[2].Text}");

				Clipboard.SetText(sb.ToString());
			}
		}

		private void chunksListView_KeyDown(object sender, KeyEventArgs e)
		{
			if (chunksListView.SelectedItems.Count == 0) return;
			if (e.KeyCode == Keys.C && e.Control)
			{
				// Let's copy the selected row(s) in csv format
				StringBuilder sb = new StringBuilder("Chunk ID, Foreground, Background, Folder Total\n");
				foreach (ListViewItem item in chunksListView.SelectedItems)
					sb.AppendLine($"{item.SubItems[0].Text}, {item.SubItems[1].Text}, {item.SubItems[2].Text}, {item.SubItems[3].Text}");

				Clipboard.SetText(sb.ToString());
			}
		}

		private void tilesListView_KeyDown(object sender, KeyEventArgs e)
		{
			if (tilesListView.SelectedItems.Count == 0) return;
			if (e.KeyCode == Keys.C && e.Control)
			{
				// Let's copy the selected row(s) in csv format
				StringBuilder sb = new StringBuilder("Tile ID, Count\n");
				foreach (ListViewItem item in tilesListView.SelectedItems)
					sb.AppendLine($"{item.SubItems[0].Text}, {item.SubItems[1].Text}");

				Clipboard.SetText(sb.ToString());
			}
		}
	}

	public class ListViewColumnSorter : System.Collections.IComparer
	{
		public int Column { get; set; }
		public SortOrder Order { get; set; }

		public ListViewColumnSorter()
		{
			Column = 1;
			Order = SortOrder.Descending;
		}

		public int Compare(object x, object y)
		{
			ListViewItem it1 = (ListViewItem)x;
			ListViewItem it2 = (ListViewItem)y;
			int result = ((int)it1.SubItems[Column].Tag).CompareTo(it2.SubItems[Column].Tag);
			return Order == SortOrder.Ascending ? result : -result;
		}
	}
}
