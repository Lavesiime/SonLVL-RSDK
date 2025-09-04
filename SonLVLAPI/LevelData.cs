using RSDKv3_4;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bitmap = System.Drawing.Bitmap;

namespace SonicRetro.SonLVL.API
{
	public static class LevelData
	{
		public static GameInfo Game;
		public static string GamePath;
		public static IDataPack DataFile;
		public static string EXEFolder;
		public static string ModFolder;
		public static GameConfig GameConfig;
		public static GameXML GameXML;
		public static string GameTitle;
		public static GameConfig.StageList.StageInfo StageInfo;
		public static StageConfig StageConfig;
		public static Color[] NewPalette = new Color[256];
		public static BitmapBits[] NewTiles;
		public static Tiles128x128 NewChunks;
		public static TileConfig Collision;
		public static Backgrounds Background;
		public static Scene Scene;
		public static bool ForegroundDeformation;
		public static List<ObjectEntry> Objects;
		public static List<ScrollData>[] BGScroll = new List<ScrollData>[8];
		public static List<GameConfig.StageList.StageInfo>[] StageLists = new List<GameConfig.StageList.StageInfo>[4];
		public static List<GameConfig.ObjectInfo> GlobalObjects;
		public static List<AdditionalScene> AdditionalScenes;
		public static Bitmap[] NewTileBmps;
		public static BitmapBits[][] NewColBmpBits;
		public static Bitmap[][] NewColBmps;
		public static Sprite[] ChunkSprites;
		public static Bitmap[][] ChunkBmps;
		public static Bitmap[] CompChunkBmps;
		public static ColorPalette BmpPal;
		public static Dictionary<string, ObjectData> INIObjDefs;
		public static List<ObjectDefinition> ObjTypes;
		public static ObjectDefinition unkobj;
		private static string dllcache;
		private static Dictionary<string, BitmapBits> spriteSheets;
		public static BitmapBits[][] ChunkColBmpBits;
		public static Bitmap[][] ChunkColBmps;
		public static Sprite[][] ChunkColSprites;
		public static Bitmap UnknownImg;
		public static Sprite UnknownSprite;
		public delegate void LogEventHandler(params string[] message);
		public static event LogEventHandler LogEvent = delegate { };
		public static event Action PaletteChangedEvent = delegate { };
		internal static readonly bool IsMonoRuntime = Type.GetType("Mono.Runtime") != null;
		internal static readonly bool IsWindows = !(Environment.OSVersion.Platform == PlatformID.MacOSX | Environment.OSVersion.Platform == PlatformID.Unix | Environment.OSVersion.Platform == PlatformID.Xbox);
		private static readonly BitmapBits InvalidTile = new BitmapBits(16, 16);
		public static readonly Color[] GrayscalePalette = new Color[256];
		public const int ColorTransparent = 0;
		public const int ColorWhite = 1;
		public const int ColorYellow = 2;
		public const int ColorBlack = 3;
		public const int ColorRed = 4;
		static readonly List<string> rsdk_files_list = new List<string>();

		static LevelData()
		{
			InvalidTile.DrawLine(15, 0, 0, 15, 0);
			InvalidTile.DrawLine(15, 0, 0, 0, 15);
			InvalidTile.DrawLine(15, 15, 15, 0, 15);
			InvalidTile.DrawLine(15, 15, 15, 15, 0);
			InvalidTile.DrawLine(15, 0, 0, 15, 15);
			InvalidTile.DrawLine(15, 0, 15, 15, 0);
			for (int i = 0; i < 256; i++)
				GrayscalePalette[i] = Color.FromArgb(i, i, i);
			using (var sr = new StringReader(Properties.Resources.rsdk_files_list))
				while (true)
				{
					var s = sr.ReadLine();
					if (s == null)
						break;
					rsdk_files_list.Add(s);
				}
		}

		public static void LoadGame(string filename)
		{
			Log($"Opening game \"{filename}\"...");
			Game = GameInfo.Load(filename);

			// For files where RSDKVer is missing entirely, it's likely not a valid Project File in the first place
			// (the user might've just opened their settings.ini on accident instead or something)
			if (Game.RSDKVer == EngineVersion.Invalid)
				throw new FormatException("Game Version not found, is this a valid game ini?");

			GamePath = Path.GetFullPath(filename);
			Game = IniSerializer.Deserialize<GameInfo>(filename);
			Directory.SetCurrentDirectory(Path.GetDirectoryName(GamePath));
			EXEFolder = Path.GetDirectoryName(Path.GetFullPath(Game.EXEFile));
			UnknownImg = Properties.Resources.UnknownImg.Copy();
			UnknownSprite = new Sprite(new BitmapBits(UnknownImg), true, -8, -7);
			Log("Game type is " + Game.RSDKVer + ".");
			string dataFile = Path.Combine(EXEFolder, Game.DataFile);
			if (File.Exists(dataFile))
			{
				if (Game.IsV5U)
				{
					Log($"Using RSDKv5u DataFile {dataFile}...");
					DataFile = new RSDKv5.DataPack(dataFile, rsdk_files_list);
				}
				else
					switch (Game.RSDKVer)
					{
						case EngineVersion.V4:
							Log($"Using RSDKv4 DataFile {dataFile}...");
							DataFile = new RSDKv4.DataPack(dataFile, rsdk_files_list);
							break;
						case EngineVersion.V3:
							Log($"Using RSDKv3 DataFile {dataFile}...");
							DataFile = new RSDKv3.DataPack(dataFile);
							break;
					}
			}
			else
			{
				Log($"No DataFile found at {dataFile}, assuming Data Folder mode in folder {EXEFolder}...");
				DataFile = new DataFolder(EXEFolder);
			}

			ModFolder = null;
			switch (Game.RSDKVer)
			{
				case EngineVersion.V4:
					GameConfig = ReadFile<RSDKv4.GameConfig>("Data/Game/GameConfig.bin");
					break;
				case EngineVersion.V3:
					GameConfig = ReadFile<RSDKv3.GameConfig>("Data/Game/GameConfig.bin");
					break;
			}
		}

		public static void LoadMod(string path)
		{
			ModFolder = path;
			switch (Game.RSDKVer)
			{
				case EngineVersion.V4:
					GameConfig = ReadFile<RSDKv4.GameConfig>("Data/Game/GameConfig.bin");
					var pal = ((RSDKv4.GameConfig)GameConfig).masterPalette;
					for (int l = 0; l < pal.colors.Length; l++)
						for (int c = 0; c < pal.colors[l].Length; c++)
							if ((l * pal.colors[l].Length) + c < 256)
								NewPalette[(l * pal.colors[l].Length) + c] = pal.colors[l][c].ToSystemColor();
					break;
				case EngineVersion.V3:
					GameConfig = ReadFile<RSDKv3.GameConfig>("Data/Game/GameConfig.bin");
					var mpal = ReadFileRaw("Data/Palettes/MasterPalette.act");
					for (int i = 0; i < Math.Min(mpal.Length / 3, 256); i++)
						NewPalette[i] = Color.FromArgb(mpal[i * 3], mpal[i * 3 + 1], mpal[i * 3 + 2]);
					break;
			}
			for (int i = 0; i < 4; i++)
				StageLists[i] = new List<GameConfig.StageList.StageInfo>(GameConfig.stageLists[i].list);
			GlobalObjects = new List<GameConfig.ObjectInfo>(GameConfig.objects);
			GameTitle = GameConfig.gameTitle;
			GameXML = null;
			if (ModFolder != null)
			{
				string xmlpath = Path.Combine(ModFolder, "Data/Game/game.xml");
				if (File.Exists(xmlpath))
				{
					GameXML = GameXML.Load(xmlpath);
					foreach (var col in GameXML.palette.colors.Where(a => a.bank == 0))
						NewPalette[col.index] = (Color)col;
					StageLists[0].AddRange(GameXML.presentationStages.Select(a => (GameConfig.StageList.StageInfo)a));
					StageLists[1].AddRange(GameXML.regularStages.Select(a => (GameConfig.StageList.StageInfo)a));
					StageLists[2].AddRange(GameXML.specialStages.Select(a => (GameConfig.StageList.StageInfo)a));
					StageLists[3].AddRange(GameXML.bonusStages.Select(a => (GameConfig.StageList.StageInfo)a));
					GlobalObjects.AddRange(GameXML.objects.Where(a => !a.forceLoad).Select(a => (GameConfig.ObjectInfo)a));

					if (GameXML.title.name != null)
						GameTitle = GameXML.title.name;
				}
			}
		}

		public static T ReadFile<T>(string filename)
			where T : new()
		{
			if (ModFolder != null)
			{
				string modpath = Path.Combine(ModFolder, filename);
				if (File.Exists(modpath))
					return (T)Activator.CreateInstance(typeof(T), modpath);
			}
			if (DataFile != null && DataFile.TryGetFileData(filename, out byte[] data))
				using (MemoryStream ms = new MemoryStream(data))
					return (T)Activator.CreateInstance(typeof(T), ms);
			if (File.Exists(filename))
				return (T)Activator.CreateInstance(typeof(T), filename);
			return new T();
		}

		public static T ReadFileNoMod<T>(string filename)
			where T : new()
		{
			if (DataFile != null && DataFile.TryGetFileData(filename, out byte[] data))
				using (MemoryStream ms = new MemoryStream(data))
					return (T)Activator.CreateInstance(typeof(T), ms);
			if (File.Exists(filename))
				return (T)Activator.CreateInstance(typeof(T), filename);
			return new T();
		}

		public static byte[] ReadFileRaw(string filename)
		{
			if (ModFolder != null)
			{
				string modpath = Path.Combine(ModFolder, filename);
				if (File.Exists(modpath))
					return File.ReadAllBytes(modpath);
			}
			if (DataFile != null && DataFile.TryGetFileData(filename, out byte[] data))
				return data;
			if (File.Exists(filename))
				return File.ReadAllBytes(filename);
			return null;
		}

		public static byte[] ReadFileRawNoMod(string filename)
		{
			if (DataFile != null && DataFile.TryGetFileData(filename, out byte[] data))
				return data;
			if (File.Exists(filename))
				return File.ReadAllBytes(filename);
			return null;
		}

		public static void LoadLevel(GameConfig.StageList.StageInfo stage)
		{
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();
			Log("Loading level " + stage.name + "...");
			StageInfo = stage;
			string stgfol = $"Data/Stages/{stage.folder}/";
			switch (Game.RSDKVer)
			{
				case EngineVersion.V4:
					StageConfig = ReadFile<RSDKv4.StageConfig>(stgfol + "StageConfig.bin");
					break;
				case EngineVersion.V3:
					StageConfig = ReadFile<RSDKv3.StageConfig>(stgfol + "StageConfig.bin");
					break;
			}
			for (int l = 0; l < StageConfig.stagePalette.colors.Length; l++)
				for (int c = 0; c < StageConfig.stagePalette.colors[l].Length; c++)
					NewPalette[(l * 16) + c + 96] = StageConfig.stagePalette.colors[l][c].ToSystemColor();
			Gif tilebmp = ReadFile<Gif>(stgfol + "16x16Tiles.gif");
			if (tilebmp.width >= 16 && tilebmp.height >= 16)
			{
				NewTiles = new BitmapBits[tilebmp.height / 16];
				for (int i = 0; i < tilebmp.height / 16; i++)
				{
					NewTiles[i] = new BitmapBits(16, 16);
					Array.Copy(tilebmp.pixels, i * 256, NewTiles[i].Bits, 0, 256);
				}
				for (int i = 128; i < 256; i++)
					NewPalette[i] = tilebmp.palette[i].ToSystemColor();
			}
			else
			{
				NewTiles = new BitmapBits[0x400];
				for (int i = 0; i < 0x400; i++)
					NewTiles[i] = new BitmapBits(16, 16);

				NewPalette.Fill(NewPalette[0], 128, 128);
			}
			NewChunks = ReadFile<Tiles128x128>(stgfol + "128x128Tiles.bin");
			Collision = ReadFile<TileConfig>(stgfol + "CollisionMasks.bin");
			AdditionalScenes = new List<AdditionalScene>();
			switch (Game.RSDKVer)
			{
				case EngineVersion.V4:
					Background = ReadFile<RSDKv4.Backgrounds>(stgfol + "Backgrounds.bin");
					Scene = ReadFile<RSDKv4.Scene>($"{stgfol}Act{stage.actID}.bin");
					foreach (var astg in StageLists.SelectMany(a => a).Where(a => a != stage && a.folder.Equals(stage.folder, StringComparison.OrdinalIgnoreCase) && !a.actID.Equals(stage.actID, StringComparison.OrdinalIgnoreCase)))
						AdditionalScenes.Add(new AdditionalScene(astg, ReadFile<RSDKv4.Scene>($"{stgfol}Act{astg.actID}.bin")));
					break;
				case EngineVersion.V3:
					Background = ReadFile<RSDKv3.Backgrounds>(stgfol + "Backgrounds.bin");
					Scene = ReadFile<RSDKv3.Scene>($"{stgfol}Act{stage.actID}.bin");
					foreach (var astg in StageLists.SelectMany(a => a).Where(a => a != stage && a.folder.Equals(stage.folder, StringComparison.OrdinalIgnoreCase) && !a.actID.Equals(stage.actID, StringComparison.OrdinalIgnoreCase)))
						AdditionalScenes.Add(new AdditionalScene(astg, ReadFile<RSDKv3.Scene>($"{stgfol}Act{astg.actID}.bin")));
					break;
			}
			Objects = new List<ObjectEntry>(Scene.entities.Count);
			foreach (var item in Scene.entities)
				Objects.Add(ObjectEntry.Create(item));
			ForegroundDeformation = (Background.hScroll.Count > 0) ? Background.hScroll[0].deform : false;
			for (int i = 0; i < 8; i++)
			{
				BGScroll[i] = new List<ScrollData>();
				List<Backgrounds.ScrollInfo> info;
				switch (Background.layers[i].type)
				{
					case Backgrounds.Layer.LayerTypes.HScroll:
						info = Background.hScroll;
						break;
					case Backgrounds.Layer.LayerTypes.VScroll:
						info = Background.vScroll;
						break;
					default:
						continue;
				}
				if (info.Count > 0)
				{
					int lastind = -1;
					for (ushort y = 0; y < Background.layers[i].lineScroll.Length; y++)
						if (Background.layers[i].lineScroll[y] != lastind)
						{
							lastind = Background.layers[i].lineScroll[y];
							BGScroll[i].Add(new ScrollData(y, info[(lastind < info.Count) ? lastind : info.Count - 1]));
						}
				}
				else
					BGScroll[i].Add(new ScrollData());
				
				Background.layers[i].lineScroll = new byte[0];
			}
			
			Background.hScroll.Clear();
			Background.vScroll.Clear();
			
			using (Bitmap palbmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
				BmpPal = palbmp.Palette;
			NewPalette.CopyTo(BmpPal.Entries, 0);
			BmpPal.Entries[ColorTransparent] = Color.Transparent;
			UnknownImg.Palette = BmpPal;
			unkobj = new DefaultObjectDefinition();
			INIObjDefs = new Dictionary<string, ObjectData>();
			spriteSheets = new Dictionary<string, BitmapBits>();
			if (Directory.Exists("SonLVLObjDefs"))
				foreach (string file in Directory.EnumerateFiles("SonLVLObjDefs", "*.ini"))
					LoadObjectDefinitionFile(file, false);
			if (ModFolder != null && Directory.Exists(Path.Combine(ModFolder, "SonLVLObjDefs")))
			{
				foreach (string file in Directory.EnumerateFiles(Path.Combine(ModFolder, "SonLVLObjDefs"), "*.ini"))
					LoadObjectDefinitionFile(file, true);
				dllcache = Path.Combine(ModFolder, "SonLVLObjDefs", "dllcache");
			}
			else
				dllcache = Path.Combine("SonLVLObjDefs", "dllcache");
			unkobj.Init(new ObjectData());
			if (INIObjDefs.Count > 0 && !Directory.Exists(dllcache))
			{
				DirectoryInfo dir = Directory.CreateDirectory(dllcache);
				dir.Attributes |= FileAttributes.Hidden;
			}
			InitObjectDefinitions();
			foreach (ObjectEntry obj in Objects)
				obj.UpdateSprite();
			Log("Drawing tile bitmaps...");
			NewTileBmps = new Bitmap[NewTiles.Length];
			for (int bi = 0; bi < NewTiles.Length; bi++)
				RedrawBlock(bi, false);
			Log("Drawing collision bitmaps...");
			NewColBmpBits = new BitmapBits[Collision.collisionMasks[0].Length][];
			NewColBmps = new Bitmap[Collision.collisionMasks[0].Length][];
			for (int i = 0; i < Collision.collisionMasks[0].Length; i++)
			{
				NewColBmpBits[i] = new BitmapBits[2];
				NewColBmps[i] = new Bitmap[2];
				RedrawCol(i, false);
			}
			Log("Drawing chunk bitmaps...");
			ChunkSprites = new Sprite[NewChunks.chunkList.Length];
			ChunkBmps = new Bitmap[NewChunks.chunkList.Length][];
			CompChunkBmps = new Bitmap[NewChunks.chunkList.Length];
			ChunkColBmpBits = new BitmapBits[NewChunks.chunkList.Length][];
			ChunkColBmps = new Bitmap[NewChunks.chunkList.Length][];
			ChunkColSprites = new Sprite[NewChunks.chunkList.Length][];
			for (int i = 0; i < NewChunks.chunkList.Length; i++)
			{
				ChunkBmps[i] = new Bitmap[2];
				ChunkColBmpBits[i] = new BitmapBits[2];
				ChunkColBmps[i] = new Bitmap[2];
				ChunkColSprites[i] = new Sprite[2];
				RedrawChunk(i);
			}
			stopwatch.Stop();
			Log($"Level loaded in {stopwatch.Elapsed.TotalSeconds} second(s).");
		}

		public static void ReloadTiles()
		{
			Gif tilebmp = ReadFile<Gif>($"Data/Stages/{StageInfo.folder}/16x16Tiles.gif");
			if (tilebmp.width >= 16 && tilebmp.height >= 16)
			{
				NewTiles = new BitmapBits[tilebmp.height / 16];
				for (int i = 0; i < tilebmp.height / 16; i++)
				{
					NewTiles[i] = new BitmapBits(16, 16);
					Array.Copy(tilebmp.pixels, i * 256, NewTiles[i].Bits, 0, 256);
				}
				for (int i = 128; i < 256; i++)
					NewPalette[i] = tilebmp.palette[i].ToSystemColor();
			}
			else
			{
				NewTiles = new BitmapBits[0x400];
				for (int i = 0; i < 0x400; i++)
					NewTiles[i] = new BitmapBits(16, 16);

				NewPalette.Fill(NewPalette[0], 128, 128);
			}
		}

		public static void ClearLevel()
		{
			Log("Clearing level...");
			switch (Game.RSDKVer)
			{
				case EngineVersion.V4:
					StageConfig = new RSDKv4.StageConfig();
					break;
				case EngineVersion.V3:
					StageConfig = new RSDKv3.StageConfig();
					break;
			}
			for (int l = 0; l < StageConfig.stagePalette.colors.Length; l++)
				for (int c = 0; c < StageConfig.stagePalette.colors[l].Length; c++)
					NewPalette[(l * 16) + c + 96] = StageConfig.stagePalette.colors[l][c].ToSystemColor();
			foreach (var item in NewTiles)
				item.Clear();
			NewPalette.Fill(NewPalette[0], 128, 128);
			NewChunks = new Tiles128x128();
			Collision = new TileConfig();
			switch (Game.RSDKVer)
			{
				case EngineVersion.V4:
					Background = new RSDKv4.Backgrounds();
					Scene = new RSDKv4.Scene();
					foreach (var astg in AdditionalScenes)
						astg.Scene = new RSDKv4.Scene();
					break;
				case EngineVersion.V3:
					Background = new RSDKv3.Backgrounds();
					Scene = new RSDKv3.Scene();
					foreach (var astg in AdditionalScenes)
						astg.Scene = new RSDKv3.Scene();
					break;
			}
			Objects = new List<ObjectEntry>();
			for (int i = 0; i < 8; i++)
				BGScroll[i] = new List<ScrollData>();
			NewPalette.CopyTo(BmpPal.Entries, 0);
			BmpPal.Entries[ColorTransparent] = Color.Transparent;
			UnknownImg.Palette = BmpPal;
			InitObjectDefinitions();
			NewTileBmps = new Bitmap[NewTiles.Length];
			for (int bi = 0; bi < NewTiles.Length; bi++)
				RedrawBlock(bi, false);
			NewColBmpBits = new BitmapBits[Collision.collisionMasks[0].Length][];
			NewColBmps = new Bitmap[Collision.collisionMasks[0].Length][];
			for (int i = 0; i < Collision.collisionMasks[0].Length; i++)
			{
				NewColBmpBits[i] = new BitmapBits[2];
				NewColBmps[i] = new Bitmap[2];
				RedrawCol(i, false);
			}
			ChunkSprites = new Sprite[NewChunks.chunkList.Length];
			ChunkBmps = new Bitmap[NewChunks.chunkList.Length][];
			CompChunkBmps = new Bitmap[NewChunks.chunkList.Length];
			ChunkColBmpBits = new BitmapBits[NewChunks.chunkList.Length][];
			ChunkColBmps = new Bitmap[NewChunks.chunkList.Length][];
			ChunkColSprites = new Sprite[NewChunks.chunkList.Length][];
			for (int i = 0; i < NewChunks.chunkList.Length; i++)
			{
				ChunkBmps[i] = new Bitmap[2];
				ChunkColBmpBits[i] = new BitmapBits[2];
				ChunkColBmps[i] = new Bitmap[2];
				ChunkColSprites[i] = new Sprite[2];
				RedrawChunk(i);
			}
		}

		public static void SaveLevel()
		{
			Log($"Saving {StageInfo.name}...");
			if (GameXML != null)
			{
				GameXML.palette.colors.RemoveAll(a => a.bank == 0 && a.index < 96);
				switch (Game.RSDKVer)
				{
					case EngineVersion.V3:
						{
							byte[] origpal = ReadFileRaw("Data/Palettes/MasterPalette.act");
							int pallen = Math.Min(origpal.Length / 3, 96);
							int v = 0;
							for (int i = 0; i < pallen; i++)
							{
								if (origpal[v] != NewPalette[i].R || origpal[v + 1] != NewPalette[i].G || origpal[v + 2] != NewPalette[i].B)
									GameXML.palette.colors.Add(new ColorXML(0, (byte)i, NewPalette[i]));
								v += 3;
							}
						}
						break;
					case EngineVersion.V4:
						{
							var origpal = ((RSDKv4.GameConfig)GameConfig).masterPalette;
							int i = 0;
							for (int l = 0; l < origpal.colors.Length; l++)
								for (int c = 0; c < origpal.colors[l].Length; c++)
								{
									if (i == 96) break;
									if (origpal.colors[l][c].r != NewPalette[i].R || origpal.colors[l][c].g != NewPalette[i].G || origpal.colors[l][c].b != NewPalette[i].B)
										GameXML.palette.colors.Add(new ColorXML(0, (byte)i, NewPalette[i]));
									++i;
								}
						}
						break;
				}
				GameXML.palette.colors.Sort((a, b) =>
				{
					int result = a.bank.CompareTo(b.bank);
					if (result == 0)
						result = a.index.CompareTo(b.index);
					return result;
				});
				GameXML.Save(Path.Combine(ModFolder, "Data/Game/game.xml"));
			}
			else
			{
				switch (Game.RSDKVer)
				{
					case EngineVersion.V3:
						{
							byte[] pal = ReadFileRaw("Data/Palettes/MasterPalette.act");
							int pallen = Math.Min(pal.Length / 3, 96);
							int v = 0;
							bool edit = false;
							for (int i = 0; i < pallen; i++)
							{
								if (pal[v] != NewPalette[i].R)
								{
									pal[v] = NewPalette[i].R;
									edit = true;
								}
								if (pal[v + 1] != NewPalette[i].G)
								{
									pal[v + 1] = NewPalette[i].G;
									edit = true;
								}
								if (pal[v + 2] != NewPalette[i].B)
								{
									pal[v + 2] = NewPalette[i].B;
									edit = true;
								}
								v += 3;
							}
							if (edit)
							{
								Directory.CreateDirectory(Path.Combine(ModFolder, "Data/Palettes"));
								File.WriteAllBytes(Path.Combine(ModFolder, "Data/Palettes/MasterPalette.act"), pal);
							}
						}
						break;
					case EngineVersion.V4:
						{
							var pal = ((RSDKv4.GameConfig)GameConfig).masterPalette;
							int i = 0;
							bool edit = false;
							for (int l = 0; l < pal.colors.Length; l++)
								for (int c = 0; c < pal.colors[l].Length; c++)
								{
									if (i == 96) break;
									if (pal.colors[l][c].r != NewPalette[i].R || pal.colors[l][c].g != NewPalette[i].G || pal.colors[l][c].b != NewPalette[i].B)
									{
										pal.colors[l][c] = new Palette.Color(NewPalette[i].R, NewPalette[i].G, NewPalette[i].B);
										edit = true;
									}
									++i;
								}
							if (edit)
							{
								Directory.CreateDirectory(Path.Combine(ModFolder, "Data/Game"));
								GameConfig.Write(Path.Combine(ModFolder, "Data/Game/GameConfig.bin"));
							}
						}
						break;
				}
			}

			// Let's make sure the stage folder exists within the mod, before anything else
			Directory.CreateDirectory(Path.Combine(ModFolder, "Data/Stages", StageInfo.folder));
			
			// First, let's save the StageConfig
			for (int i = 0; i < 32; i++)
				StageConfig.stagePalette.colors[i / StageConfig.stagePalette.colors[0].Length][i % StageConfig.stagePalette.colors[0].Length] = new Palette.Color(NewPalette[i + 96].R, NewPalette[i + 96].G, NewPalette[i + 96].B);
			SaveFile("StageConfig.bin", fn => StageConfig.Write(fn));

			// Next, let's do the stage's 16x16Tiles.gif
			// The Bitmap.Save implementation in Mono (in the context of running SonLVL-RSDK on Linux) is very prone to causing issues when saving the stage tiles gif
			// (Namely, instead of saving the gif with a global palette, it instead assigns the palette to the "first frame" in the gif instead, which RSDK doesn't like..)
			// Because of that, we wanna use the existing RSDKv3_4.Gif writer instead, if necessary

			// This is noticably slower than using the standard Bitmap class, which is why we only wanna resort to it when we have to..
			if (IsMonoRuntime)
			{
				Gif tilebmp = new Gif
				{
					width = 16,
					height = (ushort)(NewTiles.Length * 16)
				};

				// okay so tbh i think FromSystemColor is supposed to be static, but it isn't, so..
				// pardon the odd synatx, sorry!--
				tilebmp.palette[0] = tilebmp.palette[0].FromSystemColor(NewPalette[0]);

				// (note that Palette.Color is a class and not a struct, but since we immediatly discard this gif it's okay to do this--)
				tilebmp.palette.Fill(tilebmp.palette[0], 1, 95);

				for (int i = 96; i < 256; i++)
					tilebmp.palette[i] = tilebmp.palette[i].FromSystemColor(NewPalette[i]);

				for (int i = 0; i < NewTiles.Length; i++)
					Array.Copy(NewTiles[i].Bits, 0, tilebmp.pixels, i * 256, 256);

				SaveFile("16x16Tiles.gif", fn => tilebmp.Write(fn));
			}
			else
			{
				// We're just on Windows, so it's okay to use the standard Bitmap class to save
				// Compared to the RSDKv3_4.Gif method, this is much faster, so we choose to stick with this when possible

				BitmapBits tiles = new BitmapBits(16, NewTiles.Length * 16);
				for (int i = 0; i < NewTiles.Length; i++)
					tiles.DrawBitmap(NewTiles[i], 0, i * 16);

				SaveFile("16x16Tiles.gif", fn =>
				{
					Color[] palette = (Color[])NewPalette.Clone();
					palette.Fill(NewPalette[0], 1, 95);
					using (Bitmap bmp = tiles.ToBitmap(palette))
						bmp.Save(fn, ImageFormat.Gif);
				});
			}

			SaveFile("128x128Tiles.bin", fn => NewChunks.Write(fn));
			SaveFile("CollisionMasks.bin", fn => Collision.Write(fn));

			// Now, let's save the Backgrounds.bin file
			// The tilelayer part of it is simple enough, but for scrolling, we need to reconstruct the entire list
			// (this note fits in both LoadLevel as well as here, but) This can cause some issues with scripts that directly access parallax indexes
			// Namely:
			// - Because the list gets reconstructed, indexes may be shuffled around
			// - This doesn't only matter when creating new zones, but even just opening an S1 Special Stage, making no changes, and saving it is enough to break the background effects
			// - However.. I'm not sure what a good solution would be? In the aforementioned example, there are several duplicate hParallax
			//    entries, so even if they're different in the script's eyes.. *we* don't know that, nor do we have any way to find out..
			// - Another notable example is HTZ, where the Earthquakes also break when opening the level and resaving it in SonLVL-RSDK with no extra changes
			// - The "ForegroundDeformation" somewhat alleviates this problem in a roundabout way, where if an HScroll layer has 1.0 parallaxFactor, 0.0 scrollSpeed, and matches ForegroundDeformation, then
			//    It will use hParallax[0] all the time, no matter what other parallax there is
			// - However.. that's still a different index than the script expects, which is hParallax[20]...
			// - I'm.. not quite sure what the best solution to this problem is? Should we just try and preserve the original indexes all the time where possible? But.. how do we handle editing, then?
			// So... just gonna leave this note here for future me to come back to, I suppose..
			// TODO: fix it ig idk tbh

			// Should already be cleared, but let's go ahead and clear 'em again anyways just in case
			Background.hScroll.Clear();
			Background.vScroll.Clear();

			// First off, let's add our ForegroundDeformation parallax index, since the FG uses parallax index 0
			// (We add it even when it's false, since there are cases where FG doesn't deform while BG[0] does, or vice versa)
			switch (Game.RSDKVer)
			{
				case EngineVersion.V3:
					Background.hScroll.Add(new RSDKv3.Backgrounds.ScrollInfo() { deform = ForegroundDeformation } );
					break;
				case EngineVersion.V4:
					Background.hScroll.Add(new RSDKv4.Backgrounds.ScrollInfo() { deform = ForegroundDeformation } );
					break;
			}

			// Now, let's go through the 8 layers and rebuild that scrolling list!-
			for (int i = 0; i < 8; i++)
			{
				int height;
				List<Backgrounds.ScrollInfo> scrlist;
				switch (Background.layers[i].type)
				{
					case Backgrounds.Layer.LayerTypes.HScroll:
						height = Background.layers[i].height * 128;
						scrlist = Background.hScroll;
						break;
					case Backgrounds.Layer.LayerTypes.VScroll:
						height = Background.layers[i].width * 128;
						scrlist = Background.vScroll;
						break;
					default:
						continue;
				}

				Background.layers[i].lineScroll = new byte[height];
				byte scrind = 0;
				int datind = 0;
				for (int y = 0; y < height; y++)
				{
					if (datind < BGScroll[i].Count && BGScroll[i][datind].StartPos == y)
					{
						Backgrounds.ScrollInfo si = null;
						switch (Game.RSDKVer)
						{
							case EngineVersion.V3:
								si = BGScroll[i][datind++].GetInfoV3();
								break;
							case EngineVersion.V4:
								si = BGScroll[i][datind++].GetInfoV4();
								break;
						}
						
						// Now, let's match that ScrollData and see if we can find a matching entry in the hScroll/vScroll arrays
						// Normally, we'd like to merge all identical entries in the arrays..
						int tmpind = scrlist.FindIndex(a => si.Equal(a));

						// ..However, if this scroll line is identical to the very previous one, then..
						if (tmpind == scrind)
						{
							// Instead of merging the two together, let's keep 'em separate
							// (So that it doesn't appear as if the new one just disappears, also relevant for when scroll indexes are controlled by script)
							// (Not a *complete* fix for the above TODO, but it still helps at least a little!..)
							tmpind = -1;
						}

						if (tmpind == -1)
						{
							// Either we don't have an existing matching scroll entry, or we're intentionally trying to keep ourselves separate
							// Whatever the case is, let's go ahead and make a new entry!
							tmpind = scrlist.Count;
							scrlist.Add(si);
						}

						scrind = (byte)tmpind;
					}

					Background.layers[i].lineScroll[y] = scrind;
				}
			}

			SaveFile("Backgrounds.bin", fn => Background.Write(fn));
			
			SaveFile($"Act{StageInfo.actID}.bin", fn => Scene.Write(fn));
			foreach (var astg in AdditionalScenes)
				SaveFile($"Act{astg.StageInfo.actID}.bin", fn => astg.Scene.Write(fn));
		}

		private static void SaveFile(string name, Action<string> action)
		{
			string fullpath = Path.Combine(ModFolder, "Data/Stages", StageInfo.folder, name);
			bool isnew = !File.Exists(fullpath);
			action(fullpath);

			if (isnew)
			{
				var baseFile = ReadFileRawNoMod($"Data/Stages/{StageInfo.folder}/{name}");
				if (baseFile != null && baseFile.FastArrayEqual(File.ReadAllBytes(fullpath)))
					File.Delete(fullpath);
			}
		}

		public static BitmapBits DrawForeground(Rectangle? section, bool includeObjects, bool includeDebugObjects, bool objectsAboveHighPlane, bool lowPlane, bool highPlane, bool collisionPath1, bool collisionPath2)
		{
			Rectangle bounds;
			if (section.HasValue)
				bounds = section.Value;
			else
				bounds = new Rectangle(0, 0, FGWidth * 128, FGHeight * 128);
			BitmapBits LevelImg8bpp = new BitmapBits(bounds.Size);
			int cl = Math.Max(bounds.X / 128, 0);
			int ct = Math.Max(bounds.Y / 128, 0);
			int cr = Math.Min((bounds.Right - 1) / 128, FGWidth - 1);
			int cb = Math.Min((bounds.Bottom - 1) / 128, FGHeight - 1);
			for (int y = ct; y <= cb; y++)
				for (int x = cl; x <= cr; x++)
					if (Scene.layout[y][x] < NewChunks.chunkList.Length)
					{
						if ((!includeObjects || objectsAboveHighPlane) && lowPlane && highPlane)
						{
							LevelImg8bpp.DrawSprite(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
							if (collisionPath1)
								LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][0], x * 128 - bounds.X, y * 128 - bounds.Y);
							else if (collisionPath2)
								LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][1], x * 128 - bounds.X, y * 128 - bounds.Y);
						}
						else
						{
							if (lowPlane)
								LevelImg8bpp.DrawSpriteLow(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
							if (!includeObjects || objectsAboveHighPlane)
							{
								if (highPlane)
									LevelImg8bpp.DrawSpriteHigh(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
								if (collisionPath1)
									LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][0], x * 128 - bounds.X, y * 128 - bounds.Y);
								else if (collisionPath2)
									LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][1], x * 128 - bounds.X, y * 128 - bounds.Y);
							}
						}
					}
			if (includeObjects)
			{
				foreach (ObjectEntry item in Objects)
					if (!(!includeDebugObjects && GetObjectDefinition(item.Type).Debug))
						LevelImg8bpp.DrawSprite(item.Sprite, item.X - bounds.X, item.Y - bounds.Y);
				if (!objectsAboveHighPlane)
					for (int y = ct; y <= cb; y++)
						for (int x = cl; x <= cr; x++)
							if (Scene.layout[y][x] < NewChunks.chunkList.Length)
							{
								if (highPlane)
									LevelImg8bpp.DrawSpriteHigh(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
								if (collisionPath1)
									LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][0], x * 128 - bounds.X, y * 128 - bounds.Y);
								else if (collisionPath2)
									LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][1], x * 128 - bounds.X, y * 128 - bounds.Y);
							}
				if (includeDebugObjects)
					foreach (ObjectEntry item in Objects)
						if (item.DebugOverlay != null)
							LevelImg8bpp.DrawSprite(item.DebugOverlay, item.X - bounds.X, item.Y - bounds.Y);
			}
			return LevelImg8bpp;
		}

		public static BitmapBits32 DrawForeground32(Rectangle? section, Color backgroundColor, bool includeObjects, bool includeDebugObjects, bool objectsAboveHighPlane, bool lowPlane, bool highPlane, bool collisionPath1, bool collisionPath2)
		{
			Rectangle bounds;
			if (section.HasValue)
				bounds = section.Value;
			else
				bounds = new Rectangle(0, 0, FGWidth * 128, FGHeight * 128);
			BitmapBits32 LevelImg8bpp = new BitmapBits32(bounds.Size);
			NewPalette.CopyTo(LevelImg8bpp.Palette, 0);
			LevelImg8bpp.Clear(backgroundColor);
			objectsAboveHighPlane |= !includeObjects;
			int colpath = -1;
			if (collisionPath1)
				colpath = 0;
			else if (collisionPath2)
				colpath = 1;
			int cl = Math.Max(bounds.X / 128, 0);
			int ct = Math.Max(bounds.Y / 128, 0);
			int cr = Math.Min((bounds.Right - 1) / 128, FGWidth - 1);
			int cb = Math.Min((bounds.Bottom - 1) / 128, FGHeight - 1);
			for (int y = ct; y <= cb; y++)
				for (int x = cl; x <= cr; x++)
					if (Scene.layout[y][x] < NewChunks.chunkList.Length)
					{
						if (objectsAboveHighPlane && lowPlane && highPlane)
						{
							LevelImg8bpp.DrawSprite(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
							if (colpath != -1)
							{
								LevelImg8bpp.Palette[ColorWhite] = Color.White;
								LevelImg8bpp.Palette[ColorYellow] = Color.Yellow;
								LevelImg8bpp.Palette[ColorBlack] = Color.Black;
								LevelImg8bpp.Palette[ColorRed] = Color.Red;
								LevelImg8bpp.DrawSprite(ChunkColSprites[Scene.layout[y][x]][colpath], x * 128 - bounds.X, y * 128 - bounds.Y);
								Array.Copy(NewPalette, 1, LevelImg8bpp.Palette, 1, 4);
							}
						}
						else
						{
							if (lowPlane)
								LevelImg8bpp.DrawSpriteLow(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
							if (!includeObjects || objectsAboveHighPlane)
							{
								if (highPlane)
									LevelImg8bpp.DrawSpriteHigh(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
								if (colpath != -1)
								{
									LevelImg8bpp.Palette[ColorWhite] = Color.White;
									LevelImg8bpp.Palette[ColorYellow] = Color.Yellow;
									LevelImg8bpp.Palette[ColorBlack] = Color.Black;
									LevelImg8bpp.Palette[ColorRed] = Color.Red;
									LevelImg8bpp.DrawSprite(ChunkColSprites[Scene.layout[y][x]][colpath], x * 128 - bounds.X, y * 128 - bounds.Y);
									Array.Copy(NewPalette, 1, LevelImg8bpp.Palette, 1, 4);
								}
							}
						}
					}
			if (includeObjects)
			{
				foreach (ObjectEntry item in Objects)
					if (!(!includeDebugObjects && GetObjectDefinition(item.Type).Debug))
						LevelImg8bpp.DrawSprite(item.Sprite, item.X - bounds.X, item.Y - bounds.Y);
				if (!objectsAboveHighPlane)
					for (int y = ct; y <= cb; y++)
						for (int x = cl; x <= cr; x++)
							if (Scene.layout[y][x] < NewChunks.chunkList.Length)
							{
								if (highPlane)
									LevelImg8bpp.DrawSpriteHigh(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
								if (colpath != -1)
								{
									LevelImg8bpp.Palette[ColorWhite] = Color.White;
									LevelImg8bpp.Palette[ColorYellow] = Color.Yellow;
									LevelImg8bpp.Palette[ColorBlack] = Color.Black;
									LevelImg8bpp.Palette[ColorRed] = Color.Red;
									LevelImg8bpp.DrawSprite(ChunkColSprites[Scene.layout[y][x]][colpath], x * 128 - bounds.X, y * 128 - bounds.Y);
									Array.Copy(NewPalette, 1, LevelImg8bpp.Palette, 1, 4);
								}
							}
				if (includeDebugObjects)
					foreach (ObjectEntry item in Objects)
						if (item.DebugOverlay != null)
							LevelImg8bpp.DrawSprite(item.DebugOverlay, item.X - bounds.X, item.Y - bounds.Y);
			}
			return LevelImg8bpp;
		}

		public static BitmapBits DrawForegroundLayout(Rectangle? section)
		{
			Rectangle bounds;
			if (section.HasValue)
				bounds = section.Value;
			else
				bounds = new Rectangle(0, 0, FGWidth * 128, FGHeight * 128);
			BitmapBits LevelImg8bpp = new BitmapBits(bounds.Size);
			int cl = Math.Max(bounds.X / 128, 0);
			int ct = Math.Max(bounds.Y / 128, 0);
			int cr = Math.Min((bounds.Right - 1) / 128, FGWidth - 1);
			int cb = Math.Min((bounds.Bottom - 1) / 128, FGHeight - 1);
			for (int y = ct; y <= cb; y++)
				for (int x = cl; x <= cr; x++)
					if (Scene.layout[y][x] < NewChunks.chunkList.Length)
						LevelImg8bpp.DrawSprite(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
			return LevelImg8bpp;
		}

		public static BitmapBits DrawForegroundLayoutLow(Rectangle? section)
		{
			Rectangle bounds;
			if (section.HasValue)
				bounds = section.Value;
			else
				bounds = new Rectangle(0, 0, FGWidth * 128, FGHeight * 128);
			BitmapBits LevelImg8bpp = new BitmapBits(bounds.Size);
			int cl = Math.Max(bounds.X / 128, 0);
			int ct = Math.Max(bounds.Y / 128, 0);
			int cr = Math.Min((bounds.Right - 1) / 128, FGWidth - 1);
			int cb = Math.Min((bounds.Bottom - 1) / 128, FGHeight - 1);
			for (int y = ct; y <= cb; y++)
				for (int x = cl; x <= cr; x++)
					if (Scene.layout[y][x] < NewChunks.chunkList.Length)
						LevelImg8bpp.DrawSpriteLow(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
			return LevelImg8bpp;
		}

		public static BitmapBits DrawForegroundLayoutHigh(Rectangle? section)
		{
			Rectangle bounds;
			if (section.HasValue)
				bounds = section.Value;
			else
				bounds = new Rectangle(0, 0, FGWidth * 128, FGHeight * 128);
			BitmapBits LevelImg8bpp = new BitmapBits(bounds.Size);
			int cl = Math.Max(bounds.X / 128, 0);
			int ct = Math.Max(bounds.Y / 128, 0);
			int cr = Math.Min((bounds.Right - 1) / 128, FGWidth - 1);
			int cb = Math.Min((bounds.Bottom - 1) / 128, FGHeight - 1);
			for (int y = ct; y <= cb; y++)
				for (int x = cl; x <= cr; x++)
					if (Scene.layout[y][x] < NewChunks.chunkList.Length)
						LevelImg8bpp.DrawSpriteHigh(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
			return LevelImg8bpp;
		}

		public static BitmapBits DrawForegroundCollision(Rectangle? section, int colpath)
		{
			Rectangle bounds;
			if (section.HasValue)
				bounds = section.Value;
			else
				bounds = new Rectangle(0, 0, FGWidth * 128, FGHeight * 128);
			BitmapBits LevelImg8bpp = new BitmapBits(bounds.Size);
			int cl = Math.Max(bounds.X / 128, 0);
			int ct = Math.Max(bounds.Y / 128, 0);
			int cr = Math.Min((bounds.Right - 1) / 128, FGWidth - 1);
			int cb = Math.Min((bounds.Bottom - 1) / 128, FGHeight - 1);
			for (int y = ct; y <= cb; y++)
				for (int x = cl; x <= cr; x++)
					if (Scene.layout[y][x] < NewChunks.chunkList.Length)
						LevelImg8bpp.DrawSprite(ChunkColSprites[Scene.layout[y][x]][colpath], x * 128 - bounds.X, y * 128 - bounds.Y);
			return LevelImg8bpp;
		}

		public static BitmapBits DrawBackground(int layer, Rectangle? section, bool lowPlane, bool highPlane)
		{
			Rectangle bounds;
			if (section.HasValue)
				bounds = section.Value;
			else
				bounds = new Rectangle(0, 0, BGWidth[layer] * 128, BGHeight[layer] * 128);
			BitmapBits LevelImg8bpp = new BitmapBits(bounds.Size);
			for (int y = Math.Max(bounds.Y / 128, 0); y <= Math.Min((bounds.Bottom - 1) / 128, BGHeight[layer] - 1); y++)
				for (int x = Math.Max(bounds.X / 128, 0); x <= Math.Min((bounds.Right - 1) / 128, BGWidth[layer] - 1); x++)
					if (Background.layers[layer].layout[y][x] < NewChunks.chunkList.Length)
					{
						if (lowPlane && highPlane)
							LevelImg8bpp.DrawSprite(ChunkSprites[Background.layers[layer].layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
						else if (lowPlane)
							LevelImg8bpp.DrawSpriteLow(ChunkSprites[Background.layers[layer].layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
						else if (highPlane)
							LevelImg8bpp.DrawSpriteHigh(ChunkSprites[Background.layers[layer].layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
					}
			return LevelImg8bpp;
		}

		public static BitmapBits32 DrawBackground32(int layer, Rectangle? section, Color backgroundColor, bool lowPlane, bool highPlane)
		{
			Rectangle bounds;
			if (section.HasValue)
				bounds = section.Value;
			else
				bounds = new Rectangle(0, 0, BGWidth[layer] * 128, BGHeight[layer] * 128);
			BitmapBits32 LevelImg8bpp = new BitmapBits32(bounds.Size);
			NewPalette.CopyTo(LevelImg8bpp.Palette, 0);
			LevelImg8bpp.Clear(backgroundColor);
			for (int y = Math.Max(bounds.Y / 128, 0); y <= Math.Min((bounds.Bottom - 1) / 128, BGHeight[layer] - 1); y++)
				for (int x = Math.Max(bounds.X / 128, 0); x <= Math.Min((bounds.Right - 1) / 128, BGWidth[layer] - 1); x++)
					if (Background.layers[layer].layout[y][x] < NewChunks.chunkList.Length)
					{
						if (lowPlane && highPlane)
							LevelImg8bpp.DrawSprite(ChunkSprites[Background.layers[layer].layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
						else if (lowPlane)
							LevelImg8bpp.DrawSpriteLow(ChunkSprites[Background.layers[layer].layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
						else if (highPlane)
							LevelImg8bpp.DrawSpriteHigh(ChunkSprites[Background.layers[layer].layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
					}
			return LevelImg8bpp;
		}

		private static void LoadObjectDefinitionFile(string file, bool mod = false)
		{
			Log($"Loading {(mod ? "mod" : "base")} object definition database \"{file}\".");
			string basepath = Path.GetDirectoryName(file);
			Dictionary<string, ObjectData> obj = IniSerializer.Deserialize<Dictionary<string, ObjectData>>(file);
			foreach (KeyValuePair<string, ObjectData> group in obj)
				if (!string.IsNullOrEmpty(group.Key))
				{
					INIObjDefs[group.Key] = group.Value;
					if (!string.IsNullOrEmpty(group.Value.CodeFile))
					{
						string path = Path.Combine(basepath, group.Value.CodeFile);
						if (mod && !File.Exists(path) && File.Exists(Path.Combine("SonLVLObjDefs", group.Value.CodeFile)))
							path = Path.Combine("SonLVLObjDefs", group.Value.CodeFile);
						group.Value.CodeFile = path;
					}
					if (!string.IsNullOrEmpty(group.Value.XMLFile))
					{
						string path = Path.Combine(basepath, group.Value.XMLFile);
						if (mod && !File.Exists(path) && File.Exists(Path.Combine("SonLVLObjDefs", group.Value.XMLFile)))
							path = Path.Combine("SonLVLObjDefs", group.Value.XMLFile);
						group.Value.XMLFile = path;
					}
				}
		}

		private static void InitObjectDefinitions()
		{
			IEnumerable<(GameConfig.ObjectInfo objinf, byte ind)> objlist;
			if (StageConfig.loadGlobalObjects)
				objlist = GlobalObjects.Concat(StageConfig.objects).Select((o, i) => (o, (byte)(i + 1)));
			else
				objlist = StageConfig.objects.Select((o, i) => (o, (byte)(i + 1)));
			List<KeyValuePair<byte, ObjectDefinition>> objdefs = new List<KeyValuePair<byte, ObjectDefinition>>();
#if !DEBUG
			Parallel.ForEach(objlist, group =>
#else
			foreach (var group in objlist)
#endif
			{
				ObjectDefinition def = MakeObjectDefinition(group.objinf);
				lock (objdefs)
					objdefs.Add(new KeyValuePair<byte, ObjectDefinition>(group.ind, def));
#if !DEBUG
			});
#else
			}
#endif
			ObjTypes = new List<ObjectDefinition>() { unkobj };
			foreach (var item in objdefs.OrderBy(a => a.Key))
				ObjTypes.Add(item.Value);
		}

		public static ObjectDefinition MakeObjectDefinition(GameConfig.ObjectInfo objinf)
		{
			if (!INIObjDefs.TryGetValue(objinf.script, out ObjectData data))
				data = new ObjectData();
			ObjectDefinition def;
			if (data.CodeFile != null)
			{
				string fulltypename = data.CodeType;
				string dllfile = Path.Combine(dllcache, fulltypename + ".dll");
				DateTime modDate = DateTime.MinValue;
				if (File.Exists(dllfile))
					modDate = File.GetLastWriteTime(dllfile);
				string fp = data.CodeFile.Replace('/', Path.DirectorySeparatorChar);
				Log($"Loading ObjectDefinition type {fulltypename} from \"{fp}\"...");
				if (modDate >= File.GetLastWriteTime(fp) & modDate > File.GetLastWriteTime(Application.ExecutablePath))
				{
					Log($"Loading type from cached assembly \"{dllfile}\"...");
					def = (ObjectDefinition)Activator.CreateInstance(System.Reflection.Assembly.LoadFile(Path.GetFullPath(dllfile)).GetType(fulltypename));
				}
				else
				{
					Log("Compiling code file...");
					string ext = Path.GetExtension(fp);
					CodeDomProvider pr = null;
					switch (ext.ToLowerInvariant())
					{
						case ".cs":
							pr = new Microsoft.CSharp.CSharpCodeProvider();
							break;
						case ".vb":
							pr = new Microsoft.VisualBasic.VBCodeProvider();
							break;
					}
					if (pr != null)
					{
						CompilerParameters para = new CompilerParameters(new string[] { "System.dll", "System.Core.dll", "System.Drawing.dll", System.Reflection.Assembly.GetExecutingAssembly().Location, typeof(Scene).Assembly.Location })
						{
							GenerateExecutable = false,
							GenerateInMemory = false,
							IncludeDebugInformation = true,
							OutputAssembly = Path.GetFullPath(dllfile)
						};
						CompilerResults res = pr.CompileAssemblyFromFile(para, fp);
						if (res.Errors.HasErrors)
						{
							Log("Compile failed.", "Errors:");
							foreach (CompilerError item in res.Errors)
								Log(item.ToString());
							Log(string.Empty);
							def = new DefaultObjectDefinition();
						}
						else
						{
							Log("Compile succeeded.");
							def = (ObjectDefinition)Activator.CreateInstance(res.CompiledAssembly.GetType(fulltypename));
						}
					}
					else
						def = new DefaultObjectDefinition();
				}
			}
			else if (data.XMLFile != null)
				def = new XMLObjectDefinition();
			else
				def = new DefaultObjectDefinition();
#if !DEBUG
			try
#endif
			{
				def.Init(objinf);
				def.Init(data);
			}
#if !DEBUG
			catch (Exception ex)
			{
				Log("Object definition " + objinf.script + " failed to initialize!");
				Log(ex.ToString());
				def = new DefaultObjectDefinition();
				def.Init(new ObjectData());
			}
#endif
			return def;
		}

		internal static Type ExpandTypeName(string type)
		{
			switch (type)
			{
				case "bool":
					return typeof(bool);
				case "byte":
					return typeof(byte);
				case "char":
					return typeof(char);
				case "decimal":
					return typeof(decimal);
				case "double":
					return typeof(double);
				case "float":
					return typeof(float);
				case "int":
					return typeof(int);
				case "long":
					return typeof(long);
				case "object":
					return typeof(object);
				case "sbyte":
					return typeof(sbyte);
				case "short":
					return typeof(short);
				case "string":
					return typeof(string);
				case "uint":
					return typeof(uint);
				case "ulong":
					return typeof(ulong);
				case "ushort":
					return typeof(ushort);
				default:
					return Type.GetType(type);
			}
		}

		public static void RedrawBlocks(IEnumerable<int> blocks, bool drawChunks)
		{
			List<int> chunks = new List<int>();
			foreach (int block in blocks)
			{
				RedrawBlock(block, false);
				if (drawChunks)
					for (int i = 0; i < NewChunks.chunkList.Length; i++)
					{
						if (chunks.Contains(i)) continue;
						for (int k = 0; k < 128 / 16; k++)
							for (int j = 0; j < 128 / 16; j++)
								if (NewChunks.chunkList[i].tiles[j][k].tileIndex == block)
								{
									chunks.Add(i);
									goto nextchunk;
								}
						nextchunk:;
					}
			}
			if (drawChunks)
				RedrawChunks(chunks);
		}

		public static void RedrawBlock(int block, bool drawChunks)
		{
			NewTileBmps[block] = NewTiles[block].ToBitmap(BmpPal);
			if (drawChunks)
				for (int i = 0; i < NewChunks.chunkList.Length; i++)
				{
					for (int k = 0; k < 8; k++)
						for (int j = 0; j < 8; j++)
							if (NewChunks.chunkList[i].tiles[j][k].tileIndex == block)
								goto draw;
					continue;
					draw:
					RedrawChunk(i);
				}
		}

		public static void RedrawChunks(IEnumerable<int> chunks)
		{
			foreach (int chunk in chunks)
				RedrawChunk(chunk);
		}

		static readonly Dictionary<Tiles128x128.Block.Tile.Solidities, int> solidcolormap = new Dictionary<Tiles128x128.Block.Tile.Solidities, int>()
		{
			{ Tiles128x128.Block.Tile.Solidities.SolidTop, 0 },
			{ Tiles128x128.Block.Tile.Solidities.SolidAllButTop, 1 },
			{ Tiles128x128.Block.Tile.Solidities.SolidAll, 2 },
			{ Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip, 3 }
		};
		public static void RedrawChunk(int chunk)
		{
			BitmapBits tmplow = new BitmapBits(128, 128);
			BitmapBits tmphigh = new BitmapBits(128, 128);
			ChunkColBmpBits[chunk][0] = new BitmapBits(128, 128);
			ChunkColBmpBits[chunk][1] = new BitmapBits(128, 128);
			for (int by = 0; by < 8; by++)
				for (int bx = 0; bx < 8; bx++)
				{
					Tiles128x128.Block.Tile blk = NewChunks.chunkList[chunk].tiles[by][bx];
					BitmapBits blkbmp;
					if (blk.tileIndex < NewTiles.Length)
						blkbmp = NewTiles[blk.tileIndex];
					else
						blkbmp = InvalidTile;
					BitmapBits bmp = new BitmapBits(blkbmp);
					bmp.Flip(blk.direction);
					switch (blk.visualPlane)
					{
						case Tiles128x128.Block.Tile.VisualPlanes.Low:
							tmplow.DrawBitmap(bmp, bx * 16, by * 16);
							break;
						case Tiles128x128.Block.Tile.VisualPlanes.High:
							tmphigh.DrawBitmap(bmp, bx * 16, by * 16);
							break;
					}
					if (blk.solidityA != Tiles128x128.Block.Tile.Solidities.SolidNone)
					{
						bmp = new BitmapBits(NewColBmpBits[blk.tileIndex][0]);
						bmp.IncrementIndexes(solidcolormap[blk.solidityA]);
						bmp.Flip(blk.direction);
						ChunkColBmpBits[chunk][0].DrawBitmap(bmp, bx * 16, by * 16);
					}
					if (blk.solidityB != Tiles128x128.Block.Tile.Solidities.SolidNone)
					{
						bmp = new BitmapBits(NewColBmpBits[blk.tileIndex][1]);
						bmp.IncrementIndexes(solidcolormap[blk.solidityB]);
						bmp.Flip(blk.direction);
						ChunkColBmpBits[chunk][1].DrawBitmap(bmp, bx * 16, by * 16);
					}
				}
			ChunkSprites[chunk] = new Sprite(tmplow, tmphigh);
			ChunkBmps[chunk][0] = tmplow.ToBitmap(BmpPal);
			ChunkBmps[chunk][1] = tmphigh.ToBitmap(BmpPal);
			ChunkColSprites[chunk][0] = new Sprite(ChunkColBmpBits[chunk][0]);
			ChunkColSprites[chunk][1] = new Sprite(ChunkColBmpBits[chunk][1]);
			ChunkColBmps[chunk][0] = ChunkColBmpBits[chunk][0].ToBitmap(Color.Transparent, Color.White, Color.Yellow, Color.Black, Color.Red);
			ChunkColBmps[chunk][1] = ChunkColBmpBits[chunk][1].ToBitmap(Color.Transparent, Color.White, Color.Yellow, Color.Black, Color.Red);
			tmplow.DrawBitmapComposited(tmphigh, 0, 0);
			CompChunkBmps[chunk] = tmplow.ToBitmap(BmpPal);
		}

		public static ObjectDefinition GetObjectDefinition(byte ID)
		{
			if (ObjTypes != null && ID < ObjTypes.Count)
				return ObjTypes[ID];
			else
				return unkobj;
		}

		public static ObjectEntry CreateObject(byte ID)
		{
			if (Scene.entities.Count < Scene.ENTITY_LIST_SIZE)
			{
				Scene.Entity ent;
				switch (Game.RSDKVer)
				{
					case EngineVersion.V4:
						ent = new RSDKv4.Scene.Entity() { type = ID };
						break;
					case EngineVersion.V3:
						ent = new RSDKv3.Scene.Entity() { type = ID };
						break;
					default:
						return null;
				}
				ent.propertyValue = GetObjectDefinition(ID).DefaultSubtype;
				Scene.entities.Add(ent);
				ObjectEntry obj = ObjectEntry.Create(ent);
				Objects.Add(obj);
				return obj;
			}
			return null;
		}

		public static void AddObject(ObjectEntry obj)
		{
			if (Scene.entities.Count < Scene.ENTITY_LIST_SIZE)
			{
				Objects.Add(obj);
				Scene.entities.Add(obj.Entity);
			}
		}

		public static void DeleteObject(ObjectEntry obj)
		{
			Objects.Remove(obj);
			Scene.entities.Remove(obj.Entity);
		}

		public static void PaletteChanged()
		{
			NewPalette.CopyTo(BmpPal.Entries, 0);
			BmpPal.Entries[ColorTransparent] = Color.Transparent;
			foreach (Bitmap item in NewTileBmps)
				item.Palette = BmpPal;
			foreach (Bitmap[] item in ChunkBmps)
			{
				item[0].Palette = BmpPal;
				item[1].Palette = BmpPal;
			}
			foreach (Bitmap[] item in ChunkColBmps)
			{
				item[0].Palette = BmpPal;
				item[1].Palette = BmpPal;
			}
			foreach (Bitmap item in CompChunkBmps)
				item.Palette = BmpPal;
			PaletteChangedEvent();
		}

		public static void RedrawCol(int block, bool drawChunks)
		{
			for (int i = 0; i < 2; i++)
			{
				NewColBmpBits[block][i] = new BitmapBits(16, 16);
				TileConfig.CollisionMask mask = Collision.collisionMasks[i][block];
				for (int x = 0; x < 16; x++)
					if (mask.heightMasks[x].solid)
						if (mask.flipY)
						{
							for (int y = 0; y <= mask.heightMasks[x].height; y++)
								NewColBmpBits[block][i][x, y] = 1;
						}
						else
						{
							for (int y = 15; y >= mask.heightMasks[x].height; y--)
								NewColBmpBits[block][i][x, y] = 1;
						}
				NewColBmps[block][i] = NewColBmpBits[block][i].ToBitmap(Color.Transparent, Color.White);
			}
			if (drawChunks)
			{
				for (int i = 0; i < NewChunks.chunkList.Length; i++)
				{
					for (int k = 0; k < 8; k++)
						for (int j = 0; j < 8; j++)
							if (NewChunks.chunkList[i].tiles[k][j].tileIndex == block)
								goto draw;
					continue;
					draw:
					RedrawChunk(i);
				}
			}
		}

		public static ImportResult BitmapToTiles(BitmapInfo bmpi, bool[,] priority, ColInfo[][,] collision, byte? forcepal, List<BitmapBits> tiles, List<TileConfig.CollisionMask>[] cols, bool optimize, Action updateProgress = null)
		{
			int w = bmpi.Width / 16;
			int h = bmpi.Height / 16;
			ImportResult result = new ImportResult(w, h, collision != null, collision?[1] != null);
			for (int y = 0; y < h; y++)
				for (int x = 0; x < w; x++)
				{
					Tiles128x128.Block.Tile map = new Tiles128x128.Block.Tile() { visualPlane = priority[x, y] ? Tiles128x128.Block.Tile.VisualPlanes.High : Tiles128x128.Block.Tile.VisualPlanes.Low };
					if (collision != null)
					{
						map.solidityA = collision[0][x, y].Solidity;
						map.solidityB = (collision[1] != null) ? collision[1][x, y].Solidity : map.solidityA;
					}
					BitmapBits tile = BmpToTile(new BitmapInfo(bmpi, x * 16, y * 16, 16, 16), forcepal);
					TileConfig.CollisionMask mask1 = null;
					TileConfig.CollisionMask mask2 = null;
					if (collision != null)
					{
						// If the tile is empty on the collision bitmap, then it's hard to differentiate between:
						// - the collision is SolidNone in the chunk, but the collision mask for the tile still has data
						// - or, it's empty because the tile's collision mask is actually empty (and the tile's solidity on the chunk doesn't matter)
						// Here, we wanna try and reduce creating duplicates when possible
						// So, if the tile's not solid, then let's just act as if we don't have any collision data for it
						mask1 = collision[0][x, y].CollisionMask;
						if (collision[1] != null)
							mask2 = collision[1][x, y].CollisionMask;

						if (map.solidityA == Tiles128x128.Block.Tile.Solidities.SolidNone)
							mask1 = null;

						if (map.solidityB == Tiles128x128.Block.Tile.Solidities.SolidNone)
							mask2 = null;
					}
					bool match = false;
					if (optimize)
					{
						BitmapBits tileh = new BitmapBits(tile);
						tileh.Flip(true, false);
						TileConfig.CollisionMask mask1h = null;
						TileConfig.CollisionMask mask2h = null;
						if (mask1 != null)
						{
							mask1h = mask1.Clone();
							mask1h.Flip(true, false);
						}
						if (mask2 != null)
						{
							mask2h = mask2.Clone();
							mask2h.Flip(true, false);
						}
						BitmapBits tilev = new BitmapBits(tile);
						tilev.Flip(false, true);
						TileConfig.CollisionMask mask1v = null;
						TileConfig.CollisionMask mask2v = null;
						if (mask1 != null)
						{
							mask1v = mask1.Clone();
							mask1v.Flip(false, true);
						}
						if (mask2 != null)
						{
							mask2v = mask2.Clone();
							mask2v.Flip(false, true);
						}
						BitmapBits tilehv = new BitmapBits(tilev);
						tilehv.Flip(true, false);
						TileConfig.CollisionMask mask1hv = null;
						TileConfig.CollisionMask mask2hv = null;
						if (mask1v != null)
						{
							mask1hv = mask1v.Clone();
							mask1hv.Flip(true, false);
						}
						if (mask2v != null)
						{
							mask2hv = mask2v.Clone();
							mask2hv.Flip(true, false);
						}

						for (int i = 0; i < tiles.Count; i++)
						{
							if (tiles[i].Bits.FastArrayEqual(tile.Bits)
								&& (mask1 == null || mask1.Equal(cols[0][i], 0x12))
								&& (mask2 == null || mask2.Equal(cols[1][i], 0x12)))
							{
								match = true;
								map.tileIndex = (ushort)i;
								break;
							}
							if (tiles[i].Bits.FastArrayEqual(tileh.Bits)
								&& (mask1h == null || mask1h.Equal(cols[0][i], 0x12))
								&& (mask2h == null || mask2h.Equal(cols[1][i], 0x12)))
							{
								match = true;
								map.tileIndex = (ushort)i;
								map.direction = Tiles128x128.Block.Tile.Directions.FlipX;
								break;
							}
							if (tiles[i].Bits.FastArrayEqual(tilev.Bits)
								&& (mask1v == null || mask1v.Equal(cols[0][i], 0x12))
								&& (mask2v == null || mask2v.Equal(cols[1][i], 0x12)))
							{
								match = true;
								map.tileIndex = (ushort)i;
								map.direction = Tiles128x128.Block.Tile.Directions.FlipY;
								break;
							}
							if (tiles[i].Bits.FastArrayEqual(tilehv.Bits)
								&& (mask1hv == null || mask1hv.Equal(cols[0][i], 0x12))
								&& (mask2hv == null || mask2hv.Equal(cols[1][i], 0x12)))
							{
								match = true;
								map.tileIndex = (ushort)i;
								map.direction = Tiles128x128.Block.Tile.Directions.FlipXY;
								break;
							}
						}
					}
					if (!match)
					{
						tiles.Add(tile);
						result.Art.Add(tile);
						if (collision != null)
						{
							if (mask1 == null)
								mask1 = collision[0][x, y].CollisionMask;

							cols[0].Add(mask1);
							result.Collision1.Add(mask1);

							if (collision[1] != null)
							{
								if (mask2 == null)
									mask2 = collision[1][x, y].CollisionMask;

								cols[1].Add(mask2);
								result.Collision2.Add(mask2);
							}
						}
						map.tileIndex = (ushort)(tiles.Count - 1);
					}
					result.Mappings[x, y] = map;
					updateProgress?.Invoke();
				}
			return result;
		}

		public static BitmapBits BmpToTile(BitmapInfo bmp, byte? forcepal)
		{
			BitmapBits bmpbits = new BitmapBits(16, 16);
			switch (bmp.PixelFormat)
			{
				case PixelFormat.Format1bppIndexed:
					LoadBitmap1BppIndexed(bmpbits, bmp.Pixels, bmp.Stride);
					if (forcepal.HasValue)
						bmpbits.IncrementIndexes(forcepal.Value * 16);
					break;
				case PixelFormat.Format32bppArgb:
					LoadBitmap32BppArgb(bmpbits, bmp.Pixels, bmp.Stride, NewPalette);
					break;
				case PixelFormat.Format4bppIndexed:
					LoadBitmap4BppIndexed(bmpbits, bmp.Pixels, bmp.Stride);
					if (forcepal.HasValue)
						bmpbits.IncrementIndexes(forcepal.Value * 16);
					break;
				case PixelFormat.Format8bppIndexed:
					LoadBitmap8BppIndexed(bmpbits, bmp.Pixels, bmp.Stride);
					break;
				default:
					throw new Exception("wat");
			}
			return bmpbits;
		}

		public static ColInfo[,] GetColMap(Bitmap bmp)
		{
			if (bmp.PixelFormat != PixelFormat.Format32bppArgb)
				bmp = bmp.To32bpp();
			BitmapBits bmpbits = new BitmapBits(bmp.Width, bmp.Height);
			BitmapData bmpd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
			int stride = bmpd.Stride;
			byte[] Bits = new byte[Math.Abs(stride) * bmpd.Height];
			System.Runtime.InteropServices.Marshal.Copy(bmpd.Scan0, Bits, 0, Bits.Length);
			bmp.UnlockBits(bmpd);
			LoadBitmap32BppArgb(bmpbits, Bits, stride, new Color[] { Color.Magenta, Color.White, Color.Yellow, Color.Black, Color.Red });
			ColInfo[,] result = new ColInfo[bmpbits.Width / 16, bmpbits.Height / 16];
			Parallel.For(0, bmpbits.Height / 16, by =>
			{
				for (int bx = 0; bx < bmpbits.Width / 16; bx++)
				{
					ushort[] coltypes = new ushort[4];
					TileConfig.CollisionMask mask = new TileConfig.CollisionMask();
					int fcnt = 0;
					int ccnt = 0;
					for (int x = 0; x < 16; x++)
					{
						if (bmpbits[bx * 16 + x, by * 16] != 0)
						{
							if (bmpbits[bx * 16 + x, by * 16 + 15] == 0)
								++ccnt;
						}
						else if (bmpbits[bx * 16 + x, by * 16 + 15] != 0)
							++fcnt;
					}
					if (ccnt > fcnt)
						mask.flipY = true;
					Point? start = null;
					Point? end = null;
					for (int x = 0; x < 16; x++)
					{
						if (mask.flipY)
						{
							for (int y = 0; y < 16; y++)
							{
								if (bmpbits[bx * 16 + x, by * 16 + y] != 0)
								{
									coltypes[bmpbits[bx * 16 + x, by * 16 + y] - 1]++;
									mask.heightMasks[x].solid = true;
									mask.heightMasks[x].height = (byte)y;
								}
								else
								{
									if (mask.heightMasks[x].solid)
									{
										if (!start.HasValue)
											start = new Point(x, y - 1);
										end = new Point(x, y - 1);
									}
									break;
								}
							}
							if (mask.heightMasks[x].solid && mask.heightMasks[x].height == 15)
							{
								if (!start.HasValue || start.Value.Y == 15)
									start = new Point(x, 15);
								if (!end.HasValue || end.Value.Y != 15)
									end = new Point(x, 15);
							}
						}
						else
						{
							for (int y = 15; y >= 0; y--)
							{
								if (bmpbits[bx * 16 + x, by * 16 + y] != 0)
								{
									coltypes[bmpbits[bx * 16 + x, by * 16 + y] - 1]++;
									mask.heightMasks[x].solid = true;
									mask.heightMasks[x].height = (byte)y;
								}
								else
								{
									if (mask.heightMasks[x].solid)
									{
										if (!start.HasValue)
											start = new Point(x, y + 1);
										end = new Point(x, y + 1);
									}
									break;
								}
							}
							if (mask.heightMasks[x].solid && mask.heightMasks[x].height == 0)
							{
								if (!start.HasValue || start.Value.Y == 0)
									start = new Point(x, 0);
								if (!end.HasValue || end.Value.Y != 0)
									end = new Point(x, 0);
							}
						}
					}
					Tiles128x128.Block.Tile.Solidities solid = Tiles128x128.Block.Tile.Solidities.SolidNone;
					if (start.HasValue)
					{
						solid = Tiles128x128.Block.Tile.Solidities.SolidTop;
						ushort max = coltypes[0];
						if (coltypes[1] > max)
						{
							solid = Tiles128x128.Block.Tile.Solidities.SolidAllButTop;
							max = coltypes[1];
						}
						if (coltypes[2] > max)
						{
							solid = Tiles128x128.Block.Tile.Solidities.SolidAll;
							max = coltypes[2];
						}
						if (coltypes[3] > max)
							solid = Tiles128x128.Block.Tile.Solidities.SolidTopNoGrip;
						if (start.Value.Y != end.Value.Y)
							if (mask.flipY)
							{
								mask.roofAngle = (byte)(Math.Atan2(end.Value.Y - start.Value.Y, end.Value.X - start.Value.X) * (256 / (2 * Math.PI)) + 0x80);
								switch (Math.Sign(mask.roofAngle.CompareTo(0x80)))
								{
									case -1:
										mask.rWallAngle = mask.roofAngle;
										break;
									case 1:
										mask.lWallAngle = mask.roofAngle;
										break;
								}
							}
							else
							{
								mask.floorAngle = (byte)(Math.Atan2(end.Value.Y - start.Value.Y, end.Value.X - start.Value.X) * (256 / (2 * Math.PI)));
								switch (Math.Sign(mask.floorAngle.CompareTo(0x80)))
								{
									case -1:
										mask.rWallAngle = mask.floorAngle;
										break;
									case 1:
										mask.lWallAngle = mask.floorAngle;
										break;
								}
							}
					}
					result[bx, by] = new ColInfo(solid, mask);
				}
			});
			return result;
		}

		public static void GetPriMap(Bitmap bmp, bool[,] primap)
		{
			if (bmp.PixelFormat != PixelFormat.Format32bppArgb)
				bmp = bmp.To32bpp();
			BitmapBits bmpbits = new BitmapBits(bmp.Width, bmp.Height);
			BitmapData bmpd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
			int stride = bmpd.Stride;
			byte[] Bits = new byte[Math.Abs(stride) * bmpd.Height];
			System.Runtime.InteropServices.Marshal.Copy(bmpd.Scan0, Bits, 0, Bits.Length);
			bmp.UnlockBits(bmpd);
			LoadBitmap32BppArgb(bmpbits, Bits, stride, new Color[] { Color.Black, Color.White });
			int w = Math.Min(primap.GetLength(0), bmpbits.Width / 16);
			int h = Math.Min(primap.GetLength(1), bmpbits.Height / 16);
			Parallel.For(0, h, ty =>
			{
				for (int tx = 0; tx < w; tx++)
				{
					ushort[] cnt = new ushort[2];
					for (int y = 0; y < 16; y++)
						for (int x = 0; x < 16; x++)
							cnt[bmpbits[(tx * 16) + x, (ty * 16) + y]]++;
					primap[tx, ty] = cnt[0] < cnt[1];
				}
			});
		}

		private static void LoadBitmap1BppIndexed(BitmapBits bmp, byte[] Bits, int Stride)
		{
			int dstaddr = 0;
			for (int y = 0; y < bmp.Height; y++)
			{
				int srcaddr = y * Math.Abs(Stride);
				for (int x = 0; x < bmp.Width; x += 8)
				{
					byte b = Bits[srcaddr++];
					bmp.Bits[dstaddr++] = (byte)(b >> 7 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 6 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 5 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 4 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 3 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 2 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 1 & 1);
					bmp.Bits[dstaddr++] = (byte)(b & 1);
				}
			}
		}

		private static void LoadBitmap32BppArgb(BitmapBits bmp, byte[] Bits, int Stride, Color[] palette)
		{
			int dstaddr = 0;
			for (int y = 0; y < bmp.Height; y++)
			{
				int srcaddr = y * Math.Abs(Stride);
				for (int x = 0; x < bmp.Width; x++)
				{
					Color col = Color.FromArgb(BitConverter.ToInt32(Bits, srcaddr + (x * 4)));
					bmp.Bits[dstaddr++] = (byte)((col.A >= 128) ? col.FindNearestMatch(palette) : 0);
				}
			}
		}

		private static void LoadBitmap4BppIndexed(BitmapBits bmp, byte[] Bits, int Stride)
		{
			int dstaddr = 0;
			for (int y = 0; y < bmp.Height; y++)
			{
				int srcaddr = y * Math.Abs(Stride);
				for (int x = 0; x < bmp.Width; x += 2)
				{
					byte b = Bits[srcaddr++];
					bmp.Bits[dstaddr++] = (byte)(b >> 4);
					bmp.Bits[dstaddr++] = (byte)(b & 0xF);
				}
			}
		}

		private static void LoadBitmap8BppIndexed(BitmapBits bmp, byte[] Bits, int Stride)
		{
			int dstaddr = 0;
			for (int y = 0; y < bmp.Height; y++)
			{
				int srcaddr = y * Math.Abs(Stride);
				for (int x = 0; x < bmp.Width; x++)
					bmp.Bits[dstaddr++] = Bits[srcaddr++];
			}
		}

		public static void Log(params string[] message) { LogEvent(message); }

		public static Bitmap BitmapBitsToBitmap(BitmapBits bmp)
		{
			return bmp.ToBitmap(BmpPal);
		}

		public static BitmapBits GetSpriteSheet(string sheetname)
		{
			lock (spriteSheets)
			{
				sheetname = "Data/Sprites/" + sheetname;
				if (spriteSheets.TryGetValue(sheetname, out BitmapBits bits))
					return bits;
				BitmapBits img = new BitmapBits(LevelData.ReadFile<Gif>(sheetname));
				spriteSheets.Add(sheetname, img);
				return img;
			}
		}

		public static Size FGSize { get { return new Size(FGWidth, FGHeight); } }

		public static int FGWidth { get { return Scene.width; } }
		public static int FGHeight { get { return Scene.height; } }

		public static readonly Indexer<int, Size> BGSize = new Indexer<int, Size>(ind => new Size(BGWidth[ind], BGHeight[ind]));

		public static readonly Indexer<int, int> BGWidth = new Indexer<int, int>(ind => Background.layers[ind].width);
		public static readonly Indexer<int, int> BGHeight = new Indexer<int, int>(ind => Background.layers[ind].height);

		public static void ResizeFG(Size newSize) { ResizeFG(newSize.Width, newSize.Height); }

		public static void ResizeFG(int width, int height)
		{
			Scene.resize((byte)width, (byte)height);
		}

		public static void ResizeBG(int layer, Size newSize) { ResizeBG(layer, newSize.Width, newSize.Height); }

		public static void ResizeBG(int layer, int width, int height)
		{
			Background.layers[layer].resize((byte)width, (byte)height);
		}

		public static void RemapLayouts(Action<ushort[][], int, int> func)
		{
			for (int y = 0; y < FGHeight; y++)
				for (int x = 0; x < FGWidth; x++)
					func(Scene.layout, x, y);
			for (int i = 0; i < 8; i++)
				for (int y = 0; y < BGHeight[i]; y++)
					for (int x = 0; x < BGWidth[i]; x++)
						func(Background.layers[i].layout, x, y);
			foreach (var astg in AdditionalScenes)
				for (int y = 0; y < astg.Scene.height; y++)
					for (int x = 0; x < astg.Scene.width; x++)
						func(astg.Scene.layout, x, y);
		}

		public static Tiles128x128.Block Flip(this Tiles128x128.Block chunk, bool xflip, bool yflip)
		{
			Tiles128x128.Block result = new Tiles128x128.Block();
			if (xflip)
			{
				if (yflip)
				{
					for (int y = 0; y < 8; y++)
						for (int x = 0; x < 8; x++)
						{
							result.tiles[y][x] = chunk.tiles[7 - y][7 - x].Clone();
							result.tiles[y][x].direction ^= Tiles128x128.Block.Tile.Directions.FlipXY;
						}
				}
				else
				{
					for (int y = 0; y < 8; y++)
						for (int x = 0; x < 8; x++)
						{
							result.tiles[y][x] = chunk.tiles[y][7 - x].Clone();
							result.tiles[y][x].direction ^= Tiles128x128.Block.Tile.Directions.FlipX;
						}
				}
			}
			else if (yflip)
			{
				for (int y = 0; y < 8; y++)
					for (int x = 0; x < 8; x++)
					{
						result.tiles[y][x] = chunk.tiles[7 - y][x].Clone();
						result.tiles[y][x].direction ^= Tiles128x128.Block.Tile.Directions.FlipY;
					}
			}
			else
				result = chunk.Clone();
			return result;
		}

		public static Tiles128x128.Block Clone(this Tiles128x128.Block src)
		{
			Tiles128x128.Block result = new Tiles128x128.Block();
			for (int y = 0; y < 8; y++)
				for (int x = 0; x < 8; x++)
					result.tiles[y][x] = src.tiles[y][x].Clone();
			return result;
		}

		public static bool Equal(this Tiles128x128.Block src, Tiles128x128.Block other)
		{
			for (int y = 0; y < 8; y++)
				for (int x = 0; x < 8; x++)
					if (!src.tiles[y][x].Equal(other.tiles[y][x]))
						return false;
			return true;
		}

		public static Tiles128x128.Block.Tile Clone(this Tiles128x128.Block.Tile src) => new Tiles128x128.Block.Tile()
		{
			direction = src.direction,
			solidityA = src.solidityA,
			solidityB = src.solidityB,
			tileIndex = src.tileIndex,
			visualPlane = src.visualPlane
		};

		public static bool Equal(this Tiles128x128.Block.Tile src, Tiles128x128.Block.Tile other)
		{
			return src.direction == other.direction
				&& src.solidityA == other.solidityA
				&& src.solidityB == other.solidityB
				&& src.tileIndex == other.tileIndex
				&& src.visualPlane == other.visualPlane;
		}

		
		public static bool HasFreeTiles()
		{
			return Enumerable.Range(0, NewTiles.Length).Except(NewChunks.chunkList.SelectMany(a => a.tiles.SelectMany(b => b).Select(c => c.tileIndex)).Select(a => (int)a))
				.Any(c => NewTiles[c].Bits.FastArrayEqual(0));
		}

		public static bool HasFreeChunks()
		{
			return Enumerable.Range(0, NewChunks.chunkList.Length).Except(Scene.layout.SelectMany(a => a).Union(Background.layers.SelectMany(a => a.layout.SelectMany(b => b))).Select(a => (int)a))
				.Any(c => NewChunks.chunkList[c].tiles.SelectMany(a => a).All(b => b.direction == Tiles128x128.Block.Tile.Directions.FlipNone && b.tileIndex == 0));
		}

		public static IEnumerable<ushort> GetFreeTiles()
		{
			return Enumerable.Range(0, NewTiles.Length).Select(a => (ushort)a).Except(NewChunks.chunkList.SelectMany(a => a.tiles.SelectMany(b => b).Select(c => c.tileIndex)))
				.Where(c => NewTiles[c].Bits.FastArrayEqual(0));
		}

		public static IEnumerable<ushort> GetFreeChunks()
		{
			return Enumerable.Range(0, NewChunks.chunkList.Length).Select(a => (ushort)a).Except(Scene.layout.SelectMany(a => a).Union(Background.layers.SelectMany(a => a.layout.SelectMany(b => b))))
				.Where(c => NewChunks.chunkList[c].tiles.SelectMany(a => a).All(b => b.direction == Tiles128x128.Block.Tile.Directions.FlipNone && b.tileIndex == 0));
		}

		public static void GetLongestFreeTileStreak(out ushort start, out int length)
		{
			// This function is used for art importing, when tiles should be grouped together
			// (ie you want all ani tiles to be in a straight line, rather than sprinkled across the tile list)

			var freeTiles = GetFreeTiles().ToList();
			ushort curStart = start = freeTiles[0];
			int curLength = length = 1;

			// Let's find the longest streak and where it starts
			for (int i = 1; i < freeTiles.Count; i++)
			{
				if (freeTiles[i] == freeTiles[i - 1] + 1)
					curLength++; // Still goin..
				else
				{
					// Streak broke, let's see if we got a new best
					if (curLength > length)
					{
						length = curLength;
						start = curStart;
					}

					// Either way, we gotta reset..
					curStart = freeTiles[i];
					curLength = 1;
				}
			}

			if (curLength > start)
			{
				length = curLength;
				start = curStart;
			}
		}

		public static void CalcAngles(this TileConfig.CollisionMask mask)
		{
			mask.floorAngle = 0;
			mask.rWallAngle = 0x40;
			mask.roofAngle = 0x80;
			mask.lWallAngle = 0xC0;
			Point? start = null;
			Point? end = null;
			for (int x = 0; x < 16; x++)
			{
				if (mask.heightMasks[x].solid)
				{
					if (!start.HasValue)
						start = new Point(x, mask.heightMasks[x].height);
					end = new Point(x, mask.heightMasks[x].height);
					if (mask.flipY)
					{
						if (mask.heightMasks[x].height == 15 && start.Value.Y == 15)
							start = new Point(x, 15);
					}
					else
					{
						if (mask.heightMasks[x].height == 0 && start.Value.Y == 0)
							start = new Point(x, 0);
					}
				}
			}
			if (start.HasValue && start.Value.Y != end.Value.Y)
				if (mask.flipY)
				{
					mask.roofAngle = (byte)(Math.Atan2(end.Value.Y - start.Value.Y, end.Value.X - start.Value.X) * (256 / (2 * Math.PI)) + 0x80);
					switch (Math.Sign(mask.roofAngle.CompareTo(0x80)))
					{
						case -1:
							mask.rWallAngle = mask.roofAngle;
							break;
						case 1:
							mask.lWallAngle = mask.roofAngle;
							break;
					}
				}
				else
				{
					mask.floorAngle = (byte)(Math.Atan2(end.Value.Y - start.Value.Y, end.Value.X - start.Value.X) * (256 / (2 * Math.PI)));
					switch (Math.Sign(mask.floorAngle.CompareTo(0x80)))
					{
						case -1:
							mask.rWallAngle = mask.floorAngle;
							break;
						case 1:
							mask.lWallAngle = mask.floorAngle;
							break;
					}
				}
		}

		public static void Flip(this TileConfig.CollisionMask mask, bool xflip, bool yflip)
		{
			if (xflip)
			{
				Array.Reverse(mask.heightMasks);
				if (yflip)
				{
					mask.flipY = !mask.flipY;
					for (int i = 0; i < 16; i++)
						if (mask.heightMasks[i].solid)
							mask.heightMasks[i].height = (byte)(15 - mask.heightMasks[i].height);
					if (!mask.flipY)
					{
						if (mask.roofAngle != 0x80)
						{
							mask.floorAngle = (byte)((mask.roofAngle + 0x80) & 0xFF);
							switch (Math.Sign(mask.floorAngle.CompareTo(0x80)))
							{
								case -1:
									mask.rWallAngle = mask.floorAngle;
									mask.lWallAngle = 0xC0;
									break;
								case 1:
									mask.lWallAngle = mask.floorAngle;
									mask.rWallAngle = 0x40;
									break;
							}
						}
					}
					else
					{
						if (mask.floorAngle != 0)
						{
							mask.roofAngle = (byte)((mask.floorAngle + 0x80) & 0xFF);
							switch (Math.Sign(mask.roofAngle.CompareTo(0x80)))
							{
								case -1:
									mask.rWallAngle = mask.roofAngle;
									mask.lWallAngle = 0xC0;
									break;
								case 1:
									mask.lWallAngle = mask.roofAngle;
									mask.rWallAngle = 0x40;
									break;
							}
						}
					}
				}
				else if (mask.flipY)
				{
					if (mask.roofAngle != 0x80)
					{
						mask.roofAngle = (byte)(-mask.roofAngle & 0xFF);
						switch (Math.Sign(mask.roofAngle.CompareTo(0x80)))
						{
							case -1:
								mask.rWallAngle = mask.roofAngle;
								mask.lWallAngle = 0xC0;
								break;
							case 1:
								mask.lWallAngle = mask.roofAngle;
								mask.rWallAngle = 0x40;
								break;
						}
					}
				}
				else
				{
					if (mask.floorAngle != 0)
					{
						mask.floorAngle = (byte)(-mask.floorAngle & 0xFF);
						switch (Math.Sign(mask.floorAngle.CompareTo(0x80)))
						{
							case -1:
								mask.rWallAngle = mask.floorAngle;
								mask.lWallAngle = 0xC0;
								break;
							case 1:
								mask.lWallAngle = mask.floorAngle;
								mask.rWallAngle = 0x40;
								break;
						}
					}
				}
			}
			else if (yflip)
			{
				mask.flipY = !mask.flipY;
				for (int i = 0; i < 16; i++)
					if (mask.heightMasks[i].solid)
						mask.heightMasks[i].height = (byte)(15 - mask.heightMasks[i].height);
				if (!mask.flipY)
				{
					if (mask.roofAngle != 0x80)
					{
						mask.floorAngle = (byte)((-(mask.roofAngle + 0x40) - 0x40) & 0xFF);
						switch (Math.Sign(mask.floorAngle.CompareTo(0x80)))
						{
							case -1:
								mask.rWallAngle = mask.floorAngle;
								mask.lWallAngle = 0xC0;
								break;
							case 1:
								mask.lWallAngle = mask.floorAngle;
								mask.rWallAngle = 0x40;
								break;
						}
					}
				}
				else
				{
					if (mask.floorAngle != 0)
					{
						mask.roofAngle = (byte)((-(mask.floorAngle + 0x40) - 0x40) & 0xFF);
						switch (Math.Sign(mask.roofAngle.CompareTo(0x80)))
						{
							case -1:
								mask.rWallAngle = mask.roofAngle;
								mask.lWallAngle = 0xC0;
								break;
							case 1:
								mask.lWallAngle = mask.roofAngle;
								mask.rWallAngle = 0x40;
								break;
						}
					}
				}
			}
		}

		public static TileConfig.CollisionMask.HeightMask Clone(this TileConfig.CollisionMask.HeightMask src)
		{
			return new TileConfig.CollisionMask.HeightMask()
			{
				height = src.height,
				solid = src.solid
			};
		}

		public static TileConfig.CollisionMask Clone(this TileConfig.CollisionMask src)
		{
			return new TileConfig.CollisionMask()
			{
				flags = src.flags,
				flipY = src.flipY,
				floorAngle = src.floorAngle,
				rWallAngle = src.rWallAngle,
				roofAngle = src.roofAngle,
				lWallAngle = src.lWallAngle,
				heightMasks = src.heightMasks.Select(a => a.Clone()).ToArray()
			};
		}

		public static bool Equal(this TileConfig.CollisionMask src, TileConfig.CollisionMask other)
		{
			if (src.flags == other.flags
				&& src.flipY == other.flipY
				&& src.floorAngle == other.floorAngle
				&& src.rWallAngle == other.rWallAngle
				&& src.roofAngle == other.roofAngle
				&& src.lWallAngle == other.lWallAngle)
			{
				for (int i = 0; i < 16; i++)
				{
					if (src.heightMasks[i].solid != other.heightMasks[i].solid)
						return false;
					if (src.heightMasks[i].solid)
						if (src.heightMasks[i].height != other.heightMasks[i].height)
							return false;
				}
				return true;
			}
			return false;
		}

		public static bool Equal(this TileConfig.CollisionMask src, TileConfig.CollisionMask other, int threshold)
		{
			if (src.flags == other.flags
				&& src.flipY == other.flipY
				&& Math.Abs(src.floorAngle - other.floorAngle) < threshold
				&& Math.Abs(src.rWallAngle - other.rWallAngle) < threshold
				&& Math.Abs(src.roofAngle - other.roofAngle) < threshold
				&& Math.Abs(src.lWallAngle - other.lWallAngle) < threshold)
			{
				for (int i = 0; i < 16; i++)
				{
					if (src.heightMasks[i].solid != other.heightMasks[i].solid)
						return false;
					if (src.heightMasks[i].solid)
						if (src.heightMasks[i].height != other.heightMasks[i].height)
							return false;
				}
				return true;
			}
			if (src.flipY != other.flipY)
			{
				for (int i = 0; i < 16; i++)
				{
					if (src.heightMasks[i].solid != other.heightMasks[i].solid)
						return false;
					if (src.heightMasks[i].solid)
						if (!src.flipY && (15 - src.heightMasks[i].height) != other.heightMasks[i].height)
							return false;
						else if (!other.flipY && src.heightMasks[i].height != (15 - other.heightMasks[i].height))
							return false;
				}
				return true;
			}
			return false;
		}

		public static bool Equal(this Backgrounds.ScrollInfo src, Backgrounds.ScrollInfo other) => src.deform == other.deform && src.parallaxFactor == other.parallaxFactor && src.scrollSpeed == other.scrollSpeed;

		public static RSDKv3.Scene.Entity Clone(this RSDKv3.Scene.Entity src) => new RSDKv3.Scene.Entity(src.type, src.propertyValue, src.xpos, src.ypos);

		public static RSDKv4.Scene.Entity Clone(this RSDKv4.Scene.Entity src) => new RSDKv4.Scene.Entity(src.type, src.propertyValue, src.xpos, src.ypos)
		{
			alpha = src.alpha,
			animation = src.animation,
			animationSpeed = src.animationSpeed,
			direction = src.direction,
			drawOrder = src.drawOrder,
			frame = src.frame,
			inkEffect = src.inkEffect,
			priority = src.priority,
			rotation = src.rotation,
			scale = src.scale,
			state = src.state,
			value0 = src.value0,
			value1 = src.value1,
			value2 = src.value2,
			value3 = src.value3
		};
	}

	public enum EngineVersion
	{
		Invalid,
		V3,
		V4
	}

	[Serializable]
	public class ColInfo
	{
		public Tiles128x128.Block.Tile.Solidities Solidity { get; }
		public TileConfig.CollisionMask CollisionMask { get; }

		public ColInfo()
		{
			Solidity = Tiles128x128.Block.Tile.Solidities.SolidNone;
			CollisionMask = new TileConfig.CollisionMask();
		}

		public ColInfo(Tiles128x128.Block.Tile.Solidities solidity, TileConfig.CollisionMask mask)
		{
			Solidity = solidity;
			CollisionMask = mask;
		}
	}

	public class BitmapInfo
	{
		public int Width { get; }
		public int Height { get; }
		public Size Size => new Size(Width, Height);
		public PixelFormat PixelFormat { get; }
		public int Stride { get; }
		public byte[] Pixels { get; }

		public BitmapInfo(Bitmap bitmap)
		{
			Width = bitmap.Width;
			Height = bitmap.Height;
			switch (bitmap.PixelFormat)
			{
				case PixelFormat.Format1bppIndexed:
				case PixelFormat.Format32bppArgb:
				case PixelFormat.Format4bppIndexed:
				case PixelFormat.Format8bppIndexed:
					PixelFormat = bitmap.PixelFormat;
					break;
				default:
					bitmap = bitmap.To32bpp();
					PixelFormat = PixelFormat.Format32bppArgb;
					break;
			}
			BitmapData bmpd = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat);
			Stride = Math.Abs(bmpd.Stride);
			Pixels = new byte[Stride * Height];
			System.Runtime.InteropServices.Marshal.Copy(bmpd.Scan0, Pixels, 0, Pixels.Length);
			bitmap.UnlockBits(bmpd);
		}

		public BitmapInfo(BitmapInfo source, int x, int y, int width, int height)
		{
			switch (source.PixelFormat)
			{
				case PixelFormat.Format1bppIndexed:
					if (x % 8 != 0)
						throw new FormatException("X coordinate of 1bpp image section must be multiple of 8.");
					if (width % 8 != 0)
						throw new FormatException("Width of 1bpp image section must be multiple of 8.");
					break;
				case PixelFormat.Format4bppIndexed:
					if (x % 2 != 0)
						throw new FormatException("X coordinate of 4bpp image section must be multiple of 2.");
					if (width % 2 != 0)
						throw new FormatException("Width of 4bpp image section must be multiple of 2.");
					break;
			}
			Width = width;
			Height = height;
			PixelFormat = source.PixelFormat;
			switch (PixelFormat)
			{
				case PixelFormat.Format1bppIndexed:
					Stride = width / 8;
					x /= 8;
					break;
				case PixelFormat.Format4bppIndexed:
					Stride = width / 2;
					x /= 2;
					break;
				case PixelFormat.Format8bppIndexed:
					Stride = width;
					break;
				case PixelFormat.Format32bppArgb:
					Stride = width * 4;
					x *= 4;
					break;
			}
			Pixels = new byte[height * Stride];
			for (int v = 0; v < height; v++)
				Array.Copy(source.Pixels, ((y + v) * source.Stride) + x, Pixels, v * Stride, Stride);
		}

		public Bitmap ToBitmap()
		{
			Bitmap bitmap = new Bitmap(Width, Height, PixelFormat);
			BitmapData bmpd = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat);
			byte[] bmpbits = new byte[Height * Math.Abs(bmpd.Stride)];
			for (int y = 0; y < Height; y++)
				Array.Copy(Pixels, y * Stride, bmpbits, y * Math.Abs(bmpd.Stride), Width);
			System.Runtime.InteropServices.Marshal.Copy(bmpbits, 0, bmpd.Scan0, bmpbits.Length);
			bitmap.UnlockBits(bmpd);
			return bitmap;
		}
	}

	public class ImportResult
	{
		public Tiles128x128.Block.Tile[,] Mappings { get; }
		public List<BitmapBits> Art { get; }
		public List<TileConfig.CollisionMask> Collision1 { get; }
		public List<TileConfig.CollisionMask> Collision2 { get; }

		public ImportResult(int width, int height, bool col1, bool col2)
		{
			Mappings = new Tiles128x128.Block.Tile[width, height];
			Art = new List<BitmapBits>();
			if (col1)
				Collision1 = new List<TileConfig.CollisionMask>();
			if (col2)
				Collision2 = new List<TileConfig.CollisionMask>();
		}
	}

	public class AdditionalScene
	{
		public GameConfig.StageList.StageInfo StageInfo { get; }
		public Scene Scene { get; set; }

		public AdditionalScene(GameConfig.StageList.StageInfo info, Scene scene)
		{
			StageInfo = info;
			Scene = scene;
		}
	}

	public class Indexer<TIndex, TResult>
	{
		readonly Func<TIndex, TResult> func;

		public Indexer(Func<TIndex, TResult> func)
		{
			this.func = func;
		}

		public TResult this[TIndex index]
		{
			get => func(index);
		}
	}
}
