using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SonicRetro.SonLVL.API;
using System.Linq;

namespace SonicRetro.SonLVL.LevelConverter
{
	public partial class MainForm : Form
	{
		public static MainForm Instance { get; private set; }

		public MainForm()
		{
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			Instance = this;
			LevelData.LogEvent += new LevelData.LogEventHandler(Log);
			InitializeComponent();
		}

		void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			Log(e.Exception.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
			File.WriteAllLines("LevelConverter.log", LogFile.ToArray());
			using (ErrorDialog ed = new ErrorDialog("Unhandled Exception " + e.Exception.GetType().Name + "\nLog file has been saved.\n\nDo you want to try to continue running?", true))
			{
				if (ed.ShowDialog(this) == DialogResult.Cancel)
					Close();
			}
		}

		internal void Log(params string[] lines)
		{
			LogFile.AddRange(lines);
		}

		internal List<string> LogFile = new List<string>();

		private void MainForm_Load(object sender, EventArgs e)
		{
			srcVersion.SelectedIndex = 0;
			objectMode.SelectedIndex = 0;
		}

		private void CheckEnableConvert()
		{
			button1.Enabled = false;
			if (!File.Exists(fileSelector1.FileName))
				return;
			if (dstVersion.SelectedIndex == -1 || dstVersion.SelectedIndex == srcVersion.SelectedIndex)
				return;
			switch ((ObjectMode)objectMode.SelectedIndex)
			{
				case ObjectMode.MatchGlobalsAddStage:
				case ObjectMode.MatchGlobalsDelete:
					if (!File.Exists(srcGameConfig.FileName) || !File.Exists(dstGameConfig.FileName))
						return;
					break;
				case ObjectMode.DeleteGlobal:
				case ObjectMode.AddGlobalStage:
					if (!File.Exists(srcGameConfig.FileName))
						return;
					break;
				case ObjectMode.AsIs:
					if (dstVersion.SelectedIndex == 2 && !File.Exists(dstGameConfig.FileName))
						return;
					break;
			}
			button1.Enabled = true;
		}

		private void srcVersion_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (srcVersion.SelectedIndex)
			{
				case 2: // v5
					fileSelector1.Filter = "Scene Files|Scene*.bin";
					break;
				default:
					fileSelector1.Filter = "Scene Files|Act*.bin";
					break;
			}
		}

		private void fileSelector1_FileNameChanged(object sender, EventArgs e)
		{
			CheckEnableConvert();
		}

		private void dstVersion_SelectedIndexChanged(object sender, EventArgs e)
		{
			CheckEnableConvert();
		}

		private void objectMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			CheckEnableConvert();
			switch ((ObjectMode)objectMode.SelectedIndex)
			{
				case ObjectMode.MatchGlobalsAddStage:
				case ObjectMode.MatchGlobalsDelete:
					srcGCLabel.Visible = true;
					srcGameConfig.Visible = true;
					dstGCLabel.Visible = true;
					dstGameConfig.Visible = true;
					break;
				case ObjectMode.DeleteGlobal:
				case ObjectMode.AddGlobalStage:
					srcGCLabel.Visible = true;
					srcGameConfig.Visible = true;
					dstGCLabel.Visible = false;
					dstGameConfig.Visible = false;
					break;
				case ObjectMode.AsIs:
					if (dstVersion.SelectedIndex != 2)
						goto default;
					srcGCLabel.Visible = false;
					srcGameConfig.Visible = false;
					dstGCLabel.Visible = true;
					dstGameConfig.Visible = true;
					break;
				default:
					srcGCLabel.Visible = false;
					srcGameConfig.Visible = false;
					dstGCLabel.Visible = false;
					dstGameConfig.Visible = false;
					break;
			}
		}

		private void srcGameConfig_FileNameChanged(object sender, EventArgs e)
		{
			CheckEnableConvert();
		}

		private void dstGameConfig_FileNameChanged(object sender, EventArgs e)
		{
			CheckEnableConvert();
		}

		static readonly Action<string, string, ObjectMode, string, string>[][] conversionFuncs =
		{
			new Action<string, string, ObjectMode, string, string>[] { null, ConvertV3ToV4, ConvertV3ToV5 },
			new Action<string, string, ObjectMode, string, string>[] { ConvertV4ToV3, null, ConvertV4ToV5 },
			new Action<string, string, ObjectMode, string, string>[] { ConvertV5ToV3, ConvertV5ToV4, null },
		};

		private void button1_Click(object sender, EventArgs e)
		{
			switch (dstVersion.SelectedIndex)
			{
				case 2: // v5
					saveFileDialog1.Filter = "Scene Files|Scene*.bin";
					saveFileDialog1.FileName = Path.GetFileName(fileSelector1.FileName).Replace("Act", "Scene");
					break;
				default:
					saveFileDialog1.Filter = "Scene Files|Act*.bin";
					saveFileDialog1.FileName = Path.GetFileName(fileSelector1.FileName).Replace("Scene", "Act");
					break;
			}
			if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
				conversionFuncs[srcVersion.SelectedIndex][dstVersion.SelectedIndex](fileSelector1.FileName, saveFileDialog1.FileName, (ObjectMode)objectMode.SelectedIndex, srcGameConfig.FileName, dstGameConfig.FileName);
		}

		private static void ConvertV3ToV4(string srcFile, string dstFile, ObjectMode objMode, string srcGCFile, string dstGCFile)
		{
			string srcFol = Path.GetDirectoryName(srcFile);
			string dstFol = Path.GetDirectoryName(dstFile);
			File.Copy(Path.Combine(srcFol, "16x16Tiles.gif"), Path.Combine(dstFol, "16x16Tiles.gif"), true);
			File.Copy(Path.Combine(srcFol, "128x128Tiles.bin"), Path.Combine(dstFol, "128x128Tiles.bin"), true);
			File.Copy(Path.Combine(srcFol, "CollisionMasks.bin"), Path.Combine(dstFol, "CollisionMasks.bin"), true);
			RSDKv3.Backgrounds srcBG = new RSDKv3.Backgrounds(Path.Combine(srcFol, "Backgrounds.bin"));
			RSDKv4.Backgrounds dstBG = new RSDKv4.Backgrounds();
			dstBG.hScroll = new List<RSDKv3_4.Backgrounds.ScrollInfo>(srcBG.hScroll.Select(a => new RSDKv4.Backgrounds.ScrollInfo() { deform = a.deform, parallaxFactor = a.parallaxFactor, scrollSpeed = a.scrollSpeed }));
			dstBG.vScroll = new List<RSDKv3_4.Backgrounds.ScrollInfo>(srcBG.vScroll.Select(a => new RSDKv4.Backgrounds.ScrollInfo() { deform = a.deform, parallaxFactor = a.parallaxFactor, scrollSpeed = a.scrollSpeed }));
			for (int i = 0; i < 8; i++)
			{
				dstBG.layers[i].type = srcBG.layers[i].type;
				dstBG.layers[i].parallaxFactor = srcBG.layers[i].parallaxFactor;
				dstBG.layers[i].scrollSpeed = srcBG.layers[i].scrollSpeed;
				dstBG.layers[i].lineScroll = srcBG.layers[i].lineScroll;
				dstBG.layers[i].layout = srcBG.layers[i].layout;
				dstBG.layers[i].width = srcBG.layers[i].width;
				dstBG.layers[i].height = srcBG.layers[i].height;
			}
			dstBG.Write(Path.Combine(dstFol, "Backgrounds.bin"));
			RSDKv3.StageConfig srcConf = new RSDKv3.StageConfig(Path.Combine(srcFol, "StageConfig.bin"));
			RSDKv4.StageConfig dstConf = new RSDKv4.StageConfig
			{
				stagePalette = srcConf.stagePalette,
				soundFX = srcConf.soundFX
			};
			RSDKv3.Scene srcScene = new RSDKv3.Scene(srcFile);
			RSDKv4.Scene dstScene = new RSDKv4.Scene
			{
				title = srcScene.title,
				layout = srcScene.layout,
				width = srcScene.width,
				height = srcScene.height,
				activeLayer0 = srcScene.activeLayer0,
				activeLayer1 = srcScene.activeLayer1,
				activeLayer2 = srcScene.activeLayer2,
				activeLayer3 = srcScene.activeLayer3,
				layerMidpoint = srcScene.layerMidpoint
			};
			if (objMode == ObjectMode.ConvertAllToBlank)
			{
				dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Select(a => new RSDKv4.Scene.Entity(0, 0, a.xpos, a.ypos)));
			}
			else if (srcConf.loadGlobalObjects)
			{
				List<RSDKv3_4.GameConfig.ObjectInfo> srcObjs = null;
				if (File.Exists(srcGCFile))
					srcObjs = new RSDKv3.GameConfig(srcGCFile).objects;
				List<RSDKv3_4.GameConfig.ObjectInfo> dstObjs = null;
				if (File.Exists(dstGCFile))
					dstObjs = new RSDKv4.GameConfig(dstGCFile).objects;
				switch (objMode)
				{
					case ObjectMode.MatchGlobalsAddStage:
						Dictionary<int, int> objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.FindIndex(a => a.name == srcObjs[i].name);
							if (ind == -1)
							{
								dstConf.objects.Add(srcObjs[i]);
								objmap.Add(i + 1, dstObjs.Count + dstConf.objects.Count);
							}
							else
							{
								dstConf.loadGlobalObjects = true;
								objmap.Add(i + 1, ind + 1);
							}
						}
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + dstConf.objects.Count + 1);
						dstConf.objects.AddRange(srcConf.objects);
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Select(a => new RSDKv4.Scene.Entity((byte)objmap[a.type], a.propertyValue, a.xpos, a.ypos)));
						break;
					case ObjectMode.MatchGlobalsDelete:
						objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.FindIndex(a => a.name == srcObjs[i].name);
							if (ind == -1)
								objmap.Add(i + 1, -1);
							else
								objmap.Add(i + 1, ind + 1);
						}
						dstConf.loadGlobalObjects = objmap.Values.Any(a => a != -1);
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + 1);
						dstConf.objects = srcConf.objects;
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Where(a => objmap[a.type] != -1).Select(a => new RSDKv4.Scene.Entity((byte)objmap[a.type], a.propertyValue, a.xpos, a.ypos)));
						break;
					case ObjectMode.DeleteGlobal:
						dstConf.objects = srcConf.objects;
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Where(a => a.type > srcObjs.Count).Select(a => new RSDKv4.Scene.Entity((byte)(a.type - srcObjs.Count), a.propertyValue, a.xpos, a.ypos)));
						break;
					case ObjectMode.AddGlobalStage:
						dstConf.objects = new List<RSDKv3_4.GameConfig.ObjectInfo>(srcObjs.Concat(srcConf.objects));
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Select(a => new RSDKv4.Scene.Entity(a.type, a.propertyValue, a.xpos, a.ypos)));
						break;
					case ObjectMode.AsIs:
						dstConf.loadGlobalObjects = true;
						dstConf.objects = srcConf.objects;
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Select(a => new RSDKv4.Scene.Entity(a.type, a.propertyValue, a.xpos, a.ypos)));
						break;
				}
			}
			else if (objMode != ObjectMode.DontInclude)
			{
				dstConf.objects = srcConf.objects;
				dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Select(a => new RSDKv4.Scene.Entity(a.type, a.propertyValue, a.xpos, a.ypos)));
			}
			dstConf.Write(Path.Combine(dstFol, "StageConfig.bin"));
			dstScene.Write(dstFile);
		}

		private static void ConvertV3ToV5(string srcFile, string dstFile, ObjectMode objMode, string srcGCFile, string dstGCFile)
		{
			string srcFol = Path.GetDirectoryName(srcFile);
			string dstFol = Path.GetDirectoryName(dstFile);
			File.Copy(Path.Combine(srcFol, "16x16Tiles.gif"), Path.Combine(dstFol, "16x16Tiles.gif"), true);
			RSDKv3_4.TileConfig srcTiles = new RSDKv3_4.TileConfig(Path.Combine(srcFol, "CollisionMasks.bin"));
			RSDKv5.TileConfig dstTiles = new RSDKv5.TileConfig();
			for (int i = 0; i < 2; i++)
				dstTiles.collisionMasks[i] = srcTiles.collisionMasks[i].Select(a => new RSDKv5.TileConfig.CollisionMask()
				{
					flipY = a.flipY,
					flags = a.flags,
					floorAngle = a.floorAngle,
					lWallAngle = a.lWallAngle,
					rWallAngle = a.rWallAngle,
					roofAngle = a.roofAngle,
					heightMasks = a.heightMasks.Select(b => new RSDKv5.TileConfig.CollisionMask.HeightMask()
					{
						height = b.height,
						solid = b.solid
					}).ToArray()
				}).ToArray();
			dstTiles.Write(Path.Combine(dstFol, "TileConfig.bin"));
			RSDKv3.StageConfig srcConf = new RSDKv3.StageConfig(Path.Combine(srcFol, "StageConfig.bin"));
			RSDKv5.StageConfig dstConf = new RSDKv5.StageConfig
			{
				soundFX = srcConf.soundFX.Select(a => new RSDKv5.GameConfig.SoundInfo() { name = a.path }).ToList()
			};
			RSDKv3_4.Gif gif = new RSDKv3_4.Gif(Path.Combine(srcFol, "16x16Tiles.gif"));
			bool[] blanktiles = new bool[0x400];
			for (int i = 0; i < 0x400; i++)
				blanktiles[i] = gif.pixels.FastArrayEqual(0, i * 256, 256) && srcTiles.collisionMasks[0][i].heightMasks.All(a => !a.solid) && srcTiles.collisionMasks[1][i].heightMasks.All(a => !a.solid);
			for (int i = 0; i < 16; i++)
				gif.palette.Skip(i * 16).Take(16).Select(b => new RSDKv5.Color(b.r, b.g, b.b)).ToArray().CopyTo(dstConf.palettes[0].colors[i], 0);
			srcConf.stagePalette.colors.Select(a => a.Select(b => new RSDKv5.Color(b.r, b.g, b.b)).ToArray()).ToArray().CopyTo(dstConf.palettes[0].colors, 6);
			RSDKv3_4.Tiles128x128 srcChunks = new RSDKv3_4.Tiles128x128(Path.Combine(srcFol, "128x128Tiles.bin"));
			RSDKv3.Scene srcScene = new RSDKv3.Scene(srcFile);
			RSDKv3.Backgrounds srcBG = new RSDKv3.Backgrounds(Path.Combine(srcFol, "Backgrounds.bin"));
			RSDKv5.Scene dstScene = new RSDKv5.Scene();
			RSDKv5.SceneLayer fgLow = new RSDKv5.SceneLayer("FG Low", (ushort)(srcScene.width * 8), (ushort)(srcScene.height * 8))
			{
				drawOrder = 1
			};
			RSDKv5.SceneLayer fgHigh = new RSDKv5.SceneLayer("FG High", (ushort)(srcScene.width * 8), (ushort)(srcScene.height * 8))
			{
				drawOrder = 6
			};
			bool hightiles = false;
			for (int y = 0; y < srcScene.height; y++)
				for (int x = 0; x < srcScene.width; x++)
					if (srcScene.layout[y][x] < srcChunks.chunkList.Length)
					{
						RSDKv3_4.Tiles128x128.Block block = srcChunks.chunkList[srcScene.layout[y][x]];
						for (int by = 0; by < 8; by++)
							for (int bx = 0; bx < 8; bx++)
							{
								RSDKv3_4.Tiles128x128.Block.Tile srcTile = block.tiles[by][bx];
								if (!blanktiles[srcTile.tileIndex])
								{
									RSDKv5.SceneLayer.Tile dstTile = new RSDKv5.SceneLayer.Tile
									{
										tileIndex = srcTile.tileIndex,
										direction = (RSDKv5.SceneLayer.Tile.Directions)srcTile.direction
									};
									switch (srcTile.solidityA)
									{
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll:
											dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidAll;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop:
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip:
											dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidTop;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop:
											dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone:
											dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidNone;
											break;
									}
									switch (srcTile.solidityB)
									{
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll:
											dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidAll;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop:
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip:
											dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidTop;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop:
											dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone:
											dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidNone;
											break;
									}
									switch (srcTile.visualPlane)
									{
										case RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.Low:
											fgLow.layout[y * 8 + by][x * 8 + bx] = dstTile;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High:
											fgHigh.layout[y * 8 + by][x * 8 + bx] = dstTile;
											hightiles = true;
											break;
									}
								}
							}
					}
			dstScene.layers.Add(fgLow);
			if (hightiles)
				dstScene.layers.Add(fgHigh);
			List<RSDKv3_4.Backgrounds.Layer> bglayers = new List<RSDKv3_4.Backgrounds.Layer>(8);
			int midpoint = int.MaxValue;
			if (srcScene.layerMidpoint == RSDKv3_4.Scene.LayerMidpoints.BeforeLayer0)
				midpoint = 0;
			switch (srcScene.activeLayer0)
			{
				case RSDKv3_4.Scene.ActiveLayers.Foreground:
				case RSDKv3_4.Scene.ActiveLayers.None:
					break;
				default:
					bglayers.Add(srcBG.layers[(int)srcScene.activeLayer0 - 1]);
					break;
			}
			if (srcScene.layerMidpoint == RSDKv3_4.Scene.LayerMidpoints.AfterLayer0)
				midpoint = bglayers.Count;
			switch (srcScene.activeLayer1)
			{
				case RSDKv3_4.Scene.ActiveLayers.Foreground:
				case RSDKv3_4.Scene.ActiveLayers.None:
					break;
				default:
					bglayers.Add(srcBG.layers[(int)srcScene.activeLayer1 - 1]);
					break;
			}
			if (srcScene.layerMidpoint == RSDKv3_4.Scene.LayerMidpoints.AfterLayer1)
				midpoint = bglayers.Count;
			switch (srcScene.activeLayer2)
			{
				case RSDKv3_4.Scene.ActiveLayers.Foreground:
				case RSDKv3_4.Scene.ActiveLayers.None:
					break;
				default:
					bglayers.Add(srcBG.layers[(int)srcScene.activeLayer2 - 1]);
					break;
			}
			if (srcScene.layerMidpoint == RSDKv3_4.Scene.LayerMidpoints.AfterLayer2)
				midpoint = bglayers.Count;
			switch (srcScene.activeLayer3)
			{
				case RSDKv3_4.Scene.ActiveLayers.Foreground:
				case RSDKv3_4.Scene.ActiveLayers.None:
					break;
				default:
					bglayers.Add(srcBG.layers[(int)srcScene.activeLayer3 - 1]);
					break;
			}
			int activelayers = bglayers.Count;
			bglayers.AddRange(srcBG.layers.Where(a => a.width > 0 && a.height > 0 && !bglayers.Contains(a)));
			for (int i = 0; i < bglayers.Count; i++)
			{
				RSDKv3_4.Backgrounds.Layer srcLayer = bglayers[i];
				RSDKv5.SceneLayer dstLayer = new RSDKv5.SceneLayer($"Background {i + 1}", (ushort)(srcLayer.width * 8), (ushort)(srcLayer.height * 8));
				hightiles = false;
				if (i < activelayers && i >= midpoint)
				{
					dstLayer.drawOrder = 7;
					hightiles = true;
				}
				else if (i >= activelayers)
					dstLayer.drawOrder = 16;
				byte[] scrollinds = srcLayer.lineScroll.Distinct().ToArray();
				switch (srcLayer.type)
				{
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
						dstLayer.type = RSDKv5.SceneLayer.Types.HScroll;
						dstLayer.scrollInfo = scrollinds.Select(a => new RSDKv5.ScrollInfo()
						{
							deform = srcBG.hScroll[a].deform,
							parallaxFactor = (short)srcBG.hScroll[a].parallaxFactor,
							scrollSpeed = (short)(srcBG.hScroll[a].scrollSpeed << 2)
						}).ToList();
						dstLayer.lineScroll = srcLayer.lineScroll.Select(a => (byte)Array.IndexOf(scrollinds, a)).ToArray();
						break;
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
						dstLayer.type = RSDKv5.SceneLayer.Types.VScroll;
						dstLayer.scrollInfo = scrollinds.Select(a => new RSDKv5.ScrollInfo()
						{
							deform = srcBG.vScroll[a].deform,
							parallaxFactor = (short)srcBG.vScroll[a].parallaxFactor,
							scrollSpeed = (short)(srcBG.vScroll[a].scrollSpeed << 2)
						}).ToList();
						dstLayer.lineScroll = srcLayer.lineScroll.Select(a => (byte)Array.IndexOf(scrollinds, a)).ToArray();
						break;
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.Sky3D:
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.Floor3D:
						dstLayer.type = RSDKv5.SceneLayer.Types.RotoZoom;
						break;
				}
				dstLayer.parallaxFactor = (short)srcLayer.parallaxFactor;
				dstLayer.scrollSpeed = (short)(srcLayer.scrollSpeed << 2);
				bool blank = true;
				for (int y = 0; y < srcLayer.height; y++)
					for (int x = 0; x < srcLayer.width; x++)
						if (srcLayer.layout[y][x] < srcChunks.chunkList.Length)
						{
							RSDKv3_4.Tiles128x128.Block block = srcChunks.chunkList[srcLayer.layout[y][x]];
							for (int by = 0; by < 8; by++)
								for (int bx = 0; bx < 8; bx++)
								{
									RSDKv3_4.Tiles128x128.Block.Tile srcTile = block.tiles[by][bx];
									if (!blanktiles[srcTile.tileIndex])
									{
										if (i < activelayers)
											switch (srcTile.visualPlane)
											{
												case RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.Low:
													if (hightiles)
														continue;
													break;
												case RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High:
													if (!hightiles)
														continue;
													break;
											}
										blank = false;
										RSDKv5.SceneLayer.Tile dstTile = new RSDKv5.SceneLayer.Tile
										{
											tileIndex = srcTile.tileIndex,
											direction = (RSDKv5.SceneLayer.Tile.Directions)srcTile.direction
										};
										switch (srcTile.solidityA)
										{
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll:
												dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidAll;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop:
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip:
												dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidTop;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop:
												dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone:
												dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidNone;
												break;
										}
										switch (srcTile.solidityB)
										{
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll:
												dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidAll;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop:
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip:
												dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidTop;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop:
												dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone:
												dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidNone;
												break;
										}
										dstLayer.layout[y * 8 + by][x * 8 + bx] = dstTile;
									}
								}
						}
				if (!blank)
					dstScene.layers.Add(dstLayer);
				if (dstScene.layers.Count == 8)
					break;
			}
			if (objMode == ObjectMode.ConvertAllToBlank)
			{
				dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
				dstScene.objects[0].entities = new List<RSDKv5.SceneEntity>(srcScene.entities.Select(a => new RSDKv5.SceneEntity(dstScene.objects[0], (ushort)srcScene.entities.IndexOf(a)) { xpos = a.xpos, ypos = a.ypos }));
			}
			else if (srcConf.loadGlobalObjects)
			{
				List<RSDKv3_4.GameConfig.ObjectInfo> srcObjs = null;
				if (File.Exists(srcGCFile))
					srcObjs = new RSDKv3.GameConfig(srcGCFile).objects;
				List<string> dstObjs = null;
				if (File.Exists(dstGCFile))
					dstObjs = new RSDKv5.GameConfig(dstGCFile).objects;
				switch (objMode)
				{
					case ObjectMode.MatchGlobalsAddStage:
						Dictionary<int, int> objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.IndexOf(srcObjs[i].name);
							if (ind == -1)
							{
								dstConf.objects.Add(srcObjs[i].name);
								objmap.Add(i + 1, dstObjs.Count + dstConf.objects.Count);
							}
							else
							{
								dstConf.loadGlobalObjects = true;
								objmap.Add(i + 1, ind + 1);
							}
						}
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + dstConf.objects.Count + 1);
						dstConf.objects.AddRange(srcConf.objects.Select(a => a.name));
						dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
						foreach (var obj in dstObjs)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var obj in dstConf.objects)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var ent in srcScene.entities)
						{
							RSDKv5.SceneObject so = dstScene.objects[objmap[ent.type]];
							so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
						}
						break;
					case ObjectMode.MatchGlobalsDelete:
						objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.IndexOf(srcObjs[i].name);
							if (ind == -1)
								objmap.Add(i + 1, -1);
							else
								objmap.Add(i + 1, ind + 1);
						}
						dstConf.loadGlobalObjects = objmap.Values.Any(a => a != -1);
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + 1);
						dstConf.objects = srcConf.objects.Select(a => a.name).ToList();
						dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
						foreach (var obj in dstObjs)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var obj in dstConf.objects)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var ent in srcScene.entities.Where(a => objmap[a.type] != -1))
						{
							RSDKv5.SceneObject so = dstScene.objects[objmap[ent.type]];
							so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
						}
						break;
					case ObjectMode.DeleteGlobal:
						dstConf.objects = srcConf.objects.Select(a => a.name).ToList();
						dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
						foreach (var obj in dstConf.objects)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var ent in srcScene.entities.Where(a => a.type > srcObjs.Count))
						{
							RSDKv5.SceneObject so = dstScene.objects[ent.type - srcObjs.Count];
							so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
						}
						break;
					case ObjectMode.AddGlobalStage:
						dstConf.objects = srcObjs.Select(a => a.name).Concat(srcConf.objects.Select(a => a.name)).ToList();
						dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
						foreach (var obj in dstConf.objects)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var ent in srcScene.entities)
						{
							RSDKv5.SceneObject so = dstScene.objects[ent.type];
							so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
						}
						break;
					case ObjectMode.AsIs:
						dstConf.loadGlobalObjects = true;
						dstConf.objects = srcConf.objects.Select(a => a.name).ToList();
						dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
						foreach (var obj in dstObjs)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var obj in dstConf.objects)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var ent in srcScene.entities)
						{
							RSDKv5.SceneObject so = dstScene.objects[ent.type];
							so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
						}
						break;
				}
			}
			else if (objMode != ObjectMode.DontInclude)
			{
				dstConf.objects = srcConf.objects.Select(a => a.name).ToList();
				dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
				foreach (var obj in dstConf.objects)
					dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
				foreach (var ent in srcScene.entities)
				{
					RSDKv5.SceneObject so = dstScene.objects[ent.type];
					so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
				}
			}
			dstConf.Write(Path.Combine(dstFol, "StageConfig.bin"));
			dstScene.Write(dstFile);
		}

		private static void ConvertV4ToV3(string srcFile, string dstFile, ObjectMode objMode, string srcGCFile, string dstGCFile)
		{
			string srcFol = Path.GetDirectoryName(srcFile);
			string dstFol = Path.GetDirectoryName(dstFile);
			File.Copy(Path.Combine(srcFol, "16x16Tiles.gif"), Path.Combine(dstFol, "16x16Tiles.gif"), true);
			File.Copy(Path.Combine(srcFol, "128x128Tiles.bin"), Path.Combine(dstFol, "128x128Tiles.bin"), true);
			File.Copy(Path.Combine(srcFol, "CollisionMasks.bin"), Path.Combine(dstFol, "CollisionMasks.bin"), true);
			RSDKv4.Backgrounds srcBG = new RSDKv4.Backgrounds(Path.Combine(srcFol, "Backgrounds.bin"));
			RSDKv3.Backgrounds dstBG = new RSDKv3.Backgrounds();
			dstBG.hScroll = new List<RSDKv3_4.Backgrounds.ScrollInfo>(srcBG.hScroll.Select(a => new RSDKv3.Backgrounds.ScrollInfo() { deform = a.deform, parallaxFactor = a.parallaxFactor, scrollSpeed = a.scrollSpeed }));
			dstBG.vScroll = new List<RSDKv3_4.Backgrounds.ScrollInfo>(srcBG.vScroll.Select(a => new RSDKv3.Backgrounds.ScrollInfo() { deform = a.deform, parallaxFactor = a.parallaxFactor, scrollSpeed = a.scrollSpeed }));
			for (int i = 0; i < 8; i++)
			{
				dstBG.layers[i].type = srcBG.layers[i].type;
				dstBG.layers[i].parallaxFactor = srcBG.layers[i].parallaxFactor;
				dstBG.layers[i].scrollSpeed = srcBG.layers[i].scrollSpeed;
				dstBG.layers[i].lineScroll = srcBG.layers[i].lineScroll;
				dstBG.layers[i].layout = srcBG.layers[i].layout;
				dstBG.layers[i].width = srcBG.layers[i].width;
				dstBG.layers[i].height = srcBG.layers[i].height;
			}
			dstBG.Write(Path.Combine(dstFol, "Backgrounds.bin"));
			RSDKv4.StageConfig srcConf = new RSDKv4.StageConfig(Path.Combine(srcFol, "StageConfig.bin"));
			RSDKv3.StageConfig dstConf = new RSDKv3.StageConfig
			{
				stagePalette = srcConf.stagePalette,
				soundFX = srcConf.soundFX
			};
			RSDKv4.Scene srcScene = new RSDKv4.Scene(srcFile);
			RSDKv3.Scene dstScene = new RSDKv3.Scene
			{
				title = srcScene.title,
				layout = srcScene.layout,
				width = srcScene.width,
				height = srcScene.height,
				activeLayer0 = srcScene.activeLayer0,
				activeLayer1 = srcScene.activeLayer1,
				activeLayer2 = srcScene.activeLayer2,
				activeLayer3 = srcScene.activeLayer3,
				layerMidpoint = srcScene.layerMidpoint
			};
			if (objMode == ObjectMode.ConvertAllToBlank)
			{
				dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Select(a => new RSDKv3.Scene.Entity(0, 0, a.xpos, a.ypos)));
			}
			else if (srcConf.loadGlobalObjects)
			{
				List<RSDKv3_4.GameConfig.ObjectInfo> srcObjs = null;
				if (File.Exists(srcGCFile))
					srcObjs = new RSDKv4.GameConfig(srcGCFile).objects;
				List<RSDKv3_4.GameConfig.ObjectInfo> dstObjs = null;
				if (File.Exists(dstGCFile))
					dstObjs = new RSDKv3.GameConfig(dstGCFile).objects;
				switch (objMode)
				{
					case ObjectMode.MatchGlobalsAddStage:
						Dictionary<int, int> objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.FindIndex(a => a.name == srcObjs[i].name);
							if (ind == -1)
							{
								dstConf.objects.Add(srcObjs[i]);
								objmap.Add(i + 1, dstObjs.Count + dstConf.objects.Count);
							}
							else
							{
								dstConf.loadGlobalObjects = true;
								objmap.Add(i + 1, ind + 1);
							}
						}
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + dstConf.objects.Count + 1);
						dstConf.objects.AddRange(srcConf.objects);
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Select(a => new RSDKv3.Scene.Entity((byte)objmap[a.type], a.propertyValue, a.xpos, a.ypos)));
						break;
					case ObjectMode.MatchGlobalsDelete:
						objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.FindIndex(a => a.name == srcObjs[i].name);
							if (ind == -1)
								objmap.Add(i + 1, -1);
							else
								objmap.Add(i + 1, ind + 1);
						}
						dstConf.loadGlobalObjects = objmap.Values.Any(a => a != -1);
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + 1);
						dstConf.objects = srcConf.objects;
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Where(a => objmap[a.type] != -1).Select(a => new RSDKv3.Scene.Entity((byte)objmap[a.type], a.propertyValue, a.xpos, a.ypos)));
						break;
					case ObjectMode.DeleteGlobal:
						dstConf.objects = srcConf.objects;
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Where(a => a.type > srcObjs.Count).Select(a => new RSDKv3.Scene.Entity((byte)(a.type - srcObjs.Count), a.propertyValue, a.xpos, a.ypos)));
						break;
					case ObjectMode.AddGlobalStage:
						dstConf.objects = new List<RSDKv3_4.GameConfig.ObjectInfo>(srcObjs.Concat(srcConf.objects));
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Select(a => new RSDKv3.Scene.Entity(a.type, a.propertyValue, a.xpos, a.ypos)));
						break;
					case ObjectMode.AsIs:
						dstConf.loadGlobalObjects = true;
						dstConf.objects = srcConf.objects;
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Select(a => new RSDKv3.Scene.Entity(a.type, a.propertyValue, a.xpos, a.ypos)));
						break;
				}
			}
			else if (objMode != ObjectMode.DontInclude)
			{
				dstConf.objects = srcConf.objects;
				dstScene.entities = new List<RSDKv3_4.Scene.Entity>(srcScene.entities.Select(a => new RSDKv3.Scene.Entity(a.type, a.propertyValue, a.xpos, a.ypos)));
			}
			dstConf.Write(Path.Combine(dstFol, "StageConfig.bin"));
			dstScene.Write(dstFile);
		}

		private static void ConvertV4ToV5(string srcFile, string dstFile, ObjectMode objMode, string srcGCFile, string dstGCFile)
		{
			string srcFol = Path.GetDirectoryName(srcFile);
			string dstFol = Path.GetDirectoryName(dstFile);
			File.Copy(Path.Combine(srcFol, "16x16Tiles.gif"), Path.Combine(dstFol, "16x16Tiles.gif"), true);
			RSDKv3_4.TileConfig srcTiles = new RSDKv3_4.TileConfig(Path.Combine(srcFol, "CollisionMasks.bin"));
			RSDKv5.TileConfig dstTiles = new RSDKv5.TileConfig();
			for (int i = 0; i < 2; i++)
				dstTiles.collisionMasks[i] = srcTiles.collisionMasks[i].Select(a => new RSDKv5.TileConfig.CollisionMask()
				{
					flipY = a.flipY,
					flags = a.flags,
					floorAngle = a.floorAngle,
					lWallAngle = a.lWallAngle,
					rWallAngle = a.rWallAngle,
					roofAngle = a.roofAngle,
					heightMasks = a.heightMasks.Select(b => new RSDKv5.TileConfig.CollisionMask.HeightMask()
					{
						height = b.height,
						solid = b.solid
					}).ToArray()
				}).ToArray();
			dstTiles.Write(Path.Combine(dstFol, "TileConfig.bin"));
			RSDKv4.StageConfig srcConf = new RSDKv4.StageConfig(Path.Combine(srcFol, "StageConfig.bin"));
			RSDKv5.StageConfig dstConf = new RSDKv5.StageConfig
			{
				soundFX = srcConf.soundFX.Select(a => new RSDKv5.GameConfig.SoundInfo() { name = a.path }).ToList()
			};
			RSDKv3_4.Gif gif = new RSDKv3_4.Gif(Path.Combine(srcFol, "16x16Tiles.gif"));
			bool[] blanktiles = new bool[0x400];
			for (int i = 0; i < 0x400; i++)
				blanktiles[i] = gif.pixels.FastArrayEqual(0, i * 256, 256) && srcTiles.collisionMasks[0][i].heightMasks.All(a => !a.solid) && srcTiles.collisionMasks[1][i].heightMasks.All(a => !a.solid);
			for (int i = 0; i < 16; i++)
				gif.palette.Skip(i * 16).Take(16).Select(b => new RSDKv5.Color(b.r, b.g, b.b)).ToArray().CopyTo(dstConf.palettes[0].colors[i], 0);
			srcConf.stagePalette.colors.Select(a => a.Select(b => new RSDKv5.Color(b.r, b.g, b.b)).ToArray()).ToArray().CopyTo(dstConf.palettes[0].colors, 6);
			RSDKv3_4.Tiles128x128 srcChunks = new RSDKv3_4.Tiles128x128(Path.Combine(srcFol, "128x128Tiles.bin"));
			RSDKv4.Scene srcScene = new RSDKv4.Scene(srcFile);
			RSDKv4.Backgrounds srcBG = new RSDKv4.Backgrounds(Path.Combine(srcFol, "Backgrounds.bin"));
			RSDKv5.Scene dstScene = new RSDKv5.Scene();
			RSDKv5.SceneLayer fgLow = new RSDKv5.SceneLayer("FG Low", (ushort)(srcScene.width * 8), (ushort)(srcScene.height * 8))
			{
				drawOrder = 1
			};
			RSDKv5.SceneLayer fgHigh = new RSDKv5.SceneLayer("FG High", (ushort)(srcScene.width * 8), (ushort)(srcScene.height * 8))
			{
				drawOrder = 6
			};
			bool hightiles = false;
			for (int y = 0; y < srcScene.height; y++)
				for (int x = 0; x < srcScene.width; x++)
					if (srcScene.layout[y][x] < srcChunks.chunkList.Length)
					{
						RSDKv3_4.Tiles128x128.Block block = srcChunks.chunkList[srcScene.layout[y][x]];
						for (int by = 0; by < 8; by++)
							for (int bx = 0; bx < 8; bx++)
							{
								RSDKv3_4.Tiles128x128.Block.Tile srcTile = block.tiles[by][bx];
								if (!blanktiles[srcTile.tileIndex])
								{
									RSDKv5.SceneLayer.Tile dstTile = new RSDKv5.SceneLayer.Tile
									{
										tileIndex = srcTile.tileIndex,
										direction = (RSDKv5.SceneLayer.Tile.Directions)srcTile.direction
									};
									switch (srcTile.solidityA)
									{
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll:
											dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidAll;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop:
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip:
											dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidTop;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop:
											dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone:
											dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidNone;
											break;
									}
									switch (srcTile.solidityB)
									{
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll:
											dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidAll;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop:
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip:
											dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidTop;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop:
											dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone:
											dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidNone;
											break;
									}
									switch (srcTile.visualPlane)
									{
										case RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.Low:
											fgLow.layout[y * 8 + by][x * 8 + bx] = dstTile;
											break;
										case RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High:
											fgHigh.layout[y * 8 + by][x * 8 + bx] = dstTile;
											hightiles = true;
											break;
									}
								}
							}
					}
			dstScene.layers.Add(fgLow);
			if (hightiles)
				dstScene.layers.Add(fgHigh);
			List<RSDKv3_4.Backgrounds.Layer> bglayers = new List<RSDKv3_4.Backgrounds.Layer>(8);
			int midpoint = int.MaxValue;
			if (srcScene.layerMidpoint == RSDKv3_4.Scene.LayerMidpoints.BeforeLayer0)
				midpoint = 0;
			switch (srcScene.activeLayer0)
			{
				case RSDKv3_4.Scene.ActiveLayers.Foreground:
				case RSDKv3_4.Scene.ActiveLayers.None:
					break;
				default:
					bglayers.Add(srcBG.layers[(int)srcScene.activeLayer0 - 1]);
					break;
			}
			if (srcScene.layerMidpoint == RSDKv3_4.Scene.LayerMidpoints.AfterLayer0)
				midpoint = bglayers.Count;
			switch (srcScene.activeLayer1)
			{
				case RSDKv3_4.Scene.ActiveLayers.Foreground:
				case RSDKv3_4.Scene.ActiveLayers.None:
					break;
				default:
					bglayers.Add(srcBG.layers[(int)srcScene.activeLayer1 - 1]);
					break;
			}
			if (srcScene.layerMidpoint == RSDKv3_4.Scene.LayerMidpoints.AfterLayer1)
				midpoint = bglayers.Count;
			switch (srcScene.activeLayer2)
			{
				case RSDKv3_4.Scene.ActiveLayers.Foreground:
				case RSDKv3_4.Scene.ActiveLayers.None:
					break;
				default:
					bglayers.Add(srcBG.layers[(int)srcScene.activeLayer2 - 1]);
					break;
			}
			if (srcScene.layerMidpoint == RSDKv3_4.Scene.LayerMidpoints.AfterLayer2)
				midpoint = bglayers.Count;
			switch (srcScene.activeLayer3)
			{
				case RSDKv3_4.Scene.ActiveLayers.Foreground:
				case RSDKv3_4.Scene.ActiveLayers.None:
					break;
				default:
					bglayers.Add(srcBG.layers[(int)srcScene.activeLayer3 - 1]);
					break;
			}
			int activelayers = bglayers.Count;
			bglayers.AddRange(srcBG.layers.Where(a => a.width > 0 && a.height > 0 && !bglayers.Contains(a)));
			for (int i = 0; i < bglayers.Count; i++)
			{
				RSDKv3_4.Backgrounds.Layer srcLayer = bglayers[i];
				RSDKv5.SceneLayer dstLayer = new RSDKv5.SceneLayer($"Background {i + 1}", (ushort)(srcLayer.width * 8), (ushort)(srcLayer.height * 8));
				hightiles = false;
				if (i < activelayers && i >= midpoint)
				{
					dstLayer.drawOrder = 7;
					hightiles = true;
				}
				else if (i >= activelayers)
					dstLayer.drawOrder = 16;
				byte[] scrollinds = srcLayer.lineScroll.Distinct().ToArray();
				switch (srcLayer.type)
				{
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll:
						dstLayer.type = RSDKv5.SceneLayer.Types.HScroll;
						dstLayer.scrollInfo = scrollinds.Select(a => new RSDKv5.ScrollInfo()
						{
							deform = srcBG.hScroll[a].deform,
							parallaxFactor = (short)srcBG.hScroll[a].parallaxFactor,
							scrollSpeed = (short)(srcBG.hScroll[a].scrollSpeed << 2)
						}).ToList();
						dstLayer.lineScroll = srcLayer.lineScroll.Select(a => (byte)Array.IndexOf(scrollinds, a)).ToArray();
						break;
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll:
						dstLayer.type = RSDKv5.SceneLayer.Types.VScroll;
						dstLayer.scrollInfo = scrollinds.Select(a => new RSDKv5.ScrollInfo()
						{
							deform = srcBG.vScroll[a].deform,
							parallaxFactor = (short)srcBG.vScroll[a].parallaxFactor,
							scrollSpeed = (short)(srcBG.vScroll[a].scrollSpeed << 2)
						}).ToList();
						dstLayer.lineScroll = srcLayer.lineScroll.Select(a => (byte)Array.IndexOf(scrollinds, a)).ToArray();
						break;
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.Sky3D:
					case RSDKv3_4.Backgrounds.Layer.LayerTypes.Floor3D:
						dstLayer.type = RSDKv5.SceneLayer.Types.RotoZoom;
						break;
				}
				dstLayer.parallaxFactor = (short)srcLayer.parallaxFactor;
				dstLayer.scrollSpeed = (short)(srcLayer.scrollSpeed << 2);
				bool blank = true;
				for (int y = 0; y < srcLayer.height; y++)
					for (int x = 0; x < srcLayer.width; x++)
						if (srcLayer.layout[y][x] < srcChunks.chunkList.Length)
						{
							RSDKv3_4.Tiles128x128.Block block = srcChunks.chunkList[srcLayer.layout[y][x]];
							for (int by = 0; by < 8; by++)
								for (int bx = 0; bx < 8; bx++)
								{
									RSDKv3_4.Tiles128x128.Block.Tile srcTile = block.tiles[by][bx];
									if (!blanktiles[srcTile.tileIndex])
									{
										if (i < activelayers)
											switch (srcTile.visualPlane)
											{
												case RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.Low:
													if (hightiles)
														continue;
													break;
												case RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High:
													if (!hightiles)
														continue;
													break;
											}
										blank = false;
										RSDKv5.SceneLayer.Tile dstTile = new RSDKv5.SceneLayer.Tile
										{
											tileIndex = srcTile.tileIndex,
											direction = (RSDKv5.SceneLayer.Tile.Directions)srcTile.direction
										};
										switch (srcTile.solidityA)
										{
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll:
												dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidAll;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop:
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip:
												dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidTop;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop:
												dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone:
												dstTile.solidityA = RSDKv5.SceneLayer.Tile.Solidities.SolidNone;
												break;
										}
										switch (srcTile.solidityB)
										{
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll:
												dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidAll;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop:
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip:
												dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidTop;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop:
												dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop;
												break;
											case RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone:
												dstTile.solidityB = RSDKv5.SceneLayer.Tile.Solidities.SolidNone;
												break;
										}
										dstLayer.layout[y * 8 + by][x * 8 + bx] = dstTile;
									}
								}
						}
				if (!blank)
					dstScene.layers.Add(dstLayer);
				if (dstScene.layers.Count == 8)
					break;
			}
			if (objMode == ObjectMode.ConvertAllToBlank)
			{
				dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
				dstScene.objects[0].entities = new List<RSDKv5.SceneEntity>(srcScene.entities.Select(a => new RSDKv5.SceneEntity(dstScene.objects[0], (ushort)srcScene.entities.IndexOf(a)) { xpos = a.xpos, ypos = a.ypos }));
			}
			else if (srcConf.loadGlobalObjects)
			{
				List<RSDKv3_4.GameConfig.ObjectInfo> srcObjs = null;
				if (File.Exists(srcGCFile))
					srcObjs = new RSDKv4.GameConfig(srcGCFile).objects;
				List<string> dstObjs = null;
				if (File.Exists(dstGCFile))
					dstObjs = new RSDKv5.GameConfig(dstGCFile).objects;
				switch (objMode)
				{
					case ObjectMode.MatchGlobalsAddStage:
						Dictionary<int, int> objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.IndexOf(srcObjs[i].name);
							if (ind == -1)
							{
								dstConf.objects.Add(srcObjs[i].name);
								objmap.Add(i + 1, dstObjs.Count + dstConf.objects.Count);
							}
							else
							{
								dstConf.loadGlobalObjects = true;
								objmap.Add(i + 1, ind + 1);
							}
						}
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + dstConf.objects.Count + 1);
						dstConf.objects.AddRange(srcConf.objects.Select(a => a.name));
						dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
						foreach (var obj in dstObjs)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var obj in dstConf.objects)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var ent in srcScene.entities)
						{
							RSDKv5.SceneObject so = dstScene.objects[objmap[ent.type]];
							so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
						}
						break;
					case ObjectMode.MatchGlobalsDelete:
						objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.IndexOf(srcObjs[i].name);
							if (ind == -1)
								objmap.Add(i + 1, -1);
							else
								objmap.Add(i + 1, ind + 1);
						}
						dstConf.loadGlobalObjects = objmap.Values.Any(a => a != -1);
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + 1);
						dstConf.objects = srcConf.objects.Select(a => a.name).ToList();
						dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
						foreach (var obj in dstObjs)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var obj in dstConf.objects)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var ent in srcScene.entities.Where(a => objmap[a.type] != -1))
						{
							RSDKv5.SceneObject so = dstScene.objects[objmap[ent.type]];
							so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
						}
						break;
					case ObjectMode.DeleteGlobal:
						dstConf.objects = srcConf.objects.Select(a => a.name).ToList();
						dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
						foreach (var obj in dstConf.objects)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var ent in srcScene.entities.Where(a => a.type > srcObjs.Count))
						{
							RSDKv5.SceneObject so = dstScene.objects[ent.type - srcObjs.Count];
							so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
						}
						break;
					case ObjectMode.AddGlobalStage:
						dstConf.objects = srcObjs.Select(a => a.name).Concat(srcConf.objects.Select(a => a.name)).ToList();
						dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
						foreach (var obj in dstConf.objects)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var ent in srcScene.entities)
						{
							RSDKv5.SceneObject so = dstScene.objects[ent.type];
							so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
						}
						break;
					case ObjectMode.AsIs:
						dstConf.loadGlobalObjects = true;
						dstConf.objects = srcConf.objects.Select(a => a.name).ToList();
						dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
						foreach (var obj in dstObjs)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var obj in dstConf.objects)
							dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
						foreach (var ent in srcScene.entities)
						{
							RSDKv5.SceneObject so = dstScene.objects[ent.type];
							so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
						}
						break;
				}
			}
			else if (objMode != ObjectMode.DontInclude)
			{
				dstConf.objects = srcConf.objects.Select(a => a.name).ToList();
				dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier("Blank Object") });
				foreach (var obj in dstConf.objects)
					dstScene.objects.Add(new RSDKv5.SceneObject() { name = new RSDKv5.NameIdentifier(obj) });
				foreach (var ent in srcScene.entities)
				{
					RSDKv5.SceneObject so = dstScene.objects[ent.type];
					so.entities.Add(new RSDKv5.SceneEntity(so, (ushort)srcScene.entities.IndexOf(ent)) { xpos = ent.xpos, ypos = ent.ypos });
				}
			}
			dstConf.Write(Path.Combine(dstFol, "StageConfig.bin"));
			dstScene.Write(dstFile);
		}

		private static void ConvertV5ToV3(string srcFile, string dstFile, ObjectMode objMode, string srcGCFile, string dstGCFile)
		{
			string srcFol = Path.GetDirectoryName(srcFile);
			string dstFol = Path.GetDirectoryName(dstFile);
			File.Copy(Path.Combine(srcFol, "16x16Tiles.gif"), Path.Combine(dstFol, "16x16Tiles.gif"), true);
			RSDKv5.TileConfig srcTiles = new RSDKv5.TileConfig(Path.Combine(srcFol, "TileConfig.bin"));
			RSDKv3_4.TileConfig dstTiles = new RSDKv3_4.TileConfig();
			for (int i = 0; i < 2; i++)
				dstTiles.collisionMasks[i] = srcTiles.collisionMasks[i].Select(a => new RSDKv3_4.TileConfig.CollisionMask()
				{
					flipY = a.flipY,
					flags = a.flags,
					floorAngle = a.floorAngle,
					lWallAngle = a.lWallAngle,
					rWallAngle = a.rWallAngle,
					roofAngle = a.roofAngle,
					heightMasks = a.heightMasks.Select(b => new RSDKv3_4.TileConfig.CollisionMask.HeightMask()
					{
						height = b.height,
						solid = b.solid
					}).ToArray()
				}).ToArray();
			dstTiles.Write(Path.Combine(dstFol, "CollisionMasks.bin"));
			RSDKv5.StageConfig srcConf = new RSDKv5.StageConfig(Path.Combine(srcFol, "StageConfig.bin"));
			RSDKv3_4.StageConfig dstConf = new RSDKv3.StageConfig
			{
				soundFX = srcConf.soundFX.Select(a => new RSDKv3_4.GameConfig.SoundInfo() { name = Path.GetFileNameWithoutExtension(a.name), path = a.name }).ToList()
			};
			dstConf.stagePalette.colors = srcConf.palettes[0].colors.Skip(6).Take(2).Select(a => a.Select(b => new RSDKv3_4.Palette.Color(b.r, b.g, b.b)).ToArray()).ToArray();
			RSDKv5.Scene srcScene = new RSDKv5.Scene(srcFile);
			srcScene.layers = srcScene.layers.OrderBy(a => a.drawOrder).ToList();
			RSDKv3_4.Scene dstScene = new RSDKv3.Scene();
			RSDKv5.SceneLayer fgLayer = srcScene.layers.Find(a => a.name == "Playfield");
			RSDKv5.SceneLayer fgHigh = null;
			if (fgLayer == null)
				fgLayer = srcScene.layers.Find(a => a.name == "FG Low");
			if (fgLayer == null)
				fgLayer = srcScene.layers.Find(a => a.name == "FG High");
			else
				fgHigh = srcScene.layers.Find(a => a.name == "FG High");
			RSDKv3_4.Tiles128x128 dstChunk = new RSDKv3_4.Tiles128x128();
			List<int[]> chunks = new List<int[]>(dstChunk.chunkList.Length) { new int[64] };
			chunks[0].FastFill(ushort.MaxValue);
			if (fgLayer != null)
			{
				int width = Math.DivRem(fgLayer.layout[0].Length, 8, out int rem);
				if (rem != 0)
					++width;
				int height = Math.DivRem(fgLayer.layout.Length, 8, out rem);
				if (rem != 0)
					++height;
				dstScene.resize((byte)width, (byte)height);
				if (fgHigh != null)
				{
					bool blank = true;
					for (int y = 0; y < height; y++)
						for (int x = 0; x < width; x++)
						{
							int[] ch = new int[64];
							for (int by = 0; by < 8; by++)
								for (int bx = 0; bx < 8; bx++)
									if ((y * 8) + by < fgLayer.layout.Length && (x * 8) + bx < fgLayer.layout[0].Length)
									{
										if (fgLayer.layout[(y * 8) + by][(x * 8) + bx].isBlank)
										{
											if (fgHigh.layout[(y * 8) + by][(x * 8) + bx].isBlank)
												ch[by * 8 + bx] = ushort.MaxValue;
											else
											{
												ch[by * 8 + bx] = fgHigh.layout[(y * 8) + by][(x * 8) + bx] | 0x10000;
												fgHigh.layout[(y * 8) + by][(x * 8) + bx] = new RSDKv5.SceneLayer.Tile();
											}
										}
										else
										{
											ch[by * 8 + bx] = fgLayer.layout[(y * 8) + by][(x * 8) + bx];
											if (!fgHigh.layout[(y * 8) + by][(x * 8) + bx].isBlank)
												blank = false;
										}
									}
									else
										ch[by * 8 + bx] = ushort.MaxValue;
							int cid = chunks.FindIndex(a => a.FastArrayEqual(ch));
							if (cid == -1)
							{
								if (chunks.Count < dstChunk.chunkList.Length)
									dstScene.layout[y][x] = (ushort)chunks.Count;
								chunks.Add(ch);
							}
							else
								dstScene.layout[y][x] = (ushort)cid;
						}
					if (blank)
						srcScene.layers.Remove(fgHigh);
				}
				else
				{
					for (int y = 0; y < height; y++)
						for (int x = 0; x < width; x++)
						{
							int[] ch = new int[64];
							for (int by = 0; by < 8; by++)
								for (int bx = 0; bx < 8; bx++)
									if ((y * 8) + by < fgLayer.layout.Length && (x * 8) + bx < fgLayer.layout[0].Length)
										ch[by * 8 + bx] = fgLayer.layout[(y * 8) + by][(x * 8) + bx];
									else
										ch[by * 8 + bx] = ushort.MaxValue;
							int cid = chunks.FindIndex(a => a.FastArrayEqual(ch));
							if (cid == -1)
							{
								if (chunks.Count < dstChunk.chunkList.Length)
									dstScene.layout[y][x] = (ushort)chunks.Count;
								chunks.Add(ch);
							}
							else
								dstScene.layout[y][x] = (ushort)cid;
						}
				}
			}
			List<int> activeLayers = new List<int>(5);
			List<RSDKv3_4.Backgrounds.Layer> bglayers = new List<RSDKv3_4.Backgrounds.Layer>(8);
			int plane = 0;
			RSDKv3_4.Backgrounds dstBG = new RSDKv3.Backgrounds();
			foreach (var layer in srcScene.layers)
			{
				if (layer == fgLayer)
				{
					activeLayers.Add(0);
					if (activeLayers.Count > 4)
						activeLayers.RemoveAt(0);
					if (fgHigh != null)
					{
						activeLayers.Add(0);
						if (activeLayers.Count > 4)
							activeLayers.RemoveAt(0);
						plane = 0x10000;
					}
				}
				else
				{
					int width = Math.DivRem(layer.layout[0].Length, 8, out int rem);
					if (rem != 0)
						++width;
					int height = Math.DivRem(layer.layout.Length, 8, out rem);
					if (rem != 0)
						++height;
					RSDKv3_4.Backgrounds.Layer bg = new RSDKv3.Backgrounds.Layer((byte)width, (byte)height)
					{
						scrollSpeed = (byte)(layer.scrollSpeed >> 2),
						parallaxFactor = (ushort)layer.parallaxFactor,
					};
					switch (layer.type)
					{
						case RSDKv5.SceneLayer.Types.HScroll:
							bg.type = RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll;
							bg.lineScroll = layer.lineScroll.Select(a => (byte)(a + dstBG.hScroll.Count)).ToArray();
							dstBG.hScroll.AddRange(layer.scrollInfo.Select(a => new RSDKv3.Backgrounds.ScrollInfo()
							{
								deform = a.deform,
								parallaxFactor = (ushort)a.parallaxFactor,
								scrollSpeed = (byte)(a.scrollSpeed >> 2)
							}));
							break;
						case RSDKv5.SceneLayer.Types.VScroll:
							bg.type = RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll;
							bg.lineScroll = layer.lineScroll.Select(a => (byte)(a + dstBG.vScroll.Count)).ToArray();
							dstBG.vScroll.AddRange(layer.scrollInfo.Select(a => new RSDKv3.Backgrounds.ScrollInfo()
							{
								deform = a.deform,
								parallaxFactor = (ushort)a.parallaxFactor,
								scrollSpeed = (byte)(a.scrollSpeed >> 2)
							}));
							break;
						case RSDKv5.SceneLayer.Types.RotoZoom:
							bg.type = RSDKv3_4.Backgrounds.Layer.LayerTypes.Floor3D;
							break;
					}
					for (int y = 0; y < height; y++)
						for (int x = 0; x < width; x++)
						{
							int[] ch = new int[64];
							for (int by = 0; by < 8; by++)
								for (int bx = 0; bx < 8; bx++)
									if ((y * 8) + by < layer.layout.Length && (x * 8) + bx < layer.layout[0].Length)
									{
										if (layer.layout[(y * 8) + by][(x * 8) + bx].isBlank)
											ch[by * 8 + bx] = ushort.MaxValue;
										else
											ch[by * 8 + bx] = layer.layout[(y * 8) + by][(x * 8) + bx] | plane;
									}
									else
										ch[by * 8 + bx] = ushort.MaxValue;
							int cid = chunks.FindIndex(a => a.FastArrayEqual(ch));
							if (cid == -1)
							{
								if (chunks.Count < dstChunk.chunkList.Length)
									bg.layout[y][x] = (ushort)chunks.Count;
								chunks.Add(ch);
							}
							else
								bg.layout[y][x] = (ushort)cid;
						}
					bglayers.Add(bg);
					if (layer.drawOrder != 16 && (layer == fgHigh || activeLayers.Count < 4 || activeLayers[0] != 0))
					{
						activeLayers.Add(bglayers.Count);
						if (activeLayers.Count > 4)
							activeLayers.RemoveAt(0);
					}
				}
			}
			bglayers.CopyTo(dstBG.layers);
			dstScene.activeLayer0 = (RSDKv3_4.Scene.ActiveLayers)activeLayers[0];
			if (activeLayers.Count > 1)
			{
				dstScene.activeLayer1 = (RSDKv3_4.Scene.ActiveLayers)activeLayers[1];
				if (activeLayers.Count > 2)
				{
					dstScene.activeLayer2 = (RSDKv3_4.Scene.ActiveLayers)activeLayers[2];
					if (activeLayers.Count > 3)
						dstScene.activeLayer3 = (RSDKv3_4.Scene.ActiveLayers)activeLayers[3];
					else
						dstScene.activeLayer3 = RSDKv3_4.Scene.ActiveLayers.None;
				}
				else
				{
					dstScene.activeLayer2 = RSDKv3_4.Scene.ActiveLayers.None;
					dstScene.activeLayer3 = RSDKv3_4.Scene.ActiveLayers.None;
				}
			}
			else
			{
				dstScene.activeLayer1 = RSDKv3_4.Scene.ActiveLayers.None;
				dstScene.activeLayer2 = RSDKv3_4.Scene.ActiveLayers.None;
				dstScene.activeLayer3 = RSDKv3_4.Scene.ActiveLayers.None;
			}
			dstScene.layerMidpoint = (RSDKv3_4.Scene.LayerMidpoints)activeLayers.IndexOf(0) + 1;
			if (chunks.Count > dstChunk.chunkList.Length)
				MessageBox.Show(Instance, $"Chunk count exceeded limit by {chunks.Count - dstChunk.chunkList.Length}! Some layers will be missing chunks.", "Conversion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			chunks.Take(dstChunk.chunkList.Length).Select(src =>
			{
				var newcnk = new RSDKv3_4.Tiles128x128.Block();
				for (int by = 0; by < 8; by++)
					for (int bx = 0; bx < 8; bx++)
					{
						RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes p = (src[by * 8 + bx] & 0x10000) == 0x10000 ? RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High : RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.Low;
						RSDKv5.SceneLayer.Tile v = src[by * 8 + bx];
						RSDKv3_4.Tiles128x128.Block.Tile tile = new RSDKv3_4.Tiles128x128.Block.Tile();
						tile.tileIndex = v.tileIndex;
						if (!v.isBlank)
						{
							tile.direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)v.direction;
							switch (v.solidityA)
							{
								case RSDKv5.SceneLayer.Tile.Solidities.SolidNone:
									tile.solidityA = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidTop:
									tile.solidityA = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop:
									tile.solidityA = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidAll:
									tile.solidityA = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll;
									break;
							}
							switch (v.solidityB)
							{
								case RSDKv5.SceneLayer.Tile.Solidities.SolidNone:
									tile.solidityB = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidTop:
									tile.solidityB = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop:
									tile.solidityB = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidAll:
									tile.solidityB = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll;
									break;
							}
							tile.visualPlane = p;
						}
						newcnk.tiles[by][bx] = tile;
					}
				return newcnk;
			}).ToArray().CopyTo(dstChunk.chunkList, 0);
			dstChunk.Write(Path.Combine(dstFol, "128x128Tiles.bin"));
			IOrderedEnumerable<RSDKv5.SceneEntity> sceneEntities = srcScene.objects.SelectMany(a => a.entities).OrderBy(a => a.slotID);
			if (objMode == ObjectMode.ConvertAllToBlank)
			{
				dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Select(a => new RSDKv3.Scene.Entity(0, 0, a.xpos, a.ypos)));
			}
			else if (srcConf.loadGlobalObjects)
			{
				List<string> srcObjs = null;
				if (File.Exists(srcGCFile))
					srcObjs = new RSDKv5.GameConfig(srcGCFile).objects;
				List<RSDKv3_4.GameConfig.ObjectInfo> dstObjs = null;
				if (File.Exists(dstGCFile))
					dstObjs = new RSDKv3.GameConfig(dstGCFile).objects;
				switch (objMode)
				{
					case ObjectMode.MatchGlobalsAddStage:
						Dictionary<int, int> objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.FindIndex(a => a.name == srcObjs[i]);
							if (ind == -1)
							{
								dstConf.objects.Add(new RSDKv3_4.GameConfig.ObjectInfo() { name = srcObjs[i], script = srcObjs[i] + ".txt" });
								objmap.Add(i + 1, dstObjs.Count + dstConf.objects.Count);
							}
							else
							{
								dstConf.loadGlobalObjects = true;
								objmap.Add(i + 1, ind + 1);
							}
						}
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + dstConf.objects.Count + 1);
						dstConf.objects.AddRange(srcConf.objects.Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }));
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Select(a => new RSDKv3.Scene.Entity((byte)objmap[srcScene.objects.IndexOf(a.type)], 0, a.xpos, a.ypos)));
						break;
					case ObjectMode.MatchGlobalsDelete:
						objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.FindIndex(a => a.name == srcObjs[i]);
							if (ind == -1)
								objmap.Add(i + 1, -1);
							else
								objmap.Add(i + 1, ind + 1);
						}
						dstConf.loadGlobalObjects = objmap.Values.Any(a => a != -1);
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + 1);
						dstConf.objects = srcConf.objects.Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }).ToList();
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Where(a => objmap[srcScene.objects.IndexOf(a.type)] != -1).Select(a => new RSDKv3.Scene.Entity((byte)objmap[srcScene.objects.IndexOf(a.type)], 0, a.xpos, a.ypos)));
						break;
					case ObjectMode.DeleteGlobal:
						dstConf.objects = srcConf.objects.Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }).ToList();
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Where(a => srcScene.objects.IndexOf(a.type) > srcObjs.Count).Select(a => new RSDKv3.Scene.Entity((byte)(srcScene.objects.IndexOf(a.type) - srcObjs.Count), 0, a.xpos, a.ypos)));
						break;
					case ObjectMode.AddGlobalStage:
						dstConf.objects = srcObjs.Concat(srcConf.objects).Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }).ToList();
						dstConf.objects = new List<RSDKv3_4.GameConfig.ObjectInfo>();
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Select(a => new RSDKv3.Scene.Entity((byte)srcScene.objects.IndexOf(a.type), 0, a.xpos, a.ypos)));
						break;
					case ObjectMode.AsIs:
						dstConf.loadGlobalObjects = true;
						dstConf.objects = srcConf.objects.Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }).ToList();
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Select(a => new RSDKv3.Scene.Entity((byte)srcScene.objects.IndexOf(a.type), 0, a.xpos, a.ypos)));
						break;
				}
			}
			else if (objMode != ObjectMode.DontInclude)
			{
				dstConf.objects = srcConf.objects.Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }).ToList();
				dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Select(a => new RSDKv3.Scene.Entity((byte)srcScene.objects.IndexOf(a.type), 0, a.xpos, a.ypos)));
			}
			dstConf.Write(Path.Combine(dstFol, "StageConfig.bin"));
			dstScene.Write(dstFile);
			dstBG.Write(Path.Combine(dstFol, "Backgrounds.bin"));
		}

		private static void ConvertV5ToV4(string srcFile, string dstFile, ObjectMode objMode, string srcGCFile, string dstGCFile)
		{
			string srcFol = Path.GetDirectoryName(srcFile);
			string dstFol = Path.GetDirectoryName(dstFile);
			File.Copy(Path.Combine(srcFol, "16x16Tiles.gif"), Path.Combine(dstFol, "16x16Tiles.gif"), true);
			RSDKv5.TileConfig srcTiles = new RSDKv5.TileConfig(Path.Combine(srcFol, "TileConfig.bin"));
			RSDKv3_4.TileConfig dstTiles = new RSDKv3_4.TileConfig();
			for (int i = 0; i < 2; i++)
				dstTiles.collisionMasks[i] = srcTiles.collisionMasks[i].Select(a => new RSDKv3_4.TileConfig.CollisionMask()
				{
					flipY = a.flipY,
					flags = a.flags,
					floorAngle = a.floorAngle,
					lWallAngle = a.lWallAngle,
					rWallAngle = a.rWallAngle,
					roofAngle = a.roofAngle,
					heightMasks = a.heightMasks.Select(b => new RSDKv3_4.TileConfig.CollisionMask.HeightMask()
					{
						height = b.height,
						solid = b.solid
					}).ToArray()
				}).ToArray();
			dstTiles.Write(Path.Combine(dstFol, "CollisionMasks.bin"));
			RSDKv5.StageConfig srcConf = new RSDKv5.StageConfig(Path.Combine(srcFol, "StageConfig.bin"));
			RSDKv3_4.StageConfig dstConf = new RSDKv4.StageConfig
			{
				soundFX = srcConf.soundFX.Select(a => new RSDKv3_4.GameConfig.SoundInfo() { name = Path.GetFileNameWithoutExtension(a.name), path = a.name }).ToList()
			};
			dstConf.stagePalette.colors = srcConf.palettes[0].colors.Skip(6).Take(2).Select(a => a.Select(b => new RSDKv3_4.Palette.Color(b.r, b.g, b.b)).ToArray()).ToArray();
			RSDKv5.Scene srcScene = new RSDKv5.Scene(srcFile);
			srcScene.layers = srcScene.layers.OrderBy(a => a.drawOrder).ToList();
			RSDKv3_4.Scene dstScene = new RSDKv4.Scene();
			RSDKv5.SceneLayer fgLayer = srcScene.layers.Find(a => a.name == "Playfield");
			RSDKv5.SceneLayer fgHigh = null;
			if (fgLayer == null)
				fgLayer = srcScene.layers.Find(a => a.name == "FG Low");
			if (fgLayer == null)
				fgLayer = srcScene.layers.Find(a => a.name == "FG High");
			else
				fgHigh = srcScene.layers.Find(a => a.name == "FG High");
			RSDKv3_4.Tiles128x128 dstChunk = new RSDKv3_4.Tiles128x128();
			List<int[]> chunks = new List<int[]>(dstChunk.chunkList.Length) { new int[64] };
			chunks[0].FastFill(ushort.MaxValue);
			if (fgLayer != null)
			{
				int width = Math.DivRem(fgLayer.layout[0].Length, 8, out int rem);
				if (rem != 0)
					++width;
				int height = Math.DivRem(fgLayer.layout.Length, 8, out rem);
				if (rem != 0)
					++height;
				dstScene.resize((byte)width, (byte)height);
				if (fgHigh != null)
				{
					bool blank = true;
					for (int y = 0; y < height; y++)
						for (int x = 0; x < width; x++)
						{
							int[] ch = new int[64];
							for (int by = 0; by < 8; by++)
								for (int bx = 0; bx < 8; bx++)
									if ((y * 8) + by < fgLayer.layout.Length && (x * 8) + bx < fgLayer.layout[0].Length)
									{
										if (fgLayer.layout[(y * 8) + by][(x * 8) + bx].isBlank)
										{
											if (fgHigh.layout[(y * 8) + by][(x * 8) + bx].isBlank)
												ch[by * 8 + bx] = ushort.MaxValue;
											else
											{
												ch[by * 8 + bx] = fgHigh.layout[(y * 8) + by][(x * 8) + bx] | 0x10000;
												fgHigh.layout[(y * 8) + by][(x * 8) + bx] = new RSDKv5.SceneLayer.Tile();
											}
										}
										else
										{
											ch[by * 8 + bx] = fgLayer.layout[(y * 8) + by][(x * 8) + bx];
											if (!fgHigh.layout[(y * 8) + by][(x * 8) + bx].isBlank)
												blank = false;
										}
									}
									else
										ch[by * 8 + bx] = ushort.MaxValue;
							int cid = chunks.FindIndex(a => a.FastArrayEqual(ch));
							if (cid == -1)
							{
								if (chunks.Count < dstChunk.chunkList.Length)
									dstScene.layout[y][x] = (ushort)chunks.Count;
								chunks.Add(ch);
							}
							else
								dstScene.layout[y][x] = (ushort)cid;
						}
					if (blank)
						srcScene.layers.Remove(fgHigh);
				}
				else
				{
					for (int y = 0; y < height; y++)
						for (int x = 0; x < width; x++)
						{
							int[] ch = new int[64];
							for (int by = 0; by < 8; by++)
								for (int bx = 0; bx < 8; bx++)
									if ((y * 8) + by < fgLayer.layout.Length && (x * 8) + bx < fgLayer.layout[0].Length)
										ch[by * 8 + bx] = fgLayer.layout[(y * 8) + by][(x * 8) + bx];
									else
										ch[by * 8 + bx] = ushort.MaxValue;
							int cid = chunks.FindIndex(a => a.FastArrayEqual(ch));
							if (cid == -1)
							{
								if (chunks.Count < dstChunk.chunkList.Length)
									dstScene.layout[y][x] = (ushort)chunks.Count;
								chunks.Add(ch);
							}
							else
								dstScene.layout[y][x] = (ushort)cid;
						}
				}
			}
			List<int> activeLayers = new List<int>(5);
			List<RSDKv3_4.Backgrounds.Layer> bglayers = new List<RSDKv3_4.Backgrounds.Layer>(8);
			int plane = 0;
			RSDKv3_4.Backgrounds dstBG = new RSDKv4.Backgrounds();
			foreach (var layer in srcScene.layers)
			{
				if (layer == fgLayer)
				{
					activeLayers.Add(0);
					if (activeLayers.Count > 4)
						activeLayers.RemoveAt(0);
					if (fgHigh != null)
					{
						activeLayers.Add(0);
						if (activeLayers.Count > 4)
							activeLayers.RemoveAt(0);
						plane = 0x10000;
					}
				}
				else
				{
					int width = Math.DivRem(layer.layout[0].Length, 8, out int rem);
					if (rem != 0)
						++width;
					int height = Math.DivRem(layer.layout.Length, 8, out rem);
					if (rem != 0)
						++height;
					RSDKv3_4.Backgrounds.Layer bg = new RSDKv4.Backgrounds.Layer((byte)width, (byte)height)
					{
						scrollSpeed = (byte)(layer.scrollSpeed >> 2),
						parallaxFactor = (ushort)layer.parallaxFactor,
					};
					switch (layer.type)
					{
						case RSDKv5.SceneLayer.Types.HScroll:
							bg.type = RSDKv3_4.Backgrounds.Layer.LayerTypes.HScroll;
							bg.lineScroll = layer.lineScroll.Select(a => (byte)(a + dstBG.hScroll.Count)).ToArray();
							dstBG.hScroll.AddRange(layer.scrollInfo.Select(a => new RSDKv4.Backgrounds.ScrollInfo()
							{
								deform = a.deform,
								parallaxFactor = (ushort)a.parallaxFactor,
								scrollSpeed = (byte)(a.scrollSpeed >> 2)
							}));
							break;
						case RSDKv5.SceneLayer.Types.VScroll:
							bg.type = RSDKv3_4.Backgrounds.Layer.LayerTypes.VScroll;
							bg.lineScroll = layer.lineScroll.Select(a => (byte)(a + dstBG.vScroll.Count)).ToArray();
							dstBG.vScroll.AddRange(layer.scrollInfo.Select(a => new RSDKv4.Backgrounds.ScrollInfo()
							{
								deform = a.deform,
								parallaxFactor = (ushort)a.parallaxFactor,
								scrollSpeed = (byte)(a.scrollSpeed >> 2)
							}));
							break;
						case RSDKv5.SceneLayer.Types.RotoZoom:
							bg.type = RSDKv3_4.Backgrounds.Layer.LayerTypes.Floor3D;
							break;
					}
					for (int y = 0; y < height; y++)
						for (int x = 0; x < width; x++)
						{
							int[] ch = new int[64];
							for (int by = 0; by < 8; by++)
								for (int bx = 0; bx < 8; bx++)
									if ((y * 8) + by < layer.layout.Length && (x * 8) + bx < layer.layout[0].Length)
									{
										if (layer.layout[(y * 8) + by][(x * 8) + bx].isBlank)
											ch[by * 8 + bx] = ushort.MaxValue;
										else
											ch[by * 8 + bx] = layer.layout[(y * 8) + by][(x * 8) + bx] | plane;
									}
									else
										ch[by * 8 + bx] = ushort.MaxValue;
							int cid = chunks.FindIndex(a => a.FastArrayEqual(ch));
							if (cid == -1)
							{
								if (chunks.Count < dstChunk.chunkList.Length)
									bg.layout[y][x] = (ushort)chunks.Count;
								chunks.Add(ch);
							}
							else
								bg.layout[y][x] = (ushort)cid;
						}
					bglayers.Add(bg);
					if (layer.drawOrder != 16 && (layer == fgHigh || activeLayers.Count < 4 || activeLayers[0] != 0))
					{
						activeLayers.Add(bglayers.Count);
						if (activeLayers.Count > 4)
							activeLayers.RemoveAt(0);
					}
				}
			}
			bglayers.CopyTo(dstBG.layers);
			dstScene.activeLayer0 = (RSDKv3_4.Scene.ActiveLayers)activeLayers[0];
			if (activeLayers.Count > 1)
			{
				dstScene.activeLayer1 = (RSDKv3_4.Scene.ActiveLayers)activeLayers[1];
				if (activeLayers.Count > 2)
				{
					dstScene.activeLayer2 = (RSDKv3_4.Scene.ActiveLayers)activeLayers[2];
					if (activeLayers.Count > 3)
						dstScene.activeLayer3 = (RSDKv3_4.Scene.ActiveLayers)activeLayers[3];
					else
						dstScene.activeLayer3 = RSDKv3_4.Scene.ActiveLayers.None;
				}
				else
				{
					dstScene.activeLayer2 = RSDKv3_4.Scene.ActiveLayers.None;
					dstScene.activeLayer3 = RSDKv3_4.Scene.ActiveLayers.None;
				}
			}
			else
			{
				dstScene.activeLayer1 = RSDKv3_4.Scene.ActiveLayers.None;
				dstScene.activeLayer2 = RSDKv3_4.Scene.ActiveLayers.None;
				dstScene.activeLayer3 = RSDKv3_4.Scene.ActiveLayers.None;
			}
			dstScene.layerMidpoint = (RSDKv3_4.Scene.LayerMidpoints)activeLayers.IndexOf(0) + 1;
			if (chunks.Count > dstChunk.chunkList.Length)
				MessageBox.Show(Instance, $"Chunk count exceeded limit by {chunks.Count - dstChunk.chunkList.Length}! Some layers will be missing chunks.", "Conversion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			chunks.Take(dstChunk.chunkList.Length).Select(src =>
			{
				var newcnk = new RSDKv3_4.Tiles128x128.Block();
				for (int by = 0; by < 8; by++)
					for (int bx = 0; bx < 8; bx++)
					{
						RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes p = (src[by * 8 + bx] & 0x10000) == 0x10000 ? RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High : RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.Low;
						RSDKv5.SceneLayer.Tile v = src[by * 8 + bx];
						RSDKv3_4.Tiles128x128.Block.Tile tile = new RSDKv3_4.Tiles128x128.Block.Tile();
						tile.tileIndex = v.tileIndex;
						if (!v.isBlank)
						{
							tile.direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)v.direction;
							switch (v.solidityA)
							{
								case RSDKv5.SceneLayer.Tile.Solidities.SolidNone:
									tile.solidityA = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidTop:
									tile.solidityA = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop:
									tile.solidityA = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidAll:
									tile.solidityA = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll;
									break;
							}
							switch (v.solidityB)
							{
								case RSDKv5.SceneLayer.Tile.Solidities.SolidNone:
									tile.solidityB = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidNone;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidTop:
									tile.solidityB = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidTop;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidAllButTop:
									tile.solidityB = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAllButTop;
									break;
								case RSDKv5.SceneLayer.Tile.Solidities.SolidAll:
									tile.solidityB = RSDKv3_4.Tiles128x128.Block.Tile.Solidities.SolidAll;
									break;
							}
							tile.visualPlane = p;
						}
						newcnk.tiles[by][bx] = tile;
					}
				return newcnk;
			}).ToArray().CopyTo(dstChunk.chunkList, 0);
			dstChunk.Write(Path.Combine(dstFol, "128x128Tiles.bin"));
			IOrderedEnumerable<RSDKv5.SceneEntity> sceneEntities = srcScene.objects.SelectMany(a => a.entities).OrderBy(a => a.slotID);
			if (objMode == ObjectMode.ConvertAllToBlank)
			{
				dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Select(a => new RSDKv4.Scene.Entity(0, 0, a.xpos, a.ypos)));
			}
			else if (srcConf.loadGlobalObjects)
			{
				List<string> srcObjs = null;
				if (File.Exists(srcGCFile))
					srcObjs = new RSDKv5.GameConfig(srcGCFile).objects;
				List<RSDKv3_4.GameConfig.ObjectInfo> dstObjs = null;
				if (File.Exists(dstGCFile))
					dstObjs = new RSDKv4.GameConfig(dstGCFile).objects;
				switch (objMode)
				{
					case ObjectMode.MatchGlobalsAddStage:
						Dictionary<int, int> objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.FindIndex(a => a.name == srcObjs[i]);
							if (ind == -1)
							{
								dstConf.objects.Add(new RSDKv3_4.GameConfig.ObjectInfo() { name = srcObjs[i], script = srcObjs[i] + ".txt" });
								objmap.Add(i + 1, dstObjs.Count + dstConf.objects.Count);
							}
							else
							{
								dstConf.loadGlobalObjects = true;
								objmap.Add(i + 1, ind + 1);
							}
						}
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + dstConf.objects.Count + 1);
						dstConf.objects.AddRange(srcConf.objects.Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }));
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Select(a => new RSDKv4.Scene.Entity((byte)objmap[srcScene.objects.IndexOf(a.type)], 0, a.xpos, a.ypos)));
						break;
					case ObjectMode.MatchGlobalsDelete:
						objmap = new Dictionary<int, int>() { { 0, 0 } };
						for (int i = 0; i < srcObjs.Count; i++)
						{
							int ind = dstObjs.FindIndex(a => a.name == srcObjs[i]);
							if (ind == -1)
								objmap.Add(i + 1, -1);
							else
								objmap.Add(i + 1, ind + 1);
						}
						dstConf.loadGlobalObjects = objmap.Values.Any(a => a != -1);
						for (int i = 0; i < srcConf.objects.Count; i++)
							objmap.Add(i + srcObjs.Count + 1, i + dstObjs.Count + 1);
						dstConf.objects = srcConf.objects.Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }).ToList();
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Where(a => objmap[srcScene.objects.IndexOf(a.type)] != -1).Select(a => new RSDKv4.Scene.Entity((byte)objmap[srcScene.objects.IndexOf(a.type)], 0, a.xpos, a.ypos)));
						break;
					case ObjectMode.DeleteGlobal:
						dstConf.objects = srcConf.objects.Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }).ToList();
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Where(a => srcScene.objects.IndexOf(a.type) > srcObjs.Count).Select(a => new RSDKv4.Scene.Entity((byte)(srcScene.objects.IndexOf(a.type) - srcObjs.Count), 0, a.xpos, a.ypos)));
						break;
					case ObjectMode.AddGlobalStage:
						dstConf.objects = srcObjs.Concat(srcConf.objects).Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }).ToList();
						dstConf.objects = new List<RSDKv3_4.GameConfig.ObjectInfo>();
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Select(a => new RSDKv4.Scene.Entity((byte)srcScene.objects.IndexOf(a.type), 0, a.xpos, a.ypos)));
						break;
					case ObjectMode.AsIs:
						dstConf.loadGlobalObjects = true;
						dstConf.objects = srcConf.objects.Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }).ToList();
						dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Select(a => new RSDKv4.Scene.Entity((byte)srcScene.objects.IndexOf(a.type), 0, a.xpos, a.ypos)));
						break;
				}
			}
			else if (objMode != ObjectMode.DontInclude)
			{
				dstConf.objects = srcConf.objects.Select(a => new RSDKv3_4.GameConfig.ObjectInfo() { name = a, script = a + ".txt" }).ToList();
				dstScene.entities = new List<RSDKv3_4.Scene.Entity>(sceneEntities.Select(a => new RSDKv4.Scene.Entity((byte)srcScene.objects.IndexOf(a.type), 0, a.xpos, a.ypos)));
			}
			dstConf.Write(Path.Combine(dstFol, "StageConfig.bin"));
			dstScene.Write(dstFile);
			dstBG.Write(Path.Combine(dstFol, "Backgrounds.bin"));
		}
	}

	enum ObjectMode
	{
		DontInclude,
		ConvertAllToBlank,
		MatchGlobalsAddStage,
		MatchGlobalsDelete,
		DeleteGlobal,
		AddGlobalStage,
		AsIs
	}
}
