using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class CrushBlocks : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			Sprite block;
			Sprite[] frames = new Sprite[5];
			
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
			
			frames[0] = new Sprite(block, -80, -16);
			frames[1] = new Sprite(block, -48, -16);
			frames[2] = new Sprite(block, -16, -16);
			frames[3] = new Sprite(block,  16, -16);
			frames[4] = new Sprite(block,  48, -16);
			
			List<Sprite> sprites = new List<Sprite>();
			
			// Sprite Frame, Y Offset (X Offset is always 0)
			int[] spritedata = new int[] {
				 0, -48,
				 
				 0, -16,
				 1, -16,
				 
				 0,  16,
				 1,  16,
				 2,  16,
				 
				 0,  48,
				 1,  48,
				 2,  48,
				 3,  48,
				 
				 1, -48,
				 2, -48,
				 3, -48,
				 4, -48,
				 
				 2, -16,
				 3, -16,
				 4, -16,
				 
				 3,  16,
				 4,  16,
				 
				 4,  48
			};
			
			for (int i = 0; i < spritedata.Length; i += 2)
				sprites.Add(new Sprite(frames[spritedata[i]], 0, spritedata[i+1]));
			
			sprite = new Sprite(sprites.ToArray());
			
			// Not sure if a debug vis for this object is really necessary.. but it doesn't hurt ig
			BitmapBits bitmap = new BitmapBits(161, 128);
			
			// due to the object's irregular shape, if we want an outline the best way to do that is to use a bunch of DrawLines (i think?)
			int[] points = new int[] {
				32, 0,
				159, 0,
				159, 127,
				128,  127,
				128, 95,
				96, 95,
				96, 63,
				64, 63,
				64, 31,
				32, 31,
				32, 0
			};
			
			for (int i = 0; i < points.Length-2; i += 2)
				bitmap.DrawLine(6, points[i], points[i+1], points[i+2], points[i+3]);
			
			Sprite outline = new Sprite(bitmap, 32, -64);
			
			debug = new Sprite(outline, new Sprite(outline, true, true));
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