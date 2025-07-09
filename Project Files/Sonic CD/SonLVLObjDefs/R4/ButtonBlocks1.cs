using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class ButtonBlocks1 : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			Sprite block;
			Sprite[] frames = new Sprite[6];
			
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
			
			frames[0] = new Sprite(block,   0, -64);
			frames[1] = new Sprite(block,   0, -32);
			frames[2] = new Sprite(block, -32,   0);
			frames[3] = new Sprite(block,   0,   0);
			frames[4] = new Sprite(block, -32,  32);
			frames[5] = new Sprite(block,   0,  32);
			
			sprite = new Sprite(frames);
			
			BitmapBits bitmap = new BitmapBits(65, 193);
			
			// due to the object's irregular shape, if we want an outline the best way to do that is to use a bunch of DrawLines (i think?)
			int[] points = new int[] {
				 0, 63,
				31, 63,
				31,  0,
				63,  0,
				63, 127,
				 0, 127,
				 0, 63
			};
			
			// 128px up
			
			for (int i = 0; i < points.Length-2; i += 2)
				bitmap.DrawLine(6, points[i], points[i+1], points[i+2], points[i+3]);
			
			// Movement line
			bitmap.DrawLine(6, 32, 96, 32, 96 + 128);
			
			debug = new Sprite(bitmap, -32, -128 - 64);
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