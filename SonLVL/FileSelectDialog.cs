using System.Collections.Generic;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public partial class FileSelectDialog : Form
	{
		private TreeNode selectedNode;

		public FileSelectDialog(string title, List<string> files)
		{
			InitializeComponent();

			Text = title;
			foreach (var item in files)
			{
				var parent = treeView1.Nodes;
				foreach (var it2 in item.Split('/'))
				{
					if (parent.ContainsKey(it2))
						parent = parent[it2].Nodes;
					else
						parent = parent.Add(it2, it2).Nodes;
				}
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			selectedNode = e.Node;
			button1.Enabled = selectedNode.Nodes.Count == 0;
		}

		private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (button1.Enabled)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		public string SelectedPath => selectedNode.FullPath;
	}
}
