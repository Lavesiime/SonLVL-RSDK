using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class FallingBlocks : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet;
			Sprite[] frames = new Sprite[4];
			int sprx1 = 0, sprx2 = 0, spry = 0;
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
					sprx1 = 163;
					sprx2 = 163;
					spry = 1;
					break;
				case 'B': // Past
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					sprx1 = 1;
					sprx2 = 34;
					spry = 157;
					break;
				case 'C': // Good Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					sprx1 = 1;
					sprx2 = 1;
					spry = 190;
					break;
				case 'D': // Bad Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					sprx1 = 1;
					sprx2 = 1;
					spry = 223;
					break;
			}
			
			frames[0] = new Sprite(sheet.GetSection(sprx1, spry, 32, 32), -16, -64);
			frames[1] = new Sprite(sheet.GetSection(sprx1, spry, 32, 32), -16, -32);
			frames[2] = new Sprite(sheet.GetSection(sprx2, spry, 32, 32), -16,   0);
			frames[3] = new Sprite(LevelData.GetSpriteSheet("Global/Items3.gif").GetSection(50, 100, 32, 32), -16, 32); // (downward spikes frame)
			
			sprite = new Sprite(frames);
			
			Rectangle bounds = sprite.Bounds;
			BitmapBits outline = new BitmapBits(bounds.Size);
			outline.DrawRectangle(6, 0, 0, bounds.Width - 1, bounds.Height - 1); // LevelData.ColorWhite
			debug = new Sprite(outline, bounds.X, bounds.Y);
			
			BitmapBits bitmap = new BitmapBits(1, 32);
			
			for (int i = 0; i < 32; i += 6)
				bitmap.DrawLine(6, 0, i, 0, i + 3);
			debug = new Sprite(debug, new Sprite(bitmap, 0, 64));
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