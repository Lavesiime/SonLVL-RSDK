using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class BlockRowsA : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			Sprite block;
			Sprite[] frames = new Sprite[4];
			
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
			
			frames[0] = new Sprite(block, -32, -32);
			frames[1] = new Sprite(block,   0, -32);
			frames[2] = new Sprite(block, -32,   0);
			frames[3] = new Sprite(block,   0,   0);
			
			block = new Sprite(frames);
			
			sprite = new Sprite(new Sprite(block, -160, -32), new Sprite(block, -16, -32), new Sprite(block, 128, -32), new Sprite(block, 272, -32));
			sprite = new Sprite(new Sprite(sprite, -32, 0), new Sprite(sprite, -144, 64));
			
			BitmapBits bitmap = new BitmapBits(606, 2);
			bitmap.DrawLine(6, 0, 0, 606, 0);
			debug = new Sprite(bitmap, -303, -32);
			debug = new Sprite(debug, new Sprite(debug, false, true));
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
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			return new Rectangle(-32 + obj.X - 48, -32 + obj.Y - 32, 64, 64); // sprite offset - obj position - block offset
		}
	}
}