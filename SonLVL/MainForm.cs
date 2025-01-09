using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace SonicRetro.SonLVL.GUI
{
	public partial class MainForm : Form
	{
		public static MainForm Instance { get; private set; }
		Settings Settings;
		readonly int pid;

		public MainForm()
		{
			Application.ThreadException += Application_ThreadException;
			Instance = this;
			pid = System.Diagnostics.Process.GetCurrentProcess().Id;
			if (Program.IsMonoRuntime)
				Log("Mono runtime detected.");
			Log("Operating system: " + Environment.OSVersion.ToString());
			LevelData.LogEvent += Log;
			LevelData.PaletteChangedEvent += LevelData_PaletteChangedEvent;
			InitializeComponent();
			if (Program.IsMonoRuntime)
				floorAngle.TextChanged += ColAngle_TextChanged;
		}

		const int ColorGrid = 255;

		void LevelData_PaletteChangedEvent()
		{
			LevelData.BmpPal.Entries.CopyTo(LevelImgPalette.Entries, 0);
			if (Settings.BackgroundColor.A < 255)
				LevelImgPalette.Entries[LevelData.ColorTransparent] = LevelData.NewPalette[Settings.BackgroundColor.A];
			else
				LevelImgPalette.Entries[LevelData.ColorTransparent] = Settings.BackgroundColor;
			if (invertColorsToolStripMenuItem.Checked)
				for (int i = 0; i < 256; i++)
					LevelImgPalette.Entries[i] = LevelImgPalette.Entries[i].Invert();
			ChunkSelector.Invalidate();
			DrawChunkPicture();
			chunkBlockEditor.Invalidate();
			DrawPalette();
			DrawTilePicture();
			TileSelector.Invalidate();
			DrawLevel();
		}

		void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			Log(e.Exception.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
			File.WriteAllLines("SonLVL-RSDK.log", LogFile.ToArray());
			using (ErrorDialog ed = new ErrorDialog("Unhandled Exception " + e.Exception.GetType().Name + "\nLog file has been saved.\n\nDo you want to try to continue running?", loaded))
			{
				if (ed.ShowDialog(this) == DialogResult.Cancel)
					Close();
			}
		}

		class LevelStuff
		{
			public string FullName;
			public ToolStripMenuItem MenuItem;
			public RSDKv3_4.GameConfig.StageList.StageInfo Stage;
		}

		ImageAttributes imageTransparency = new ImageAttributes();
		Bitmap LevelBmp;
		Graphics LevelGfx, PalettePanelGfx;
		bool loaded;
		bool saved;
		ushort SelectedChunk;
		List<Entry> SelectedItems;
		ObjectList ObjectSelect;
		Rectangle FGSelection, BGSelection;
		ColorPalette LevelImgPalette;
		int bglayer;
		double ZoomLevel = 1;
		byte ObjGrid = 0;
		bool objdrag = false;
		bool dragdrop = false;
		byte dragobj;
		Point dragpoint;
		bool selecting = false;
		Point selpoint;
		Point lastchunkpoint;
		Point lastmouse;
		internal LogWindow LogWindow;
		internal List<string> LogFile = new List<string>();
		List<LevelStuff> levelMenuItems;
		string levelname;
		List<string> scriptFiles;
		List<string> sfxFiles;
		Dictionary<char, HUDImage> HUDLetters, HUDNumbers;
		FindObjectsDialog findObjectsDialog;
		FindChunksDialog findFGChunksDialog;
		FindChunksDialog findBGChunksDialog;
		ReplaceChunksDialog replaceFGChunksDialog;
		ReplaceChunksDialog replaceBGChunksDialog;
		ReplaceChunkBlocksDialog replaceChunkBlocksDialog;
		List<LayoutSection> savedLayoutSections;
		List<Bitmap> savedLayoutSectionImages;
		MouseButtons chunkblockMouseDraw = MouseButtons.Left;
		MouseButtons chunkblockMouseSelect = MouseButtons.Right;
		Dictionary<int, int> objectTypeListMap = new Dictionary<int, int>();
		readonly UndoSystem undoSystem = new SonLVLUndoSystem();

		internal void Log(params string[] lines)
		{
			lock (LogFile)
			{
				LogFile.AddRange(lines);
				if (LogWindow != null)
					LogWindow.Invoke(new MethodInvoker(LogWindow.UpdateLines));
			}
		}

		Tab CurrentTab
		{
			get { return (Tab)tabControl1.SelectedIndex; }
			set { tabControl1.SelectedIndex = (int)value; }
		}

		ArtTab CurrentArtTab
		{
			get { return (ArtTab)tabControl4.SelectedIndex; }
			set { tabControl4.SelectedIndex = (int)value; }
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Settings = Settings.Load();
			imageTransparency.SetColorMatrix(new ColorMatrix() { Matrix33 = 0.75f }, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
			PalettePanelGfx = PalettePanel.CreateGraphics();
			string HUDpath = Path.Combine(Application.StartupPath, "HUD");
			HUDLetters = new Dictionary<char, HUDImage>();
			Dictionary<char, string> huditems = IniSerializer.Deserialize<Dictionary<char, string>>(Path.Combine(HUDpath, "HUD.ini"));
			foreach (KeyValuePair<char, string> item in huditems)
			{
				BitmapBits bmp = new BitmapBits(Path.Combine(HUDpath, item.Value + ".png"));
				HUDLetters.Add(item.Key, new HUDImage(bmp));
			}
			HUDNumbers = new Dictionary<char, HUDImage>();
			huditems = IniSerializer.Deserialize<Dictionary<char, string>>(Path.Combine(HUDpath, "HUDnum.ini"));
			foreach (KeyValuePair<char, string> item in huditems)
			{
				BitmapBits bmp = new BitmapBits(Path.Combine(HUDpath, item.Value + ".png"));
				HUDNumbers.Add(item.Key, new HUDImage(bmp));
			}
			objectsAboveHighPlaneToolStripMenuItem.Checked = Settings.ObjectsAboveHighPlane;
			hUDToolStripMenuItem.Checked = Settings.ShowHUD;
			//invertColorsToolStripMenuItem.Checked = Settings.InvertColors;
			lowToolStripMenuItem.Checked = Settings.ViewLowPlane;
			highToolStripMenuItem.Checked = Settings.ViewHighPlane;
			switch (Settings.ViewCollision)
			{
				case CollisionPath.Path1:
					noneToolStripMenuItem1.Checked = false;
					path1ToolStripMenuItem.Checked = true;
					break;
				case CollisionPath.Path2:
					noneToolStripMenuItem1.Checked = false;
					path2ToolStripMenuItem.Checked = true;
					break;
			}
			anglesToolStripMenuItem.Checked = Settings.ViewAngles;
			enableGridToolStripMenuItem.Checked = Settings.ShowGrid;
			foreach (ToolStripMenuItem item in zoomToolStripMenuItem.DropDownItems)
				if (item.Text == Settings.ZoomLevel)
				{
					zoomToolStripMenuItem_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(item));
					break;
				}
			objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[Settings.ObjectGridSize]));
			includeObjectsWithForegroundSelectionToolStripMenuItem.Checked = Settings.IncludeObjectsInForegroundSelection;
			transparentBackgroundToolStripMenuItem.Checked = Settings.TransparentBackgroundExport;
			hideDebugObjectsToolStripMenuItem.Checked = Settings.HideDebugObjectsExport;
			includeobjectsWithFGToolStripMenuItem.Checked = Settings.IncludeObjectsFG;
			exportArtcollisionpriorityToolStripMenuItem.Checked = Settings.ExportArtCollisionPriority;
			CurrentTab = Settings.CurrentTab;
			CurrentArtTab = Settings.CurrentArtTab;
			switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem.Checked = Settings.SwitchChunkBlockMouseButtons;
			switch (Settings.WindowMode)
			{
				case WindowMode.Maximized:
					WindowState = FormWindowState.Maximized;
					break;
				case WindowMode.Fullscreen:
					prevbnds = Bounds;
					prevstate = WindowState;
					TopMost = true;
					WindowState = FormWindowState.Normal;
					FormBorderStyle = FormBorderStyle.None;
					Bounds = Screen.FromControl(this).Bounds;
					break;
			}
			enableDraggingPaletteButton.Checked = Settings.EnableDraggingPalette;
			enableDraggingTilesButton.Checked = Settings.EnableDraggingTiles;
			enableDraggingChunksButton.Checked = Settings.EnableDraggingChunks;
			if (System.Diagnostics.Debugger.IsAttached)
				logToolStripMenuItem_Click(sender, e);
			if (Settings.MRUList == null)
				Settings.MRUList = new List<string>();
			else
			{
				List<string> mru = new List<string>();
				foreach (string item in Settings.MRUList)
				{
					if (File.Exists(item) && !item.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
					{
						mru.Add(item);
						recentProjectsToolStripMenuItem.DropDownItems.Add(item.Replace("&", "&&"));
					}
				}
				Settings.MRUList = mru;
				if (mru.Count > 0) recentProjectsToolStripMenuItem.DropDownItems.Remove(noneToolStripMenuItem2);
			}
			if (Settings.RecentMods == null)
				Settings.RecentMods = new List<MRUModItem>();
			recentModsToolStripMenuItem.Visible = false;
			selectModToolStripMenuItem.Visible = false;
			editGameConfigToolStripMenuItem.Visible = false;
			findObjectsDialog = new FindObjectsDialog();
			findFGChunksDialog = new FindChunksDialog();
			findBGChunksDialog = new FindChunksDialog();
			replaceFGChunksDialog = new ReplaceChunksDialog();
			replaceBGChunksDialog = new ReplaceChunksDialog();
			replaceChunkBlocksDialog = new ReplaceChunkBlocksDialog();
			collisionLayerSelector.SelectedIndex = 0;
			objectOrder.ListViewItemSorter = new ListViewIndexComparer();

			// Optional params: project file path, mod ini path, folder, act
			if (Program.Arguments.Length > 0 && File.Exists(Program.Arguments[0]))
			{
				LoadINI(Program.Arguments[0]);
				
				if (Program.Arguments.Length > 1 && File.Exists(Program.Arguments[1]))
				{
					LoadMod(Program.Arguments[1]);

					if (Program.Arguments.Length > 3)
					{
						LevelStuff ls = levelMenuItems.FirstOrDefault(a => a.Stage.folder.Equals(Program.Arguments[2], StringComparison.OrdinalIgnoreCase) && a.Stage.actID.Equals(Program.Arguments[3], StringComparison.OrdinalIgnoreCase));
						if (ls != null)
						{
							ls.MenuItem.Checked = true;
							Enabled = false;
							UseWaitCursor = true;
							levelname = ls.FullName;
							LoadLevel(ls);
						}
					}
				}
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			scrollPreviewButton.Checked = false;
			if (loaded && !saved)
			{
				switch (MessageBox.Show(this, "Do you want to save?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
				{
					case DialogResult.Yes:
						saveToolStripMenuItem_Click(this, EventArgs.Empty);
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
			if (Settings != null)
			{
				Settings.ShowHUD = hUDToolStripMenuItem.Checked;
				//Settings.InvertColors = invertColorsToolStripMenuItem.Checked;
				if (path1ToolStripMenuItem.Checked)
					Settings.ViewCollision = CollisionPath.Path1;
				else if (path2ToolStripMenuItem.Checked)
					Settings.ViewCollision = CollisionPath.Path2;
				else
					Settings.ViewCollision = CollisionPath.None;
				Settings.ViewAngles = anglesToolStripMenuItem.Checked;
				Settings.ShowGrid = enableGridToolStripMenuItem.Checked;
				Settings.ZoomLevel = zoomToolStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>().Single((a) => a.Checked).Text;
				Settings.ObjectGridSize = ObjGrid;
				Settings.IncludeObjectsInForegroundSelection = includeObjectsWithForegroundSelectionToolStripMenuItem.Checked;
				Settings.CurrentTab = CurrentTab;
				Settings.CurrentArtTab = CurrentArtTab;
				if (TopMost)
					Settings.WindowMode = WindowMode.Fullscreen;
				else if (WindowState == FormWindowState.Maximized)
					Settings.WindowMode = WindowMode.Maximized;
				else
					Settings.WindowMode = WindowMode.Normal;
				Settings.EnableDraggingPalette = enableDraggingPaletteButton.Checked;
				Settings.EnableDraggingTiles = enableDraggingTilesButton.Checked;
				Settings.EnableDraggingChunks = enableDraggingChunksButton.Checked;
				Settings.Save();
			}
		}

		private void LoadINI(string filename)
		{
#if !DEBUG
			try
#endif
			{
				LevelData.LoadGame(filename);
			}
#if !DEBUG
			catch (Exception ex)
			{
				fileToolStripMenuItem.HideDropDown();
				Log(ex.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
				using (LoadErrorDialog ed = new LoadErrorDialog(false, ex.GetType().Name + ": " + ex.Message))
					ed.ShowDialog(this);
				return;
			}
#endif
			if (Settings.MRUList.Count == 0)
				recentProjectsToolStripMenuItem.DropDownItems.Remove(noneToolStripMenuItem2);
			if (Settings.MRUList.Contains(filename))
			{
				recentProjectsToolStripMenuItem.DropDownItems.RemoveAt(Settings.MRUList.IndexOf(filename));
				Settings.MRUList.Remove(filename);
			}
			Settings.MRUList.Insert(0, filename);
			recentProjectsToolStripMenuItem.DropDownItems.Insert(0, new ToolStripMenuItem(filename));
			switch (LevelData.Game.RSDKVer)
			{
				case EngineVersion.V4:
					Icon = Properties.Resources.Tailsmon2;
					break;
				case EngineVersion.V3:
					Icon = Properties.Resources.clockmon;
					break;
				default:
					throw new NotImplementedException("Game type is not supported!");
			}
			buildAndRunToolStripMenuItem.Enabled = true;
			Text = "SonLVL-RSDK - " + LevelData.GameTitle;
			LoadMod(null);
		}

		private void LoadMod(string path)
		{
			if (path == null)
			{
				editGameConfigToolStripMenuItem.Enabled = false;
			}
			else
				editGameConfigToolStripMenuItem.Enabled = true;
			try
			{
				LevelData.LoadMod(Path.GetDirectoryName(path));
			}
			catch (Exception ex)
			{
				fileToolStripMenuItem.HideDropDown();
				Log(ex.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
				using (LoadErrorDialog ed = new LoadErrorDialog(false, ex.GetType().Name + ": " + ex.Message))
					ed.ShowDialog(this);
				return;
			}
			levelMenuItems = new List<LevelStuff>();
			var groups = new List<List<RSDKv3_4.GameConfig.StageList.StageInfo>>();
			List<RSDKv3_4.GameConfig.StageList.StageInfo> curgrp = null;
			foreach (var item in LevelData.StageLists)
				if (item.highlighted)
				{
					curgrp = new List<RSDKv3_4.GameConfig.StageList.StageInfo>() { item };
					groups.Add(curgrp);
				}
				else if (curgrp == null)
					groups.Add(new List<RSDKv3_4.GameConfig.StageList.StageInfo>() { item });
				else
					curgrp.Add(item);
			ToolStripMenuItem parent = changeLevelToolStripMenuItem;
			parent.DropDownItems.Clear();
			foreach (var grp in groups)
			{
				if (grp.Count > 1)
				{
					string basename = grp[0].name.Substring(0, Math.Max(0, grp[0].name.Length - grp.Skip(1).Max(a => a.name.Length)));
					var par2 = new ToolStripMenuItem(basename.Trim());
					parent.DropDownItems.Add(par2);
					foreach (var item in grp)
						if (item.name != null)
						{
							string name = item.name;
							string text = item.name;
							if (item.highlighted)
								text = text.Remove(0, basename.Length);
							else
								name = basename + name;
							LevelStuff ls = new LevelStuff() { FullName = name, Stage = item };
							ToolStripMenuItem ts = new ToolStripMenuItem(text, null, new EventHandler(LevelToolStripMenuItem_Clicked)) { Tag = ls };
							ls.MenuItem = ts;
							levelMenuItems.Add(ls);
							par2.DropDownItems.Add(ts);
						}
				}
				else if (!string.IsNullOrEmpty(grp[0].name))
				{
					LevelStuff ls = new LevelStuff() { FullName = grp[0].name, Stage = grp[0] };
					ToolStripMenuItem ts = new ToolStripMenuItem(grp[0].name, null, new EventHandler(LevelToolStripMenuItem_Clicked)) { Tag = ls };
					ls.MenuItem = ts;
					levelMenuItems.Add(ls);
					parent.DropDownItems.Add(ts);
				}
			}
			scriptFiles = new List<string>();
			
			// originally "Scripts" was supposed to be in the EXE folder, but both work
			// (and RE2 has it be in the Data folder) so let's support both, but with just "Scripts" at higher priority
			if (Directory.Exists(Path.Combine(LevelData.EXEFolder, "Scripts")))
				scriptFiles.AddRange(GetFilesRelative(Path.Combine(LevelData.EXEFolder, "Scripts"), "*.txt"));
			else if (Directory.Exists(Path.Combine(LevelData.EXEFolder, "Data/Scripts")))
				scriptFiles.AddRange(GetFilesRelative(Path.Combine(LevelData.EXEFolder, "Data/Scripts"), "*.txt"));
			
			// base decomp mods use "Data/Scripts" while S1F/S2A/SCDU mods use just "Scripts", let's go ahead and support 'em both too
			if (LevelData.ModFolder != null)
				if (Directory.Exists(Path.Combine(LevelData.ModFolder, "Data/Scripts")))
					scriptFiles.AddRange(GetFilesRelative(Path.Combine(Directory.GetCurrentDirectory(), LevelData.ModFolder, "Data/Scripts"), "*.txt").Where(a => !scriptFiles.Contains(a)));
				else if (Directory.Exists(Path.Combine(LevelData.ModFolder, "Scripts")))
					scriptFiles.AddRange(GetFilesRelative(Path.Combine(Directory.GetCurrentDirectory(), LevelData.ModFolder, "Scripts"), "*.txt").Where(a => !scriptFiles.Contains(a)));
			
			objectScriptBox.AutoCompleteCustomSource.Clear();
			objectScriptBox.AutoCompleteCustomSource.AddRange(scriptFiles.ToArray());

			sfxFiles = new List<string>();
			if (Directory.Exists(Path.Combine(LevelData.EXEFolder, "Data/SoundFX")))
			{
				sfxFiles.AddRange(GetFilesRelative(Path.Combine(LevelData.EXEFolder, "Data/SoundFX"), "*.wav"));
				if (LevelData.Game.RSDKVer == EngineVersion.V4)
					sfxFiles.AddRange(GetFilesRelative(Path.Combine(LevelData.EXEFolder, "Data/SoundFX"), "*.ogg"));
			}
			if (LevelData.ModFolder != null && Directory.Exists(Path.Combine(LevelData.ModFolder, "Data/SoundFX")))
			{
				sfxFiles.AddRange(GetFilesRelative(Path.Combine(Directory.GetCurrentDirectory(), LevelData.ModFolder, "Data/SoundFX"), "*.wav").Where(a => !sfxFiles.Contains(a)));
				if (LevelData.Game.RSDKVer == EngineVersion.V4)
					sfxFiles.AddRange(GetFilesRelative(Path.Combine(Directory.GetCurrentDirectory(), LevelData.ModFolder, "Data/SoundFX"), "*.ogg").Where(a => !sfxFiles.Contains(a)));
			}
			sfxFileBox.AutoCompleteCustomSource.Clear();
			sfxFileBox.AutoCompleteCustomSource.AddRange(sfxFiles.ToArray());
			if (Settings.RecentMods.Count == 0)
				recentModsToolStripMenuItem.DropDownItems.Remove(noneToolStripMenuItem);
			int ind = Settings.RecentMods.FindIndex(a => a.INIPath == LevelData.GamePath && a.ModPath == path);
			if (ind != -1)
			{
				recentModsToolStripMenuItem.DropDownItems.RemoveAt(ind);
				Settings.RecentMods.RemoveAt(ind);
			}
			string modname;
			if (path == null)
				modname = $"No Mod ({LevelData.GameTitle})";
			else
				modname = $"{IniSerializer.Deserialize<ModInfo>(path).Name ?? "Unknown Mod"} ({LevelData.GameTitle})";
			Settings.RecentMods.Insert(0, new MRUModItem(modname, LevelData.GamePath, path));
			recentModsToolStripMenuItem.DropDownItems.Insert(0, new ToolStripMenuItem(modname));
			Text = "SonLVL-RSDK - " + LevelData.GameTitle;
		}

		private IEnumerable<string> GetFilesRelative(string folder, string pattern) => Directory.EnumerateFiles(folder, pattern, SearchOption.AllDirectories).Select(a => a.Substring(folder.Length + 1).Replace(Path.DirectorySeparatorChar, '/'));

#region Main Menu
#region File Menu
		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (loaded && !saved)
			{
				switch (MessageBox.Show(this, "Do you want to save?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
				{
					case DialogResult.Yes:
						saveToolStripMenuItem_Click(this, EventArgs.Empty);
						break;
					case DialogResult.Cancel:
						return;
				}
			}
			using (OpenFileDialog a = new OpenFileDialog()
			{
				DefaultExt = "ini",
				Filter = "INI Files|*.ini|All Files|*.*"
			})
				if (a.ShowDialog(this) == DialogResult.OK)
				{
					loaded = false;
					LoadINI(a.FileName);
				}
		}

		private void editGameConfigToolStripMenuItem_Click(object sender, EventArgs e)
		{
			fileToolStripMenuItem.DropDown.Hide();
			if (loaded && !saved)
			{
				switch (MessageBox.Show(this, "Do you want to save?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
				{
					case DialogResult.Yes:
						saveToolStripMenuItem_Click(this, EventArgs.Empty);
						break;
					case DialogResult.Cancel:
						return;
				}
			}
			using (GameConfigEditorDialog dlg = new GameConfigEditorDialog(scriptFiles, sfxFiles))
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					loaded = false;
					LoadMod(Path.Combine(LevelData.ModFolder, "mod.ini"));
					if (LevelData.StageInfo != null)
					{
						LevelStuff stuff = levelMenuItems.FirstOrDefault(a => a.Stage.folder == LevelData.StageInfo.folder && a.Stage.actID == LevelData.StageInfo.actID);
						if (stuff != null)
						{
							stuff.MenuItem.Checked = true;
							Enabled = false;
							UseWaitCursor = true;
							levelname = stuff.FullName;
							LoadLevel(stuff);
						}
					}
				}
		}

		private void LevelToolStripMenuItem_Clicked(object sender, EventArgs e)
		{
			if (loaded && !saved)
			{
				fileToolStripMenuItem.DropDown.Hide();
				switch (MessageBox.Show(this, "Do you want to save?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
				{
					case DialogResult.Yes:
						saveToolStripMenuItem_Click(this, EventArgs.Empty);
						break;
					case DialogResult.Cancel:
						return;
				}
			}
			loaded = false;
			foreach (var item in levelMenuItems)
				item.MenuItem.Checked = false;
			ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
			menuitem.Checked = true;
			Enabled = false;
			UseWaitCursor = true;
			LevelStuff level = (LevelStuff)menuitem.Tag;
			levelname = level.FullName;
			LoadLevel(level);
		}

		private void LoadLevel(LevelStuff level)
		{
			Text = $"SonLVL-RSDK - {LevelData.GameTitle} - Loading {levelname}...";
#if !DEBUG
			initerror = null;
			backgroundLevelLoader.RunWorkerAsync(level);
#else
			LoadLevelPart2(level);
			LoadLevelPart3();
#endif
		}

		Exception initerror = null;
		private void backgroundLevelLoader_DoWork(object sender, DoWorkEventArgs e)
		{
#if !DEBUG
			try
#endif
			{
				LoadLevelPart2((LevelStuff)e.Argument);
			}
#if !DEBUG
			catch (Exception ex) { initerror = ex; }
#endif
		}

		private void LoadLevelPart2(LevelStuff argument)
		{
			SelectedChunk = 0;
			LevelData.LoadLevel(argument.Stage);
			undoSystem.Init();
			LevelImgPalette = new Bitmap(1, 1, PixelFormat.Format8bppIndexed).Palette;
			LevelData.BmpPal.Entries.CopyTo(LevelImgPalette.Entries, 0);
			if (Settings.BackgroundColor.A < 255)
				LevelImgPalette.Entries[LevelData.ColorTransparent] = LevelData.NewPalette[Settings.BackgroundColor.A];
			else
				LevelImgPalette.Entries[LevelData.ColorTransparent] = Settings.BackgroundColor;
		}

		private void backgroundLevelLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (initerror != null)
			{
				Log(initerror.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
				File.WriteAllLines("SonLVL-RSDK.log", LogFile.ToArray());
				string msg = initerror.GetType().Name + ": " + initerror.Message;
				if (initerror is AggregateException ae)
				{
					msg += " =>";
					foreach (Exception ex in ae.InnerExceptions)
						msg += Environment.NewLine + ex.GetType().Name + ": " + ex.Message;
				}
				using (LoadErrorDialog ed = new LoadErrorDialog(true, msg))
					ed.ShowDialog(this);
				Text = "SonLVL-RSDK - " + LevelData.GameTitle;
				Enabled = true;
				return;
			}
			LoadLevelPart3();
		}

		private void LoadLevelPart3()
		{
			Log("Load completed.");
			ChunkSelector.Images = LevelData.CompChunkBmps;
			ChunkSelector.SelectedIndex = 0;
			flipChunkHButton.Enabled = flipChunkVButton.Enabled = true;
			remapChunksButton.Enabled = remapTilesButton.Enabled = true;
			drawChunkToolStripButton.Enabled = importChunksToolStripButton.Enabled = LevelData.HasFreeChunks();
			drawTileToolStripButton.Enabled = importTilesToolStripButton.Enabled = LevelData.HasFreeTiles();
			TileSelector.Images = LevelData.NewTileBmps;
			TileSelector.SelectedIndex = 0;
			TileSelector.ChangeSize();
			if (ObjectSelect == null)
			{
				ObjectSelect = new ObjectList();
				ObjectSelect.listView1.SelectedIndexChanged += new EventHandler(ObjectSelect_listView1_SelectedIndexChanged);
				ObjectSelect.listView2.SelectedIndexChanged += new EventHandler(ObjectSelect_listView2_SelectedIndexChanged);
			}
			InitObjectTypes();
			Text = "SonLVL-RSDK - " + LevelData.GameTitle + " - " + this.levelname;
			UpdateScrollBars();
			objectPanel.HScrollValue = 0;
			objectPanel.HScrollMinimum = -128;
			objectPanel.HScrollSmallChange = 16;
			objectPanel.HScrollLargeChange = 128;
			objectPanel.VScrollValue = 0;
			objectPanel.VScrollMinimum = -128;
			objectPanel.VScrollSmallChange = 16;
			objectPanel.VScrollLargeChange = 128;
			objectPanel.HScrollEnabled = true;
			objectPanel.VScrollEnabled = true;
			foregroundPanel.HScrollValue = 0;
			foregroundPanel.HScrollMinimum = -128;
			foregroundPanel.HScrollSmallChange = 16;
			foregroundPanel.HScrollLargeChange = 128;
			foregroundPanel.VScrollValue = 0;
			foregroundPanel.VScrollMinimum = -128;
			foregroundPanel.VScrollSmallChange = 16;
			foregroundPanel.VScrollLargeChange = 128;
			foregroundPanel.HScrollEnabled = true;
			foregroundPanel.VScrollEnabled = true;
			backgroundPanel.HScrollValue = 0;
			backgroundPanel.HScrollSmallChange = 16;
			backgroundPanel.HScrollLargeChange = 128;
			backgroundPanel.VScrollValue = 0;
			backgroundPanel.VScrollSmallChange = 16;
			backgroundPanel.VScrollLargeChange = 128;
			backgroundPanel.HScrollEnabled = true;
			backgroundPanel.VScrollEnabled = true;
			colorEditingPanel.Enabled = true;
			paletteToolStrip.Enabled = true;
			string[] levnam = LevelData.Scene.title.Split('-');
			levelNameBox.Text = levnam[0];
			if (levnam.Length > 1)
				levelNameBox2.Text = levnam[1];
			else
				levelNameBox2.Text = string.Empty;
			levelNameBox.MaxLength = 255 - LevelData.Scene.title.Length;
			levelNameBox2.MaxLength = 255 - LevelData.Scene.title.Length;
			midpointBox.SelectedIndex = (int)LevelData.Scene.layerMidpoint;
			layer0Box.SelectedIndex = (int)LevelData.Scene.activeLayer0;
			layer1Box.SelectedIndex = (int)LevelData.Scene.activeLayer1;
			layer2Box.SelectedIndex = (int)LevelData.Scene.activeLayer2;
			layer3Box.SelectedIndex = (int)LevelData.Scene.activeLayer3;
			foregroundDeformation.Checked = LevelData.ForegroundDeformation;
			loadGlobalObjects.Checked = LevelData.StageConfig.loadGlobalObjects;
			objectListBox.BeginUpdate();
			objectListBox.Items.Clear();
			foreach (var item in LevelData.StageConfig.objects)
				objectListBox.Items.Add(item.name);
			objectListBox.EndUpdate();
			objectAddButton.Enabled = LevelData.ObjTypes.Count < 256;
			sfxListBox.BeginUpdate();
			sfxListBox.Items.Clear();
			foreach (var sfx in LevelData.StageConfig.soundFX)
				sfxListBox.Items.Add(sfx.name);
			sfxListBox.EndUpdate();
			sfxAddButton.Enabled = LevelData.StageConfig.soundFX.Count < 255;
			loaded = true;
			SelectedItems = new List<Entry>();
			saveToolStripMenuItem.Enabled = true;
			editToolStripMenuItem.Enabled = true;
			exportToolStripMenuItem.Enabled = true;
			if (invertColorsToolStripMenuItem.Checked)
				for (int i = 0; i < 256; i++)
					LevelImgPalette.Entries[i] = LevelImgPalette.Entries[i].Invert();
			objectListBox_SelectedIndexChanged(this, EventArgs.Empty);
			sfxListBox_SelectedIndexChanged(this, EventArgs.Empty);
			findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = false;
			
			if (File.Exists(LevelData.StageInfo.folder + ".sls"))
				using (FileStream fs = File.OpenRead(LevelData.StageInfo.folder + ".sls"))
					savedLayoutSections = (List<LayoutSection>)new BinaryFormatter().Deserialize(fs);
			else
				savedLayoutSections = new List<LayoutSection>();

			// migrate old act-specific data to folder-wide data
			string filename = this.levelname + ".sls";
			foreach (char c in Path.GetInvalidFileNameChars())
				filename = filename.Replace(c, '_');

			if (File.Exists(filename))
			{
				using (FileStream fs = File.OpenRead(filename))
					savedLayoutSections.AddRange((List<LayoutSection>)new BinaryFormatter().Deserialize(fs));
				File.Delete(filename);

				using (FileStream fs = File.Create(LevelData.StageInfo.folder + ".sls"))
					new BinaryFormatter().Serialize(fs, savedLayoutSections);
			}

			savedLayoutSectionImages = new List<Bitmap>();
			layoutSectionPreview.Image = null;
			layoutSectionListBox.BeginUpdate();
			layoutSectionListBox.Items.Clear();
			foreach (LayoutSection sec in savedLayoutSections)
			{
				layoutSectionListBox.Items.Add(sec.Name);
				savedLayoutSectionImages.Add(MakeLayoutSectionImage(sec, true));
			}
			layoutSectionListBox.EndUpdate();
			foundobjs = null;
			BGSelection = FGSelection = Rectangle.Empty;
			SelectedObjectChanged();
			UpdateScrollControls();
			ChunkID.Maximum = LevelData.NewChunks.chunkList.Length - 1;
			TileID.Maximum = LevelData.NewTiles.Length - 1;
			ChunkCount.Text = LevelData.NewChunks.chunkList.Length.ToString("X");
			TileCount.Text = LevelData.NewTiles.Length.ToString("X");
			deleteUnusedTilesToolStripButton.Enabled = deleteUnusedChunksToolStripButton.Enabled = ChunkID.Enabled = TileID.Enabled =
				removeDuplicateTilesToolStripButton.Enabled = copyCollisionAllButton.Enabled = copyCollisionSingleButton.Enabled = calculateAngleButton.Enabled =
				removeDuplicateChunksToolStripButton.Enabled = replaceChunkBlocksToolStripButton.Enabled = bgLayerDropDown.Enabled = reloadTilesToolStripButton.Enabled =
				resizeBackgroundToolStripButton.Enabled = replaceBackgroundToolStripButton.Enabled = resizeForegroundToolStripButton.Enabled = importToolStripButton.Enabled =
				deleteToolStripButton.Enabled = replaceForegroundToolStripButton.Enabled = clearBackgroundToolStripButton.Enabled = clearForegroundToolStripButton.Enabled =
				usageCountsToolStripMenuItem.Enabled = titleCardGroup.Enabled = layerSettingsGroup.Enabled = objectListGroup.Enabled = soundEffectsGroup.Enabled =
				objectPanel.PanelAllowDrop = objectOrder.AllowDrop = TileSelector.AllowDrop = true;
			undoToolStripMenuItem.Enabled = false;
			undoToolStripMenuItem.DropDownItems.Clear();
			redoToolStripMenuItem.Enabled = false;
			redoToolStripMenuItem.DropDownItems.Clear();
			Enabled = true;
			UseWaitCursor = false;
			saved = true;
			DrawLevel();
		}

		private void InitObjectTypes()
		{
			ObjectSelect.listView1.BeginUpdate();
			ObjectSelect.listView1.Items.Clear();
			ObjectSelect.imageList1.Images.Clear();
			objectTypeList.BeginUpdate();
			objectTypeList.Items.Clear();
			objectTypeImages.Images.Clear();
			objectTypeListMap.Clear();
			for (int i = 0; i < LevelData.ObjTypes.Count; i++)
			{
				Bitmap image = LevelData.ObjTypes[i].Image.GetBitmap().ToBitmap(LevelData.BmpPal);
				ObjectSelect.imageList1.Images.Add(image.Resize(ObjectSelect.imageList1.ImageSize));
				objectTypeImages.Images.Add(image.Resize(objectTypeImages.ImageSize));
				if (!LevelData.ObjTypes[i].Hidden)
				{
					objectTypeListMap.Add(i, objectTypeList.Items.Count);
					ObjectSelect.listView1.Items.Add(new ListViewItem((i == 0) ? "Blank Object" : LevelData.ObjTypes[i].Name, ObjectSelect.imageList1.Images.Count - 1) { Tag = (byte)i });
					objectTypeList.Items.Add(new ListViewItem((i == 0) ? "Blank Object" : LevelData.ObjTypes[i].Name, objectTypeImages.Images.Count - 1) { Tag = (byte)i });
				}
			}
			ObjectSelect.listView1.EndUpdate();
			objectTypeList.EndUpdate();
			ObjectSelect.listView2.Items.Clear();
			ObjectSelect.imageList2.Images.Clear();
			objectOrder.BeginUpdate();
			objectOrder.Items.Clear();
			foreach (var obj in LevelData.Objects)
				objectOrder.Items.Add(obj.Name, obj.Type < objectTypeImages.Images.Count ? obj.Type : 0);
			objectOrder.EndUpdate();
		}

		private Bitmap MakeLayoutSectionImage(LayoutSection sec, bool transparent)
		{
			int w = sec.Layout.GetLength(0), h = sec.Layout.GetLength(1);
			BitmapBits bmp = new BitmapBits(w * 128, h * 128);
			for (int y = 0; y < h; y++)
				for (int x = 0; x < w; x++)
					if (sec.Layout[x, y] < LevelData.NewChunks.chunkList.Length)
						bmp.DrawSpriteLow(LevelData.ChunkSprites[sec.Layout[x, y]], x * 128, y * 128);
			foreach (Entry ent in sec.Objects)
			{
				ent.UpdateSprite();
				bmp.DrawSprite(ent.Sprite, ent.X, ent.Y);
			}
			for (int y = 0; y < h; y++)
				for (int x = 0; x < w; x++)
					if (sec.Layout[x, y] < LevelData.NewChunks.chunkList.Length)
						bmp.DrawSpriteHigh(LevelData.ChunkSprites[sec.Layout[x, y]], x * 128, y * 128);
			return transparent ? LevelData.BitmapBitsToBitmap(bmp) : bmp.ToBitmap(LevelImgPalette);
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LevelData.SaveLevel();
			saved = true;
		}

		private void buildAndRunToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (LevelData.Scene != null)
				saveToolStripMenuItem_Click(sender, e);

			string path = Path.GetFullPath(LevelData.Game.EXEFile);

			if (!File.Exists(path))
			{
				if (MessageBox.Show(this, $"Unable to locate game executable at \"{path}\".\n\nWould you like to select the game executable again?", "SonLVL-RSDK", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
					return;
				
				using (OpenFileDialog a = new OpenFileDialog()
				{
					DefaultExt = "exe",
					Filter = "EXE Files|*.exe|All Files|*.*",
					Title = "Select your game's EXE",
					InitialDirectory = Path.GetDirectoryName(path)
				})
					if (a.ShowDialog(this) == DialogResult.OK)
					{
						LevelData.Game.EXEFile = a.FileName;
						path = Path.GetFullPath(LevelData.Game.EXEFile);
						LevelData.Game.Save(LevelData.GamePath);
					}
					else
						return;
			}

			if (loaded)
				System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path, $"stage={LevelData.StageInfo.folder} scene={LevelData.StageInfo.actID}") { WorkingDirectory = LevelData.EXEFolder });
			else
				System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { WorkingDirectory = LevelData.EXEFolder });
		}

		private void recentProjectsToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			loaded = false;
			LoadINI(Settings.MRUList[recentProjectsToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem)]);
		}

		private void recentModsToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			loaded = false;
			MRUModItem item = Settings.RecentMods[recentModsToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem)];
			LoadINI(item.INIPath);
			LoadMod(item.ModPath);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region Edit Menu
		private void SaveState(string name)
		{
			if (undoSystem.CreateState(name))
			{
				redoToolStripMenuItem.DropDownItems.Clear();
				redoToolStripMenuItem.Enabled = false;
				undoToolStripMenuItem.DropDownItems.Insert(0, new ToolStripMenuItem(name));
				undoToolStripMenuItem.Enabled = true;
				saved = false;
			}
		}

		byte[][] prevTiles;
		RSDKv3_4.TileConfig.CollisionMask[][] prevCol;
		RSDKv3_4.Tiles128x128.Block[] prevChunks;
		private void SaveArtData()
		{
			prevTiles = LevelData.NewTiles.Select(a => (byte[])a.Bits.Clone()).ToArray();
			prevCol = LevelData.Collision.collisionMasks.Select(a => a.Select(b => b.Clone()).ToArray()).ToArray();
			prevChunks = LevelData.NewChunks.chunkList.Select(a => a.Clone()).ToArray();
		}

		private void RefreshLevel()
		{
			loaded = false;
			LevelData.Objects = new List<ObjectEntry>(LevelData.Scene.entities.Count);
			foreach (var item in LevelData.Scene.entities)
				LevelData.Objects.Add(ObjectEntry.Create(item));
			foreach (var obj in LevelData.Objects)
				obj.UpdateSprite();
			/*
			for (int i = 0; i < 8; i++)
			{
				LevelData.BGScroll[i] = new List<ScrollData>();
				List<RSDKv3_4.Backgrounds.ScrollInfo> info;
				switch (LevelData.Background.layers[i].type)
				{
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
						info = LevelData.Background.hScroll;
						break;
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
						info = LevelData.Background.vScroll;
						break;
					default:
						continue;
				}
				if (info.Count > 0)
				{
					int lastind = -1;
					for (ushort y = 0; y < LevelData.Background.layers[i].lineScroll.Length; y++)
						if (LevelData.Background.layers[i].lineScroll[y] != lastind)
						{
							lastind = LevelData.Background.layers[i].lineScroll[y];
							LevelData.BGScroll[i].Add(new ScrollData(y, info[lastind]));
						}
				}
				else
					LevelData.BGScroll[i].Add(new ScrollData());
			}
			*/
			var redrawblocks = new SortedSet<int>();
			for (int bi = 0; bi < LevelData.NewTiles.Length; bi++)
				if (!prevTiles[bi].FastArrayEqual(LevelData.NewTiles[bi].Bits))
				{
					LevelData.RedrawBlock(bi, false);
					redrawblocks.Add(bi);
				}
			for (int i = 0; i < LevelData.Collision.collisionMasks[0].Length; i++)
				if (!prevCol[0][i].Equal(LevelData.Collision.collisionMasks[0][i]) || !prevCol[1][i].Equal(LevelData.Collision.collisionMasks[1][i]))
				{
					LevelData.RedrawCol(i, false);
					redrawblocks.Add(i);
				}
			for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
				if (!prevChunks[i].Equal(LevelData.NewChunks.chunkList[i]) || LevelData.NewChunks.chunkList[i].tiles.SelectMany(a => a).Any(b => redrawblocks.Contains(b.tileIndex)))
					LevelData.RedrawChunk(i);
			drawChunkToolStripButton.Enabled = importChunksToolStripButton.Enabled = LevelData.HasFreeChunks();
			drawTileToolStripButton.Enabled = importTilesToolStripButton.Enabled = LevelData.HasFreeTiles();
			InitObjectTypes();
			UpdateScrollBars();
			string[] levnam = LevelData.Scene.title.Split('-');
			levelNameBox.Text = levnam[0];
			if (levnam.Length > 1)
				levelNameBox2.Text = levnam[1];
			else
				levelNameBox2.Text = string.Empty;
			levelNameBox.MaxLength = 255 - LevelData.Scene.title.Length;
			levelNameBox2.MaxLength = 255 - LevelData.Scene.title.Length;
			midpointBox.SelectedIndex = (int)LevelData.Scene.layerMidpoint;
			layer0Box.SelectedIndex = (int)LevelData.Scene.activeLayer0;
			layer1Box.SelectedIndex = (int)LevelData.Scene.activeLayer1;
			layer2Box.SelectedIndex = (int)LevelData.Scene.activeLayer2;
			layer3Box.SelectedIndex = (int)LevelData.Scene.activeLayer3;
			foregroundDeformation.Checked = LevelData.ForegroundDeformation;
			loadGlobalObjects.Checked = LevelData.StageConfig.loadGlobalObjects;
			objectListBox.BeginUpdate();
			objectListBox.Items.Clear();
			foreach (var item in LevelData.StageConfig.objects)
				objectListBox.Items.Add(item.name);
			objectListBox.EndUpdate();
			objectAddButton.Enabled = LevelData.ObjTypes.Count < 256;
			sfxListBox.BeginUpdate();
			sfxListBox.Items.Clear();
			foreach (var sfx in LevelData.StageConfig.soundFX)
				sfxListBox.Items.Add(sfx.name);
			sfxListBox.EndUpdate();
			sfxAddButton.Enabled = LevelData.StageConfig.soundFX.Count < 255;
			loaded = true;
			SelectedItems = new List<Entry>();
			findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = false;
			LevelData.PaletteChanged();
			foundobjs = null;
			SelectedObjectChanged();
			UpdateScrollControls();
			ChunkID.Maximum = LevelData.NewChunks.chunkList.Length - 1;
			TileID.Maximum = LevelData.NewTiles.Length - 1;
			ChunkCount.Text = LevelData.NewChunks.chunkList.Length.ToString("X");
			TileCount.Text = LevelData.NewTiles.Length.ToString("X");
			DrawLevel();
		}

		private void Undo()
		{
			SaveArtData();
			undoSystem.Undo();
			ToolStripItem item = undoToolStripMenuItem.DropDownItems[0];
			undoToolStripMenuItem.DropDownItems.RemoveAt(0);
			redoToolStripMenuItem.DropDownItems.Insert(0, item);
			undoToolStripMenuItem.Enabled = undoSystem.CanUndo;
			redoToolStripMenuItem.Enabled = true;
			RefreshLevel();
			
			saved = false;
		}

		private void Redo()
		{
			SaveArtData();
			undoSystem.Redo();
			ToolStripItem item = redoToolStripMenuItem.DropDownItems[0];
			redoToolStripMenuItem.DropDownItems.RemoveAt(0);
			undoToolStripMenuItem.DropDownItems.Insert(0, item);
			redoToolStripMenuItem.Enabled = undoSystem.CanRedo;
			undoToolStripMenuItem.Enabled = true;
			RefreshLevel();

			saved = false;
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e) => Undo();

		private void undoToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			SaveArtData();
			int count = undoToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem) + 1;
			undoSystem.Undo(count);
			for (int i = 0; i < count; i++)
			{
				ToolStripItem item = undoToolStripMenuItem.DropDownItems[0];
				undoToolStripMenuItem.DropDownItems.RemoveAt(0);
				redoToolStripMenuItem.DropDownItems.Insert(0, item);
			}
			undoToolStripMenuItem.Enabled = undoSystem.CanUndo;
			redoToolStripMenuItem.Enabled = true;
			RefreshLevel();

			saved = false;
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e) => Redo();

		private void redoToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			SaveArtData();
			int count = redoToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem) + 1;
			undoSystem.Redo(count);
			for (int i = 0; i < count; i++)
			{
				ToolStripItem item = redoToolStripMenuItem.DropDownItems[0];
				redoToolStripMenuItem.DropDownItems.RemoveAt(0);
				undoToolStripMenuItem.DropDownItems.Insert(0, item);
			}
			redoToolStripMenuItem.Enabled = undoSystem.CanRedo;
			undoToolStripMenuItem.Enabled = true;
			RefreshLevel();

			saved = false;
		}

		List<ObjectEntry> foundobjs;
		int lastfoundobj;
		Point? lastfoundfgchunk;
		ushort searchfgchunk;
		Point? lastfoundbgchunk;
		ushort searchbgchunk;
		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (CurrentTab)
			{
				case Tab.Objects:
					DialogResult res = findObjectsDialog.ShowDialog(this);
					if (res != DialogResult.Yes && res != DialogResult.OK)
						return;
					foundobjs = new List<ObjectEntry>(LevelData.Objects);
					byte? id = findObjectsDialog.ID;
					if (id.HasValue)
						foundobjs = new List<ObjectEntry>(foundobjs.Where(a => a.Type == id.Value));
					byte? sub = findObjectsDialog.SubType;
					if (sub.HasValue)
						foundobjs = new List<ObjectEntry>(foundobjs.Where(a => a.PropertyValue == sub.Value));
					SelectedItems.Clear();
					switch (res)
					{
						case DialogResult.Yes:
							SelectedItems.AddRange(foundobjs.OfType<Entry>());
							if (SelectedItems.Count > 0)
								MessageBox.Show(this, SelectedItems.Count + " object" + (SelectedItems.Count > 1 ? "s" : "") + " found.",
									"SonLVL-RSDK");
							break;
						case DialogResult.OK:
							if (foundobjs.Count > 0)
								SelectedItems.Add(foundobjs[0]);
							break;
					}
					if (SelectedItems.Count > 0)
					{
						ScrollToObject(SelectedItems[0]);
						lastfoundobj = 0;
						findNextToolStripMenuItem.Enabled = foundobjs.Count > 1;
						findPreviousToolStripMenuItem.Enabled = false;
					}
					else
					{
						MessageBox.Show(this, "No matching objects found.", "SonLVL-RSDK");
						findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = false;
						foundobjs = null;
					}
					SelectedObjectChanged();
					DrawLevel();
					break;
				case Tab.Foreground:
					switch (findFGChunksDialog.ShowDialog(this))
					{
						case DialogResult.Yes:
							int count = 0;
							for (int x = 0; x < LevelData.FGWidth; x++)
								for (int y = 0; y < LevelData.FGHeight; y++)
									if (LevelData.Scene.layout[y][x] == findFGChunksDialog.chunkSelect.Value)
										count++;
							MessageBox.Show(this, count + " chunk" + (count != 1 ? "s" : "") + " found.",
								"SonLVL-RSDK");
							break;
						case DialogResult.OK:
							for (int x = 0; x < LevelData.FGWidth; x++)
								for (int y = 0; y < LevelData.FGHeight; y++)
									if (LevelData.Scene.layout[y][x] == findFGChunksDialog.chunkSelect.Value)
									{
										lastfoundfgchunk = new Point(x, y);
										searchfgchunk = (ushort)findFGChunksDialog.chunkSelect.Value;
										findNextToolStripMenuItem.Enabled = true;
										findPreviousToolStripMenuItem.Enabled = false;
										FGSelection = new Rectangle(x, y, 1, 1);
										loaded = false;
										objectPanel.HScrollValue = (int)Math.Max(objectPanel.HScrollMinimum, Math.Min(objectPanel.HScrollMaximum - objectPanel.HScrollLargeChange + 1,
											(x * 128) + (128 / 2) - ((objectPanel.PanelWidth / 2) / ZoomLevel)));
										objectPanel.VScrollValue = (int)Math.Max(objectPanel.VScrollMinimum, Math.Min(objectPanel.VScrollMaximum - objectPanel.VScrollLargeChange + 1,
											(y * 128) + (128 / 2) - ((objectPanel.PanelHeight / 2) / ZoomLevel)));
										foregroundPanel.HScrollValue = (int)Math.Max(foregroundPanel.HScrollMinimum, Math.Min(foregroundPanel.HScrollMaximum - foregroundPanel.HScrollLargeChange + 1,
											(x * 128) + (128 / 2) - ((foregroundPanel.PanelWidth / 2) / ZoomLevel)));
										foregroundPanel.VScrollValue = (int)Math.Max(foregroundPanel.VScrollMinimum, Math.Min(foregroundPanel.VScrollMaximum - foregroundPanel.VScrollLargeChange + 1,
											(y * 128) + (128 / 2) - ((foregroundPanel.PanelHeight / 2) / ZoomLevel)));
										loaded = true;
										DrawLevel();
										return;
									}
							MessageBox.Show(this, "No matching chunks found.", "SonLVL-RSDK");
							lastfoundfgchunk = null;
							findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = false;
							break;
					}
					break;
				case Tab.Background:
					switch (findBGChunksDialog.ShowDialog(this))
					{
						case DialogResult.Yes:
							int count = 0;
							for (int x = 0; x < LevelData.BGWidth[bglayer]; x++)
								for (int y = 0; y < LevelData.BGHeight[bglayer]; y++)
									if (LevelData.Background.layers[bglayer].layout[y][x] == findBGChunksDialog.chunkSelect.Value)
										count++;
							MessageBox.Show(this, count + " chunk" + (count != 1 ? "s" : "") + " found.",
								"SonLVL-RSDK");
							break;
						case DialogResult.OK:
							for (int x = 0; x < LevelData.BGWidth[bglayer]; x++)
								for (int y = 0; y < LevelData.BGHeight[bglayer]; y++)
									if (LevelData.Background.layers[bglayer].layout[y][x] == findBGChunksDialog.chunkSelect.Value)
									{
										lastfoundbgchunk = new Point(x, y);
										searchbgchunk = (ushort)findBGChunksDialog.chunkSelect.Value;
										findNextToolStripMenuItem.Enabled = true;
										findPreviousToolStripMenuItem.Enabled = false;
										BGSelection = new Rectangle(x, y, 1, 1);
										loaded = false;
										backgroundPanel.HScrollValue = (int)Math.Max(backgroundPanel.HScrollMinimum, Math.Min(backgroundPanel.HScrollMaximum - backgroundPanel.HScrollLargeChange + 1,
											(x * 128) + (128 / 2) - ((backgroundPanel.PanelWidth / 2) / ZoomLevel)));
										backgroundPanel.VScrollValue = (int)Math.Max(backgroundPanel.VScrollMinimum, Math.Min(backgroundPanel.VScrollMaximum - backgroundPanel.VScrollLargeChange + 1,
											(y * 128) + (128 / 2) - ((backgroundPanel.PanelHeight / 2) / ZoomLevel)));
										loaded = true;
										DrawLevel();
										return;
									}
							MessageBox.Show(this, "No matching chunks found.", "SonLVL-RSDK");
							lastfoundbgchunk = null;
							findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = false;
							break;
					}
					break;
			}
		}

		private void ScrollToObject(Entry item)
		{
			loaded = false;
			objectPanel.HScrollValue = (int)Math.Max(objectPanel.HScrollMinimum, Math.Min(objectPanel.HScrollMaximum - objectPanel.HScrollLargeChange + 1, item.X - ((objectPanel.PanelWidth / 2) / ZoomLevel)));
			objectPanel.VScrollValue = (int)Math.Max(objectPanel.VScrollMinimum, Math.Min(objectPanel.VScrollMaximum - objectPanel.VScrollLargeChange + 1, item.Y - ((objectPanel.PanelHeight / 2) / ZoomLevel)));
			foregroundPanel.HScrollValue = (int)Math.Max(foregroundPanel.HScrollMinimum, Math.Min(foregroundPanel.HScrollMaximum - foregroundPanel.HScrollLargeChange + 1, item.X - ((foregroundPanel.PanelWidth / 2) / ZoomLevel)));
			foregroundPanel.VScrollValue = (int)Math.Max(foregroundPanel.VScrollMinimum, Math.Min(foregroundPanel.VScrollMaximum - foregroundPanel.VScrollLargeChange + 1, item.Y - ((foregroundPanel.PanelHeight / 2) / ZoomLevel)));
			loaded = true;
		}

		private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (CurrentTab)
			{
				case Tab.Objects:
					if (lastfoundobj < foundobjs.Count - 1)
					{
						SelectedItems.Clear();
						SelectedItems.Add(foundobjs[++lastfoundobj]);
						ScrollToObject(SelectedItems[0]);
						findPreviousToolStripMenuItem.Enabled = true;
						SelectedObjectChanged();
						DrawLevel();
					}
					else
					{
						MessageBox.Show(this, "No more objects found.", "SonLVL-RSDK");
						findNextToolStripMenuItem.Enabled = false;
					}
					break;
				case Tab.Foreground:
					for (int x = 0; x < LevelData.FGWidth; x++)
						for (int y = 0; y < LevelData.FGHeight; y++)
						{
							if (x == 0 && y == 0)
							{
								x = lastfoundfgchunk.Value.X;
								y = lastfoundfgchunk.Value.Y;
							}
							else if (LevelData.Scene.layout[y][x] == searchfgchunk)
							{
								lastfoundfgchunk = new Point(x, y);
								findPreviousToolStripMenuItem.Enabled = true;
								FGSelection = new Rectangle(x, y, 1, 1);
								loaded = false;
								foregroundPanel.HScrollValue = (int)Math.Max(foregroundPanel.HScrollMinimum, Math.Min(foregroundPanel.HScrollMaximum - foregroundPanel.HScrollLargeChange + 1,
									(x * 128) + (128 / 2) - ((foregroundPanel.PanelWidth / 2) / ZoomLevel)));
								foregroundPanel.VScrollValue = (int)Math.Max(foregroundPanel.VScrollMinimum, Math.Min(foregroundPanel.VScrollMaximum - foregroundPanel.VScrollLargeChange + 1,
									(y * 128) + (128 / 2) - ((foregroundPanel.PanelHeight / 2) / ZoomLevel)));
								loaded = true;
								DrawLevel();
								return;
							}
						}
					MessageBox.Show(this, "No more chunks found.", "SonLVL-RSDK");
					findNextToolStripMenuItem.Enabled = false;
					break;
				case Tab.Background:
					for (int x = 0; x < LevelData.BGWidth[bglayer]; x++)
						for (int y = 0; y < LevelData.BGHeight[bglayer]; y++)
						{
							if (x == 0 && y == 0)
							{
								x = lastfoundbgchunk.Value.X;
								y = lastfoundbgchunk.Value.Y;
							}
							else if (LevelData.Background.layers[bglayer].layout[y][x] == searchbgchunk)
							{
								lastfoundbgchunk = new Point(x, y);
								findPreviousToolStripMenuItem.Enabled = true;
								BGSelection = new Rectangle(x, y, 1, 1);
								loaded = false;
								backgroundPanel.HScrollValue = (int)Math.Max(backgroundPanel.HScrollMinimum, Math.Min(backgroundPanel.HScrollMaximum - backgroundPanel.HScrollLargeChange + 1,
									(x * 128) + (128 / 2) - ((backgroundPanel.PanelWidth / 2) / ZoomLevel)));
								backgroundPanel.VScrollValue = (int)Math.Max(backgroundPanel.VScrollMinimum, Math.Min(backgroundPanel.VScrollMaximum - backgroundPanel.VScrollLargeChange + 1,
									(y * 128) + (128 / 2) - ((backgroundPanel.PanelHeight / 2) / ZoomLevel)));
								loaded = true;
								DrawLevel();
								return;
							}
						}
					MessageBox.Show(this, "No more chunks found.", "SonLVL-RSDK");
					findNextToolStripMenuItem.Enabled = false;
					break;
			}
		}

		private void findPreviousToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (CurrentTab)
			{
				case Tab.Objects:
					if (lastfoundobj > 0)
					{
						SelectedItems.Clear();
						SelectedItems.Add(foundobjs[--lastfoundobj]);
						ScrollToObject(SelectedItems[0]);
						findNextToolStripMenuItem.Enabled = true;
						SelectedObjectChanged();
						DrawLevel();
					}
					else
					{
						MessageBox.Show(this, "No more objects found.", "SonLVL-RSDK");
						findPreviousToolStripMenuItem.Enabled = false;
					}
					break;
				case Tab.Foreground:
					for (int x = (LevelData.FGWidth - 1); x >= 0; x--)
						for (int y = (LevelData.FGHeight - 1); y >= 0; y--)
						{
							if (x == (LevelData.FGWidth - 1) && y == (LevelData.FGHeight - 1))
							{
								x = lastfoundfgchunk.Value.X;
								y = lastfoundfgchunk.Value.Y;
							}
							else if (LevelData.Scene.layout[y][x] == searchfgchunk)
							{
								lastfoundfgchunk = new Point(x, y);
								findNextToolStripMenuItem.Enabled = true;
								FGSelection = new Rectangle(x, y, 1, 1);
								loaded = false;
								foregroundPanel.HScrollValue = (int)Math.Max(foregroundPanel.HScrollMinimum, Math.Min(foregroundPanel.HScrollMaximum - foregroundPanel.HScrollLargeChange + 1,
									(x * 128) + (128 / 2) - ((foregroundPanel.PanelWidth / 2) / ZoomLevel)));
								foregroundPanel.VScrollValue = (int)Math.Max(foregroundPanel.VScrollMinimum, Math.Min(foregroundPanel.VScrollMaximum - foregroundPanel.VScrollLargeChange + 1,
									(y * 128) + (128 / 2) - ((foregroundPanel.PanelHeight / 2) / ZoomLevel)));
								loaded = true;
								DrawLevel();
								return;
							}
						}
					MessageBox.Show(this, "No more chunks found.", "SonLVL-RSDK");
					findPreviousToolStripMenuItem.Enabled = false;
					break;
				case Tab.Background:
					for (int x = (LevelData.BGWidth[bglayer] - 1); x >= 0; x--)
						for (int y = (LevelData.BGHeight[bglayer] - 1); y >= 0; y--)
						{
							if (x == (LevelData.BGWidth[bglayer] - 1) && y == (LevelData.BGHeight[bglayer] - 1))
							{
								x = lastfoundbgchunk.Value.X;
								y = lastfoundbgchunk.Value.Y;
							}
							else if (LevelData.Background.layers[bglayer].layout[y][x] == searchbgchunk)
							{
								lastfoundbgchunk = new Point(x, y);
								findNextToolStripMenuItem.Enabled = true;
								BGSelection = new Rectangle(x, y, 1, 1);
								loaded = false;
								backgroundPanel.HScrollValue = (int)Math.Max(backgroundPanel.HScrollMinimum, Math.Min(backgroundPanel.HScrollMaximum - backgroundPanel.HScrollLargeChange + 1,
									(x * 128) + (128 / 2) - ((backgroundPanel.PanelWidth / 2) / ZoomLevel)));
								backgroundPanel.VScrollValue = (int)Math.Max(backgroundPanel.VScrollMinimum, Math.Min(backgroundPanel.VScrollMaximum - backgroundPanel.VScrollLargeChange + 1,
									(y * 128) + (128 / 2) - ((backgroundPanel.PanelHeight / 2) / ZoomLevel)));
								loaded = true;
								DrawLevel();
								return;
							}
						}
					MessageBox.Show(this, "No more chunks found.", "SonLVL-RSDK");
					findPreviousToolStripMenuItem.Enabled = false;
					break;
			}
		}

		private void resizeLevelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (ResizeLevelDialog dg = new ResizeLevelDialog())
			{
				Size cursize;
				if (CurrentTab == Tab.Background)
					cursize = LevelData.BGSize[bglayer];
				else
					cursize = LevelData.FGSize;
				if (cursize.IsEmpty)
					cursize = new Size(1, 1);
				dg.levelHeight.Value = cursize.Height;
				dg.levelWidth.Value = cursize.Width;
				if (dg.ShowDialog(this) == DialogResult.OK)
				{
					if (CurrentTab == Tab.Background)
					{
						LevelData.ResizeBG(bglayer, (int)dg.levelWidth.Value, (int)dg.levelHeight.Value);
						if (LevelData.BGScroll[bglayer].Count == 0)
							LevelData.BGScroll[bglayer].Add(new ScrollData());

						BGSelection.Width = Math.Min(BGSelection.Right, LevelData.BGWidth[bglayer]) - BGSelection.Left;
						BGSelection.Height = Math.Min(BGSelection.Bottom, LevelData.BGHeight[bglayer]) - BGSelection.Top;
					}
					else
					{
						LevelData.ResizeFG((int)dg.levelWidth.Value, (int)dg.levelHeight.Value);

						FGSelection.Width = Math.Min(FGSelection.Right, LevelData.FGWidth) - FGSelection.Left;
						FGSelection.Height = Math.Min(FGSelection.Bottom, LevelData.FGHeight) - FGSelection.Top;
					}
					loaded = false;
					UpdateScrollBars();
					loaded = true;
					UpdateScrollControls();
					DrawLevel();
					SaveState($"Resize {(CurrentTab == Tab.Background ? $"Background {bglayer + 1}" : "Foreground")}");
				}
			}
		}

		private void clearLevelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "This will reset ALL data for this level. Are you sure?", "Clear Level", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
			{
				loaded = false;
				LevelData.ClearLevel();
				LevelData.BmpPal.Entries.CopyTo(LevelImgPalette.Entries, 0);
				if (Settings.BackgroundColor.A < 255)
					LevelImgPalette.Entries[LevelData.ColorTransparent] = LevelData.NewPalette[Settings.BackgroundColor.A];
				else
					LevelImgPalette.Entries[LevelData.ColorTransparent] = Settings.BackgroundColor;
				ChunkSelector.Images = LevelData.CompChunkBmps;
				ChunkSelector.SelectedIndex = 0;
				importChunksToolStripButton.Enabled = true;
				drawChunkToolStripButton.Enabled = importChunksToolStripButton.Enabled;
				importTilesToolStripButton.Enabled = true;
				drawTileToolStripButton.Enabled = importTilesToolStripButton.Enabled;
				TileSelector.Images = LevelData.NewTileBmps;
				TileSelector.SelectedIndex = 0;
				TileSelector.ChangeSize();
				InitObjectTypes();
				UpdateScrollBars();
				string[] levnam = LevelData.Scene.title.Split('-');
				levelNameBox.Text = levnam[0];
				if (levnam.Length > 1)
					levelNameBox2.Text = levnam[1];
				levelNameBox.MaxLength = 255 - LevelData.Scene.title.Length;
				levelNameBox2.MaxLength = 255 - LevelData.Scene.title.Length;
				midpointBox.SelectedIndex = (int)LevelData.Scene.layerMidpoint;
				layer0Box.SelectedIndex = (int)LevelData.Scene.activeLayer0;
				layer1Box.SelectedIndex = (int)LevelData.Scene.activeLayer1;
				layer2Box.SelectedIndex = (int)LevelData.Scene.activeLayer2;
				layer3Box.SelectedIndex = (int)LevelData.Scene.activeLayer3;
				foregroundDeformation.Checked = LevelData.ForegroundDeformation;
				loadGlobalObjects.Checked = LevelData.StageConfig.loadGlobalObjects;
				objectListBox.Items.Clear();
				objectAddButton.Enabled = LevelData.ObjTypes.Count < 256;
				sfxListBox.Items.Clear();
				sfxAddButton.Enabled = true;
				loaded = true;
				SelectedItems = new List<Entry>();
				if (invertColorsToolStripMenuItem.Checked)
					for (int i = 0; i < 256; i++)
						LevelImgPalette.Entries[i] = LevelImgPalette.Entries[i].Invert();
				findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = false;
				savedLayoutSections = new List<LayoutSection>();
				savedLayoutSectionImages = new List<Bitmap>();
				layoutSectionListBox.Items.Clear();
				foundobjs = null;
				SelectedObjectChanged();
				UpdateScrollControls();
				ChunkID.Maximum = LevelData.NewChunks.chunkList.Length - 1;
				TileID.Maximum = LevelData.NewTiles.Length - 1;
				ChunkCount.Text = LevelData.NewChunks.chunkList.Length.ToString("X");
				TileCount.Text = LevelData.NewTiles.Length.ToString("X");
				DrawLevel();
				SaveState("Clear Level");
			}
		}

		private void switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			if (switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem.Checked)
			{
				chunkblockMouseDraw = MouseButtons.Right;
				chunkblockMouseSelect = MouseButtons.Left;
				chunkCtrlLabel.Text = "RMB: Paint w/ selected tile\nLMB: Select tile";
			}
			else
			{
				chunkblockMouseDraw = MouseButtons.Left;
				chunkblockMouseSelect = MouseButtons.Right;
				chunkCtrlLabel.Text = "LMB: Paint w/ selected tile\nRMB: Select tile";
			}
			Settings.SwitchChunkBlockMouseButtons = switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem.Checked;
		}
		#endregion

		#region View Menu
		private void includeObjectsWithFGToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			Settings.IncludeObjectsFG = includeobjectsWithFGToolStripMenuItem.Checked;
			DrawLevel();
		}

		private void objectsAboveHighPlaneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			objectsAboveHighPlaneToolStripMenuItem.Checked = !objectsAboveHighPlaneToolStripMenuItem.Checked;
			DrawLevel();
		}

		private void invertColorsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LevelData_PaletteChangedEvent();
		}

		private void bgColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (BackgroundColorDialog dialog = new BackgroundColorDialog())
			{
				if (Settings.BackgroundColor.A == 255)
				{
					dialog.index.Value = 160;
					dialog.index.Enabled = false;
					dialog.useConstantColor.Checked = true;
				}
				else
				{
					dialog.index.Value = Settings.BackgroundColor.A;
					dialog.constantColorOverlay.Visible = dialog.useLevelColor.Checked = true;
				}

				dialog.constantColorBox.BackColor = Color.FromArgb(255, Settings.BackgroundColor);

				if (dialog.ShowDialog(this) == DialogResult.OK)
				{
					if (dialog.useLevelColor.Checked)
						Settings.BackgroundColor = Color.FromArgb((int)dialog.index.Value, Settings.BackgroundColor);
					else
						Settings.BackgroundColor = dialog.constantColorBox.BackColor;

					if (loaded)
					{
						if (Settings.BackgroundColor.A < 255)
							LevelImgPalette.Entries[LevelData.ColorTransparent] = LevelData.NewPalette[Settings.BackgroundColor.A];
						else
							LevelImgPalette.Entries[LevelData.ColorTransparent] = Settings.BackgroundColor;

						DrawLevel();
					}
				}
			}
		}

		private void lowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DrawLevel();
			DrawChunkPicture();
		}

		private void highToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DrawLevel();
			DrawChunkPicture();
		}

		private void collisionToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (collisionToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem) < 3)
			{
				bool angles = anglesToolStripMenuItem.Checked;
				foreach (ToolStripItem item in collisionToolStripMenuItem.DropDownItems)
					if (item is ToolStripMenuItem item1)
						item1.Checked = false;
				((ToolStripMenuItem)e.ClickedItem).Checked = true;
				anglesToolStripMenuItem.Checked = angles;
				DrawLevel();
				DrawChunkPicture();
			}
		}

		private void anglesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			anglesToolStripMenuItem.Checked = !anglesToolStripMenuItem.Checked;
			DrawLevel();
		}

		private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ColorDialog a = new ColorDialog
			{
				AllowFullOpen = true,
				AnyColor = true,
				FullOpen = true,
				SolidColorOnly = true,
				Color = Settings.GridColor
			};
			if (cols != null)
				a.CustomColors = cols;
			if (a.ShowDialog() == DialogResult.OK)
			{
				Settings.GridColor = a.Color;
				if (loaded)
					DrawLevel();
			}
			cols = a.CustomColors;
			a.Dispose();
		}

		private void zoomToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			foreach (ToolStripMenuItem item in zoomToolStripMenuItem.DropDownItems)
				item.Checked = false;
			((ToolStripMenuItem)e.ClickedItem).Checked = true;
			switch (zoomToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem))
			{
				case 0: // 1/8x
					ZoomLevel = 0.125;
					break;
				case 1: // 1/4x
					ZoomLevel = 0.25;
					break;
				case 2: // 1/2x
					ZoomLevel = 0.5;
					break;
				default:
					ZoomLevel = zoomToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem) - 2;
					break;
			}
			if (!loaded) return;
			loaded = false;
			UpdateScrollBars();
			loaded = true;
			if (scrollPreviewButton.Checked)
				backgroundPanel.PanelGraphics.Clear(LevelImgPalette.Entries[LevelData.ColorTransparent]);
			else
				DrawLevel();
		}

		private void logToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LogWindow = new LogWindow();
			LogWindow.Show(this);
			logToolStripMenuItem.Enabled = false;
		}
		#endregion

		#region Export Menu
		private void paletteToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (paletteToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem) < 3)
			{
				exportToolStripMenuItem.DropDown.Hide();

				int[] data =
				{
					0, 6,
					6, 10,
					0, 16
				};

				int start = data[paletteToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem) * 2];
				int rows = data[paletteToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem) * 2 + 1];

				using (SaveFileDialog a = new SaveFileDialog() { DefaultExt = "png", Filter = "PNG Files|*.png|Palette Files|*.act|JASC-PAL Files|*.pal;*.PspPalette", RestoreDirectory = true })
					if (a.ShowDialog(this) == DialogResult.OK)
					{
						switch (Path.GetExtension(a.FileName).ToLower())
						{
							default:
							case ".png":
								BitmapBits bmp = new BitmapBits(16 * 8, rows * 8);
								for (int y = start; y < (start + rows); y++)
									for (int x = 0; x < 16; x++)
										bmp.FillRectangle((byte)((y * 16) + x), x * 8, (y - start) * 8, 8, 8);

								Color[] palette = (Color[])LevelData.NewPalette.Clone();
								if (start > 0)
									palette.Fill(LevelData.NewPalette[0], 0, start * 16);
								if ((start + rows) < 16)
									palette.Fill(LevelData.NewPalette[0], (start + rows) * 16, palette.Length - ((start + rows) * 16));
								bmp.ToBitmap(palette).Save(a.FileName);
								break;

							case ".act":
								using (FileStream str = File.Create(a.FileName))
								using (BinaryWriter bw = new BinaryWriter(str))
									for (int i = (start * 16); i < (start + rows) * 16; i++)
									{
										bw.Write(LevelData.NewPalette[i].R);
										bw.Write(LevelData.NewPalette[i].G);
										bw.Write(LevelData.NewPalette[i].B);
									}
								break;

							case ".pal":
							case ".psppalette":
								using (StreamWriter writer = File.CreateText(a.FileName))
								{
									writer.WriteLine("JASC-PAL");
									writer.WriteLine("0100");
									writer.WriteLine(rows * 16);
									for (int i = (start * 16); i < (start + rows) * 16; i++)
										writer.WriteLine("{0} {1} {2}", LevelData.NewPalette[i].R, LevelData.NewPalette[i].G, LevelData.NewPalette[i].B);
									writer.Close();
								}
								break;
						}
					}
			}
		}

		private void tilesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			exportToolStripMenuItem.DropDown.Hide();
			using (FolderBrowserDialog a = new FolderBrowserDialog() { SelectedPath = Environment.CurrentDirectory })
				if (a.ShowDialog() == DialogResult.OK)
					for (int i = 0; i < LevelData.NewTileBmps.Length; i++)
						LevelData.NewTileBmps[i]
							.Save(Path.Combine(a.SelectedPath,
							(useHexadecimalIndexesToolStripMenuItem.Checked ? i.ToString("X3") : i.ToString()) + ".png"));
		}

		private void chunksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!highToolStripMenuItem.Checked && !lowToolStripMenuItem.Checked && !path1ToolStripMenuItem.Checked && !path2ToolStripMenuItem.Checked)
			{
				MessageBox.Show(this, "Cannot export chunks with nothing visible.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			using (FolderBrowserDialog a = new FolderBrowserDialog() { SelectedPath = Environment.CurrentDirectory })
				if (a.ShowDialog() == DialogResult.OK)
				{
					ColorPalette pal;
					using (Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
						pal = bmp.Palette;
					LevelImgPalette.Entries.CopyTo(pal.Entries, 0);
					if (transparentBackgroundToolStripMenuItem.Checked)
						pal.Entries[0] = Color.Transparent;
					for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
					{
						string pathBase = Path.Combine(a.SelectedPath, useHexadecimalIndexesToolStripMenuItem.Checked ? i.ToString("X3") : i.ToString());
						if (exportArtcollisionpriorityToolStripMenuItem.Checked)
						{
							BitmapBits bits = new BitmapBits(128, 128);
							bits.DrawSprite(LevelData.ChunkSprites[i]);
							bits.ToBitmap(pal).Save(pathBase + ".png");
							LevelData.ChunkColBmpBits[i][0].ToBitmap4bpp(Color.Magenta, Color.White, Color.Yellow, Color.Black, Color.Red).Save(pathBase + "_col1.png");
							LevelData.ChunkColBmpBits[i][1].ToBitmap4bpp(Color.Magenta, Color.White, Color.Yellow, Color.Black, Color.Red).Save(pathBase + "_col2.png");
							BitmapBits pri = new BitmapBits(128, 128);
							BitmapBits fa1 = new BitmapBits(128, 128);
							BitmapBits fa2 = new BitmapBits(128, 128);
							BitmapBits la1 = new BitmapBits(128, 128);
							BitmapBits la2 = new BitmapBits(128, 128);
							BitmapBits ra1 = new BitmapBits(128, 128);
							BitmapBits ra2 = new BitmapBits(128, 128);
							BitmapBits ca1 = new BitmapBits(128, 128);
							BitmapBits ca2 = new BitmapBits(128, 128);
							BitmapBits f1 = new BitmapBits(128, 128);
							BitmapBits f2 = new BitmapBits(128, 128);
							for (int cy = 0; cy < 128 / 16; cy++)
								for (int cx = 0; cx < 128 / 16; cx++)
								{
									if (LevelData.NewChunks.chunkList[i].tiles[cy][cx].visualPlane == RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High)
										pri.FillRectangle(1, cx * 16, cy * 16, 16, 16);
									if (LevelData.NewChunks.chunkList[i].tiles[cy][cx].tileIndex < LevelData.NewTiles.Length)
									{
										RSDKv3_4.TileConfig.CollisionMask cm1 = LevelData.Collision.collisionMasks[0][LevelData.NewChunks.chunkList[i].tiles[cy][cx].tileIndex];
										RSDKv3_4.TileConfig.CollisionMask cm2 = LevelData.Collision.collisionMasks[1][LevelData.NewChunks.chunkList[i].tiles[cy][cx].tileIndex];
										fa1.FillRectangle(cm1.floorAngle, cx * 16, cy * 16, 16, 16);
										fa2.FillRectangle(cm2.floorAngle, cx * 16, cy * 16, 16, 16);
										la1.FillRectangle(cm1.lWallAngle, cx * 16, cy * 16, 16, 16);
										la2.FillRectangle(cm2.lWallAngle, cx * 16, cy * 16, 16, 16);
										ra1.FillRectangle(cm1.rWallAngle, cx * 16, cy * 16, 16, 16);
										ra2.FillRectangle(cm2.rWallAngle, cx * 16, cy * 16, 16, 16);
										ca1.FillRectangle(cm1.roofAngle, cx * 16, cy * 16, 16, 16);
										ca2.FillRectangle(cm2.roofAngle, cx * 16, cy * 16, 16, 16);
										f1.FillRectangle(cm1.flags, cx * 16, cy * 16, 16, 16);
										f2.FillRectangle(cm2.flags, cx * 16, cy * 16, 16, 16);
									}
								}
							pri.ToBitmap1bpp(Color.Black, Color.White).Save(pathBase + "_pri.png");
							fa1.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_fa1.png");
							fa2.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_fa2.png");
							la1.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_la1.png");
							la2.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_la2.png");
							ra1.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_ra1.png");
							ra2.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_ra2.png");
							ca1.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_ca1.png");
							ca2.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_ca2.png");
							f1.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_f1.png");
							f2.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_f2.png");
						}
						else
						{
							if (path1ToolStripMenuItem.Checked || path2ToolStripMenuItem.Checked)
							{
								BitmapBits32 bits = new BitmapBits32(128, 128);
								pal.Entries.CopyTo(bits.Palette, 0);
								bits.Clear(pal.Entries[0]);
								if (highToolStripMenuItem.Checked & lowToolStripMenuItem.Checked)
									bits.DrawSprite(LevelData.ChunkSprites[i]);
								else if (lowToolStripMenuItem.Checked)
									bits.DrawSpriteLow(LevelData.ChunkSprites[i]);
								else if (highToolStripMenuItem.Checked)
									bits.DrawSpriteHigh(LevelData.ChunkSprites[i]);

								bits.Palette[LevelData.ColorWhite] = Color.White;
								bits.Palette[LevelData.ColorYellow] = Color.Yellow;
								bits.Palette[LevelData.ColorBlack] = Color.Black;
								bits.Palette[LevelData.ColorRed] = Color.Red;

								if (path1ToolStripMenuItem.Checked)
									bits.DrawBitmap(LevelData.ChunkColBmpBits[i][0], 0, 0);
								else if (path2ToolStripMenuItem.Checked)
									bits.DrawBitmap(LevelData.ChunkColBmpBits[i][1], 0, 0);
								bits.ToBitmap().Save(pathBase + ".png");
							}
							else
							{
								BitmapBits bits = new BitmapBits(128, 128);
								if (highToolStripMenuItem.Checked & lowToolStripMenuItem.Checked)
									bits.DrawSprite(LevelData.ChunkSprites[i]);
								else if (lowToolStripMenuItem.Checked)
									bits.DrawSpriteLow(LevelData.ChunkSprites[i]);
								else if (highToolStripMenuItem.Checked)
									bits.DrawSpriteHigh(LevelData.ChunkSprites[i]);
								bits.ToBitmap(pal).Save(pathBase + ".png");
							}
						}
					}
				}
		}

		private void solidityMapsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog a = new FolderBrowserDialog() { SelectedPath = Environment.CurrentDirectory })
				if (a.ShowDialog() == DialogResult.OK)
					for (int i = 0; i < LevelData.NewColBmpBits.Length; i++)
					{
						LevelData.NewColBmpBits[i][0].ToBitmap1bpp(Color.Transparent, Color.White).Save(Path.Combine(a.SelectedPath,
							"0_" + (useHexadecimalIndexesToolStripMenuItem.Checked ? i.ToString("X3") : i.ToString()) + ".png"));
						LevelData.NewColBmpBits[i][1].ToBitmap1bpp(Color.Transparent, Color.White).Save(Path.Combine(a.SelectedPath,
							"1_" + (useHexadecimalIndexesToolStripMenuItem.Checked ? i.ToString("X3") : i.ToString()) + ".png"));
					}
		}

		private void foregroundToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog a = new SaveFileDialog()
			{
				DefaultExt = "png",
				Filter = "PNG Files|*.png",
				RestoreDirectory = true
			})
				if (a.ShowDialog() == DialogResult.OK)
				{
					if (exportArtcollisionpriorityToolStripMenuItem.Checked)
					{
						string pathBase = Path.Combine(Path.GetDirectoryName(a.FileName), Path.GetFileNameWithoutExtension(a.FileName));
						string pathExt = Path.GetExtension(a.FileName);
						BitmapBits bmp = LevelData.DrawForegroundLayout(null);
						Bitmap res = bmp.ToBitmap();
						ColorPalette pal = res.Palette;
						LevelImgPalette.Entries.CopyTo(pal.Entries, 0);
						if (transparentBackgroundToolStripMenuItem.Checked)
							pal.Entries[0] = Color.Transparent;
						res.Palette = pal;
						res.Save(a.FileName);
						LevelData.DrawForegroundCollision(null, 0).ToBitmap4bpp(Color.Magenta, Color.White, Color.Yellow, Color.Black, Color.Red).Save(pathBase + "_col1" + pathExt);
						LevelData.DrawForegroundCollision(null, 1).ToBitmap4bpp(Color.Magenta, Color.White, Color.Yellow, Color.Black, Color.Red).Save(pathBase + "_col2" + pathExt);
						BitmapBits pri = new BitmapBits(bmp.Width, bmp.Height);
						BitmapBits fa1 = new BitmapBits(bmp.Width, bmp.Height);
						BitmapBits fa2 = new BitmapBits(bmp.Width, bmp.Height);
						BitmapBits la1 = new BitmapBits(bmp.Width, bmp.Height);
						BitmapBits la2 = new BitmapBits(bmp.Width, bmp.Height);
						BitmapBits ra1 = new BitmapBits(bmp.Width, bmp.Height);
						BitmapBits ra2 = new BitmapBits(bmp.Width, bmp.Height);
						BitmapBits ca1 = new BitmapBits(bmp.Width, bmp.Height);
						BitmapBits ca2 = new BitmapBits(bmp.Width, bmp.Height);
						BitmapBits f1 = new BitmapBits(bmp.Width, bmp.Height);
						BitmapBits f2 = new BitmapBits(bmp.Width, bmp.Height);
						for (int ly = 0; ly < LevelData.FGHeight; ly++)
							for (int lx = 0; lx < LevelData.FGWidth; lx++)
							{
								if (LevelData.Scene.layout[ly][lx] >= LevelData.NewChunks.chunkList.Length) continue;
								RSDKv3_4.Tiles128x128.Block cnk = LevelData.NewChunks.chunkList[LevelData.Scene.layout[ly][lx]];
								for (int cy = 0; cy < 8; cy++)
									for (int cx = 0; cx < 8; cx++)
									{
										if (cnk.tiles[cy][cx].visualPlane == RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High)
											pri.FillRectangle(1, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
										if (cnk.tiles[cy][cx].tileIndex < LevelData.NewTiles.Length)
										{
											RSDKv3_4.TileConfig.CollisionMask cm1 = LevelData.Collision.collisionMasks[0][cnk.tiles[cy][cx].tileIndex];
											RSDKv3_4.TileConfig.CollisionMask cm2 = LevelData.Collision.collisionMasks[1][cnk.tiles[cy][cx].tileIndex];
											fa1.FillRectangle(cm1.floorAngle, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
											fa2.FillRectangle(cm2.floorAngle, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
											la1.FillRectangle(cm1.lWallAngle, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
											la2.FillRectangle(cm2.lWallAngle, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
											ra1.FillRectangle(cm1.rWallAngle, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
											ra2.FillRectangle(cm2.rWallAngle, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
											ca1.FillRectangle(cm1.roofAngle, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
											ca2.FillRectangle(cm2.roofAngle, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
											f1.FillRectangle(cm1.flags, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
											f2.FillRectangle(cm2.flags, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
										}
									}
							}
						pri.ToBitmap1bpp(Color.Black, Color.White).Save(pathBase + "_pri" + pathExt);
						fa1.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_fa1" + pathExt);
						fa2.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_fa2" + pathExt);
						la1.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_la1" + pathExt);
						la2.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_la2" + pathExt);
						ra1.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_ra1" + pathExt);
						ra2.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_ra2" + pathExt);
						ca1.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_ca1" + pathExt);
						ca2.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_ca2" + pathExt);
						f1.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_f1" + pathExt);
						f2.ToBitmap(LevelData.GrayscalePalette).Save(pathBase + "_f2" + pathExt);
					}
					else
					{
						if (path1ToolStripMenuItem.Checked || path2ToolStripMenuItem.Checked)
						{
							BitmapBits32 bmp = LevelData.DrawForeground32(null, transparentBackgroundToolStripMenuItem.Checked ? Color.Transparent : LevelImgPalette.Entries[LevelData.ColorTransparent], includeobjectsWithFGToolStripMenuItem.Checked, !hideDebugObjectsToolStripMenuItem.Checked, objectsAboveHighPlaneToolStripMenuItem.Checked, lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked, path1ToolStripMenuItem.Checked, path2ToolStripMenuItem.Checked);
							using (Bitmap res = bmp.ToBitmap())
								res.Save(a.FileName);
						}
						else
						{
							BitmapBits bmp = LevelData.DrawForeground(null, includeobjectsWithFGToolStripMenuItem.Checked, !hideDebugObjectsToolStripMenuItem.Checked, objectsAboveHighPlaneToolStripMenuItem.Checked, lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked, path1ToolStripMenuItem.Checked, path2ToolStripMenuItem.Checked);
							Color[] palette;
							if (transparentBackgroundToolStripMenuItem.Checked)
							{
								palette = (Color[])LevelImgPalette.Entries.Clone();
								palette[0] = Color.Transparent;
							}
							else
								palette = LevelImgPalette.Entries;
							using (Bitmap res = bmp.ToBitmap(palette))
								res.Save(a.FileName);
						}
					}
				}
		}

		private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog a = new SaveFileDialog()
			{
				DefaultExt = "png",
				Filter = "PNG Files|*.png",
				RestoreDirectory = true
			})
				if (a.ShowDialog() == DialogResult.OK)
				{
					if (exportArtcollisionpriorityToolStripMenuItem.Checked)
					{
						string pathBase = Path.Combine(Path.GetDirectoryName(a.FileName), Path.GetFileNameWithoutExtension(a.FileName));
						string pathExt = Path.GetExtension(a.FileName);
						BitmapBits bmp = LevelData.DrawBackground(bglayer, null, true, true);
						Bitmap res = bmp.ToBitmap();
						ColorPalette pal = res.Palette;
						LevelImgPalette.Entries.CopyTo(pal.Entries, 0);
						if (transparentBackgroundToolStripMenuItem.Checked)
							pal.Entries[0] = Color.Transparent;
						res.Palette = pal;
						res.Save(a.FileName);
						bmp.Clear();
						for (int ly = 0; ly < LevelData.BGHeight[bglayer]; ly++)
							for (int lx = 0; lx < LevelData.BGWidth[bglayer]; lx++)
							{
								if (LevelData.Background.layers[bglayer].layout[ly][lx] >= LevelData.NewChunks.chunkList.Length) continue;
								RSDKv3_4.Tiles128x128.Block cnk = LevelData.NewChunks.chunkList[LevelData.Background.layers[bglayer].layout[ly][lx]];
								for (int cy = 0; cy < 8; cy++)
									for (int cx = 0; cx < 8; cx++)
									{
										if (cnk.tiles[cy][cx].visualPlane == RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High)
											bmp.FillRectangle(1, lx * 128 + cx * 16, ly * 128 + cy * 16, 16, 16);
									}
							}
						bmp.ToBitmap1bpp(Color.Black, Color.White).Save(pathBase + "_pri" + pathExt);
					}
					else
					{
						if (path1ToolStripMenuItem.Checked || path2ToolStripMenuItem.Checked)
						{
							BitmapBits32 bmp = LevelData.DrawBackground32(bglayer, null, transparentBackgroundToolStripMenuItem.Checked ? Color.Transparent : LevelImgPalette.Entries[LevelData.ColorTransparent], lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
							using (Bitmap res = bmp.ToBitmap())
								res.Save(a.FileName);
						}
						else
						{
							BitmapBits bmp = LevelData.DrawBackground(bglayer, null, lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
							Color[] palette;
							if (transparentBackgroundToolStripMenuItem.Checked)
							{
								palette = (Color[])LevelImgPalette.Entries.Clone();
								palette[0] = Color.Transparent;
							}
							else
								palette = LevelImgPalette.Entries;
							using (Bitmap res = bmp.ToBitmap(palette))
								res.Save(a.FileName);
						}
					}
				}
		}

		private void transparentBackgroundToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			Settings.TransparentBackgroundExport = transparentBackgroundToolStripMenuItem.Checked;
		}

		private void hideDebugObjectsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			Settings.HideDebugObjectsExport = hideDebugObjectsToolStripMenuItem.Checked;
		}

		private void useHexadecimalIndexesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			Settings.UseHexadecimalIndexesExport = useHexadecimalIndexesToolStripMenuItem.Checked;
		}

		private void exportArtcollisionpriorityToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			Settings.ExportArtCollisionPriority = exportArtcollisionpriorityToolStripMenuItem.Checked;
		}
#endregion

#region Help Menu
		private void viewReadmeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "readme.txt"));
		}

		private void reportBugToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (BugReportDialog err = new BugReportDialog("SonLVL-RSDK", string.Join(Environment.NewLine, LogFile.ToArray())))
				err.ShowDialog();
		}
#endregion
#endregion

		void ObjectSelect_listView2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			if (ObjectSelect.listView1.SelectedIndices.Count == 0) return;
			if (ObjectSelect.listView2.SelectedIndices.Count == 0) return;
			ObjectSelect.numericUpDown2.Value = (byte)ObjectSelect.listView2.SelectedItems[0].Tag;
		}

		void ObjectSelect_listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			if (ObjectSelect.listView1.SelectedIndices.Count == 0) return;
			byte ID = (byte)ObjectSelect.listView1.SelectedItems[0].Tag;
			ObjectSelect.numericUpDown1.Value = ID;
			ObjectSelect.numericUpDown2.Value = LevelData.ObjTypes[ID].DefaultSubtype;
			ObjectSelect.listView2.Items.Clear();
			ObjectSelect.imageList2.Images.Clear();
			foreach (byte item in LevelData.ObjTypes[ID].Subtypes)
			{
				ObjectSelect.imageList2.Images.Add(LevelData.ObjTypes[ID].SubtypeImage(item).GetBitmap().ToBitmap(LevelData.BmpPal).Resize(ObjectSelect.imageList2.ImageSize));
				ObjectSelect.listView2.Items.Add(new ListViewItem(LevelData.ObjTypes[ID].SubtypeName(item), ObjectSelect.imageList2.Images.Count - 1) { Tag = item, Selected = item == LevelData.ObjTypes[ID].DefaultSubtype });
			}
		}

		void ObjectProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			foreach (Entry item in SelectedItems)
			{
				if (item is ObjectEntry oe && e.ChangedItem.PropertyDescriptor.Name == "Type")
				{
					var lvi = objectOrder.Items[LevelData.Objects.IndexOf(oe)];
					lvi.Text = item.Name;
					lvi.ImageIndex = oe.Type < objectTypeImages.Images.Count ? oe.Type : 0;
				}
				item.UpdateSprite();
			}
			DrawLevel();
			if (e.ChangedItem.PropertyDescriptor.Name == "Type")
				ObjectProperties.Refresh();
			SaveState($"Change Objects {e.ChangedItem.Label}");
		}

		void bgLayerDropDown_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			foreach (var item in bgLayerDropDown.DropDownItems.OfType<ToolStripMenuItem>())
				item.Checked = item == e.ClickedItem;
			bglayer = bgLayerDropDown.DropDownItems.IndexOf(e.ClickedItem);
			bgLayerDropDown.Text = $"Layer: {bglayer + 1}";
			BGSelection = Rectangle.Empty;
			UpdateScrollControls();
			UpdateScrollBars();
			DrawLevel();
		}

		BitmapBits32 LevelImg8bpp;
		static readonly SolidBrush objectBrush = new SolidBrush(Color.FromArgb(128, Color.Cyan));
		static readonly Pen selectionPen = new Pen(Color.FromArgb(128, Color.Black)) { DashStyle = DashStyle.Dot };
		static readonly SolidBrush selectionBrush = new SolidBrush(Color.FromArgb(128, Color.White));
		internal void DrawLevel()
		{
			if (!loaded) return;
			if (CurrentTab == Tab.Background && scrollPreviewButton.Checked) return;
			ScrollingPanel panel;
			Rectangle selection;
			switch (CurrentTab)
			{
				case Tab.Objects:
					panel = objectPanel;
					selection = Rectangle.Empty;
					break;
				case Tab.Foreground:
					panel = foregroundPanel;
					selection = FGSelection;
					break;
				case Tab.Background:
					panel = backgroundPanel;
					selection = BGSelection;
					break;
				default:
					return;
			}
			Point camera = new Point(panel.HScrollValue, panel.VScrollValue);
			Size lvlsize = Size.Empty;
			ushort[][] layout = null;
			Rectangle dispRect = new Rectangle(camera.X, camera.Y, (int)(panel.PanelWidth / ZoomLevel), (int)(panel.PanelHeight / ZoomLevel));
			switch (CurrentTab)
			{
				case Tab.Objects:
				case Tab.Foreground:
					lvlsize = LevelData.FGSize;
					layout = LevelData.Scene.layout;
					LevelImg8bpp = LevelData.DrawForeground32(dispRect, LevelImgPalette.Entries[LevelData.ColorTransparent], CurrentTab == Tab.Objects || includeobjectsWithFGToolStripMenuItem.Checked, true, objectsAboveHighPlaneToolStripMenuItem.Checked, lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked, path1ToolStripMenuItem.Checked, path2ToolStripMenuItem.Checked);
					break;
				case Tab.Background:
					lvlsize = LevelData.BGSize[bglayer];
					layout = LevelData.Background.layers[bglayer].layout;
					if (tabControl3.SelectedIndex == 2 && (scrollCamX.Value != 0 || scrollCamY.Value != 0 || scrollFrame.Value != 0) && LevelData.BGWidth[bglayer] != 0 && LevelData.BGHeight[bglayer] != 0)
					{
						decimal layerscrollpos = LevelData.Background.layers[bglayer].scrollSpeed / 64m * scrollFrame.Value;
						int widthpx = LevelData.BGWidth[bglayer] * 128;
						int heightpx = LevelData.BGHeight[bglayer] * 128;
						BitmapBits32 tmpimg;
						switch (LevelData.Background.layers[bglayer].type)
						{
							case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
								int yoff = (int)(scrollCamY.Value * (LevelData.Background.layers[bglayer].parallaxFactor / 256m) + layerscrollpos + camera.Y) % heightpx;
								if (yoff < 0)
									yoff += heightpx;
								Rectangle rect = new Rectangle(0, yoff, widthpx, Math.Min(dispRect.Height, heightpx));
								if (rect.Bottom <= heightpx)
								{
									tmpimg = LevelData.DrawBackground32(bglayer, rect, LevelImgPalette.Entries[LevelData.ColorTransparent], lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
								}
								else
								{
									tmpimg = LevelData.DrawBackground32(bglayer, new Rectangle(0, 0, widthpx, heightpx), LevelImgPalette.Entries[LevelData.ColorTransparent], lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
									tmpimg.ScrollVertical(yoff);
									tmpimg = tmpimg.GetSection(0, 0, tmpimg.Width, rect.Height);
								}
								LevelImg8bpp = new BitmapBits32(Math.Min(dispRect.Width, widthpx), rect.Height);
								int[] linepos = new int[rect.Height];
								int scrind = LevelData.BGScroll[bglayer].FindLastIndex(a => a.StartPos <= yoff);
								int dstind = 0;
								while (dstind < rect.Height)
								{
									int pos = (int)(scrollCamX.Value * LevelData.BGScroll[bglayer][scrind].ParallaxFactor + LevelData.BGScroll[bglayer][scrind].ScrollSpeed * scrollFrame.Value + camera.X);
									int len;
									if (scrind == LevelData.BGScroll[bglayer].Count - 1)
									{
										len = heightpx - yoff;
										scrind = 0;
										yoff = 0;
									}
									else
									{
										len = LevelData.BGScroll[bglayer][scrind + 1].StartPos - LevelData.BGScroll[bglayer][scrind].StartPos - (yoff - LevelData.BGScroll[bglayer][scrind].StartPos);
										++scrind;
										yoff += len;
									}
									len = Math.Min(rect.Height - dstind, len);
									linepos.FastFill(pos, dstind, len);
									dstind += len;
								}
								tmpimg.ScrollHV(LevelImg8bpp, 0, 0, linepos);
								break;
							case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
								int xoff = (int)(scrollCamX.Value * (LevelData.Background.layers[bglayer].parallaxFactor / 256m) + layerscrollpos + camera.X) % widthpx;
								if (xoff < 0)
									xoff += widthpx;
								rect = new Rectangle(xoff, 0, Math.Min(dispRect.Width, widthpx), heightpx);
								if (rect.Right <= widthpx)
								{
									tmpimg = LevelData.DrawBackground32(bglayer, rect, LevelImgPalette.Entries[LevelData.ColorTransparent], lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
								}
								else
								{
									tmpimg = LevelData.DrawBackground32(bglayer, new Rectangle(0, 0, widthpx, rect.Height), LevelImgPalette.Entries[LevelData.ColorTransparent], lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
									tmpimg.ScrollHorizontal(xoff);
									tmpimg = tmpimg.GetSection(0, 0, rect.Width, tmpimg.Height);
								}
								LevelImg8bpp = new BitmapBits32(rect.Width, Math.Min(dispRect.Height, heightpx));
								linepos = new int[rect.Width];
								scrind = LevelData.BGScroll[bglayer].FindLastIndex(a => a.StartPos <= xoff);
								dstind = 0;
								while (dstind < rect.Width)
								{
									int pos = (int)(scrollCamY.Value * LevelData.BGScroll[bglayer][scrind].ParallaxFactor + LevelData.BGScroll[bglayer][scrind].ScrollSpeed * scrollFrame.Value + camera.Y);
									int len;
									if (scrind == LevelData.BGScroll[bglayer].Count - 1)
									{
										len = widthpx - xoff;
										scrind = 0;
										xoff = 0;
									}
									else
									{
										len = LevelData.BGScroll[bglayer][scrind + 1].StartPos - LevelData.BGScroll[bglayer][scrind].StartPos - (xoff - LevelData.BGScroll[bglayer][scrind].StartPos);
										++scrind;
										xoff += len;
									}
									len = Math.Min(rect.Width - dstind, len);
									linepos.FastFill(pos, dstind, len);
									dstind += len;
								}
								tmpimg.ScrollVH(LevelImg8bpp, 0, 0, linepos);
								break;
						}
						tmpimg = LevelImg8bpp;
						LevelImg8bpp = new BitmapBits32(dispRect.Size);
						LevelImg8bpp.Clear(LevelImgPalette.Entries[LevelData.ColorTransparent]);
						LevelImg8bpp.DrawBitmap(tmpimg, 0, 0);
					}
					else
						LevelImg8bpp = LevelData.DrawBackground32(bglayer, dispRect, LevelImgPalette.Entries[LevelData.ColorTransparent], lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
					break;
			}

			if ((enableGridToolStripMenuItem.Checked) && ((CurrentTab != Tab.Objects) || (ObjGrid > 0)))
			{
				int gs = (CurrentTab == Tab.Objects) ? 1 << ObjGrid : 128;

				int a = Math.Max(-camera.Y, 0);
				int b = Math.Min(LevelImg8bpp.Height - 1, (lvlsize.Height * 128) - camera.Y - 1);
				int c = Math.Min(camera.X + (LevelImg8bpp.Width - 1), lvlsize.Width * 128);
				for (int x = Math.Max(camera.X & ~(gs - 1), 0); x <= c; x += gs)
					LevelImg8bpp.DrawLine(Settings.GridColor, x - camera.X, a, x - camera.X, b);

				a = Math.Max(-camera.X, 0);
				b = Math.Min(LevelImg8bpp.Width - 1, (lvlsize.Width * 128) - camera.X - 1);
				c = Math.Min(camera.Y + (LevelImg8bpp.Height - 1), lvlsize.Height * 128);
				for (int y = Math.Max(camera.Y & ~(gs - 1), 0); y <= c; y += gs)
					LevelImg8bpp.DrawLine(Settings.GridColor, a, y - camera.Y, b, y - camera.Y);
			}

			LevelImg8bpp.Palette[LevelData.ColorWhite] = Color.White;
			LevelImg8bpp.Palette[LevelData.ColorYellow] = Color.Yellow;
			LevelImg8bpp.Palette[LevelData.ColorBlack] = Color.Black;
			
			if (CurrentTab != Tab.Background)
			{
				if (anglesToolStripMenuItem.Checked && !noneToolStripMenuItem1.Checked)
					for (int y = Math.Max(camera.Y / 128, 0); y <= Math.Min((camera.Y + (panel.PanelHeight - 1) / ZoomLevel) / 128, lvlsize.Height - 1); y++)
						for (int x = Math.Max(camera.X / 128, 0); x <= Math.Min((camera.X + (panel.PanelWidth - 1) / ZoomLevel) / 128, lvlsize.Width - 1); x++)
							for (int b = 0; b < 8; b++)
								for (int a = 0; a < 8; a++)
									if (layout[y][x] < LevelData.NewChunks.chunkList.Length)
									{
										RSDKv3_4.Tiles128x128.Block.Tile blk = LevelData.NewChunks.chunkList[layout[y][x]].tiles[b][a];
										if (blk.tileIndex >= LevelData.NewTiles.Length) continue;
										RSDKv3_4.Tiles128x128.Block.Tile.Solidities solid = path2ToolStripMenuItem.Checked ? blk.solidityB : blk.solidityA;
										if (solid == RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone) continue;
										RSDKv3_4.TileConfig.CollisionMask mask = LevelData.Collision.collisionMasks[path2ToolStripMenuItem.Checked ? 1 : 0][blk.tileIndex];
										byte angle = mask.flipY ? mask.roofAngle : mask.floorAngle;
										if (angle != 0xFF)
										{
											switch (blk.direction)
											{
												case RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX:
													angle = (byte)(-angle & 0xFF);
													break;
												case RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY:
													angle = (byte)((-(angle + 0x40) - 0x40) & 0xFF);
													break;
												case RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipXY:
													angle = (byte)((angle + 0x80) & 0xFF);
													break;
											}
										}
										DrawHUDNum(x * 128 + a * 16 - camera.X, y * 128 + b * 16 - camera.Y, angle.ToString("X2"));
									}
			}
			else if (tabControl3.SelectedIndex == 2 && LevelData.BGScroll[bglayer].Count > 0 && showScrollAreas.Checked)
				switch (LevelData.Background.layers[bglayer].type)
				{
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
						int scrlind = LevelData.BGScroll[bglayer].FindIndex(a => a.StartPos > camera.Y);
						if (scrlind > 0)
							scrlind--;
						else
							scrlind = 0;
						for (; scrlind < LevelData.BGScroll[bglayer].Count && LevelData.BGScroll[bglayer][scrlind].StartPos < dispRect.Bottom; scrlind++)
						{
							ScrollData scrollData = LevelData.BGScroll[bglayer][scrlind];
							int height;
							if (scrlind != LevelData.BGScroll[bglayer].Count - 1)
								height = LevelData.BGScroll[bglayer][scrlind + 1].StartPos - scrollData.StartPos;
							else
								height = dispRect.Bottom - scrollData.StartPos;
							LevelImg8bpp.DrawLine((scrollList.SelectedIndex == scrlind) ? Color.Blue : Color.Yellow, 0, scrollData.StartPos - camera.Y, dispRect.Width, scrollData.StartPos - camera.Y);
						}
						break;
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
						scrlind = LevelData.BGScroll[bglayer].FindIndex(a => a.StartPos > camera.X);
						if (scrlind > 0)
							scrlind--;
						else
							scrlind = 0;
						for (; scrlind < LevelData.BGScroll[bglayer].Count && LevelData.BGScroll[bglayer][scrlind].StartPos < dispRect.Right; scrlind++)
						{
							ScrollData scrollData = LevelData.BGScroll[bglayer][scrlind];
							int width;
							if (scrlind != LevelData.BGScroll[bglayer].Count - 1)
								width = LevelData.BGScroll[bglayer][scrlind + 1].StartPos - scrollData.StartPos;
							else
								width = dispRect.Right - scrollData.StartPos;
							LevelImg8bpp.DrawLine((scrollList.SelectedIndex == scrlind) ? Color.Blue : Color.Yellow, scrollData.StartPos - camera.X, 0, scrollData.StartPos - camera.X, dispRect.Height);
						}
						break;
				}
			if (hUDToolStripMenuItem.Checked)
			{
				Rectangle hudbnd;
				Rectangle tmpbnd = hudbnd = DrawHUDStr(8, 8, "Screen Pos: ");
				hudbnd = Rectangle.Union(hudbnd, DrawHUDNum(tmpbnd.Right, tmpbnd.Top, camera.X.ToString("D5") + ' ' + camera.Y.ToString("D5")));
				tmpbnd = DrawHUDStr(hudbnd.Left, hudbnd.Bottom, "Level Size: ");
				hudbnd = Rectangle.Union(hudbnd, tmpbnd);
				hudbnd = Rectangle.Union(hudbnd, DrawHUDNum(tmpbnd.Right, tmpbnd.Top, (lvlsize.Width * 128).ToString("D5") + ' ' + (lvlsize.Height * 128).ToString("D5")));
				switch (CurrentTab)
				{
					case Tab.Objects:
					case Tab.Foreground:
						hudbnd = Rectangle.Union(hudbnd, DrawHUDStr(hudbnd.Left, hudbnd.Bottom, $"Objects: {LevelData.Objects.Count}/{RSDKv3_4.Scene.ENTITY_LIST_SIZE}"));
						break;
				}
				switch (CurrentTab)
				{
					case Tab.Foreground:
					case Tab.Background:
						tmpbnd = DrawHUDStr(hudbnd.Left, hudbnd.Bottom, "Chunk: ");
						hudbnd = Rectangle.Union(hudbnd, tmpbnd);
						hudbnd = Rectangle.Union(hudbnd, DrawHUDNum(tmpbnd.Right, tmpbnd.Top, SelectedChunk.ToString("X3")));
						break;
				}
				if (CurrentTab != Tab.Background)
				{
					if (path1ToolStripMenuItem.Checked)
						hudbnd = Rectangle.Union(hudbnd, DrawHUDStr(hudbnd.Left, hudbnd.Bottom, "Path 1"));
					else if (path2ToolStripMenuItem.Checked)
						hudbnd = Rectangle.Union(hudbnd, DrawHUDStr(hudbnd.Left, hudbnd.Bottom, "Path 2"));
				}
			}

			Array.Copy(LevelData.NewPalette, 1, LevelImg8bpp.Palette, 1, 3);
			
			if (CurrentTab == Tab.Objects && dragdrop)
				LevelImg8bpp.DrawSprite(LevelData.GetObjectDefinition(dragobj).Image, dragpoint);
			LevelBmp = LevelImg8bpp.ToBitmap();
			LevelGfx = Graphics.FromImage(LevelBmp);
			LevelGfx.SetOptions();
			Point pnlcur = panel.PanelPointToClient(Cursor.Position);
			switch (CurrentTab)
			{
				case Tab.Objects:
					foreach (Entry item in SelectedItems)
					{
						Rectangle bnd = item.Bounds;
						bnd.Offset(-camera.X, -camera.Y);
						LevelGfx.FillRectangle(objectBrush, bnd);
						bnd.Width--; bnd.Height--;
						LevelGfx.DrawRectangle(selectionPen, bnd);
					}
					if (selecting)
					{
						Rectangle selbnds = Rectangle.FromLTRB(
						Math.Min(selpoint.X, lastmouse.X) - camera.X,
						Math.Min(selpoint.Y, lastmouse.Y) - camera.Y,
						Math.Max(selpoint.X, lastmouse.X) - camera.X,
						Math.Max(selpoint.Y, lastmouse.Y) - camera.Y);
						LevelGfx.FillRectangle(selectionBrush, selbnds);
						selbnds.Width--; selbnds.Height--;
						LevelGfx.DrawRectangle(selectionPen, selbnds);
					}
					break;
				case Tab.Foreground:
					if (tabControl2.SelectedIndex == 0)
					{
						if (!selecting && SelectedChunk < LevelData.NewChunks.chunkList.Length)
							LevelGfx.DrawImage(LevelData.CompChunkBmps[SelectedChunk],
							new Rectangle(((((int)(pnlcur.X / ZoomLevel) + camera.X) / 128) * 128) - camera.X, ((((int)(pnlcur.Y / ZoomLevel) + camera.Y) / 128) * 128) - camera.Y, 128, 128),
							0, 0, 128, 128,
							GraphicsUnit.Pixel, imageTransparency);
					}
					else
					{
						if (!selecting && layoutSectionListBox.SelectedIndex != -1 && layoutSectionListBox.SelectedIndex < layoutSectionListBox.Items.Count)
							LevelGfx.DrawImage(savedLayoutSectionImages[layoutSectionListBox.SelectedIndex],
							new Rectangle(((((int)(pnlcur.X / ZoomLevel) + camera.X) / 128) * 128) - camera.X, ((((int)(pnlcur.Y / ZoomLevel) + camera.Y) / 128) * 128) - camera.Y, savedLayoutSectionImages[layoutSectionListBox.SelectedIndex].Width, savedLayoutSectionImages[layoutSectionListBox.SelectedIndex].Height),
							0, 0, savedLayoutSectionImages[layoutSectionListBox.SelectedIndex].Width, savedLayoutSectionImages[layoutSectionListBox.SelectedIndex].Height,
							GraphicsUnit.Pixel, imageTransparency);
					}

					if (!selection.IsEmpty)
					{
						Rectangle selbnds = selection.Scale(128, 128);
						selbnds.Offset(-camera.X, -camera.Y);
						LevelGfx.FillRectangle(selectionBrush, selbnds);
						selbnds.Width--; selbnds.Height--;
						LevelGfx.DrawRectangle(selectionPen, selbnds);
					}
					break;
				case Tab.Background:
					if (tabControl3.SelectedIndex != 2)
					{
						if (tabControl3.SelectedIndex == 0)
						{
							if (!selecting && SelectedChunk < LevelData.NewChunks.chunkList.Length)
								LevelGfx.DrawImage(LevelData.CompChunkBmps[SelectedChunk],
								new Rectangle(((((int)(pnlcur.X / ZoomLevel) + camera.X) / 128) * 128) - camera.X, ((((int)(pnlcur.Y / ZoomLevel) + camera.Y) / 128) * 128) - camera.Y, 128, 128),
								0, 0, 128, 128,
								GraphicsUnit.Pixel, imageTransparency);
						}
						else
						{
							if (!selecting && layoutSectionListBox.SelectedIndex != -1 && layoutSectionListBox.SelectedIndex < layoutSectionListBox.Items.Count)
								LevelGfx.DrawImage(savedLayoutSectionImages[layoutSectionListBox.SelectedIndex],
								new Rectangle(((((int)(pnlcur.X / ZoomLevel) + camera.X) / 128) * 128) - camera.X, ((((int)(pnlcur.Y / ZoomLevel) + camera.Y) / 128) * 128) - camera.Y, savedLayoutSectionImages[layoutSectionListBox.SelectedIndex].Width, savedLayoutSectionImages[layoutSectionListBox.SelectedIndex].Height),
								0, 0, savedLayoutSectionImages[layoutSectionListBox.SelectedIndex].Width, savedLayoutSectionImages[layoutSectionListBox.SelectedIndex].Height,
								GraphicsUnit.Pixel, imageTransparency);
						}

						if (!selection.IsEmpty)
						{
							Rectangle selbnds = selection.Scale(128, 128);
							selbnds.Offset(-camera.X, -camera.Y);
							LevelGfx.FillRectangle(selectionBrush, selbnds);
							selbnds.Width--; selbnds.Height--;
							LevelGfx.DrawRectangle(selectionPen, selbnds);
						}
					}
					break;
			}
			panel.GraphicsBuffer.Graphics.DrawImage(LevelBmp, 0, 0, panel.PanelWidth, panel.PanelHeight);
			panel.GraphicsBuffer.Render(panel.PanelGraphics);
		}

		public Rectangle DrawHUDStr(int x, int y, string str)
		{
			HUDImage curimg;
			int curX = x;
			int curY = y;
			Rectangle bounds = new Rectangle() { X = x, Y = y };
			int maxX = x;
			foreach (string line in str.Split(new char[] { '\n' }, StringSplitOptions.None))
			{
				int maxY = 0;
				foreach (char ch in line)
				{
					if (HUDLetters.ContainsKey(char.ToUpperInvariant(ch)))
						curimg = HUDLetters[char.ToUpperInvariant(ch)];
					else
						curimg = HUDLetters[' '];
					LevelImg8bpp.DrawSprite(curimg.Image, curX, curY);
					curX += curimg.Width;
					maxX = Math.Max(maxX, curX);
					maxY = Math.Max(maxY, curimg.Height);
				}
				curY += maxY;
				curX = x;
			}
			bounds.Width = maxX - x;
			bounds.Height = curY - y;
			return bounds;
		}

		public Rectangle DrawHUDNum(int x, int y, string str)
		{
			HUDImage curimg;
			int curX = x;
			int curY = y;
			Rectangle bounds = new Rectangle() { X = x, Y = y };
			int maxX = x;
			foreach (string line in str.Split(new char[] { '\n' }, StringSplitOptions.None))
			{
				int maxY = 0;
				foreach (char ch in line)
				{
					if (HUDNumbers.ContainsKey(char.ToUpperInvariant(ch)))
						curimg = HUDNumbers[char.ToUpperInvariant(ch)];
					else
						curimg = HUDNumbers[' '];
					LevelImg8bpp.DrawSprite(curimg.Image, curX, curY);
					curX += curimg.Width;
					maxX = Math.Max(maxX, curX);
					maxY = Math.Max(maxY, curimg.Height);
				}
				curY += maxY;
				curX = x;
			}
			bounds.Width = maxX - x;
			bounds.Height = curY - y;
			return bounds;
		}

		private void panel_Paint(object sender, PaintEventArgs e)
		{
			DrawLevel();
		}

		private void UpdateScrollBars()
		{
			objectPanel.HScrollMaximum = (int)Math.Max(((LevelData.FGWidth + 2) * 128) - (objectPanel.PanelWidth / ZoomLevel), objectPanel.HScrollLargeChange - 1);
			objectPanel.VScrollMaximum = (int)Math.Max(((LevelData.FGHeight + 2) * 128) - (objectPanel.PanelHeight / ZoomLevel), objectPanel.VScrollLargeChange - 1);
			foregroundPanel.HScrollMaximum = (int)Math.Max(((LevelData.FGWidth + 2) * 128) - (foregroundPanel.PanelWidth / ZoomLevel), foregroundPanel.HScrollLargeChange - 1);
			foregroundPanel.VScrollMaximum = (int)Math.Max(((LevelData.FGHeight + 2) * 128) - (foregroundPanel.PanelHeight / ZoomLevel), foregroundPanel.VScrollLargeChange - 1);
			backgroundPanel.HScrollMaximum = (int)Math.Max(((LevelData.BGWidth[bglayer] + 1) * 128) - (backgroundPanel.PanelWidth / ZoomLevel), backgroundPanel.HScrollLargeChange - 1);
			backgroundPanel.VScrollMaximum = (int)Math.Max(((LevelData.BGHeight[bglayer] + 1) * 128) - (backgroundPanel.PanelHeight / ZoomLevel), backgroundPanel.VScrollLargeChange - 1);
		}

		Rectangle prevbnds;
		FormWindowState prevstate;
		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					if (e.Alt)
					{
						if (!TopMost)
						{
							prevbnds = Bounds;
							prevstate = WindowState;
							TopMost = true;
							WindowState = FormWindowState.Normal;
							FormBorderStyle = FormBorderStyle.None;
							Bounds = Screen.FromControl(this).Bounds;
						}
						else
						{
							TopMost = false;
							WindowState = prevstate;
							FormBorderStyle = FormBorderStyle.Sizable;
							Bounds = prevbnds;
						}
					}
					break;
				case Keys.D1:
				case Keys.NumPad1:
					if (e.Control)
						CurrentTab = Tab.Objects;
					break;
				case Keys.D2:
				case Keys.NumPad2:
					if (e.Control)
						CurrentTab = Tab.Foreground;
					break;
				case Keys.D3:
				case Keys.NumPad3:
					if (e.Control)
						CurrentTab = Tab.Background;
					break;
				case Keys.D4:
				case Keys.NumPad4:
					if (e.Control)
						CurrentTab = Tab.Art;
					break;
				case Keys.D5:
				case Keys.NumPad5:
					if (e.Control)
						CurrentTab = Tab.Palette;
					break;
				case Keys.D6:
				case Keys.NumPad6:
					if (e.Control)
						CurrentTab = Tab.Settings;
					break;
				case Keys.Y:
					if (e.Control && undoSystem.CanRedo)
						Redo();
					break;
				case Keys.Z:
					if (e.Control && undoSystem.CanUndo)
						Undo();
					break;
			}
		}

		private void objectPanel_KeyDown(object sender, KeyEventArgs e)
		{
			long hstep = e.Control ? int.MaxValue : e.Shift ? 128 : 16;
			long vstep = e.Control ? int.MaxValue : e.Shift ? 128 : 16;
			switch (e.KeyCode)
			{
				case Keys.Up:
					if (!loaded) return;
					objectPanel.VScrollValue = (int)Math.Max(objectPanel.VScrollValue - vstep, objectPanel.VScrollMinimum);
					break;
				case Keys.Down:
					if (!loaded) return;
					objectPanel.VScrollValue = Math.Max((int)Math.Min(objectPanel.VScrollValue + vstep, objectPanel.VScrollMaximum - objectPanel.VScrollLargeChange + 1), objectPanel.VScrollMinimum);
					break;
				case Keys.Left:
					if (!loaded) return;
					objectPanel.HScrollValue = (int)Math.Max(objectPanel.HScrollValue - hstep, objectPanel.HScrollMinimum);
					break;
				case Keys.Right:
					if (!loaded) return;
					objectPanel.HScrollValue = Math.Max((int)Math.Min(objectPanel.HScrollValue + hstep, objectPanel.HScrollMaximum - objectPanel.HScrollLargeChange + 1), objectPanel.HScrollMinimum);
					break;
				case Keys.Delete:
					if (!loaded) return;
					if (SelectedItems.Count > 0)
						deleteToolStripMenuItem_Click(sender, EventArgs.Empty);
					break;
				case Keys.A:
					if (!loaded) return;
					if (e.Control)
						selectAllObjectsToolStripMenuItem_Click(sender, EventArgs.Empty);
					else
					{
						for (int i = 0; i < SelectedItems.Count; i++)
						{
							if (SelectedItems[i] is ObjectEntry oi)
							{
								oi.Type = (byte)(oi.Type == 0 ? 255 : oi.Type - 1);
								var lvi = objectOrder.Items[LevelData.Objects.IndexOf(oi)];
								lvi.Text = oi.Name;
								lvi.ImageIndex = oi.Type < objectTypeImages.Images.Count ? oi.Type : 0;
								SelectedItems[i].UpdateSprite();
							}
						}
						DrawLevel();
						ObjectProperties.Refresh();
						SaveState("Change Objects Type");
					}
					break;
				case Keys.D:
					if (!loaded) return;
					objectOrder.BeginUpdate();
					
					SelectedItems.Sort((a, b) =>
						LevelData.Scene.entities.IndexOf(((ObjectEntry)a).Entity).CompareTo(LevelData.Scene.entities.IndexOf(((ObjectEntry)b).Entity))
					);

					foreach (ObjectEntry obj in SelectedItems)
					{
						int index = LevelData.Scene.entities.IndexOf(obj.Entity);
						if (index == 0)
							break;
						
						loaded = false;
						ListViewItem lvi = objectOrder.Items[index];
						objectOrder.Items[index] = (ListViewItem)objectOrder.Items[index - 1].Clone();
						objectOrder.Items[index - 1] = lvi;
						loaded = true;

						LevelData.Scene.entities.Swap(index, index - 1);
						LevelData.Objects.Swap(index, index - 1);
					}

					foreach (ObjectEntry obj in SelectedItems)
						obj.UpdateSprite();
					
					objectOrder.EndUpdate();
					if (objectOrder.SelectedIndices.Count > 0)
						objectOrder.EnsureVisible(objectOrder.SelectedIndices[0]);

					DrawLevel();
					ObjectProperties.Refresh();
					SaveState("Change Objects EntityPos");
					break;
				case Keys.C:
					if (!loaded) return;
					if (SelectedItems.Count > 0)
						if (e.Control)
							copyToolStripMenuItem_Click(sender, EventArgs.Empty);
						else
						{
							objectOrder.BeginUpdate();

							SelectedItems.Sort((a, b) =>
								LevelData.Scene.entities.IndexOf(((ObjectEntry)b).Entity).CompareTo(LevelData.Scene.entities.IndexOf(((ObjectEntry)a).Entity))
							);

							foreach (ObjectEntry obj in SelectedItems)
							{
								int index = LevelData.Scene.entities.IndexOf(obj.Entity);
								if (index == LevelData.Scene.entities.Count - 1)
									break;

								loaded = false;
								ListViewItem lvi = objectOrder.Items[index];
								objectOrder.Items[index] = (ListViewItem)objectOrder.Items[index + 1].Clone();
								objectOrder.Items[index + 1] = lvi;
								loaded = true;

								LevelData.Scene.entities.Swap(index, index + 1);
								LevelData.Objects.Swap(index, index + 1);
							}

							foreach (ObjectEntry obj in SelectedItems)
								obj.UpdateSprite();

							objectOrder.EndUpdate();
							if (objectOrder.SelectedIndices.Count > 0)
								objectOrder.EnsureVisible(objectOrder.SelectedIndices[0]);

							DrawLevel();
							ObjectProperties.Refresh();
							SaveState("Change Objects EntityPos");
						}
					break;
				case Keys.D0:
				case Keys.D1:
					objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[0]));
					break;
				case Keys.D2:
					objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[1]));
					break;
				case Keys.D3:
					objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[2]));
					break;
				case Keys.D4:
					objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[3]));
					break;
				case Keys.D5:
					objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[4]));
					break;
				case Keys.D6:
					objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[5]));
					break;
				case Keys.D7:
					objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[6]));
					break;
				case Keys.D8:
					objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[7]));
					break;
				case Keys.D9:
					objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[8]));
					break;
				case Keys.J:
					int gs = ObjGrid + 1;
					if (gs < objGridSizeDropDownButton.DropDownItems.Count)
						objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[gs]));
					break;
				case Keys.M:
					if (ObjGrid > 0)
						objGridSizeDropDownButton_DropDownItemClicked(this, new ToolStripItemClickedEventArgs(objGridSizeDropDownButton.DropDownItems[ObjGrid - 1]));
					break;
				case Keys.S:
					if (!loaded) return;
					if (!e.Control)
					{
						foreach (ObjectEntry item in SelectedItems.OfType<ObjectEntry>())
						{
							unchecked
							{
								--item.PropertyValue;
							}
							item.UpdateSprite();
						}
						DrawLevel();
						ObjectProperties.Refresh();
						SaveState("Change Objects PropertyValue");
					}
					break;
				case Keys.V:
					if (!loaded) return;
					if (e.Control)
					{
						menuLoc = new Point(objectPanel.PanelWidth / 2, objectPanel.PanelHeight / 2);
						pasteToolStripMenuItem_Click(sender, EventArgs.Empty);
					}
					break;
				case Keys.X:
					if (!loaded) return;
					if (e.Control)
					{
						if (SelectedItems.Count > 0)
							cutToolStripMenuItem_Click(sender, EventArgs.Empty);
					}
					else
					{
						foreach (ObjectEntry item in SelectedItems.OfType<ObjectEntry>())
						{
							++item.PropertyValue;
							item.UpdateSprite();
						}
						DrawLevel();
						ObjectProperties.Refresh();
						SaveState("Change Objects PropertyValue");
					}
					break;
				case Keys.Z:
					if (!loaded) return;
					if (!e.Control)
					{
						for (int i = 0; i < SelectedItems.Count; i++)
						{
							if (SelectedItems[i] is ObjectEntry oi)
							{
								oi.Type = (byte)(oi.Type == 255 ? 0 : oi.Type + 1);
								var lvi = objectOrder.Items[LevelData.Objects.IndexOf(oi)];
								lvi.Text = oi.Name;
								lvi.ImageIndex = oi.Type < objectTypeImages.Images.Count ? oi.Type : 0;
								SelectedItems[i].UpdateSprite();
							}
						}
						DrawLevel();
						ObjectProperties.Refresh();
						SaveState("Change Objects Type");
					}
					break;
				case Keys.NumPad1:
					if (!loaded || e.Control) return;
					gs = 1 << ObjGrid;
					foreach (Entry ent in SelectedItems)
					{
						ent.X -= (short)gs;
						ent.Y += (short)gs;
						ent.AdjustSpritePosition(-gs, gs);
					}
					DrawLevel();
					ObjectProperties.Refresh();
					SaveState("Move Objects");
					break;
				case Keys.NumPad2:
					if (!loaded || e.Control) return;
					gs = 1 << ObjGrid;
					foreach (Entry ent in SelectedItems)
					{
						ent.Y += (short)gs;
						ent.AdjustSpritePosition(0, gs);
					}
					DrawLevel();
					ObjectProperties.Refresh();
					SaveState("Move Objects");
					break;
				case Keys.NumPad3:
					if (!loaded || e.Control) return;
					gs = 1 << ObjGrid;
					foreach (Entry ent in SelectedItems)
					{
						ent.X += (short)gs;
						ent.Y += (short)gs;
						ent.AdjustSpritePosition(gs, gs);
					}
					DrawLevel();
					ObjectProperties.Refresh();
					SaveState("Move Objects");
					break;
				case Keys.NumPad4:
					if (!loaded || e.Control) return;
					gs = 1 << ObjGrid;
					foreach (Entry ent in SelectedItems)
					{
						ent.X -= (short)gs;
						ent.AdjustSpritePosition(-gs, 0);
					}
					DrawLevel();
					ObjectProperties.Refresh();
					SaveState("Move Objects");
					break;
				case Keys.NumPad6:
					if (!loaded) return;
					gs = 1 << ObjGrid;
					foreach (Entry ent in SelectedItems)
					{
						ent.X += (short)gs;
						ent.AdjustSpritePosition(gs, 0);
					}
					DrawLevel();
					ObjectProperties.Refresh();
					SaveState("Move Objects");
					break;
				case Keys.NumPad7:
					if (!loaded) return;
					gs = 1 << ObjGrid;
					foreach (Entry ent in SelectedItems)
					{
						ent.X -= (short)gs;
						ent.Y -= (short)gs;
						ent.AdjustSpritePosition(-gs, -gs);
					}
					DrawLevel();
					ObjectProperties.Refresh();
					SaveState("Move Objects");
					break;
				case Keys.NumPad8:
					if (!loaded) return;
					gs = 1 << ObjGrid;
					foreach (Entry ent in SelectedItems)
					{
						ent.Y -= (short)gs;
						ent.AdjustSpritePosition(0, -gs);
					}
					DrawLevel();
					ObjectProperties.Refresh();
					SaveState("Move Objects");
					break;
				case Keys.NumPad9:
					if (!loaded) return;
					gs = 1 << ObjGrid;
					foreach (Entry ent in SelectedItems)
					{
						ent.X += (short)gs;
						ent.Y -= (short)gs;
						ent.AdjustSpritePosition(gs, -gs);
					}
					DrawLevel();
					ObjectProperties.Refresh();
					SaveState("Move Objects");
					break;
			}
			panel_KeyDown(sender, e);
		}

		private void foregroundPanel_KeyDown(object sender, KeyEventArgs e)
		{
			long hstep = e.Control ? int.MaxValue : e.Shift ? 128 : 16;
			long vstep = e.Control ? int.MaxValue : e.Shift ? 128 : 16;
			switch (e.KeyCode)
			{
				case Keys.Up:
					if (!loaded) return;
					foregroundPanel.VScrollValue = (int)Math.Max(foregroundPanel.VScrollValue - vstep, foregroundPanel.VScrollMinimum);
					break;
				case Keys.Down:
					if (!loaded) return;
					foregroundPanel.VScrollValue = Math.Max((int)Math.Min(foregroundPanel.VScrollValue + vstep, foregroundPanel.VScrollMaximum - foregroundPanel.VScrollLargeChange + 1), foregroundPanel.VScrollMinimum);
					break;
				case Keys.Left:
					if (!loaded) return;
					foregroundPanel.HScrollValue = (int)Math.Max(foregroundPanel.HScrollValue - hstep, foregroundPanel.HScrollMinimum);
					break;
				case Keys.Right:
					if (!loaded) return;
					foregroundPanel.HScrollValue = Math.Max((int)Math.Min(foregroundPanel.HScrollValue + hstep, foregroundPanel.HScrollMaximum - foregroundPanel.HScrollLargeChange + 1), foregroundPanel.HScrollMinimum);
					break;
				case Keys.A:
					if (!loaded) return;
					SelectedChunk = (ushort)(SelectedChunk == 0 ? LevelData.NewChunks.chunkList.Length - 1 : SelectedChunk - 1);
					if (SelectedChunk < LevelData.NewChunks.chunkList.Length)
						ChunkSelector.SelectedIndex = SelectedChunk;
					DrawLevel();
					break;
				case Keys.Z:
					if (!loaded) return;
					if (!e.Control)
					{
						SelectedChunk = (ushort)(SelectedChunk == LevelData.NewChunks.chunkList.Length - 1 ? 0 : SelectedChunk + 1);
						if (SelectedChunk < LevelData.NewChunks.chunkList.Length)
							ChunkSelector.SelectedIndex = SelectedChunk;
						DrawLevel();
					}
					break;
				case Keys.S:
					if (!loaded) return;
					if (!e.Control && layoutSectionListBox.Items.Count > 0)
					{
						layoutSectionListBox.SelectedIndex = (layoutSectionListBox.SelectedIndex <= 0 ? layoutSectionListBox.Items.Count - 1 : layoutSectionListBox.SelectedIndex - 1);
						DrawLevel();
					}
					break;
				case Keys.X:
					if (!loaded) return;
					if (e.Control)
						cutToolStripMenuItem1_Click(this, EventArgs.Empty);
					else if (layoutSectionListBox.Items.Count > 0)
					{
						layoutSectionListBox.SelectedIndex = (layoutSectionListBox.SelectedIndex == layoutSectionListBox.Items.Count - 1 ? 0 : layoutSectionListBox.SelectedIndex + 1);
						DrawLevel();
					}
					break;
				case Keys.C:
					if (!loaded) return;
					if (e.Control && !FGSelection.IsEmpty)
						copyToolStripMenuItem1_Click(this, EventArgs.Empty);
					break;
				case Keys.V:
					if (!loaded) return;
					if (e.Control && !FGSelection.IsEmpty && Clipboard.ContainsData(typeof(LayoutSection).AssemblyQualifiedName))
						pasteOnceToolStripMenuItem_Click(this, EventArgs.Empty);
					break;
				case Keys.Escape:
					if (!loaded) return;
					FGSelection = Rectangle.Empty;
					DrawLevel();
					break;
			}
			panel_KeyDown(sender, e);
		}

		private void backgroundPanel_KeyDown(object sender, KeyEventArgs e)
		{
			if (scrollPreviewButton.Checked)
			{
				switch (e.KeyCode)
				{
					case Keys.Up:
						scrolloff.Y--;
						break;
					case Keys.Down:
						scrolloff.Y++;
						break;
					case Keys.Left:
						scrolloff.X--;
						break;
					case Keys.Right:
						scrolloff.X++;
						break;
				}
			}
			else
			{
				long hstep = e.Control ? int.MaxValue : e.Shift ? 128 : 16;
				long vstep = e.Control ? int.MaxValue : e.Shift ? 128 : 16;
				switch (e.KeyCode)
				{
					case Keys.Up:
						if (!loaded) return;
						backgroundPanel.VScrollValue = (int)Math.Max(backgroundPanel.VScrollValue - vstep, backgroundPanel.VScrollMinimum);
						break;
					case Keys.Down:
						if (!loaded) return;
						backgroundPanel.VScrollValue = Math.Max((int)Math.Min(backgroundPanel.VScrollValue + vstep, backgroundPanel.VScrollMaximum - backgroundPanel.VScrollLargeChange + 1), backgroundPanel.VScrollMinimum);
						break;
					case Keys.Left:
						if (!loaded) return;
						backgroundPanel.HScrollValue = (int)Math.Max(backgroundPanel.HScrollValue - hstep, backgroundPanel.HScrollMinimum);
						break;
					case Keys.Right:
						if (!loaded) return;
						backgroundPanel.HScrollValue = Math.Max((int)Math.Min(backgroundPanel.HScrollValue + hstep, backgroundPanel.HScrollMaximum - backgroundPanel.HScrollLargeChange + 1), backgroundPanel.HScrollMinimum);
						break;
					case Keys.A:
						if (!loaded) return;
						SelectedChunk = (ushort)(SelectedChunk == 0 ? LevelData.NewChunks.chunkList.Length - 1 : SelectedChunk - 1);
						if (SelectedChunk < LevelData.NewChunks.chunkList.Length)
							ChunkSelector.SelectedIndex = SelectedChunk;
						DrawLevel();
						break;
					case Keys.Z:
						if (!loaded) return;
						if (!e.Control)
						{
							SelectedChunk = (ushort)(SelectedChunk == LevelData.NewChunks.chunkList.Length - 1 ? 0 : SelectedChunk + 1);
							if (SelectedChunk < LevelData.NewChunks.chunkList.Length)
								ChunkSelector.SelectedIndex = SelectedChunk;
							DrawLevel();
						}
						break;
					case Keys.S:
						if (!loaded) return;
						if (!e.Control && layoutSectionListBox.Items.Count > 0)
						{
							layoutSectionListBox.SelectedIndex = (layoutSectionListBox.SelectedIndex <= 0 ? layoutSectionListBox.Items.Count - 1 : layoutSectionListBox.SelectedIndex - 1);
							DrawLevel();
						}
						break;
					case Keys.X:
						if (!loaded) return;
						if (e.Control)
							cutToolStripMenuItem1_Click(this, EventArgs.Empty);
						else if (layoutSectionListBox.Items.Count > 0)
						{
							layoutSectionListBox.SelectedIndex = (layoutSectionListBox.SelectedIndex == layoutSectionListBox.Items.Count - 1 ? 0 : layoutSectionListBox.SelectedIndex + 1);
							DrawLevel();
						}
						break;
					case Keys.C:
						if (!loaded) return;
						if (e.Control && !BGSelection.IsEmpty)
							copyToolStripMenuItem1_Click(this, EventArgs.Empty);
						break;
					case Keys.V:
						if (!loaded) return;
						if (e.Control && !BGSelection.IsEmpty && Clipboard.ContainsData(typeof(LayoutSection).AssemblyQualifiedName))
							pasteOnceToolStripMenuItem_Click(this, EventArgs.Empty);
						break;
					case Keys.Escape:
						if (!loaded) return;
						BGSelection = Rectangle.Empty;
						DrawLevel();
						break;
				}
			}
			panel_KeyDown(sender, e);
		}

		private void panel_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Q:
					bool angles = anglesToolStripMenuItem.Checked;
					foreach (ToolStripItem item in collisionToolStripMenuItem.DropDownItems)
						if (item is ToolStripMenuItem item1)
							item1.Checked = false;
					noneToolStripMenuItem1.Checked = true;
					anglesToolStripMenuItem.Checked = angles;
					DrawLevel();
					break;
				case Keys.W:
					angles = anglesToolStripMenuItem.Checked;
					foreach (ToolStripItem item in collisionToolStripMenuItem.DropDownItems)
						if (item is ToolStripMenuItem item1)
							item1.Checked = false;
					path1ToolStripMenuItem.Checked = true;
					anglesToolStripMenuItem.Checked = angles;
					DrawLevel();
					break;
				case Keys.E:
					angles = anglesToolStripMenuItem.Checked;
					foreach (ToolStripItem item in collisionToolStripMenuItem.DropDownItems)
						if (item is ToolStripMenuItem item1)
							item1.Checked = false;
					path2ToolStripMenuItem.Checked = true;
					anglesToolStripMenuItem.Checked = angles;
					DrawLevel();
					break;
				case Keys.R:
					if (!(e.Alt & e.Control))
					{
						anglesToolStripMenuItem.Checked = !anglesToolStripMenuItem.Checked;
						DrawLevel();
					}
					break;
				case Keys.T:
					objectsAboveHighPlaneToolStripMenuItem.Checked = !objectsAboveHighPlaneToolStripMenuItem.Checked;
					DrawLevel();
					break;
				case Keys.Y:
					if (!e.Control)
					{
						lowToolStripMenuItem.Checked = !lowToolStripMenuItem.Checked;
						DrawLevel();
					}
					break;
				case Keys.U:
					highToolStripMenuItem.Checked = !highToolStripMenuItem.Checked;
					DrawLevel();
					break;
				case Keys.I:
					enableGridToolStripMenuItem.Checked = !enableGridToolStripMenuItem.Checked;
					DrawLevel();
					break;
				case Keys.O:
					if (!e.Control)
					{
						hUDToolStripMenuItem.Checked = !hUDToolStripMenuItem.Checked;
						DrawLevel();
					}
					break;
				case Keys.P:
					snapToolStripMenuItem.Checked = !snapToolStripMenuItem.Checked;
					break;
				case Keys.OemMinus:
				case Keys.Subtract:
					for (int i = 1; i < zoomToolStripMenuItem.DropDownItems.Count; i++)
						if (((ToolStripMenuItem)zoomToolStripMenuItem.DropDownItems[i]).Checked)
						{
							zoomToolStripMenuItem_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(zoomToolStripMenuItem.DropDownItems[i - 1]));
							break;
						}
					break;
				case Keys.Oemplus:
				case Keys.Add:
					for (int i = 0; i < zoomToolStripMenuItem.DropDownItems.Count - 1; i++)
						if (((ToolStripMenuItem)zoomToolStripMenuItem.DropDownItems[i]).Checked)
						{
							zoomToolStripMenuItem_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(zoomToolStripMenuItem.DropDownItems[i + 1]));
							break;
						}
					break;
			}
		}

		private Entry GetEntryAtPoint(Point point)
		{
			foreach (ObjectEntry item in LevelData.Objects.Reverse<ObjectEntry>())
				if (item.Bounds.Contains(point))
					return item;
			return null;
		}

		private void objectPanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			double gs = snapToolStripMenuItem.Checked ? 1 << ObjGrid : 1;
			int curx = (int)(e.X / ZoomLevel) + objectPanel.HScrollValue;
			int cury = (int)(e.Y / ZoomLevel) + objectPanel.VScrollValue;
			short gridx = (short)(Math.Round(curx / gs, MidpointRounding.AwayFromZero) * gs);
			short gridy = (short)(Math.Round(cury / gs, MidpointRounding.AwayFromZero) * gs);
			switch (e.Button)
			{
				case MouseButtons.Left:
					if (e.Clicks == 2)
					{
						if (LevelData.Scene.entities.Count < RSDKv3_4.Scene.ENTITY_LIST_SIZE)
						{
							if (ObjectSelect.ShowDialog(this) == DialogResult.OK)
							{
								ObjectEntry ent = LevelData.CreateObject((byte)ObjectSelect.numericUpDown1.Value);
								objectOrder.Items.Add(ent.Name, ent.Type < objectTypeImages.Images.Count ? ent.Type : 0);
								ent.PropertyValue = (byte)ObjectSelect.numericUpDown2.Value;
								ent.X = gridx;
								ent.Y = gridy;
								ent.UpdateSprite();
								SelectedItems.Clear();
								SelectedItems.Add(ent);
								SelectedObjectChanged();
								DrawLevel();
								SaveState("Add Object");
							}
							return;
						}
					}
					Entry entry = GetEntryAtPoint(new Point(curx, cury));
					if (entry == null)
					{
						objdrag = false;
						selecting = true;
						selpoint = new Point(curx, cury);
						SelectedItems.Clear();
						SelectedObjectChanged();
					}
					else
					{
						if (ModifierKeys == Keys.Control)
						{
							if (SelectedItems.Contains(entry))
								SelectedItems.Remove(entry);
							else
								SelectedItems.Add(entry);
						}
						else if (!SelectedItems.Contains(entry))
						{
							SelectedItems.Clear();
							SelectedItems.Add(entry);
						}
						SelectedObjectChanged();
						objdrag = true;
						DrawLevel();
					}
					break;
				case MouseButtons.Right:
					menuLoc = e.Location;
					entry = GetEntryAtPoint(new Point(curx, cury));
					if (entry != null)
					{
						if (!SelectedItems.Contains(entry))
						{
							SelectedItems.Clear();
							SelectedItems.Add(entry);
						}
						SelectedObjectChanged();
						DrawLevel();
					}
					objdrag = false;
					if (SelectedItems.Count > 0)
					{
						cutToolStripMenuItem.Enabled = true;
						copyToolStripMenuItem.Enabled = true;
						deleteToolStripMenuItem.Enabled = true;
					}
					else
					{
						cutToolStripMenuItem.Enabled = false;
						copyToolStripMenuItem.Enabled = false;
						deleteToolStripMenuItem.Enabled = false;
					}
					if (LevelData.Scene.entities.Count < RSDKv3_4.Scene.ENTITY_LIST_SIZE)
					{
						addObjectToolStripMenuItem.Enabled = true;
						addGroupOfObjectsToolStripMenuItem.Enabled = true;
						pasteToolStripMenuItem.Enabled = Clipboard.ContainsData(typeof(List<Entry>).AssemblyQualifiedName);
					}
					else
					{
						addObjectToolStripMenuItem.Enabled = false;
						addGroupOfObjectsToolStripMenuItem.Enabled = false;
						pasteToolStripMenuItem.Enabled = false;
					}
					objectContextMenuStrip.Show(objectPanel, menuLoc);
					break;
			}
		}

		private void objectPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			if (e.X < 0 || e.Y < 0 || e.X > objectPanel.PanelWidth || e.Y > objectPanel.PanelHeight) return;
			Point mouse = new Point((int)(e.X / ZoomLevel) + objectPanel.HScrollValue, (int)(e.Y / ZoomLevel) + objectPanel.VScrollValue);
			bool redraw = false;
			switch (e.Button)
			{
				case MouseButtons.Left:
					if (objdrag)
					{
						int difX = mouse.X - lastmouse.X;
						int difY = mouse.Y - lastmouse.Y;
						foreach (Entry item in SelectedItems)
						{
							item.X = (short)(item.X + difX);
							item.Y = (short)(item.Y + difY);
							item.AdjustSpritePosition(difX, difY);
						}
						redraw = true;
					}
					else if (selecting)
					{
						int selobjs = SelectedItems.Count;
						SelectedItems.Clear();
						Rectangle selbnds = Rectangle.FromLTRB(
						Math.Min(selpoint.X, mouse.X),
						Math.Min(selpoint.Y, mouse.Y),
						Math.Max(selpoint.X, mouse.X),
						Math.Max(selpoint.Y, mouse.Y));
						foreach (ObjectEntry item in LevelData.Objects)
							if (item.Bounds.IntersectsWith(selbnds))
								SelectedItems.Add(item);
						if (selobjs != SelectedItems.Count) SelectedObjectChanged();
						redraw = true;
					}
					break;
			}
			objectPanel.PanelCursor = GetEntryAtPoint(mouse) == null ? Cursors.Default : Cursors.SizeAll;
			if (redraw) DrawLevel();
			lastmouse = mouse;
		}

		private void objectPanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (objdrag)
			{
				if (ObjGrid > 0 && snapToolStripMenuItem.Checked)
				{
					double gs = 1 << ObjGrid;
					foreach (Entry item in SelectedItems)
					{
						item.X = (short)(Math.Round(item.X / gs, MidpointRounding.AwayFromZero) * gs);
						item.Y = (short)(Math.Round(item.Y / gs, MidpointRounding.AwayFromZero) * gs);
						item.UpdateSprite();
					}
				}
				else
					foreach (Entry item in SelectedItems)
						item.UpdateSprite();
				ObjectProperties.SelectedObjects = SelectedItems.ToArray();
				SaveState("Move Objects");
			}
			objdrag = false;
			selecting = false;
			DrawLevel();
		}

		private void foregroundPanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			Point chunkpoint = new Point(((int)(e.X / ZoomLevel) + foregroundPanel.HScrollValue) / 128, ((int)(e.Y / ZoomLevel) + foregroundPanel.VScrollValue) / 128);
			if (chunkpoint.X < 0 || chunkpoint.Y < 0 || chunkpoint.X >= LevelData.FGWidth || chunkpoint.Y >= LevelData.FGHeight)
			{
				FGSelection = Rectangle.Empty;
				return;
			}
			switch (e.Button)
			{
				case MouseButtons.Left:
					FGSelection = Rectangle.Empty;
					if (tabControl2.SelectedIndex == 0)
						LevelData.Scene.layout[chunkpoint.Y][chunkpoint.X] = SelectedChunk;
					else if (layoutSectionListBox.SelectedIndex != -1)
					{
						menuLoc = chunkpoint;
						PasteLayoutSectionOnce(savedLayoutSections[layoutSectionListBox.SelectedIndex]);
						SaveState("Place Layout Section");
					}
					DrawLevel();
					break;
				case MouseButtons.Right:
					menuLoc = chunkpoint;
					if (!FGSelection.Contains(chunkpoint))
					{
						FGSelection = Rectangle.Empty;
						DrawLevel();
					}
					lastmouse = new Point((int)(e.X / ZoomLevel) + foregroundPanel.HScrollValue, (int)(e.Y / ZoomLevel) + foregroundPanel.VScrollValue);
					break;
			}
		}

		private void foregroundPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			if (e.X < 0 || e.Y < 0 || e.X > foregroundPanel.PanelWidth || e.Y > foregroundPanel.PanelHeight) return;
			Point mouse = new Point((int)(e.X / ZoomLevel) + foregroundPanel.HScrollValue, (int)(e.Y / ZoomLevel) + foregroundPanel.VScrollValue);
			Point chunkpoint = new Point(mouse.X / 128, mouse.Y / 128);
			if (chunkpoint.X < 0 || chunkpoint.Y < 0 || chunkpoint.X >= LevelData.FGWidth || chunkpoint.Y >= LevelData.FGHeight) return;
			switch (e.Button)
			{
				case MouseButtons.Left:
					if (tabControl2.SelectedIndex == 0 && LevelData.Scene.layout[chunkpoint.Y][chunkpoint.X] != SelectedChunk)
					{
						LevelData.Scene.layout[chunkpoint.Y][chunkpoint.X] = SelectedChunk;
						DrawLevel();
					}
					break;
				case MouseButtons.Right:
					if (!selecting)
						if (Math.Sqrt(Math.Pow(e.X - lastmouse.X, 2) + Math.Pow(e.Y - lastmouse.Y, 2)) > 5)
							selecting = true;
						else
							break;
					if (FGSelection.IsEmpty)
						FGSelection = new Rectangle(chunkpoint, new Size(1, 1));
					else
					{
						int l = Math.Min(FGSelection.Left, chunkpoint.X);
						int t = Math.Min(FGSelection.Top, chunkpoint.Y);
						int r = Math.Max(FGSelection.Right, chunkpoint.X + 1);
						int b = Math.Max(FGSelection.Bottom, chunkpoint.Y + 1);
						if (FGSelection.Width > 1 && lastchunkpoint.X == l && chunkpoint.X > lastchunkpoint.X)
							l = chunkpoint.X;
						if (FGSelection.Height > 1 && lastchunkpoint.Y == t && chunkpoint.Y > lastchunkpoint.Y)
							t = chunkpoint.Y;
						if (FGSelection.Width > 1 && lastchunkpoint.X == r - 1 && chunkpoint.X < lastchunkpoint.X)
							r = chunkpoint.X + 1;
						if (FGSelection.Height > 1 && lastchunkpoint.Y == b - 1 && chunkpoint.Y < lastchunkpoint.Y)
							b = chunkpoint.Y + 1;
						FGSelection = Rectangle.FromLTRB(l, t, r, b);
					}
					DrawLevel();
					break;
				default:
					if (chunkpoint != lastchunkpoint)
						DrawLevel();
					break;
			}
			lastchunkpoint = chunkpoint;
		}

		private void foregroundPanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			switch (e.Button)
			{
				case MouseButtons.Left:
					DrawLevel();
					if (tabControl2.SelectedIndex == 0)
						SaveState("Draw Foreground");
					break;
				case MouseButtons.Right:
					Point mouse = new Point((int)(e.X / ZoomLevel) + foregroundPanel.HScrollValue, (int)(e.Y / ZoomLevel) + foregroundPanel.VScrollValue);
					Point chunkpoint = new Point(mouse.X / 128, mouse.Y / 128);
					if (chunkpoint.X < 0 || chunkpoint.Y < 0 || chunkpoint.X >= LevelData.FGWidth || chunkpoint.Y >= LevelData.FGHeight) return;
					if (FGSelection.IsEmpty)
					{
						if (tabControl2.SelectedIndex == 0)
						{
							SelectedChunk = LevelData.Scene.layout[chunkpoint.Y][chunkpoint.X];
							if (SelectedChunk < LevelData.NewChunks.chunkList.Length)
								ChunkSelector.SelectedIndex = SelectedChunk;
							DrawLevel();
						}
					}
					else if (!selecting)
					{
						pasteOnceToolStripMenuItem.Enabled = pasteRepeatingToolStripMenuItem.Enabled = Clipboard.ContainsData(typeof(LayoutSection).AssemblyQualifiedName);
						pasteSectionOnceToolStripMenuItem.Enabled = pasteSectionRepeatingToolStripMenuItem.Enabled = layoutSectionListBox.SelectedIndex != -1;
						layoutContextMenuStrip.Show(foregroundPanel, e.Location);
					}
					selecting = false;
					break;
			}
		}

		int selectedScrollLine = -1;
		private void backgroundPanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			if (scrollPreviewButton.Checked) return;
			if (tabControl3.SelectedIndex == 2)
			{
				if (!showScrollAreas.Checked) return;
				Point mouse = new Point((int)(e.X / ZoomLevel) + backgroundPanel.HScrollValue, (int)(e.Y / ZoomLevel) + backgroundPanel.VScrollValue);
				selectedScrollLine = -1;
				switch (LevelData.Background.layers[bglayer].type)
				{
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
						int i = LevelData.BGScroll[bglayer].FindIndex(a => a.StartPos == mouse.Y);
						if (i == -1)
							i = LevelData.BGScroll[bglayer].FindIndex(a => Math.Abs(a.StartPos - mouse.Y) == 1);
						if (i > 0)
						{
							switch (e.Button)
							{
								case MouseButtons.Left:
									int max;
									if (i == LevelData.BGScroll[bglayer].Count - 1)
										max = LevelData.BGHeight[bglayer] * 128 - 1;
									else
										max = LevelData.BGScroll[bglayer][i + 1].StartPos - 1;
									if (max != LevelData.BGScroll[bglayer][i - 1].StartPos + 1)
										scrollList.SelectedIndex = selectedScrollLine = i;
									break;
								case MouseButtons.Right:
									LevelData.BGScroll[bglayer].RemoveAt(i);
									scrollList.Items.RemoveAt(i);
									DrawLevel();
									SaveState("Delete Scroll Line");
									break;
							}
						}
						else if (i == -1 && e.Button == MouseButtons.Right)
						{
							i = LevelData.BGScroll[bglayer].FindLastIndex(a => a.StartPos < mouse.Y);
							LevelData.BGScroll[bglayer].Insert(i + 1, new ScrollData((ushort)mouse.Y)
							{
								Deform = LevelData.BGScroll[bglayer][i].Deform,
								ParallaxFactor = LevelData.BGScroll[bglayer][i].ParallaxFactor,
								ScrollSpeed = LevelData.BGScroll[bglayer][i].ScrollSpeed
							});
							scrollList.Items.Insert(i + 1, mouse.Y.ToString("X4"));
							scrollList.SelectedIndex = i + 1;
							DrawLevel();
							SaveState("Insert Scroll Line");
						}
						break;
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
						i = LevelData.BGScroll[bglayer].FindIndex(a => a.StartPos == mouse.X);
						if (i == -1)
							i = LevelData.BGScroll[bglayer].FindIndex(a => Math.Abs(a.StartPos - mouse.X) == 1);
						if (i > 0)
						{
							switch (e.Button)
							{
								case MouseButtons.Left:
									int max;
									if (i == LevelData.BGScroll[bglayer].Count - 1)
										max = LevelData.BGWidth[bglayer] * 128 - 1;
									else
										max = LevelData.BGScroll[bglayer][i + 1].StartPos - 1;
									if (max != LevelData.BGScroll[bglayer][i - 1].StartPos + 1)
										scrollList.SelectedIndex = selectedScrollLine = i;
									break;
								case MouseButtons.Right:
									LevelData.BGScroll[bglayer].RemoveAt(i);
									scrollList.Items.RemoveAt(i);
									DrawLevel();
									SaveState("Delete Scroll Line");
									break;
							}
						}
						else if (i == -1 && e.Button == MouseButtons.Right)
						{
							i = LevelData.BGScroll[bglayer].FindLastIndex(a => a.StartPos < mouse.X);
							LevelData.BGScroll[bglayer].Insert(i + 1, new ScrollData((ushort)mouse.X)
							{
								Deform = LevelData.BGScroll[bglayer][i].Deform,
								ParallaxFactor = LevelData.BGScroll[bglayer][i].ParallaxFactor,
								ScrollSpeed = LevelData.BGScroll[bglayer][i].ScrollSpeed
							});
							scrollList.Items.Insert(i + 1, mouse.X.ToString("X4"));
							scrollList.SelectedIndex = i + 1;
							DrawLevel();
							SaveState("Insert Scroll Line");
						}
						break;
				}
			}
			else
			{
				Point chunkpoint = new Point(((int)(e.X / ZoomLevel) + backgroundPanel.HScrollValue) / 128, ((int)(e.Y / ZoomLevel) + backgroundPanel.VScrollValue) / 128);
				if (chunkpoint.X >= LevelData.BGWidth[bglayer] || chunkpoint.Y >= LevelData.BGHeight[bglayer])
				{
					BGSelection = Rectangle.Empty;
					return;
				}
				switch (e.Button)
				{
					case MouseButtons.Left:
						BGSelection = Rectangle.Empty;
						if (tabControl2.SelectedIndex == 0)
							LevelData.Background.layers[bglayer].layout[chunkpoint.Y][chunkpoint.X] = SelectedChunk;
						else if (layoutSectionListBox.SelectedIndex != -1)
						{
							menuLoc = chunkpoint;
							PasteLayoutSectionOnce(savedLayoutSections[layoutSectionListBox.SelectedIndex]);
							SaveState("Place Layout Section");
						}
						DrawLevel();
						break;
					case MouseButtons.Right:
						menuLoc = chunkpoint;
						if (!BGSelection.Contains(chunkpoint))
						{
							BGSelection = Rectangle.Empty;
							DrawLevel();
						}
						lastmouse = new Point((int)(e.X / ZoomLevel) + foregroundPanel.HScrollValue, (int)(e.Y / ZoomLevel) + foregroundPanel.VScrollValue);
						break;
				}
			}
		}

		private void backgroundPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			if (scrollPreviewButton.Checked) return;
			if (e.X < 0 || e.Y < 0 || e.X > backgroundPanel.PanelWidth || e.Y > backgroundPanel.PanelHeight) return;
			Point mouse = new Point((int)(e.X / ZoomLevel) + backgroundPanel.HScrollValue, (int)(e.Y / ZoomLevel) + backgroundPanel.VScrollValue);
			if (tabControl3.SelectedIndex == 2)
			{
				if (!showScrollAreas.Checked) return;
				if (e.Button == MouseButtons.Left && selectedScrollLine != -1)
				{
					switch (LevelData.Background.layers[bglayer].type)
					{
						case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
							int max;
							if (selectedScrollLine == LevelData.BGScroll[bglayer].Count - 1)
								max = LevelData.BGHeight[bglayer] * 128 - 1;
							else
								max = LevelData.BGScroll[bglayer][selectedScrollLine + 1].StartPos - 1;
							LevelData.BGScroll[bglayer][selectedScrollLine].StartPos = (ushort)Math.Max(Math.Min(mouse.Y, max), LevelData.BGScroll[bglayer][selectedScrollLine - 1].StartPos + 1);
							break;
						case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
							if (selectedScrollLine == LevelData.BGScroll[bglayer].Count - 1)
								max = LevelData.BGWidth[bglayer] * 128 - 1;
							else
								max = LevelData.BGScroll[bglayer][selectedScrollLine + 1].StartPos - 1;
							LevelData.BGScroll[bglayer][selectedScrollLine].StartPos = (ushort)Math.Max(Math.Min(mouse.X, max), LevelData.BGScroll[bglayer][selectedScrollLine - 1].StartPos + 1);
							break;
					}
					DrawLevel();
				}
				else
				{
					switch (LevelData.Background.layers[bglayer].type)
					{
						case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
							int i = LevelData.BGScroll[bglayer].FindIndex(a => a.StartPos == mouse.Y);
							if (i == -1)
								i = LevelData.BGScroll[bglayer].FindIndex(a => Math.Abs(a.StartPos - mouse.Y) == 1);
							if (i > 0)
							{
								int max;
								if (i == LevelData.BGScroll[bglayer].Count - 1)
									max = LevelData.BGHeight[bglayer] * 128 - 1;
								else
									max = LevelData.BGScroll[bglayer][i + 1].StartPos - 1;
								if (max != LevelData.BGScroll[bglayer][i - 1].StartPos + 1)
									backgroundPanel.PanelCursor = Cursors.SizeNS;
								else
									backgroundPanel.PanelCursor = Cursors.Default;
							}
							else
								backgroundPanel.PanelCursor = Cursors.Default;
							break;
						case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
							i = LevelData.BGScroll[bglayer].FindIndex(a => a.StartPos == mouse.X);
							if (i == -1)
								i = LevelData.BGScroll[bglayer].FindIndex(a => Math.Abs(a.StartPos - mouse.X) == 1);
							if (i > 0)
							{
								int max;
								if (i == LevelData.BGScroll[bglayer].Count - 1)
									max = LevelData.BGWidth[bglayer] * 128 - 1;
								else
									max = LevelData.BGScroll[bglayer][i + 1].StartPos - 1;
								if (max != LevelData.BGScroll[bglayer][i - 1].StartPos + 1)
									backgroundPanel.PanelCursor = Cursors.SizeWE;
								else
									backgroundPanel.PanelCursor = Cursors.Default;
							}
							else
								backgroundPanel.PanelCursor = Cursors.Default;
							break;
					}
				}
			}
			else
			{
				Point chunkpoint = new Point(mouse.X / 128, mouse.Y / 128);
				if (chunkpoint.X >= LevelData.BGWidth[bglayer] || chunkpoint.Y >= LevelData.BGHeight[bglayer]) return;
				switch (e.Button)
				{
					case MouseButtons.Left:
						if (tabControl3.SelectedIndex == 0 && LevelData.Background.layers[bglayer].layout[chunkpoint.Y][chunkpoint.X] != SelectedChunk)
						{
							LevelData.Background.layers[bglayer].layout[chunkpoint.Y][chunkpoint.X] = SelectedChunk;
							DrawLevel();
						}
						break;
					case MouseButtons.Right:
						if (!selecting)
							if (Math.Sqrt(Math.Pow(e.X - lastmouse.X, 2) + Math.Pow(e.Y - lastmouse.Y, 2)) > 5)
								selecting = true;
							else
								break;
						if (BGSelection.IsEmpty)
							BGSelection = new Rectangle(chunkpoint, new Size(1, 1));
						else
						{
							int l = Math.Min(BGSelection.Left, chunkpoint.X);
							int t = Math.Min(BGSelection.Top, chunkpoint.Y);
							int r = Math.Max(BGSelection.Right, chunkpoint.X + 1);
							int b = Math.Max(BGSelection.Bottom, chunkpoint.Y + 1);
							if (BGSelection.Width > 1 && lastchunkpoint.X == l && chunkpoint.X > lastchunkpoint.X)
								l = chunkpoint.X;
							if (BGSelection.Height > 1 && lastchunkpoint.Y == t && chunkpoint.Y > lastchunkpoint.Y)
								t = chunkpoint.Y;
							if (BGSelection.Width > 1 && lastchunkpoint.X == r - 1 && chunkpoint.X < lastchunkpoint.X)
								r = chunkpoint.X + 1;
							if (BGSelection.Height > 1 && lastchunkpoint.Y == b - 1 && chunkpoint.Y < lastchunkpoint.Y)
								b = chunkpoint.Y + 1;
							BGSelection = Rectangle.FromLTRB(l, t, r, b);
						}
						DrawLevel();
						break;
					default:
						if (chunkpoint != lastchunkpoint)
							DrawLevel();
						break;
				}
				lastchunkpoint = chunkpoint;
			}
		}

		private void backgroundPanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (!loaded || scrollPreviewButton.Checked) return;
			if (tabControl3.SelectedIndex == 2)
			{
				if (selectedScrollLine != -1)
				{
					scrollList.Items[selectedScrollLine] = LevelData.BGScroll[bglayer][selectedScrollLine].StartPos.ToString("X4");
					scrollOffset.Value = LevelData.BGScroll[bglayer][selectedScrollLine].StartPos;
					SaveState("Move Scroll Line");
				}
			}
			else
			{
				switch (e.Button)
				{
					case MouseButtons.Left:
						DrawLevel();
						if (tabControl3.SelectedIndex == 0)
							SaveState($"Draw Background {bglayer + 1}");
						break;
					case MouseButtons.Right:
						Point mouse = new Point((int)(e.X / ZoomLevel) + backgroundPanel.HScrollValue, (int)(e.Y / ZoomLevel) + backgroundPanel.VScrollValue);
						Point chunkpoint = new Point(mouse.X / 128, mouse.Y / 128);
						if (chunkpoint.X < 0 || chunkpoint.Y < 0 || chunkpoint.X >= LevelData.BGWidth[bglayer] || chunkpoint.Y >= LevelData.BGHeight[bglayer]) return;
						if (BGSelection.IsEmpty)
						{
							if (tabControl3.SelectedIndex == 0)
							{
								SelectedChunk = LevelData.Background.layers[bglayer].layout[chunkpoint.Y][chunkpoint.X];
								if (SelectedChunk < LevelData.NewChunks.chunkList.Length)
									ChunkSelector.SelectedIndex = SelectedChunk;
								DrawLevel();
							}
						}
						else if (!selecting)
						{
							pasteOnceToolStripMenuItem.Enabled = pasteRepeatingToolStripMenuItem.Enabled = Clipboard.ContainsData(typeof(LayoutSection).AssemblyQualifiedName);
							pasteSectionOnceToolStripMenuItem.Enabled = pasteSectionRepeatingToolStripMenuItem.Enabled = layoutSectionListBox.SelectedIndex != -1;
							layoutContextMenuStrip.Show(backgroundPanel, e.Location);
						}
						selecting = false;
						break;
				}
			}
		}

		private void ChunkSelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ChunkSelector.SelectedIndex == -1 || ChunkSelector.SelectedIndex >= LevelData.NewChunks.chunkList.Length) return;
			drawChunkToolStripButton.Enabled = importChunksToolStripButton.Enabled = LevelData.HasFreeChunks();
			SelectedChunk = (ushort)ChunkSelector.SelectedIndex;
			SelectedChunkBlock = new Rectangle(0, 0, 1, 1);
			chunkBlockEditor.SelectedObjects = new[] { LevelData.NewChunks.chunkList[SelectedChunk].tiles[0][0] };
			DrawChunkPicture();
			ChunkID.Value = SelectedChunk;
			ChunkCount.Text = LevelData.NewChunks.chunkList.Length.ToString("X");
			DrawLevel();
		}

		private void SelectedObjectChanged()
		{
			ObjectProperties.SelectedObjects = SelectedItems.ToArray();
			alignLeftWallToolStripButton.Enabled = alignRightWallToolStripButton.Enabled = alignGroundToolStripButton.Enabled =
				alignCeilingToolStripButton.Enabled = SelectedItems.Count > 0;
			alignBottomsToolStripButton.Enabled = alignCentersToolStripButton.Enabled = alignLeftsToolStripButton.Enabled =
				alignMiddlesToolStripButton.Enabled = alignRightsToolStripButton.Enabled = alignTopsToolStripButton.Enabled =
				SelectedItems.Count > 1;
			if (SelectedItems.Count > 0)
			{
				objectOrder.SelectedIndices.Clear();
				objectOrder.SelectedIndices.Add(LevelData.Objects.IndexOf((ObjectEntry)SelectedItems[0]));
			}
		}

		private void ScrollBar_ValueChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			loaded = false;
			switch (CurrentTab)
			{
				case Tab.Objects:
					foregroundPanel.HScrollValue = Math.Min(objectPanel.HScrollValue, foregroundPanel.HScrollMaximum - foregroundPanel.HScrollLargeChange + 1);
					foregroundPanel.VScrollValue = Math.Min(objectPanel.VScrollValue, foregroundPanel.VScrollMaximum - foregroundPanel.VScrollLargeChange + 1);
					break;
				case Tab.Foreground:
					objectPanel.HScrollValue = Math.Min(foregroundPanel.HScrollValue, objectPanel.HScrollMaximum - objectPanel.HScrollLargeChange + 1);
					objectPanel.VScrollValue = Math.Min(foregroundPanel.VScrollValue, objectPanel.VScrollMaximum - objectPanel.VScrollLargeChange + 1);
					break;
			}
			loaded = true;
			DrawLevel();
		}

		private void panel_Resize(object sender, EventArgs e)
		{
			if (!loaded) return;
			loaded = false;
			UpdateScrollBars();
			loaded = true;
			DrawLevel();
		}

		Point menuLoc;
		private void addObjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ObjectSelect.ShowDialog(this) == DialogResult.OK)
			{
				ObjectEntry ent = LevelData.CreateObject((byte)ObjectSelect.numericUpDown1.Value);
				objectOrder.Items.Add(ent.Name, ent.Type < objectTypeImages.Images.Count ? ent.Type : 0);
				ent.PropertyValue = (byte)ObjectSelect.numericUpDown2.Value;
				double gs = snapToolStripMenuItem.Checked ? 1 << ObjGrid : 1;
				ent.X = (short)(Math.Round((menuLoc.X / ZoomLevel + objectPanel.HScrollValue) / gs, MidpointRounding.AwayFromZero) * gs);
				ent.Y = (short)(Math.Round((menuLoc.Y / ZoomLevel + objectPanel.VScrollValue) / gs, MidpointRounding.AwayFromZero) * gs);
				ent.UpdateSprite();
				SelectedItems.Clear();
				SelectedItems.Add(ent);
				SelectedObjectChanged();
				DrawLevel();
				SaveState("Add Object");
			}
		}

		private void addGroupOfObjectsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ObjectSelect.ShowDialog(this) == DialogResult.OK)
			{
				byte ID = (byte)ObjectSelect.numericUpDown1.Value;
				byte sub = (byte)ObjectSelect.numericUpDown2.Value;
				using (AddGroupDialog dlg = new AddGroupDialog())
				{
					dlg.Text = "Add Group of Objects";
					if (dlg.ShowDialog(this) == DialogResult.OK)
					{
						double gs = snapToolStripMenuItem.Checked ? 1 << ObjGrid : 1;
						Point pt = new Point(
							(ushort)(Math.Round((menuLoc.X / ZoomLevel + objectPanel.HScrollValue) / gs, MidpointRounding.AwayFromZero) * gs),
							(ushort)(Math.Round((menuLoc.Y / ZoomLevel + objectPanel.VScrollValue) / gs, MidpointRounding.AwayFromZero) * gs)
							);
						int xst = pt.X;
						Size xsz = new Size((int)dlg.XDist.Value, 0);
						Size ysz = new Size(0, (int)dlg.YDist.Value);
						SelectedItems.Clear();
						for (int y = 0; y < dlg.Rows.Value; y++)
						{
							for (int x = 0; x < dlg.Columns.Value; x++)
							{
								ObjectEntry ent = LevelData.CreateObject(ID);
								objectOrder.Items.Add(ent.Name, ent.Type < objectTypeImages.Images.Count ? ent.Type : 0);
								ent.PropertyValue = sub;
								ent.X = (short)(pt.X);
								ent.Y = (short)(pt.Y);
								ent.UpdateSprite();
								SelectedItems.Add(ent);
								pt += xsz;
							}
							pt.X = xst;
							pt += ysz;
						}
						SelectedObjectChanged();
						DrawLevel();
						SaveState("Add Objects");
					}
				}
			}
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<Entry> selitems = new List<Entry>();
			foreach (Entry item in SelectedItems)
			{
				if (item is ObjectEntry oe)
				{
					objectOrder.Items.RemoveAt(LevelData.Objects.IndexOf(oe));
					LevelData.DeleteObject(oe);
					selitems.Add(item);
				}
			}
			if (selitems.Count == 0) return;
			Clipboard.SetData(typeof(List<Entry>).AssemblyQualifiedName, selitems);
			SelectedItems.Clear();
			SelectedObjectChanged();
			DrawLevel();
			SaveState("Cut Objects");
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<Entry> selitems = new List<Entry>();
			foreach (Entry item in SelectedItems)
			{
				if (item is ObjectEntry)
					selitems.Add(item);
			}
			if (selitems.Count == 0) return;
			Clipboard.SetData(typeof(List<Entry>).AssemblyQualifiedName, selitems);
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsData(typeof(List<Entry>).AssemblyQualifiedName))
			{
				List<Entry> objs = (Clipboard.GetData(typeof(List<Entry>).AssemblyQualifiedName) as List<Entry>).Select(a => a.Clone()).ToList();
				Point upleft = new Point(int.MaxValue, int.MaxValue);
				foreach (Entry item in objs)
				{
					upleft.X = Math.Min(upleft.X, item.X);
					upleft.Y = Math.Min(upleft.Y, item.Y);
				}
				Size off = new Size(((int)(menuLoc.X / ZoomLevel) + objectPanel.HScrollValue) - upleft.X, ((int)(menuLoc.Y / ZoomLevel) + objectPanel.VScrollValue) - upleft.Y);
				SelectedItems = new List<Entry>(objs);
				double gs = snapToolStripMenuItem.Checked ? 1 << ObjGrid : 1;
				foreach (Entry item in objs)
				{
					item.X += (short)off.Width;
					item.Y += (short)off.Height;
					item.X = (short)(Math.Round(item.X / gs, MidpointRounding.AwayFromZero) * gs);
					item.Y = (short)(Math.Round(item.Y / gs, MidpointRounding.AwayFromZero) * gs);
					item.ResetPos();
					if (item is ObjectEntry oe)
					{
						LevelData.AddObject(oe);
						objectOrder.Items.Add(oe.Name, oe.Type < objectTypeImages.Images.Count ? oe.Type : 0);
					}
					item.UpdateSprite();
				}
				SelectedObjectChanged();
				DrawLevel();
				SaveState("Paste Objects");
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<Entry> selitems = new List<Entry>();
			foreach (Entry item in SelectedItems)
			{
				if (item is ObjectEntry oe)
				{
					objectOrder.Items.RemoveAt(LevelData.Objects.IndexOf(oe));
					LevelData.DeleteObject(oe);
					selitems.Add(item);
				}
			}
			if (selitems.Count == 0) return;
			SelectedItems.Clear();
			SelectedObjectChanged();
			DrawLevel();
			SaveState("Delete Objects");
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			scrollPreviewButton.Checked = false;
			selecting = false;
			switch (CurrentTab)
			{
				case Tab.Objects:
					gotoToolStripMenuItem.Enabled = findToolStripMenuItem.Enabled = true;
					findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = foundobjs == null;
					objectPanel.Focus();
					break;
				case Tab.Foreground:
					gotoToolStripMenuItem.Enabled = findToolStripMenuItem.Enabled = true;
					findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = lastfoundfgchunk.HasValue;
					tabPage8.Controls.Add(ChunkSelector);
					tabPage9.Controls.Add(layoutSectionSplitContainer);
					ChunkSelector.AllowDrop = false;
					foregroundPanel.Focus();
					break;
				case Tab.Background:
					gotoToolStripMenuItem.Enabled = findToolStripMenuItem.Enabled = true;
					findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = lastfoundbgchunk.HasValue;
					tabPage10.Controls.Add(ChunkSelector);
					tabPage11.Controls.Add(layoutSectionSplitContainer);
					ChunkSelector.AllowDrop = false;
					backgroundPanel.Focus();
					break;
				case Tab.Art:
					gotoToolStripMenuItem.Enabled = findToolStripMenuItem.Enabled = findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = false;
					panel10.Controls.Add(ChunkSelector);
					ChunkSelector.AllowDrop = true;
					break;
				case Tab.Palette:
					gotoToolStripMenuItem.Enabled = findToolStripMenuItem.Enabled = findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = false;

					if (loaded)
					{
						loaded = false;
						colorRed.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].R;
						colorGreen.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].G;
						colorBlue.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].B;
						colorHex.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].ToArgb() & 0xFFFFFF;
						loaded = true;
					}
					break;
				default:
					gotoToolStripMenuItem.Enabled = findToolStripMenuItem.Enabled = findNextToolStripMenuItem.Enabled = findPreviousToolStripMenuItem.Enabled = false;
					break;
			}
			DrawLevel();
		}

		int SelectedTile;
		Rectangle SelectedChunkBlock;
		Point SelectedColor;
		RSDKv3_4.Tiles128x128.Block.Tile copiedChunkBlock = new RSDKv3_4.Tiles128x128.Block.Tile();

		private RSDKv3_4.Tiles128x128.Block.Tile[] GetSelectedChunkBlocks()
		{
			RSDKv3_4.Tiles128x128.Block.Tile[] blocks = new RSDKv3_4.Tiles128x128.Block.Tile[SelectedChunkBlock.Width * SelectedChunkBlock.Height];
			int i = 0;
			for (int y = SelectedChunkBlock.Top; y < SelectedChunkBlock.Bottom; y++)
				for (int x = SelectedChunkBlock.Left; x < SelectedChunkBlock.Right; x++)
					blocks[i++] = LevelData.NewChunks.chunkList[SelectedChunk].tiles[y][x];
			return blocks;
		}

		private void ChunkPicture_MouseDown(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			if (e.Button == chunkblockMouseDraw)
				ChunkPicture_MouseMove(sender, e);
			else if (e.Button == chunkblockMouseSelect)
			{
				if (!SelectedChunkBlock.Contains(e.X / 16, e.Y / 16))
				{
					SelectedChunkBlock = new Rectangle(e.X / 16, e.Y / 16, 1, 1);
					copiedChunkBlock = LevelData.NewChunks.chunkList[SelectedChunk].tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X];
					if (copiedChunkBlock.tileIndex < LevelData.NewTiles.Length)
						TileSelector.SelectedIndex = copiedChunkBlock.tileIndex;
					chunkBlockEditor.SelectedObjects = new[] { copiedChunkBlock };
					DrawChunkPicture();
					selecting = true; // don't show the context menu when starting a new selection
				}
				else
					lastmouse = e.Location;
			}
		}

		private void ChunkPicture_MouseMove(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			if (e.X > 0 && e.Y > 0 && e.X < 128 && e.Y < 128)
				if (e.Button == chunkblockMouseDraw)
				{
					SelectedChunkBlock = new Rectangle(e.X / 16, e.Y / 16, 1, 1);
					RSDKv3_4.Tiles128x128.Block.Tile destBlock = LevelData.NewChunks.chunkList[SelectedChunk].tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X];
					destBlock.tileIndex = copiedChunkBlock.tileIndex;
					destBlock.solidityA = copiedChunkBlock.solidityA;
					destBlock.solidityB = copiedChunkBlock.solidityB;
					destBlock.direction = copiedChunkBlock.direction;
					destBlock.visualPlane = copiedChunkBlock.visualPlane;
					chunkBlockEditor.SelectedObjects = new[] { destBlock };
					LevelData.RedrawChunk(SelectedChunk);
					DrawChunkPicture();
					ChunkSelector.Invalidate();
				}
				else if (e.Button == chunkblockMouseSelect)
				{
					if (!selecting)
						if (Math.Sqrt(Math.Pow(e.X - lastmouse.X, 2) + Math.Pow(e.Y - lastmouse.Y, 2)) > 5)
							selecting = true;
						else
							return;
					SelectedChunkBlock = Rectangle.FromLTRB(Math.Min(SelectedChunkBlock.Left, e.X / 16), Math.Min(SelectedChunkBlock.Top, e.Y / 16), Math.Max(SelectedChunkBlock.Right, e.X / 16 + 1), Math.Max(SelectedChunkBlock.Bottom, e.Y / 16 + 1));
					copiedChunkBlock = (chunkBlockEditor.SelectedObjects = GetSelectedChunkBlocks())[0];
					DrawChunkPicture();
				}
		}

		private void ChunkPicture_MouseUp(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			if (e.Button == chunkblockMouseDraw)
			{
				LevelData.RedrawChunk(SelectedChunk);
				DrawLevel();
				DrawChunkPicture();
				ChunkSelector.Invalidate();
				SaveState("Draw Chunk Blocks");
			}
			else if (e.Button == chunkblockMouseSelect)
			{
				if (!selecting)
				{
					pasteChunkBlocksToolStripMenuItem.Enabled = Clipboard.ContainsData(typeof(RSDKv3_4.Tiles128x128.Block.Tile[,]).AssemblyQualifiedName);
					chunkBlockContextMenuStrip.Show(ChunkPicture, e.Location);
				}
				selecting = false;
			}
		}

		private void ChunkPicture_KeyDown(object sender, KeyEventArgs e)
		{
			if (!loaded) return;
			RSDKv3_4.Tiles128x128.Block.Tile[] blocks = GetSelectedChunkBlocks();
			switch (e.KeyCode)
			{
				case Keys.B:
					foreach (RSDKv3_4.Tiles128x128.Block.Tile item in blocks)
						if (e.Shift)
							item.tileIndex = (ushort)(item.tileIndex == 0 ? LevelData.NewTiles.Length - 1 : item.tileIndex - 1);
						else
							item.tileIndex = (ushort)((item.tileIndex + 1) % LevelData.NewTiles.Length);
					SaveState("Change Chunk Blocks Index");
					break;
				case Keys.Down:
					if (SelectedChunkBlock.Y < 7)
					{
						SelectedChunkBlock = new Rectangle(SelectedChunkBlock.X, SelectedChunkBlock.Y + 1, 1, 1);
						copiedChunkBlock = LevelData.NewChunks.chunkList[SelectedChunk].tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X];
						blocks = new[] { copiedChunkBlock };
						if (copiedChunkBlock.tileIndex < LevelData.NewTiles.Length)
							TileSelector.SelectedIndex = copiedChunkBlock.tileIndex;
					}
					else
						return;
					break;
				case Keys.End:
					ChunkSelector.SelectedIndex = ChunkSelector.Images.Count - 1;
					return;
				case Keys.Home:
					ChunkSelector.SelectedIndex = 0;
					break;
				case Keys.Left:
					if (SelectedChunkBlock.X > 0)
					{
						SelectedChunkBlock = new Rectangle(SelectedChunkBlock.X - 1, SelectedChunkBlock.Y, 1, 1);
						copiedChunkBlock = LevelData.NewChunks.chunkList[SelectedChunk].tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X];
						blocks = new[] { copiedChunkBlock };
						if (copiedChunkBlock.tileIndex < LevelData.NewTiles.Length)
							TileSelector.SelectedIndex = copiedChunkBlock.tileIndex;
					}
					else
						return;
					break;
				case Keys.PageDown:
					if (ChunkSelector.SelectedIndex < ChunkSelector.Images.Count - 1)
						ChunkSelector.SelectedIndex++;
					return;
				case Keys.PageUp:
					if (ChunkSelector.SelectedIndex > 0)
						ChunkSelector.SelectedIndex--;
					return;
				case Keys.Right:
					if (SelectedChunkBlock.X < 7)
					{
						SelectedChunkBlock = new Rectangle(SelectedChunkBlock.X + 1, SelectedChunkBlock.Y, 1, 1);
						copiedChunkBlock = LevelData.NewChunks.chunkList[SelectedChunk].tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X];
						blocks = new[] { copiedChunkBlock };
						if (copiedChunkBlock.tileIndex < LevelData.NewTiles.Length)
							TileSelector.SelectedIndex = copiedChunkBlock.tileIndex;
					}
					else
						return;
					break;
				case Keys.Q:
					bool angles = anglesToolStripMenuItem.Checked;
					foreach (ToolStripItem item in collisionToolStripMenuItem.DropDownItems)
						if (item is ToolStripMenuItem item1)
							item1.Checked = false;
					noneToolStripMenuItem1.Checked = true;
					anglesToolStripMenuItem.Checked = angles;
					DrawChunkPicture();
					break;
				case Keys.W:
					angles = anglesToolStripMenuItem.Checked;
					foreach (ToolStripItem item in collisionToolStripMenuItem.DropDownItems)
						if (item is ToolStripMenuItem item1)
							item1.Checked = false;
					path1ToolStripMenuItem.Checked = true;
					anglesToolStripMenuItem.Checked = angles;
					DrawChunkPicture();
					break;
				case Keys.E:
					angles = anglesToolStripMenuItem.Checked;
					foreach (ToolStripItem item in collisionToolStripMenuItem.DropDownItems)
						if (item is ToolStripMenuItem item1)
							item1.Checked = false;
					path2ToolStripMenuItem.Checked = true;
					anglesToolStripMenuItem.Checked = angles;
					DrawChunkPicture();
					break;
				case Keys.S:
					if (!e.Control)
					{
						foreach (RSDKv3_4.Tiles128x128.Block.Tile item in blocks)
							if (e.Shift)
								item.solidityA = (item.solidityA == RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip ? RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll : item.solidityA + 1);
							else
								item.solidityA = (item.solidityA == RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll ? RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip : item.solidityA - 1);
						SaveState("Change Chunk Blocks Solidity A");
					}
					break;
				case Keys.T:
					foreach (RSDKv3_4.Tiles128x128.Block.Tile item in blocks)
						if (e.Shift)
							item.solidityB = (item.solidityB == RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip ? RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll : item.solidityB + 1);
						else
							item.solidityB = (item.solidityB == RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll ? RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip : item.solidityB - 1);
					SaveState("Change Chunk Blocks Solidity B");
					break;
				case Keys.Up:
					if (SelectedChunkBlock.Y > 0)
					{
						SelectedChunkBlock = new Rectangle(SelectedChunkBlock.X, SelectedChunkBlock.Y - 1, 1, 1);
						copiedChunkBlock = LevelData.NewChunks.chunkList[SelectedChunk].tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X];
						blocks = new[] { copiedChunkBlock };
						if (copiedChunkBlock.tileIndex < LevelData.NewTiles.Length)
							TileSelector.SelectedIndex = copiedChunkBlock.tileIndex;
					}
					else
						return;
					break;
				case Keys.X:
					foreach (RSDKv3_4.Tiles128x128.Block.Tile item in blocks)
						item.direction ^= RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX;
					SaveState("Change Chunk Blocks X Flip");
					break;
				case Keys.Y:
					if (!e.Control)
					{
						foreach (RSDKv3_4.Tiles128x128.Block.Tile item in blocks)
							item.direction ^= RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY;
						SaveState("Change Chunk Blocks Y Flip");
					}
					break;
				default:
					return;
			}
			LevelData.RedrawChunk(SelectedChunk);
			DrawLevel();
			DrawChunkPicture();
			ChunkSelector.Invalidate();
			chunkBlockEditor.SelectedObjects = blocks;
		}

		private void chunkBlockEditor_PropertyValueChanged(object sender, EventArgs e)
		{
			LevelData.RedrawChunk(SelectedChunk);
			DrawLevel();
			DrawChunkPicture();
			SaveState("Change Chunk Blocks Property");
		}

		private void DrawChunkPicture()
		{
			if (!loaded) return;
			BitmapBits32 bmp = new BitmapBits32(128, 128);
			LevelImgPalette.Entries.CopyTo(bmp.Palette, 0);
			bmp.Clear(bmp.Palette[LevelData.ColorTransparent]);
			if (lowToolStripMenuItem.Checked && highToolStripMenuItem.Checked)
				bmp.DrawSprite(LevelData.ChunkSprites[SelectedChunk], 0, 0);
			else if (lowToolStripMenuItem.Checked)
				bmp.DrawSpriteLow(LevelData.ChunkSprites[SelectedChunk], 0, 0);
			else if (highToolStripMenuItem.Checked)
				bmp.DrawSpriteHigh(LevelData.ChunkSprites[SelectedChunk], 0, 0);

			bmp.Palette[LevelData.ColorWhite] = Color.White;
			bmp.Palette[LevelData.ColorYellow] = Color.Yellow;
			bmp.Palette[LevelData.ColorBlack] = Color.Black;
			bmp.Palette[LevelData.ColorRed] = Color.Red;

			if (path1ToolStripMenuItem.Checked)
				bmp.DrawBitmap(LevelData.ChunkColBmpBits[SelectedChunk][0], 0, 0);
			if (path2ToolStripMenuItem.Checked)
				bmp.DrawBitmap(LevelData.ChunkColBmpBits[SelectedChunk][1], 0, 0);

			bmp.DrawRectangle(Color.White, SelectedChunkBlock.X * 16 - 1, SelectedChunkBlock.Y * 16 - 1, SelectedChunkBlock.Width * 16 + 1, SelectedChunkBlock.Height * 16 + 1);
			using (Graphics gfx = ChunkPicture.CreateGraphics())
			{
				gfx.SetOptions();
				gfx.DrawImage(bmp.ToBitmap(), 0, 0, 128, 128);
			}
		}

		private void ChunkPicture_Paint(object sender, PaintEventArgs e)
		{
			DrawChunkPicture();
		}

		private void flipChunkHButton_Click(object sender, EventArgs e)
		{
			RSDKv3_4.Tiles128x128.Block newcnk = LevelData.NewChunks.chunkList[SelectedChunk].Flip(true, false);
			LevelData.NewChunks.chunkList[SelectedChunk] = newcnk;
			LevelData.RedrawChunk(SelectedChunk);
			copiedChunkBlock = (chunkBlockEditor.SelectedObjects = GetSelectedChunkBlocks())[0];
			if (newcnk.tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X].tileIndex < LevelData.NewTiles.Length)
				TileSelector.SelectedIndex = newcnk.tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X].tileIndex;
			DrawChunkPicture();
			ChunkSelector.Invalidate();
			SaveState("Flip Chunk Horiz");
		}

		private void flipChunkVButton_Click(object sender, EventArgs e)
		{
			RSDKv3_4.Tiles128x128.Block newcnk = LevelData.NewChunks.chunkList[SelectedChunk].Flip(false, true);
			LevelData.NewChunks.chunkList[SelectedChunk] = newcnk;
			LevelData.RedrawChunk(SelectedChunk);
			copiedChunkBlock = (chunkBlockEditor.SelectedObjects = GetSelectedChunkBlocks())[0];
			if (newcnk.tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X].tileIndex < LevelData.NewTiles.Length)
				TileSelector.SelectedIndex = newcnk.tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X].tileIndex;
			DrawChunkPicture();
			ChunkSelector.Invalidate();
			SaveState("Flip Chunk Vert");
		}

		Color[,] disppal = null;
		private void DrawPalette()
		{
			if (!loaded) return;
			Color[,] pal = disppal;
			if (pal == null)
			{
				pal = new Color[16, 16];
				for (int y = 0; y < 16; y++)
					for (int x = 0; x < 16; x++)
						pal[y, x] = LevelData.NewPalette[(y * 16) + x];
			}
			for (int y = 0; y < 16; y++)
				for (int x = 0; x < 16; x++)
				{
					PalettePanelGfx.FillRectangle(new SolidBrush(pal[y, x]), x * 20, y * 20, 20, 20);
					PalettePanelGfx.DrawRectangle(Pens.White, x * 20, y * 20, 19, 19);
				}
			if (disppal == null)
				PalettePanelGfx.DrawRectangle(new Pen(Color.Yellow, 2), SelectedColor.X * 20, SelectedColor.Y * 20, 20, 20);
			else if (lastmouse.Y == SelectedColor.Y)
				PalettePanelGfx.DrawRectangle(new Pen(Color.Yellow, 2), lastmouse.X * 20, lastmouse.Y * 20, 20, 20);
			else
				PalettePanelGfx.DrawRectangle(new Pen(Color.Yellow, 2), 0, lastmouse.Y * 20, 320, 20);
		}

		private void PalettePanel_Paint(object sender, PaintEventArgs e)
		{
			DrawPalette();
		}

		int[] cols;
		private void PalettePanel_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (!loaded || e.Button != MouseButtons.Left) return;
			int line = e.Y / 20;
			int index = e.X / 20;
			if (index < 0 || index > 15 || line < 0 || line > 15) return;
			SelectedColor = new Point(index, line);
			ColorDialog a = new ColorDialog
			{
				AllowFullOpen = true,
				AnyColor = true,
				FullOpen = true,
				Color = LevelData.NewPalette[(line * 16) + index]
			};
			if (cols != null)
				a.CustomColors = cols;
			if (a.ShowDialog() == DialogResult.OK)
			{
				LevelData.NewPalette[(line * 16) + index] = a.Color;
				LevelData.PaletteChanged();
				loaded = false;
				colorRed.Value = a.Color.R;
				colorGreen.Value = a.Color.G;
				colorBlue.Value = a.Color.B;
				colorHex.Value = a.Color.ToArgb() & 0xFFFFFF;
				loaded = true;
				SaveState($"Change Color {(line * 16) + index}");
			}
			cols = a.CustomColors;
		}

		private void color_ValueChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X] = Color.FromArgb((byte)colorRed.Value, (byte)colorGreen.Value, (byte)colorBlue.Value);

			loaded = false;
			colorHex.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].ToArgb() & 0xFFFFFF;
			loaded = true;

			LevelData.PaletteChanged();
			SaveState($"Change Color {(SelectedColor.Y * 16) + SelectedColor.X}");
		}

		private void colorHex_ValueChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X] = Color.FromArgb((int)((int)colorHex.Value | 0xFF000000));
			
			loaded = false;
			colorRed.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].R;
			colorGreen.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].G;
			colorBlue.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].B;
			loaded = true;

			LevelData.PaletteChanged();
			SaveState($"Change Color {(SelectedColor.Y * 16) + SelectedColor.X}");
		}

		private void PalettePanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			Point mouseColor = new Point(e.X / 20, e.Y / 20);
			if (mouseColor.X < 0 || mouseColor.X > 15 || mouseColor.Y < 0 || mouseColor.Y > 15) return;
			if (mouseColor == SelectedColor) return;
			bool newpal = mouseColor.Y != SelectedColor.Y;
			switch (e.Button)
			{
				case MouseButtons.Left:
					SetSelectedColor(mouseColor);
					break;
				case MouseButtons.Right:
					if (!newpal)
					{
						int start = Math.Min(SelectedColor.X, mouseColor.X);
						int end = Math.Max(SelectedColor.X, mouseColor.X);
						if (end - start == 1) return;
						Color startcol = LevelData.NewPalette[(SelectedColor.Y * 16) + start];
						Color endcol = LevelData.NewPalette[(SelectedColor.Y * 16) + end];
						double r = startcol.R;
						double g = startcol.G;
						double b = startcol.B;
						double radd = (endcol.R - startcol.R) / (double)(end - start);
						double gadd = (endcol.G - startcol.G) / (double)(end - start);
						double badd = (endcol.B - startcol.B) / (double)(end - start);
						for (int x = start + 1; x < end; x++)
						{
							r += radd;
							g += gadd;
							b += badd;
							LevelData.NewPalette[(SelectedColor.Y * 16) + x] = Color.FromArgb((int)Math.Round(r, MidpointRounding.AwayFromZero), (int)Math.Round(g, MidpointRounding.AwayFromZero), (int)Math.Round(b, MidpointRounding.AwayFromZero));
						}
						LevelData.PaletteChanged();
						SaveState("Create Palette Gradient");
					}
					break;
			}
		}

		private void SetSelectedColor(Point color)
		{
			SelectedColor = color;
			lastmouse = color;
			DrawPalette();
			loaded = false;
			colorRed.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].R;
			colorGreen.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].G;
			colorBlue.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].B;
			colorHex.Value = LevelData.NewPalette[(SelectedColor.Y * 16) + SelectedColor.X].ToArgb() & 0xFFFFFF;
			loaded = true;
		}

		private void PalettePanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (!loaded || e.Button != MouseButtons.Left || !enableDraggingPaletteButton.Checked) return;
			Point mouseColor = new Point(e.X / 20, e.Y / 20);
			if (mouseColor == lastmouse) return;
			if (mouseColor == SelectedColor)
			{
				disppal = null;
				lastmouse = mouseColor;
				DrawPalette();
			}
			if (mouseColor.X < 0 || mouseColor.Y < 0 || mouseColor.X > 15 || mouseColor.Y > 15) return;
			List<List<Point>> palidxs = new List<List<Point>>();
			for (int y = 0; y < 16; y++)
			{
				List<Point> l = new List<Point>();
				for (int x = 0; x < 16; x++)
					l.Add(new Point(x, y));
				palidxs.Add(l);
			}
			if (mouseColor.Y != SelectedColor.Y)
			{
				if (mouseColor.Y == lastmouse.Y)
				{
					lastmouse = mouseColor;
					return;
				}
				if ((ModifierKeys & Keys.Control) == Keys.Control)
					palidxs.Swap(SelectedColor.Y, mouseColor.Y);
				else
					palidxs.Move(SelectedColor.Y, mouseColor.Y > SelectedColor.Y ? mouseColor.Y + 1 : mouseColor.Y);
			}
			else
			{
				if ((ModifierKeys & Keys.Control) == Keys.Control)
					palidxs[mouseColor.Y].Swap(SelectedColor.X, mouseColor.X);
				else
					palidxs[mouseColor.Y].Move(SelectedColor.X, mouseColor.X > SelectedColor.X ? mouseColor.X + 1 : mouseColor.X);
			}
			disppal = new Color[16, 16];
			for (int y = 0; y < 16; y++)
				for (int x = 0; x < 16; x++)
					disppal[y, x] = LevelData.NewPalette[(palidxs[y][x].Y * 16) + palidxs[y][x].X];
			lastmouse = mouseColor;
			DrawPalette();
		}

		private void PalettePanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (!loaded || e.Button != MouseButtons.Left || !enableDraggingPaletteButton.Checked) return;
			Point mouseColor = lastmouse;
			if (mouseColor == SelectedColor) return;
			if (mouseColor.X < 0 || mouseColor.Y < 0 || mouseColor.X > 15 || mouseColor.Y > 15) return;
			disppal = null;
			byte src = (byte)((SelectedColor.Y * 16) + SelectedColor.X);
			byte dst = (byte)((mouseColor.Y * 16) + mouseColor.X);
			Dictionary<byte, byte> map = new Dictionary<byte, byte> { { src, dst } };
			if (ModifierKeys.HasFlag(Keys.Control))
				map[dst] = src;
			else if (src > dst)
			{
				for (byte i = dst; i < src; i++)
					map[i] = (byte)(i + 1);
			}
			else
			{
				for (byte i = dst; i > src; i--)
					map[i] = (byte)(i - 1);
			}
			List<int> tiles = new List<int>();
			for (int t = 0; t < LevelData.NewTiles.Length; t++)
			{
				BitmapBits block = LevelData.NewTiles[t];
				bool edit = false;
				foreach (var item in map)
					if (Array.IndexOf(block.Bits, item.Key) != -1)
					{
						edit = true;
						block.ReplaceColor(item.Key, item.Value);
					}
				if (edit)
					tiles.Add(t);
			}
			if (tiles.Count > 0)
				LevelData.RedrawBlocks(tiles, true);
			Color[] tmp = (Color[])LevelData.NewPalette.Clone();
			foreach (var item in map)
				LevelData.NewPalette[item.Value] = tmp[item.Key];
			SelectedColor = mouseColor;
			LevelData.PaletteChanged();
			SaveState($"{(ModifierKeys.HasFlag(Keys.Control) ? "Swap" : "Move")} Color");
		}

		private void importGlobalPaletteToolStripButton_Click(object sender, EventArgs e)
		{
			importPalette(0, 6);
		}

		private void importStagePaletteToolStripButton_Click(object sender, EventArgs e)
		{
			importPalette(96, 10);
		}

		private void importPalette(int start, int rows)
		{
			using (OpenFileDialog a = new OpenFileDialog())
			{
				a.DefaultExt = "act";
				a.Filter = "Palette Files|*.act|Image Files|*.bmp;*.png;*.jpg;*.gif";
				a.RestoreDirectory = true;
				if (a.ShowDialog(this) == DialogResult.OK)
				{
					switch (Path.GetExtension(a.FileName))
					{
						case ".act":
							byte[] palette = File.ReadAllBytes(a.FileName);
							for (int i = 0; i < Math.Min(palette.Length / 3, rows * 16); i++)
								LevelData.NewPalette[start + i] = Color.FromArgb(palette[i * 3], palette[i * 3 + 1], palette[i * 3 + 2]);
							break;
						case ".bmp":
						case ".png":
						case ".jpg":
						case ".gif":
							using (Bitmap bmp = new Bitmap(a.FileName))
							{
								if ((bmp.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
								{
									Color[] pal = bmp.Palette.Entries;
									for (int i = start; i < Math.Min(Math.Min(pal.Length, 255), start + (rows * 16)); i++)
										LevelData.NewPalette[i] = pal[i];
								}
								else
									for (int y = 0; y < bmp.Height; y += 8)
									{
										for (int x = 0; x < bmp.Width; x += 8)
										{
											int index = start + ((y / 8) * (bmp.Width)) + (x / 8);
											LevelData.NewPalette[index] = bmp.GetPixel(x, y);
											if (index > 255)
											{
												y = bmp.Height;
												break;
											}
										}
									}
							}
							break;
					}
				}
			}
			LevelData.PaletteChanged();
			SaveState("Import Palette");
		}

		private void importPaletteToolStripButton_Click(object sender, EventArgs e)
		{
			/*using (OpenFileDialog a = new OpenFileDialog())
			{
				a.DefaultExt = "bin";
				a.Filter = "MD Palettes|*.bin|Image Files|*.bmp;*.png;*.jpg;*.gif";
				a.RestoreDirectory = true;
				if (a.ShowDialog(this) == DialogResult.OK)
				{
					int l = SelectedColor.Y;
					int x = SelectedColor.X;
					switch (Path.GetExtension(a.FileName))
					{
						case ".bin":
							SonLVLColor[] colors = SonLVLColor.Load(a.FileName, LevelData.Level.PaletteFormat);
							for (int i = 0; i < colors.Length; i++)
							{
								LevelData.Palette[LevelData.CurPal][l, x] = colors[i];
								x++;
								if (x == 16)
								{
									x = 0;
									l++;
									if (l == 4)
										break;
								}
							}
							break;
						case ".bmp":
						case ".png":
						case ".jpg":
						case ".gif":
							using (Bitmap bmp = new Bitmap(a.FileName))
							{
								if ((bmp.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
								{
									Color[] pal = bmp.Palette.Entries;
									for (int i = 0; i < pal.Length; i++)
									{
										LevelData.ColorToPalette(l, x++, pal[i]);
										if (x == 16)
										{
											x = 0;
											l++;
											if (l == 4)
												break;
										}
									}
								}
								else
									for (int y = 0; y < bmp.Height; y += 8)
									{
										for (int ix = 0; ix < bmp.Width; ix += 8)
										{
											LevelData.ColorToPalette(l, x++, bmp.GetPixel(ix, y));
											if (x == 16)
											{
												x = 0;
												l++;
												if (l == 4)
													break;
											}
										}
										if (l == 4)
											break;
									}
							}
							break;
					}
				}
			}
			LevelData.PaletteChanged();*/
		}

		private void TileSelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			importTilesToolStripButton.Enabled = LevelData.HasFreeTiles();
			drawTileToolStripButton.Enabled = importTilesToolStripButton.Enabled;
			if (TileSelector.SelectedIndex > -1)
			{
				rotateTileRightButton.Enabled = flipTileHButton.Enabled = flipTileVButton.Enabled = true;
				SelectedTile = TileSelector.SelectedIndex;
				TileID.Value = SelectedTile;
				DrawTilePicture();
				if (copiedChunkBlock.tileIndex != SelectedTile)
				{
					copiedChunkBlock = copiedChunkBlock.Clone();
					copiedChunkBlock.tileIndex = (ushort)SelectedTile;
				}
				collisionCeiling.Checked = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].flipY;
				floorAngle.Value = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].floorAngle;
				rightAngle.Value = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].rWallAngle;
				ceilingAngle.Value = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].roofAngle;
				leftAngle.Value = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].lWallAngle;
				colFlags.Value = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].flags;
				DrawColPicture();
			}
			else
				rotateTileRightButton.Enabled = flipTileHButton.Enabled = flipTileVButton.Enabled = false;
		}

		private void TilePicture_Paint(object sender, PaintEventArgs e)
		{
			DrawTilePicture();
		}

		private void DrawTilePicture()
		{
			if (TileSelector.SelectedIndex == -1) return;
			using (Graphics gfx = TilePicture.CreateGraphics())
			{
				gfx.SetOptions();
				gfx.DrawImage(LevelData.NewTiles[SelectedTile].Scale(8).ToBitmap(LevelImgPalette), 0, 0, TilePicture.Width, TilePicture.Height);
			}
		}

		private void TilePicture_MouseDown(object sender, MouseEventArgs e)
		{
			if (TileSelector.SelectedIndex == -1) return;
			if (e.Button == MouseButtons.Left)
			{
				LevelData.NewTiles[SelectedTile][e.X / 8, e.Y / 8] = (byte)((SelectedColor.Y * 16) + SelectedColor.X);
				DrawTilePicture();
			}
			else if (e.Button == MouseButtons.Right)
			{
				int y = Math.DivRem(LevelData.NewTiles[SelectedTile][e.X / 8, e.Y / 8], 16, out int x);
				SetSelectedColor(new Point(x, y));
			}
		}

		private void TilePicture_MouseMove(object sender, MouseEventArgs e)
		{
			if (TileSelector.SelectedIndex == -1) return;
			if (e.Button == MouseButtons.Left && new Rectangle(Point.Empty, TilePicture.Size).Contains(e.Location))
			{
				LevelData.NewTiles[SelectedTile][e.X / 8, e.Y / 8] = (byte)((SelectedColor.Y * 16) + SelectedColor.X);
				DrawTilePicture();
			}
		}

		private void TilePicture_MouseUp(object sender, MouseEventArgs e)
		{
			if (TileSelector.SelectedIndex == -1 || e.Button != MouseButtons.Left) return;
			LevelData.RedrawBlock(SelectedTile, true);
			TileSelector.Invalidate();
			chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
			SaveState("Paint Tile");
		}

		private void ChunkSelector_MouseDown(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			if (CurrentTab == Tab.Art & e.Button == MouseButtons.Right)
			{
				pasteOverToolStripMenuItem.Enabled = Clipboard.ContainsData(typeof(RSDKv3_4.Tiles128x128.Block).AssemblyQualifiedName) || Clipboard.ContainsData(typeof(ChunkCopyData).AssemblyQualifiedName);
				duplicateTilesToolStripMenuItem.Enabled = LevelData.HasFreeChunks();
				deepCopyToolStripMenuItem.Visible = true;
				importOverToolStripMenuItem.Text = "&Reimport...";
				tileContextMenuStrip.Show(ChunkSelector, e.Location);
			}
		}

		private void TileSelector_MouseDown(object sender, MouseEventArgs e)
		{
			if (!loaded) return;
			if (e.Button == MouseButtons.Right)
			{
				pasteOverToolStripMenuItem.Enabled = Clipboard.ContainsData(typeof(TileCopyData).AssemblyQualifiedName);
				duplicateTilesToolStripMenuItem.Enabled = LevelData.HasFreeTiles();
				deepCopyToolStripMenuItem.Visible = false;
				importOverToolStripMenuItem.Text = "&Import Over...";
				tileContextMenuStrip.Show(TileSelector, e.Location);
			}
		}

		private void cutTilesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (CurrentArtTab)
			{
				case ArtTab.Chunks:
					DataObject d = new DataObject(typeof(RSDKv3_4.Tiles128x128.Block).AssemblyQualifiedName, LevelData.NewChunks.chunkList[SelectedChunk]);
					d.SetImage(LevelData.ChunkSprites[SelectedChunk].GetBitmap().ToBitmap(LevelImgPalette));
					Clipboard.SetDataObject(d);
					DeleteChunk();
					SaveState("Cut Chunk");
					break;
				case ArtTab.Tiles:
					d = new DataObject(typeof(TileCopyData).AssemblyQualifiedName, new TileCopyData(LevelData.NewTiles[SelectedTile], LevelData.Collision.collisionMasks[0][SelectedTile], LevelData.Collision.collisionMasks[1][SelectedTile]));
					d.SetImage(LevelData.NewTiles[SelectedTile].ToBitmap(LevelImgPalette));
					Clipboard.SetDataObject(d);
					DeleteTile();
					SaveState("Cut Tile");
					break;
			}
		}

		private void DeleteChunk()
		{
			LevelData.NewChunks.chunkList[SelectedChunk] = new RSDKv3_4.Tiles128x128.Block();
			LevelData.RedrawChunk(SelectedChunk);
			ChunkSelector.Invalidate();
			DrawChunkPicture();
			chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
		}

		private void DeleteTile()
		{
			LevelData.NewTiles[SelectedTile].Clear();
			LevelData.Collision.collisionMasks[0][SelectedTile] = new RSDKv3_4.TileConfig.CollisionMask();
			LevelData.Collision.collisionMasks[1][SelectedTile] = new RSDKv3_4.TileConfig.CollisionMask();
			TileSelector.Invalidate();
			DrawTilePicture();
			chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
			LevelData.RedrawBlock(SelectedTile, true);
			LevelData.RedrawCol(SelectedTile, true);
			DrawColPicture();
		}

		private void copyTilesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (CurrentArtTab)
			{
				case ArtTab.Chunks:
					DataObject d = new DataObject(typeof(RSDKv3_4.Tiles128x128.Block).AssemblyQualifiedName, LevelData.NewChunks.chunkList[SelectedChunk]);
					d.SetImage(LevelData.ChunkSprites[SelectedChunk].GetBitmap().ToBitmap(LevelImgPalette));
					Clipboard.SetDataObject(d);
					break;
				case ArtTab.Tiles:
					d = new DataObject(typeof(TileCopyData).AssemblyQualifiedName, new TileCopyData(LevelData.NewTiles[SelectedTile], LevelData.Collision.collisionMasks[0][SelectedTile], LevelData.Collision.collisionMasks[1][SelectedTile]));
					d.SetImage(LevelData.NewTiles[SelectedTile].ToBitmap(LevelImgPalette));
					Clipboard.SetDataObject(d);
					break;
			}
		}
		private void duplicateTilesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (CurrentArtTab)
			{
				case ArtTab.Chunks:
					ushort tmp = LevelData.GetFreeChunks().First();
					LevelData.NewChunks.chunkList[tmp] = LevelData.NewChunks.chunkList[SelectedChunk].Clone();
					LevelData.RedrawChunk(tmp);
					SelectedChunk = tmp;
					ChunkSelector.SelectedIndex = tmp;
					SaveState("Duplicate Chunk");
					break;
				case ArtTab.Tiles:
					tmp = LevelData.GetFreeTiles().First();
					LevelData.NewTiles[tmp] = new BitmapBits(LevelData.NewTiles[SelectedTile]);
					LevelData.Collision.collisionMasks[0][tmp] = LevelData.Collision.collisionMasks[0][SelectedTile].Clone();
					LevelData.Collision.collisionMasks[1][tmp] = LevelData.Collision.collisionMasks[1][SelectedTile].Clone();
					LevelData.RedrawCol(tmp, true);
					LevelData.RedrawBlock(tmp, false);
					SelectedTile = tmp;
					TileSelector.SelectedIndex = tmp;
					SaveState("Duplicate Tile");
					break;
			}
		}

		private void deleteTilesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (CurrentArtTab)
			{
				case ArtTab.Chunks:
					DeleteChunk();
					SaveState("Delete Chunk");
					break;
				case ArtTab.Tiles:
					DeleteTile();
					SaveState("Delete Tile");
					break;
			}
		}

		private void importToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog opendlg = new OpenFileDialog())
			{
				opendlg.DefaultExt = "png";
				opendlg.Filter = "Image Files|*.bmp;*.png;*.jpg;*.gif";
				opendlg.RestoreDirectory = true;
				if (opendlg.ShowDialog(this) == DialogResult.OK)
				{
					Bitmap bmp = new Bitmap(opendlg.FileName);
					switch (CurrentArtTab)
					{
						case ArtTab.Chunks:
							if (bmp.Width < 128 || bmp.Height < 128)
							{
								MessageBox.Show(this, $"The image you have selected is too small ({bmp.Width}x{bmp.Height}). It must be at least as large as one chunk (128x128)", "SonLVL-RSDK Chunk Importer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								bmp.Dispose();
								return;
							}
							break;
						case ArtTab.Tiles:
							if (bmp.Width < 16 || bmp.Height < 16)
							{
								MessageBox.Show(this, $"The image you have selected is too small ({bmp.Width}x{bmp.Height}). It must be at least as large as one tile (16x16)", "SonLVL-RSDK Tile Importer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								bmp.Dispose();
								return;
							}
							break;
					}

					Bitmap colbmp1 = null, colbmp2 = null, pribmp = null;
					if (CurrentArtTab != ArtTab.Tiles)
					{
						string fmt = Path.Combine(Path.GetDirectoryName(opendlg.FileName),
							Path.GetFileNameWithoutExtension(opendlg.FileName) + "_{0}" + Path.GetExtension(opendlg.FileName));
						if (File.Exists(string.Format(fmt, "col1")))
						{
							colbmp1 = new Bitmap(string.Format(fmt, "col1"));
							if (File.Exists(string.Format(fmt, "col2")))
								colbmp2 = new Bitmap(string.Format(fmt, "col2"));
						}
						else if (File.Exists(string.Format(fmt, "col")))
							colbmp1 = new Bitmap(string.Format(fmt, "col"));
						if (File.Exists(string.Format(fmt, "pri")))
							pribmp = new Bitmap(string.Format(fmt, "pri"));
					}
					ImportImage(bmp, colbmp1, colbmp2, pribmp, null);
					SaveState($"Import {(CurrentArtTab == ArtTab.Chunks ? "Chunks" : "Tiles")}");
				}
			}
		}

		private bool ImportImage(Bitmap bmp, Bitmap colbmp1, Bitmap colbmp2, Bitmap pribmp, ushort[,] layout)
		{
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();
			int w = bmp.Width;
			int h = bmp.Height;
			Enabled = false;
			UseWaitCursor = true;
			importProgressControl1_SizeChanged(this, EventArgs.Empty);
			importProgressControl1.CurrentProgress = 0;
			importProgressControl1.MaximumProgress = (w / 16) * (h / 16);
			importProgressControl1.BringToFront();
			importProgressControl1.Show();
			Application.DoEvents();
			BitmapInfo bmpi = new BitmapInfo(bmp);
			Application.DoEvents();
			bmp.Dispose();
			ColInfo[][,] blockcoldata = null;
			if (colbmp1 != null)
			{
				blockcoldata = ProcessColBmps(colbmp1, colbmp2, w, h);
				Application.DoEvents();
			}
			bool[,] priority = new bool[w / 16, h / 16];
			if (pribmp != null)
			{
				using (pribmp)
					LevelData.GetPriMap(pribmp, priority);
				Application.DoEvents();
			}
			byte? forcepal = bmpi.PixelFormat == PixelFormat.Format1bppIndexed || bmpi.PixelFormat == PixelFormat.Format4bppIndexed ? (byte)SelectedColor.Y : (byte?)null;
			Application.DoEvents();
			List<BitmapBits> tiles = new List<BitmapBits>(LevelData.NewTiles);
			List<RSDKv3_4.TileConfig.CollisionMask>[] cols = new[] { new List<RSDKv3_4.TileConfig.CollisionMask>(LevelData.Collision.collisionMasks[0]), new List<RSDKv3_4.TileConfig.CollisionMask>(LevelData.Collision.collisionMasks[1]) };
			List<RSDKv3_4.Tiles128x128.Block> chunks = new List<RSDKv3_4.Tiles128x128.Block>(LevelData.NewChunks.chunkList);
			Application.DoEvents();
			ImportResult ir = LevelData.BitmapToTiles(bmpi, priority, blockcoldata, forcepal, tiles, cols, true, () =>
			{
				importProgressControl1.CurrentProgress++;
				Application.DoEvents();
			});
			List<ushort> freetiles = LevelData.GetFreeTiles().ToList();
			if (ir.Art.Count > freetiles.Count)
			{
				importProgressControl1.Hide();
				Enabled = true;
				UseWaitCursor = false;
				MessageBox.Show(this, "There are " + (ir.Art.Count - freetiles.Count) + " tiles over the limit.\nImport cannot proceed.", "SonLVL-RSDK", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (ir.Art.Count > 0)
			{
				for (int y = 0; y < ir.Mappings.GetLength(1); y++)
					for (int x = 0; x < ir.Mappings.GetLength(0); x++)
						if (ir.Mappings[x, y].tileIndex >= LevelData.NewTiles.Length)
							ir.Mappings[x, y].tileIndex = freetiles[ir.Mappings[x, y].tileIndex - LevelData.NewTiles.Length];
				for (int i = 0; i < ir.Art.Count; i++)
				{
					ushort ind = freetiles[i];
					LevelData.NewTiles[ind] = ir.Art[i];
					LevelData.RedrawBlock(ind, false);
					if (ir.Collision1 != null)
					{
						LevelData.Collision.collisionMasks[0][ind] = ir.Collision1[i];
						if (ir.Collision2 != null)
							LevelData.Collision.collisionMasks[1][ind] = ir.Collision2[i];
						LevelData.RedrawCol(ind, false);
					}
				}
				if (CurrentTab == Tab.Art && CurrentArtTab == ArtTab.Tiles)
					TileSelector.SelectedIndex = freetiles[0];
				TileSelector.Invalidate();
			}
			List<RSDKv3_4.Tiles128x128.Block> newChunks = new List<RSDKv3_4.Tiles128x128.Block>();
			switch (CurrentTab)
			{
				case Tab.Foreground:
				case Tab.Background:
					for (int cy = 0; cy < h / 128; cy++)
						for (int cx = 0; cx < w / 128; cx++)
							ImportChunk(ir.Mappings, chunks, newChunks, layout, cx, cy, blockcoldata != null, (blockcoldata != null && blockcoldata[1] != null), pribmp != null);
					break;
				case Tab.Art:
					switch (CurrentArtTab)
					{
						case ArtTab.Chunks:
							for (int cy = 0; cy < h / 128; cy++)
								for (int cx = 0; cx < w / 128; cx++)
									ImportChunk(ir.Mappings, chunks, newChunks, layout, cx, cy, blockcoldata != null, (blockcoldata != null && blockcoldata[1] != null), pribmp != null);
							break;
						case ArtTab.Tiles:
							break;
					}
					break;
			}
			List<ushort> freechunks = LevelData.GetFreeChunks().ToList();
			if (newChunks.Count > freechunks.Count)
			{
				importProgressControl1.Hide();
				Enabled = true;
				UseWaitCursor = false;
				MessageBox.Show(this, "There are " + (newChunks.Count - freechunks.Count) + " chunks over the limit.\nImport cannot proceed.", "SonLVL-RSDK", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (newChunks.Count > 0)
			{
				if (layout != null)
					for (int cy = 0; cy < h / 128; cy++)
						for (int cx = 0; cx < w / 128; cx++)
							if (layout[cx, cy] >= LevelData.NewChunks.chunkList.Length)
								layout[cx, cy] = freechunks[layout[cx, cy] - LevelData.NewChunks.chunkList.Length];
				foreach (var (cnk, ind) in newChunks.Zip(freechunks, (a, b) => (a, b)))
				{
					Application.DoEvents();
					LevelData.NewChunks.chunkList[ind] = cnk;
					LevelData.RedrawChunk(ind);
				}
				if ((CurrentTab == Tab.Foreground || CurrentTab == Tab.Background) || (CurrentTab == Tab.Art && CurrentArtTab == ArtTab.Chunks))
					ChunkSelector.SelectedIndex = freechunks[0];
				ChunkSelector.Invalidate();
			}
			else if (ir.Art.Count > 0)
				chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
			sw.Stop();
			System.Text.StringBuilder msg = new System.Text.StringBuilder();
			msg.AppendFormat("New tiles: {0}\n", ir.Art.Count);
			msg.AppendFormat("New chunks: {0}\n", newChunks.Count);
			msg.Append("\nCompleted in ");
			if (sw.Elapsed.Hours > 0)
			{
				msg.AppendFormat("{0}:{1:00}:{2:00}", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);
				if (sw.Elapsed.Milliseconds > 0)
					msg.AppendFormat(".{000}", sw.Elapsed.Milliseconds);
			}
			else if (sw.Elapsed.Minutes > 0)
			{
				msg.AppendFormat("{0}:{1:00}", sw.Elapsed.Minutes, sw.Elapsed.Seconds);
				if (sw.Elapsed.Milliseconds > 0)
					msg.AppendFormat(".{000}", sw.Elapsed.Milliseconds);
			}
			else
			{
				msg.AppendFormat("{0}", sw.Elapsed.Seconds);
				if (sw.Elapsed.Milliseconds > 0)
					msg.AppendFormat(".{000}", sw.Elapsed.Milliseconds);
			}
			MessageBox.Show(this, msg.ToString(), "Import Results");
			importProgressControl1.Hide();
			Enabled = true;
			UseWaitCursor = false;
			return true;
		}

		private void ImportChunk(RSDKv3_4.Tiles128x128.Block.Tile[,] map, List<RSDKv3_4.Tiles128x128.Block> chunks, List<RSDKv3_4.Tiles128x128.Block> newChunks, ushort[,] layout, int cx, int cy, bool solidityA, bool solidityB, bool priority)
		{
			RSDKv3_4.Tiles128x128.Block cnk = new RSDKv3_4.Tiles128x128.Block();
			for (int by = 0; by < 8; by++)
				for (int bx = 0; bx < 8; bx++)
					cnk.tiles[by][bx] = map[cx * 8 + bx, cy * 8 + by];
			for (ushort i = 0; i < chunks.Count; i++)
			{
				Application.DoEvents();

				for (int y = 0; y < 8; y++)
					for (int x = 0; x < 8; x++)
						if (!(cnk.tiles[y][x].tileIndex == chunks[i].tiles[y][x].tileIndex
						&& cnk.tiles[y][x].direction == chunks[i].tiles[y][x].direction
						&& (!solidityA || cnk.tiles[y][x].solidityA == chunks[i].tiles[y][x].solidityA)
						&& (!solidityB || cnk.tiles[y][x].solidityB == chunks[i].tiles[y][x].solidityB)
						&& (!priority || cnk.tiles[y][x].visualPlane == chunks[i].tiles[y][x].visualPlane)))
							goto next;

				if (layout != null)
					layout[cx, cy] = i;
				return;
			next:;
			}
			chunks.Add(cnk);
			newChunks.Add(cnk);
			if (layout != null)
				layout[cx, cy] = (ushort)(chunks.Count - 1);
		}

		private void DrawColPicture()
		{
			if (TileSelector.SelectedIndex == -1) return;
			using (Graphics gfx = ColPicture.CreateGraphics())
			{
				gfx.SetOptions();
				if (showBlockBehindCollisionCheckBox.Checked)
				{
					BitmapBits32 bmp = new BitmapBits32(16, 16);
					LevelImgPalette.Entries.CopyTo(bmp.Palette, 0);
					bmp.Clear(bmp.Palette[LevelData.ColorTransparent]);
					bmp.DrawBitmap(LevelData.NewTiles[SelectedTile], 0, 0);
					bmp.Palette[1] = Color.White;
					bmp.DrawBitmap(LevelData.NewColBmpBits[SelectedTile][collisionLayerSelector.SelectedIndex], 0, 0);
					gfx.DrawImage(bmp.Scale(8).ToBitmap(), 0, 0, 128, 128);
				}
				else
					gfx.DrawImage(LevelData.NewColBmpBits[SelectedTile][collisionLayerSelector.SelectedIndex].Scale(8).ToBitmap(Color.Black, Color.White), 0, 0, 128, 128);
			}
		}

		private void ColPicture_Paint(object sender, PaintEventArgs e)
		{
			DrawColPicture();
		}

		private void collisionLayerSelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (TileSelector.SelectedIndex == -1) return;
			collisionCeiling.Checked = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].flipY;
			floorAngle.Value = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].floorAngle;
			rightAngle.Value = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].rWallAngle;
			ceilingAngle.Value = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].roofAngle;
			leftAngle.Value = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].lWallAngle;
			colFlags.Value = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].flags;
			DrawColPicture();

			copyCollisionSingleButton.Text = $"Copy to Layer {(collisionLayerSelector.SelectedIndex ^ 1) + 1}";
		}

		private void ColPicture_MouseDown(object sender, MouseEventArgs e)
		{
			if (TileSelector.SelectedIndex == -1) return;
			int x = e.X / 8;
			int y = e.Y / 8;
			if (y == 16)
			{
				LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].heightMasks[x].solid = false;
				LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].heightMasks[x].height = 0;
			}
			else
			{
				LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].heightMasks[x].solid = true;
				LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].heightMasks[x].height = (byte)y;
			}
			LevelData.RedrawCol(SelectedTile, false);
			DrawColPicture();
		}

		private void ColPicture_MouseMove(object sender, MouseEventArgs e)
		{
			if (TileSelector.SelectedIndex == -1 || e.Button == MouseButtons.None) return;
			int x = e.X / 8;
			if (x < 0 || x > 15) return;
			int y = e.Y / 8;
			if (y < 0 || y > 16) return;
			if (y == 16)
			{
				LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].heightMasks[x].solid = false;
				LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].heightMasks[x].height = 0;
			}
			else
			{
				LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].heightMasks[x].solid = true;
				LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].heightMasks[x].height = (byte)y;
			}
			LevelData.RedrawCol(SelectedTile, false);
			DrawColPicture();
		}

		private void ColPicture_MouseUp(object sender, MouseEventArgs e)
		{
			if (TileSelector.SelectedIndex == -1) return;
			LevelData.RedrawCol(SelectedTile, true);
			DrawColPicture();
			SaveState("Edit Collision Mask");
		}

		private void collisionCeiling_CheckedChanged(object sender, EventArgs e)
		{
			if (TileSelector.SelectedIndex == -1) return;
			LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].flipY = collisionCeiling.Checked;
			LevelData.RedrawCol(SelectedTile, true);
			DrawColPicture();
			SaveState("Flip Collision");
		}

		private void floorAngle_ValueChanged(object sender, EventArgs e)
		{
			if (TileSelector.SelectedIndex == -1) return;
			LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].floorAngle = (byte)floorAngle.Value;
			SaveState("Change Floor Angle");
		}

		private void leftAngle_ValueChanged(object sender, EventArgs e)
		{
			if (TileSelector.SelectedIndex == -1) return;
			LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].lWallAngle = (byte)leftAngle.Value;
			SaveState("Change Left Angle");
		}

		private void rightAngle_ValueChanged(object sender, EventArgs e)
		{
			if (TileSelector.SelectedIndex == -1) return;
			LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].rWallAngle = (byte)rightAngle.Value;
			SaveState("Change Right Angle");
		}

		private void ceilingAngle_ValueChanged(object sender, EventArgs e)
		{
			if (TileSelector.SelectedIndex == -1) return;
			LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].roofAngle = (byte)ceilingAngle.Value;
			SaveState("Change Ceiling Angle");
		}

		private void ColAngle_TextChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			if (byte.TryParse(floorAngle.Text, System.Globalization.NumberStyles.HexNumber, null, out byte value))
				floorAngle.Value = value;
		}

		private void colFlags_ValueChanged(object sender, EventArgs e)
		{
			if (TileSelector.SelectedIndex == -1) return;
			LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].flags = (byte)colFlags.Value;
			SaveState("Change Collision Flags");
		}

		private void rotateTileRightButton_Click(object sender, EventArgs e)
		{
			LevelData.NewTiles[SelectedTile].Rotate(3);
			LevelData.RedrawBlock(SelectedTile, true);
			chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
			TileSelector_SelectedIndexChanged(this, EventArgs.Empty);
			TileSelector.Invalidate();
			DrawChunkPicture();
			SaveState("Rotate Tile Right");
		}

		private void drawToolStripButton_Click(object sender, EventArgs e)
		{
			using (DrawTileDialog dlg = new DrawTileDialog())
			{
				switch (CurrentArtTab)
				{
					case ArtTab.Chunks:
						dlg.tile = new BitmapBits(128, 128);
						dlg.numericUpDown1.Value = 2;
						break;
					case ArtTab.Tiles:
						dlg.tile = new BitmapBits(16, 16);
						dlg.numericUpDown1.Value = 16;
						break;
				}
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					ImportImage(dlg.tile.ToBitmap(LevelData.BmpPal), null, null, null, null);
					SaveState($"Draw {(CurrentArtTab == ArtTab.Chunks ? "Chunk" : "Tile")}");
				}
			}
		}

		private void reloadTilesToolStripButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Reload 16x16Tiles.gif? Only do this if the file was edited in an external program, it will reset all tile changes made in SonLVL-RSDK!", "SonLVL-RSDK", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
				return;

			prevTiles = LevelData.NewTiles.Select(a => (byte[])a.Bits.Clone()).ToArray();
			
			LevelData.ReloadTiles();
			
			var redrawblocks = new SortedSet<int>();
			for (int i = 0; i < LevelData.NewTiles.Length; i++)
				if (!prevTiles[i].FastArrayEqual(LevelData.NewTiles[i].Bits))
				{
					LevelData.RedrawBlock(i, false);
					redrawblocks.Add(i);
				}

			for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
				if (LevelData.NewChunks.chunkList[i].tiles.SelectMany(a => a).Any(b => redrawblocks.Contains(b.tileIndex)))
					LevelData.RedrawChunk(i);

			TileSelector.Invalidate();
			DrawChunkPicture();
			DrawTilePicture();

			SaveState("Reload Tiles");
		}

		private void TileList_KeyDown(object sender, KeyEventArgs e)
		{
			if (CurrentTab > Tab.Background)
			{
				switch (e.KeyCode)
				{
					case Keys.C:
						if (e.Control)
							copyTilesToolStripMenuItem_Click(sender, EventArgs.Empty);
						break;
					case Keys.D:
						if (e.Control)
							switch (CurrentArtTab)
							{
								case ArtTab.Chunks:
									if (LevelData.HasFreeChunks())
										duplicateTilesToolStripMenuItem_Click(sender, EventArgs.Empty);
									break;
								case ArtTab.Tiles:
									if (LevelData.HasFreeTiles())
										duplicateTilesToolStripMenuItem_Click(sender, EventArgs.Empty);
									break;
							}
						break;
					case Keys.Delete:
						switch (CurrentArtTab)
						{
							case ArtTab.Chunks:
								deleteTilesToolStripMenuItem_Click(sender, EventArgs.Empty);
								break;
							case ArtTab.Tiles:
								deleteTilesToolStripMenuItem_Click(sender, EventArgs.Empty);
								break;
						}
						break;
					case Keys.V:
						if (e.Control)
							switch (CurrentArtTab)
							{
								case ArtTab.Chunks:
									if ((Clipboard.ContainsData(typeof(ChunkCopyData).AssemblyQualifiedName) || Clipboard.ContainsData(typeof(RSDKv3_4.Tiles128x128.Block).AssemblyQualifiedName)))
									{
										pasteOverToolStripMenuItem_Click(sender, EventArgs.Empty);
										LevelData.RedrawChunk(SelectedChunk);
										ChunkSelector.Invalidate();
										DrawChunkPicture();
										chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
									}
										break;
								case ArtTab.Tiles:
									if (Clipboard.ContainsData(typeof(TileCopyData).AssemblyQualifiedName))
									{
										pasteOverToolStripMenuItem_Click(sender, EventArgs.Empty);
										TileSelector.Invalidate();
										DrawTilePicture();
										chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
										LevelData.RedrawBlock(SelectedTile, true);
									}
									break;
							}
						break;
					case Keys.X:
						if (e.Control)
							switch (CurrentArtTab)
							{
								case ArtTab.Chunks:
									if (LevelData.NewChunks.chunkList.Length > 1)
										cutTilesToolStripMenuItem_Click(sender, EventArgs.Empty);
									break;
								case ArtTab.Tiles:
									if (TileSelector.Images.Count > 1)
										cutTilesToolStripMenuItem_Click(sender, EventArgs.Empty);
									break;
							}
						break;
				}
			}
		}

		private void selectAllObjectsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SelectedItems = new List<Entry>(LevelData.Objects.Cast<Entry>());
			SelectedObjectChanged();
			DrawLevel();
		}

		private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			DataObject d = new DataObject();
			ushort[][] layout;
			Rectangle selection;
			Rectangle area;
			if (CurrentTab == Tab.Background)
			{
				layout = LevelData.Background.layers[bglayer].layout;
				selection = BGSelection;
				area = new Rectangle(BGSelection.X * 128, BGSelection.Y * 128, BGSelection.Width * 128, BGSelection.Height * 128);
				d.SetImage(LevelData.DrawBackground(bglayer, area, lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked).ToBitmap(LevelImgPalette));
			}
			else
			{
				layout = LevelData.Scene.layout;
				selection = FGSelection;
				area = new Rectangle(FGSelection.X * 128, FGSelection.Y * 128, FGSelection.Width * 128, FGSelection.Height * 128);
				d.SetImage(LevelData.DrawForeground(area, includeObjectsWithForegroundSelectionToolStripMenuItem.Checked, true, objectsAboveHighPlaneToolStripMenuItem.Checked, lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked, false, false).ToBitmap(LevelImgPalette));
			}
			ushort[,] layoutsection = new ushort[selection.Width, selection.Height];
			for (int y = 0; y < selection.Height; y++)
				for (int x = 0; x < selection.Width; x++)
				{
					layoutsection[x, y] = layout[y + selection.Y][x + selection.X];
					layout[y + selection.Y][x + selection.X] = 0;
				}
			List<Entry> objectselection = new List<Entry>();
			List<Entry> objstodelete = new List<Entry>();
			if (includeObjectsWithForegroundSelectionToolStripMenuItem.Checked && CurrentTab == Tab.Foreground)
			{
				int x = selection.Left * 128;
				int y = selection.Top * 128;
				foreach (ObjectEntry item in LevelData.Objects)
					if (item.Y >= y & item.Y < selection.Bottom * 128
						& item.X >= x & item.X < selection.Right * 128)
					{
						Entry ent = item.Clone();
						ent.X -= (short)x;
						ent.Y -= (short)y;
						objectselection.Add(ent);
						objstodelete.Add(item);
					}
				foreach (Entry item in objstodelete)
				{
					if (item is ObjectEntry oe)
					{
						objectOrder.Items.RemoveAt(LevelData.Objects.IndexOf(oe));
						LevelData.DeleteObject(oe);
					}
					if (SelectedItems.Contains(item))
						SelectedItems.Remove(item);
				}
				SelectedObjectChanged();
			}

			d.SetData(typeof(LayoutSection).AssemblyQualifiedName, new LayoutSection(layoutsection, objectselection));

			if (CurrentTab == Tab.Background)
				BGSelection = Rectangle.Empty;
			else
				FGSelection = Rectangle.Empty;

			Clipboard.SetDataObject(d);
			
			DrawLevel();
			SaveState($"Cut {(CurrentTab == Tab.Background ? $"Background {bglayer + 1}" : "Foreground")}");
		}

		private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			DataObject d = new DataObject(typeof(LayoutSection).AssemblyQualifiedName, CreateLayoutSection(includeObjectsWithForegroundSelectionToolStripMenuItem.Checked));
			
			Rectangle area;
			
			if (CurrentTab == Tab.Background)
			{
				area = new Rectangle(BGSelection.X * 128, BGSelection.Y * 128, BGSelection.Width * 128, BGSelection.Height * 128);
				d.SetImage(LevelData.DrawBackground(bglayer, area, lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked).ToBitmap(LevelImgPalette));
			}
			else
			{
				area = new Rectangle(FGSelection.X * 128, FGSelection.Y * 128, FGSelection.Width * 128, FGSelection.Height * 128);
				d.SetImage(LevelData.DrawForeground(area, includeobjectsWithFGToolStripMenuItem.Checked, true, objectsAboveHighPlaneToolStripMenuItem.Checked, lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked, false, false).ToBitmap(LevelImgPalette));
			}
			
			Clipboard.SetDataObject(d);
		}

		private LayoutSection CreateLayoutSection(bool includeObjects)
		{
			ushort[][] layout;
			Rectangle selection;
			if (CurrentTab == Tab.Background)
			{
				layout = LevelData.Background.layers[bglayer].layout;
				selection = BGSelection;
			}
			else
			{
				layout = LevelData.Scene.layout;
				selection = FGSelection;
			}
			ushort[,] layoutsection = new ushort[selection.Width, selection.Height];
			for (int y = 0; y < selection.Height; y++)
				for (int x = 0; x < selection.Width; x++)
					layoutsection[x, y] = layout[y + selection.Y][x + selection.X];
			List<Entry> objectselection = new List<Entry>();
			if (includeObjects && CurrentTab == Tab.Foreground)
			{
				int x = selection.Left * 128;
				int y = selection.Top * 128;
				if (LevelData.Objects != null)
					foreach (ObjectEntry item in LevelData.Objects)
						if (item.Y >= y & item.Y < selection.Bottom * 128
							& item.X >= x & item.X < selection.Right * 128)
						{
							Entry ent = item.Clone();
							ent.X -= (short)x;
							ent.Y -= (short)y;
							objectselection.Add(ent);
						}
			}
			return new LayoutSection(layoutsection, objectselection);
		}

		private void pasteOnceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LayoutSection section = (LayoutSection)Clipboard.GetData(typeof(LayoutSection).AssemblyQualifiedName);
			if (CurrentTab == Tab.Background)
			{
				BGSelection = new Rectangle(menuLoc.X, menuLoc.Y, section.Layout.GetLength(0), section.Layout.GetLength(1));
				BGSelection.Width = Math.Min(BGSelection.Right, LevelData.BGWidth[bglayer]) - BGSelection.Left;
				BGSelection.Height = Math.Min(BGSelection.Bottom, LevelData.BGHeight[bglayer]) - BGSelection.Top;
			}
			else
			{
				FGSelection = new Rectangle(menuLoc.X, menuLoc.Y, section.Layout.GetLength(0), section.Layout.GetLength(1));
				FGSelection.Width = Math.Min(FGSelection.Right, LevelData.FGWidth) - FGSelection.Left;
				FGSelection.Height = Math.Min(FGSelection.Bottom, LevelData.FGHeight) - FGSelection.Top;
			}
			PasteLayoutSectionOnce(section);
			SaveState($"Paste {(CurrentTab == Tab.Background ? $"Background {bglayer + 1}" : "Foreground")} Once");
		}

		private void PasteLayoutSectionOnce(LayoutSection section)
		{
			ushort[][] layout;
			int w, h;
			if (CurrentTab == Tab.Background)
			{
				layout = LevelData.Background.layers[bglayer].layout;
				w = Math.Min(section.Layout.GetLength(0), LevelData.BGWidth[bglayer] - menuLoc.X);
				h = Math.Min(section.Layout.GetLength(1), LevelData.BGHeight[bglayer] - menuLoc.Y);
			}
			else
			{
				layout = LevelData.Scene.layout;
				w = Math.Min(section.Layout.GetLength(0), LevelData.FGWidth - menuLoc.X);
				h = Math.Min(section.Layout.GetLength(1), LevelData.FGHeight - menuLoc.Y);
			}
			for (int y = 0; y < h; y++)
				for (int x = 0; x < w; x++)
					layout[y + menuLoc.Y][x + menuLoc.X] = section.Layout[x, y];
			if (CurrentTab == Tab.Foreground)
			{
				Size off = new Size(menuLoc.X * 128, menuLoc.Y * 128);
				foreach (Entry item in section.Objects)
				{
					Entry newent = item.Clone();
					newent.X = (short)(newent.X + off.Width);
					newent.Y = (short)(newent.Y + off.Height);
					if (newent is ObjectEntry oe)
					{
						LevelData.AddObject(oe);
						objectOrder.Items.Add(oe.Name, oe.Type < objectTypeImages.Images.Count ? oe.Type : 0);
					}
					newent.UpdateSprite();
				}
			}
			DrawLevel();
		}

		private void pasteRepeatingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LayoutSection section = (LayoutSection)Clipboard.GetData(typeof(LayoutSection).AssemblyQualifiedName);
			PasteLayoutSectionRepeating(section);
			SaveState($"Paste {(CurrentTab == Tab.Background ? $"Background {bglayer + 1}" : "Foreground")} Repeating");
		}

		private void PasteLayoutSectionRepeating(LayoutSection section)
		{
			ushort[][] layout;
			Rectangle selection;
			if (CurrentTab == Tab.Background)
			{
				layout = LevelData.Background.layers[bglayer].layout;
				selection = BGSelection;
			}
			else
			{
				layout = LevelData.Scene.layout;
				selection = FGSelection;
			}
			int width = section.Layout.GetLength(0);
			int height = section.Layout.GetLength(1);
			for (int y = 0; y < selection.Height; y++)
				for (int x = 0; x < selection.Width; x++)
					layout[y + selection.Y][x + selection.X] = section.Layout[x % width, y % height];
			if (includeObjectsWithForegroundSelectionToolStripMenuItem.Checked && CurrentTab == Tab.Foreground)
			{
				int w = (int)Math.Ceiling(selection.Width / (double)width);
				int h = (int)Math.Ceiling(selection.Height / (double)height);
				Point bottomright = new Point(selection.Right * 128, selection.Bottom * 128);
				for (int y = 0; y < h; y++)
					for (int x = 0; x < w; x++)
					{
						Size off = new Size((selection.X + (x * width)) * 128, (selection.Y + (y * height)) * 128);
						foreach (Entry item in section.Objects)
						{
							Entry it2 = item.Clone();
							it2.X = (short)(it2.X + off.Width);
							it2.Y = (short)(it2.Y + off.Height);
							if (it2.X < bottomright.X & it2.Y < bottomright.Y)
							{
								if (it2 is ObjectEntry oe)
								{
									LevelData.AddObject(oe);
									objectOrder.Items.Add(oe.Name, oe.Type < objectTypeImages.Images.Count ? oe.Type : 0);
								}
								it2.UpdateSprite();
							}
						}
					}
			}
			DrawLevel();
		}

		private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ushort[][] layout;
			Rectangle selection;
			if (CurrentTab == Tab.Background)
			{
				layout = LevelData.Background.layers[bglayer].layout;
				selection = BGSelection;
			}
			else
			{
				layout = LevelData.Scene.layout;
				selection = FGSelection;
			}
			for (int y = selection.Top; y < selection.Bottom; y++)
				for (int x = selection.Left; x < selection.Right; x++)
					layout[y][x] = 0;
			if (includeObjectsWithForegroundSelectionToolStripMenuItem.Checked & CurrentTab == Tab.Foreground)
			{
				List<Entry> objectselection = new List<Entry>();
				if (LevelData.Objects != null)
					foreach (ObjectEntry item in LevelData.Objects)
						if (item.Y >= selection.Top * 128 & item.Y < selection.Bottom * 128
							& item.X >= selection.Left * 128 & item.X < selection.Right * 128)
							objectselection.Add(item);
				foreach (Entry item in objectselection)
				{
					if (item is ObjectEntry oe)
					{
						objectOrder.Items.RemoveAt(LevelData.Objects.IndexOf(oe));
						LevelData.DeleteObject(oe);
					}
					if (SelectedItems.Contains(item))
						SelectedItems.Remove(item);
				}
				SelectedObjectChanged();
			}
			DrawLevel();
			SaveState($"Delete {(CurrentTab == Tab.Background ? $"Background {bglayer + 1}" : "Foreground")}");
		}

		private void fillToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ushort[][] layout;
			Rectangle selection;
			if (CurrentTab == Tab.Background)
			{
				layout = LevelData.Background.layers[bglayer].layout;
				selection = BGSelection;
			}
			else
			{
				layout = LevelData.Scene.layout;
				selection = FGSelection;
			}
			for (int y = selection.Top; y < selection.Bottom; y++)
				for (int x = selection.Left; x < selection.Right; x++)
					layout[y][x] = SelectedChunk;
			DrawLevel();
			SaveState($"Fill {(CurrentTab == Tab.Background ? $"Background {bglayer + 1}" : "Foreground")}");
		}

		private void objectTypeList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			objectTypeList.DoDragDrop(new DataObject("SonicRetro.SonLVLRSDK.GUI.ObjectDrop", (byte)((ListViewItem)e.Item).Tag), DragDropEffects.Copy);
		}

		private void objectPanel_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("SonicRetro.SonLVLRSDK.GUI.ObjectDrop"))
			{
				e.Effect = DragDropEffects.All;
				dragdrop = true;
				dragobj = (byte)e.Data.GetData("SonicRetro.SonLVLRSDK.GUI.ObjectDrop");
				dragpoint = objectPanel.PanelPointToClient(new Point(e.X, e.Y));
				dragpoint = new Point((int)(dragpoint.X / ZoomLevel), (int)(dragpoint.Y / ZoomLevel));
				DrawLevel();
			}
			else
				dragdrop = false;
		}

		private void objectPanel_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("SonicRetro.SonLVLRSDK.GUI.ObjectDrop"))
			{
				e.Effect = DragDropEffects.All;
				dragdrop = true;
				dragobj = (byte)e.Data.GetData("SonicRetro.SonLVLRSDK.GUI.ObjectDrop");
				dragpoint = objectPanel.PanelPointToClient(new Point(e.X, e.Y));
				dragpoint = new Point((int)(dragpoint.X / ZoomLevel), (int)(dragpoint.Y / ZoomLevel));
				DrawLevel();
			}
			else
				dragdrop = false;
		}

		private void objectPanel_DragLeave(object sender, EventArgs e)
		{
			dragdrop = false;
			DrawLevel();
		}

		private void objectPanel_DragDrop(object sender, DragEventArgs e)
		{
			dragdrop = false;
			if (e.Data.GetDataPresent("SonicRetro.SonLVLRSDK.GUI.ObjectDrop") && LevelData.Scene.entities.Count < RSDKv3_4.Scene.ENTITY_LIST_SIZE)
			{
				double gs = snapToolStripMenuItem.Checked ? 1 << ObjGrid : 1;
				Point clientPoint = objectPanel.PanelPointToClient(new Point(e.X, e.Y));
				clientPoint = new Point((int)(clientPoint.X / ZoomLevel), (int)(clientPoint.Y / ZoomLevel));
				ObjectEntry obj = LevelData.CreateObject((byte)e.Data.GetData("SonicRetro.SonLVLRSDK.GUI.ObjectDrop"));
				objectOrder.Items.Add(obj.Name, obj.Type < objectTypeImages.Images.Count ? obj.Type : 0);
				obj.X = (short)(Math.Round((clientPoint.X + objectPanel.HScrollValue) / gs, MidpointRounding.AwayFromZero) * gs);
				obj.Y = (short)(Math.Round((clientPoint.Y + objectPanel.VScrollValue) / gs, MidpointRounding.AwayFromZero) * gs);
				obj.UpdateSprite();
				SelectedItems = new List<Entry>(1) { obj };
				SelectedObjectChanged();
				objectPanel.FocusPanel();
				DrawLevel();
				SaveState("Add Object");
			}
		}

		private void insertLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (InsertDeleteDialog dlg = new InsertDeleteDialog())
			{
				dlg.Text = "Insert";
				dlg.moveObjects.Visible = dlg.moveObjects.Checked = CurrentTab == Tab.Foreground;
				if (dlg.ShowDialog(this) != DialogResult.OK) return;
				Rectangle selection;
				if (CurrentTab == Tab.Background)
					selection = BGSelection;
				else
					selection = FGSelection;
				if (dlg.shiftH.Checked)
				{
					ushort[][] layout;
					if (CurrentTab == Tab.Background)
					{
						if (LevelData.BGWidth[bglayer] < 255)
							LevelData.ResizeBG(bglayer, Math.Min(255, LevelData.BGWidth[bglayer] + selection.Width), LevelData.BGHeight[bglayer]);
						layout = LevelData.Background.layers[bglayer].layout;
					}
					else
					{
						if (LevelData.FGWidth < 255)
							LevelData.ResizeFG(Math.Min(255, LevelData.FGWidth + selection.Width), LevelData.FGHeight);
						layout = LevelData.Scene.layout;
					}
					for (int y = selection.Top; y < selection.Bottom; y++)
						for (int x = layout[y].Length - selection.Width - 1; x >= selection.Left; x--)
							layout[y][x + selection.Width] = layout[y][x];
					for (int y = selection.Top; y < selection.Bottom; y++)
						for (int x = selection.Left; x < selection.Right; x++)
							layout[y][x] = 0;
					if (dlg.moveObjects.Checked)
					{
						if (LevelData.Objects != null)
							foreach (ObjectEntry item in LevelData.Objects)
								if (item.Y >= selection.Top * 128 && item.Y < selection.Bottom * 128 && item.X >= selection.Left * 128)
								{
									item.X += (short)(selection.Width * 128);
									item.UpdateSprite();
								}
					}
				}
				else if (dlg.shiftV.Checked)
				{
					ushort[][] layout;
					if (CurrentTab == Tab.Background)
					{
						if (LevelData.BGHeight[bglayer] < 255)
							LevelData.ResizeBG(bglayer, LevelData.BGWidth[bglayer], Math.Min(255, LevelData.BGHeight[bglayer] + selection.Height));
						layout = LevelData.Background.layers[bglayer].layout;
					}
					else
					{
						if (LevelData.FGHeight < 255)
							LevelData.ResizeFG(LevelData.FGWidth, Math.Min(255, LevelData.FGHeight + selection.Height));
						layout = LevelData.Scene.layout;
					}
					for (int x = selection.Left; x < selection.Right; x++)
						for (int y = layout.Length - selection.Height - 1; y >= selection.Top; y--)
							layout[y + selection.Height][x] = layout[y][x];
					for (int x = selection.Left; x < selection.Right; x++)
						for (int y = selection.Top; y < selection.Bottom; y++)
							layout[y][x] = 0;
					if (dlg.moveObjects.Checked)
					{
						if (LevelData.Objects != null)
							foreach (ObjectEntry item in LevelData.Objects)
								if (item.X >= selection.Left * 128 && item.X < selection.Right * 128 && item.Y >= selection.Top * 128)
								{
									item.Y += (short)(selection.Height * 128);
									item.UpdateSprite();
								}
					}
				}
				else if (dlg.entireRow.Checked)
				{
					ushort[][] layout;
					int width;
					if (CurrentTab == Tab.Background)
					{
						width = LevelData.BGWidth[bglayer];
						if (LevelData.BGHeight[bglayer] < 255)
							LevelData.ResizeBG(bglayer, width, Math.Min(255, LevelData.BGHeight[bglayer] + selection.Height));
						layout = LevelData.Background.layers[bglayer].layout;
					}
					else
					{
						width = LevelData.FGWidth;
						if (LevelData.FGHeight < 255)
							LevelData.ResizeFG(width, Math.Min(255, LevelData.FGHeight + selection.Height));
						layout = LevelData.Scene.layout;
					}
					for (int x = 0; x < width; x++)
						for (int y = layout.Length - selection.Height - 1; y >= selection.Top; y--)
							layout[y + selection.Height][x] = layout[y][x];
					for (int x = 0; x < width; x++)
						for (int y = selection.Top; y < selection.Bottom; y++)
							layout[y][x] = 0;
					if (dlg.moveObjects.Checked)
					{
						if (LevelData.Objects != null)
							foreach (ObjectEntry item in LevelData.Objects)
								if (item.Y >= selection.Top * 128)
								{
									item.Y += (short)(selection.Height * 128);
									item.UpdateSprite();
								}
					}
				}
				else if (dlg.entireColumn.Checked)
				{
					ushort[][] layout;
					if (CurrentTab == Tab.Background)
					{
						if (LevelData.BGWidth[bglayer] < 255)
							LevelData.ResizeBG(bglayer, Math.Min(255, LevelData.BGWidth[bglayer] + selection.Width), LevelData.BGHeight[bglayer]);
						layout = LevelData.Background.layers[bglayer].layout;
					}
					else
					{
						if (LevelData.FGWidth < 255)
							LevelData.ResizeFG(Math.Min(255, LevelData.FGWidth + selection.Width), LevelData.FGHeight);
						layout = LevelData.Scene.layout;
					}
					for (int y = 0; y < layout.Length; y++)
						for (int x = layout[y].Length - selection.Width - 1; x >= selection.Left; x--)
							layout[y][x + selection.Width] = layout[y][x];
					for (int y = 0; y < layout.Length; y++)
						for (int x = selection.Left; x < selection.Right; x++)
							layout[y][x] = 0;
					if (dlg.moveObjects.Checked)
					{
						if (LevelData.Objects != null)
							foreach (ObjectEntry item in LevelData.Objects)
								if (item.X >= selection.Left * 128)
								{
									item.X += (short)(selection.Width * 128);
									item.UpdateSprite();
								}
					}
				}
				UpdateScrollBars();
				DrawLevel();
				SaveState($"Insert {(CurrentTab == Tab.Background ? $"Background {bglayer + 1}" : "Foreground")}");
			}
		}

		private void deleteLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (InsertDeleteDialog dlg = new InsertDeleteDialog())
			{
				dlg.Text = "Delete";
				dlg.shiftH.Text = "Shift cells left";
				dlg.shiftV.Text = "Shift cells up";
				dlg.moveObjects.Visible = dlg.moveObjects.Checked = CurrentTab == Tab.Foreground;
				if (dlg.ShowDialog(this) != DialogResult.OK) return;
				Rectangle selection;
				if (CurrentTab == Tab.Background)
					selection = BGSelection;
				else
					selection = FGSelection;
				if (dlg.shiftH.Checked)
				{
					ushort[][] layout;
					if (CurrentTab == Tab.Background)
						layout = LevelData.Background.layers[bglayer].layout;
					else
						layout = LevelData.Scene.layout;
					for (int y = selection.Top; y < selection.Bottom; y++)
						for (int x = selection.Left; x < layout[y].Length - selection.Width; x++)
							layout[y][x] = layout[y][x + selection.Width];
					for (int y = selection.Top; y < selection.Bottom; y++)
						for (int x = layout[y].Length - selection.Width; x < layout[y].Length; x++)
							layout[y][x] = 0;
					if (dlg.moveObjects.Checked)
					{
						if (LevelData.Objects != null)
							foreach (ObjectEntry item in LevelData.Objects)
								if (item.Y >= selection.Top * 128 & item.Y < selection.Bottom * 128 & item.X >= selection.Right * 128)
								{
									item.X -= (short)(selection.Width * 128);
									item.UpdateSprite();
								}
					}
				}
				else if (dlg.shiftV.Checked)
				{
					ushort[][] layout;
					if (CurrentTab == Tab.Background)
						layout = LevelData.Background.layers[bglayer].layout;
					else
						layout = LevelData.Scene.layout;
					for (int x = selection.Left; x < selection.Right; x++)
						for (int y = selection.Top; y < layout.Length - selection.Height; y++)
							layout[y][x] = layout[y + selection.Height][x];
					for (int x = selection.Left; x < selection.Right; x++)
						for (int y = layout.Length - selection.Height; y < layout.Length; y++)
							layout[y][x] = 0;
					if (dlg.moveObjects.Checked)
					{
						if (LevelData.Objects != null)
							foreach (ObjectEntry item in LevelData.Objects)
								if (item.X >= selection.Left * 128 & item.X < selection.Right * 128 & item.Y >= selection.Bottom * 128)
								{
									item.Y -= (short)(selection.Height * 128);
									item.UpdateSprite();
								}
					}
				}
				else if (dlg.entireRow.Checked)
				{
					ushort[][] layout;
					int width;
					if (CurrentTab == Tab.Background)
					{
						layout = LevelData.Background.layers[bglayer].layout;
						width = LevelData.BGWidth[bglayer];
					}
					else
					{
						layout = LevelData.Scene.layout;
						width = LevelData.FGWidth;
					}
					for (int x = 0; x < width; x++)
						for (int y = selection.Top; y < layout.Length - selection.Height; y++)
							layout[y][x] = layout[y + selection.Height][x];
					for (int x = 0; x < width; x++)
						for (int y = layout.Length - selection.Height; y < layout.Length; y++)
							layout[y][x] = 0;
					if (dlg.moveObjects.Checked)
					{
						if (LevelData.Objects != null)
							foreach (ObjectEntry item in LevelData.Objects)
								if (item.Y >= selection.Bottom * 128)
								{
									item.Y -= (short)(selection.Height * 128);
									item.UpdateSprite();
								}
					}
					if (CurrentTab == Tab.Background)
					{
						if (LevelData.BGHeight[bglayer] > selection.Height)
						{
							LevelData.ResizeBG(bglayer, LevelData.BGWidth[bglayer], LevelData.BGHeight[bglayer] - selection.Height);
							UpdateScrollBars();
							DrawLevel();
						}
					}
					else
					{
						if (LevelData.FGHeight > selection.Height)
						{
							LevelData.ResizeFG(LevelData.FGWidth, LevelData.FGHeight - selection.Height);
							UpdateScrollBars();
							DrawLevel();
						}
					}
				}
				else if (dlg.entireColumn.Checked)
				{
					ushort[][] layout;
					if (CurrentTab == Tab.Background)
						layout = LevelData.Background.layers[bglayer].layout;
					else
						layout = LevelData.Scene.layout;
					for (int y = 0; y < layout.Length; y++)
						for (int x = selection.Left; x < layout[y].Length - selection.Width; x++)
							layout[y][x] = layout[y][x + selection.Width];
					for (int y = 0; y < layout.Length; y++)
						for (int x = layout[y].Length - selection.Width; x < layout[y].Length; x++)
							layout[y][x] = 0;
					if (dlg.moveObjects.Checked)
					{
						if (LevelData.Objects != null)
							foreach (ObjectEntry item in LevelData.Objects)
								if (item.X >= selection.Right * 128)
								{
									item.X -= (short)(selection.Width * 128);
									item.UpdateSprite();
								}
					}
					if (CurrentTab == Tab.Background)
					{
						if (LevelData.BGWidth[bglayer] > selection.Width)
						{
							LevelData.ResizeBG(bglayer, LevelData.BGWidth[bglayer] - selection.Width, LevelData.BGHeight[bglayer]);
							UpdateScrollBars();
							DrawLevel();
						}
					}
					else
					{
						if (LevelData.FGWidth > selection.Width)
						{
							LevelData.ResizeFG(LevelData.FGWidth - selection.Width, LevelData.FGHeight);
							UpdateScrollBars();
							DrawLevel();
						}
					}
				}
				SaveState($"Delete {(CurrentTab == Tab.Background ? $"Background {bglayer + 1}" : "Foreground")}");
			}
		}

		private bool alignWall_common(int x, int y, bool top)
		{
			int cnkx = Math.DivRem(x, 128, out int blkx);
			int cnky = Math.DivRem(y, 128, out int blky);
			blkx = Math.DivRem(blkx, 16, out int colx);
			blky = Math.DivRem(blky, 16, out int coly);

			RSDKv3_4.Tiles128x128.Block.Tile blk = LevelData.NewChunks.chunkList[LevelData.Scene.layout[cnky][cnkx]].tiles[blky][blkx];
			RSDKv3_4.Tiles128x128.Block.Tile.Solidities solid;
			RSDKv3_4.TileConfig.CollisionMask mask;
			if (path2ToolStripMenuItem.Checked)
			{
				solid = blk.solidityB;
				mask = LevelData.Collision.collisionMasks[1][blk.tileIndex];
			}
			else
			{
				solid = blk.solidityA;
				mask = LevelData.Collision.collisionMasks[0][blk.tileIndex];
			}
			if (top)
				switch (solid)
				{
					case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop:
					case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone:
						return false;
				}
			else
				switch (solid)
				{
					case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop:
					case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip:
					case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone:
						return false;
				}
			var height = mask.heightMasks[colx];
			if (!height.solid)
				return false;
			if (mask.flipY)
				return coly <= height.height;
			else
				return coly >= height.height;
		}

		private void alignLeftWallToolStripButton_Click(object sender, EventArgs e)
		{
			foreach (Entry item in SelectedItems)
			{
				Rectangle bounds = item.Bounds;
				int x = bounds.Left - 1;
				int y = bounds.Top + (bounds.Height / 2);
				while (x > 0)
				{
					if (alignWall_common(x, y, false))
						break;
					x--;
				}
				item.X = (short)(x + 1 + (item.X - bounds.Left));
				item.UpdateSprite();
			}
			SelectedObjectChanged();
			ScrollToObject(SelectedItems[0]);
			DrawLevel();
			SaveState("Align Objects to Left Wall");
		}

		private void alignGroundToolStripButton_Click(object sender, EventArgs e)
		{
			foreach (Entry item in SelectedItems)
			{
				Rectangle bounds = item.Bounds;
				int x = bounds.Left + (bounds.Width / 2);
				int y = bounds.Bottom;
				while (y < LevelData.FGHeight * 128 - 1)
				{
					if (alignWall_common(x, y, true))
						break;
					y++;
				}
				item.Y = (short)(y + (item.Y - bounds.Bottom));
				item.UpdateSprite();
			}
			SelectedObjectChanged();
			ScrollToObject(SelectedItems[0]);
			DrawLevel();
			SaveState("Align Objects to Ground");
		}

		private void alignRightWallToolStripButton_Click(object sender, EventArgs e)
		{
			foreach (Entry item in SelectedItems)
			{
				Rectangle bounds = item.Bounds;
				int x = bounds.Right;
				int y = bounds.Top + (bounds.Height / 2);
				while (x < LevelData.FGWidth * 128 - 1)
				{
					if (alignWall_common(x, y, false))
						break;
					x++;
				}
				item.X = (short)(x + (item.X - bounds.Right));
				item.UpdateSprite();
			}
			SelectedObjectChanged();
			ScrollToObject(SelectedItems[0]);
			DrawLevel();
			SaveState("Align Objects to Right Wall");
		}

		private void alignCeilingToolStripButton_Click(object sender, EventArgs e)
		{
			foreach (Entry item in SelectedItems)
			{
				Rectangle bounds = item.Bounds;
				int x = bounds.Left + (bounds.Width / 2);
				int y = bounds.Top - 1;
				while (y > 0)
				{
					if (alignWall_common(x, y, false))
						break;
					y--;
				}
				item.Y = (short)(y + 1 + (item.Y - bounds.Top));
				item.UpdateSprite();
			}
			SelectedObjectChanged();
			ScrollToObject(SelectedItems[0]);
			DrawLevel();
			SaveState("Align Objects to Ceiling");
		}

		private void alignLeftsToolStripButton_Click(object sender, EventArgs e)
		{
			int left = int.MaxValue;
			foreach (Entry item in SelectedItems)
				left = Math.Min(left, item.Bounds.Left);
			foreach (Entry item in SelectedItems)
			{
				item.X = (short)(left + (item.X - item.Bounds.Left));
				item.UpdateSprite();
			}
			SelectedObjectChanged();
			ScrollToObject(SelectedItems[0]);
			DrawLevel();
			SaveState("Align Object Lefts");
		}

		private void alignCentersToolStripButton_Click(object sender, EventArgs e)
		{
			int left = int.MaxValue;
			int right = int.MinValue;
			foreach (Entry item in SelectedItems)
			{
				Rectangle bounds = item.Bounds;
				left = Math.Min(left, bounds.Left);
				right = Math.Max(right, bounds.Right);
			}
			int center = left + (right - left) / 2;
			foreach (Entry item in SelectedItems)
			{
				Rectangle bounds = item.Bounds;
				item.X = (short)(center + (item.X - (bounds.Left + (bounds.Width / 2))));
				item.UpdateSprite();
			}
			SelectedObjectChanged();
			ScrollToObject(SelectedItems[0]);
			DrawLevel();
			SaveState("Align Object Centers");
		}

		private void alignRightsToolStripButton_Click(object sender, EventArgs e)
		{
			int right = int.MinValue;
			foreach (Entry item in SelectedItems)
				right = Math.Max(right, item.Bounds.Right);
			foreach (Entry item in SelectedItems)
			{
				item.X = (short)(right + (item.X - item.Bounds.Right));
				item.UpdateSprite();
			}
			SelectedObjectChanged();
			ScrollToObject(SelectedItems[0]);
			DrawLevel();
			SaveState("Align Object Rights");
		}

		private void alignTopsToolStripButton_Click(object sender, EventArgs e)
		{
			int top = int.MaxValue;
			foreach (Entry item in SelectedItems)
				top = Math.Min(top, item.Bounds.Top);
			foreach (Entry item in SelectedItems)
			{
				item.Y = (short)(top + (item.Y - item.Bounds.Top));
				item.UpdateSprite();
			}
			SelectedObjectChanged();
			ScrollToObject(SelectedItems[0]);
			DrawLevel();
			SaveState("Align Object Tops");
		}

		private void alignMiddlesToolStripButton_Click(object sender, EventArgs e)
		{
			int top = int.MaxValue;
			int bottom = int.MinValue;
			foreach (Entry item in SelectedItems)
			{
				Rectangle bounds = item.Bounds;
				top = Math.Min(top, bounds.Top);
				bottom = Math.Max(bottom, bounds.Bottom);
			}
			int middle = top + (bottom - top) / 2;
			foreach (Entry item in SelectedItems)
			{
				Rectangle bounds = item.Bounds;
				item.Y = (short)(middle + (item.Y - (bounds.Top + (bounds.Height / 2))));
				item.UpdateSprite();
			}
			SelectedObjectChanged();
			ScrollToObject(SelectedItems[0]);
			DrawLevel();
			SaveState("Align Object Middles");
		}

		private void alignBottomsToolStripButton_Click(object sender, EventArgs e)
		{
			int bottom = int.MinValue;
			foreach (Entry item in SelectedItems)
				bottom = Math.Max(bottom, item.Bounds.Bottom);
			foreach (Entry item in SelectedItems)
			{
				item.Y = (short)(bottom + (item.Y - item.Bounds.Bottom));
				item.UpdateSprite();
			}
			SelectedObjectChanged();
			ScrollToObject(SelectedItems[0]);
			DrawLevel();
			SaveState("Align Object Bottoms");
		}

		private void objectsAboveHighPlaneToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			Settings.ObjectsAboveHighPlane = objectsAboveHighPlaneToolStripMenuItem.Checked;
		}

		private void lowToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			Settings.ViewLowPlane = lowToolStripMenuItem.Checked;
		}

		private void highToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			Settings.ViewHighPlane = highToolStripMenuItem.Checked;
		}

		private void objGridSizeDropDownButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			foreach (ToolStripMenuItem item in objGridSizeDropDownButton.DropDownItems)
				item.Checked = false;
			((ToolStripMenuItem)e.ClickedItem).Checked = true;
			objGridSizeDropDownButton.Text = "Grid Size: " + (1 << (ObjGrid = (byte)objGridSizeDropDownButton.DropDownItems.IndexOf(e.ClickedItem)));
			if (!loaded) return;
			DrawLevel();
		}

		private void ChunkSelector_ItemDrag(object sender, EventArgs e)
		{
			if (CurrentTab == Tab.Art && enableDraggingChunksButton.Checked)
				DoDragDrop(new DataObject("SonLVLChunkIndex_" + pid, ChunkSelector.SelectedIndex), DragDropEffects.Move);
		}

		bool chunk_dragdrop;
		int chunk_dragobj;
		Point chunk_dragpoint;
		private void ChunkSelector_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("SonLVLChunkIndex_" + pid))
			{
				e.Effect = DragDropEffects.Move;
				chunk_dragdrop = true;
				chunk_dragobj = (int)e.Data.GetData("SonLVLChunkIndex_" + pid);
				chunk_dragpoint = ChunkSelector.PointToClient(new Point(e.X, e.Y));
				ChunkSelector.Invalidate();
			}
			else
				chunk_dragdrop = false;
		}

		private void ChunkSelector_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("SonLVLChunkIndex_" + pid))
			{
				e.Effect = DragDropEffects.Move;
				chunk_dragdrop = true;
				chunk_dragobj = (int)e.Data.GetData("SonLVLChunkIndex_" + pid);
				chunk_dragpoint = ChunkSelector.PointToClient(new Point(e.X, e.Y));
				if (chunk_dragpoint.Y < 8)
					ChunkSelector.ScrollValue -= 8 - dragpoint.Y;
				else if (dragpoint.Y > ChunkSelector.Height - 8)
					ChunkSelector.ScrollValue += dragpoint.Y - (ChunkSelector.Height - 8);
				ChunkSelector.Invalidate();
			}
			else
				chunk_dragdrop = false;
		}

		private void ChunkSelector_DragLeave(object sender, EventArgs e)
		{
			chunk_dragdrop = false;
			ChunkSelector.Invalidate();
		}

		private void ChunkSelector_Paint(object sender, PaintEventArgs e)
		{
			if (chunk_dragdrop)
			{
				e.Graphics.DrawImage(ChunkSelector.Images[chunk_dragobj], chunk_dragpoint.X - (ChunkSelector.ImageWidth / 2),
					chunk_dragpoint.Y - (ChunkSelector.ImageHeight / 2), ChunkSelector.ImageWidth, ChunkSelector.ImageHeight);
				Rectangle r = ChunkSelector.GetItemBounds(ChunkSelector.GetItemAtPoint(chunk_dragpoint));
				if ((ModifierKeys & Keys.Control) == Keys.Control)
					e.Graphics.DrawRectangle(new Pen(Color.Black, 2), r);
				else
					e.Graphics.DrawLine(new Pen(Color.Black, 2), r.Left + 1, r.Top, r.Left + 1, r.Bottom);
			}
		}

		private void ChunkSelector_DragDrop(object sender, DragEventArgs e)
		{
			chunk_dragdrop = false;
			if (e.Data.GetDataPresent("SonLVLChunkIndex_" + pid))
			{
				Point clientPoint = ChunkSelector.PointToClient(new Point(e.X, e.Y));
				ushort newindex = (ushort)ChunkSelector.GetItemAtPoint(clientPoint);
				ushort oldindex = (ushort)(int)e.Data.GetData("SonLVLChunkIndex_" + pid);
				if (newindex == oldindex) return;
				if ((ModifierKeys & Keys.Control) == Keys.Control)
				{
					if (newindex == LevelData.NewChunks.chunkList.Length) return;
					LevelData.NewChunks.chunkList.Swap(oldindex, newindex);
					LevelData.ChunkSprites.Swap(oldindex, newindex);
					LevelData.ChunkBmps.Swap(oldindex, newindex);
					LevelData.ChunkColBmpBits.Swap(oldindex, newindex);
					LevelData.ChunkColBmps.Swap(oldindex, newindex);
					LevelData.CompChunkBmps.Swap(oldindex, newindex);
					LevelData.RemapLayouts((layout, x, y) =>
					{
						if (layout[y][x] == newindex)
							layout[y][x] = oldindex;
						else if (layout[y][x] == oldindex)
							layout[y][x] = newindex;
					});
					ChunkSelector.SelectedIndex = newindex;
				}
				else
				{
					if (newindex == oldindex + 1) return;
					LevelData.NewChunks.chunkList.Move(oldindex, newindex);
					LevelData.ChunkSprites.Move(oldindex, newindex);
					LevelData.ChunkBmps.Move(oldindex, newindex);
					LevelData.ChunkColBmpBits.Move(oldindex, newindex);
					LevelData.ChunkColBmps.Move(oldindex, newindex);
					LevelData.CompChunkBmps.Move(oldindex, newindex);
					LevelData.RemapLayouts((layout, x, y) =>
					{
						ushort c = layout[y][x];
						if (newindex > oldindex)
						{
							if (c == oldindex)
								layout[y][x] = (ushort)(newindex - 1);
							else if (c > oldindex && c < newindex)
								layout[y][x] = (ushort)(c - 1);
						}
						else
						{
							if (c == oldindex)
								layout[y][x] = newindex;
							else if (c >= newindex && c < oldindex)
								layout[y][x] = (ushort)(c + 1);
						}
					});
					if (newindex > oldindex)
						ChunkSelector.SelectedIndex = newindex - 1;
					else
						ChunkSelector.SelectedIndex = newindex;
				}
				SaveState($"{(ModifierKeys.HasFlag(Keys.Control) ? "Swap" : "Move")} Chunk");
			}
		}

		private void TileSelector_ItemDrag(object sender, EventArgs e)
		{
			if (enableDraggingTilesButton.Checked)
				DoDragDrop(new DataObject("SonLVLTileIndex_" + pid, TileSelector.SelectedIndex), DragDropEffects.Move);
		}

		bool tile_dragdrop;
		int tile_dragobj;
		Point tile_dragpoint;
		private void TileSelector_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("SonLVLTileIndex_" + pid))
			{
				e.Effect = DragDropEffects.Move;
				tile_dragdrop = true;
				tile_dragobj = (int)e.Data.GetData("SonLVLTileIndex_" + pid);
				tile_dragpoint = TileSelector.PointToClient(new Point(e.X, e.Y));
				TileSelector.Invalidate();
			}
			else
				tile_dragdrop = false;
		}

		private void TileSelector_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("SonLVLTileIndex_" + pid))
			{
				e.Effect = DragDropEffects.Move;
				tile_dragdrop = true;
				tile_dragobj = (int)e.Data.GetData("SonLVLTileIndex_" + pid);
				tile_dragpoint = TileSelector.PointToClient(new Point(e.X, e.Y));
				if (tile_dragpoint.Y < 8)
					TileSelector.ScrollValue -= 8 - dragpoint.Y;
				else if (dragpoint.Y > TileSelector.Height - 8)
					TileSelector.ScrollValue += dragpoint.Y - (TileSelector.Height - 8);
				TileSelector.Invalidate();
			}
			else
				tile_dragdrop = false;
		}

		private void TileSelector_DragLeave(object sender, EventArgs e)
		{
			tile_dragdrop = false;
			TileSelector.Invalidate();
		}

		private void TileSelector_Paint(object sender, PaintEventArgs e)
		{
			if (tile_dragdrop)
			{
				e.Graphics.DrawImage(TileSelector.Images[tile_dragobj], tile_dragpoint.X - (TileSelector.ImageWidth / 2),
					tile_dragpoint.Y - (TileSelector.ImageHeight / 2), TileSelector.ImageWidth, TileSelector.ImageHeight);
				Rectangle r = TileSelector.GetItemBounds(TileSelector.GetItemAtPoint(tile_dragpoint));
				if ((ModifierKeys & Keys.Control) == Keys.Control)
					e.Graphics.DrawRectangle(new Pen(Color.Black, 2), r);
				else
					e.Graphics.DrawLine(new Pen(Color.Black, 2), r.Left + 1, r.Top, r.Left + 1, r.Bottom);
			}
		}

		private void TileSelector_DragDrop(object sender, DragEventArgs e)
		{
			tile_dragdrop = false;
			if (e.Data.GetDataPresent("SonLVLTileIndex_" + pid))
			{
				Point clientPoint = TileSelector.PointToClient(new Point(e.X, e.Y));
				ushort newindex = (ushort)TileSelector.GetItemAtPoint(clientPoint);
				ushort oldindex = (ushort)(int)e.Data.GetData("SonLVLTileIndex_" + pid);
				if (newindex == oldindex) return;
				if ((ModifierKeys & Keys.Control) == Keys.Control)
				{
					if (newindex == TileSelector.Images.Count) return;
					LevelData.NewTiles.Swap(oldindex, newindex);
					LevelData.NewTileBmps.Swap(oldindex, newindex);
					LevelData.Collision.collisionMasks[0].Swap(oldindex, newindex);
					LevelData.Collision.collisionMasks[1].Swap(oldindex, newindex);
					LevelData.NewColBmpBits.Swap(oldindex, newindex);
					LevelData.NewColBmps.Swap(oldindex, newindex);
					for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
						for (int y = 0; y < 8; y++)
							for (int x = 0; x < 8; x++)
							{
								if (LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex == newindex)
									LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex = oldindex;
								else if (LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex == oldindex)
									LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex = newindex;
							}
					TileSelector.SelectedIndex = newindex;
				}
				else
				{
					if (newindex == oldindex + 1) return;
					if (newindex > oldindex) --newindex;
					LevelData.NewTiles.Move(oldindex, newindex);
					LevelData.NewTileBmps.Move(oldindex, newindex);
					LevelData.Collision.collisionMasks[0].Move(oldindex, newindex);
					LevelData.Collision.collisionMasks[1].Move(oldindex, newindex);
					LevelData.NewColBmpBits.Move(oldindex, newindex);
					LevelData.NewColBmps.Move(oldindex, newindex);
					for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
						for (int y = 0; y < 8; y++)
							for (int x = 0; x < 8; x++)
							{
								ushort t = LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex;
								if (newindex > oldindex)
								{
									if (t == oldindex)
										LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex = newindex;
									else if (t > oldindex && t <= newindex)
										LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex = (ushort)(t - 1);
								}
								else
								{
									if (t == oldindex)
										LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex = newindex;
									else if (t >= newindex && t < oldindex)
										LevelData.NewChunks.chunkList[i].tiles[y][x].tileIndex = (ushort)(t + 1);
								}
							}
					TileSelector.SelectedIndex = newindex;
				}
				SaveState($"{(ModifierKeys.HasFlag(Keys.Control) ? "Swap" : "Move")} Tile");
			}
		}

		private void remapChunksButton_Click(object sender, EventArgs e)
		{
			using (TileRemappingDialog dlg = new TileRemappingDialog("Chunks", LevelData.CompChunkBmps, 128, 128))
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					List<RSDKv3_4.Tiles128x128.Block> oldchunks = LevelData.NewChunks.chunkList.ToList();
					List<Sprite> oldchunkbmpbits = new List<Sprite>(LevelData.ChunkSprites);
					List<Bitmap[]> oldchunkbmps = new List<Bitmap[]>(LevelData.ChunkBmps);
					List<BitmapBits[]> oldchunkcolbmpbits = new List<BitmapBits[]>(LevelData.ChunkColBmpBits);
					List<Bitmap[]> oldchunkcolbmps = new List<Bitmap[]>(LevelData.ChunkColBmps);
					List<Bitmap> oldcompchunkbmps = new List<Bitmap>(LevelData.CompChunkBmps);
					Dictionary<ushort, ushort> bytedict = new Dictionary<ushort, ushort>(dlg.TileMap.Count);
					foreach (KeyValuePair<int, int> item in dlg.TileMap)
					{
						LevelData.NewChunks.chunkList[item.Value] = oldchunks[item.Key];
						LevelData.ChunkSprites[item.Value] = oldchunkbmpbits[item.Key];
						LevelData.ChunkBmps[item.Value] = oldchunkbmps[item.Key];
						LevelData.ChunkColBmpBits[item.Value] = oldchunkcolbmpbits[item.Key];
						LevelData.ChunkColBmps[item.Value] = oldchunkcolbmps[item.Key];
						LevelData.CompChunkBmps[item.Value] = oldcompchunkbmps[item.Key];
						bytedict.Add((ushort)item.Key, (ushort)item.Value);
					}
					LevelData.RemapLayouts((layout, x, y) =>
					{
						if (bytedict.ContainsKey(layout[y][x]))
							layout[y][x] = bytedict[layout[y][x]];
					});
					ChunkSelector.ChangeSize();
					ChunkSelector_SelectedIndexChanged(this, EventArgs.Empty);
					SaveState("Remap Chunks");
				}
		}

		private void remapTilesButton_Click(object sender, EventArgs e)
		{
			using (TileRemappingDialog dlg = new TileRemappingDialog("Tiles", TileSelector.Images, TileSelector.ImageWidth, TileSelector.ImageHeight))
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					List<BitmapBits> oldtiles = LevelData.NewTiles.ToList();
					List<Bitmap> oldimages = new List<Bitmap>(LevelData.NewTileBmps);
					List<RSDKv3_4.TileConfig.CollisionMask> oldmasks1 = new List<RSDKv3_4.TileConfig.CollisionMask>(LevelData.Collision.collisionMasks[0]);
					List<RSDKv3_4.TileConfig.CollisionMask> oldmasks2 = new List<RSDKv3_4.TileConfig.CollisionMask>(LevelData.Collision.collisionMasks[1]);
					List<BitmapBits[]> oldcolbmpbits = new List<BitmapBits[]>(LevelData.NewColBmpBits);
					List<Bitmap[]> oldcolbmps = new List<Bitmap[]>(LevelData.NewColBmps);
					Dictionary<ushort, ushort> ushortdict = new Dictionary<ushort, ushort>(dlg.TileMap.Count);
					foreach (KeyValuePair<int, int> item in dlg.TileMap)
					{
						LevelData.NewTiles[item.Value] = oldtiles[item.Key];
						LevelData.NewTileBmps[item.Value] = oldimages[item.Key];
						LevelData.Collision.collisionMasks[0][item.Value] = oldmasks1[item.Key];
						LevelData.Collision.collisionMasks[1][item.Value] = oldmasks2[item.Key];
						LevelData.NewColBmpBits[item.Value] = oldcolbmpbits[item.Key];
						LevelData.NewColBmps[item.Value] = oldcolbmps[item.Key];
						ushortdict.Add((ushort)item.Key, (ushort)item.Value);
					}
					for (int b = 0; b < LevelData.NewChunks.chunkList.Length; b++)
					{
						bool redraw = false;
						for (int y = 0; y < 8; y++)
							for (int x = 0; x < 8; x++)
								if (ushortdict.ContainsKey(LevelData.NewChunks.chunkList[b].tiles[y][x].tileIndex))
								{
									redraw = true;
									LevelData.NewChunks.chunkList[b].tiles[y][x].tileIndex = ushortdict[LevelData.NewChunks.chunkList[b].tiles[y][x].tileIndex];
								}
						if (redraw)
							LevelData.RedrawChunk(b);
					}
					TileSelector.ChangeSize();
					TileSelector_SelectedIndexChanged(this, EventArgs.Empty);
					SaveState("Remap Tiles");
				}
		}

		private void saveSectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (LayoutSectionNameDialog dlg = new LayoutSectionNameDialog())
			{
				dlg.Value = "Section " + (savedLayoutSections.Count + 1);

				if (CurrentTab == Tab.Foreground)
				{
					dlg.includeObjects.Visible = true;
					dlg.includeObjects.Checked = includeObjectsWithForegroundSelectionToolStripMenuItem.Checked;
				}

				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					LayoutSection sec = CreateLayoutSection(dlg.includeObjects.Checked);
					sec.Name = dlg.Value;
					savedLayoutSections.Add(sec);
					savedLayoutSectionImages.Add(MakeLayoutSectionImage(sec, true));
					layoutSectionListBox.Items.Add(sec.Name);
					layoutSectionListBox.SelectedIndex = savedLayoutSections.Count - 1;
					string levelname = this.levelname;
					foreach (char c in Path.GetInvalidFileNameChars())
						levelname = levelname.Replace(c, '_');
					using (FileStream fs = File.Create(LevelData.StageInfo.folder + ".sls"))
						new BinaryFormatter().Serialize(fs, savedLayoutSections);
				}
			}
		}

		private void layoutSectionListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (layoutSectionListBox.SelectedIndex == -1)
				layoutSectionPreview.Image = null;
			else
				layoutSectionPreview.Image = savedLayoutSectionImages[layoutSectionListBox.SelectedIndex];

			deleteToolStripButton.Enabled = (layoutSectionListBox.SelectedIndex != -1);

			DrawLevel();
		}

		private void layoutSectionListBox_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
				deleteToolStripButton_Click(this, EventArgs.Empty);
		}

		private void deleteToolStripButton_Click(object sender, EventArgs e)
		{
			if (layoutSectionListBox.SelectedIndex != -1 &&
				MessageBox.Show(this, "Are you sure you want to delete layout section \"" + savedLayoutSections[layoutSectionListBox.SelectedIndex].Name + "\"?", "SonLVL-RSDK", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				savedLayoutSections.RemoveAt(layoutSectionListBox.SelectedIndex);
				savedLayoutSectionImages.RemoveAt(layoutSectionListBox.SelectedIndex);
				layoutSectionListBox.Items.RemoveAt(layoutSectionListBox.SelectedIndex);
				string levelname = this.levelname;
				foreach (char c in Path.GetInvalidFileNameChars())
					levelname = levelname.Replace(c, '_');
				using (FileStream fs = File.Create(LevelData.StageInfo.folder + ".sls"))
					new BinaryFormatter().Serialize(fs, savedLayoutSections);
			}
		}

		private void pasteSectionOnceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PasteLayoutSectionOnce(savedLayoutSections[layoutSectionListBox.SelectedIndex]);
			SaveState("Paste Section Once");
		}

		private void pasteSectionRepeatingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PasteLayoutSectionRepeating(savedLayoutSections[layoutSectionListBox.SelectedIndex]);
			SaveState("Paste Section Repeating");
		}

		private void deepCopyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (CurrentArtTab)
			{
				case ArtTab.Chunks:
					DataObject d = new DataObject(typeof(ChunkCopyData).AssemblyQualifiedName, new ChunkCopyData(LevelData.NewChunks.chunkList[SelectedChunk]));
					d.SetImage(LevelData.ChunkSprites[SelectedChunk].GetBitmap().ToBitmap(LevelImgPalette));
					Clipboard.SetDataObject(d);
					break;
			}
		}

		private void flipTileHButton_Click(object sender, EventArgs e)
		{
			LevelData.NewTiles[SelectedTile].Flip(true, false);
			LevelData.RedrawBlock(SelectedTile, false);
			LevelData.Collision.collisionMasks[0][SelectedTile].Flip(true, false);
			LevelData.Collision.collisionMasks[1][SelectedTile].Flip(true, false);
			LevelData.RedrawCol(SelectedTile, true);
			chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
			TileSelector_SelectedIndexChanged(this, EventArgs.Empty);
			TileSelector.Invalidate();
			DrawColPicture();
			SaveState("Flip Tile Horiz");
		}

		private void flipTileVButton_Click(object sender, EventArgs e)
		{
			LevelData.NewTiles[SelectedTile].Flip(false, true);
			LevelData.RedrawBlock(SelectedTile, false);
			LevelData.Collision.collisionMasks[0][SelectedTile].Flip(false, true);
			LevelData.Collision.collisionMasks[1][SelectedTile].Flip(false, true);
			LevelData.RedrawCol(SelectedTile, true);
			chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
			collisionCeiling.Checked = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].flipY;
			TileSelector_SelectedIndexChanged(this, EventArgs.Empty);
			DrawTilePicture();
			TileSelector.Invalidate();
			DrawColPicture();
		}

		private void showBlockBehindCollisionCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			DrawColPicture();
		}

		private void pasteOverToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (CurrentArtTab)
			{
				case ArtTab.Chunks:
					if (Clipboard.ContainsData(typeof(ChunkCopyData).AssemblyQualifiedName))
					{
						ChunkCopyData cnkcpy = (ChunkCopyData)Clipboard.GetData(typeof(ChunkCopyData).AssemblyQualifiedName);
						Queue<ushort> freetiles = new Queue<ushort>(LevelData.GetFreeTiles());
						List<ushort> tileinds = new List<ushort>(cnkcpy.Tiles.Count);
						for (int i = 0; i < cnkcpy.Tiles.Count; i++)
						{
							BitmapBits tile = cnkcpy.Tiles[i];
							ushort ti = ushort.MaxValue;
							for (ushort j = 0; j < LevelData.NewTiles.Length; j++)
								if (tile.Bits.FastArrayEqual(LevelData.NewTiles[j].Bits)
									&& cnkcpy.Collision[i][0].Equal(LevelData.Collision.collisionMasks[0][j])
									&& cnkcpy.Collision[i][1].Equal(LevelData.Collision.collisionMasks[1][j]))
								{
									ti = j;
									break;
								}
							if (ti == ushort.MaxValue)
							{
								if (freetiles.Count == 0)
								{
									MessageBox.Show(this, "Level does not have enough free tiles.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
									return;
								}
								ti = freetiles.Dequeue();
								LevelData.NewTiles[ti] = new BitmapBits(tile);
								LevelData.Collision.collisionMasks[0][ti] = cnkcpy.Collision[i][0].Clone();
								LevelData.Collision.collisionMasks[1][ti] = cnkcpy.Collision[i][1].Clone();
								LevelData.RedrawBlock(ti, false);
								LevelData.RedrawCol(ti, false);
							}
							tileinds.Add(ti);
						}
						TileSelector.Invalidate();
						var cnk = cnkcpy.Chunk.Clone();
						for (int y = 0; y < 8; y++)
							for (int x = 0; x < 8; x++)
								cnk.tiles[y][x].tileIndex = tileinds[cnk.tiles[y][x].tileIndex];
						LevelData.NewChunks.chunkList[SelectedChunk] = cnk;
					}
					else
						LevelData.NewChunks.chunkList[SelectedChunk] = ((RSDKv3_4.Tiles128x128.Block)Clipboard.GetData(typeof(RSDKv3_4.Tiles128x128.Block).AssemblyQualifiedName)).Clone();
					LevelData.RedrawChunk(SelectedChunk);
					SaveState("Paste Over Chunk");
					break;
				case ArtTab.Tiles:
					TileCopyData copyData = (TileCopyData)Clipboard.GetData(typeof(TileCopyData).AssemblyQualifiedName);
					copyData.Bits.CopyTo(LevelData.NewTiles[SelectedTile].Bits, 0);
					LevelData.Collision.collisionMasks[0][SelectedTile] = copyData.Mask1;
					LevelData.Collision.collisionMasks[1][SelectedTile] = copyData.Mask2;
					LevelData.RedrawBlock(SelectedTile, false);
					LevelData.RedrawCol(SelectedTile, true);
					DrawChunkPicture();
					TileSelector_SelectedIndexChanged(this, EventArgs.Empty);
					SaveState("Paste Over Tile");
					break;
			}
		}

		private ColInfo[][,] ProcessColBmps(Bitmap colbmp1, Bitmap colbmp2, int w, int h)
		{
			ColInfo[,] coldata1;
			ColInfo[,] coldata2 = null;
			using (colbmp1)
			using (Bitmap tmp = new Bitmap(w, h))
			using (Graphics g = Graphics.FromImage(tmp))
			{
				g.SetOptions();
				g.DrawImage(colbmp1, 0, 0, colbmp1.Width, colbmp1.Height);
				coldata1 = LevelData.GetColMap(tmp);
			}
			Application.DoEvents();
			if (colbmp2 != null)
				using (colbmp2)
				using (Bitmap tmp = new Bitmap(w, h))
				using (Graphics g = Graphics.FromImage(tmp))
				{
					g.SetOptions();
					g.DrawImage(colbmp2, 0, 0, colbmp2.Width, colbmp2.Height);
					coldata2 = LevelData.GetColMap(tmp);
					Application.DoEvents();
				}
			return new ColInfo[2][,] { coldata1, coldata2 };
		}

		private void importToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog opendlg = new OpenFileDialog()
			{
				DefaultExt = "png",
				Filter = "Image Files|*.bmp;*.png;*.jpg;*.gif",
				RestoreDirectory = true
			})
				if (opendlg.ShowDialog(this) == DialogResult.OK)
					using (Bitmap bmp = new Bitmap(opendlg.FileName))
					{
						if (bmp.Width < 128 || bmp.Height < 128)
						{
							MessageBox.Show(this, $"The image you have selected is too small ({bmp.Width}x{bmp.Height}). It must be at least as large as one chunk (128x128)", "SonLVL-RSDK Layout Importer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
						Bitmap colbmp1 = null, colbmp2 = null, pribmp = null;
						string fmt = Path.Combine(Path.GetDirectoryName(opendlg.FileName),
							Path.GetFileNameWithoutExtension(opendlg.FileName) + "_{0}" + Path.GetExtension(opendlg.FileName));
						if (File.Exists(string.Format(fmt, "col1")))
						{
							colbmp1 = new Bitmap(string.Format(fmt, "col1"));
							if (File.Exists(string.Format(fmt, "col2")))
								colbmp2 = new Bitmap(string.Format(fmt, "col2"));
						}
						else if (File.Exists(string.Format(fmt, "col")))
							colbmp1 = new Bitmap(string.Format(fmt, "col"));
						if (File.Exists(string.Format(fmt, "pri")))
							pribmp = new Bitmap(string.Format(fmt, "pri"));
						ushort[,] section = new ushort[bmp.Width / 128, bmp.Height / 128];
						if (!ImportImage(bmp, colbmp1, colbmp2, pribmp, section))
							return;
						ushort[][] layout;
						int w, h;
						if (CurrentTab == Tab.Background)
						{
							layout = LevelData.Background.layers[bglayer].layout;
							w = Math.Min(section.GetLength(0), LevelData.BGWidth[bglayer] - menuLoc.X);
							h = Math.Min(section.GetLength(1), LevelData.BGHeight[bglayer] - menuLoc.Y);
						}
						else
						{
							layout = LevelData.Scene.layout;
							w = Math.Min(section.GetLength(0), LevelData.FGWidth - menuLoc.X);
							h = Math.Min(section.GetLength(1), LevelData.FGHeight - menuLoc.Y);
						}
						for (int y = 0; y < h; y++)
							for (int x = 0; x < w; x++)
								layout[y + menuLoc.Y][x + menuLoc.X] = section[x, y];
						SaveState($"Import {(CurrentTab == Tab.Background ? $"Background {bglayer + 1}" : "Foreground")}");
					}
		}

		private void importToolStripButton_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog opendlg = new OpenFileDialog()
			{
				DefaultExt = "png",
				Filter = "Image Files|*.bmp;*.png;*.jpg;*.gif",
				RestoreDirectory = true
			})
				if (opendlg.ShowDialog(this) == DialogResult.OK)
					using (Bitmap bmp = new Bitmap(opendlg.FileName))
					{
						if (bmp.Width < 128 || bmp.Height < 128)
						{
							MessageBox.Show(this, $"The image you have selected is too small ({bmp.Width}x{bmp.Height}). It must be at least as large as one chunk (128x128)", "SonLVL-RSDK Layout Section Importer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
						Bitmap colbmp1 = null, colbmp2 = null, pribmp = null;
						string fmt = Path.Combine(Path.GetDirectoryName(opendlg.FileName),
							Path.GetFileNameWithoutExtension(opendlg.FileName) + "_{0}" + Path.GetExtension(opendlg.FileName));
						if (File.Exists(string.Format(fmt, "col1")))
						{
							colbmp1 = new Bitmap(string.Format(fmt, "col1"));
							if (File.Exists(string.Format(fmt, "col2")))
								colbmp2 = new Bitmap(string.Format(fmt, "col2"));
						}
						else if (File.Exists(string.Format(fmt, "col")))
							colbmp1 = new Bitmap(string.Format(fmt, "col"));
						if (File.Exists(string.Format(fmt, "pri")))
							pribmp = new Bitmap(string.Format(fmt, "pri"));
						ushort[,] layout = new ushort[bmp.Width / 128, bmp.Height / 128];
						if (!ImportImage(bmp, colbmp1, colbmp2, pribmp, layout))
							return;
						LayoutSection section = new LayoutSection(layout, new List<Entry>());
						using (LayoutSectionNameDialog dlg = new LayoutSectionNameDialog() { Value = Path.GetFileNameWithoutExtension(opendlg.FileName) })
						{
							if (dlg.ShowDialog(this) == DialogResult.OK)
							{
								section.Name = dlg.Value;
								savedLayoutSections.Add(section);
								savedLayoutSectionImages.Add(MakeLayoutSectionImage(section, true));
								layoutSectionListBox.Items.Add(section.Name);
								layoutSectionListBox.SelectedIndex = savedLayoutSections.Count - 1;
								string levelname = this.levelname;
								foreach (char c in Path.GetInvalidFileNameChars())
									levelname = levelname.Replace(c, '_');
								using (FileStream fs = File.Create(LevelData.StageInfo.folder + ".sls"))
									new BinaryFormatter().Serialize(fs, savedLayoutSections);
							}
						}
						SaveState("Import Chunks");
					}
		}

		private void importProgressControl1_SizeChanged(object sender, EventArgs e)
		{
			importProgressControl1.Location = new Point((ClientSize.Width / 2) - (importProgressControl1.Width / 2), (ClientSize.Height / 2) - (importProgressControl1.Height / 2));
		}

		private void deleteUnusedTilesToolStripButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Are you sure you want to clear all tiles not used in chunks?", "Delete Unused Tiles", MessageBoxButtons.OKCancel) != DialogResult.OK)
				return;
			int numdel = 0;
			foreach (var i in Enumerable.Range(0, LevelData.NewTiles.Length).Select(a => (ushort)a).Except(LevelData.NewChunks.chunkList.SelectMany(a => a.tiles.SelectMany(b => b).Select(c => c.tileIndex))))
			{
				LevelData.NewTiles[i].Clear();
				LevelData.RedrawBlock(i, false);
				LevelData.Collision.collisionMasks[0][i] = new RSDKv3_4.TileConfig.CollisionMask();
				LevelData.Collision.collisionMasks[1][i] = new RSDKv3_4.TileConfig.CollisionMask();
				++numdel;
			}
			TileSelector.Invalidate();
			chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
			SaveState("Delete Unused Tiles");
			MessageBox.Show(this, $"Deleted {numdel} unused tiles.", "SonLVL-RSDK");

			drawTileToolStripButton.Enabled = importTilesToolStripButton.Enabled = LevelData.HasFreeTiles();
		}

		private void deleteUnusedChunksToolStripButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "This action may break other levels that share the same chunk set, or levels that alter the level layout dynamically.\n\nAre you sure you want to clear all chunks not used in the layout?", "Delete Unused Chunks", MessageBoxButtons.OKCancel) != DialogResult.OK)
				return;
			int numdel = 0;
			foreach (var i in Enumerable.Range(0, LevelData.NewChunks.chunkList.Length).Select(a => (ushort)a).Except(LevelData.Scene.layout.SelectMany(a => a).Union(LevelData.Background.layers.SelectMany(a => a.layout.SelectMany(b => b))).Union(LevelData.AdditionalScenes.SelectMany(a => a.Scene.layout.SelectMany(b => b)))))
			{
				LevelData.NewChunks.chunkList[i] = new RSDKv3_4.Tiles128x128.Block();
				LevelData.RedrawChunk(i);
				++numdel;
			}
			ChunkSelector.Invalidate();
			SaveState("Delete Unused Chunks");
			MessageBox.Show(this, $"Deleted {numdel} unused chunks.", "SonLVL-RSDK");

			drawChunkToolStripButton.Enabled = importChunksToolStripButton.Enabled = LevelData.HasFreeChunks();
		}

		private void clearForegroundToolStripButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Are you sure you want to clear the foreground layout?", "Clear Foreground", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				foreach (var row in LevelData.Scene.layout)
					Array.Clear(row, 0, row.Length);
				SaveState("Clear Foreground");
			}
		}

		private void clearBackgroundToolStripButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Are you sure you want to clear the selected background layer?", "Clear Background", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				foreach (var row in LevelData.Background.layers[bglayer].layout)
					Array.Clear(row, 0, row.Length);
				SaveState($"Clear Background {bglayer + 1}");
			}
		}

		private void calculateAngleButton_Click(object sender, EventArgs e)
		{
			RSDKv3_4.TileConfig.CollisionMask collisionMask = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile];
			collisionMask.CalcAngles();
			floorAngle.Value = collisionMask.floorAngle;
			rightAngle.Value = collisionMask.rWallAngle;
			ceilingAngle.Value = collisionMask.roofAngle;
			leftAngle.Value = collisionMask.lWallAngle;
			SaveState("Calculate Collision Angles");
		}

		private void copyCollisionSingleButton_Click(object sender, EventArgs e)
		{
			if (LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex ^ 1][SelectedTile].Equals(LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile])) return;

			LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex ^ 1][SelectedTile] = LevelData.Collision.collisionMasks[collisionLayerSelector.SelectedIndex][SelectedTile].Clone();
			LevelData.RedrawCol(SelectedTile, true);
			SaveState($"Copy Tile Collision to Path {(collisionLayerSelector.SelectedIndex ^ 1) + 1}");
		}

		private void usageCountsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (StatisticsDialog dlg = new StatisticsDialog())
				dlg.ShowDialog(this);
		}

		private void replaceForegroundToolStripButton_Click(object sender, EventArgs e)
		{
			if (replaceFGChunksDialog.ShowDialog(this) == DialogResult.OK)
			{
				ushort fc = (ushort)replaceFGChunksDialog.findChunk.Value;
				ushort rc = (ushort)replaceFGChunksDialog.replaceChunk.Value;
				int cnt = 0;
				for (int y = 0; y < LevelData.FGHeight; y++)
					for (int x = 0; x < LevelData.FGWidth; x++)
						if (LevelData.Scene.layout[y][x] == fc)
						{
							LevelData.Scene.layout[y][x] = rc;
							cnt++;
						}
				SaveState("Replace Foreground");
				DrawLevel();
				MessageBox.Show(this, $"Replaced {cnt} chunks.", "SonLVL-RSDK");
			}
		}

		private void replaceBackgroundToolStripButton_Click(object sender, EventArgs e)
		{
			if (replaceBGChunksDialog.ShowDialog(this) == DialogResult.OK)
			{
				ushort fc = (ushort)replaceBGChunksDialog.findChunk.Value;
				ushort rc = (ushort)replaceBGChunksDialog.replaceChunk.Value;
				int cnt = 0;
				for (int y = 0; y < LevelData.BGHeight[bglayer]; y++)
					for (int x = 0; x < LevelData.BGWidth[bglayer]; x++)
						if (LevelData.Background.layers[bglayer].layout[y][x] == fc)
						{
							LevelData.Background.layers[bglayer].layout[y][x] = rc;
							cnt++;
						}
				SaveState($"Replace Background {bglayer + 1}");
				DrawLevel();
				MessageBox.Show(this, $"Replaced {cnt} chunks.", "SonLVL-RSDK");
			}
		}

		private void replaceChunkBlocksToolStripButton_Click(object sender, EventArgs e)
		{
			if (replaceChunkBlocksDialog.ShowDialog(this) == DialogResult.OK)
			{
				var query = LevelData.NewChunks.chunkList.SelectMany((a, b) => a.tiles.SelectMany(c => c).Select(d => (b, d)));
				ushort? block = replaceChunkBlocksDialog.findBlock.Block;
				if (block.HasValue)
					query = query.Where(a => a.d.tileIndex == block.Value);
				bool? xflip = replaceChunkBlocksDialog.findBlock.XFlip;
				if (xflip.HasValue)
					query = query.Where(a => a.d.direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX) == xflip.Value);
				bool? yflip = replaceChunkBlocksDialog.findBlock.YFlip;
				if (yflip.HasValue)
					query = query.Where(a => a.d.direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY) == yflip.Value);
				RSDKv3_4.Tiles128x128.Block.Tile.Solidities? solid1 = replaceChunkBlocksDialog.findBlock.Solidity1;
				if (solid1.HasValue)
					query = query.Where(a => a.d.solidityA == solid1.Value);
				RSDKv3_4.Tiles128x128.Block.Tile.Solidities? solid2 = replaceChunkBlocksDialog.findBlock.Solidity2;
				if (solid2.HasValue)
					query = query.Where(a => a.d.solidityB == solid2.Value);
				RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes? plane = replaceChunkBlocksDialog.findBlock.Plane;
				if (plane.HasValue)
					query = query.Where(a => a.d.visualPlane == plane.Value);
				var list = query.ToList();
				block = replaceChunkBlocksDialog.replaceBlock.Block;
				xflip = replaceChunkBlocksDialog.replaceBlock.XFlip;
				yflip = replaceChunkBlocksDialog.replaceBlock.YFlip;
				solid1 = replaceChunkBlocksDialog.replaceBlock.Solidity1;
				solid2 = replaceChunkBlocksDialog.replaceBlock.Solidity2;
				plane = replaceChunkBlocksDialog.replaceBlock.Plane;
				foreach (RSDKv3_4.Tiles128x128.Block.Tile blk in list.Select(a => a.d))
				{
					if (block.HasValue)
						blk.tileIndex = block.Value;
					if (xflip.HasValue)
					{
						if (xflip.Value)
							blk.direction |= RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX;
						else
							blk.direction &= ~RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX;
					}
					if (yflip.HasValue)
					{
						if (yflip.Value)
							blk.direction |= RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY;
						else
							blk.direction &= ~RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY;
					}
					if (solid1.HasValue)
						blk.solidityA = solid1.Value;
					if (solid2.HasValue)
						blk.solidityB = solid2.Value;
					if (plane.HasValue)
						blk.visualPlane = plane.Value;
				}
				foreach (int i in list.Select(a => a.b).Distinct())
					LevelData.RedrawChunk(i);
				ChunkSelector.Invalidate();
				DrawChunkPicture();
				chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
				SaveState("Replace Chunk Blocks");
				MessageBox.Show(this, $"Replaced {list.Count} chunk blocks.", "SonLVL-RSDK");
			}
		}

		private void removeDuplicateChunksToolStripButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "This action may break other levels that share the same chunk set, or levels that alter the level layout dynamically.\n\nAre you sure you want to remove all duplicate chunks?", "SonLVL-RSDK", MessageBoxButtons.OKCancel) != DialogResult.OK)
				return;
			Dictionary<ushort, RSDKv3_4.Tiles128x128.Block> chunks = new Dictionary<ushort, RSDKv3_4.Tiles128x128.Block>(LevelData.NewChunks.chunkList.Length);
			Dictionary<ushort, ushort> chunkMap = new Dictionary<ushort, ushort>(LevelData.NewChunks.chunkList.Length);
			int deleted = 0;
			for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
			{
				var cnk = LevelData.NewChunks.chunkList[i];
				foreach (var item in chunks)
					if (cnk.Equal(item.Value))
					{
						chunkMap[(ushort)i] = item.Key;
						LevelData.NewChunks.chunkList[i] = new RSDKv3_4.Tiles128x128.Block();
						LevelData.RedrawChunk(i);
						deleted++;
						break;
					}
				if (!chunkMap.ContainsKey((ushort)i))
					chunks[(ushort)i] = cnk;
			}
			if (deleted > 0)
			{
				LevelData.RemapLayouts((layout, x, y) =>
				{
					if (chunkMap.ContainsKey(layout[y][x]))
						layout[y][x] = chunkMap[layout[y][x]];
				});
				DrawLevel();
				ChunkSelector.Invalidate();
				SaveState("Remove Duplicate Chunks");
			}
			MessageBox.Show(this, $"Removed {deleted} duplicate chunks.", "SonLVL-RSDK");
		}

		private void flipChunkBlocksHorizontallyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RSDKv3_4.Tiles128x128.Block chunk = LevelData.NewChunks.chunkList[SelectedChunk];
			for (int y = SelectedChunkBlock.Top; y < SelectedChunkBlock.Bottom; y++)
			{
				Array.Reverse(chunk.tiles[y], SelectedChunkBlock.X, SelectedChunkBlock.Width);
				for (int x = SelectedChunkBlock.Left; x < SelectedChunkBlock.Right; x++)
					chunk.tiles[y][x].direction ^= RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX;
			}
			LevelData.RedrawChunk(SelectedChunk);
			copiedChunkBlock = (chunkBlockEditor.SelectedObjects = GetSelectedChunkBlocks())[0];
			if (copiedChunkBlock.tileIndex < LevelData.NewTiles.Length)
				TileSelector.SelectedIndex = chunk.tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X].tileIndex;
			ChunkSelector.Invalidate();
			DrawChunkPicture();
			SaveState("Flip Chunk Blocks Horiz");
		}

		private void flipChunkBlocksVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RSDKv3_4.Tiles128x128.Block chunk = LevelData.NewChunks.chunkList[SelectedChunk];
			Array.Reverse(chunk.tiles, SelectedChunkBlock.Y, SelectedChunkBlock.Height);
			for (int y = SelectedChunkBlock.Top; y < SelectedChunkBlock.Bottom; y++)
				for (int x = SelectedChunkBlock.Left; x < SelectedChunkBlock.Right; x++)
					chunk.tiles[y][x].direction ^= RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY;
			LevelData.RedrawChunk(SelectedChunk);
			copiedChunkBlock = (chunkBlockEditor.SelectedObjects = GetSelectedChunkBlocks())[0];
			if (copiedChunkBlock.tileIndex < LevelData.NewTiles.Length)
				TileSelector.SelectedIndex = chunk.tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X].tileIndex;
			ChunkSelector.Invalidate();
			DrawChunkPicture();
			SaveState("Flip Chunk Blocks Vert");
		}

		private void copyChunkBlocksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RSDKv3_4.Tiles128x128.Block chunk = LevelData.NewChunks.chunkList[SelectedChunk];
			RSDKv3_4.Tiles128x128.Block.Tile[,] blocks = new RSDKv3_4.Tiles128x128.Block.Tile[SelectedChunkBlock.Width, SelectedChunkBlock.Height];
			for (int y = 0; y < SelectedChunkBlock.Height; y++)
				for (int x = 0; x < SelectedChunkBlock.Width; x++)
					blocks[x, y] = chunk.tiles[SelectedChunkBlock.Y + y][SelectedChunkBlock.X + x];
			Clipboard.SetData(typeof(RSDKv3_4.Tiles128x128.Block.Tile[,]).AssemblyQualifiedName, blocks);
		}

		private void pasteChunkBlocksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RSDKv3_4.Tiles128x128.Block chunk = LevelData.NewChunks.chunkList[SelectedChunk];
			RSDKv3_4.Tiles128x128.Block.Tile[,] blocks = (RSDKv3_4.Tiles128x128.Block.Tile[,])Clipboard.GetData(typeof(RSDKv3_4.Tiles128x128.Block.Tile[,]).AssemblyQualifiedName);
			for (int y = 0; y < Math.Min(blocks.GetLength(1), (128 / 16) - SelectedChunkBlock.Y); y++)
				for (int x = 0; x < Math.Min(blocks.GetLength(0), (128 / 16) - SelectedChunkBlock.X); x++)
					chunk.tiles[SelectedChunkBlock.Y + y][SelectedChunkBlock.X + x] = blocks[x, y].Clone();
			LevelData.RedrawChunk(SelectedChunk);
			copiedChunkBlock = (chunkBlockEditor.SelectedObjects = GetSelectedChunkBlocks())[0];
			if (copiedChunkBlock.tileIndex < LevelData.NewTiles.Length)
				TileSelector.SelectedIndex = chunk.tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X].tileIndex;
			ChunkSelector.Invalidate();
			DrawChunkPicture();
			SaveState("Paste Chunk Blocks");
		}

		private void clearChunkBlocksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RSDKv3_4.Tiles128x128.Block chunk = LevelData.NewChunks.chunkList[SelectedChunk];
			for (int y = 0; y < SelectedChunkBlock.Height; y++)
				for (int x = 0; x < SelectedChunkBlock.Width; x++)
					chunk.tiles[SelectedChunkBlock.Y + y][SelectedChunkBlock.X + x] = new RSDKv3_4.Tiles128x128.Block.Tile();
			LevelData.RedrawChunk(SelectedChunk);
			copiedChunkBlock = (chunkBlockEditor.SelectedObjects = GetSelectedChunkBlocks())[0];
			if (copiedChunkBlock.tileIndex < LevelData.NewTiles.Length)
				TileSelector.SelectedIndex = chunk.tiles[SelectedChunkBlock.Y][SelectedChunkBlock.X].tileIndex;
			ChunkSelector.Invalidate();
			DrawChunkPicture();
			SaveState("Clear Chunk Blocks");
		}

		private void ChunkID_ValueChanged(object sender, EventArgs e)
		{
			ChunkSelector.SelectedIndex = (int)ChunkID.Value;
		}

		private void TileID_ValueChanged(object sender, EventArgs e)
		{
			TileSelector.SelectedIndex = (int)TileID.Value;
		}

		private void ExportTileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ColorPalette pal;
			using (Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
				pal = bmp.Palette;
			LevelImgPalette.Entries.CopyTo(pal.Entries, 0);
			if (transparentBackgroundToolStripMenuItem.Checked)
				pal.Entries[0] = Color.Transparent;
			switch (CurrentArtTab)
			{
				case ArtTab.Chunks:
					if (!highToolStripMenuItem.Checked && !lowToolStripMenuItem.Checked && !path1ToolStripMenuItem.Checked && !path2ToolStripMenuItem.Checked)
					{
						MessageBox.Show(this, "Cannot export chunk with nothing visible.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					using (SaveFileDialog a = new SaveFileDialog() { FileName = (useHexadecimalIndexesToolStripMenuItem.Checked ? SelectedChunk.ToString("X2") : SelectedChunk.ToString()) + ".png", Filter = "PNG Images|*.png" })
						if (a.ShowDialog() == DialogResult.OK)
						{
							string pathBase = Path.ChangeExtension(a.FileName, null);
							if (exportArtcollisionpriorityToolStripMenuItem.Checked)
							{
								BitmapBits bits = new BitmapBits(128, 128);
								bits.DrawSprite(LevelData.ChunkSprites[SelectedChunk]);
								bits.ToBitmap(pal).Save(pathBase + ".png");
								LevelData.ChunkColBmpBits[SelectedChunk][0].ToBitmap4bpp(Color.Magenta, Color.White, Color.Yellow, Color.Black, Color.Red).Save(pathBase + "_col1.png");
								LevelData.ChunkColBmpBits[SelectedChunk][1].ToBitmap4bpp(Color.Magenta, Color.White, Color.Yellow, Color.Black, Color.Red).Save(pathBase + "_col2.png");
								bits = new BitmapBits(128, 128);
								for (int cy = 0; cy < 8; cy++)
									for (int cx = 0; cx < 8; cx++)
										if (LevelData.NewChunks.chunkList[SelectedChunk].tiles[cy][cx].visualPlane == RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High)
											bits.FillRectangle(1, cx * 16, cy * 16, 16, 16);
								bits.ToBitmap1bpp(Color.Black, Color.White).Save(pathBase + "_pri.png");
							}
							else
							{
								if (path1ToolStripMenuItem.Checked || path2ToolStripMenuItem.Checked)
								{
									BitmapBits32 bits = new BitmapBits32(128, 128);
									pal.Entries.CopyTo(bits.Palette, 0);
									bits.Clear(pal.Entries[0]);
									if (highToolStripMenuItem.Checked & lowToolStripMenuItem.Checked)
										bits.DrawSprite(LevelData.ChunkSprites[SelectedChunk]);
									else if (lowToolStripMenuItem.Checked)
										bits.DrawSpriteLow(LevelData.ChunkSprites[SelectedChunk]);
									else if (highToolStripMenuItem.Checked)
										bits.DrawSpriteHigh(LevelData.ChunkSprites[SelectedChunk]);

									bits.Palette[LevelData.ColorWhite] = Color.White;
									bits.Palette[LevelData.ColorYellow] = Color.Yellow;
									bits.Palette[LevelData.ColorBlack] = Color.Black;
									bits.Palette[LevelData.ColorRed] = Color.Red;

									if (path1ToolStripMenuItem.Checked)
										bits.DrawBitmap(LevelData.ChunkColBmpBits[SelectedChunk][0], 0, 0);
									else if (path2ToolStripMenuItem.Checked)
										bits.DrawBitmap(LevelData.ChunkColBmpBits[SelectedChunk][1], 0, 0);
									bits.ToBitmap().Save(pathBase + ".png");
								}
								else
								{
									BitmapBits bits = new BitmapBits(128, 128);
									if (highToolStripMenuItem.Checked & lowToolStripMenuItem.Checked)
										bits.DrawSprite(LevelData.ChunkSprites[SelectedChunk]);
									else if (lowToolStripMenuItem.Checked)
										bits.DrawSpriteLow(LevelData.ChunkSprites[SelectedChunk]);
									else if (highToolStripMenuItem.Checked)
										bits.DrawSpriteHigh(LevelData.ChunkSprites[SelectedChunk]);
									bits.ToBitmap(pal).Save(pathBase + ".png");
								}
							}
						}
					break;
				case ArtTab.Tiles:
					using (SaveFileDialog a = new SaveFileDialog() { FileName = (useHexadecimalIndexesToolStripMenuItem.Checked ? SelectedTile.ToString("X2") : SelectedTile.ToString()) + ".png", Filter = "PNG Images|*.png" })
						if (a.ShowDialog() == DialogResult.OK)
							LevelData.NewTiles[SelectedTile].ToBitmap(pal).Save(a.FileName);

					break;
			}
		}

		private void ExportLayoutSectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog a = new SaveFileDialog()
			{
				DefaultExt = "png",
				Filter = "PNG Files|*.png",
				RestoreDirectory = true
			})
				if (a.ShowDialog() == DialogResult.OK)
				{
					Rectangle area;
					switch (CurrentTab)
					{
						case Tab.Foreground:
							area = new Rectangle(FGSelection.X * 128, FGSelection.Y * 128, FGSelection.Width * 128, FGSelection.Height * 128);
							if (exportArtcollisionpriorityToolStripMenuItem.Checked)
							{
								string pathBase = Path.Combine(Path.GetDirectoryName(a.FileName), Path.GetFileNameWithoutExtension(a.FileName));
								string pathExt = Path.GetExtension(a.FileName);
								BitmapBits bmp = LevelData.DrawForegroundLayout(area);
								if (transparentBackgroundToolStripMenuItem.Checked)
									bmp.ReplaceColor(0, 160);
								Bitmap res = bmp.ToBitmap();
								ColorPalette pal = res.Palette;
								LevelImgPalette.Entries.CopyTo(pal.Entries, 0);
								if (transparentBackgroundToolStripMenuItem.Checked)
									pal.Entries[0] = Color.Transparent;
								res.Palette = pal;
								res.Save(a.FileName);
								LevelData.DrawForegroundCollision(area, 0).ToBitmap4bpp(Color.Magenta, Color.White, Color.Yellow, Color.Black, Color.Red).Save(pathBase + "_col1" + pathExt);
								LevelData.DrawForegroundCollision(area, 1).ToBitmap4bpp(Color.Magenta, Color.White, Color.Yellow, Color.Black, Color.Red).Save(pathBase + "_col2" + pathExt);
								bmp.Clear();
								for (int ly = FGSelection.Top; ly < FGSelection.Bottom; ly++)
									for (int lx = FGSelection.Left; lx < FGSelection.Right; lx++)
									{
										if (LevelData.Scene.layout[ly][lx] >= LevelData.NewChunks.chunkList.Length) continue;
										var cnk = LevelData.NewChunks.chunkList[LevelData.Scene.layout[ly][lx]];
										for (int cy = 0; cy < 8; cy++)
											for (int cx = 0; cx < 8; cx++)
												if (cnk.tiles[cy][cx].visualPlane == RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High)
													bmp.FillRectangle(1, lx * 128 + cx * 16 - area.Left, ly * 128 + cy * 16 - area.Top, 16, 16);
									}
								bmp.ToBitmap1bpp(Color.Black, Color.White).Save(pathBase + "_pri" + pathExt);
							}
							else
							{
								if (path1ToolStripMenuItem.Checked || path2ToolStripMenuItem.Checked)
								{
									BitmapBits32 bmp = LevelData.DrawForeground32(area, transparentBackgroundToolStripMenuItem.Checked ? Color.Transparent : LevelImgPalette.Entries[LevelData.ColorTransparent], includeobjectsWithFGToolStripMenuItem.Checked, !hideDebugObjectsToolStripMenuItem.Checked, objectsAboveHighPlaneToolStripMenuItem.Checked, lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked, path1ToolStripMenuItem.Checked, path2ToolStripMenuItem.Checked);
									using (Bitmap res = bmp.ToBitmap())
										res.Save(a.FileName);
								}
								else
								{
									BitmapBits bmp = LevelData.DrawForeground(area, includeobjectsWithFGToolStripMenuItem.Checked, !hideDebugObjectsToolStripMenuItem.Checked, objectsAboveHighPlaneToolStripMenuItem.Checked, lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked, path1ToolStripMenuItem.Checked, path2ToolStripMenuItem.Checked);
									Color[] palette;
									if (transparentBackgroundToolStripMenuItem.Checked)
									{
										palette = (Color[])LevelImgPalette.Entries.Clone();
										palette[0] = Color.Transparent;
									}
									else
										palette = LevelImgPalette.Entries;
									using (Bitmap res = bmp.ToBitmap(palette))
										res.Save(a.FileName);
								}
							}
							break;
						case Tab.Background:
							area = new Rectangle(BGSelection.X * 128, BGSelection.Y * 128, BGSelection.Width * 128, BGSelection.Height * 128);
							if (exportArtcollisionpriorityToolStripMenuItem.Checked)
							{
								string pathBase = Path.Combine(Path.GetDirectoryName(a.FileName), Path.GetFileNameWithoutExtension(a.FileName));
								string pathExt = Path.GetExtension(a.FileName);
								BitmapBits bmp = LevelData.DrawBackground(bglayer, area, true, true);
								if (transparentBackgroundToolStripMenuItem.Checked)
									bmp.ReplaceColor(0, 160);
								Bitmap res = bmp.ToBitmap();
								ColorPalette pal = res.Palette;
								LevelImgPalette.Entries.CopyTo(pal.Entries, 0);
								if (transparentBackgroundToolStripMenuItem.Checked)
									pal.Entries[0] = Color.Transparent;
								res.Palette = pal;
								res.Save(a.FileName);
								bmp.Clear();
								for (int ly = BGSelection.Top; ly < BGSelection.Bottom; ly++)
									for (int lx = BGSelection.Left; lx < BGSelection.Right; lx++)
									{
										if (LevelData.Background.layers[bglayer].layout[ly][lx] >= LevelData.NewChunks.chunkList.Length) continue;
										var cnk = LevelData.NewChunks.chunkList[LevelData.Background.layers[bglayer].layout[ly][lx]];
										for (int cy = 0; cy < 8; cy++)
											for (int cx = 0; cx < 8; cx++)
												if (cnk.tiles[cy][cx].visualPlane == RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High)
													bmp.FillRectangle(1, lx * 128 + cx * 16 - area.Left, ly * 128 + cy * 16 - area.Top, 16, 16);
									}
								bmp.ToBitmap1bpp(Color.Black, Color.White).Save(pathBase + "_pri" + pathExt);
							}
							else
							{
								if (path1ToolStripMenuItem.Checked || path2ToolStripMenuItem.Checked)
								{
									BitmapBits32 bmp = LevelData.DrawBackground32(bglayer, area, transparentBackgroundToolStripMenuItem.Checked ? Color.Transparent : LevelImgPalette.Entries[LevelData.ColorTransparent], lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
									using (Bitmap res = bmp.ToBitmap())
										res.Save(a.FileName);
								}
								else
								{
									BitmapBits bmp = LevelData.DrawBackground(bglayer, area, lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
									Color[] palette;
									if (transparentBackgroundToolStripMenuItem.Checked)
									{
										palette = (Color[])LevelImgPalette.Entries.Clone();
										palette[0] = Color.Transparent;
									}
									else
										palette = LevelImgPalette.Entries;
									using (Bitmap res = bmp.ToBitmap(palette))
										res.Save(a.FileName);
								}
							}
							break;
					}
				}
		}

		private void objectOrder_ItemDrag(object sender, ItemDragEventArgs e)
		{
			objectOrder.DoDragDrop(new DataObject("SonicRetro.SonLVLRSDK.GUI.ObjectIndex", e.Item), DragDropEffects.Move | DragDropEffects.Scroll);
		}

		private void objectOrder_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("SonicRetro.SonLVLRSDK.GUI.ObjectIndex"))
				e.Effect = e.AllowedEffect;
		}

		private void objectOrder_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("SonicRetro.SonLVLRSDK.GUI.ObjectIndex"))
				objectOrder.InsertionMark.Index = objectOrder.InsertionMark.NearestIndex(objectOrder.PointToClient(new Point(e.X, e.Y)));
		}

		private void objectOrder_DragLeave(object sender, EventArgs e)
		{
			objectOrder.InsertionMark.Index = -1;
		}

		private void objectOrder_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("SonicRetro.SonLVLRSDK.GUI.ObjectIndex"))
			{
				ListViewItem item = (ListViewItem)e.Data.GetData("SonicRetro.SonLVLRSDK.GUI.ObjectIndex");
				int src = objectOrder.Items.IndexOf(item);
				int dst = objectOrder.InsertionMark.Index;
				if (src != dst && dst != src + 1)
				{
					LevelData.Scene.entities.Move(src, dst);
					LevelData.Objects.Move(src, dst);
					objectOrder.BeginUpdate();
					ListViewItem item1 = (ListViewItem)item.Clone();
					objectOrder.Items.Insert(dst, item1);
					//if (item1.Index == src) LevelData.NewChunks.chunkList[700].Clone(); //..huh?
					objectOrder.Items.Remove(item);
					objectOrder.EndUpdate();
					item1.Selected = true;
				}
				SaveState("Change Object Order");
			}
		}

		private void objectOrder_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			if (objectOrder.SelectedIndices.Count > 0)
			{
				ObjectEntry item = LevelData.Objects[objectOrder.SelectedIndices[0]];
				if (!SelectedItems.Contains(item))
				{
					SelectedItems.Clear();
					SelectedItems.Add(item);
					SelectedObjectChanged();
					ScrollToObject(item);
					DrawLevel();
				}
			}
		}

		private void UpdateScrollControls()
		{
			scrollPreviewButton.Checked = false;
			if (LevelData.BGSize[bglayer].IsEmpty)
			{
				tabPage13.Hide();
				return;
			}
			else
				tabPage13.Show();
			layerScrollType.SelectedIndex = (int)LevelData.Background.layers[bglayer].type - 1;
			switch (LevelData.Background.layers[bglayer].type)
			{
				case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
				case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
					scrollPreviewButton.Enabled = scrollEditPanel.Enabled = true;
					layerParallaxFactor.Value = LevelData.Background.layers[bglayer].parallaxFactor / 256m;
					layerScrollSpeed.Value = LevelData.Background.layers[bglayer].scrollSpeed / 64m;
					scrollList.BeginUpdate();
					scrollList.Items.Clear();
					foreach (var item in LevelData.BGScroll[bglayer])
						scrollList.Items.Add(item.StartPos.ToString("X4"));
					scrollList.EndUpdate();
					scrollList.SelectedIndex = 0;
					break;
				default:
					scrollPreviewButton.Enabled = scrollEditPanel.Enabled = false;
					break;
			}
		}

		private void layerScrollType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			if ((RSDKv3_4.Backgrounds.Layer.LayerTypes)(layerScrollType.SelectedIndex + 1) == LevelData.Background.layers[bglayer].type) return;
			if (MessageBox.Show(this, "Changing scroll type will reset all scroll settings for the layer! Are you sure?", "SonLVL-RSDK", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				scrollPreviewButton.Checked = false;
				LevelData.Background.layers[bglayer].type = (RSDKv3_4.Backgrounds.Layer.LayerTypes)(layerScrollType.SelectedIndex + 1);
				LevelData.BGScroll[bglayer].Clear();
				switch (LevelData.Background.layers[bglayer].type)
				{
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
						LevelData.BGScroll[bglayer].Add(new ScrollData());
						break;
				}
				UpdateScrollControls();
				SaveState($"Change BG {bglayer + 1} Scroll Type");
			}
			else
				layerScrollType.SelectedIndex = (int)LevelData.Background.layers[bglayer].type - 1;
		}

		System.Threading.Tasks.Task bgscrolltask;
		System.Threading.CancellationTokenSource bgscrollcts;
		System.Threading.CancellationToken bgscrolltoken;
		private void scrollPreviewButton_CheckedChanged(object sender, EventArgs e)
		{
			if (scrollPreviewButton.Checked)
			{
				frametarget = (double)(System.Diagnostics.Stopwatch.Frequency / scrollTargetFPS.Value);
				layerscrollpos = 0;
				linescrollpos = new double[LevelData.BGScroll[bglayer].Count];
				scrolloff = new Point((int)scrollCamX.Value, (int)scrollCamY.Value);
				scrollEditPanel.Enabled = false;
				backgroundPanel.PanelGraphics.Clear(LevelImgPalette.Entries[LevelData.ColorTransparent]);
				backgroundPanel.FocusPanel();
				if (dspact == null)
					dspact = DrawScrollPreview;
				bgscrollcts = new System.Threading.CancellationTokenSource();
				bgscrolltoken = bgscrollcts.Token;
				bgscrolltask = System.Threading.Tasks.Task.Run(bgscrollfunc, bgscrolltoken);
				backgroundPanel.HScrollEnabled = backgroundPanel.VScrollEnabled = false;
			}
			else
			{
				bgscrollcts.Cancel();
				Application.DoEvents();
				bgscrolltask.Wait(16);
				switch (LevelData.Background.layers[bglayer].type)
				{
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
						scrollEditPanel.Enabled = true;
						break;
					default:
						scrollEditPanel.Enabled = false;
						break;
				}
				backgroundPanel.HScrollEnabled = backgroundPanel.VScrollEnabled = true;
				DrawLevel();
			}
		}

		static readonly double frametime = System.Diagnostics.Stopwatch.Frequency / 60.0;
		double frametarget = frametime;
		Point scrolloff;
		double layerscrollpos;
		double[] linescrollpos;
		private void bgscrollfunc()
		{
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			double overrun = 0;
			while (!bgscrolltoken.IsCancellationRequested)
			{
				overrun += stopwatch.ElapsedTicks - frametarget;
				stopwatch.Restart();
				double scrlmult = frametarget / frametime;
				if (overrun >= frametarget)
				{
					scrlmult *= 2;
					overrun -= frametarget;
				}
				layerscrollpos += LevelData.Background.layers[bglayer].scrollSpeed / (64d * scrlmult);
				for (int i = 0; i < linescrollpos.Length; i++)
					linescrollpos[i] += (double)LevelData.BGScroll[bglayer][i].ScrollSpeed * scrlmult;
				int widthpx = LevelData.BGWidth[bglayer] * 128;
				int heightpx = LevelData.BGHeight[bglayer] * 128;
				Point scrloff = scrolloff;
				switch (LevelData.Background.layers[bglayer].type)
				{
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
						int yoff = (int)(scrloff.Y * (LevelData.Background.layers[bglayer].parallaxFactor / 256d) + layerscrollpos) % heightpx;
						if (yoff < 0)
							yoff += heightpx;
						Rectangle rect = new Rectangle(0, yoff, widthpx, Math.Min((int)(backgroundPanel.PanelHeight / ZoomLevel), heightpx));
						BitmapBits32 tmpimg;
						if (rect.Bottom <= heightpx)
						{
							tmpimg = LevelData.DrawBackground32(bglayer, rect, LevelImgPalette.Entries[LevelData.ColorTransparent], lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
						}
						else
						{
							tmpimg = LevelData.DrawBackground32(bglayer, new Rectangle(0, 0, widthpx, heightpx), LevelImgPalette.Entries[LevelData.ColorTransparent], lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
							tmpimg.ScrollVertical(yoff);
							tmpimg = tmpimg.GetSection(0, 0, tmpimg.Width, rect.Height);
						}
						LevelImg8bpp = new BitmapBits32(Math.Min((int)(backgroundPanel.PanelWidth / ZoomLevel), widthpx), rect.Height);
						int[] linepos = new int[rect.Height];
						int scrind = LevelData.BGScroll[bglayer].FindLastIndex(a => a.StartPos <= yoff);
						int dstind = 0;
						while (dstind < rect.Height)
						{
							int pos = (int)(scrloff.X * (double)LevelData.BGScroll[bglayer][scrind].ParallaxFactor + linescrollpos[scrind]);
							int len;
							if (scrind == LevelData.BGScroll[bglayer].Count - 1)
							{
								len = heightpx - yoff;
								scrind = 0;
								yoff = 0;
							}
							else
							{
								len = LevelData.BGScroll[bglayer][scrind + 1].StartPos - LevelData.BGScroll[bglayer][scrind].StartPos - (yoff - LevelData.BGScroll[bglayer][scrind].StartPos);
								++scrind;
								yoff += len;
							}
							len = Math.Min(rect.Height - dstind, len);
							linepos.FastFill(pos, dstind, len);
							dstind += len;
						}
						tmpimg.ScrollHV(LevelImg8bpp, 0, 0, linepos);
						break;
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
						int xoff = (int)(scrloff.X * (LevelData.Background.layers[bglayer].parallaxFactor / 256d) + layerscrollpos) % widthpx;
						if (xoff < 0)
							xoff += widthpx;
						rect = new Rectangle(xoff, 0, Math.Min((int)(backgroundPanel.PanelWidth / ZoomLevel), widthpx), heightpx);
						if (rect.Right <= widthpx)
						{
							tmpimg = LevelData.DrawBackground32(bglayer, rect, LevelImgPalette.Entries[LevelData.ColorTransparent], lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
						}
						else
						{
							tmpimg = LevelData.DrawBackground32(bglayer, new Rectangle(0, 0, widthpx, rect.Height), LevelImgPalette.Entries[LevelData.ColorTransparent], lowToolStripMenuItem.Checked, highToolStripMenuItem.Checked);
							tmpimg.ScrollHorizontal(xoff);
							tmpimg = tmpimg.GetSection(0, 0, rect.Width, tmpimg.Height);
						}
						LevelImg8bpp = new BitmapBits32(rect.Width, Math.Min((int)(backgroundPanel.PanelHeight / ZoomLevel), heightpx));
						linepos = new int[rect.Width];
						scrind = LevelData.BGScroll[bglayer].FindLastIndex(a => a.StartPos <= xoff);
						dstind = 0;
						while (dstind < rect.Width)
						{
							int pos = (int)(scrloff.Y * (double)LevelData.BGScroll[bglayer][scrind].ParallaxFactor + linescrollpos[scrind]);
							int len;
							if (scrind == LevelData.BGScroll[bglayer].Count - 1)
							{
								len = widthpx - xoff;
								scrind = 0;
								xoff = 0;
							}
							else
							{
								len = LevelData.BGScroll[bglayer][scrind + 1].StartPos - LevelData.BGScroll[bglayer][scrind].StartPos - (xoff - LevelData.BGScroll[bglayer][scrind].StartPos);
								++scrind;
								xoff += len;
							}
							len = Math.Min(rect.Width - dstind, len);
							linepos.FastFill(pos, dstind, len);
							dstind += len;
						}
						tmpimg.ScrollVH(LevelImg8bpp, 0, 0, linepos);
						break;
				}
				LevelBmp = LevelImg8bpp.ToBitmap();
				Invoke(dspact);
				while (stopwatch.ElapsedTicks + overrun < frametarget)
					System.Threading.Thread.Sleep(1);
			}
		}

		Action dspact;
		private void DrawScrollPreview()
		{
			backgroundPanel.PanelGraphics.DrawImage(LevelBmp, 0, 0, (float)(LevelBmp.Width * ZoomLevel), (float)(LevelBmp.Height * ZoomLevel));
		}

		private void layerParallaxFactor_ValueChanged(object sender, EventArgs e)
		{
			LevelData.Background.layers[bglayer].parallaxFactor = (ushort)(layerParallaxFactor.Value * 256);
			if (scrollCamX.Value > 0 || scrollCamY.Value > 0)
				DrawLevel();
			SaveState($"Change BG {bglayer + 1} Parallax Factor");
		}

		private void layerScrollSpeed_ValueChanged(object sender, EventArgs e)
		{
			LevelData.Background.layers[bglayer].scrollSpeed = (byte)(layerScrollSpeed.Value * 64m);
			if (scrollFrame.Value > 0)
				DrawLevel();
			SaveState($"Change BG {bglayer + 1} Scroll Speed");
		}

		private void scrollList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (scrollList.SelectedIndex == -1) return;
			int max = 0;
			if (scrollList.SelectedIndex == LevelData.BGScroll[bglayer].Count - 1)
				switch (LevelData.Background.layers[bglayer].type)
				{
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
						max = LevelData.BGHeight[bglayer] * 128 - 1;
						break;
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
						max = LevelData.BGWidth[bglayer] * 128 - 1;
						break;
				}
			else
				max = LevelData.BGScroll[bglayer][scrollList.SelectedIndex + 1].StartPos - 1;
			addScrollButton.Enabled = LevelData.BGScroll[bglayer][scrollList.SelectedIndex].StartPos != max;
			if (scrollList.SelectedIndex > 0)
			{
				deleteScrollButton.Enabled = true;
				loaded = false;
				scrollOffset.Enabled = true;
				scrollOffset.Minimum = LevelData.BGScroll[bglayer][scrollList.SelectedIndex - 1].StartPos + 1;
				scrollOffset.Maximum = max;
				scrollOffset.Value = LevelData.BGScroll[bglayer][scrollList.SelectedIndex].StartPos;
				loaded = true;
			}
			else
			{
				deleteScrollButton.Enabled = false;
				loaded = false;
				scrollOffset.Enabled = false;
				scrollOffset.Minimum = 0;
				scrollOffset.Maximum = 0;
				loaded = true;
			}
			scrollEnableDeformation.Checked = LevelData.BGScroll[bglayer][scrollList.SelectedIndex].Deform;
			scrollParallaxFactor.Value = LevelData.BGScroll[bglayer][scrollList.SelectedIndex].ParallaxFactor;
			scrollScrollSpeed.Value = LevelData.BGScroll[bglayer][scrollList.SelectedIndex].ScrollSpeed;

			if (LevelData.BGScroll[bglayer].Count > 0 && showScrollAreas.Checked)
				DrawLevel();
		}

		private void addScrollButton_Click(object sender, EventArgs e)
		{
			int max = 0;
			if (scrollList.SelectedIndex == LevelData.BGScroll[bglayer].Count - 1)
				switch (LevelData.Background.layers[bglayer].type)
				{
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
						max = LevelData.BGHeight[bglayer] * 128 - 1;
						break;
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
						max = LevelData.BGWidth[bglayer] * 128 - 1;
						break;
				}
			else
				max = LevelData.BGScroll[bglayer][scrollList.SelectedIndex + 1].StartPos - 1;
			if (LevelData.BGScroll[bglayer][scrollList.SelectedIndex].StartPos != max - 1)
				max = LevelData.BGScroll[bglayer][scrollList.SelectedIndex].StartPos + ((max - LevelData.BGScroll[bglayer][scrollList.SelectedIndex].StartPos) / 2);
			LevelData.BGScroll[bglayer].Insert(scrollList.SelectedIndex + 1, new ScrollData((ushort)max));
			scrollList.Items.Insert(scrollList.SelectedIndex + 1, max.ToString("X4"));
			scrollList.SelectedIndex++;
			SaveState("Insert Scroll Line");
		}

		private void deleteScrollButton_Click(object sender, EventArgs e)
		{
			int ind = scrollList.SelectedIndex;
			LevelData.BGScroll[bglayer].RemoveAt(ind);
			scrollList.Items.RemoveAt(ind);
			if (ind == scrollList.Items.Count)
				--ind;
			scrollList.SelectedIndex = ind;
			SaveState("Delete Scroll Line");
		}

		private void scrollOffset_ValueChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.BGScroll[bglayer][scrollList.SelectedIndex].StartPos = (ushort)scrollOffset.Value;
			scrollList.Items[scrollList.SelectedIndex] = LevelData.BGScroll[bglayer][scrollList.SelectedIndex].StartPos.ToString("X4");
			DrawLevel();
			SaveState("Change Scroll Line Offset");
		}

		private void scrollEnableDeformation_CheckedChanged(object sender, EventArgs e)
		{
			LevelData.BGScroll[bglayer][scrollList.SelectedIndex].Deform = scrollEnableDeformation.Checked;
			SaveState("Change Scroll Line Deform");
		}

		private void scrollParallaxFactor_ValueChanged(object sender, EventArgs e)
		{
			LevelData.BGScroll[bglayer][scrollList.SelectedIndex].ParallaxFactor = scrollParallaxFactor.Value;
			if (scrollCamX.Value > 0 || scrollCamY.Value > 0)
				DrawLevel();
			SaveState("Change Scroll Line Parallax Factor");
		}

		private void scrollScrollSpeed_ValueChanged(object sender, EventArgs e)
		{
			LevelData.BGScroll[bglayer][scrollList.SelectedIndex].ScrollSpeed = scrollScrollSpeed.Value;
			if (scrollFrame.Value > 0)
				DrawLevel();
			SaveState("Change Scroll Line Speed");
		}

		private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControl3.SelectedIndex != 2)
				scrollPreviewButton.Checked = false;
			else
			{
				if (!loaded || LevelData.BGSize[bglayer].IsEmpty)
					tabPage13.Hide();
				else
					tabPage13.Show();
			}
			DrawLevel();
		}

		private void showScrollAreas_CheckedChanged(object sender, EventArgs e)
		{
			DrawLevel();
		}

		private void scrollCamX_ValueChanged(object sender, EventArgs e)
		{
			DrawLevel();
		}

		private void scrollCamY_ValueChanged(object sender, EventArgs e)
		{
			DrawLevel();
		}

		private void scrollFrame_ValueChanged(object sender, EventArgs e)
		{
			DrawLevel();
		}

		private void levelNameBox_TextChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			loaded = false;
			int selst = levelNameBox.SelectionStart;
			int selend = levelNameBox.SelectionStart + levelNameBox.SelectionLength;
			System.Text.StringBuilder text = new System.Text.StringBuilder(levelNameBox.Text);
			bool modified = false;
			for (int i = 0; i < text.Length; i++)
				if (text[i] != ' ' && (text[i] < 'A' || text[i] > 'Z'))
				{
					if (selst > i)
						--selst;
					if (selend > i)
						--selend;
					text.Remove(i--, 1);
					modified = true;
				}
			if (modified)
			{
				levelNameBox.Text = text.ToString();
				levelNameBox.SelectionStart = selst;
				levelNameBox.SelectionLength = selend - selst;
			}
			levelNameBox2.MaxLength = 254 - text.Length;
			if (levelNameBox2.TextLength > 0)
			{
				text.Append('-');
				text.Append(levelNameBox2.Text);
			}
			LevelData.Scene.title = text.ToString();
			loaded = true;
			SaveState("Change Level Name");
		}

		private void levelNameBox2_TextChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			loaded = false;
			if (levelNameBox2.TextLength == 0)
			{
				LevelData.Scene.title = levelNameBox.Text;
				levelNameBox.MaxLength = 255;
				loaded = true;
				return;
			}
			int selst = levelNameBox2.SelectionStart;
			int selend = levelNameBox2.SelectionStart + levelNameBox2.SelectionLength;
			System.Text.StringBuilder text = new System.Text.StringBuilder(levelNameBox2.Text);
			bool modified = false;
			for (int i = 0; i < text.Length; i++)
				if (text[i] != ' ' && (text[i] < 'A' || text[i] > 'Z'))
				{
					if (selst > i)
						--selst;
					if (selend > i)
						--selend;
					text.Remove(i--, 1);
					modified = true;
				}
			if (modified)
			{
				levelNameBox2.Text = text.ToString();
				levelNameBox2.SelectionStart = selst;
				levelNameBox2.SelectionLength = selend - selst;
				if (levelNameBox2.TextLength == 0)
				{
					LevelData.Scene.title = levelNameBox.Text;
					levelNameBox.MaxLength = 255;
					loaded = true;
					return;
				}
			}
			levelNameBox.MaxLength = 254 - text.Length;
			if (levelNameBox2.TextLength > 0)
			{
				text.Insert(0, '-');
				text.Insert(0, levelNameBox.Text);
			}
			LevelData.Scene.title = text.ToString();
			loaded = true;
			SaveState("Change Level Name");
		}

		private void midpointBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.Scene.layerMidpoint = (RSDKv3_4.Scene.LayerMidpoints)midpointBox.SelectedIndex;
			SaveState("Change Layer Midpoint");
		}

		private void layer0Box_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.Scene.activeLayer0 = (RSDKv3_4.Scene.ActiveLayers)layer0Box.SelectedIndex;
			SaveState("Change Active Layer 0");
		}

		private void layer1Box_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.Scene.activeLayer1 = (RSDKv3_4.Scene.ActiveLayers)layer1Box.SelectedIndex;
			SaveState("Change Active Layer 1");
		}

		private void layer2Box_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.Scene.activeLayer2 = (RSDKv3_4.Scene.ActiveLayers)layer2Box.SelectedIndex;
			SaveState("Change Active Layer 2");
		}

		private void layer3Box_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.Scene.activeLayer3 = (RSDKv3_4.Scene.ActiveLayers)layer3Box.SelectedIndex;
			SaveState("Change Active Layer 3");
		}

		private void foregroundDeformation_CheckedChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.ForegroundDeformation = foregroundDeformation.Checked;
			SaveState("Toggle Foreground Deformation");
		}

		private void loadGlobalObjects_CheckedChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			bool reload = false;
			if (loadGlobalObjects.Checked)
			{
				switch (MessageBox.Show(this, "Enabling global objects will cause all the object types currently in the level to be shifted.\n\nDo you want to adjust the types of all the entities in the level to match?", "Enable Global Objects", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3))
				{
					case DialogResult.Yes:
						foreach (var item in LevelData.Objects)
							if (item.Type > 0)
								item.Type += (byte)LevelData.GlobalObjects.Count;
						foreach (var astg in LevelData.AdditionalScenes)
							foreach (var item in astg.Scene.entities)
								if (item.type > 0)
									item.type += (byte)LevelData.GlobalObjects.Count;
						break;
					case DialogResult.No:
						reload = true;
						break;
					default:
						loaded = false;
						loadGlobalObjects.Checked = false;
						loaded = true;
						return;
				}
				LevelData.ObjTypes.InsertRange(1, LevelData.GlobalObjects.Select(o => LevelData.MakeObjectDefinition(o)));
			}
			else
			{
				switch (MessageBox.Show(this, "Disabling global objects will cause all the object types currently in the level to be shifted.\n\nDo you want to adjust the types of all the entities in the level to match and delete entities using global types?", "Disable Global Objects", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3))
				{
					case DialogResult.Yes:
						List<ObjectEntry> todelete = new List<ObjectEntry>();
						foreach (var item in LevelData.Objects)
							if (item.Type > 0)
							{
								if (item.Type > LevelData.GlobalObjects.Count + 1)
									item.Type -= (byte)LevelData.GlobalObjects.Count;
								else
									todelete.Add(item);
							}
						foreach (var item in todelete)
						{
							LevelData.DeleteObject(item);
							SelectedItems.Remove(item);
						}
						foreach (var astg in LevelData.AdditionalScenes)
							for (int i = 0; i < astg.Scene.entities.Count; i++)
								if (astg.Scene.entities[i].type > 0)
									if (astg.Scene.entities[i].type > LevelData.GlobalObjects.Count + 1)
										astg.Scene.entities[i].type -= (byte)LevelData.GlobalObjects.Count;
									else
										astg.Scene.entities.RemoveAt(i--);
						break;
					case DialogResult.No:
						reload = true;
						break;
					default:
						loaded = false;
						loadGlobalObjects.Checked = true;
						loaded = true;
						return;
				}
				LevelData.ObjTypes.RemoveRange(1, LevelData.GlobalObjects.Count);
			}
			LevelData.StageConfig.loadGlobalObjects = loadGlobalObjects.Checked;
			InitObjectTypes();
			if (reload)
				foreach (var item in LevelData.Objects)
					item.UpdateSprite();
			SelectedObjectChanged();
			objectAddButton.Enabled = LevelData.ObjTypes.Count < 256;
			SaveState("Change Load Global Objects");
		}

		private void objectListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			if (objectListBox.SelectedIndex == -1)
			{
				objectDeleteButton.Enabled = false;
				objectNameBox.Enabled = false;
				objectScriptBox.Enabled = false;
				browseScriptButton.Enabled = false;
				objectUpButton.Enabled = false;
				objectDownButton.Enabled = false;
				objectTypeID.Text = "Object Type ID:";
			}
			else
			{
				objectDeleteButton.Enabled = true;
				objectNameBox.Enabled = true;
				objectScriptBox.Enabled = true;
				browseScriptButton.Enabled = true;
				objectUpButton.Enabled = objectListBox.SelectedIndex > 0;
				objectDownButton.Enabled = objectListBox.SelectedIndex < LevelData.StageConfig.objects.Count - 1;
				objectTypeID.Text = $"Object Type ID: {objectListBox.SelectedIndex + (LevelData.StageConfig.loadGlobalObjects ? LevelData.GameConfig.objects.Count : 0) + 1}";
				loaded = false;
				objectNameBox.Text = LevelData.StageConfig.objects[objectListBox.SelectedIndex].name;
				objectScriptBox.Text = LevelData.StageConfig.objects[objectListBox.SelectedIndex].script;
				loaded = true;
			}
		}

		private void objectAddButton_Click(object sender, EventArgs e)
		{
			var info = new RSDKv3_4.GameConfig.ObjectInfo();
			LevelData.StageConfig.objects.Add(info);
			var def = LevelData.MakeObjectDefinition(info);
			LevelData.ObjTypes.Add(def);
			Bitmap image = def.Image.GetBitmap().ToBitmap(LevelData.BmpPal);
			ObjectSelect.imageList1.Images.Add(image.Resize(ObjectSelect.imageList1.ImageSize));
			objectTypeImages.Images.Add(image.Resize(objectTypeImages.ImageSize));
			if (!def.Hidden)
			{
				objectTypeListMap.Add(LevelData.ObjTypes.Count - 1, objectTypeList.Items.Count);
				ObjectSelect.listView1.Items.Add(new ListViewItem(def.Name, ObjectSelect.imageList1.Images.Count - 1) { Tag = (byte)(LevelData.ObjTypes.Count - 1) });
				objectTypeList.Items.Add(new ListViewItem(def.Name, objectTypeImages.Images.Count - 1) { Tag = (byte)(LevelData.ObjTypes.Count - 1) });
			}
			objectListBox.Items.Add(info.name);
			objectListBox.SelectedIndex = objectListBox.Items.Count - 1;
			objectAddButton.Enabled = LevelData.ObjTypes.Count < 256;
			SaveState("Add Object Type");
		}

		private void objectDeleteButton_Click(object sender, EventArgs e)
		{
			bool reload = false;
			byte idx = (byte)(objectListBox.SelectedIndex + 1);
			if (LevelData.StageConfig.loadGlobalObjects)
				idx += (byte)LevelData.GlobalObjects.Count;
			switch (MessageBox.Show(this, "Do you want to adjust the types of all the entities in the level and delete entities using this type?", "Delete Object Type", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3))
			{
				case DialogResult.Yes:
					List<ObjectEntry> todelete = new List<ObjectEntry>();
					foreach (var item in LevelData.Objects)
						switch (Math.Sign(item.Type.CompareTo(idx)))
						{
							case 1:
								item.Type--;
								break;
							case 0:
								todelete.Add(item);
								break;
						}
					foreach (var item in todelete)
					{
						LevelData.DeleteObject(item);
						SelectedItems.Remove(item);
					}
					foreach (var astg in LevelData.AdditionalScenes)
						for (int i = 0; i < astg.Scene.entities.Count; i++)
							switch (Math.Sign(astg.Scene.entities[i].type.CompareTo(idx)))
							{
								case 1:
									astg.Scene.entities[i].type--;
									break;
								case 0:
									astg.Scene.entities.RemoveAt(i--);
									break;
							}
					break;
				case DialogResult.No:
					reload = true;
					break;
				default:
					return;
			}
			LevelData.StageConfig.objects.RemoveAt(objectListBox.SelectedIndex);
			LevelData.ObjTypes.RemoveAt(idx);
			InitObjectTypes();
			if (reload)
				foreach (var item in LevelData.Objects)
					if (item.Type >= idx)
						item.UpdateSprite();
			SelectedObjectChanged();
			objectListBox.Items.RemoveAt(objectListBox.SelectedIndex);
			objectAddButton.Enabled = LevelData.ObjTypes.Count < 256;
			SaveState("Delete Object Type");
		}

		private void objectUpButton_Click(object sender, EventArgs e)
		{
			if (!loaded) return;
			byte idx = (byte)(objectListBox.SelectedIndex + 1);
			if (LevelData.StageConfig.loadGlobalObjects)
				idx += (byte)LevelData.GlobalObjects.Count;
			foreach (var item in LevelData.Objects)
			{
				if (item.Type == idx)
					item.Type--;
				else if (item.Type == idx - 1)
					item.Type++;
			}
			LevelData.StageConfig.objects.Swap(objectListBox.SelectedIndex, objectListBox.SelectedIndex - 1);
			LevelData.ObjTypes.Swap(idx, idx - 1);
			InitObjectTypes();
			SelectedObjectChanged();
			loaded = false;
			objectListBox.MoveSelectionUp();
			loaded = true;
			objectListBox_SelectedIndexChanged(this, EventArgs.Empty);
			SaveState("Swap Object Type Up");
		}

		private void objectDownButton_Click(object sender, EventArgs e)
		{
			if (!loaded) return;
			byte idx = (byte)(objectListBox.SelectedIndex + 1);
			if (LevelData.StageConfig.loadGlobalObjects)
				idx += (byte)LevelData.GlobalObjects.Count;
			foreach (var item in LevelData.Objects)
			{
				if (item.Type == idx)
					item.Type++;
				else if (item.Type == idx + 1)
					item.Type--;
			}
			LevelData.StageConfig.objects.Swap(objectListBox.SelectedIndex, objectListBox.SelectedIndex + 1);
			LevelData.ObjTypes.Swap(idx, idx + 1);
			InitObjectTypes();
			SelectedObjectChanged();
			loaded = false;
			objectListBox.MoveSelectionDown();
			loaded = true;
			objectListBox_SelectedIndexChanged(this, EventArgs.Empty);
			SaveState("Swap Object Type Down");
		}

		private void objectNameBox_TextChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			var info = LevelData.StageConfig.objects[objectListBox.SelectedIndex];
			info.name = objectNameBox.Text;
			byte idx = (byte)(objectListBox.SelectedIndex + 1);
			if (LevelData.StageConfig.loadGlobalObjects)
				idx += (byte)LevelData.GlobalObjects.Count;
			LevelData.GetObjectDefinition(idx).Init(info);
			if (objectTypeListMap.TryGetValue(idx, out var idx2))
			{
				ObjectSelect.listView1.Items[idx2].Text = objectNameBox.Text;
				objectTypeList.Items[idx2].Text = objectNameBox.Text;
			}
			loaded = false;
			objectListBox.Items[objectListBox.SelectedIndex] = objectNameBox.Text;
			loaded = true;
			for (int i = 0; i < LevelData.Objects.Count; i++)
				if (LevelData.Objects[i].Type == idx)
					objectOrder.Items[i].Text = objectNameBox.Text;
			SaveState("Change Object Name");
		}

		private void objectScriptBox_TextChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			var info = LevelData.StageConfig.objects[objectListBox.SelectedIndex];
			info.script = objectScriptBox.Text;
			byte idx = (byte)(objectListBox.SelectedIndex + 1);
			if (LevelData.StageConfig.loadGlobalObjects)
				idx += (byte)LevelData.GlobalObjects.Count;
			var def = LevelData.MakeObjectDefinition(info);
			LevelData.ObjTypes[idx] = def;
			InitObjectTypes();
			foreach (var item in LevelData.Objects)
				if (item.Type == idx)
					item.UpdateSprite();
			SelectedObjectChanged();
			SaveState("Change Object Script");
		}

		private void browseScriptButton_Click(object sender, EventArgs e)
		{
			using (FileSelectDialog dlg = new FileSelectDialog("Scripts", scriptFiles))
				if (dlg.ShowDialog(this) == DialogResult.OK)
					objectScriptBox.Text = dlg.SelectedPath;
		}

		private void sfxListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			if (sfxListBox.SelectedIndex == -1)
			{
				sfxDeleteButton.Enabled = false;
				sfxNameBox.Enabled = false;
				sfxFileBox.Enabled = false;
				sfxBrowseButton.Enabled = false;
				sfxUpButton.Enabled = false;
				sfxDownButton.Enabled = false;
				sfxID.Text = "Sound ID:";
			}
			else
			{
				sfxDeleteButton.Enabled = true;
				sfxNameBox.Enabled = LevelData.Game.RSDKVer == EngineVersion.V4;
				sfxFileBox.Enabled = true;
				sfxBrowseButton.Enabled = true;
				sfxUpButton.Enabled = sfxListBox.SelectedIndex > 0;
				sfxDownButton.Enabled = sfxListBox.SelectedIndex < LevelData.StageConfig.soundFX.Count - 1;
				sfxID.Text = $"Sound ID: {sfxListBox.SelectedIndex + (LevelData.Game.RSDKVer == EngineVersion.V3 ? 0 : LevelData.GameConfig.soundFX.Count)}";
				loaded = false;
				sfxNameBox.Text = LevelData.StageConfig.soundFX[sfxListBox.SelectedIndex].name;
				sfxFileBox.Text = LevelData.StageConfig.soundFX[sfxListBox.SelectedIndex].path;
				loaded = true;
			}
		}

		private void sfxAddButton_Click(object sender, EventArgs e)
		{
			var info = new RSDKv3_4.GameConfig.SoundInfo();
			LevelData.StageConfig.soundFX.Add(info);
			sfxListBox.Items.Add(info.name);
			sfxListBox.SelectedIndex = sfxListBox.Items.Count - 1;
			sfxAddButton.Enabled = LevelData.StageConfig.soundFX.Count < 255;
			SaveState("Add Sound Effect");
		}

		private void sfxDeleteButton_Click(object sender, EventArgs e)
		{
			LevelData.StageConfig.soundFX.RemoveAt(sfxListBox.SelectedIndex);
			sfxListBox.Items.RemoveAt(sfxListBox.SelectedIndex);
			sfxAddButton.Enabled = LevelData.StageConfig.soundFX.Count < 255;
			SaveState("Delete Sound Effect");
		}

		private void sfxNameBox_TextChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.StageConfig.soundFX[sfxListBox.SelectedIndex].name = sfxNameBox.Text;
			loaded = false;
			sfxListBox.Items[sfxListBox.SelectedIndex] = sfxNameBox.Text;
			loaded = true;
			SaveState("Change Sound Effect Name");
		}

		private void sfxFileBox_TextChanged(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.StageConfig.soundFX[sfxListBox.SelectedIndex].path = sfxFileBox.Text;
			if (LevelData.Game.RSDKVer != EngineVersion.V4)
				sfxNameBox.Text = Path.GetFileNameWithoutExtension(sfxFileBox.Text);
			SaveState("Change Sound Effect File");
		}

		private void sfxBrowseButton_Click(object sender, EventArgs e)
		{
			using (FileSelectDialog dlg = new FileSelectDialog("Sound Effects", sfxFiles))
				if (dlg.ShowDialog(this) == DialogResult.OK)
					sfxFileBox.Text = dlg.SelectedPath;
		}

		private void sfxUpButton_Click(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.StageConfig.soundFX.Swap(sfxListBox.SelectedIndex, sfxListBox.SelectedIndex - 1);
			loaded = false;
			sfxListBox.MoveSelectionUp();
			loaded = true;
			sfxListBox_SelectedIndexChanged(this, EventArgs.Empty);
			SaveState("Swap Sound Effect Up");
		}
		private void sfxDownButton_Click(object sender, EventArgs e)
		{
			if (!loaded) return;
			LevelData.StageConfig.soundFX.Swap(sfxListBox.SelectedIndex, sfxListBox.SelectedIndex + 1);
			loaded = false;
			sfxListBox.MoveSelectionDown();
			loaded = true;
			sfxListBox_SelectedIndexChanged(this, EventArgs.Empty);
			SaveState("Swap Sound Effect Down");
		}

		private void copyCollisionAllButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Are you sure you want to replace all of Path 2's collision with Path 1's?", "SonLVL-RSDK", MessageBoxButtons.OKCancel) != DialogResult.OK)
				return;
			
			var redrawblocks = new SortedSet<int>();
			for (int i = 0; i < LevelData.Collision.collisionMasks[0].Length; i++)
			{
				if (!LevelData.Collision.collisionMasks[0][i].Equal(LevelData.Collision.collisionMasks[1][i]))
				{
					LevelData.Collision.collisionMasks[1][i] = LevelData.Collision.collisionMasks[0][i].Clone();

					LevelData.RedrawCol(i, false);
					redrawblocks.Add(i);
				}
			}

			for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
			{
				bool redraw = false;
				foreach (RSDKv3_4.Tiles128x128.Block.Tile tile in LevelData.NewChunks.chunkList[i].tiles.SelectMany(a => a))
				{
					redraw |= redrawblocks.Contains(tile.tileIndex) || (tile.solidityA != tile.solidityB);
					tile.solidityB = tile.solidityA;
				}

				if (redraw)
				{
					LevelData.RedrawChunk(i);
					if (i == SelectedChunk)
						DrawChunkPicture();
				}
			}

			SaveState("Copy Path 1 Collision to Path 2");
		}

		private void gotoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (GoToDialog dlg = new GoToDialog())
			{
				dlg.entityPos.Maximum = 31 + LevelData.Objects.Count;
				dlg.entityCountLabel.Text = $"/ {dlg.entityPos.Maximum}";

				dlg.gotoEntity.Enabled = (CurrentTab == Tab.Objects) && (LevelData.Objects.Count > 0);
				Size levelsize = (CurrentTab == Tab.Background) ? LevelData.BGSize[bglayer] : LevelData.FGSize;

				dlg.xpos.Maximum = levelsize.Width * 128;
				dlg.widthLabel.Text = $"/ {dlg.xpos.Maximum}";

				dlg.ypos.Maximum = levelsize.Height * 128;
				dlg.heightLabel.Text = $"/ {dlg.ypos.Maximum}";

				ScrollingPanel panel;
				switch (CurrentTab)
				{
					case Tab.Objects:
						panel = objectPanel;
						break;
					case Tab.Foreground:
						panel = foregroundPanel;
						break;
					case Tab.Background:
						panel = backgroundPanel;
						break;
					default:
						return;
				}

				dlg.xpos.Value = Math.Max(0, panel.HScrollValue);
				dlg.ypos.Value = Math.Max(0, panel.VScrollValue);

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					if (dlg.gotoEntity.Checked)
					{
						SelectedItems.Clear();
						SelectedItems.Add(LevelData.Objects[(int)dlg.entityPos.Value - 32]);
						SelectedObjectChanged();
						ScrollToObject(SelectedItems[0]);
					}
					else if (dlg.gotoPosition.Checked)
					{
						panel.HScrollValue = (int)Math.Max(panel.HScrollMinimum, Math.Min(panel.HScrollMaximum - panel.HScrollLargeChange + 1, (int)dlg.xpos.Value));
						panel.VScrollValue = (int)Math.Max(panel.VScrollMinimum, Math.Min(panel.VScrollMaximum - panel.VScrollLargeChange + 1, (int)dlg.ypos.Value));
					}
				}
			}
		}

		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop) && (((string[])e.Data.GetData(DataFormats.FileDrop))[0].ToLower().EndsWith(".ini")))
				e.Effect = DragDropEffects.Copy;
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			loaded = false;
			LoadINI(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
		}

		private void removeDuplicateTilesToolStripButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Are you sure you want to remove all duplicate tiles?", "SonLVL-RSDK", MessageBoxButtons.OKCancel) != DialogResult.OK)
				return;
			Dictionary<ushort, TileData> tiles = new Dictionary<ushort, TileData>(LevelData.NewTiles.Length);
			Dictionary<ushort, RSDKv3_4.Tiles128x128.Block.Tile> tileMap = new Dictionary<ushort, RSDKv3_4.Tiles128x128.Block.Tile>(LevelData.NewTiles.Length);
			int deleted = 0;
			for (ushort i = 0; i < LevelData.NewTiles.Length; i++)
			{
				TileData data = new TileData(LevelData.NewTiles[i], LevelData.Collision.collisionMasks[0][i], LevelData.Collision.collisionMasks[1][i]);
				TileData datah = data.Clone();
				datah.Flip(true, false);
				TileData datav = data.Clone();
				datav.Flip(false, true);
				TileData datahv = datav.Clone();
				datahv.Flip(true, false);
				foreach (var item in tiles)
				{
					if (data.Equals(item.Value))
					{
						tileMap[i] = new RSDKv3_4.Tiles128x128.Block.Tile() { tileIndex = item.Key };
						LevelData.NewTiles[i].Clear();
						LevelData.RedrawBlock(i, false);
						LevelData.Collision.collisionMasks[0][i] = new RSDKv3_4.TileConfig.CollisionMask();
						LevelData.Collision.collisionMasks[1][i] = new RSDKv3_4.TileConfig.CollisionMask();
						LevelData.RedrawCol(i, false);
						deleted++;
						break;
					}
					if (datah.Equals(item.Value))
					{
						tileMap[i] = new RSDKv3_4.Tiles128x128.Block.Tile() { tileIndex = item.Key, direction = RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX };
						LevelData.NewTiles[i].Clear();
						LevelData.RedrawBlock(i, false);
						LevelData.Collision.collisionMasks[0][i] = new RSDKv3_4.TileConfig.CollisionMask();
						LevelData.Collision.collisionMasks[1][i] = new RSDKv3_4.TileConfig.CollisionMask();
						LevelData.RedrawCol(i, false);
						deleted++;
						break;
					}
					if (datav.Equals(item.Value))
					{
						tileMap[i] = new RSDKv3_4.Tiles128x128.Block.Tile() { tileIndex = item.Key, direction = RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY };
						LevelData.NewTiles[i].Clear();
						LevelData.RedrawBlock(i, false);
						LevelData.Collision.collisionMasks[0][i] = new RSDKv3_4.TileConfig.CollisionMask();
						LevelData.Collision.collisionMasks[1][i] = new RSDKv3_4.TileConfig.CollisionMask();
						LevelData.RedrawCol(i, false);
						deleted++;
						break;
					}
					if (datahv.Equals(item.Value))
					{
						tileMap[i] = new RSDKv3_4.Tiles128x128.Block.Tile() { tileIndex = item.Key, direction = RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipXY };
						LevelData.NewTiles[i].Clear();
						LevelData.RedrawBlock(i, false);
						LevelData.Collision.collisionMasks[0][i] = new RSDKv3_4.TileConfig.CollisionMask();
						LevelData.Collision.collisionMasks[1][i] = new RSDKv3_4.TileConfig.CollisionMask();
						LevelData.RedrawCol(i, false);
						deleted++;
						break;
					}
				}
				if (!tileMap.ContainsKey(i))
					tiles[i] = data;
			}
			if (deleted > 0)
			{
				for (int i = 0; i < LevelData.NewChunks.chunkList.Length; i++)
					foreach (RSDKv3_4.Tiles128x128.Block.Tile cb in LevelData.NewChunks.chunkList[i].tiles.SelectMany(a => a))
						if (tileMap.ContainsKey(cb.tileIndex))
						{
							RSDKv3_4.Tiles128x128.Block.Tile nb = tileMap[cb.tileIndex];
							cb.tileIndex = nb.tileIndex;
							cb.direction ^= nb.direction;
						}
				chunkBlockEditor.SelectedObjects = chunkBlockEditor.SelectedObjects;
				DrawLevel();
				TileSelector.Invalidate();
				SaveState("Remove Duplicate Tiles");
			}
			MessageBox.Show(this, $"Removed {deleted} duplicate tiles.", "SonLVL-RSDK");
		}

		private void importOverToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog opendlg = new OpenFileDialog())
			{
				opendlg.DefaultExt = "png";
				opendlg.Filter = "Image Files|*.bmp;*.png;*.jpg;*.gif";
				opendlg.RestoreDirectory = true;
				if (opendlg.ShowDialog(this) == DialogResult.OK)
				{
					BitmapInfo bmpi;
					using (Bitmap bmp = new Bitmap(opendlg.FileName))
						bmpi = new BitmapInfo(bmp);
					switch (CurrentArtTab)
					{
						case ArtTab.Chunks:
							if (bmpi.Width < 128 || bmpi.Height < 128)
							{
								MessageBox.Show(this, "Image must be at least 128x128 to import chunk.", "SonLVL-RSDK");
								return;
							}
							break;
						case ArtTab.Tiles:
							if (bmpi.Width < 16 || bmpi.Height < 16)
							{
								MessageBox.Show(this, "Image must be at least 16x16 to import tile.", "SonLVL-RSDK");
								return;
							}
							break;
					}
					ImportResult res = LevelData.BitmapToTiles(bmpi, new bool[bmpi.Width / 16, bmpi.Height / 16], null, null, new List<BitmapBits>(), null, false, Application.DoEvents);
					List<int> editedTiles = new List<int>();
					switch (CurrentArtTab)
					{
						case ArtTab.Chunks:
							RSDKv3_4.Tiles128x128.Block cnk = LevelData.NewChunks.chunkList[SelectedChunk];
							for (int by = 0; by < 8; by++)
								for (int bx = 0; bx < 8; bx++)
								{
									RSDKv3_4.Tiles128x128.Block.Tile blk = cnk.tiles[by][bx];
									if (!editedTiles.Contains(blk.tileIndex))
									{
										LevelData.NewTiles[blk.tileIndex] = new BitmapBits(res.Art[res.Mappings[bx, by].tileIndex]);
										LevelData.NewTiles[blk.tileIndex].Flip(blk.direction);
										editedTiles.Add(blk.tileIndex);
									}
								}
							break;
						case ArtTab.Tiles:
							LevelData.NewTiles[SelectedTile] = res.Art[res.Mappings[0, 0].tileIndex];
							editedTiles.Add(SelectedTile);
							break;
					}
					TileSelector.Invalidate();
					LevelData.RedrawBlocks(editedTiles, true);
					if (editedTiles.Contains(SelectedTile))
						TileSelector_SelectedIndexChanged(this, EventArgs.Empty);
					DrawChunkPicture();
					ChunkSelector.Invalidate();
					SaveState($"Import Over {(CurrentArtTab == ArtTab.Chunks ? "Chunk" : "Tile")}");
				}
			}
		}

		private void drawOverToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (DrawTileDialog dlg = new DrawTileDialog())
			{
				switch (CurrentArtTab)
				{
					case ArtTab.Chunks:
						dlg.tile = new BitmapBits(128, 128);
						dlg.tile.DrawSprite(LevelData.ChunkSprites[SelectedChunk]);
						dlg.numericUpDown1.Value = 2;
						break;
					case ArtTab.Tiles:
						dlg.tile = new BitmapBits(LevelData.NewTiles[SelectedTile]);
						dlg.numericUpDown1.Value = 16;
						break;
				}
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					BitmapInfo bmpi = new BitmapInfo(dlg.tile.ToBitmap(LevelData.NewPalette));
					ImportResult res = LevelData.BitmapToTiles(bmpi, new bool[bmpi.Width / 16, bmpi.Height / 16], null, null, new List<BitmapBits>(), null, false, Application.DoEvents);
					List<int> editedTiles = new List<int>();
					switch (CurrentArtTab)
					{
						case ArtTab.Chunks:
							RSDKv3_4.Tiles128x128.Block cnk = LevelData.NewChunks.chunkList[SelectedChunk];
							for (int by = 0; by < 8; by++)
								for (int bx = 0; bx < 8; bx++)
								{
									RSDKv3_4.Tiles128x128.Block.Tile blk = cnk.tiles[by][bx];
									if (!editedTiles.Contains(blk.tileIndex))
									{
										LevelData.NewTiles[blk.tileIndex] = new BitmapBits(res.Art[res.Mappings[bx, by].tileIndex]);
										LevelData.NewTiles[blk.tileIndex].Flip(blk.direction);
										editedTiles.Add(blk.tileIndex);
									}
								}
							break;
						case ArtTab.Tiles:
							LevelData.NewTiles[SelectedTile] = res.Art[res.Mappings[0, 0].tileIndex];
							editedTiles.Add(SelectedTile);
							break;
					}
					TileSelector.Invalidate();
					LevelData.RedrawBlocks(editedTiles, true);
					if (editedTiles.Contains(SelectedTile))
						TileSelector_SelectedIndexChanged(this, EventArgs.Empty);
					DrawChunkPicture();
					ChunkSelector.Invalidate();
					SaveState($"Import Over {(CurrentArtTab == ArtTab.Chunks ? "Chunk" : "Tile")}");
				}
			}
		}

		private void TileSelector_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (loaded && e.Button == MouseButtons.Left)
			{
				foreach (RSDKv3_4.Tiles128x128.Block.Tile til in GetSelectedChunkBlocks())
					til.tileIndex = (ushort)TileSelector.SelectedIndex;
				SaveState("Change Chunk Blocks Index");
			}
		}
	}

	[Serializable]
	public class LayoutSection
	{
		public string Name { get; set; }
		public ushort[,] Layout { get; set; }
		public List<Entry> Objects { get; set; }

		public LayoutSection(ushort[,] layout, List<Entry> objects)
		{
			Layout = layout;
			Objects = objects;
		}
	}

	[Serializable]
	public class TileCopyData
	{
		public byte[] Bits { get; set; }
		public RSDKv3_4.TileConfig.CollisionMask Mask1 { get; set; }
		public RSDKv3_4.TileConfig.CollisionMask Mask2 { get; set; }

		public TileCopyData(BitmapBits bitmap, RSDKv3_4.TileConfig.CollisionMask mask1, RSDKv3_4.TileConfig.CollisionMask mask2)
		{
			Bits = new BitmapBits(bitmap).Bits;
			Mask1 = mask1.Clone();
			Mask2 = mask2.Clone();
		}
	}

	[Serializable]
	public class ChunkCopyData
	{
		public List<BitmapBits> Tiles { get; set; }
		public List<RSDKv3_4.TileConfig.CollisionMask[]> Collision { get; set; }
		public RSDKv3_4.Tiles128x128.Block Chunk { get; set; }

		public ChunkCopyData(RSDKv3_4.Tiles128x128.Block chunk)
		{
			Chunk = chunk.Clone();
			List<ushort> blocks = new List<ushort>();
			Collision = new List<RSDKv3_4.TileConfig.CollisionMask[]>();
			for (int y = 0; y < 8; y++)
				for (int x = 0; x < 8; x++)
					if (chunk.tiles[y][x].tileIndex < LevelData.NewTiles.Length)
					{
						int i = blocks.IndexOf(chunk.tiles[y][x].tileIndex);
						if (i == -1)
						{
							i = blocks.Count;
							blocks.Add(chunk.tiles[y][x].tileIndex);
						}
						Chunk.tiles[y][x].tileIndex = (ushort)i;
					}
			Tiles = blocks.Select(a => LevelData.NewTiles[a]).ToList();
			Collision = blocks.Select(a => new[] { LevelData.Collision.collisionMasks[0][a], LevelData.Collision.collisionMasks[1][a] }).ToList();
		}
	}

	class HUDImage
	{
		public Sprite Image { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		public HUDImage(BitmapBits image)
		{
			Image = new Sprite(image);
			Width = image.Width;
			Height = image.Height;
		}
	}

	class ListViewIndexComparer : System.Collections.IComparer
	{
		public int Compare(object x, object y)
		{
			return ((ListViewItem)x).Index - ((ListViewItem)y).Index;
		}
	}
}
