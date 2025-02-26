using System.Collections.Generic;

namespace SonicRetro.SonLVL.API
{
	public class GameInfo
	{
		public string EXEFile { get; set; }
		public string DataFile { get; set; }
		public EngineVersion RSDKVer { get; set; }
		public bool IsV5U { get; set; }
		[IniCollection(IniCollectionMode.IndexOnly)]
		public Dictionary<string, LevelInfo> Levels { get; set; }

		public static GameInfo Load(string filename) => IniSerializer.Deserialize<GameInfo>(filename);

		public void Save(string filename) => IniSerializer.Serialize(this, filename);
	}

	public class LevelInfo
	{
		[IniName("folder")]
		public string folder { get; set; }
		[IniName("id")]
		public string id { get; set; }
		[IniName("header")]
		public bool header { get; set; }
	}
}
