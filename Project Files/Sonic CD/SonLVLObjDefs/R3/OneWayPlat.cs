using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R3
{
	class OneWayPlat : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("R3/Objects.gif").GetSection(165, 1, 64, 16), -96, -8);
			
			// tagging this area withLevelData.ColorWhite
			BitmapBits bitmap = new BitmapBits(64 + 32 + 1, 17);
			bitmap.DrawRectangle(6, 32, 0, 63, 15);
			bitmap.DrawLine(6, 0, 8, 63, 8);
			debug = new Sprite(bitmap, -64, -8);
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