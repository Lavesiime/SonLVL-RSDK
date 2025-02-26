using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public class Settings
	{
		private static string filename;

		[DefaultValue(true)]
		public bool ShowHUD { get; set; }
		[IniCollection(IniCollectionMode.SingleLine, Format = "|")]
		public List<string> MRUList { get; set; }
		[IniName("RecentMod")]
		[IniCollection(IniCollectionMode.NoSquareBrackets, StartIndex = 1)]
		public List<MRUModItem> RecentMods { get; set; }
		public bool ShowGrid { get; set; }
		[IniIgnore]
		public Color GridColor { get; set; }
		[IniName("GridColor")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Color? GridColorInternal
		{
			get => GridColor;
			set => GridColor = value ?? Color.Red;
		}
		[IniName("ObjectGridSize")]
		public int ObjectGridSizeInternal
		{
			get => 1 << ObjectGridSize;
			set => ObjectGridSize = (byte)Math.Max(0, Math.Min(8, Math.Log(value, 2)));
		}
		[IniIgnore]
		public byte ObjectGridSize { get; set; }
		public string Username { get; set; }
		public Color BackgroundColor { get; set; }
		[DefaultValue(true)]
		public bool TransparentBackgroundExport { get; set; }
		[DefaultValue(true)]
		public bool IncludeObjectsFG { get; set; }
		public bool HideDebugObjectsExport { get; set; }
		public bool UseHexadecimalIndexesExport { get; set; }
		public bool ExportArtCollisionPriority { get; set; }
		public bool ObjectsAboveHighPlane { get; set; }
		[DefaultValue(true)]
		public bool ViewLowPlane { get; set; }
		[DefaultValue(true)]
		public bool ViewHighPlane { get; set; }
		public CollisionPath ViewCollision { get; set; }
		public bool ViewAngles { get; set; }
		[DefaultValue("1x")]
		public string ZoomLevel { get; set; }
		public Tab CurrentTab { get; set; }
		public ArtTab CurrentArtTab { get; set; }
		public bool SwitchChunkBlockMouseButtons { get; set; }
		public WindowMode WindowMode { get; set; }
		[DefaultValue(true)]
		public bool EnableDraggingPalette { get; set; }
		[DefaultValue(true)]
		public bool EnableDraggingTiles { get; set; }
		[DefaultValue(true)]
		public bool EnableDraggingChunks { get; set; }

		public static Settings Load()
		{
			filename = Path.Combine(Application.StartupPath, "SonLVL.ini");
			if (File.Exists(filename))
				return IniSerializer.Deserialize<Settings>(filename);
			else
			{
				Settings result = new Settings();
				result.ShowHUD = true;
				result.MRUList = new List<string>();
				result.ShowGrid = false;
				result.GridColor = Color.Red;
				result.Username = null;
				result.BackgroundColor = Color.FromArgb(160, 0, 0, 192);
				result.TransparentBackgroundExport = true;
				result.ViewLowPlane = result.ViewHighPlane = true;
				result.ZoomLevel = "1x";
				result.EnableDraggingPalette = true;
				result.EnableDraggingTiles = true;
				result.EnableDraggingChunks = true;
				return result;
			}
		}

		public void Save()
		{
			IniSerializer.Serialize(this, filename);
		}
	}

	[TypeConverter(typeof(MRUModItemConverter))]
	public class MRUModItem
	{
		public string Name { get; set; }
		public string INIPath { get; set; }
		public string ModPath { get; set; }

		public MRUModItem(string name, string exepath, string modpath)
		{
			Name = name;
			INIPath = exepath;
			ModPath = modpath;
		}

		public MRUModItem(string data)
		{
			string[] split = data.Split('|');
			Name = split[0];
			INIPath = split[1];
			if (split.Length > 2)
				ModPath = split[2];
		}

		public override string ToString()
		{
			if (ModPath == null)
				return $"{Name}|{INIPath}";
			return $"{Name}|{INIPath}|{ModPath}";
		}
	}

	public class MRUModItemConverter : TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(MRUModItem))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is MRUModItem sf)
				return sf.ToString();
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string st)
				return new MRUModItem(st);
			return base.ConvertFrom(context, culture, value);
		}
	}


	public enum CollisionPath
	{
		None,
		Path1,
		Path2
	}

	public enum Tab
	{
		Objects,
		Foreground,
		Background,
		Art,
		Palette,
		Settings
	}

	public enum ArtTab
	{
		Chunks,
		Tiles,
		Solids
	}

	public enum WindowMode
	{
		Normal,
		Maximized,
		Fullscreen
	}
}
