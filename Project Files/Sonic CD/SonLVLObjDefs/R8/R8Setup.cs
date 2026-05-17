using SonicRetro.SonLVL.API;
using System;
using System.Drawing;

namespace SCDObjectDefinitions.R8
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
	
	public class R81ASetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R81A_PalCycle.act", 160, 32, 6);
			base.Init(data);
		}
	}
	
	public class R81BSetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R81B_PalCycle.act", 160, 32, 3);
			base.Init(data);
		}
	}
	
	public class R81CSetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R81C_PalCycle.act", 160, 32, 6);
			base.Init(data);
		}
	}
	
	public class R81DSetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R81D_PalCycle.act", 160, 32, 6);
			base.Init(data);
		}
	}
	
	public class R82ASetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R82A_PalCycle.act", 160, 32, 6);
			base.Init(data);
		}
	}
	
	public class R82BSetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R82B_PalCycle.act", 160, 32, 6);
			base.Init(data);
		}
	}
	
	public class R82CSetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R82C_PalCycle.act", 160, 32, 6);
			base.Init(data);
		}
	}
	
	public class R82DSetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R82D_PalCycle.act", 160, 32, 6);
			base.Init(data);
		}
	}
	
	public class R83CSetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R83C_PalCycle.act", 160, 32, 6);
			base.Init(data);
		}
	}
	
	public class R83DSetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			LevelData.AddPaletteCycle("Flashing Lights", "R83D_PalCycle.act", 160, 32, 6);
			base.Init(data);
		}
	}
}
