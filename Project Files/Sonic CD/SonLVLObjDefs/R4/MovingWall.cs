using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class MovingWall : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		// the obj uses prop val, but i don't think it's supposed to be set from the editor?
		
		public override void Init(ObjectData data)
		{
			Sprite block;
			Sprite[] frames = new Sprite[8];
			
			// this obj is only ever used in TTZ2 Present anyways, but let's do all this time period stuff since that's what the base game does too
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects.gif").GetSection(163, 1, 32, 32));
					break;
				case 'B': // Past
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 157, 32, 32));
					break;
				case 'C': // Good Future
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 190, 32, 32));
					break;
				case 'D': // Bad Future
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 223, 32, 32));
					break;
			}
			
			frames[0] = new Sprite(block, -32, -64);
			frames[1] = new Sprite(block, 0, -64);
			frames[2] = new Sprite(block, -32, -32);
			frames[3] = new Sprite(block, 0, -32);
			frames[4] = new Sprite(block, -32, 0);
			frames[5] = new Sprite(block, 0, 0);
			frames[6] = new Sprite(block, -32, 32);
			frames[7] = new Sprite(block, 0, 32);
			
			sprite = new Sprite(frames);
			
			BitmapBits bitmap = new BitmapBits(224, 129);
			bitmap.DrawRectangle(6, 160, 0, 63, 127); // LevelData.ColorWhite
			bitmap.DrawLine(6, 0, 64, 192, 64); // LevelData.ColorWhite
			debug = new Sprite(bitmap, 0, -64);
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