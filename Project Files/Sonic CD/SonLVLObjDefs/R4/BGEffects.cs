using SonicRetro.SonLVL.API;
using System;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	// All we do here is just add the palette cycles to the stage data
	// We don't really have a fancy render to draw, sorry..
	// These objects are all separate scripts in-game, but they're combined into one file here for ease
	
	// Order of parameters is:
	// - Name: Name of the cycle
	// - File: File name
	// - Index: The in-game starting index (as in where the colours are applied to)
	// - Length: How many colours are in a single frame
	// - Count: How many frames there are in total
	
	// Optional parameters, for when palette file lines are interleaved:
	// - Offset: The offset in the file where the colours for this cycle start (defaults to 0)
	// - Gap: How many extra colours are inbetween each entry (defaults to 0)
	
	public class BGEffectsA2 : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Waterfall", "R4A_PalCycle.act", 176, 16, 4);
			base.Init(data);
		}
	}
	
	public class BGEffectsB2 : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			// In-game, the object attempts to load 4 lines, but only uses the first 3 anyways
			// The file only contains 3 lines in the first place, so..
			LevelData.AddPaletteCycle("Waterfall", "R4B_PalCycle.act", 176, 16, 3);
			base.Init(data);
		}
	}
	
	public class BGEffectsC2 : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Waterfall", "R4C_PalCycle.act", 176, 16, 4);
			base.Init(data);
		}
	}
	
	public class BGEffectsD2 : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Waterfall", "R4D_PalCycle.act", 176, 16, 3);
			base.Init(data);
		}
	}
}
