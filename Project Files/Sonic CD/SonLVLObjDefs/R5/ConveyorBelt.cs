using SonicRetro.SonLVL.API;
using System;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	public class ConveyorBelt : DefaultObjectDefinition
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
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					LevelData.AddPaletteCycle("Conveyor Belt (A)", "R5A_PalCycle.act", 160, 16, 3, 0, 16);
					LevelData.AddPaletteCycle("Conveyor Belt (B)", "R5A_PalCycle.act", 160, 16, 3, 96, 16);
					
					LevelData.AddPaletteCycle("Waterfall", "R5A_PalCycle.act", 192, 16, 6, 16, 16);
					break;
					
				case 'B': // Past
					LevelData.AddPaletteCycle("Conveyor Belt (A)", "R5B_PalCycle.act", 160, 16, 3,  0);
					LevelData.AddPaletteCycle("Conveyor Belt (B)", "R5B_PalCycle.act", 160, 16, 3, 48);
					break;
					
				case 'C': // Good Future
					LevelData.AddPaletteCycle("Conveyor Belt (A)", "R5C_PalCycle.act", 160, 16, 3,  0);
					LevelData.AddPaletteCycle("Conveyor Belt (B)", "R5C_PalCycle.act", 160, 16, 3, 48);
					break;
					
				case 'D': // Bad Future
					LevelData.AddPaletteCycle("Conveyor Belt (A)", "R5D_PalCycle.act", 160, 16, 3,  0);
					LevelData.AddPaletteCycle("Conveyor Belt (B)", "R5D_PalCycle.act", 160, 16, 3, 48);
					break;
			}
			
			base.Init(data);
		}
	}
}
