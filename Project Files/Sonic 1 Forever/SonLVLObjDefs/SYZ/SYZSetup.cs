using SonicRetro.SonLVL.API;
using System;
using System.Drawing;

namespace S1ObjectDefinitions.SYZ
{
	public class SYZSetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			// All we do here is just add the palette cycles to the stage data
			// We don't really have a fancy render to draw, sorry..
			
			// Order of parameters is:
			// - Name: Name of the cycle
			// - File: File name
			// - Index: The in-game starting index (as in where the colours are applied to)
			// - Length: How many colours are in a single frame
			// - Count: How many frames there are in total
			
			// Optional parameters, for when palette file lines are interleaved:
			// - Offset: The offset in the file where the colours for this cycle start (defaults to 0)
			// - Gap: How many extra colours are inbetween each entry (defaults to 0)
			
			LevelData.AddPaletteCycle("Flashing Lights", "SYZ_PalCycle.act", 160, 32, 4);
			
			base.Init(data);
		}
	}
}
