namespace SonicRetro.SonLVL.GUI
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
            System.Windows.Forms.ToolStrip chunkListToolStrip;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ToolStrip tileListToolStrip;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Panel panel11;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.Panel panel9;
            System.Windows.Forms.Label label26;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
            System.Windows.Forms.Label label27;
            System.Windows.Forms.ToolStrip layoutSectionListToolStrip;
            System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
            System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
            System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
            System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
            System.Windows.Forms.ToolStripMenuItem toolStripMenuItem17;
            System.Windows.Forms.ToolStripMenuItem toolStripMenuItem18;
            System.Windows.Forms.ToolStripMenuItem toolStripMenuItem19;
            System.Windows.Forms.ToolStripMenuItem toolStripMenuItem20;
            this.importChunksToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.drawChunkToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteUnusedChunksToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.removeDuplicateChunksToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.replaceChunkBlocksToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.remapChunksButton = new System.Windows.Forms.ToolStripButton();
            this.enableDraggingChunksButton = new System.Windows.Forms.ToolStripButton();
            this.importTilesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.drawTileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteUnusedTilesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.removeDuplicateTilesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.reloadTilesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyCollisionAllButton = new System.Windows.Forms.ToolStripButton();
            this.remapTilesButton = new System.Windows.Forms.ToolStripButton();
            this.enableDraggingTilesButton = new System.Windows.Forms.ToolStripButton();
            this.label29 = new System.Windows.Forms.Label();
            this.flipTileHButton = new System.Windows.Forms.Button();
            this.flipTileVButton = new System.Windows.Forms.Button();
            this.TileID = new System.Windows.Forms.NumericUpDown();
            this.rotateTileRightButton = new System.Windows.Forms.Button();
            this.TileCount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chunkHighlightHighTilesCheckBox = new System.Windows.Forms.CheckBox();
            this.label31 = new System.Windows.Forms.Label();
            this.chunkShowGridCheckBox = new System.Windows.Forms.CheckBox();
            this.label30 = new System.Windows.Forms.Label();
            this.chunkColBRadioButton = new System.Windows.Forms.RadioButton();
            this.chunkColARadioButton = new System.Windows.Forms.RadioButton();
            this.chunkColNoneRadioButton = new System.Windows.Forms.RadioButton();
            this.chunkShowLowTilesCheckBox = new System.Windows.Forms.CheckBox();
            this.chunkShowHighTilesCheckBox = new System.Windows.Forms.CheckBox();
            this.label28 = new System.Windows.Forms.Label();
            this.flipChunkVButton = new System.Windows.Forms.Button();
            this.flipChunkHButton = new System.Windows.Forms.Button();
            this.ChunkCount = new System.Windows.Forms.Label();
            this.chunkCtrlLabel = new System.Windows.Forms.Label();
            this.ChunkID = new System.Windows.Forms.NumericUpDown();
            this.ChunkPicture = new SonicRetro.SonLVL.API.KeyboardPanel();
            this.copyCollisionSingleButton = new System.Windows.Forms.Button();
            this.colFlags = new System.Windows.Forms.NumericUpDown();
            this.ceilingAngle = new System.Windows.Forms.NumericUpDown();
            this.rightAngle = new System.Windows.Forms.NumericUpDown();
            this.leftAngle = new System.Windows.Forms.NumericUpDown();
            this.collisionCeiling = new System.Windows.Forms.CheckBox();
            this.collisionLayerSelector = new System.Windows.Forms.ComboBox();
            this.calculateAngleButton = new System.Windows.Forms.Button();
            this.showBlockBehindCollisionCheckBox = new System.Windows.Forms.CheckBox();
            this.floorAngle = new System.Windows.Forms.NumericUpDown();
            this.ColPicture = new System.Windows.Forms.Panel();
            this.TilePicture = new System.Windows.Forms.Panel();
            this.importToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.panel10 = new System.Windows.Forms.Panel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editGameConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.presentationStagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regularStagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specialStagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bonusStagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildAndRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.recentModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.gotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.useHexadecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectsAboveHighPlaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hUDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bgColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.path1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.path2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.anglesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.usageCountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chunksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foregroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transparentBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideDebugObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportArtcollisionpriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewReadmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportBugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundLevelLoader = new System.ComponentModel.BackgroundWorker();
            this.objectContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addGroupOfObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ObjectProperties = new System.Windows.Forms.PropertyGrid();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.objectTypeList = new System.Windows.Forms.ListView();
            this.objectTypeImages = new System.Windows.Forms.ImageList(this.components);
            this.objToolStrip = new System.Windows.Forms.ToolStrip();
            this.objGridSizeDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.alignLeftWallToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.alignGroundToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.alignRightWallToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.alignCeilingToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.alignLeftsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.alignCentersToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.alignRightsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.alignTopsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.alignMiddlesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.alignBottomsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tabControl5 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.objectOrder = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.fgToolStrip = new System.Windows.Forms.ToolStrip();
            this.resizeForegroundToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.replaceForegroundToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.clearForegroundToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.ChunkSelector = new SonicRetro.SonLVL.API.TileList();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.layoutSectionSplitContainer = new System.Windows.Forms.SplitContainer();
            this.layoutSectionListBox = new System.Windows.Forms.ListBox();
            this.layoutSectionPreview = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.bgToolStrip = new System.Windows.Forms.ToolStrip();
            this.bgLayerDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.resizeBackgroundToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.replaceBackgroundToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.clearBackgroundToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.bgDuplicateLayerOverToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.layer1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layer2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layer3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layer4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layer5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layer6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layer7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layer8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.previewGroupBox = new System.Windows.Forms.GroupBox();
            this.scrollCamX = new System.Windows.Forms.NumericUpDown();
            this.scrollPreviewButton = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.moveCameraLabel = new System.Windows.Forms.Label();
            this.scrollCamY = new System.Windows.Forms.NumericUpDown();
            this.scrollEditPanel = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.showScrollAreas = new System.Windows.Forms.CheckBox();
            this.scrollOffset = new System.Windows.Forms.NumericUpDown();
            this.scrollList = new System.Windows.Forms.ListBox();
            this.scrollEnableDeformation = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.addScrollButton = new System.Windows.Forms.Button();
            this.scrollScrollSpeed = new System.Windows.Forms.NumericUpDown();
            this.deleteScrollButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.scrollParallaxFactor = new System.Windows.Forms.NumericUpDown();
            this.layerScrollSpeed = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.layerParallaxFactor = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.layerScrollType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.TileSelector = new SonicRetro.SonLVL.API.TileList();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.PalettePanel = new System.Windows.Forms.Panel();
            this.colorEditingPanel = new System.Windows.Forms.Panel();
            this.colorBlue = new System.Windows.Forms.NumericUpDown();
            this.colorGreen = new System.Windows.Forms.NumericUpDown();
            this.colorRed = new System.Windows.Forms.NumericUpDown();
            this.paletteToolStrip = new System.Windows.Forms.ToolStrip();
            this.enableDraggingPaletteButton = new System.Windows.Forms.ToolStripButton();
            this.exportPaletteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.importPaletteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.soundEffectsGroup = new System.Windows.Forms.GroupBox();
            this.sfxDownButton = new System.Windows.Forms.Button();
            this.sfxUpButton = new System.Windows.Forms.Button();
            this.sfxID = new System.Windows.Forms.Label();
            this.sfxBrowseButton = new System.Windows.Forms.Button();
            this.sfxFileBox = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.sfxNameBox = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.sfxDeleteButton = new System.Windows.Forms.Button();
            this.sfxAddButton = new System.Windows.Forms.Button();
            this.sfxListBox = new System.Windows.Forms.ListBox();
            this.objectListGroup = new System.Windows.Forms.GroupBox();
            this.objectDownButton = new System.Windows.Forms.Button();
            this.objectUpButton = new System.Windows.Forms.Button();
            this.objectTypeID = new System.Windows.Forms.Label();
            this.browseScriptButton = new System.Windows.Forms.Button();
            this.objectScriptBox = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.objectNameBox = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.objectDeleteButton = new System.Windows.Forms.Button();
            this.objectAddButton = new System.Windows.Forms.Button();
            this.objectListBox = new System.Windows.Forms.ListBox();
            this.loadGlobalObjects = new System.Windows.Forms.CheckBox();
            this.layerSettingsGroup = new System.Windows.Forms.GroupBox();
            this.lowTilesLabel = new System.Windows.Forms.Label();
            this.midpointTrackBar = new System.Windows.Forms.TrackBar();
            this.foregroundDeformation = new System.Windows.Forms.CheckBox();
            this.layer3Box = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.layer2Box = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.layer1Box = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.layer0Box = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.midpointLabel = new System.Windows.Forms.Label();
            this.titleCardGroup = new System.Windows.Forms.GroupBox();
            this.levelNameBox = new System.Windows.Forms.TextBox();
            this.tileContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deepCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteOverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importOverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawOverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteOnceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteRepeatingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLayoutSectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.insertLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chunkBlockContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.flipChunkBlocksHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipChunkBlocksVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyChunkBlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteChunkBlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearChunkBlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.objectPanel = new SonicRetro.SonLVL.ScrollingPanel();
            this.showGridToolStripCheckBoxButton = new SonicRetro.SonLVL.ToolStripCheckBoxButton();
            this.snapObjectsToolStripCheckBoxButton = new SonicRetro.SonLVL.ToolStripCheckBoxButton();
            this.foregroundPanel = new SonicRetro.SonLVL.ScrollingPanel();
            this.displayObjectsToolStripCheckBoxButton = new SonicRetro.SonLVL.ToolStripCheckBoxButton();
            this.backgroundPanel = new SonicRetro.SonLVL.ScrollingPanel();
            this.chunkBlockEditor = new SonicRetro.SonLVL.ChunkBlockEditor();
            this.colorHex = new SonicRetro.SonLVL.NumericUpDownPadded();
            this.importProgressControl1 = new SonicRetro.SonLVL.ImportProgressControl();
            this.label14 = new System.Windows.Forms.Label();
            toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            chunkListToolStrip = new System.Windows.Forms.ToolStrip();
            tileListToolStrip = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            label1 = new System.Windows.Forms.Label();
            panel11 = new System.Windows.Forms.Panel();
            tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            panel9 = new System.Windows.Forms.Panel();
            label26 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            label27 = new System.Windows.Forms.Label();
            layoutSectionListToolStrip = new System.Windows.Forms.ToolStrip();
            toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem18 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem20 = new System.Windows.Forms.ToolStripMenuItem();
            chunkListToolStrip.SuspendLayout();
            tileListToolStrip.SuspendLayout();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TileID)).BeginInit();
            tableLayoutPanel5.SuspendLayout();
            panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChunkID)).BeginInit();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colFlags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceilingAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorAngle)).BeginInit();
            tableLayoutPanel6.SuspendLayout();
            layoutSectionListToolStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.objectContextMenuStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.objToolStrip.SuspendLayout();
            this.tabControl5.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.fgToolStrip.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSectionSplitContainer)).BeginInit();
            this.layoutSectionSplitContainer.Panel1.SuspendLayout();
            this.layoutSectionSplitContainer.Panel2.SuspendLayout();
            this.layoutSectionSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSectionPreview)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.bgToolStrip.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.previewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrollCamX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrollCamY)).BeginInit();
            this.scrollEditPanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrollOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrollScrollSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrollParallaxFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerScrollSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerParallaxFactor)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.panel8.SuspendLayout();
            this.colorEditingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRed)).BeginInit();
            this.paletteToolStrip.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.soundEffectsGroup.SuspendLayout();
            this.objectListGroup.SuspendLayout();
            this.layerSettingsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.midpointTrackBar)).BeginInit();
            this.titleCardGroup.SuspendLayout();
            this.tileContextMenuStrip.SuspendLayout();
            this.layoutContextMenuStrip.SuspendLayout();
            this.chunkBlockContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorHex)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new System.Drawing.Size(6, 42);
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(10, 109);
            label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(61, 25);
            label4.TabIndex = 4;
            label4.Text = "Blue:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(10, 59);
            label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(77, 25);
            label3.TabIndex = 2;
            label3.Text = "Green:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(10, 9);
            label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(57, 25);
            label2.TabIndex = 0;
            label2.Text = "Red:";
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new System.Drawing.Size(6, 42);
            // 
            // chunkListToolStrip
            // 
            chunkListToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            chunkListToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            chunkListToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importChunksToolStripButton,
            this.drawChunkToolStripButton,
            this.deleteUnusedChunksToolStripButton,
            this.removeDuplicateChunksToolStripButton,
            this.replaceChunkBlocksToolStripButton,
            this.remapChunksButton,
            this.enableDraggingChunksButton});
            chunkListToolStrip.Location = new System.Drawing.Point(6, 6);
            chunkListToolStrip.Name = "chunkListToolStrip";
            chunkListToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            chunkListToolStrip.Size = new System.Drawing.Size(1586, 42);
            chunkListToolStrip.TabIndex = 0;
            chunkListToolStrip.Text = "toolStrip1";
            // 
            // importChunksToolStripButton
            // 
            this.importChunksToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.importChunksToolStripButton.Enabled = false;
            this.importChunksToolStripButton.Name = "importChunksToolStripButton";
            this.importChunksToolStripButton.Size = new System.Drawing.Size(104, 44);
            this.importChunksToolStripButton.Text = "Import...";
            this.importChunksToolStripButton.ToolTipText = "Import a chunk from an image.";
            this.importChunksToolStripButton.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // drawChunkToolStripButton
            // 
            this.drawChunkToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.drawChunkToolStripButton.Enabled = false;
            this.drawChunkToolStripButton.Name = "drawChunkToolStripButton";
            this.drawChunkToolStripButton.Size = new System.Drawing.Size(87, 44);
            this.drawChunkToolStripButton.Text = "Draw...";
            this.drawChunkToolStripButton.ToolTipText = "Draw a chunk and import it into the stage.";
            this.drawChunkToolStripButton.Click += new System.EventHandler(this.drawToolStripButton_Click);
            // 
            // deleteUnusedChunksToolStripButton
            // 
            this.deleteUnusedChunksToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteUnusedChunksToolStripButton.Name = "deleteUnusedChunksToolStripButton";
            this.deleteUnusedChunksToolStripButton.Size = new System.Drawing.Size(176, 44);
            this.deleteUnusedChunksToolStripButton.Text = "Delete Unused";
            this.deleteUnusedChunksToolStripButton.ToolTipText = "Delete all chunks not used in any act of this stage.";
            this.deleteUnusedChunksToolStripButton.Click += new System.EventHandler(this.deleteUnusedChunksToolStripButton_Click);
            // 
            // removeDuplicateChunksToolStripButton
            // 
            this.removeDuplicateChunksToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.removeDuplicateChunksToolStripButton.Name = "removeDuplicateChunksToolStripButton";
            this.removeDuplicateChunksToolStripButton.Size = new System.Drawing.Size(222, 44);
            this.removeDuplicateChunksToolStripButton.Text = "Remove Duplicates";
            this.removeDuplicateChunksToolStripButton.ToolTipText = "Removes all duplicate chunks in the chunk list.";
            this.removeDuplicateChunksToolStripButton.Click += new System.EventHandler(this.removeDuplicateChunksToolStripButton_Click);
            // 
            // replaceChunkBlocksToolStripButton
            // 
            this.replaceChunkBlocksToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.replaceChunkBlocksToolStripButton.Name = "replaceChunkBlocksToolStripButton";
            this.replaceChunkBlocksToolStripButton.Size = new System.Drawing.Size(100, 44);
            this.replaceChunkBlocksToolStripButton.Text = "Replace";
            this.replaceChunkBlocksToolStripButton.ToolTipText = "Replace all instances of a certain tile within the stage\'s chunks.";
            this.replaceChunkBlocksToolStripButton.Click += new System.EventHandler(this.replaceChunkBlocksToolStripButton_Click);
            // 
            // remapChunksButton
            // 
            this.remapChunksButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.remapChunksButton.Enabled = false;
            this.remapChunksButton.Image = ((System.Drawing.Image)(resources.GetObject("remapChunksButton.Image")));
            this.remapChunksButton.Name = "remapChunksButton";
            this.remapChunksButton.Size = new System.Drawing.Size(266, 36);
            this.remapChunksButton.Text = "Advanced Remapping...";
            this.remapChunksButton.ToolTipText = "Change the order of several chunks at a time.";
            this.remapChunksButton.Click += new System.EventHandler(this.remapChunksButton_Click);
            // 
            // enableDraggingChunksButton
            // 
            this.enableDraggingChunksButton.Checked = true;
            this.enableDraggingChunksButton.CheckOnClick = true;
            this.enableDraggingChunksButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableDraggingChunksButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.enableDraggingChunksButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.enableDraggingChunksButton.Name = "enableDraggingChunksButton";
            this.enableDraggingChunksButton.Size = new System.Drawing.Size(195, 44);
            this.enableDraggingChunksButton.Text = "Enable Dragging";
            this.enableDraggingChunksButton.ToolTipText = "Toggle dragging chunks around to reorder them.";
            // 
            // tileListToolStrip
            // 
            tileListToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            tileListToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            tileListToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importTilesToolStripButton,
            this.drawTileToolStripButton,
            this.deleteUnusedTilesToolStripButton,
            this.removeDuplicateTilesToolStripButton,
            this.reloadTilesToolStripButton,
            this.copyCollisionAllButton,
            this.remapTilesButton,
            this.enableDraggingTilesButton});
            tileListToolStrip.Location = new System.Drawing.Point(6, 6);
            tileListToolStrip.Name = "tileListToolStrip";
            tileListToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            tileListToolStrip.Size = new System.Drawing.Size(1586, 42);
            tileListToolStrip.TabIndex = 2;
            tileListToolStrip.Text = "toolStrip3";
            // 
            // importTilesToolStripButton
            // 
            this.importTilesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.importTilesToolStripButton.Enabled = false;
            this.importTilesToolStripButton.Name = "importTilesToolStripButton";
            this.importTilesToolStripButton.Size = new System.Drawing.Size(104, 44);
            this.importTilesToolStripButton.Text = "Import...";
            this.importTilesToolStripButton.ToolTipText = "Import a tile from an image.";
            this.importTilesToolStripButton.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // drawTileToolStripButton
            // 
            this.drawTileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.drawTileToolStripButton.Enabled = false;
            this.drawTileToolStripButton.Name = "drawTileToolStripButton";
            this.drawTileToolStripButton.Size = new System.Drawing.Size(87, 44);
            this.drawTileToolStripButton.Text = "Draw...";
            this.drawTileToolStripButton.ToolTipText = "Draw a tile and import it into the stage.";
            this.drawTileToolStripButton.Click += new System.EventHandler(this.drawToolStripButton_Click);
            // 
            // deleteUnusedTilesToolStripButton
            // 
            this.deleteUnusedTilesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteUnusedTilesToolStripButton.Name = "deleteUnusedTilesToolStripButton";
            this.deleteUnusedTilesToolStripButton.Size = new System.Drawing.Size(176, 44);
            this.deleteUnusedTilesToolStripButton.Text = "Delete Unused";
            this.deleteUnusedTilesToolStripButton.ToolTipText = "Delete all tiles not used in any chunks.";
            this.deleteUnusedTilesToolStripButton.Click += new System.EventHandler(this.deleteUnusedTilesToolStripButton_Click);
            // 
            // removeDuplicateTilesToolStripButton
            // 
            this.removeDuplicateTilesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.removeDuplicateTilesToolStripButton.Name = "removeDuplicateTilesToolStripButton";
            this.removeDuplicateTilesToolStripButton.Size = new System.Drawing.Size(222, 44);
            this.removeDuplicateTilesToolStripButton.Text = "Remove Duplicates";
            this.removeDuplicateTilesToolStripButton.ToolTipText = "Removes all tiles with identical art and collision to other tiles.";
            this.removeDuplicateTilesToolStripButton.Click += new System.EventHandler(this.removeDuplicateTilesToolStripButton_Click);
            // 
            // reloadTilesToolStripButton
            // 
            this.reloadTilesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.reloadTilesToolStripButton.Name = "reloadTilesToolStripButton";
            this.reloadTilesToolStripButton.Size = new System.Drawing.Size(90, 44);
            this.reloadTilesToolStripButton.Text = "Reload";
            this.reloadTilesToolStripButton.ToolTipText = "Reload the 16x16Tiles.gif.";
            this.reloadTilesToolStripButton.Click += new System.EventHandler(this.reloadTilesToolStripButton_Click);
            // 
            // copyCollisionAllButton
            // 
            this.copyCollisionAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.copyCollisionAllButton.Name = "copyCollisionAllButton";
            this.copyCollisionAllButton.Size = new System.Drawing.Size(186, 44);
            this.copyCollisionAllButton.Text = "Copy Collision...";
            this.copyCollisionAllButton.ToolTipText = "Copy collision of all tiles between layers.";
            this.copyCollisionAllButton.Click += new System.EventHandler(this.copyCollisionAllButton_Click);
            // 
            // remapTilesButton
            // 
            this.remapTilesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.remapTilesButton.Enabled = false;
            this.remapTilesButton.Image = ((System.Drawing.Image)(resources.GetObject("remapTilesButton.Image")));
            this.remapTilesButton.Name = "remapTilesButton";
            this.remapTilesButton.Size = new System.Drawing.Size(266, 44);
            this.remapTilesButton.Text = "Advanced Remapping...";
            this.remapTilesButton.ToolTipText = "Change the order of several tiles at a time.";
            this.remapTilesButton.Click += new System.EventHandler(this.remapTilesButton_Click);
            // 
            // enableDraggingTilesButton
            // 
            this.enableDraggingTilesButton.Checked = true;
            this.enableDraggingTilesButton.CheckOnClick = true;
            this.enableDraggingTilesButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableDraggingTilesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.enableDraggingTilesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.enableDraggingTilesButton.Name = "enableDraggingTilesButton";
            this.enableDraggingTilesButton.Size = new System.Drawing.Size(195, 44);
            this.enableDraggingTilesButton.Text = "Enable Dragging";
            this.enableDraggingTilesButton.ToolTipText = "Toggle dragging tiles around to reorder them.";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(441, 6);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 378);
            label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(128, 25);
            label1.TabIndex = 6;
            label1.Text = "Floor Angle:";
            // 
            // panel11
            // 
            panel11.AutoSize = true;
            panel11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panel11.Controls.Add(this.label29);
            panel11.Controls.Add(this.flipTileHButton);
            panel11.Controls.Add(this.flipTileVButton);
            panel11.Controls.Add(this.TileID);
            panel11.Controls.Add(this.rotateTileRightButton);
            panel11.Controls.Add(this.TileCount);
            panel11.Location = new System.Drawing.Point(0, 259);
            panel11.Margin = new System.Windows.Forms.Padding(0);
            panel11.Name = "panel11";
            panel11.Size = new System.Drawing.Size(259, 249);
            panel11.TabIndex = 11;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 10);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(143, 25);
            this.label29.TabIndex = 11;
            this.label29.Text = "Selected Tile:";
            // 
            // flipTileHButton
            // 
            this.flipTileHButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flipTileHButton.Enabled = false;
            this.flipTileHButton.Location = new System.Drawing.Point(6, 86);
            this.flipTileHButton.Margin = new System.Windows.Forms.Padding(6);
            this.flipTileHButton.Name = "flipTileHButton";
            this.flipTileHButton.Size = new System.Drawing.Size(247, 45);
            this.flipTileHButton.TabIndex = 9;
            this.flipTileHButton.Text = "Flip Tile Horizontally";
            this.toolTip.SetToolTip(this.flipTileHButton, "Flip the tile\'s art and collision horizontally.");
            this.flipTileHButton.UseVisualStyleBackColor = true;
            this.flipTileHButton.Click += new System.EventHandler(this.flipTileHButton_Click);
            // 
            // flipTileVButton
            // 
            this.flipTileVButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flipTileVButton.Enabled = false;
            this.flipTileVButton.Location = new System.Drawing.Point(6, 141);
            this.flipTileVButton.Margin = new System.Windows.Forms.Padding(6);
            this.flipTileVButton.Name = "flipTileVButton";
            this.flipTileVButton.Size = new System.Drawing.Size(247, 45);
            this.flipTileVButton.TabIndex = 10;
            this.flipTileVButton.Text = "Flip Tile Vertically";
            this.toolTip.SetToolTip(this.flipTileVButton, "Flip the tile\'s art and collision vertically.");
            this.flipTileVButton.UseVisualStyleBackColor = true;
            this.flipTileVButton.Click += new System.EventHandler(this.flipTileVButton_Click);
            // 
            // TileID
            // 
            this.TileID.Hexadecimal = true;
            this.TileID.Location = new System.Drawing.Point(11, 41);
            this.TileID.Margin = new System.Windows.Forms.Padding(6);
            this.TileID.Maximum = new decimal(new int[] {
            2047,
            0,
            0,
            0});
            this.TileID.Name = "TileID";
            this.TileID.Size = new System.Drawing.Size(162, 31);
            this.TileID.TabIndex = 3;
            this.TileID.ValueChanged += new System.EventHandler(this.TileID_ValueChanged);
            // 
            // rotateTileRightButton
            // 
            this.rotateTileRightButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rotateTileRightButton.Enabled = false;
            this.rotateTileRightButton.Location = new System.Drawing.Point(6, 198);
            this.rotateTileRightButton.Margin = new System.Windows.Forms.Padding(6);
            this.rotateTileRightButton.Name = "rotateTileRightButton";
            this.rotateTileRightButton.Size = new System.Drawing.Size(247, 45);
            this.rotateTileRightButton.TabIndex = 5;
            this.rotateTileRightButton.Text = "Rotate Tile Right";
            this.rotateTileRightButton.UseVisualStyleBackColor = true;
            this.rotateTileRightButton.Click += new System.EventHandler(this.rotateTileRightButton_Click);
            // 
            // TileCount
            // 
            this.TileCount.AutoSize = true;
            this.TileCount.Location = new System.Drawing.Point(185, 43);
            this.TileCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.TileCount.Name = "TileCount";
            this.TileCount.Size = new System.Drawing.Size(62, 25);
            this.TileCount.TabIndex = 4;
            this.TileCount.Text = "/ 3FF";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.AutoSize = true;
            tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(panel1, 0, 1);
            tableLayoutPanel5.Controls.Add(this.ChunkPicture, 0, 0);
            tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel4.SetRowSpan(tableLayoutPanel5, 2);
            tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel5.Size = new System.Drawing.Size(524, 1170);
            tableLayoutPanel5.TabIndex = 9;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(this.groupBox1);
            panel1.Controls.Add(this.label28);
            panel1.Controls.Add(this.flipChunkVButton);
            panel1.Controls.Add(this.chunkBlockEditor);
            panel1.Controls.Add(this.flipChunkHButton);
            panel1.Controls.Add(this.ChunkCount);
            panel1.Controls.Add(this.chunkCtrlLabel);
            panel1.Controls.Add(this.ChunkID);
            panel1.Location = new System.Drawing.Point(0, 506);
            panel1.Margin = new System.Windows.Forms.Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(521, 664);
            panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chunkHighlightHighTilesCheckBox);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.chunkShowGridCheckBox);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.chunkColBRadioButton);
            this.groupBox1.Controls.Add(this.chunkColARadioButton);
            this.groupBox1.Controls.Add(this.chunkColNoneRadioButton);
            this.groupBox1.Controls.Add(this.chunkShowLowTilesCheckBox);
            this.groupBox1.Controls.Add(this.chunkShowHighTilesCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(6, 441);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(512, 220);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chunk View";
            // 
            // chunkHighlightHighTilesCheckBox
            // 
            this.chunkHighlightHighTilesCheckBox.AutoSize = true;
            this.chunkHighlightHighTilesCheckBox.Location = new System.Drawing.Point(11, 171);
            this.chunkHighlightHighTilesCheckBox.Name = "chunkHighlightHighTilesCheckBox";
            this.chunkHighlightHighTilesCheckBox.Size = new System.Drawing.Size(290, 29);
            this.chunkHighlightHighTilesCheckBox.TabIndex = 8;
            this.chunkHighlightHighTilesCheckBox.Text = "Highlight High Layer Tiles";
            this.toolTip.SetToolTip(this.chunkHighlightHighTilesCheckBox, "Highlight each High Plane tile with the selected grid color, while leaving Low Pl" +
        "ane tiles alone.");
            this.chunkHighlightHighTilesCheckBox.UseVisualStyleBackColor = true;
            this.chunkHighlightHighTilesCheckBox.CheckedChanged += new System.EventHandler(this.chunkHighlightHighTilesCheckBox_CheckedChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 86);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(83, 25);
            this.label31.TabIndex = 7;
            this.label31.Text = "Layers:";
            // 
            // chunkShowGridCheckBox
            // 
            this.chunkShowGridCheckBox.AutoSize = true;
            this.chunkShowGridCheckBox.Location = new System.Drawing.Point(11, 136);
            this.chunkShowGridCheckBox.Name = "chunkShowGridCheckBox";
            this.chunkShowGridCheckBox.Size = new System.Drawing.Size(143, 29);
            this.chunkShowGridCheckBox.TabIndex = 6;
            this.chunkShowGridCheckBox.Text = "Show Grid";
            this.toolTip.SetToolTip(this.chunkShowGridCheckBox, "Toggle grid visibility.");
            this.chunkShowGridCheckBox.UseVisualStyleBackColor = true;
            this.chunkShowGridCheckBox.CheckedChanged += new System.EventHandler(this.chunkShowGridCheckBox_CheckedChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 39);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(152, 25);
            this.label30.TabIndex = 5;
            this.label30.Text = "Collision View:";
            // 
            // chunkColBRadioButton
            // 
            this.chunkColBRadioButton.AutoSize = true;
            this.chunkColBRadioButton.Location = new System.Drawing.Point(385, 37);
            this.chunkColBRadioButton.Name = "chunkColBRadioButton";
            this.chunkColBRadioButton.Size = new System.Drawing.Size(118, 29);
            this.chunkColBRadioButton.TabIndex = 4;
            this.chunkColBRadioButton.Text = "Plane B";
            this.toolTip.SetToolTip(this.chunkColBRadioButton, "Show collision for Plane B.");
            this.chunkColBRadioButton.UseVisualStyleBackColor = true;
            this.chunkColBRadioButton.CheckedChanged += new System.EventHandler(this.chunkColBRadioButton_CheckedChanged);
            // 
            // chunkColARadioButton
            // 
            this.chunkColARadioButton.AutoSize = true;
            this.chunkColARadioButton.Location = new System.Drawing.Point(260, 37);
            this.chunkColARadioButton.Name = "chunkColARadioButton";
            this.chunkColARadioButton.Size = new System.Drawing.Size(118, 29);
            this.chunkColARadioButton.TabIndex = 3;
            this.chunkColARadioButton.Text = "Plane A";
            this.toolTip.SetToolTip(this.chunkColARadioButton, "Show collision for Plane A.");
            this.chunkColARadioButton.UseVisualStyleBackColor = true;
            this.chunkColARadioButton.CheckedChanged += new System.EventHandler(this.chunkColARadioButton_CheckedChanged);
            // 
            // chunkColNoneRadioButton
            // 
            this.chunkColNoneRadioButton.AutoSize = true;
            this.chunkColNoneRadioButton.Checked = true;
            this.chunkColNoneRadioButton.Location = new System.Drawing.Point(158, 37);
            this.chunkColNoneRadioButton.Name = "chunkColNoneRadioButton";
            this.chunkColNoneRadioButton.Size = new System.Drawing.Size(94, 29);
            this.chunkColNoneRadioButton.TabIndex = 2;
            this.chunkColNoneRadioButton.TabStop = true;
            this.chunkColNoneRadioButton.Text = "None";
            this.toolTip.SetToolTip(this.chunkColNoneRadioButton, "Hide collision visualizations.");
            this.chunkColNoneRadioButton.UseVisualStyleBackColor = true;
            this.chunkColNoneRadioButton.CheckedChanged += new System.EventHandler(this.chunkColNoneRadioButton_CheckedChanged);
            // 
            // chunkShowLowTilesCheckBox
            // 
            this.chunkShowLowTilesCheckBox.AutoSize = true;
            this.chunkShowLowTilesCheckBox.Checked = true;
            this.chunkShowLowTilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chunkShowLowTilesCheckBox.Location = new System.Drawing.Point(93, 86);
            this.chunkShowLowTilesCheckBox.Name = "chunkShowLowTilesCheckBox";
            this.chunkShowLowTilesCheckBox.Size = new System.Drawing.Size(194, 29);
            this.chunkShowLowTilesCheckBox.TabIndex = 1;
            this.chunkShowLowTilesCheckBox.Text = "Show Low Tiles";
            this.toolTip.SetToolTip(this.chunkShowLowTilesCheckBox, "Toggle visibilty of Low Plane tiles.");
            this.chunkShowLowTilesCheckBox.UseVisualStyleBackColor = true;
            this.chunkShowLowTilesCheckBox.CheckedChanged += new System.EventHandler(this.chunkShowLowTilesCheckBox_CheckedChanged);
            // 
            // chunkShowHighTilesCheckBox
            // 
            this.chunkShowHighTilesCheckBox.AutoSize = true;
            this.chunkShowHighTilesCheckBox.Checked = true;
            this.chunkShowHighTilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chunkShowHighTilesCheckBox.Location = new System.Drawing.Point(298, 86);
            this.chunkShowHighTilesCheckBox.Name = "chunkShowHighTilesCheckBox";
            this.chunkShowHighTilesCheckBox.Size = new System.Drawing.Size(199, 29);
            this.chunkShowHighTilesCheckBox.TabIndex = 0;
            this.chunkShowHighTilesCheckBox.Text = "Show High Tiles";
            this.toolTip.SetToolTip(this.chunkShowHighTilesCheckBox, "Toggle visibilty of High Plane tiles.");
            this.chunkShowHighTilesCheckBox.UseVisualStyleBackColor = true;
            this.chunkShowHighTilesCheckBox.CheckedChanged += new System.EventHandler(this.chunkShowHighTilesCheckBox_CheckedChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 21);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(170, 25);
            this.label28.TabIndex = 7;
            this.label28.Text = "Selected Chunk:";
            // 
            // flipChunkVButton
            // 
            this.flipChunkVButton.AutoSize = true;
            this.flipChunkVButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flipChunkVButton.Enabled = false;
            this.flipChunkVButton.Location = new System.Drawing.Point(267, 63);
            this.flipChunkVButton.Margin = new System.Windows.Forms.Padding(6);
            this.flipChunkVButton.Name = "flipChunkVButton";
            this.flipChunkVButton.Size = new System.Drawing.Size(219, 35);
            this.flipChunkVButton.TabIndex = 6;
            this.flipChunkVButton.Text = "Flip Chunk Vertically";
            this.toolTip.SetToolTip(this.flipChunkVButton, "Flip the chunk vertically.");
            this.flipChunkVButton.UseVisualStyleBackColor = true;
            this.flipChunkVButton.Click += new System.EventHandler(this.flipChunkVButton_Click);
            // 
            // flipChunkHButton
            // 
            this.flipChunkHButton.AutoSize = true;
            this.flipChunkHButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flipChunkHButton.Enabled = false;
            this.flipChunkHButton.Location = new System.Drawing.Point(11, 63);
            this.flipChunkHButton.Margin = new System.Windows.Forms.Padding(6);
            this.flipChunkHButton.Name = "flipChunkHButton";
            this.flipChunkHButton.Size = new System.Drawing.Size(244, 35);
            this.flipChunkHButton.TabIndex = 5;
            this.flipChunkHButton.Text = "Flip Chunk Horizontally";
            this.toolTip.SetToolTip(this.flipChunkHButton, "Flip the chunk horizontally.");
            this.flipChunkHButton.UseVisualStyleBackColor = true;
            this.flipChunkHButton.Click += new System.EventHandler(this.flipChunkHButton_Click);
            // 
            // ChunkCount
            // 
            this.ChunkCount.AutoSize = true;
            this.ChunkCount.Location = new System.Drawing.Point(394, 21);
            this.ChunkCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ChunkCount.Name = "ChunkCount";
            this.ChunkCount.Size = new System.Drawing.Size(62, 25);
            this.ChunkCount.TabIndex = 3;
            this.ChunkCount.Text = "/ 1FF";
            // 
            // chunkCtrlLabel
            // 
            this.chunkCtrlLabel.AutoSize = true;
            this.chunkCtrlLabel.Location = new System.Drawing.Point(6, 120);
            this.chunkCtrlLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.chunkCtrlLabel.Name = "chunkCtrlLabel";
            this.chunkCtrlLabel.Size = new System.Drawing.Size(339, 50);
            this.chunkCtrlLabel.TabIndex = 4;
            this.chunkCtrlLabel.Text = "Left Click:   Paint with selected tile\nRight Click: Select tile";
            // 
            // ChunkID
            // 
            this.ChunkID.Hexadecimal = true;
            this.ChunkID.Location = new System.Drawing.Point(194, 18);
            this.ChunkID.Margin = new System.Windows.Forms.Padding(6);
            this.ChunkID.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ChunkID.Name = "ChunkID";
            this.ChunkID.Size = new System.Drawing.Size(188, 31);
            this.ChunkID.TabIndex = 2;
            this.ChunkID.ValueChanged += new System.EventHandler(this.ChunkID_ValueChanged);
            // 
            // ChunkPicture
            // 
            this.ChunkPicture.Location = new System.Drawing.Point(6, 6);
            this.ChunkPicture.Margin = new System.Windows.Forms.Padding(6);
            this.ChunkPicture.Name = "ChunkPicture";
            this.ChunkPicture.Size = new System.Drawing.Size(512, 494);
            this.ChunkPicture.TabIndex = 1;
            this.ChunkPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.ChunkPicture_Paint);
            this.ChunkPicture.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChunkPicture_KeyDown);
            this.ChunkPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChunkPicture_MouseDown);
            this.ChunkPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChunkPicture_MouseMove);
            this.ChunkPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChunkPicture_MouseUp);
            // 
            // panel9
            // 
            panel9.AutoSize = true;
            panel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panel9.Controls.Add(this.copyCollisionSingleButton);
            panel9.Controls.Add(label26);
            panel9.Controls.Add(this.colFlags);
            panel9.Controls.Add(label7);
            panel9.Controls.Add(this.ceilingAngle);
            panel9.Controls.Add(label6);
            panel9.Controls.Add(this.rightAngle);
            panel9.Controls.Add(label5);
            panel9.Controls.Add(this.leftAngle);
            panel9.Controls.Add(this.collisionCeiling);
            panel9.Controls.Add(this.collisionLayerSelector);
            panel9.Controls.Add(this.calculateAngleButton);
            panel9.Controls.Add(label1);
            panel9.Controls.Add(this.showBlockBehindCollisionCheckBox);
            panel9.Controls.Add(this.floorAngle);
            panel9.Controls.Add(this.ColPicture);
            panel9.Location = new System.Drawing.Point(524, 508);
            panel9.Margin = new System.Windows.Forms.Padding(0);
            panel9.Name = "panel9";
            panel9.Size = new System.Drawing.Size(262, 766);
            panel9.TabIndex = 1;
            // 
            // copyCollisionSingleButton
            // 
            this.copyCollisionSingleButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.copyCollisionSingleButton.Location = new System.Drawing.Point(6, 678);
            this.copyCollisionSingleButton.Margin = new System.Windows.Forms.Padding(6);
            this.copyCollisionSingleButton.Name = "copyCollisionSingleButton";
            this.copyCollisionSingleButton.Size = new System.Drawing.Size(240, 41);
            this.copyCollisionSingleButton.TabIndex = 18;
            this.copyCollisionSingleButton.Text = "Copy to Plane B";
            this.toolTip.SetToolTip(this.copyCollisionSingleButton, "Copy the shown collision info to the other plane.");
            this.copyCollisionSingleButton.UseVisualStyleBackColor = true;
            this.copyCollisionSingleButton.Click += new System.EventHandler(this.copyCollisionSingleButton_Click);
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new System.Drawing.Point(6, 634);
            label26.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label26.Name = "label26";
            label26.Size = new System.Drawing.Size(71, 25);
            label26.TabIndex = 17;
            label26.Text = "Flags:";
            // 
            // colFlags
            // 
            this.colFlags.Location = new System.Drawing.Point(164, 632);
            this.colFlags.Margin = new System.Windows.Forms.Padding(6);
            this.colFlags.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.colFlags.Name = "colFlags";
            this.colFlags.Size = new System.Drawing.Size(82, 31);
            this.colFlags.TabIndex = 16;
            this.toolTip.SetToolTip(this.colFlags, "A generic value, used by scripts for any purpose.");
            this.colFlags.ValueChanged += new System.EventHandler(this.colFlags_ValueChanged);
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(6, 528);
            label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(145, 25);
            label7.TabIndex = 15;
            label7.Text = "Ceiling Angle:";
            // 
            // ceilingAngle
            // 
            this.ceilingAngle.Hexadecimal = true;
            this.ceilingAngle.Location = new System.Drawing.Point(164, 527);
            this.ceilingAngle.Margin = new System.Windows.Forms.Padding(6);
            this.ceilingAngle.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ceilingAngle.Name = "ceilingAngle";
            this.ceilingAngle.Size = new System.Drawing.Size(82, 31);
            this.ceilingAngle.TabIndex = 14;
            this.toolTip.SetToolTip(this.ceilingAngle, "The angle of the tile when walking on its bottom.");
            this.ceilingAngle.ValueChanged += new System.EventHandler(this.ceilingAngle_ValueChanged);
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(6, 478);
            label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(129, 25);
            label6.TabIndex = 13;
            label6.Text = "Right Angle:";
            // 
            // rightAngle
            // 
            this.rightAngle.Hexadecimal = true;
            this.rightAngle.Location = new System.Drawing.Point(164, 475);
            this.rightAngle.Margin = new System.Windows.Forms.Padding(6);
            this.rightAngle.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.rightAngle.Name = "rightAngle";
            this.rightAngle.Size = new System.Drawing.Size(82, 31);
            this.rightAngle.TabIndex = 12;
            this.toolTip.SetToolTip(this.rightAngle, "The angle of the tile when walking on its right side.");
            this.rightAngle.ValueChanged += new System.EventHandler(this.rightAngle_ValueChanged);
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 428);
            label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(115, 25);
            label5.TabIndex = 11;
            label5.Text = "Left Angle:";
            // 
            // leftAngle
            // 
            this.leftAngle.Hexadecimal = true;
            this.leftAngle.Location = new System.Drawing.Point(164, 426);
            this.leftAngle.Margin = new System.Windows.Forms.Padding(6);
            this.leftAngle.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.leftAngle.Name = "leftAngle";
            this.leftAngle.Size = new System.Drawing.Size(82, 31);
            this.leftAngle.TabIndex = 10;
            this.toolTip.SetToolTip(this.leftAngle, "The angle of the tile when walking on its left side.");
            this.leftAngle.ValueChanged += new System.EventHandler(this.leftAngle_ValueChanged);
            // 
            // collisionCeiling
            // 
            this.collisionCeiling.AutoSize = true;
            this.collisionCeiling.Location = new System.Drawing.Point(6, 331);
            this.collisionCeiling.Margin = new System.Windows.Forms.Padding(6);
            this.collisionCeiling.Name = "collisionCeiling";
            this.collisionCeiling.Size = new System.Drawing.Size(110, 29);
            this.collisionCeiling.TabIndex = 9;
            this.collisionCeiling.Text = "Ceiling";
            this.collisionCeiling.UseVisualStyleBackColor = true;
            this.collisionCeiling.CheckedChanged += new System.EventHandler(this.collisionCeiling_CheckedChanged);
            // 
            // collisionLayerSelector
            // 
            this.collisionLayerSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.collisionLayerSelector.FormattingEnabled = true;
            this.collisionLayerSelector.Items.AddRange(new object[] {
            "Plane A",
            "Plane B"});
            this.collisionLayerSelector.Location = new System.Drawing.Point(6, 6);
            this.collisionLayerSelector.Margin = new System.Windows.Forms.Padding(6);
            this.collisionLayerSelector.Name = "collisionLayerSelector";
            this.collisionLayerSelector.Size = new System.Drawing.Size(240, 33);
            this.collisionLayerSelector.TabIndex = 8;
            this.collisionLayerSelector.SelectedIndexChanged += new System.EventHandler(this.collisionLayerSelector_SelectedIndexChanged);
            // 
            // calculateAngleButton
            // 
            this.calculateAngleButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.calculateAngleButton.Location = new System.Drawing.Point(6, 575);
            this.calculateAngleButton.Margin = new System.Windows.Forms.Padding(6);
            this.calculateAngleButton.Name = "calculateAngleButton";
            this.calculateAngleButton.Size = new System.Drawing.Size(240, 41);
            this.calculateAngleButton.TabIndex = 7;
            this.calculateAngleButton.Text = "Calculate Angles";
            this.toolTip.SetToolTip(this.calculateAngleButton, "Calculate the collision angles of this tile.");
            this.calculateAngleButton.UseVisualStyleBackColor = true;
            this.calculateAngleButton.Click += new System.EventHandler(this.calculateAngleButton_Click);
            // 
            // showBlockBehindCollisionCheckBox
            // 
            this.showBlockBehindCollisionCheckBox.AutoSize = true;
            this.showBlockBehindCollisionCheckBox.Location = new System.Drawing.Point(10, 731);
            this.showBlockBehindCollisionCheckBox.Margin = new System.Windows.Forms.Padding(6);
            this.showBlockBehindCollisionCheckBox.Name = "showBlockBehindCollisionCheckBox";
            this.showBlockBehindCollisionCheckBox.Size = new System.Drawing.Size(138, 29);
            this.showBlockBehindCollisionCheckBox.TabIndex = 5;
            this.showBlockBehindCollisionCheckBox.Text = "Show Tile";
            this.toolTip.SetToolTip(this.showBlockBehindCollisionCheckBox, "Show the art of the tile behind the collision visualisation.");
            this.showBlockBehindCollisionCheckBox.UseVisualStyleBackColor = true;
            this.showBlockBehindCollisionCheckBox.CheckedChanged += new System.EventHandler(this.showBlockBehindCollisionCheckBox_CheckedChanged);
            // 
            // floorAngle
            // 
            this.floorAngle.Hexadecimal = true;
            this.floorAngle.Location = new System.Drawing.Point(164, 376);
            this.floorAngle.Margin = new System.Windows.Forms.Padding(6);
            this.floorAngle.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.floorAngle.Name = "floorAngle";
            this.floorAngle.Size = new System.Drawing.Size(82, 31);
            this.floorAngle.TabIndex = 3;
            this.toolTip.SetToolTip(this.floorAngle, "The angle of the tile when walking on its top.");
            this.floorAngle.ValueChanged += new System.EventHandler(this.floorAngle_ValueChanged);
            // 
            // ColPicture
            // 
            this.ColPicture.BackColor = System.Drawing.Color.Black;
            this.ColPicture.Location = new System.Drawing.Point(6, 58);
            this.ColPicture.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.ColPicture.Name = "ColPicture";
            this.ColPicture.Size = new System.Drawing.Size(256, 261);
            this.ColPicture.TabIndex = 2;
            this.ColPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.ColPicture_Paint);
            this.ColPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColPicture_MouseDown);
            this.ColPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColPicture_MouseMove);
            this.ColPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ColPicture_MouseUp);
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(this.TilePicture, 0, 0);
            tableLayoutPanel6.Controls.Add(panel11, 0, 1);
            tableLayoutPanel6.Location = new System.Drawing.Point(524, 0);
            tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel6.Size = new System.Drawing.Size(268, 508);
            tableLayoutPanel6.TabIndex = 9;
            // 
            // TilePicture
            // 
            this.TilePicture.Location = new System.Drawing.Point(6, 6);
            this.TilePicture.Margin = new System.Windows.Forms.Padding(6);
            this.TilePicture.Name = "TilePicture";
            this.TilePicture.Size = new System.Drawing.Size(256, 247);
            this.TilePicture.TabIndex = 1;
            this.TilePicture.Paint += new System.Windows.Forms.PaintEventHandler(this.TilePicture_Paint);
            this.TilePicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TilePicture_MouseDown);
            this.TilePicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TilePicture_MouseMove);
            this.TilePicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TilePicture_MouseUp);
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new System.Drawing.Point(10, 175);
            label27.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            label27.Name = "label27";
            label27.Size = new System.Drawing.Size(56, 25);
            label27.TabIndex = 6;
            label27.Text = "Hex:";
            // 
            // layoutSectionListToolStrip
            // 
            layoutSectionListToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            layoutSectionListToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            layoutSectionListToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripButton,
            this.deleteToolStripButton});
            layoutSectionListToolStrip.Location = new System.Drawing.Point(0, 0);
            layoutSectionListToolStrip.Name = "layoutSectionListToolStrip";
            layoutSectionListToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            layoutSectionListToolStrip.Size = new System.Drawing.Size(405, 42);
            layoutSectionListToolStrip.TabIndex = 1;
            layoutSectionListToolStrip.Text = "toolStrip1";
            // 
            // importToolStripButton
            // 
            this.importToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.importToolStripButton.Enabled = false;
            this.importToolStripButton.Name = "importToolStripButton";
            this.importToolStripButton.Size = new System.Drawing.Size(104, 44);
            this.importToolStripButton.Text = "I&mport...";
            this.importToolStripButton.Click += new System.EventHandler(this.importToolStripButton_Click);
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteToolStripButton.Enabled = false;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(88, 44);
            this.deleteToolStripButton.Text = "&Delete";
            this.deleteToolStripButton.Click += new System.EventHandler(this.deleteToolStripButton_Click);
            // 
            // toolStripMenuItem13
            // 
            toolStripMenuItem13.Checked = true;
            toolStripMenuItem13.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripMenuItem13.Name = "toolStripMenuItem13";
            toolStripMenuItem13.Size = new System.Drawing.Size(160, 44);
            toolStripMenuItem13.Text = "1";
            // 
            // toolStripMenuItem14
            // 
            toolStripMenuItem14.Name = "toolStripMenuItem14";
            toolStripMenuItem14.Size = new System.Drawing.Size(160, 44);
            toolStripMenuItem14.Text = "2";
            // 
            // toolStripMenuItem15
            // 
            toolStripMenuItem15.Name = "toolStripMenuItem15";
            toolStripMenuItem15.Size = new System.Drawing.Size(160, 44);
            toolStripMenuItem15.Text = "3";
            // 
            // toolStripMenuItem16
            // 
            toolStripMenuItem16.Name = "toolStripMenuItem16";
            toolStripMenuItem16.Size = new System.Drawing.Size(160, 44);
            toolStripMenuItem16.Text = "4";
            // 
            // toolStripMenuItem17
            // 
            toolStripMenuItem17.Name = "toolStripMenuItem17";
            toolStripMenuItem17.Size = new System.Drawing.Size(160, 44);
            toolStripMenuItem17.Text = "5";
            // 
            // toolStripMenuItem18
            // 
            toolStripMenuItem18.Name = "toolStripMenuItem18";
            toolStripMenuItem18.Size = new System.Drawing.Size(160, 44);
            toolStripMenuItem18.Text = "6";
            // 
            // toolStripMenuItem19
            // 
            toolStripMenuItem19.Name = "toolStripMenuItem19";
            toolStripMenuItem19.Size = new System.Drawing.Size(160, 44);
            toolStripMenuItem19.Text = "7";
            // 
            // toolStripMenuItem20
            // 
            toolStripMenuItem20.Name = "toolStripMenuItem20";
            toolStripMenuItem20.Size = new System.Drawing.Size(160, 44);
            toolStripMenuItem20.Text = "8";
            // 
            // panel10
            // 
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(6, 48);
            this.panel10.Margin = new System.Windows.Forms.Padding(0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1586, 1173);
            this.panel10.TabIndex = 4;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.selectModToolStripMenuItem,
            this.editGameConfigToolStripMenuItem,
            this.changeLevelToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.buildAndRunToolStripMenuItem,
            this.recentProjectsToolStripMenuItem,
            this.recentModsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(71, 42);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(375, 44);
            this.openToolStripMenuItem.Text = "&Open Game...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // selectModToolStripMenuItem
            // 
            this.selectModToolStripMenuItem.Enabled = false;
            this.selectModToolStripMenuItem.Name = "selectModToolStripMenuItem";
            this.selectModToolStripMenuItem.Size = new System.Drawing.Size(375, 44);
            this.selectModToolStripMenuItem.Text = "Select &Mod";
            // 
            // editGameConfigToolStripMenuItem
            // 
            this.editGameConfigToolStripMenuItem.Enabled = false;
            this.editGameConfigToolStripMenuItem.Name = "editGameConfigToolStripMenuItem";
            this.editGameConfigToolStripMenuItem.Size = new System.Drawing.Size(375, 44);
            this.editGameConfigToolStripMenuItem.Text = "Edit &GameConfig...";
            this.editGameConfigToolStripMenuItem.Click += new System.EventHandler(this.editGameConfigToolStripMenuItem_Click);
            // 
            // changeLevelToolStripMenuItem
            // 
            this.changeLevelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.presentationStagesToolStripMenuItem,
            this.regularStagesToolStripMenuItem,
            this.specialStagesToolStripMenuItem,
            this.bonusStagesToolStripMenuItem});
            this.changeLevelToolStripMenuItem.Enabled = false;
            this.changeLevelToolStripMenuItem.Name = "changeLevelToolStripMenuItem";
            this.changeLevelToolStripMenuItem.Size = new System.Drawing.Size(375, 44);
            this.changeLevelToolStripMenuItem.Text = "&Change Level...";
            // 
            // presentationStagesToolStripMenuItem
            // 
            this.presentationStagesToolStripMenuItem.Name = "presentationStagesToolStripMenuItem";
            this.presentationStagesToolStripMenuItem.Size = new System.Drawing.Size(356, 44);
            this.presentationStagesToolStripMenuItem.Text = "Presentation Stages";
            // 
            // regularStagesToolStripMenuItem
            // 
            this.regularStagesToolStripMenuItem.Name = "regularStagesToolStripMenuItem";
            this.regularStagesToolStripMenuItem.Size = new System.Drawing.Size(356, 44);
            this.regularStagesToolStripMenuItem.Text = "Regular Stages";
            // 
            // specialStagesToolStripMenuItem
            // 
            this.specialStagesToolStripMenuItem.Name = "specialStagesToolStripMenuItem";
            this.specialStagesToolStripMenuItem.Size = new System.Drawing.Size(356, 44);
            this.specialStagesToolStripMenuItem.Text = "Special Stages";
            // 
            // bonusStagesToolStripMenuItem
            // 
            this.bonusStagesToolStripMenuItem.Name = "bonusStagesToolStripMenuItem";
            this.bonusStagesToolStripMenuItem.Size = new System.Drawing.Size(356, 44);
            this.bonusStagesToolStripMenuItem.Text = "Bonus Stages";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(375, 44);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // buildAndRunToolStripMenuItem
            // 
            this.buildAndRunToolStripMenuItem.Enabled = false;
            this.buildAndRunToolStripMenuItem.Name = "buildAndRunToolStripMenuItem";
            this.buildAndRunToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.buildAndRunToolStripMenuItem.Size = new System.Drawing.Size(375, 44);
            this.buildAndRunToolStripMenuItem.Text = "&Play...";
            this.buildAndRunToolStripMenuItem.Click += new System.EventHandler(this.buildAndRunToolStripMenuItem_Click);
            // 
            // recentProjectsToolStripMenuItem
            // 
            this.recentProjectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem2});
            this.recentProjectsToolStripMenuItem.Name = "recentProjectsToolStripMenuItem";
            this.recentProjectsToolStripMenuItem.Size = new System.Drawing.Size(375, 44);
            this.recentProjectsToolStripMenuItem.Text = "&Recent Games";
            this.recentProjectsToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.recentProjectsToolStripMenuItem_DropDownItemClicked);
            // 
            // noneToolStripMenuItem2
            // 
            this.noneToolStripMenuItem2.Enabled = false;
            this.noneToolStripMenuItem2.Name = "noneToolStripMenuItem2";
            this.noneToolStripMenuItem2.Size = new System.Drawing.Size(216, 44);
            this.noneToolStripMenuItem2.Text = "(none)";
            // 
            // recentModsToolStripMenuItem
            // 
            this.recentModsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem});
            this.recentModsToolStripMenuItem.Name = "recentModsToolStripMenuItem";
            this.recentModsToolStripMenuItem.Size = new System.Drawing.Size(375, 44);
            this.recentModsToolStripMenuItem.Text = "Recent Mo&ds";
            this.recentModsToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.recentModsToolStripMenuItem_DropDownItemClicked);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Enabled = false;
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(216, 44);
            this.noneToolStripMenuItem.Text = "(none)";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Alt+F4";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(375, 44);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.mainMenuStrip.Size = new System.Drawing.Size(1316, 48);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator2,
            this.gotoToolStripMenuItem,
            this.findToolStripMenuItem,
            this.findNextToolStripMenuItem,
            this.findPreviousToolStripMenuItem,
            this.toolStripSeparator3,
            this.clearLevelToolStripMenuItem,
            this.toolStripSeparator14,
            this.useHexadecimalToolStripMenuItem,
            this.switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem});
            this.editToolStripMenuItem.Enabled = false;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(74, 42);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Enabled = false;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(553, 44);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.undoToolStripMenuItem_DropDownItemClicked);
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Y";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(553, 44);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.redoToolStripMenuItem_DropDownItemClicked);
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(550, 6);
            // 
            // gotoToolStripMenuItem
            // 
            this.gotoToolStripMenuItem.Name = "gotoToolStripMenuItem";
            this.gotoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.gotoToolStripMenuItem.Size = new System.Drawing.Size(553, 44);
            this.gotoToolStripMenuItem.Text = "&Go To..";
            this.gotoToolStripMenuItem.Click += new System.EventHandler(this.gotoToolStripMenuItem_Click);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size(553, 44);
            this.findToolStripMenuItem.Text = "&Find...";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // findNextToolStripMenuItem
            // 
            this.findNextToolStripMenuItem.Enabled = false;
            this.findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            this.findNextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.findNextToolStripMenuItem.Size = new System.Drawing.Size(553, 44);
            this.findNextToolStripMenuItem.Text = "Find &Next";
            this.findNextToolStripMenuItem.Click += new System.EventHandler(this.findNextToolStripMenuItem_Click);
            // 
            // findPreviousToolStripMenuItem
            // 
            this.findPreviousToolStripMenuItem.Enabled = false;
            this.findPreviousToolStripMenuItem.Name = "findPreviousToolStripMenuItem";
            this.findPreviousToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.findPreviousToolStripMenuItem.Size = new System.Drawing.Size(553, 44);
            this.findPreviousToolStripMenuItem.Text = "Find &Previous";
            this.findPreviousToolStripMenuItem.Click += new System.EventHandler(this.findPreviousToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(550, 6);
            // 
            // clearLevelToolStripMenuItem
            // 
            this.clearLevelToolStripMenuItem.Name = "clearLevelToolStripMenuItem";
            this.clearLevelToolStripMenuItem.Size = new System.Drawing.Size(553, 44);
            this.clearLevelToolStripMenuItem.Text = "&Clear Level";
            this.clearLevelToolStripMenuItem.Click += new System.EventHandler(this.clearLevelToolStripMenuItem_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(550, 6);
            // 
            // useHexadecimalToolStripMenuItem
            // 
            this.useHexadecimalToolStripMenuItem.Checked = true;
            this.useHexadecimalToolStripMenuItem.CheckOnClick = true;
            this.useHexadecimalToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useHexadecimalToolStripMenuItem.Name = "useHexadecimalToolStripMenuItem";
            this.useHexadecimalToolStripMenuItem.Size = new System.Drawing.Size(553, 44);
            this.useHexadecimalToolStripMenuItem.Text = "Use Hexadecimal for art IDs";
            this.useHexadecimalToolStripMenuItem.CheckedChanged += new System.EventHandler(this.useHexadecimalToolStripMenuItem_CheckedChanged);
            // 
            // switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem
            // 
            this.switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem.CheckOnClick = true;
            this.switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem.Name = "switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem";
            this.switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem.Size = new System.Drawing.Size(553, 44);
            this.switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem.Text = "Switch &mouse buttons in chunk editor";
            this.switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem_CheckedChanged);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectsAboveHighPlaneToolStripMenuItem,
            this.hUDToolStripMenuItem,
            this.bgColorToolStripMenuItem,
            this.gridColorToolStripMenuItem,
            this.layersToolStripMenuItem,
            this.collisionToolStripMenuItem,
            this.zoomToolStripMenuItem,
            this.toolStripSeparator4,
            this.usageCountsToolStripMenuItem,
            this.logToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(85, 42);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // objectsAboveHighPlaneToolStripMenuItem
            // 
            this.objectsAboveHighPlaneToolStripMenuItem.Name = "objectsAboveHighPlaneToolStripMenuItem";
            this.objectsAboveHighPlaneToolStripMenuItem.ShortcutKeyDisplayString = "T";
            this.objectsAboveHighPlaneToolStripMenuItem.Size = new System.Drawing.Size(447, 44);
            this.objectsAboveHighPlaneToolStripMenuItem.Text = "&Objects above high plane";
            this.objectsAboveHighPlaneToolStripMenuItem.CheckedChanged += new System.EventHandler(this.objectsAboveHighPlaneToolStripMenuItem_CheckedChanged);
            this.objectsAboveHighPlaneToolStripMenuItem.Click += new System.EventHandler(this.objectsAboveHighPlaneToolStripMenuItem_Click);
            // 
            // hUDToolStripMenuItem
            // 
            this.hUDToolStripMenuItem.Checked = true;
            this.hUDToolStripMenuItem.CheckOnClick = true;
            this.hUDToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hUDToolStripMenuItem.Name = "hUDToolStripMenuItem";
            this.hUDToolStripMenuItem.ShortcutKeyDisplayString = "O";
            this.hUDToolStripMenuItem.Size = new System.Drawing.Size(447, 44);
            this.hUDToolStripMenuItem.Text = "&HUD";
            // 
            // bgColorToolStripMenuItem
            // 
            this.bgColorToolStripMenuItem.Name = "bgColorToolStripMenuItem";
            this.bgColorToolStripMenuItem.Size = new System.Drawing.Size(447, 44);
            this.bgColorToolStripMenuItem.Text = "Background Color...";
            this.bgColorToolStripMenuItem.Click += new System.EventHandler(this.bgColorToolStripMenuItem_Click);
            // 
            // gridColorToolStripMenuItem
            // 
            this.gridColorToolStripMenuItem.Name = "gridColorToolStripMenuItem";
            this.gridColorToolStripMenuItem.Size = new System.Drawing.Size(447, 44);
            this.gridColorToolStripMenuItem.Text = "&Grid Color...";
            this.gridColorToolStripMenuItem.Click += new System.EventHandler(this.gridColorToolStripMenuItem_Click);
            // 
            // layersToolStripMenuItem
            // 
            this.layersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lowToolStripMenuItem,
            this.highToolStripMenuItem});
            this.layersToolStripMenuItem.Name = "layersToolStripMenuItem";
            this.layersToolStripMenuItem.Size = new System.Drawing.Size(447, 44);
            this.layersToolStripMenuItem.Text = "&Layers";
            // 
            // lowToolStripMenuItem
            // 
            this.lowToolStripMenuItem.Checked = true;
            this.lowToolStripMenuItem.CheckOnClick = true;
            this.lowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lowToolStripMenuItem.Name = "lowToolStripMenuItem";
            this.lowToolStripMenuItem.ShortcutKeyDisplayString = "Y";
            this.lowToolStripMenuItem.Size = new System.Drawing.Size(228, 44);
            this.lowToolStripMenuItem.Text = "&Low";
            this.lowToolStripMenuItem.CheckedChanged += new System.EventHandler(this.lowToolStripMenuItem_CheckedChanged);
            this.lowToolStripMenuItem.Click += new System.EventHandler(this.lowToolStripMenuItem_Click);
            // 
            // highToolStripMenuItem
            // 
            this.highToolStripMenuItem.Checked = true;
            this.highToolStripMenuItem.CheckOnClick = true;
            this.highToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.highToolStripMenuItem.Name = "highToolStripMenuItem";
            this.highToolStripMenuItem.ShortcutKeyDisplayString = "U";
            this.highToolStripMenuItem.Size = new System.Drawing.Size(228, 44);
            this.highToolStripMenuItem.Text = "&High";
            this.highToolStripMenuItem.CheckedChanged += new System.EventHandler(this.highToolStripMenuItem_CheckedChanged);
            this.highToolStripMenuItem.Click += new System.EventHandler(this.highToolStripMenuItem_Click);
            // 
            // collisionToolStripMenuItem
            // 
            this.collisionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem1,
            this.path1ToolStripMenuItem,
            this.path2ToolStripMenuItem,
            this.toolStripSeparator6,
            this.anglesToolStripMenuItem});
            this.collisionToolStripMenuItem.Name = "collisionToolStripMenuItem";
            this.collisionToolStripMenuItem.Size = new System.Drawing.Size(447, 44);
            this.collisionToolStripMenuItem.Text = "&Collision";
            this.collisionToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.collisionToolStripMenuItem_DropDownItemClicked);
            // 
            // noneToolStripMenuItem1
            // 
            this.noneToolStripMenuItem1.Checked = true;
            this.noneToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noneToolStripMenuItem1.Name = "noneToolStripMenuItem1";
            this.noneToolStripMenuItem1.ShortcutKeyDisplayString = "Q";
            this.noneToolStripMenuItem1.Size = new System.Drawing.Size(249, 44);
            this.noneToolStripMenuItem1.Text = "&None";
            // 
            // path1ToolStripMenuItem
            // 
            this.path1ToolStripMenuItem.Name = "path1ToolStripMenuItem";
            this.path1ToolStripMenuItem.ShortcutKeyDisplayString = "W";
            this.path1ToolStripMenuItem.Size = new System.Drawing.Size(249, 44);
            this.path1ToolStripMenuItem.Text = "Path &1";
            // 
            // path2ToolStripMenuItem
            // 
            this.path2ToolStripMenuItem.Name = "path2ToolStripMenuItem";
            this.path2ToolStripMenuItem.ShortcutKeyDisplayString = "E";
            this.path2ToolStripMenuItem.Size = new System.Drawing.Size(249, 44);
            this.path2ToolStripMenuItem.Text = "Path &2";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(246, 6);
            // 
            // anglesToolStripMenuItem
            // 
            this.anglesToolStripMenuItem.CheckOnClick = true;
            this.anglesToolStripMenuItem.Name = "anglesToolStripMenuItem";
            this.anglesToolStripMenuItem.ShortcutKeyDisplayString = "R";
            this.anglesToolStripMenuItem.Size = new System.Drawing.Size(249, 44);
            this.anglesToolStripMenuItem.Text = "Angles";
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xToolStripMenuItem7,
            this.xToolStripMenuItem6,
            this.xToolStripMenuItem,
            this.xToolStripMenuItem1,
            this.xToolStripMenuItem2,
            this.xToolStripMenuItem3,
            this.xToolStripMenuItem4,
            this.xToolStripMenuItem5});
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.ShortcutKeyDisplayString = "+ -";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(447, 44);
            this.zoomToolStripMenuItem.Text = "&Zoom";
            this.zoomToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.zoomToolStripMenuItem_DropDownItemClicked);
            // 
            // xToolStripMenuItem7
            // 
            this.xToolStripMenuItem7.Name = "xToolStripMenuItem7";
            this.xToolStripMenuItem7.Size = new System.Drawing.Size(193, 44);
            this.xToolStripMenuItem7.Text = "1/8x";
            // 
            // xToolStripMenuItem6
            // 
            this.xToolStripMenuItem6.Name = "xToolStripMenuItem6";
            this.xToolStripMenuItem6.Size = new System.Drawing.Size(193, 44);
            this.xToolStripMenuItem6.Text = "1/4x";
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.Size = new System.Drawing.Size(193, 44);
            this.xToolStripMenuItem.Text = "1/2x";
            // 
            // xToolStripMenuItem1
            // 
            this.xToolStripMenuItem1.Checked = true;
            this.xToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xToolStripMenuItem1.Name = "xToolStripMenuItem1";
            this.xToolStripMenuItem1.Size = new System.Drawing.Size(193, 44);
            this.xToolStripMenuItem1.Text = "1x";
            // 
            // xToolStripMenuItem2
            // 
            this.xToolStripMenuItem2.Name = "xToolStripMenuItem2";
            this.xToolStripMenuItem2.Size = new System.Drawing.Size(193, 44);
            this.xToolStripMenuItem2.Text = "2x";
            // 
            // xToolStripMenuItem3
            // 
            this.xToolStripMenuItem3.Name = "xToolStripMenuItem3";
            this.xToolStripMenuItem3.Size = new System.Drawing.Size(193, 44);
            this.xToolStripMenuItem3.Text = "3x";
            // 
            // xToolStripMenuItem4
            // 
            this.xToolStripMenuItem4.Name = "xToolStripMenuItem4";
            this.xToolStripMenuItem4.Size = new System.Drawing.Size(193, 44);
            this.xToolStripMenuItem4.Text = "4x";
            // 
            // xToolStripMenuItem5
            // 
            this.xToolStripMenuItem5.Name = "xToolStripMenuItem5";
            this.xToolStripMenuItem5.Size = new System.Drawing.Size(193, 44);
            this.xToolStripMenuItem5.Text = "5x";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(444, 6);
            // 
            // usageCountsToolStripMenuItem
            // 
            this.usageCountsToolStripMenuItem.Enabled = false;
            this.usageCountsToolStripMenuItem.Name = "usageCountsToolStripMenuItem";
            this.usageCountsToolStripMenuItem.Size = new System.Drawing.Size(447, 44);
            this.usageCountsToolStripMenuItem.Text = "&Usage Counts";
            this.usageCountsToolStripMenuItem.Click += new System.EventHandler(this.usageCountsToolStripMenuItem_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(447, 44);
            this.logToolStripMenuItem.Text = "&Log";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.logToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paletteToolStripMenuItem,
            this.tilesToolStripMenuItem,
            this.chunksToolStripMenuItem,
            this.foregroundToolStripMenuItem,
            this.backgroundToolStripMenuItem,
            toolStripSeparator1,
            this.transparentBackgroundToolStripMenuItem,
            this.hideDebugObjectsToolStripMenuItem,
            this.exportArtcollisionpriorityToolStripMenuItem});
            this.exportToolStripMenuItem.Enabled = false;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(101, 42);
            this.exportToolStripMenuItem.Text = "E&xport";
            // 
            // paletteToolStripMenuItem
            // 
            this.paletteToolStripMenuItem.Name = "paletteToolStripMenuItem";
            this.paletteToolStripMenuItem.Size = new System.Drawing.Size(444, 44);
            this.paletteToolStripMenuItem.Text = "&Palette";
            this.paletteToolStripMenuItem.Click += new System.EventHandler(this.paletteToolStripMenuItem_Click);
            // 
            // tilesToolStripMenuItem
            // 
            this.tilesToolStripMenuItem.Name = "tilesToolStripMenuItem";
            this.tilesToolStripMenuItem.Size = new System.Drawing.Size(444, 44);
            this.tilesToolStripMenuItem.Text = "&Tiles";
            this.tilesToolStripMenuItem.Click += new System.EventHandler(this.tilesToolStripMenuItem_Click);
            // 
            // chunksToolStripMenuItem
            // 
            this.chunksToolStripMenuItem.Name = "chunksToolStripMenuItem";
            this.chunksToolStripMenuItem.Size = new System.Drawing.Size(444, 44);
            this.chunksToolStripMenuItem.Text = "&Chunks";
            this.chunksToolStripMenuItem.Click += new System.EventHandler(this.chunksToolStripMenuItem_Click);
            // 
            // foregroundToolStripMenuItem
            // 
            this.foregroundToolStripMenuItem.Name = "foregroundToolStripMenuItem";
            this.foregroundToolStripMenuItem.Size = new System.Drawing.Size(444, 44);
            this.foregroundToolStripMenuItem.Text = "&Foreground";
            this.foregroundToolStripMenuItem.Click += new System.EventHandler(this.foregroundToolStripMenuItem_Click);
            // 
            // backgroundToolStripMenuItem
            // 
            this.backgroundToolStripMenuItem.Name = "backgroundToolStripMenuItem";
            this.backgroundToolStripMenuItem.Size = new System.Drawing.Size(444, 44);
            this.backgroundToolStripMenuItem.Text = "B&ackground";
            this.backgroundToolStripMenuItem.Click += new System.EventHandler(this.backgroundToolStripMenuItem_Click);
            // 
            // transparentBackgroundToolStripMenuItem
            // 
            this.transparentBackgroundToolStripMenuItem.Checked = true;
            this.transparentBackgroundToolStripMenuItem.CheckOnClick = true;
            this.transparentBackgroundToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.transparentBackgroundToolStripMenuItem.Name = "transparentBackgroundToolStripMenuItem";
            this.transparentBackgroundToolStripMenuItem.Size = new System.Drawing.Size(444, 44);
            this.transparentBackgroundToolStripMenuItem.Text = "T&ransparent background";
            this.transparentBackgroundToolStripMenuItem.CheckedChanged += new System.EventHandler(this.transparentBackgroundToolStripMenuItem_CheckedChanged);
            // 
            // hideDebugObjectsToolStripMenuItem
            // 
            this.hideDebugObjectsToolStripMenuItem.CheckOnClick = true;
            this.hideDebugObjectsToolStripMenuItem.Name = "hideDebugObjectsToolStripMenuItem";
            this.hideDebugObjectsToolStripMenuItem.Size = new System.Drawing.Size(444, 44);
            this.hideDebugObjectsToolStripMenuItem.Text = "&Hide Debug Objects";
            this.hideDebugObjectsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.hideDebugObjectsToolStripMenuItem_CheckedChanged);
            // 
            // exportArtcollisionpriorityToolStripMenuItem
            // 
            this.exportArtcollisionpriorityToolStripMenuItem.CheckOnClick = true;
            this.exportArtcollisionpriorityToolStripMenuItem.Name = "exportArtcollisionpriorityToolStripMenuItem";
            this.exportArtcollisionpriorityToolStripMenuItem.Size = new System.Drawing.Size(444, 44);
            this.exportArtcollisionpriorityToolStripMenuItem.Text = "Export art+collision+priority";
            this.exportArtcollisionpriorityToolStripMenuItem.CheckedChanged += new System.EventHandler(this.exportArtcollisionpriorityToolStripMenuItem_CheckedChanged);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewReadmeToolStripMenuItem,
            this.reportBugToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(84, 42);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // viewReadmeToolStripMenuItem
            // 
            this.viewReadmeToolStripMenuItem.Name = "viewReadmeToolStripMenuItem";
            this.viewReadmeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.viewReadmeToolStripMenuItem.Size = new System.Drawing.Size(330, 44);
            this.viewReadmeToolStripMenuItem.Text = "View &Readme";
            this.viewReadmeToolStripMenuItem.Click += new System.EventHandler(this.viewReadmeToolStripMenuItem_Click);
            // 
            // reportBugToolStripMenuItem
            // 
            this.reportBugToolStripMenuItem.Name = "reportBugToolStripMenuItem";
            this.reportBugToolStripMenuItem.Size = new System.Drawing.Size(330, 44);
            this.reportBugToolStripMenuItem.Text = "Report &Bug...";
            this.reportBugToolStripMenuItem.Click += new System.EventHandler(this.reportBugToolStripMenuItem_Click);
            // 
            // backgroundLevelLoader
            // 
            this.backgroundLevelLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundLevelLoader_DoWork);
            this.backgroundLevelLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundLevelLoader_RunWorkerCompleted);
            // 
            // objectContextMenuStrip
            // 
            this.objectContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.objectContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addObjectToolStripMenuItem,
            this.addGroupOfObjectsToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator5,
            this.selectAllObjectsToolStripMenuItem});
            this.objectContextMenuStrip.Name = "contextMenuStrip1";
            this.objectContextMenuStrip.Size = new System.Drawing.Size(340, 276);
            // 
            // addObjectToolStripMenuItem
            // 
            this.addObjectToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.mon;
            this.addObjectToolStripMenuItem.Name = "addObjectToolStripMenuItem";
            this.addObjectToolStripMenuItem.Size = new System.Drawing.Size(339, 38);
            this.addObjectToolStripMenuItem.Text = "Add &Object...";
            this.addObjectToolStripMenuItem.Click += new System.EventHandler(this.addObjectToolStripMenuItem_Click);
            // 
            // addGroupOfObjectsToolStripMenuItem
            // 
            this.addGroupOfObjectsToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.mon;
            this.addGroupOfObjectsToolStripMenuItem.Name = "addGroupOfObjectsToolStripMenuItem";
            this.addGroupOfObjectsToolStripMenuItem.Size = new System.Drawing.Size(339, 38);
            this.addGroupOfObjectsToolStripMenuItem.Text = "Add Group of O&bjects...";
            this.addGroupOfObjectsToolStripMenuItem.Click += new System.EventHandler(this.addGroupOfObjectsToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.cut;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(339, 38);
            this.cutToolStripMenuItem.Text = "Cu&t";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.copy;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(339, 38);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.paste;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(339, 38);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(339, 38);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(336, 6);
            // 
            // selectAllObjectsToolStripMenuItem
            // 
            this.selectAllObjectsToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.mon;
            this.selectAllObjectsToolStripMenuItem.Name = "selectAllObjectsToolStripMenuItem";
            this.selectAllObjectsToolStripMenuItem.Size = new System.Drawing.Size(339, 38);
            this.selectAllObjectsToolStripMenuItem.Text = "&Select All Objects";
            this.selectAllObjectsToolStripMenuItem.Click += new System.EventHandler(this.selectAllObjectsToolStripMenuItem_Click);
            // 
            // ObjectProperties
            // 
            this.ObjectProperties.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ObjectProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObjectProperties.LineColor = System.Drawing.SystemColors.ControlDark;
            this.ObjectProperties.Location = new System.Drawing.Point(6, 6);
            this.ObjectProperties.Margin = new System.Windows.Forms.Padding(0);
            this.ObjectProperties.Name = "ObjectProperties";
            this.ObjectProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.ObjectProperties.Size = new System.Drawing.Size(335, 860);
            this.ObjectProperties.TabIndex = 12;
            this.ObjectProperties.ToolbarVisible = false;
            this.ObjectProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.ObjectProperties_PropertyValueChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage15);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 48);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1316, 966);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1300, 919);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Objects";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl5);
            this.splitContainer1.Size = new System.Drawing.Size(1300, 919);
            this.splitContainer1.SplitterDistance = 929;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(6);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.objectTypeList);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.objectPanel);
            this.splitContainer4.Panel2.Controls.Add(this.objToolStrip);
            this.splitContainer4.Size = new System.Drawing.Size(929, 919);
            this.splitContainer4.SplitterDistance = 104;
            this.splitContainer4.SplitterWidth = 8;
            this.splitContainer4.TabIndex = 3;
            // 
            // objectTypeList
            // 
            this.objectTypeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectTypeList.HideSelection = false;
            this.objectTypeList.LargeImageList = this.objectTypeImages;
            this.objectTypeList.Location = new System.Drawing.Point(0, 0);
            this.objectTypeList.Margin = new System.Windows.Forms.Padding(6);
            this.objectTypeList.MultiSelect = false;
            this.objectTypeList.Name = "objectTypeList";
            this.objectTypeList.Size = new System.Drawing.Size(104, 919);
            this.objectTypeList.TabIndex = 0;
            this.objectTypeList.UseCompatibleStateImageBehavior = false;
            this.objectTypeList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.objectTypeList_ItemDrag);
            // 
            // objectTypeImages
            // 
            this.objectTypeImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.objectTypeImages.ImageSize = new System.Drawing.Size(32, 32);
            this.objectTypeImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // objToolStrip
            // 
            this.objToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.objToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.objToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objGridSizeDropDownButton,
            this.showGridToolStripCheckBoxButton,
            this.snapObjectsToolStripCheckBoxButton,
            toolStripSeparator10,
            this.alignLeftWallToolStripButton,
            this.alignGroundToolStripButton,
            this.alignRightWallToolStripButton,
            this.alignCeilingToolStripButton,
            toolStripSeparator8,
            this.alignLeftsToolStripButton,
            this.alignCentersToolStripButton,
            this.alignRightsToolStripButton,
            toolStripSeparator9,
            this.alignTopsToolStripButton,
            this.alignMiddlesToolStripButton,
            this.alignBottomsToolStripButton});
            this.objToolStrip.Location = new System.Drawing.Point(0, 0);
            this.objToolStrip.Name = "objToolStrip";
            this.objToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.objToolStrip.Size = new System.Drawing.Size(817, 42);
            this.objToolStrip.TabIndex = 4;
            this.objToolStrip.Text = "toolStrip1";
            // 
            // objGridSizeDropDownButton
            // 
            this.objGridSizeDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.objGridSizeDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11,
            this.toolStripMenuItem3,
            this.toolStripMenuItem12});
            this.objGridSizeDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("objGridSizeDropDownButton.Image")));
            this.objGridSizeDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.objGridSizeDropDownButton.Name = "objGridSizeDropDownButton";
            this.objGridSizeDropDownButton.Size = new System.Drawing.Size(155, 36);
            this.objGridSizeDropDownButton.Text = "Grid Size: 1";
            this.objGridSizeDropDownButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.objGridSizeDropDownButton_DropDownItemClicked);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(199, 44);
            this.toolStripMenuItem2.Text = "1";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(199, 44);
            this.toolStripMenuItem4.Text = "2";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(199, 44);
            this.toolStripMenuItem5.Text = "4";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(199, 44);
            this.toolStripMenuItem6.Text = "8";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(199, 44);
            this.toolStripMenuItem7.Text = "16";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(199, 44);
            this.toolStripMenuItem8.Text = "32";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(199, 44);
            this.toolStripMenuItem9.Text = "64";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(199, 44);
            this.toolStripMenuItem10.Text = "128";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(199, 44);
            this.toolStripMenuItem11.Text = "256";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(199, 44);
            this.toolStripMenuItem3.Text = "512";
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(199, 44);
            this.toolStripMenuItem12.Text = "1024";
            // 
            // alignLeftWallToolStripButton
            // 
            this.alignLeftWallToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignLeftWallToolStripButton.Enabled = false;
            this.alignLeftWallToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("alignLeftWallToolStripButton.Image")));
            this.alignLeftWallToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignLeftWallToolStripButton.Name = "alignLeftWallToolStripButton";
            this.alignLeftWallToolStripButton.Size = new System.Drawing.Size(46, 36);
            this.alignLeftWallToolStripButton.Text = "Align with Left Wall";
            this.alignLeftWallToolStripButton.Click += new System.EventHandler(this.alignLeftWallToolStripButton_Click);
            // 
            // alignGroundToolStripButton
            // 
            this.alignGroundToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignGroundToolStripButton.Enabled = false;
            this.alignGroundToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("alignGroundToolStripButton.Image")));
            this.alignGroundToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignGroundToolStripButton.Name = "alignGroundToolStripButton";
            this.alignGroundToolStripButton.Size = new System.Drawing.Size(46, 36);
            this.alignGroundToolStripButton.Text = "Align with Ground";
            this.alignGroundToolStripButton.Click += new System.EventHandler(this.alignGroundToolStripButton_Click);
            // 
            // alignRightWallToolStripButton
            // 
            this.alignRightWallToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignRightWallToolStripButton.Enabled = false;
            this.alignRightWallToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("alignRightWallToolStripButton.Image")));
            this.alignRightWallToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignRightWallToolStripButton.Name = "alignRightWallToolStripButton";
            this.alignRightWallToolStripButton.Size = new System.Drawing.Size(46, 36);
            this.alignRightWallToolStripButton.Text = "Align with Right Wall";
            this.alignRightWallToolStripButton.Click += new System.EventHandler(this.alignRightWallToolStripButton_Click);
            // 
            // alignCeilingToolStripButton
            // 
            this.alignCeilingToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignCeilingToolStripButton.Enabled = false;
            this.alignCeilingToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("alignCeilingToolStripButton.Image")));
            this.alignCeilingToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignCeilingToolStripButton.Name = "alignCeilingToolStripButton";
            this.alignCeilingToolStripButton.Size = new System.Drawing.Size(46, 36);
            this.alignCeilingToolStripButton.Text = "Align with Ceiling";
            this.alignCeilingToolStripButton.Click += new System.EventHandler(this.alignCeilingToolStripButton_Click);
            // 
            // alignLeftsToolStripButton
            // 
            this.alignLeftsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignLeftsToolStripButton.Enabled = false;
            this.alignLeftsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("alignLeftsToolStripButton.Image")));
            this.alignLeftsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignLeftsToolStripButton.Name = "alignLeftsToolStripButton";
            this.alignLeftsToolStripButton.Size = new System.Drawing.Size(46, 36);
            this.alignLeftsToolStripButton.Text = "Align Lefts";
            this.alignLeftsToolStripButton.Click += new System.EventHandler(this.alignLeftsToolStripButton_Click);
            // 
            // alignCentersToolStripButton
            // 
            this.alignCentersToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignCentersToolStripButton.Enabled = false;
            this.alignCentersToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("alignCentersToolStripButton.Image")));
            this.alignCentersToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignCentersToolStripButton.Name = "alignCentersToolStripButton";
            this.alignCentersToolStripButton.Size = new System.Drawing.Size(46, 36);
            this.alignCentersToolStripButton.Text = "Align Centers";
            this.alignCentersToolStripButton.Click += new System.EventHandler(this.alignCentersToolStripButton_Click);
            // 
            // alignRightsToolStripButton
            // 
            this.alignRightsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignRightsToolStripButton.Enabled = false;
            this.alignRightsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("alignRightsToolStripButton.Image")));
            this.alignRightsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignRightsToolStripButton.Name = "alignRightsToolStripButton";
            this.alignRightsToolStripButton.Size = new System.Drawing.Size(46, 24);
            this.alignRightsToolStripButton.Text = "Align Rights";
            this.alignRightsToolStripButton.Click += new System.EventHandler(this.alignRightsToolStripButton_Click);
            // 
            // alignTopsToolStripButton
            // 
            this.alignTopsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignTopsToolStripButton.Enabled = false;
            this.alignTopsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("alignTopsToolStripButton.Image")));
            this.alignTopsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignTopsToolStripButton.Name = "alignTopsToolStripButton";
            this.alignTopsToolStripButton.Size = new System.Drawing.Size(46, 24);
            this.alignTopsToolStripButton.Text = "Align Tops";
            this.alignTopsToolStripButton.Click += new System.EventHandler(this.alignTopsToolStripButton_Click);
            // 
            // alignMiddlesToolStripButton
            // 
            this.alignMiddlesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignMiddlesToolStripButton.Enabled = false;
            this.alignMiddlesToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("alignMiddlesToolStripButton.Image")));
            this.alignMiddlesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignMiddlesToolStripButton.Name = "alignMiddlesToolStripButton";
            this.alignMiddlesToolStripButton.Size = new System.Drawing.Size(46, 24);
            this.alignMiddlesToolStripButton.Text = "Align Middles";
            this.alignMiddlesToolStripButton.Click += new System.EventHandler(this.alignMiddlesToolStripButton_Click);
            // 
            // alignBottomsToolStripButton
            // 
            this.alignBottomsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignBottomsToolStripButton.Enabled = false;
            this.alignBottomsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("alignBottomsToolStripButton.Image")));
            this.alignBottomsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignBottomsToolStripButton.Name = "alignBottomsToolStripButton";
            this.alignBottomsToolStripButton.Size = new System.Drawing.Size(46, 24);
            this.alignBottomsToolStripButton.Text = "Align Bottoms";
            this.alignBottomsToolStripButton.Click += new System.EventHandler(this.alignBottomsToolStripButton_Click);
            // 
            // tabControl5
            // 
            this.tabControl5.Controls.Add(this.tabPage5);
            this.tabControl5.Controls.Add(this.tabPage7);
            this.tabControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl5.Location = new System.Drawing.Point(0, 0);
            this.tabControl5.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl5.Name = "tabControl5";
            this.tabControl5.SelectedIndex = 0;
            this.tabControl5.Size = new System.Drawing.Size(363, 919);
            this.tabControl5.TabIndex = 13;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.ObjectProperties);
            this.tabPage5.Location = new System.Drawing.Point(8, 39);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage5.Size = new System.Drawing.Size(347, 872);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Properties";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.objectOrder);
            this.tabPage7.Location = new System.Drawing.Point(8, 39);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage7.Size = new System.Drawing.Size(347, 878);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "Order";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // objectOrder
            // 
            this.objectOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.objectOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectOrder.FullRowSelect = true;
            this.objectOrder.HideSelection = false;
            this.objectOrder.LargeImageList = this.objectTypeImages;
            this.objectOrder.Location = new System.Drawing.Point(6, 6);
            this.objectOrder.Margin = new System.Windows.Forms.Padding(6);
            this.objectOrder.Name = "objectOrder";
            this.objectOrder.Size = new System.Drawing.Size(335, 866);
            this.objectOrder.TabIndex = 0;
            this.objectOrder.UseCompatibleStateImageBehavior = false;
            this.objectOrder.View = System.Windows.Forms.View.Tile;
            this.objectOrder.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.objectOrder_ItemDrag);
            this.objectOrder.SelectedIndexChanged += new System.EventHandler(this.objectOrder_SelectedIndexChanged);
            this.objectOrder.DragDrop += new System.Windows.Forms.DragEventHandler(this.objectOrder_DragDrop);
            this.objectOrder.DragEnter += new System.Windows.Forms.DragEventHandler(this.objectOrder_DragEnter);
            this.objectOrder.DragOver += new System.Windows.Forms.DragEventHandler(this.objectOrder_DragOver);
            this.objectOrder.DragLeave += new System.EventHandler(this.objectOrder_DragLeave);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1300, 925);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Foreground";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.foregroundPanel);
            this.splitContainer2.Panel1.Controls.Add(this.fgToolStrip);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer2.Size = new System.Drawing.Size(1300, 925);
            this.splitContainer2.SplitterDistance = 871;
            this.splitContainer2.SplitterWidth = 8;
            this.splitContainer2.TabIndex = 4;
            // 
            // fgToolStrip
            // 
            this.fgToolStrip.Enabled = false;
            this.fgToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.fgToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.fgToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayObjectsToolStripCheckBoxButton,
            this.resizeForegroundToolStripButton,
            this.replaceForegroundToolStripButton,
            this.clearForegroundToolStripButton});
            this.fgToolStrip.Location = new System.Drawing.Point(0, 0);
            this.fgToolStrip.Name = "fgToolStrip";
            this.fgToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.fgToolStrip.Size = new System.Drawing.Size(871, 42);
            this.fgToolStrip.TabIndex = 4;
            this.fgToolStrip.Text = "toolStrip1";
            // 
            // resizeForegroundToolStripButton
            // 
            this.resizeForegroundToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.resizeForegroundToolStripButton.Name = "resizeForegroundToolStripButton";
            this.resizeForegroundToolStripButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.resizeForegroundToolStripButton.Size = new System.Drawing.Size(84, 44);
            this.resizeForegroundToolStripButton.Text = "Resize";
            this.resizeForegroundToolStripButton.ToolTipText = "Change the size of the Foreground.";
            this.resizeForegroundToolStripButton.Click += new System.EventHandler(this.resizeLayerToolStripButton_Click);
            // 
            // replaceForegroundToolStripButton
            // 
            this.replaceForegroundToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.replaceForegroundToolStripButton.Name = "replaceForegroundToolStripButton";
            this.replaceForegroundToolStripButton.Size = new System.Drawing.Size(100, 44);
            this.replaceForegroundToolStripButton.Text = "Replace";
            this.replaceForegroundToolStripButton.ToolTipText = "Replace all instances of a certain chunk in the Foreground.";
            this.replaceForegroundToolStripButton.Click += new System.EventHandler(this.replaceForegroundToolStripButton_Click);
            // 
            // clearForegroundToolStripButton
            // 
            this.clearForegroundToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearForegroundToolStripButton.Name = "clearForegroundToolStripButton";
            this.clearForegroundToolStripButton.Size = new System.Drawing.Size(72, 44);
            this.clearForegroundToolStripButton.Text = "Clear";
            this.clearForegroundToolStripButton.ToolTipText = "Clear all chunks in the Foreground.";
            this.clearForegroundToolStripButton.Click += new System.EventHandler(this.clearForegroundToolStripButton_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(421, 925);
            this.tabControl2.TabIndex = 2;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.ChunkSelector);
            this.tabPage8.Location = new System.Drawing.Point(8, 39);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(405, 878);
            this.tabPage8.TabIndex = 0;
            this.tabPage8.Text = "Chunks";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // ChunkSelector
            // 
            this.ChunkSelector.BackColor = System.Drawing.SystemColors.Window;
            this.ChunkSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChunkSelector.ImageHeight = 128;
            this.ChunkSelector.ImageSize = 128;
            this.ChunkSelector.ImageWidth = 128;
            this.ChunkSelector.Location = new System.Drawing.Point(0, 0);
            this.ChunkSelector.Margin = new System.Windows.Forms.Padding(0);
            this.ChunkSelector.Name = "ChunkSelector";
            this.ChunkSelector.ScrollValue = 0;
            this.ChunkSelector.SelectedIndex = -1;
            this.ChunkSelector.Size = new System.Drawing.Size(405, 878);
            this.ChunkSelector.TabIndex = 1;
            this.ChunkSelector.SelectedIndexChanged += new System.EventHandler(this.ChunkSelector_SelectedIndexChanged);
            this.ChunkSelector.ItemDrag += new System.EventHandler(this.ChunkSelector_ItemDrag);
            this.ChunkSelector.DragDrop += new System.Windows.Forms.DragEventHandler(this.ChunkSelector_DragDrop);
            this.ChunkSelector.DragEnter += new System.Windows.Forms.DragEventHandler(this.ChunkSelector_DragEnter);
            this.ChunkSelector.DragOver += new System.Windows.Forms.DragEventHandler(this.ChunkSelector_DragOver);
            this.ChunkSelector.DragLeave += new System.EventHandler(this.ChunkSelector_DragLeave);
            this.ChunkSelector.Paint += new System.Windows.Forms.PaintEventHandler(this.ChunkSelector_Paint);
            this.ChunkSelector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TileList_KeyDown);
            this.ChunkSelector.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChunkSelector_MouseDown);
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.layoutSectionSplitContainer);
            this.tabPage9.Location = new System.Drawing.Point(8, 39);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(405, 878);
            this.tabPage9.TabIndex = 1;
            this.tabPage9.Text = "Layout Sections";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // layoutSectionSplitContainer
            // 
            this.layoutSectionSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSectionSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.layoutSectionSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.layoutSectionSplitContainer.Name = "layoutSectionSplitContainer";
            this.layoutSectionSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // layoutSectionSplitContainer.Panel1
            // 
            this.layoutSectionSplitContainer.Panel1.Controls.Add(this.layoutSectionListBox);
            this.layoutSectionSplitContainer.Panel1.Controls.Add(layoutSectionListToolStrip);
            // 
            // layoutSectionSplitContainer.Panel2
            // 
            this.layoutSectionSplitContainer.Panel2.Controls.Add(this.layoutSectionPreview);
            this.layoutSectionSplitContainer.Size = new System.Drawing.Size(405, 878);
            this.layoutSectionSplitContainer.SplitterDistance = 372;
            this.layoutSectionSplitContainer.SplitterWidth = 8;
            this.layoutSectionSplitContainer.TabIndex = 0;
            // 
            // layoutSectionListBox
            // 
            this.layoutSectionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSectionListBox.FormattingEnabled = true;
            this.layoutSectionListBox.IntegralHeight = false;
            this.layoutSectionListBox.ItemHeight = 25;
            this.layoutSectionListBox.Location = new System.Drawing.Point(0, 42);
            this.layoutSectionListBox.Margin = new System.Windows.Forms.Padding(6);
            this.layoutSectionListBox.Name = "layoutSectionListBox";
            this.layoutSectionListBox.Size = new System.Drawing.Size(405, 330);
            this.layoutSectionListBox.TabIndex = 0;
            this.layoutSectionListBox.SelectedIndexChanged += new System.EventHandler(this.layoutSectionListBox_SelectedIndexChanged);
            this.layoutSectionListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.layoutSectionListBox_KeyDown);
            // 
            // layoutSectionPreview
            // 
            this.layoutSectionPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSectionPreview.Location = new System.Drawing.Point(0, 0);
            this.layoutSectionPreview.Margin = new System.Windows.Forms.Padding(0);
            this.layoutSectionPreview.Name = "layoutSectionPreview";
            this.layoutSectionPreview.Size = new System.Drawing.Size(405, 498);
            this.layoutSectionPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.layoutSectionPreview.TabIndex = 0;
            this.layoutSectionPreview.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer3);
            this.tabPage3.Location = new System.Drawing.Point(8, 39);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1300, 925);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Background";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.backgroundPanel);
            this.splitContainer3.Panel1.Controls.Add(this.bgToolStrip);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl3);
            this.splitContainer3.Size = new System.Drawing.Size(1300, 925);
            this.splitContainer3.SplitterDistance = 767;
            this.splitContainer3.SplitterWidth = 8;
            this.splitContainer3.TabIndex = 4;
            // 
            // bgToolStrip
            // 
            this.bgToolStrip.Enabled = false;
            this.bgToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bgToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bgToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bgLayerDropDown,
            this.resizeBackgroundToolStripButton,
            this.replaceBackgroundToolStripButton,
            this.clearBackgroundToolStripButton,
            this.bgDuplicateLayerOverToolStripButton});
            this.bgToolStrip.Location = new System.Drawing.Point(0, 0);
            this.bgToolStrip.Name = "bgToolStrip";
            this.bgToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.bgToolStrip.Size = new System.Drawing.Size(767, 42);
            this.bgToolStrip.TabIndex = 5;
            this.bgToolStrip.Text = "toolStrip1";
            // 
            // bgLayerDropDown
            // 
            this.bgLayerDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bgLayerDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripMenuItem13,
            toolStripMenuItem14,
            toolStripMenuItem15,
            toolStripMenuItem16,
            toolStripMenuItem17,
            toolStripMenuItem18,
            toolStripMenuItem19,
            toolStripMenuItem20});
            this.bgLayerDropDown.Name = "bgLayerDropDown";
            this.bgLayerDropDown.Size = new System.Drawing.Size(117, 44);
            this.bgLayerDropDown.Text = "Layer: 1";
            this.bgLayerDropDown.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bgLayerDropDown_DropDownItemClicked);
            // 
            // resizeBackgroundToolStripButton
            // 
            this.resizeBackgroundToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.resizeBackgroundToolStripButton.Name = "resizeBackgroundToolStripButton";
            this.resizeBackgroundToolStripButton.Size = new System.Drawing.Size(84, 44);
            this.resizeBackgroundToolStripButton.Text = "Resize";
            this.resizeBackgroundToolStripButton.ToolTipText = "Change the size of the current layer.";
            this.resizeBackgroundToolStripButton.Click += new System.EventHandler(this.resizeLayerToolStripButton_Click);
            // 
            // replaceBackgroundToolStripButton
            // 
            this.replaceBackgroundToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.replaceBackgroundToolStripButton.Name = "replaceBackgroundToolStripButton";
            this.replaceBackgroundToolStripButton.Size = new System.Drawing.Size(100, 44);
            this.replaceBackgroundToolStripButton.Text = "Replace";
            this.replaceBackgroundToolStripButton.ToolTipText = "Replace all instances of a certain chunk in the current layer.";
            this.replaceBackgroundToolStripButton.Click += new System.EventHandler(this.replaceBackgroundToolStripButton_Click);
            // 
            // clearBackgroundToolStripButton
            // 
            this.clearBackgroundToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearBackgroundToolStripButton.Name = "clearBackgroundToolStripButton";
            this.clearBackgroundToolStripButton.Size = new System.Drawing.Size(72, 44);
            this.clearBackgroundToolStripButton.Text = "Clear";
            this.clearBackgroundToolStripButton.ToolTipText = "Clear all chunks and scrolling data for this layer.";
            this.clearBackgroundToolStripButton.Click += new System.EventHandler(this.clearBackgroundToolStripButton_Click);
            // 
            // bgDuplicateLayerOverToolStripButton
            // 
            this.bgDuplicateLayerOverToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bgDuplicateLayerOverToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layer1ToolStripMenuItem,
            this.layer2ToolStripMenuItem,
            this.layer3ToolStripMenuItem,
            this.layer4ToolStripMenuItem,
            this.layer5ToolStripMenuItem,
            this.layer6ToolStripMenuItem,
            this.layer7ToolStripMenuItem,
            this.layer8ToolStripMenuItem});
            this.bgDuplicateLayerOverToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bgDuplicateLayerOverToolStripButton.Name = "bgDuplicateLayerOverToolStripButton";
            this.bgDuplicateLayerOverToolStripButton.Size = new System.Drawing.Size(210, 44);
            this.bgDuplicateLayerOverToolStripButton.Text = "Duplicate Over...";
            this.bgDuplicateLayerOverToolStripButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.bgDuplicateLayerOverToolStripButton_DropDownItemClicked);
            // 
            // layer1ToolStripMenuItem
            // 
            this.layer1ToolStripMenuItem.Name = "layer1ToolStripMenuItem";
            this.layer1ToolStripMenuItem.Size = new System.Drawing.Size(223, 44);
            this.layer1ToolStripMenuItem.Text = "Layer 1";
            this.layer1ToolStripMenuItem.Visible = false;
            // 
            // layer2ToolStripMenuItem
            // 
            this.layer2ToolStripMenuItem.Name = "layer2ToolStripMenuItem";
            this.layer2ToolStripMenuItem.Size = new System.Drawing.Size(223, 44);
            this.layer2ToolStripMenuItem.Text = "Layer 2";
            // 
            // layer3ToolStripMenuItem
            // 
            this.layer3ToolStripMenuItem.Name = "layer3ToolStripMenuItem";
            this.layer3ToolStripMenuItem.Size = new System.Drawing.Size(223, 44);
            this.layer3ToolStripMenuItem.Text = "Layer 3";
            // 
            // layer4ToolStripMenuItem
            // 
            this.layer4ToolStripMenuItem.Name = "layer4ToolStripMenuItem";
            this.layer4ToolStripMenuItem.Size = new System.Drawing.Size(223, 44);
            this.layer4ToolStripMenuItem.Text = "Layer 4";
            // 
            // layer5ToolStripMenuItem
            // 
            this.layer5ToolStripMenuItem.Name = "layer5ToolStripMenuItem";
            this.layer5ToolStripMenuItem.Size = new System.Drawing.Size(223, 44);
            this.layer5ToolStripMenuItem.Text = "Layer 5";
            // 
            // layer6ToolStripMenuItem
            // 
            this.layer6ToolStripMenuItem.Name = "layer6ToolStripMenuItem";
            this.layer6ToolStripMenuItem.Size = new System.Drawing.Size(223, 44);
            this.layer6ToolStripMenuItem.Text = "Layer 6";
            // 
            // layer7ToolStripMenuItem
            // 
            this.layer7ToolStripMenuItem.Name = "layer7ToolStripMenuItem";
            this.layer7ToolStripMenuItem.Size = new System.Drawing.Size(223, 44);
            this.layer7ToolStripMenuItem.Text = "Layer 7";
            // 
            // layer8ToolStripMenuItem
            // 
            this.layer8ToolStripMenuItem.Name = "layer8ToolStripMenuItem";
            this.layer8ToolStripMenuItem.Size = new System.Drawing.Size(223, 44);
            this.layer8ToolStripMenuItem.Text = "Layer 8";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage10);
            this.tabControl3.Controls.Add(this.tabPage11);
            this.tabControl3.Controls.Add(this.tabPage13);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(525, 925);
            this.tabControl3.TabIndex = 0;
            this.tabControl3.SelectedIndexChanged += new System.EventHandler(this.tabControl3_SelectedIndexChanged);
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new System.Drawing.Point(8, 39);
            this.tabPage10.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(509, 878);
            this.tabPage10.TabIndex = 0;
            this.tabPage10.Text = "Chunks";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // tabPage11
            // 
            this.tabPage11.Location = new System.Drawing.Point(8, 39);
            this.tabPage11.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(509, 878);
            this.tabPage11.TabIndex = 1;
            this.tabPage11.Text = "Layout Sections";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this.previewGroupBox);
            this.tabPage13.Controls.Add(this.scrollEditPanel);
            this.tabPage13.Controls.Add(this.layerScrollType);
            this.tabPage13.Controls.Add(this.label8);
            this.tabPage13.Location = new System.Drawing.Point(8, 39);
            this.tabPage13.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage13.Size = new System.Drawing.Size(509, 878);
            this.tabPage13.TabIndex = 2;
            this.tabPage13.Text = "Scrolling";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // previewGroupBox
            // 
            this.previewGroupBox.AutoSize = true;
            this.previewGroupBox.Controls.Add(this.scrollCamX);
            this.previewGroupBox.Controls.Add(this.scrollPreviewButton);
            this.previewGroupBox.Controls.Add(this.label15);
            this.previewGroupBox.Controls.Add(this.moveCameraLabel);
            this.previewGroupBox.Controls.Add(this.scrollCamY);
            this.previewGroupBox.Enabled = false;
            this.previewGroupBox.Location = new System.Drawing.Point(17, 818);
            this.previewGroupBox.Name = "previewGroupBox";
            this.previewGroupBox.Size = new System.Drawing.Size(442, 163);
            this.previewGroupBox.TabIndex = 12;
            this.previewGroupBox.TabStop = false;
            this.previewGroupBox.Text = "Preview";
            // 
            // scrollCamX
            // 
            this.scrollCamX.AutoSize = true;
            this.scrollCamX.Enabled = false;
            this.scrollCamX.Location = new System.Drawing.Point(95, 33);
            this.scrollCamX.Margin = new System.Windows.Forms.Padding(6);
            this.scrollCamX.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.scrollCamX.Name = "scrollCamX";
            this.scrollCamX.Size = new System.Drawing.Size(156, 31);
            this.scrollCamX.TabIndex = 7;
            this.toolTip.SetToolTip(this.scrollCamX, "Preview the scrolling parallax of the layer!");
            // 
            // scrollPreviewButton
            // 
            this.scrollPreviewButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.scrollPreviewButton.Location = new System.Drawing.Point(6, 86);
            this.scrollPreviewButton.Margin = new System.Windows.Forms.Padding(6);
            this.scrollPreviewButton.Name = "scrollPreviewButton";
            this.scrollPreviewButton.Size = new System.Drawing.Size(150, 44);
            this.scrollPreviewButton.TabIndex = 3;
            this.scrollPreviewButton.Text = "Animate!";
            this.scrollPreviewButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.scrollPreviewButton.UseVisualStyleBackColor = true;
            this.scrollPreviewButton.CheckedChanged += new System.EventHandler(this.scrollPreviewButton_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 38);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(93, 25);
            this.label15.TabIndex = 6;
            this.label15.Text = "Camera:";
            // 
            // moveCameraLabel
            // 
            this.moveCameraLabel.AutoSize = true;
            this.moveCameraLabel.Location = new System.Drawing.Point(166, 95);
            this.moveCameraLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.moveCameraLabel.Name = "moveCameraLabel";
            this.moveCameraLabel.Size = new System.Drawing.Size(267, 25);
            this.moveCameraLabel.TabIndex = 9;
            this.moveCameraLabel.Text = "Arrow Keys: Move Camera";
            this.moveCameraLabel.Visible = false;
            // 
            // scrollCamY
            // 
            this.scrollCamY.AutoSize = true;
            this.scrollCamY.Enabled = false;
            this.scrollCamY.Location = new System.Drawing.Point(266, 33);
            this.scrollCamY.Margin = new System.Windows.Forms.Padding(6);
            this.scrollCamY.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.scrollCamY.Name = "scrollCamY";
            this.scrollCamY.Size = new System.Drawing.Size(156, 31);
            this.scrollCamY.TabIndex = 8;
            this.toolTip.SetToolTip(this.scrollCamY, "The Y component of the preview camera\'s position.");
            // 
            // scrollEditPanel
            // 
            this.scrollEditPanel.AutoSize = true;
            this.scrollEditPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scrollEditPanel.Controls.Add(this.groupBox3);
            this.scrollEditPanel.Controls.Add(this.layerScrollSpeed);
            this.scrollEditPanel.Controls.Add(this.label10);
            this.scrollEditPanel.Controls.Add(this.layerParallaxFactor);
            this.scrollEditPanel.Controls.Add(this.label9);
            this.scrollEditPanel.Enabled = false;
            this.scrollEditPanel.Location = new System.Drawing.Point(6, 62);
            this.scrollEditPanel.Margin = new System.Windows.Forms.Padding(0);
            this.scrollEditPanel.Name = "scrollEditPanel";
            this.scrollEditPanel.Size = new System.Drawing.Size(463, 741);
            this.scrollEditPanel.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.showScrollAreas);
            this.groupBox3.Controls.Add(this.scrollOffset);
            this.groupBox3.Controls.Add(this.scrollList);
            this.groupBox3.Controls.Add(this.scrollEnableDeformation);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.addScrollButton);
            this.groupBox3.Controls.Add(this.scrollScrollSpeed);
            this.groupBox3.Controls.Add(this.deleteScrollButton);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.scrollParallaxFactor);
            this.groupBox3.Location = new System.Drawing.Point(14, 96);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(446, 642);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Parallax Lines";
            // 
            // showScrollAreas
            // 
            this.showScrollAreas.AutoSize = true;
            this.showScrollAreas.Location = new System.Drawing.Point(18, 34);
            this.showScrollAreas.Margin = new System.Windows.Forms.Padding(6);
            this.showScrollAreas.Name = "showScrollAreas";
            this.showScrollAreas.Size = new System.Drawing.Size(219, 29);
            this.showScrollAreas.TabIndex = 11;
            this.showScrollAreas.Text = "Show Scroll Areas";
            this.showScrollAreas.UseVisualStyleBackColor = true;
            this.showScrollAreas.CheckedChanged += new System.EventHandler(this.showScrollAreas_CheckedChanged);
            // 
            // scrollOffset
            // 
            this.scrollOffset.Location = new System.Drawing.Point(249, 434);
            this.scrollOffset.Margin = new System.Windows.Forms.Padding(6);
            this.scrollOffset.Name = "scrollOffset";
            this.scrollOffset.Size = new System.Drawing.Size(162, 31);
            this.scrollOffset.TabIndex = 13;
            this.toolTip.SetToolTip(this.scrollOffset, "The position (in pixels) of this scroll area.");
            this.scrollOffset.ValueChanged += new System.EventHandler(this.scrollOffset_ValueChanged);
            // 
            // scrollList
            // 
            this.scrollList.FormattingEnabled = true;
            this.scrollList.IntegralHeight = false;
            this.scrollList.ItemHeight = 25;
            this.scrollList.Location = new System.Drawing.Point(18, 75);
            this.scrollList.Margin = new System.Windows.Forms.Padding(6);
            this.scrollList.Name = "scrollList";
            this.scrollList.Size = new System.Drawing.Size(388, 287);
            this.scrollList.TabIndex = 4;
            this.scrollList.SelectedIndexChanged += new System.EventHandler(this.scrollList_SelectedIndexChanged);
            // 
            // scrollEnableDeformation
            // 
            this.scrollEnableDeformation.AutoSize = true;
            this.scrollEnableDeformation.Location = new System.Drawing.Point(18, 483);
            this.scrollEnableDeformation.Margin = new System.Windows.Forms.Padding(6);
            this.scrollEnableDeformation.Name = "scrollEnableDeformation";
            this.scrollEnableDeformation.Size = new System.Drawing.Size(233, 29);
            this.scrollEnableDeformation.TabIndex = 5;
            this.scrollEnableDeformation.Text = "Enable Deformation";
            this.toolTip.SetToolTip(this.scrollEnableDeformation, "If this scroll area can be deformed on a per-line basis. Requires scripts to mana" +
        "ge deformation.");
            this.scrollEnableDeformation.UseVisualStyleBackColor = true;
            this.scrollEnableDeformation.CheckedChanged += new System.EventHandler(this.scrollEnableDeformation_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 436);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 25);
            this.label13.TabIndex = 12;
            this.label13.Text = "Offset:";
            // 
            // addScrollButton
            // 
            this.addScrollButton.Location = new System.Drawing.Point(18, 377);
            this.addScrollButton.Margin = new System.Windows.Forms.Padding(6);
            this.addScrollButton.Name = "addScrollButton";
            this.addScrollButton.Size = new System.Drawing.Size(150, 44);
            this.addScrollButton.TabIndex = 6;
            this.addScrollButton.Text = "Add";
            this.addScrollButton.UseVisualStyleBackColor = true;
            this.addScrollButton.Click += new System.EventHandler(this.addScrollButton_Click);
            // 
            // scrollScrollSpeed
            // 
            this.scrollScrollSpeed.DecimalPlaces = 5;
            this.scrollScrollSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.scrollScrollSpeed.Location = new System.Drawing.Point(249, 578);
            this.scrollScrollSpeed.Margin = new System.Windows.Forms.Padding(6);
            this.scrollScrollSpeed.Maximum = new decimal(new int[] {
            3984375,
            0,
            0,
            393216});
            this.scrollScrollSpeed.Name = "scrollScrollSpeed";
            this.scrollScrollSpeed.Size = new System.Drawing.Size(162, 31);
            this.scrollScrollSpeed.TabIndex = 11;
            this.toolTip.SetToolTip(this.scrollScrollSpeed, "How fast (in pixels) this scroll area will move automatically.");
            this.scrollScrollSpeed.ValueChanged += new System.EventHandler(this.scrollScrollSpeed_ValueChanged);
            // 
            // deleteScrollButton
            // 
            this.deleteScrollButton.Enabled = false;
            this.deleteScrollButton.Location = new System.Drawing.Point(180, 377);
            this.deleteScrollButton.Margin = new System.Windows.Forms.Padding(6);
            this.deleteScrollButton.Name = "deleteScrollButton";
            this.deleteScrollButton.Size = new System.Drawing.Size(150, 44);
            this.deleteScrollButton.TabIndex = 7;
            this.deleteScrollButton.Text = "Delete";
            this.deleteScrollButton.UseVisualStyleBackColor = true;
            this.deleteScrollButton.Click += new System.EventHandler(this.deleteScrollButton_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 580);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 25);
            this.label11.TabIndex = 10;
            this.label11.Text = "Scroll Speed:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 530);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 25);
            this.label12.TabIndex = 8;
            this.label12.Text = "Parallax Factor:";
            // 
            // scrollParallaxFactor
            // 
            this.scrollParallaxFactor.DecimalPlaces = 5;
            this.scrollParallaxFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.scrollParallaxFactor.Location = new System.Drawing.Point(249, 528);
            this.scrollParallaxFactor.Margin = new System.Windows.Forms.Padding(6);
            this.scrollParallaxFactor.Maximum = new decimal(new int[] {
            -170194401,
            5,
            0,
            524288});
            this.scrollParallaxFactor.Name = "scrollParallaxFactor";
            this.scrollParallaxFactor.Size = new System.Drawing.Size(162, 31);
            this.scrollParallaxFactor.TabIndex = 9;
            this.toolTip.SetToolTip(this.scrollParallaxFactor, "How fast (in pixels) this scroll area moves with the camera.");
            this.scrollParallaxFactor.ValueChanged += new System.EventHandler(this.scrollParallaxFactor_ValueChanged);
            // 
            // layerScrollSpeed
            // 
            this.layerScrollSpeed.DecimalPlaces = 5;
            this.layerScrollSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.layerScrollSpeed.Location = new System.Drawing.Point(263, 56);
            this.layerScrollSpeed.Margin = new System.Windows.Forms.Padding(6);
            this.layerScrollSpeed.Maximum = new decimal(new int[] {
            3984375,
            0,
            0,
            393216});
            this.layerScrollSpeed.Name = "layerScrollSpeed";
            this.layerScrollSpeed.Size = new System.Drawing.Size(162, 31);
            this.layerScrollSpeed.TabIndex = 3;
            this.toolTip.SetToolTip(this.layerScrollSpeed, "How fast (in pixels) this layer will move automatically.");
            this.layerScrollSpeed.ValueChanged += new System.EventHandler(this.layerScrollSpeed_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 59);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(200, 25);
            this.label10.TabIndex = 2;
            this.label10.Text = "Layer Scroll Speed:";
            // 
            // layerParallaxFactor
            // 
            this.layerParallaxFactor.DecimalPlaces = 5;
            this.layerParallaxFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.layerParallaxFactor.Location = new System.Drawing.Point(263, 6);
            this.layerParallaxFactor.Margin = new System.Windows.Forms.Padding(6);
            this.layerParallaxFactor.Maximum = new decimal(new int[] {
            -170194401,
            5,
            0,
            524288});
            this.layerParallaxFactor.Name = "layerParallaxFactor";
            this.layerParallaxFactor.Size = new System.Drawing.Size(162, 31);
            this.layerParallaxFactor.TabIndex = 1;
            this.toolTip.SetToolTip(this.layerParallaxFactor, "How fast (in pixels) this layer moves with the camera.");
            this.layerParallaxFactor.ValueChanged += new System.EventHandler(this.layerParallaxFactor_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 9);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(223, 25);
            this.label9.TabIndex = 0;
            this.label9.Text = "Layer Parallax Factor:";
            // 
            // layerScrollType
            // 
            this.layerScrollType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layerScrollType.FormattingEnabled = true;
            this.layerScrollType.Items.AddRange(new object[] {
            "Horizontal",
            "Vertical",
            "3D Sky",
            "3D Floor"});
            this.layerScrollType.Location = new System.Drawing.Point(189, 14);
            this.layerScrollType.Margin = new System.Windows.Forms.Padding(6);
            this.layerScrollType.Name = "layerScrollType";
            this.layerScrollType.Size = new System.Drawing.Size(238, 33);
            this.layerScrollType.TabIndex = 1;
            this.layerScrollType.SelectedIndexChanged += new System.EventHandler(this.layerScrollType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 22);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "Layer Type:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel4);
            this.tabPage4.Location = new System.Drawing.Point(8, 39);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1300, 925);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Art";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoScroll = true;
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.Controls.Add(tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tabControl4, 2, 0);
            this.tableLayoutPanel4.Controls.Add(panel9, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Enabled = false;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1300, 925);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // tabControl4
            // 
            this.tabControl4.Controls.Add(this.tabPage12);
            this.tabControl4.Controls.Add(this.tabPage14);
            this.tabControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl4.Location = new System.Drawing.Point(792, 0);
            this.tabControl4.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl4.MinimumSize = new System.Drawing.Size(630, 577);
            this.tabControl4.Name = "tabControl4";
            this.tableLayoutPanel4.SetRowSpan(this.tabControl4, 2);
            this.tabControl4.SelectedIndex = 0;
            this.tabControl4.Size = new System.Drawing.Size(1614, 1274);
            this.tabControl4.TabIndex = 0;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.panel10);
            this.tabPage12.Controls.Add(chunkListToolStrip);
            this.tabPage12.Location = new System.Drawing.Point(8, 39);
            this.tabPage12.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage12.Size = new System.Drawing.Size(1598, 1227);
            this.tabPage12.TabIndex = 0;
            this.tabPage12.Text = "Chunks";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this.TileSelector);
            this.tabPage14.Controls.Add(tileListToolStrip);
            this.tabPage14.Location = new System.Drawing.Point(8, 39);
            this.tabPage14.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage14.Size = new System.Drawing.Size(1598, 1227);
            this.tabPage14.TabIndex = 2;
            this.tabPage14.Text = "Tiles";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // TileSelector
            // 
            this.TileSelector.BackColor = System.Drawing.SystemColors.Window;
            this.TileSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileSelector.ImageHeight = 32;
            this.TileSelector.ImageSize = 32;
            this.TileSelector.ImageWidth = 32;
            this.TileSelector.Location = new System.Drawing.Point(6, 48);
            this.TileSelector.Margin = new System.Windows.Forms.Padding(8);
            this.TileSelector.Name = "TileSelector";
            this.TileSelector.ScrollValue = 0;
            this.TileSelector.SelectedIndex = -1;
            this.TileSelector.Size = new System.Drawing.Size(1586, 1173);
            this.TileSelector.TabIndex = 2;
            this.TileSelector.SelectedIndexChanged += new System.EventHandler(this.TileSelector_SelectedIndexChanged);
            this.TileSelector.ItemDrag += new System.EventHandler(this.TileSelector_ItemDrag);
            this.TileSelector.DragDrop += new System.Windows.Forms.DragEventHandler(this.TileSelector_DragDrop);
            this.TileSelector.DragEnter += new System.Windows.Forms.DragEventHandler(this.TileSelector_DragEnter);
            this.TileSelector.DragOver += new System.Windows.Forms.DragEventHandler(this.TileSelector_DragOver);
            this.TileSelector.DragLeave += new System.EventHandler(this.TileSelector_DragLeave);
            this.TileSelector.Paint += new System.Windows.Forms.PaintEventHandler(this.TileSelector_Paint);
            this.TileSelector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TileList_KeyDown);
            this.TileSelector.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TileSelector_MouseDoubleClick);
            this.TileSelector.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TileSelector_MouseDown);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.tableLayoutPanel8);
            this.tabPage6.Location = new System.Drawing.Point(8, 39);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage6.Size = new System.Drawing.Size(1300, 925);
            this.tabPage6.TabIndex = 4;
            this.tabPage6.Text = "Palette";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.panel8, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.colorEditingPanel, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.paletteToolStrip, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1288, 913);
            this.tableLayoutPanel8.TabIndex = 4;
            // 
            // panel8
            // 
            this.panel8.AutoScroll = true;
            this.panel8.Controls.Add(this.PalettePanel);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(214, 42);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1074, 871);
            this.panel8.TabIndex = 3;
            // 
            // PalettePanel
            // 
            this.PalettePanel.Location = new System.Drawing.Point(0, 0);
            this.PalettePanel.Margin = new System.Windows.Forms.Padding(0);
            this.PalettePanel.Name = "PalettePanel";
            this.PalettePanel.Size = new System.Drawing.Size(640, 616);
            this.PalettePanel.TabIndex = 0;
            this.PalettePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.PalettePanel_Paint);
            this.PalettePanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PalettePanel_MouseDoubleClick);
            this.PalettePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PalettePanel_MouseDown);
            this.PalettePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PalettePanel_MouseMove);
            this.PalettePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PalettePanel_MouseUp);
            // 
            // colorEditingPanel
            // 
            this.colorEditingPanel.AutoSize = true;
            this.colorEditingPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.colorEditingPanel.Controls.Add(this.colorHex);
            this.colorEditingPanel.Controls.Add(label27);
            this.colorEditingPanel.Controls.Add(this.colorBlue);
            this.colorEditingPanel.Controls.Add(label4);
            this.colorEditingPanel.Controls.Add(this.colorGreen);
            this.colorEditingPanel.Controls.Add(label3);
            this.colorEditingPanel.Controls.Add(this.colorRed);
            this.colorEditingPanel.Controls.Add(label2);
            this.colorEditingPanel.Enabled = false;
            this.colorEditingPanel.Location = new System.Drawing.Point(0, 42);
            this.colorEditingPanel.Margin = new System.Windows.Forms.Padding(0);
            this.colorEditingPanel.Name = "colorEditingPanel";
            this.colorEditingPanel.Size = new System.Drawing.Size(214, 209);
            this.colorEditingPanel.TabIndex = 5;
            // 
            // colorBlue
            // 
            this.colorBlue.Location = new System.Drawing.Point(100, 106);
            this.colorBlue.Margin = new System.Windows.Forms.Padding(6);
            this.colorBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorBlue.Name = "colorBlue";
            this.colorBlue.Size = new System.Drawing.Size(106, 31);
            this.colorBlue.TabIndex = 5;
            this.colorBlue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip.SetToolTip(this.colorBlue, "The blue component of the currently selected color.");
            this.colorBlue.ValueChanged += new System.EventHandler(this.color_ValueChanged);
            // 
            // colorGreen
            // 
            this.colorGreen.Location = new System.Drawing.Point(100, 56);
            this.colorGreen.Margin = new System.Windows.Forms.Padding(6);
            this.colorGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorGreen.Name = "colorGreen";
            this.colorGreen.Size = new System.Drawing.Size(106, 31);
            this.colorGreen.TabIndex = 3;
            this.colorGreen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip.SetToolTip(this.colorGreen, "The green component of the currently selected color.");
            this.colorGreen.ValueChanged += new System.EventHandler(this.color_ValueChanged);
            // 
            // colorRed
            // 
            this.colorRed.Location = new System.Drawing.Point(100, 6);
            this.colorRed.Margin = new System.Windows.Forms.Padding(6);
            this.colorRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorRed.Name = "colorRed";
            this.colorRed.Size = new System.Drawing.Size(106, 31);
            this.colorRed.TabIndex = 1;
            this.colorRed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip.SetToolTip(this.colorRed, "The red component of the currently selected color.");
            this.colorRed.ValueChanged += new System.EventHandler(this.color_ValueChanged);
            // 
            // paletteToolStrip
            // 
            this.tableLayoutPanel8.SetColumnSpan(this.paletteToolStrip, 2);
            this.paletteToolStrip.Enabled = false;
            this.paletteToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.paletteToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.paletteToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableDraggingPaletteButton,
            this.exportPaletteToolStripButton,
            this.importPaletteToolStripButton});
            this.paletteToolStrip.Location = new System.Drawing.Point(0, 0);
            this.paletteToolStrip.Name = "paletteToolStrip";
            this.paletteToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.paletteToolStrip.Size = new System.Drawing.Size(1288, 42);
            this.paletteToolStrip.TabIndex = 6;
            this.paletteToolStrip.Text = "toolStrip1";
            // 
            // enableDraggingPaletteButton
            // 
            this.enableDraggingPaletteButton.Checked = true;
            this.enableDraggingPaletteButton.CheckOnClick = true;
            this.enableDraggingPaletteButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableDraggingPaletteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.enableDraggingPaletteButton.Name = "enableDraggingPaletteButton";
            this.enableDraggingPaletteButton.Size = new System.Drawing.Size(195, 44);
            this.enableDraggingPaletteButton.Text = "Enable &Dragging";
            this.enableDraggingPaletteButton.ToolTipText = "Toggle dragging colors around to reorder them.";
            // 
            // exportPaletteToolStripButton
            // 
            this.exportPaletteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exportPaletteToolStripButton.Name = "exportPaletteToolStripButton";
            this.exportPaletteToolStripButton.Size = new System.Drawing.Size(85, 44);
            this.exportPaletteToolStripButton.Text = "&Export";
            this.exportPaletteToolStripButton.ToolTipText = "Export the stage\'s palette into a palette file.";
            this.exportPaletteToolStripButton.Click += new System.EventHandler(this.paletteToolStripMenuItem_Click);
            // 
            // importPaletteToolStripButton
            // 
            this.importPaletteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.importPaletteToolStripButton.Name = "importPaletteToolStripButton";
            this.importPaletteToolStripButton.Size = new System.Drawing.Size(89, 44);
            this.importPaletteToolStripButton.Text = "&Import";
            this.importPaletteToolStripButton.ToolTipText = "Import a palette from a palette file or image into the stage.";
            this.importPaletteToolStripButton.Click += new System.EventHandler(this.importPaletteToolStripButton_Click);
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this.soundEffectsGroup);
            this.tabPage15.Controls.Add(this.objectListGroup);
            this.tabPage15.Controls.Add(this.layerSettingsGroup);
            this.tabPage15.Controls.Add(this.titleCardGroup);
            this.tabPage15.Location = new System.Drawing.Point(8, 39);
            this.tabPage15.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage15.Size = new System.Drawing.Size(1300, 919);
            this.tabPage15.TabIndex = 5;
            this.tabPage15.Text = "Settings";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // soundEffectsGroup
            // 
            this.soundEffectsGroup.AutoSize = true;
            this.soundEffectsGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.soundEffectsGroup.Controls.Add(this.sfxDownButton);
            this.soundEffectsGroup.Controls.Add(this.sfxUpButton);
            this.soundEffectsGroup.Controls.Add(this.sfxID);
            this.soundEffectsGroup.Controls.Add(this.sfxBrowseButton);
            this.soundEffectsGroup.Controls.Add(this.sfxFileBox);
            this.soundEffectsGroup.Controls.Add(this.label24);
            this.soundEffectsGroup.Controls.Add(this.sfxNameBox);
            this.soundEffectsGroup.Controls.Add(this.label25);
            this.soundEffectsGroup.Controls.Add(this.sfxDeleteButton);
            this.soundEffectsGroup.Controls.Add(this.sfxAddButton);
            this.soundEffectsGroup.Controls.Add(this.sfxListBox);
            this.soundEffectsGroup.Enabled = false;
            this.soundEffectsGroup.Location = new System.Drawing.Point(672, 472);
            this.soundEffectsGroup.Margin = new System.Windows.Forms.Padding(6);
            this.soundEffectsGroup.Name = "soundEffectsGroup";
            this.soundEffectsGroup.Padding = new System.Windows.Forms.Padding(6);
            this.soundEffectsGroup.Size = new System.Drawing.Size(730, 411);
            this.soundEffectsGroup.TabIndex = 6;
            this.soundEffectsGroup.TabStop = false;
            this.soundEffectsGroup.Text = "Sound Effects";
            // 
            // sfxDownButton
            // 
            this.sfxDownButton.Enabled = false;
            this.sfxDownButton.Location = new System.Drawing.Point(375, 331);
            this.sfxDownButton.Margin = new System.Windows.Forms.Padding(6);
            this.sfxDownButton.Name = "sfxDownButton";
            this.sfxDownButton.Size = new System.Drawing.Size(33, 44);
            this.sfxDownButton.TabIndex = 19;
            this.sfxDownButton.Text = "↓";
            this.sfxDownButton.UseVisualStyleBackColor = true;
            this.sfxDownButton.Click += new System.EventHandler(this.sfxDownButton_Click);
            // 
            // sfxUpButton
            // 
            this.sfxUpButton.Enabled = false;
            this.sfxUpButton.Location = new System.Drawing.Point(336, 331);
            this.sfxUpButton.Margin = new System.Windows.Forms.Padding(6);
            this.sfxUpButton.Name = "sfxUpButton";
            this.sfxUpButton.Size = new System.Drawing.Size(33, 44);
            this.sfxUpButton.TabIndex = 18;
            this.sfxUpButton.Text = "↑";
            this.toolTip.SetToolTip(this.sfxUpButton, "Shift the current selection up.");
            this.sfxUpButton.UseVisualStyleBackColor = true;
            this.sfxUpButton.Click += new System.EventHandler(this.sfxUpButton_Click);
            // 
            // sfxID
            // 
            this.sfxID.AutoSize = true;
            this.sfxID.Location = new System.Drawing.Point(422, 255);
            this.sfxID.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.sfxID.Name = "sfxID";
            this.sfxID.Size = new System.Drawing.Size(106, 25);
            this.sfxID.TabIndex = 17;
            this.sfxID.Text = "Sound ID:";
            // 
            // sfxBrowseButton
            // 
            this.sfxBrowseButton.Enabled = false;
            this.sfxBrowseButton.Location = new System.Drawing.Point(422, 192);
            this.sfxBrowseButton.Margin = new System.Windows.Forms.Padding(6);
            this.sfxBrowseButton.Name = "sfxBrowseButton";
            this.sfxBrowseButton.Size = new System.Drawing.Size(150, 44);
            this.sfxBrowseButton.TabIndex = 16;
            this.sfxBrowseButton.Text = "Browse...";
            this.toolTip.SetToolTip(this.sfxBrowseButton, "Select from a list of all sound effects available.");
            this.sfxBrowseButton.UseVisualStyleBackColor = true;
            this.sfxBrowseButton.Click += new System.EventHandler(this.sfxBrowseButton_Click);
            // 
            // sfxFileBox
            // 
            this.sfxFileBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.sfxFileBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.sfxFileBox.Enabled = false;
            this.sfxFileBox.Location = new System.Drawing.Point(422, 142);
            this.sfxFileBox.Margin = new System.Windows.Forms.Padding(6);
            this.sfxFileBox.MaxLength = 64;
            this.sfxFileBox.Name = "sfxFileBox";
            this.sfxFileBox.Size = new System.Drawing.Size(296, 31);
            this.sfxFileBox.TabIndex = 15;
            this.toolTip.SetToolTip(this.sfxFileBox, "The path to this sound\'s audio file.");
            this.sfxFileBox.TextChanged += new System.EventHandler(this.sfxFileBox_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(422, 111);
            this.label24.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 25);
            this.label24.TabIndex = 14;
            this.label24.Text = "File:";
            // 
            // sfxNameBox
            // 
            this.sfxNameBox.Enabled = false;
            this.sfxNameBox.Location = new System.Drawing.Point(422, 67);
            this.sfxNameBox.Margin = new System.Windows.Forms.Padding(6);
            this.sfxNameBox.MaxLength = 16;
            this.sfxNameBox.Name = "sfxNameBox";
            this.sfxNameBox.Size = new System.Drawing.Size(296, 31);
            this.sfxNameBox.TabIndex = 13;
            this.toolTip.SetToolTip(this.sfxNameBox, "The name of the sound, used for SfxName in scripts.");
            this.sfxNameBox.TextChanged += new System.EventHandler(this.sfxNameBox_TextChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(422, 36);
            this.label25.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(74, 25);
            this.label25.TabIndex = 12;
            this.label25.Text = "Name:";
            // 
            // sfxDeleteButton
            // 
            this.sfxDeleteButton.Enabled = false;
            this.sfxDeleteButton.Location = new System.Drawing.Point(174, 331);
            this.sfxDeleteButton.Margin = new System.Windows.Forms.Padding(6);
            this.sfxDeleteButton.Name = "sfxDeleteButton";
            this.sfxDeleteButton.Size = new System.Drawing.Size(150, 44);
            this.sfxDeleteButton.TabIndex = 11;
            this.sfxDeleteButton.Text = "Delete";
            this.toolTip.SetToolTip(this.sfxDeleteButton, "Delete the current stage sound effect.");
            this.sfxDeleteButton.UseVisualStyleBackColor = true;
            this.sfxDeleteButton.Click += new System.EventHandler(this.sfxDeleteButton_Click);
            // 
            // sfxAddButton
            // 
            this.sfxAddButton.Location = new System.Drawing.Point(12, 331);
            this.sfxAddButton.Margin = new System.Windows.Forms.Padding(6);
            this.sfxAddButton.Name = "sfxAddButton";
            this.sfxAddButton.Size = new System.Drawing.Size(150, 44);
            this.sfxAddButton.TabIndex = 10;
            this.sfxAddButton.Text = "Add";
            this.toolTip.SetToolTip(this.sfxAddButton, "Add a new stage sound effect.");
            this.sfxAddButton.UseVisualStyleBackColor = true;
            this.sfxAddButton.Click += new System.EventHandler(this.sfxAddButton_Click);
            // 
            // sfxListBox
            // 
            this.sfxListBox.FormattingEnabled = true;
            this.sfxListBox.ItemHeight = 25;
            this.sfxListBox.Location = new System.Drawing.Point(12, 36);
            this.sfxListBox.Margin = new System.Windows.Forms.Padding(6);
            this.sfxListBox.Name = "sfxListBox";
            this.sfxListBox.Size = new System.Drawing.Size(394, 279);
            this.sfxListBox.TabIndex = 9;
            this.sfxListBox.SelectedIndexChanged += new System.EventHandler(this.sfxListBox_SelectedIndexChanged);
            // 
            // objectListGroup
            // 
            this.objectListGroup.AutoSize = true;
            this.objectListGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.objectListGroup.Controls.Add(this.objectDownButton);
            this.objectListGroup.Controls.Add(this.objectUpButton);
            this.objectListGroup.Controls.Add(this.objectTypeID);
            this.objectListGroup.Controls.Add(this.browseScriptButton);
            this.objectListGroup.Controls.Add(this.objectScriptBox);
            this.objectListGroup.Controls.Add(this.label23);
            this.objectListGroup.Controls.Add(this.objectNameBox);
            this.objectListGroup.Controls.Add(this.label22);
            this.objectListGroup.Controls.Add(this.objectDeleteButton);
            this.objectListGroup.Controls.Add(this.objectAddButton);
            this.objectListGroup.Controls.Add(this.objectListBox);
            this.objectListGroup.Controls.Add(this.loadGlobalObjects);
            this.objectListGroup.Enabled = false;
            this.objectListGroup.Location = new System.Drawing.Point(672, 10);
            this.objectListGroup.Margin = new System.Windows.Forms.Padding(6);
            this.objectListGroup.Name = "objectListGroup";
            this.objectListGroup.Padding = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.objectListGroup.Size = new System.Drawing.Size(730, 449);
            this.objectListGroup.TabIndex = 5;
            this.objectListGroup.TabStop = false;
            this.objectListGroup.Text = "Object List";
            // 
            // objectDownButton
            // 
            this.objectDownButton.Enabled = false;
            this.objectDownButton.Location = new System.Drawing.Point(375, 375);
            this.objectDownButton.Margin = new System.Windows.Forms.Padding(6);
            this.objectDownButton.Name = "objectDownButton";
            this.objectDownButton.Size = new System.Drawing.Size(33, 44);
            this.objectDownButton.TabIndex = 11;
            this.objectDownButton.Text = "↓";
            this.toolTip.SetToolTip(this.objectDownButton, "Shift the current selection down.");
            this.objectDownButton.UseVisualStyleBackColor = true;
            this.objectDownButton.Click += new System.EventHandler(this.objectDownButton_Click);
            // 
            // objectUpButton
            // 
            this.objectUpButton.Enabled = false;
            this.objectUpButton.Location = new System.Drawing.Point(336, 375);
            this.objectUpButton.Margin = new System.Windows.Forms.Padding(6);
            this.objectUpButton.Name = "objectUpButton";
            this.objectUpButton.Size = new System.Drawing.Size(33, 44);
            this.objectUpButton.TabIndex = 10;
            this.objectUpButton.Text = "↑";
            this.toolTip.SetToolTip(this.objectUpButton, "Shift the current selection up.");
            this.objectUpButton.UseVisualStyleBackColor = true;
            this.objectUpButton.Click += new System.EventHandler(this.objectUpButton_Click);
            // 
            // objectTypeID
            // 
            this.objectTypeID.AutoSize = true;
            this.objectTypeID.Location = new System.Drawing.Point(422, 298);
            this.objectTypeID.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.objectTypeID.Name = "objectTypeID";
            this.objectTypeID.Size = new System.Drawing.Size(160, 25);
            this.objectTypeID.TabIndex = 9;
            this.objectTypeID.Text = "Object Type ID:";
            // 
            // browseScriptButton
            // 
            this.browseScriptButton.Enabled = false;
            this.browseScriptButton.Location = new System.Drawing.Point(422, 236);
            this.browseScriptButton.Margin = new System.Windows.Forms.Padding(6);
            this.browseScriptButton.Name = "browseScriptButton";
            this.browseScriptButton.Size = new System.Drawing.Size(150, 44);
            this.browseScriptButton.TabIndex = 8;
            this.browseScriptButton.Text = "Browse...";
            this.toolTip.SetToolTip(this.browseScriptButton, "Select from a list of all objects available.");
            this.browseScriptButton.UseVisualStyleBackColor = true;
            this.browseScriptButton.Click += new System.EventHandler(this.browseScriptButton_Click);
            // 
            // objectScriptBox
            // 
            this.objectScriptBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.objectScriptBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.objectScriptBox.Enabled = false;
            this.objectScriptBox.Location = new System.Drawing.Point(422, 186);
            this.objectScriptBox.Margin = new System.Windows.Forms.Padding(6);
            this.objectScriptBox.MaxLength = 64;
            this.objectScriptBox.Name = "objectScriptBox";
            this.objectScriptBox.Size = new System.Drawing.Size(296, 31);
            this.objectScriptBox.TabIndex = 7;
            this.toolTip.SetToolTip(this.objectScriptBox, "The path to the object\'s script file.");
            this.objectScriptBox.TextChanged += new System.EventHandler(this.objectScriptBox_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(422, 156);
            this.label23.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(73, 25);
            this.label23.TabIndex = 6;
            this.label23.Text = "Script:";
            // 
            // objectNameBox
            // 
            this.objectNameBox.Enabled = false;
            this.objectNameBox.Location = new System.Drawing.Point(422, 111);
            this.objectNameBox.Margin = new System.Windows.Forms.Padding(6);
            this.objectNameBox.MaxLength = 16;
            this.objectNameBox.Name = "objectNameBox";
            this.objectNameBox.Size = new System.Drawing.Size(296, 31);
            this.objectNameBox.TabIndex = 5;
            this.toolTip.SetToolTip(this.objectNameBox, "The name of the object, used for TypeName in scripts.");
            this.objectNameBox.TextChanged += new System.EventHandler(this.objectNameBox_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(422, 81);
            this.label22.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(74, 25);
            this.label22.TabIndex = 4;
            this.label22.Text = "Name:";
            // 
            // objectDeleteButton
            // 
            this.objectDeleteButton.Enabled = false;
            this.objectDeleteButton.Location = new System.Drawing.Point(174, 375);
            this.objectDeleteButton.Margin = new System.Windows.Forms.Padding(6);
            this.objectDeleteButton.Name = "objectDeleteButton";
            this.objectDeleteButton.Size = new System.Drawing.Size(150, 44);
            this.objectDeleteButton.TabIndex = 3;
            this.objectDeleteButton.Text = "Delete";
            this.toolTip.SetToolTip(this.objectDeleteButton, "Delete the current stage object.");
            this.objectDeleteButton.UseVisualStyleBackColor = true;
            this.objectDeleteButton.Click += new System.EventHandler(this.objectDeleteButton_Click);
            // 
            // objectAddButton
            // 
            this.objectAddButton.Location = new System.Drawing.Point(12, 375);
            this.objectAddButton.Margin = new System.Windows.Forms.Padding(6);
            this.objectAddButton.Name = "objectAddButton";
            this.objectAddButton.Size = new System.Drawing.Size(150, 44);
            this.objectAddButton.TabIndex = 2;
            this.objectAddButton.Text = "Add";
            this.toolTip.SetToolTip(this.objectAddButton, "Add a new stage object.");
            this.objectAddButton.UseVisualStyleBackColor = true;
            this.objectAddButton.Click += new System.EventHandler(this.objectAddButton_Click);
            // 
            // objectListBox
            // 
            this.objectListBox.FormattingEnabled = true;
            this.objectListBox.ItemHeight = 25;
            this.objectListBox.Location = new System.Drawing.Point(12, 81);
            this.objectListBox.Margin = new System.Windows.Forms.Padding(6);
            this.objectListBox.Name = "objectListBox";
            this.objectListBox.Size = new System.Drawing.Size(394, 279);
            this.objectListBox.TabIndex = 1;
            this.objectListBox.SelectedIndexChanged += new System.EventHandler(this.objectListBox_SelectedIndexChanged);
            // 
            // loadGlobalObjects
            // 
            this.loadGlobalObjects.AutoSize = true;
            this.loadGlobalObjects.Location = new System.Drawing.Point(12, 36);
            this.loadGlobalObjects.Margin = new System.Windows.Forms.Padding(6);
            this.loadGlobalObjects.Name = "loadGlobalObjects";
            this.loadGlobalObjects.Size = new System.Drawing.Size(239, 29);
            this.loadGlobalObjects.TabIndex = 0;
            this.loadGlobalObjects.Text = "Load Global Objects";
            this.toolTip.SetToolTip(this.loadGlobalObjects, "If global objects should be loaded in this stage folder.");
            this.loadGlobalObjects.UseVisualStyleBackColor = true;
            this.loadGlobalObjects.CheckedChanged += new System.EventHandler(this.loadGlobalObjects_CheckedChanged);
            // 
            // layerSettingsGroup
            // 
            this.layerSettingsGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layerSettingsGroup.Controls.Add(this.lowTilesLabel);
            this.layerSettingsGroup.Controls.Add(this.midpointTrackBar);
            this.layerSettingsGroup.Controls.Add(this.foregroundDeformation);
            this.layerSettingsGroup.Controls.Add(this.layer3Box);
            this.layerSettingsGroup.Controls.Add(this.label21);
            this.layerSettingsGroup.Controls.Add(this.layer2Box);
            this.layerSettingsGroup.Controls.Add(this.label20);
            this.layerSettingsGroup.Controls.Add(this.layer1Box);
            this.layerSettingsGroup.Controls.Add(this.label19);
            this.layerSettingsGroup.Controls.Add(this.layer0Box);
            this.layerSettingsGroup.Controls.Add(this.label18);
            this.layerSettingsGroup.Controls.Add(this.midpointLabel);
            this.layerSettingsGroup.Enabled = false;
            this.layerSettingsGroup.Location = new System.Drawing.Point(16, 121);
            this.layerSettingsGroup.Margin = new System.Windows.Forms.Padding(6);
            this.layerSettingsGroup.Name = "layerSettingsGroup";
            this.layerSettingsGroup.Padding = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.layerSettingsGroup.Size = new System.Drawing.Size(644, 375);
            this.layerSettingsGroup.TabIndex = 4;
            this.layerSettingsGroup.TabStop = false;
            this.layerSettingsGroup.Text = "Layer Settings";
            // 
            // lowTilesLabel
            // 
            this.lowTilesLabel.AutoSize = true;
            this.lowTilesLabel.Location = new System.Drawing.Point(470, 101);
            this.lowTilesLabel.Name = "lowTilesLabel";
            this.lowTilesLabel.Size = new System.Drawing.Size(163, 175);
            this.lowTilesLabel.TabIndex = 12;
            this.lowTilesLabel.Text = "Draw High Tiles\r\n\r\nDraw High Tiles\r\n\r\nDraw High Tiles\r\n\r\nDraw High Tiles\r\n";
            // 
            // midpointTrackBar
            // 
            this.midpointTrackBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.midpointTrackBar.LargeChange = 1;
            this.midpointTrackBar.Location = new System.Drawing.Point(22, 70);
            this.midpointTrackBar.Maximum = 4;
            this.midpointTrackBar.Name = "midpointTrackBar";
            this.midpointTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.midpointTrackBar.Size = new System.Drawing.Size(90, 240);
            this.midpointTrackBar.TabIndex = 11;
            this.midpointTrackBar.TickFrequency = 0;
            this.toolTip.SetToolTip(this.midpointTrackBar, "Which layers this stage\'s midpoint is between. Controls which layers draw low til" +
        "es and which ones draw high tiles.");
            this.midpointTrackBar.Value = 4;
            this.midpointTrackBar.ValueChanged += new System.EventHandler(this.midpointTrackBar_ValueChanged);
            // 
            // foregroundDeformation
            // 
            this.foregroundDeformation.AutoSize = true;
            this.foregroundDeformation.Location = new System.Drawing.Point(31, 319);
            this.foregroundDeformation.Name = "foregroundDeformation";
            this.foregroundDeformation.Size = new System.Drawing.Size(277, 29);
            this.foregroundDeformation.TabIndex = 10;
            this.foregroundDeformation.Text = "Foreground Deformation";
            this.toolTip.SetToolTip(this.foregroundDeformation, "Whether or not the Foreground layer can be deformed on a per-line basis. Requires" +
        " scripts to manage deformation.");
            this.foregroundDeformation.UseVisualStyleBackColor = true;
            this.foregroundDeformation.CheckedChanged += new System.EventHandler(this.foregroundDeformation_CheckedChanged);
            // 
            // layer3Box
            // 
            this.layer3Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layer3Box.FormattingEnabled = true;
            this.layer3Box.Items.AddRange(new object[] {
            "Foreground",
            "Background 1",
            "Background 2",
            "Background 3",
            "Background 4",
            "Background 5",
            "Background 6",
            "Background 7",
            "Background 8",
            "None"});
            this.layer3Box.Location = new System.Drawing.Point(222, 250);
            this.layer3Box.Margin = new System.Windows.Forms.Padding(6);
            this.layer3Box.Name = "layer3Box";
            this.layer3Box.Size = new System.Drawing.Size(238, 33);
            this.layer3Box.TabIndex = 9;
            this.layer3Box.SelectedIndexChanged += new System.EventHandler(this.layer3Box_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(110, 256);
            this.label21.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(90, 25);
            this.label21.TabIndex = 8;
            this.label21.Text = "Layer 3:";
            // 
            // layer2Box
            // 
            this.layer2Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layer2Box.FormattingEnabled = true;
            this.layer2Box.Items.AddRange(new object[] {
            "Foreground",
            "Background 1",
            "Background 2",
            "Background 3",
            "Background 4",
            "Background 5",
            "Background 6",
            "Background 7",
            "Background 8",
            "None"});
            this.layer2Box.Location = new System.Drawing.Point(222, 198);
            this.layer2Box.Margin = new System.Windows.Forms.Padding(6);
            this.layer2Box.Name = "layer2Box";
            this.layer2Box.Size = new System.Drawing.Size(238, 33);
            this.layer2Box.TabIndex = 7;
            this.layer2Box.SelectedIndexChanged += new System.EventHandler(this.layer2Box_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(110, 204);
            this.label20.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(90, 25);
            this.label20.TabIndex = 6;
            this.label20.Text = "Layer 2:";
            // 
            // layer1Box
            // 
            this.layer1Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layer1Box.FormattingEnabled = true;
            this.layer1Box.Items.AddRange(new object[] {
            "Foreground",
            "Background 1",
            "Background 2",
            "Background 3",
            "Background 4",
            "Background 5",
            "Background 6",
            "Background 7",
            "Background 8",
            "None"});
            this.layer1Box.Location = new System.Drawing.Point(222, 147);
            this.layer1Box.Margin = new System.Windows.Forms.Padding(6);
            this.layer1Box.Name = "layer1Box";
            this.layer1Box.Size = new System.Drawing.Size(238, 33);
            this.layer1Box.TabIndex = 5;
            this.layer1Box.SelectedIndexChanged += new System.EventHandler(this.layer1Box_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(110, 153);
            this.label19.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(90, 25);
            this.label19.TabIndex = 4;
            this.label19.Text = "Layer 1:";
            // 
            // layer0Box
            // 
            this.layer0Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layer0Box.FormattingEnabled = true;
            this.layer0Box.Items.AddRange(new object[] {
            "Foreground",
            "Background 1",
            "Background 2",
            "Background 3",
            "Background 4",
            "Background 5",
            "Background 6",
            "Background 7",
            "Background 8",
            "None"});
            this.layer0Box.Location = new System.Drawing.Point(222, 95);
            this.layer0Box.Margin = new System.Windows.Forms.Padding(6);
            this.layer0Box.Name = "layer0Box";
            this.layer0Box.Size = new System.Drawing.Size(238, 33);
            this.layer0Box.TabIndex = 3;
            this.layer0Box.SelectedIndexChanged += new System.EventHandler(this.layer0Box_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(110, 100);
            this.label18.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(90, 25);
            this.label18.TabIndex = 2;
            this.label18.Text = "Layer 0:";
            // 
            // midpointLabel
            // 
            this.midpointLabel.AutoSize = true;
            this.midpointLabel.Location = new System.Drawing.Point(12, 46);
            this.midpointLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.midpointLabel.Name = "midpointLabel";
            this.midpointLabel.Size = new System.Drawing.Size(247, 25);
            this.midpointLabel.TabIndex = 0;
            this.midpointLabel.Text = "Midpoint: Before Layer 0";
            // 
            // titleCardGroup
            // 
            this.titleCardGroup.AutoSize = true;
            this.titleCardGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.titleCardGroup.Controls.Add(this.label14);
            this.titleCardGroup.Controls.Add(this.levelNameBox);
            this.titleCardGroup.Enabled = false;
            this.titleCardGroup.Location = new System.Drawing.Point(16, 11);
            this.titleCardGroup.Margin = new System.Windows.Forms.Padding(6);
            this.titleCardGroup.Name = "titleCardGroup";
            this.titleCardGroup.Padding = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.titleCardGroup.Size = new System.Drawing.Size(394, 100);
            this.titleCardGroup.TabIndex = 3;
            this.titleCardGroup.TabStop = false;
            this.titleCardGroup.Text = "Title Card";
            // 
            // levelNameBox
            // 
            this.levelNameBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.levelNameBox.Location = new System.Drawing.Point(86, 39);
            this.levelNameBox.Margin = new System.Windows.Forms.Padding(6);
            this.levelNameBox.MaxLength = 255;
            this.levelNameBox.Name = "levelNameBox";
            this.levelNameBox.Size = new System.Drawing.Size(296, 31);
            this.levelNameBox.TabIndex = 1;
            this.toolTip.SetToolTip(this.levelNameBox, "The text to be shown on the level\'s Title Card. Dependant on scripts to draw.");
            this.levelNameBox.TextChanged += new System.EventHandler(this.levelNameBox_TextChanged);
            // 
            // tileContextMenuStrip
            // 
            this.tileContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tileContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutTilesToolStripMenuItem,
            this.copyTilesToolStripMenuItem,
            this.deepCopyToolStripMenuItem,
            this.pasteOverToolStripMenuItem,
            this.importOverToolStripMenuItem,
            this.drawOverToolStripMenuItem,
            this.exportTileToolStripMenuItem,
            this.duplicateTilesToolStripMenuItem,
            this.deleteTilesToolStripMenuItem});
            this.tileContextMenuStrip.Name = "contextMenuStrip1";
            this.tileContextMenuStrip.Size = new System.Drawing.Size(220, 346);
            // 
            // cutTilesToolStripMenuItem
            // 
            this.cutTilesToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.cut;
            this.cutTilesToolStripMenuItem.Name = "cutTilesToolStripMenuItem";
            this.cutTilesToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.cutTilesToolStripMenuItem.Text = "Cu&t";
            this.cutTilesToolStripMenuItem.Click += new System.EventHandler(this.cutTilesToolStripMenuItem_Click);
            // 
            // copyTilesToolStripMenuItem
            // 
            this.copyTilesToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.copy;
            this.copyTilesToolStripMenuItem.Name = "copyTilesToolStripMenuItem";
            this.copyTilesToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.copyTilesToolStripMenuItem.Text = "&Copy";
            this.copyTilesToolStripMenuItem.Click += new System.EventHandler(this.copyTilesToolStripMenuItem_Click);
            // 
            // deepCopyToolStripMenuItem
            // 
            this.deepCopyToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.copy;
            this.deepCopyToolStripMenuItem.Name = "deepCopyToolStripMenuItem";
            this.deepCopyToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.deepCopyToolStripMenuItem.Text = "Deep Co&py";
            this.deepCopyToolStripMenuItem.Click += new System.EventHandler(this.deepCopyToolStripMenuItem_Click);
            // 
            // pasteOverToolStripMenuItem
            // 
            this.pasteOverToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.paste;
            this.pasteOverToolStripMenuItem.Name = "pasteOverToolStripMenuItem";
            this.pasteOverToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.pasteOverToolStripMenuItem.Text = "Paste &Over";
            this.pasteOverToolStripMenuItem.Click += new System.EventHandler(this.pasteOverToolStripMenuItem_Click);
            // 
            // importOverToolStripMenuItem
            // 
            this.importOverToolStripMenuItem.Name = "importOverToolStripMenuItem";
            this.importOverToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.importOverToolStripMenuItem.Text = "&Reimport...";
            this.importOverToolStripMenuItem.Click += new System.EventHandler(this.importOverToolStripMenuItem_Click);
            // 
            // drawOverToolStripMenuItem
            // 
            this.drawOverToolStripMenuItem.Name = "drawOverToolStripMenuItem";
            this.drawOverToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.drawOverToolStripMenuItem.Text = "Draw Over...";
            this.drawOverToolStripMenuItem.Click += new System.EventHandler(this.drawOverToolStripMenuItem_Click);
            // 
            // exportTileToolStripMenuItem
            // 
            this.exportTileToolStripMenuItem.Name = "exportTileToolStripMenuItem";
            this.exportTileToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.exportTileToolStripMenuItem.Text = "E&xport...";
            this.exportTileToolStripMenuItem.Click += new System.EventHandler(this.ExportTileToolStripMenuItem_Click);
            // 
            // duplicateTilesToolStripMenuItem
            // 
            this.duplicateTilesToolStripMenuItem.Name = "duplicateTilesToolStripMenuItem";
            this.duplicateTilesToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.duplicateTilesToolStripMenuItem.Text = "D&uplicate";
            this.duplicateTilesToolStripMenuItem.Click += new System.EventHandler(this.duplicateTilesToolStripMenuItem_Click);
            // 
            // deleteTilesToolStripMenuItem
            // 
            this.deleteTilesToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.delete;
            this.deleteTilesToolStripMenuItem.Name = "deleteTilesToolStripMenuItem";
            this.deleteTilesToolStripMenuItem.Size = new System.Drawing.Size(219, 38);
            this.deleteTilesToolStripMenuItem.Text = "C&lear";
            this.deleteTilesToolStripMenuItem.Click += new System.EventHandler(this.deleteTilesToolStripMenuItem_Click);
            // 
            // layoutContextMenuStrip
            // 
            this.layoutContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.layoutContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem1,
            this.copyToolStripMenuItem1,
            this.pasteOnceToolStripMenuItem,
            this.pasteRepeatingToolStripMenuItem,
            this.toolStripSeparator11,
            this.deleteToolStripMenuItem1,
            this.fillToolStripMenuItem,
            this.toolStripSeparator12,
            this.importToolStripMenuItem2,
            this.exportLayoutSectionToolStripMenuItem,
            this.saveSectionToolStripMenuItem,
            this.toolStripSeparator7,
            this.insertLayoutToolStripMenuItem,
            this.deleteLayoutToolStripMenuItem});
            this.layoutContextMenuStrip.Name = "layoutContextMenuStrip";
            this.layoutContextMenuStrip.Size = new System.Drawing.Size(354, 440);
            // 
            // cutToolStripMenuItem1
            // 
            this.cutToolStripMenuItem1.Image = global::SonicRetro.SonLVL.Properties.Resources.cut;
            this.cutToolStripMenuItem1.Name = "cutToolStripMenuItem1";
            this.cutToolStripMenuItem1.Size = new System.Drawing.Size(353, 38);
            this.cutToolStripMenuItem1.Text = "Cu&t";
            this.cutToolStripMenuItem1.Click += new System.EventHandler(this.cutToolStripMenuItem1_Click);
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Image = global::SonicRetro.SonLVL.Properties.Resources.copy;
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(353, 38);
            this.copyToolStripMenuItem1.Text = "&Copy";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
            // 
            // pasteOnceToolStripMenuItem
            // 
            this.pasteOnceToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.paste;
            this.pasteOnceToolStripMenuItem.Name = "pasteOnceToolStripMenuItem";
            this.pasteOnceToolStripMenuItem.Size = new System.Drawing.Size(353, 38);
            this.pasteOnceToolStripMenuItem.Text = "&Paste Once";
            this.pasteOnceToolStripMenuItem.Click += new System.EventHandler(this.pasteOnceToolStripMenuItem_Click);
            // 
            // pasteRepeatingToolStripMenuItem
            // 
            this.pasteRepeatingToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.paste;
            this.pasteRepeatingToolStripMenuItem.Name = "pasteRepeatingToolStripMenuItem";
            this.pasteRepeatingToolStripMenuItem.Size = new System.Drawing.Size(353, 38);
            this.pasteRepeatingToolStripMenuItem.Text = "Paste &Repeating";
            this.pasteRepeatingToolStripMenuItem.Click += new System.EventHandler(this.pasteRepeatingToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(350, 6);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Image = global::SonicRetro.SonLVL.Properties.Resources.delete;
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(353, 38);
            this.deleteToolStripMenuItem1.Text = "C&lear";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // fillToolStripMenuItem
            // 
            this.fillToolStripMenuItem.Name = "fillToolStripMenuItem";
            this.fillToolStripMenuItem.Size = new System.Drawing.Size(353, 38);
            this.fillToolStripMenuItem.Text = "&Fill With Selected Chunk";
            this.fillToolStripMenuItem.Click += new System.EventHandler(this.fillToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(350, 6);
            // 
            // importToolStripMenuItem2
            // 
            this.importToolStripMenuItem2.Name = "importToolStripMenuItem2";
            this.importToolStripMenuItem2.Size = new System.Drawing.Size(353, 38);
            this.importToolStripMenuItem2.Text = "I&mport...";
            this.importToolStripMenuItem2.Click += new System.EventHandler(this.importToolStripMenuItem2_Click);
            // 
            // exportLayoutSectionToolStripMenuItem
            // 
            this.exportLayoutSectionToolStripMenuItem.Name = "exportLayoutSectionToolStripMenuItem";
            this.exportLayoutSectionToolStripMenuItem.Size = new System.Drawing.Size(353, 38);
            this.exportLayoutSectionToolStripMenuItem.Text = "E&xport...";
            this.exportLayoutSectionToolStripMenuItem.Click += new System.EventHandler(this.ExportLayoutSectionToolStripMenuItem_Click);
            // 
            // saveSectionToolStripMenuItem
            // 
            this.saveSectionToolStripMenuItem.Name = "saveSectionToolStripMenuItem";
            this.saveSectionToolStripMenuItem.Size = new System.Drawing.Size(353, 38);
            this.saveSectionToolStripMenuItem.Text = "&Save as Layout Section...";
            this.saveSectionToolStripMenuItem.Click += new System.EventHandler(this.saveSectionToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(350, 6);
            // 
            // insertLayoutToolStripMenuItem
            // 
            this.insertLayoutToolStripMenuItem.Name = "insertLayoutToolStripMenuItem";
            this.insertLayoutToolStripMenuItem.Size = new System.Drawing.Size(353, 38);
            this.insertLayoutToolStripMenuItem.Text = "&Insert...";
            this.insertLayoutToolStripMenuItem.Click += new System.EventHandler(this.insertLayoutToolStripMenuItem_Click);
            // 
            // deleteLayoutToolStripMenuItem
            // 
            this.deleteLayoutToolStripMenuItem.Name = "deleteLayoutToolStripMenuItem";
            this.deleteLayoutToolStripMenuItem.Size = new System.Drawing.Size(353, 38);
            this.deleteLayoutToolStripMenuItem.Text = "&Delete...";
            this.deleteLayoutToolStripMenuItem.Click += new System.EventHandler(this.deleteLayoutToolStripMenuItem_Click);
            // 
            // chunkBlockContextMenuStrip
            // 
            this.chunkBlockContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.chunkBlockContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flipChunkBlocksHorizontallyToolStripMenuItem,
            this.flipChunkBlocksVerticallyToolStripMenuItem,
            this.copyChunkBlocksToolStripMenuItem,
            this.pasteChunkBlocksToolStripMenuItem,
            this.clearChunkBlocksToolStripMenuItem});
            this.chunkBlockContextMenuStrip.Name = "solidsContextMenuStrip";
            this.chunkBlockContextMenuStrip.Size = new System.Drawing.Size(266, 194);
            // 
            // flipChunkBlocksHorizontallyToolStripMenuItem
            // 
            this.flipChunkBlocksHorizontallyToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.fliph;
            this.flipChunkBlocksHorizontallyToolStripMenuItem.Name = "flipChunkBlocksHorizontallyToolStripMenuItem";
            this.flipChunkBlocksHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(265, 38);
            this.flipChunkBlocksHorizontallyToolStripMenuItem.Text = "Flip &Horizontally";
            this.flipChunkBlocksHorizontallyToolStripMenuItem.Click += new System.EventHandler(this.flipChunkBlocksHorizontallyToolStripMenuItem_Click);
            // 
            // flipChunkBlocksVerticallyToolStripMenuItem
            // 
            this.flipChunkBlocksVerticallyToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.flipv;
            this.flipChunkBlocksVerticallyToolStripMenuItem.Name = "flipChunkBlocksVerticallyToolStripMenuItem";
            this.flipChunkBlocksVerticallyToolStripMenuItem.Size = new System.Drawing.Size(265, 38);
            this.flipChunkBlocksVerticallyToolStripMenuItem.Text = "Flip &Vertically";
            this.flipChunkBlocksVerticallyToolStripMenuItem.Click += new System.EventHandler(this.flipChunkBlocksVerticallyToolStripMenuItem_Click);
            // 
            // copyChunkBlocksToolStripMenuItem
            // 
            this.copyChunkBlocksToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.copy;
            this.copyChunkBlocksToolStripMenuItem.Name = "copyChunkBlocksToolStripMenuItem";
            this.copyChunkBlocksToolStripMenuItem.Size = new System.Drawing.Size(265, 38);
            this.copyChunkBlocksToolStripMenuItem.Text = "&Copy";
            this.copyChunkBlocksToolStripMenuItem.Click += new System.EventHandler(this.copyChunkBlocksToolStripMenuItem_Click);
            // 
            // pasteChunkBlocksToolStripMenuItem
            // 
            this.pasteChunkBlocksToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.paste;
            this.pasteChunkBlocksToolStripMenuItem.Name = "pasteChunkBlocksToolStripMenuItem";
            this.pasteChunkBlocksToolStripMenuItem.Size = new System.Drawing.Size(265, 38);
            this.pasteChunkBlocksToolStripMenuItem.Text = "&Paste";
            this.pasteChunkBlocksToolStripMenuItem.Click += new System.EventHandler(this.pasteChunkBlocksToolStripMenuItem_Click);
            // 
            // clearChunkBlocksToolStripMenuItem
            // 
            this.clearChunkBlocksToolStripMenuItem.Image = global::SonicRetro.SonLVL.Properties.Resources.delete;
            this.clearChunkBlocksToolStripMenuItem.Name = "clearChunkBlocksToolStripMenuItem";
            this.clearChunkBlocksToolStripMenuItem.Size = new System.Drawing.Size(265, 38);
            this.clearChunkBlocksToolStripMenuItem.Text = "&Clear";
            this.clearChunkBlocksToolStripMenuItem.Click += new System.EventHandler(this.clearChunkBlocksToolStripMenuItem_Click);
            // 
            // objectPanel
            // 
            this.objectPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectPanel.HScrollEnabled = false;
            this.objectPanel.HScrollLargeChange = 128;
            this.objectPanel.HScrollMaximum = 128;
            this.objectPanel.HScrollMinimum = 0;
            this.objectPanel.HScrollSmallChange = 16;
            this.objectPanel.HScrollValue = 0;
            this.objectPanel.Location = new System.Drawing.Point(0, 42);
            this.objectPanel.Margin = new System.Windows.Forms.Padding(8);
            this.objectPanel.Name = "objectPanel";
            this.objectPanel.PanelAllowDrop = false;
            this.objectPanel.PanelCursor = System.Windows.Forms.Cursors.Default;
            this.objectPanel.Size = new System.Drawing.Size(817, 877);
            this.objectPanel.TabIndex = 5;
            this.objectPanel.VScrollEnabled = false;
            this.objectPanel.VScrollLargeChange = 128;
            this.objectPanel.VScrollMaximum = 128;
            this.objectPanel.VScrollMinimum = 0;
            this.objectPanel.VScrollSmallChange = 16;
            this.objectPanel.VScrollValue = 0;
            this.objectPanel.PanelPaint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.objectPanel.PanelKeyDown += new System.Windows.Forms.KeyEventHandler(this.objectPanel_KeyDown);
            this.objectPanel.PanelMouseDown += new System.Windows.Forms.MouseEventHandler(this.objectPanel_MouseDown);
            this.objectPanel.PanelMouseUp += new System.Windows.Forms.MouseEventHandler(this.objectPanel_MouseUp);
            this.objectPanel.PanelMouseMove += new System.Windows.Forms.MouseEventHandler(this.objectPanel_MouseMove);
            this.objectPanel.PanelDragEnter += new System.Windows.Forms.DragEventHandler(this.objectPanel_DragEnter);
            this.objectPanel.PanelDragOver += new System.Windows.Forms.DragEventHandler(this.objectPanel_DragOver);
            this.objectPanel.PanelDragLeave += new System.EventHandler(this.objectPanel_DragLeave);
            this.objectPanel.PanelDragDrop += new System.Windows.Forms.DragEventHandler(this.objectPanel_DragDrop);
            this.objectPanel.ScrollBarValueChanged += new System.EventHandler(this.ScrollBar_ValueChanged);
            this.objectPanel.Resize += new System.EventHandler(this.panel_Resize);
            // 
            // showGridToolStripCheckBoxButton
            // 
            this.showGridToolStripCheckBoxButton.Checked = false;
            this.showGridToolStripCheckBoxButton.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.showGridToolStripCheckBoxButton.Name = "showGridToolStripCheckBoxButton";
            this.showGridToolStripCheckBoxButton.Size = new System.Drawing.Size(155, 42);
            this.showGridToolStripCheckBoxButton.Text = "Show &Grid";
            this.showGridToolStripCheckBoxButton.CheckedChanged += new System.EventHandler(this.showGridToolStripCheckBoxButton_CheckedChanged);
            // 
            // snapObjectsToolStripCheckBoxButton
            // 
            this.snapObjectsToolStripCheckBoxButton.Checked = false;
            this.snapObjectsToolStripCheckBoxButton.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.snapObjectsToolStripCheckBoxButton.Name = "snapObjectsToolStripCheckBoxButton";
            this.snapObjectsToolStripCheckBoxButton.Size = new System.Drawing.Size(186, 42);
            this.snapObjectsToolStripCheckBoxButton.Text = "Snap &Objects";
            this.snapObjectsToolStripCheckBoxButton.CheckedChanged += new System.EventHandler(this.snapObjectsToolStripCheckBoxButton_CheckedChanged);
            // 
            // foregroundPanel
            // 
            this.foregroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foregroundPanel.HScrollEnabled = false;
            this.foregroundPanel.HScrollLargeChange = 128;
            this.foregroundPanel.HScrollMaximum = 128;
            this.foregroundPanel.HScrollMinimum = 0;
            this.foregroundPanel.HScrollSmallChange = 16;
            this.foregroundPanel.HScrollValue = 0;
            this.foregroundPanel.Location = new System.Drawing.Point(0, 42);
            this.foregroundPanel.Margin = new System.Windows.Forms.Padding(8);
            this.foregroundPanel.Name = "foregroundPanel";
            this.foregroundPanel.PanelAllowDrop = false;
            this.foregroundPanel.PanelCursor = System.Windows.Forms.Cursors.Default;
            this.foregroundPanel.Size = new System.Drawing.Size(871, 883);
            this.foregroundPanel.TabIndex = 5;
            this.foregroundPanel.VScrollEnabled = false;
            this.foregroundPanel.VScrollLargeChange = 128;
            this.foregroundPanel.VScrollMaximum = 128;
            this.foregroundPanel.VScrollMinimum = 0;
            this.foregroundPanel.VScrollSmallChange = 16;
            this.foregroundPanel.VScrollValue = 0;
            this.foregroundPanel.PanelPaint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.foregroundPanel.PanelKeyDown += new System.Windows.Forms.KeyEventHandler(this.foregroundPanel_KeyDown);
            this.foregroundPanel.PanelMouseDown += new System.Windows.Forms.MouseEventHandler(this.foregroundPanel_MouseDown);
            this.foregroundPanel.PanelMouseUp += new System.Windows.Forms.MouseEventHandler(this.foregroundPanel_MouseUp);
            this.foregroundPanel.PanelMouseMove += new System.Windows.Forms.MouseEventHandler(this.foregroundPanel_MouseMove);
            this.foregroundPanel.ScrollBarValueChanged += new System.EventHandler(this.ScrollBar_ValueChanged);
            this.foregroundPanel.Resize += new System.EventHandler(this.panel_Resize);
            // 
            // displayObjectsToolStripCheckBoxButton
            // 
            this.displayObjectsToolStripCheckBoxButton.Checked = false;
            this.displayObjectsToolStripCheckBoxButton.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.displayObjectsToolStripCheckBoxButton.Name = "displayObjectsToolStripCheckBoxButton";
            this.displayObjectsToolStripCheckBoxButton.Size = new System.Drawing.Size(210, 50);
            this.displayObjectsToolStripCheckBoxButton.Text = "Display Objects";
            this.displayObjectsToolStripCheckBoxButton.CheckedChanged += new System.EventHandler(this.displayObjectsToolStripCheckBoxButton_CheckedChanged);
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundPanel.HScrollEnabled = false;
            this.backgroundPanel.HScrollLargeChange = 128;
            this.backgroundPanel.HScrollMaximum = 128;
            this.backgroundPanel.HScrollMinimum = 0;
            this.backgroundPanel.HScrollSmallChange = 16;
            this.backgroundPanel.HScrollValue = 0;
            this.backgroundPanel.Location = new System.Drawing.Point(0, 42);
            this.backgroundPanel.Margin = new System.Windows.Forms.Padding(8);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.PanelAllowDrop = false;
            this.backgroundPanel.PanelCursor = System.Windows.Forms.Cursors.Default;
            this.backgroundPanel.Size = new System.Drawing.Size(767, 883);
            this.backgroundPanel.TabIndex = 6;
            this.backgroundPanel.VScrollEnabled = false;
            this.backgroundPanel.VScrollLargeChange = 128;
            this.backgroundPanel.VScrollMaximum = 128;
            this.backgroundPanel.VScrollMinimum = 0;
            this.backgroundPanel.VScrollSmallChange = 16;
            this.backgroundPanel.VScrollValue = 0;
            this.backgroundPanel.PanelPaint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.backgroundPanel.PanelKeyDown += new System.Windows.Forms.KeyEventHandler(this.backgroundPanel_KeyDown);
            this.backgroundPanel.PanelMouseDown += new System.Windows.Forms.MouseEventHandler(this.backgroundPanel_MouseDown);
            this.backgroundPanel.PanelMouseUp += new System.Windows.Forms.MouseEventHandler(this.backgroundPanel_MouseUp);
            this.backgroundPanel.PanelMouseMove += new System.Windows.Forms.MouseEventHandler(this.backgroundPanel_MouseMove);
            this.backgroundPanel.ScrollBarValueChanged += new System.EventHandler(this.ScrollBar_ValueChanged);
            this.backgroundPanel.Resize += new System.EventHandler(this.panel_Resize);
            // 
            // chunkBlockEditor
            // 
            this.chunkBlockEditor.AutoSize = true;
            this.chunkBlockEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chunkBlockEditor.Hexadecimal = true;
            this.chunkBlockEditor.Location = new System.Drawing.Point(6, 182);
            this.chunkBlockEditor.Margin = new System.Windows.Forms.Padding(8);
            this.chunkBlockEditor.Name = "chunkBlockEditor";
            this.chunkBlockEditor.SelectedObjects = null;
            this.chunkBlockEditor.Size = new System.Drawing.Size(506, 248);
            this.chunkBlockEditor.TabIndex = 3;
            this.chunkBlockEditor.PropertyValueChanged += new System.EventHandler(this.chunkBlockEditor_PropertyValueChanged);
            // 
            // colorHex
            // 
            this.colorHex.Hexadecimal = true;
            this.colorHex.Location = new System.Drawing.Point(74, 172);
            this.colorHex.Margin = new System.Windows.Forms.Padding(6);
            this.colorHex.Maximum = new decimal(new int[] {
            16777215,
            0,
            0,
            0});
            this.colorHex.Name = "colorHex";
            this.colorHex.Size = new System.Drawing.Size(134, 31);
            this.colorHex.TabIndex = 7;
            this.colorHex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip.SetToolTip(this.colorHex, "The hexadecimal code of the currently selected color.");
            this.colorHex.ValueChanged += new System.EventHandler(this.colorHex_ValueChanged);
            // 
            // importProgressControl1
            // 
            this.importProgressControl1.AutoSize = true;
            this.importProgressControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.importProgressControl1.CurrentProgress = 0;
            this.importProgressControl1.Location = new System.Drawing.Point(0, 0);
            this.importProgressControl1.Margin = new System.Windows.Forms.Padding(8);
            this.importProgressControl1.MaximumProgress = 100;
            this.importProgressControl1.Name = "importProgressControl1";
            this.importProgressControl1.Size = new System.Drawing.Size(274, 81);
            this.importProgressControl1.TabIndex = 5;
            this.importProgressControl1.UseWaitCursor = true;
            this.importProgressControl1.Visible = false;
            this.importProgressControl1.SizeChanged += new System.EventHandler(this.importProgressControl1_SizeChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 42);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 25);
            this.label14.TabIndex = 2;
            this.label14.Text = "Text:";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 1014);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.importProgressControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainForm";
            this.Text = "SonLVL-RSDK";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            chunkListToolStrip.ResumeLayout(false);
            chunkListToolStrip.PerformLayout();
            tileListToolStrip.ResumeLayout(false);
            tileListToolStrip.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TileID)).EndInit();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChunkID)).EndInit();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colFlags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceilingAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorAngle)).EndInit();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            layoutSectionListToolStrip.ResumeLayout(false);
            layoutSectionListToolStrip.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.objectContextMenuStrip.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.objToolStrip.ResumeLayout(false);
            this.objToolStrip.PerformLayout();
            this.tabControl5.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.fgToolStrip.ResumeLayout(false);
            this.fgToolStrip.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.layoutSectionSplitContainer.Panel1.ResumeLayout(false);
            this.layoutSectionSplitContainer.Panel1.PerformLayout();
            this.layoutSectionSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutSectionSplitContainer)).EndInit();
            this.layoutSectionSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutSectionPreview)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.bgToolStrip.ResumeLayout(false);
            this.bgToolStrip.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            this.tabPage13.PerformLayout();
            this.previewGroupBox.ResumeLayout(false);
            this.previewGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrollCamX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrollCamY)).EndInit();
            this.scrollEditPanel.ResumeLayout(false);
            this.scrollEditPanel.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrollOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrollScrollSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrollParallaxFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerScrollSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerParallaxFactor)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabControl4.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage12.PerformLayout();
            this.tabPage14.ResumeLayout(false);
            this.tabPage14.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.colorEditingPanel.ResumeLayout(false);
            this.colorEditingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRed)).EndInit();
            this.paletteToolStrip.ResumeLayout(false);
            this.paletteToolStrip.PerformLayout();
            this.tabPage15.ResumeLayout(false);
            this.tabPage15.PerformLayout();
            this.soundEffectsGroup.ResumeLayout(false);
            this.soundEffectsGroup.PerformLayout();
            this.objectListGroup.ResumeLayout(false);
            this.objectListGroup.PerformLayout();
            this.layerSettingsGroup.ResumeLayout(false);
            this.layerSettingsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.midpointTrackBar)).EndInit();
            this.titleCardGroup.ResumeLayout(false);
            this.titleCardGroup.PerformLayout();
            this.tileContextMenuStrip.ResumeLayout(false);
            this.layoutContextMenuStrip.ResumeLayout(false);
            this.chunkBlockContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.colorHex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeLevelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.MenuStrip mainMenuStrip;
		private System.ComponentModel.BackgroundWorker backgroundLevelLoader;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem chunksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem foregroundToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem backgroundToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem transparentBackgroundToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem objectsAboveHighPlaneToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip objectContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addObjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem paletteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		internal System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem collisionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem path1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem path2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addGroupOfObjectsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem buildAndRunToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hUDToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem layersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem lowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem highToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.PropertyGrid ObjectProperties;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private SonicRetro.SonLVL.API.TileList ChunkSelector;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.ToolStripMenuItem recentProjectsToolStripMenuItem;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Label ChunkCount;
		private System.Windows.Forms.NumericUpDown ChunkID;
		private ChunkBlockEditor chunkBlockEditor;
		private SonicRetro.SonLVL.API.TileList TileSelector;
		private System.Windows.Forms.Button rotateTileRightButton;
		private System.Windows.Forms.Label TileCount;
		private System.Windows.Forms.NumericUpDown TileID;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Panel PalettePanel;
		private System.Windows.Forms.NumericUpDown floorAngle;
		private System.Windows.Forms.ContextMenuStrip tileContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem cutTilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyTilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteTilesToolStripMenuItem;
		private System.Windows.Forms.Panel panel10;
		private System.Windows.Forms.ToolStripMenuItem gridColorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewReadmeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reportBugToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem selectAllObjectsToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip layoutContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem pasteOnceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
		private System.Windows.Forms.ToolStrip bgToolStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem anglesToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private System.Windows.Forms.ListView objectTypeList;
		private System.Windows.Forms.ImageList objectTypeImages;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem insertLayoutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteLayoutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteRepeatingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fillToolStripMenuItem;
		private System.Windows.Forms.ToolStrip objToolStrip;
		private System.Windows.Forms.ToolStripButton alignGroundToolStripButton;
		private System.Windows.Forms.ToolStripButton alignLeftWallToolStripButton;
		private System.Windows.Forms.ToolStripButton alignRightWallToolStripButton;
		private System.Windows.Forms.ToolStripButton alignCeilingToolStripButton;
		private System.Windows.Forms.ToolStripButton alignLeftsToolStripButton;
		private System.Windows.Forms.ToolStripButton alignCentersToolStripButton;
		private System.Windows.Forms.ToolStripButton alignRightsToolStripButton;
		private System.Windows.Forms.ToolStripButton alignTopsToolStripButton;
		private System.Windows.Forms.ToolStripButton alignMiddlesToolStripButton;
		private System.Windows.Forms.ToolStripButton alignBottomsToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem findNextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem findPreviousToolStripMenuItem;
		private System.Windows.Forms.Button flipChunkHButton;
		private System.Windows.Forms.Button flipChunkVButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
		private System.Windows.Forms.Panel colorEditingPanel;
		private System.Windows.Forms.NumericUpDown colorBlue;
		private System.Windows.Forms.NumericUpDown colorGreen;
		private System.Windows.Forms.NumericUpDown colorRed;
		private System.Windows.Forms.ToolStripDropDownButton objGridSizeDropDownButton;
		private ToolStripCheckBoxButton showGridToolStripCheckBoxButton;
		private ToolStripCheckBoxButton snapObjectsToolStripCheckBoxButton;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem7;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem6;
		private System.Windows.Forms.ToolStripButton remapChunksButton;
		private System.Windows.Forms.ToolStripButton remapTilesButton;
		private System.Windows.Forms.TabPage tabPage8;
		private System.Windows.Forms.SplitContainer layoutSectionSplitContainer;
		private System.Windows.Forms.PictureBox layoutSectionPreview;
		private System.Windows.Forms.ListBox layoutSectionListBox;
		private System.Windows.Forms.TabControl tabControl3;
		private System.Windows.Forms.TabPage tabPage10;
		private System.Windows.Forms.TabPage tabPage11;
		private System.Windows.Forms.TabPage tabPage9;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripMenuItem saveSectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton enableDraggingChunksButton;
		private System.Windows.Forms.ToolStripButton enableDraggingTilesButton;
		private System.Windows.Forms.ToolStripMenuItem deepCopyToolStripMenuItem;
		private System.Windows.Forms.Button flipTileVButton;
		private System.Windows.Forms.Button flipTileHButton;
		private System.Windows.Forms.CheckBox showBlockBehindCollisionCheckBox;
		private System.Windows.Forms.ToolStripMenuItem pasteOverToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton importToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem2;
		private System.Windows.Forms.ToolStripButton importChunksToolStripButton;
		private System.Windows.Forms.ToolStripButton importTilesToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem switchMouseButtonsInChunkAndBlockEditorsToolStripMenuItem;
		private System.Windows.Forms.Label chunkCtrlLabel;
		private System.Windows.Forms.ToolStripMenuItem exportArtcollisionpriorityToolStripMenuItem;
		private ImportProgressControl importProgressControl1;
		private System.Windows.Forms.ToolStripButton deleteUnusedTilesToolStripButton;
		private System.Windows.Forms.ToolStripButton deleteUnusedChunksToolStripButton;
		private System.Windows.Forms.ToolStripButton clearBackgroundToolStripButton;
		private System.Windows.Forms.Button calculateAngleButton;
		private System.Windows.Forms.ToolStripMenuItem duplicateTilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem usageCountsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
		private System.Windows.Forms.ToolStripButton replaceBackgroundToolStripButton;
		private System.Windows.Forms.ToolStripButton replaceChunkBlocksToolStripButton;
		private System.Windows.Forms.ToolStripButton removeDuplicateChunksToolStripButton;
		private System.Windows.Forms.ToolStripButton removeDuplicateTilesToolStripButton;
		private System.Windows.Forms.ToolStripButton drawChunkToolStripButton;
		private System.Windows.Forms.ToolStripButton drawTileToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem importOverToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl4;
		private System.Windows.Forms.TabPage tabPage12;
		private System.Windows.Forms.TabPage tabPage14;
		private API.KeyboardPanel ChunkPicture;
		private System.Windows.Forms.Panel TilePicture;
		private System.Windows.Forms.Panel ColPicture;
		private ScrollingPanel objectPanel;
		private ScrollingPanel foregroundPanel;
		private System.Windows.Forms.ToolStrip fgToolStrip;
		private System.Windows.Forms.ToolStripButton replaceForegroundToolStripButton;
		private System.Windows.Forms.ToolStripButton clearForegroundToolStripButton;
		private ScrollingPanel backgroundPanel;
		private System.Windows.Forms.ToolStrip paletteToolStrip;
		private System.Windows.Forms.ContextMenuStrip chunkBlockContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem copyChunkBlocksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteChunkBlocksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearChunkBlocksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem flipChunkBlocksHorizontallyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem flipChunkBlocksVerticallyToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton enableDraggingPaletteButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
		private System.Windows.Forms.ToolStripMenuItem exportTileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportLayoutSectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem presentationStagesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem regularStagesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem specialStagesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bonusStagesToolStripMenuItem;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.ToolStripDropDownButton bgLayerDropDown;
		private System.Windows.Forms.ComboBox collisionLayerSelector;
		private System.Windows.Forms.CheckBox collisionCeiling;
		private System.Windows.Forms.NumericUpDown ceilingAngle;
		private System.Windows.Forms.NumericUpDown rightAngle;
		private System.Windows.Forms.NumericUpDown leftAngle;
		private System.Windows.Forms.TabControl tabControl5;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.ListView objectOrder;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.TabPage tabPage13;
		private System.Windows.Forms.ComboBox layerScrollType;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel scrollEditPanel;
		private System.Windows.Forms.NumericUpDown layerParallaxFactor;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.NumericUpDown layerScrollSpeed;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ListBox scrollList;
		private System.Windows.Forms.Button addScrollButton;
		private System.Windows.Forms.CheckBox scrollEnableDeformation;
		private System.Windows.Forms.Button deleteScrollButton;
		private System.Windows.Forms.NumericUpDown scrollScrollSpeed;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.NumericUpDown scrollParallaxFactor;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown scrollOffset;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.CheckBox scrollPreviewButton;
		private System.Windows.Forms.TabPage tabPage15;
		private System.Windows.Forms.Label moveCameraLabel;
		private System.Windows.Forms.NumericUpDown scrollCamY;
		private System.Windows.Forms.NumericUpDown scrollCamX;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.CheckBox showScrollAreas;
		private System.Windows.Forms.TextBox levelNameBox;
		private System.Windows.Forms.GroupBox titleCardGroup;
		private System.Windows.Forms.GroupBox layerSettingsGroup;
		private System.Windows.Forms.ComboBox layer3Box;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.ComboBox layer2Box;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.ComboBox layer1Box;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.ComboBox layer0Box;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label midpointLabel;
		private System.Windows.Forms.GroupBox objectListGroup;
		private System.Windows.Forms.CheckBox loadGlobalObjects;
		private System.Windows.Forms.Button browseScriptButton;
		private System.Windows.Forms.TextBox objectScriptBox;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.TextBox objectNameBox;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Button objectDeleteButton;
		private System.Windows.Forms.Button objectAddButton;
		private System.Windows.Forms.ListBox objectListBox;
		private System.Windows.Forms.GroupBox soundEffectsGroup;
		private System.Windows.Forms.Button sfxBrowseButton;
		private System.Windows.Forms.TextBox sfxFileBox;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox sfxNameBox;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Button sfxDeleteButton;
		private System.Windows.Forms.Button sfxAddButton;
		private System.Windows.Forms.ListBox sfxListBox;
		private System.Windows.Forms.ToolStripMenuItem clearLevelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectModToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editGameConfigToolStripMenuItem;
		private System.Windows.Forms.NumericUpDown colFlags;
		private System.Windows.Forms.ToolStripMenuItem recentModsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton copyCollisionAllButton;
		private System.Windows.Forms.Button copyCollisionSingleButton;
		private System.Windows.Forms.ToolStripButton importPaletteToolStripButton;
		private SonicRetro.SonLVL.NumericUpDownPadded colorHex;
		private System.Windows.Forms.Label objectTypeID;
		private System.Windows.Forms.Label sfxID;
		private System.Windows.Forms.Button sfxUpButton;
		private System.Windows.Forms.Button sfxDownButton;
		private System.Windows.Forms.Button objectDownButton;
		private System.Windows.Forms.Button objectUpButton;
		private ToolStripCheckBoxButton displayObjectsToolStripCheckBoxButton;
		private System.Windows.Forms.ToolStripButton resizeForegroundToolStripButton;
		private System.Windows.Forms.ToolStripButton resizeBackgroundToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem hideDebugObjectsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gotoToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.ToolStripButton deleteToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem bgColorToolStripMenuItem;
		private System.Windows.Forms.CheckBox foregroundDeformation;
		private System.Windows.Forms.ToolStripButton reloadTilesToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem drawOverToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton exportPaletteToolStripButton;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chunkShowHighTilesCheckBox;
		private System.Windows.Forms.CheckBox chunkHighlightHighTilesCheckBox;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.CheckBox chunkShowGridCheckBox;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.RadioButton chunkColBRadioButton;
		private System.Windows.Forms.RadioButton chunkColARadioButton;
		private System.Windows.Forms.RadioButton chunkColNoneRadioButton;
		private System.Windows.Forms.CheckBox chunkShowLowTilesCheckBox;
		private System.Windows.Forms.ToolStripDropDownButton bgDuplicateLayerOverToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem layer1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem layer2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem layer3ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem layer4ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem layer5ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem layer6ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem layer7ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem layer8ToolStripMenuItem;
		private System.Windows.Forms.TrackBar midpointTrackBar;
		private System.Windows.Forms.Label lowTilesLabel;
		private System.Windows.Forms.GroupBox previewGroupBox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ToolStripMenuItem useHexadecimalToolStripMenuItem;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Label label14;
	}
}
