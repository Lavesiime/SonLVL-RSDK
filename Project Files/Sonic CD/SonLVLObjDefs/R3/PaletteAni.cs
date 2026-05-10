using SonicRetro.SonLVL.API;
using System;
using System.Drawing;

namespace SCDObjectDefinitions.R3
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
	
	public class PaletteAni_A : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R3A_PalCycle.act", 160, 32, 6);
			base.Init(data);
		}
	}
	
	public class PaletteAni_C : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R3C_PalCycle.act", 160, 32, 3);
			base.Init(data);
		}
	}
	
	public class PaletteAni_D : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R3D_PalCycle.act", 160, 32, 6);
			base.Init(data);
		}
	}
}
