using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// this file just hosts basic renders for single-sprite objects that just need time period checks

namespace SCDObjectDefinitions.R5
{
	class BossSpikes : R5.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("D")) // Using a different sprite in the Bad Future
				return new Sprite(LevelData.GetSpriteSheet("R5/Objects2.gif").GetSection(76, 191, 48, 64), -24, -32); // Bad Future frame
			else
				return new Sprite(LevelData.GetSpriteSheet("R5/Objects2.gif").GetSection(125, 191, 48, 64), -24, -32); // Good Future frame
		}
	}
	
	class BreakWall : R5.Generic
	{
		public override Sprite GetFrame()
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					return new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(223, 141, 32, 96), -16, -48);
				case 'B': // Past
					return new Sprite(LevelData.GetSpriteSheet("R5/Objects3.gif").GetSection(132, 1, 32, 96), -16, -48);
				case 'C': // Good Future
					return new Sprite(LevelData.GetSpriteSheet("R5/Objects3.gif").GetSection(66, 1, 32, 96), -16, -48);
				case 'D': // Bad Future
					return new Sprite(LevelData.GetSpriteSheet("R5/Objects3.gif").GetSection(99, 1, 32, 96), -16, -48);
			}
		}
	}
	
	class CBSwitch : R5.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("B")) // Using a different sprite in the Past
				return new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(1, 225, 32, 28), -4, -16); // Past frame
			else
				return new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(1, 18, 32, 28), -4, -16); // Present/Future frame
		}
	}
	
	class Stalactite : R5.Generic
	{
		public override Sprite GetFrame()
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				case 'C': // half sure that the GF should use the frame next to the flowers? the base game makes the present and GF use the frames though, so let's just keep it like this..
				default:
					return new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(172, 207, 16, 48), -8, -24);
				case 'B': // Past
					return new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(189, 207, 16, 48), -8, -24);
				case 'D': // Bad Future
					return new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(155, 207, 16, 48), -8, -24); ;
			}
		}
	}
	
	abstract class Generic : ObjectDefinition
	{
		private Sprite sprite;
		
		public abstract Sprite GetFrame();
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
	}
}