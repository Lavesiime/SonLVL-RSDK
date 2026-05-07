using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SonicRetro.SonLVL.API
{
	public class GameInfo
	{
		public string EXEFile { get; set; }
		[DefaultValue(ExtractionState.Default)]
		public ExtractionState ExtractionState { get; set; }
		public string DataFile { get; set; }
		public EngineVersion RSDKVer { get; set; }
		public bool IsV5U { get; set; }
		public string BaseMod { get; set; }

		[IniCollection(IniCollectionMode.IndexOnly)]
		public Dictionary<string, LevelInfo> Levels { get; set; }

		public static GameInfo Load(string filename) => IniSerializer.Deserialize<GameInfo>(filename);

		public void Save(string filename) => IniSerializer.Serialize(this, filename);
	}

	// Represents the "Extraction State" of the user's Data.rsdk archive
	public enum ExtractionState
	{
		// Either the project file hasn't been loaded for the first time yet, or the user is using a Data Folder
		Default,

		// The user chose to extract their Data.rsdk
		Extracted,

		// The user refused to extract their Data.rsdk
		Packed
	}

	public class LevelInfo
	{
		[IniCollection(IniCollectionMode.NoSquareBrackets, StartIndex = 1)]
		[IniName("ExtraPalette")]
		public List<string> ExtraPalettes { get; set; }

		[IniCollection(IniCollectionMode.NoSquareBrackets, StartIndex = 1)]
		[IniName("PaletteCycle")]
		public PaletteCycleInfo[] PaletteCycles { get; set; }
	}

	[TypeConverter(typeof(PaletteCycleInfoConverter))]
	public class PaletteCycleInfo
	{
		// The name of the palette cycle
		public string Name;

		// The file name
		public string File;

		// Where the palette file gets applied to in the palette bank (ie 96 for the first StageConfig palette row)
		public int Index;

		// How long each "frame" is (ie in CD, most frames are either 16 or 32 colours long)
		public int Length;

		// How many frames there are in the file (so file length divided by Length in most cases)
		public int Count;

		// The offset to the beginning of the colours, within the file
		public int Offset = 0;

		// How many extra colours are between each entry
		// (ie for cases where odd numbered lines are for one area and even numbered lines are for another area)
		public int Gap = 0;

		public PaletteCycleInfo(string data)
		{
			string[] split = data.Split(':');
			Name = split[0];
			File = split[1];
			Index = int.Parse(split[2]);
			Length = int.Parse(split[3]);
			Count = int.Parse(split[4]);

			if (split.Length > 6)
			{
				Offset = int.Parse(split[5]);
				Gap = int.Parse(split[6]);
			}
		}

		public override string ToString() => $"{File}:{Index}:{Length}:{Count}" + (Gap > 0 ? $":{Gap}" : "");
	}

	public class PaletteCycleInfoConverter : TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(PaletteCycleInfo))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is PaletteCycleInfo info)
				return info.ToString();
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
				return new PaletteCycleInfo(st);
			return base.ConvertFrom(context, culture, value);
		}
	}
}
