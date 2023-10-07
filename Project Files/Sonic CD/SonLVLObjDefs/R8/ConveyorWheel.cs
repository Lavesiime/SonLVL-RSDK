using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R8
{
	class ConveyorWheel : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			BitmapBits bitmap = new BitmapBits(128, 128);
			bitmap.DrawRectangle(6, 0, 0, 127, 127); // LevelData.ColorWhite
			sprites[1] = new Sprite(sprites[0], new Sprite(bitmap, -64, -64));
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
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[1];
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			return new Rectangle(obj.X - 8, obj.Y - 8, 16, 16);
		}
	}
}