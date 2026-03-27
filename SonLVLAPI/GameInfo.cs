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

	public class LevelInfo
	{
		[IniCollection(IniCollectionMode.NoSquareBrackets, StartIndex = 1)]
		[IniName("ExtraPalette")]
		public List<string> ExtraPalettes { get; set; }
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
}
