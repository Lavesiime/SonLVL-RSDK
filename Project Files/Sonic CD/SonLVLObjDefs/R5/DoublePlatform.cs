using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class DoublePlatform : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[2];
			BitmapBits sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
			
			if (LevelData.StageInfo.folder.EndsWith("D")) // In the Bad Future, we use different sprites
			{
				sprites[0] = new Sprite(sheet.GetSection(131, 98, 64, 32), -32, -16);
				sprites[1] = new Sprite(sheet.GetSection(131, 98, 64, 32),  96, -16);
			}
			else
			{
				sprites[0] = new Sprite(sheet.GetSection(66, 98, 64, 32), -32, -16);
				sprites[1] = new Sprite(sheet.GetSection(66, 98, 64, 32),  96, -16);
			}
			
			sprite = new Sprite(sprites);
			sprite.Offset(-32, 0);
			
			BitmapBits bitmap = new BitmapBits(129, 193);
			/*
			int last = -1;
			for (int i = 0; i < 128; i++)
			{
				int point = ((int)(Math.Cos(((i * 4)/256.0) * Math.PI) * 512 * 0x3000) >> 16) + 96;
				if (last != -1)
					bitmap.DrawLine(6, i - 1, last, i, point);
				last = point;
			}
			*/
			bitmap.DrawGraphX(6, 0, 127, 0, (x) => ((int)(Math.Cos(((x * 4)/256.0) * Math.PI) * 512 * 0x3000) >> 16) + 96);
			debug = new Sprite(bitmap, 0, -192 / 2);
			debug = new Sprite(debug, new Sprite(debug, true, false));
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
			// Let's use the bounds of only one of the platforms, instead of both (which would include the gap between them)
			return new Rectangle(-32 + obj.X - 32, -16 + obj.Y, 64, 32); // x: sprite offset - obj.X - platform offset
		}
	}
}