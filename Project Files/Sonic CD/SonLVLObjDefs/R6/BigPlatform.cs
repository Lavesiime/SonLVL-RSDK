using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R6
{
	class BigPlatform : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("C")) // Using a different sprite for the Good Future
				sprite = new Sprite(LevelData.GetSpriteSheet("R6/Objects4.gif").GetSection(98, 1, 96, 192), -48, -80);
			else
				sprite = new Sprite(LevelData.GetSpriteSheet("R6/Objects4.gif").GetSection(1, 1, 96, 192), -48, -80);
			
			// meh
			// since it's downwards and not up, it's kind of hard to see
			// maybe we could reverse the sprite/debug vis pos?
			// but then it wouldn't be accurate to how it is in-game, since it starts raised
			// not sure what to do...
			
			BitmapBits bitmap = new BitmapBits(96, 192);
			bitmap.DrawRectangle(6, 0, 0, 95, 191); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -48, -80 + 128);
			
			bitmap = new BitmapBits(2, 192);
			bitmap.DrawLine(6, 0, 0, 0, 191); // LevelData.ColorWhite
			debug = new Sprite(debug, new Sprite(bitmap));
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