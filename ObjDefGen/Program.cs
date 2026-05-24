using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ObjDefGen
{
	class Program
	{
		static readonly Regex sheetregex = new Regex(@"LoadSpriteSheet\(""(.+?)""\)", RegexOptions.Compiled);
		static readonly Regex frameregex = new Regex(@"SpriteFrame\((-?[0-9]+, ?-?[0-9]+, ?[0-9]+, ?[0-9]+, ?[0-9]+, ?[0-9]+)\)", RegexOptions.Compiled);
		static void Main(string[] args)
		{
			string folder;
			if (args.Length > 0)
				folder = args[0];
			else
			{
				Console.Write("Folder: ");
				folder = Console.ReadLine().Trim('"');
			}
			folder = Path.GetFullPath(folder);
			Dictionary<string, Dictionary<string, ObjectData>> defs = new Dictionary<string, Dictionary<string, ObjectData>>();
			foreach (var fn in Directory.EnumerateFiles(folder, "*.txt", SearchOption.AllDirectories))
			{
				string[] lines = File.ReadAllLines(fn);
				int ln = Array.FindIndex(lines, a => a.StartsWith("sub ObjectStartup") || a.StartsWith("event ObjectStartup"));
				if (ln > -1)
				{
					int end = Array.FindIndex(lines, ln, a => a.StartsWith("end sub") || a.StartsWith("end event"));
					if (end == -1) continue;
					string sheet = null;
					for (; ln < end; ln++)
					{
						Match m = sheetregex.Match(lines[ln]);
						if (m.Success)
							sheet = m.Groups[1].Value;
						else if (sheet != null)
						{
							m = frameregex.Match(lines[ln]);
							if (m.Success)
							{
								string scrpath = fn.Substring(folder.Length + 1).Replace(Path.DirectorySeparatorChar, '/');
								string fol = scrpath.Remove(scrpath.IndexOf('/'));
								if (!defs.ContainsKey(fol))
									defs.Add(fol, new Dictionary<string, ObjectData>());
								defs[fol].Add(scrpath, new ObjectData() { Sheet = sheet, Frame = new SpriteFrame(m.Groups[1].Value) });
								break;
							}
						}
					}
				}
			}
			if (defs.Count > 0)
				foreach (var item in defs)
					IniSerializer.Serialize(item.Value, Path.Combine(folder, item.Key + ".ini"));
		}
	}
}
