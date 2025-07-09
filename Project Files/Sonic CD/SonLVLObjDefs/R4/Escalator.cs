using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class Escalator : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				default:
				case 'A': // Present
				case 'B': // Past
					sprite = new Sprite(LevelData.GetSpriteSheet("R4/Objects.gif").GetSection(130, 1, 32, 32), -16, -16);
					break;
				case 'C': // Good Future
					sprite = new Sprite(LevelData.GetSpriteSheet("R4/Objects3.gif").GetSection(1, 150, 32, 32), -16, -16);
					break;
				case 'D': // Bad Future
					sprite = new Sprite(LevelData.GetSpriteSheet("R4/Objects3.gif").GetSection(1, 183, 32, 32), -16, -16);
					break;
			}
			
			BitmapBits bitmap = new BitmapBits(177, 177); // (160 travel dist + 16 sprite width + 1)
			bitmap.DrawLine(6, 0, 176, 160, 16); // LevelData.ColorWhite
			bitmap.DrawRectangle(6, 144, 0, 31, 31); // LevelData.ColorWhite
			debug = new Sprite(bitmap, 0, -177);
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
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}