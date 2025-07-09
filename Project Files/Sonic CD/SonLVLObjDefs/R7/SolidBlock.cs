using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class SolidBlock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[9];
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("C")) // Using a different sprite for the Good Future
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(117, 219, 32, 32), -16, -16);
			else
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(1, 1, 32, 32), -16, -16);
			
			sprites[1] = new Sprite(new Sprite(sprites[0], -16, 0), new Sprite(sprites[0], 16, 0));
			sprites[2] = new Sprite(new Sprite(sprites[0], -32, 0), sprites[0], new Sprite(sprites[0], 32, 0));
			sprites[3] = new Sprite(new Sprite(sprites[0], -48, 0), new Sprite(sprites[0], -16, 0), new Sprite(sprites[0], 16, 0), new Sprite(sprites[0], 48, 0));
			
			sprites[4] = new Sprite(new Sprite(sprites[0], 0, -16), new Sprite(sprites[0], 0, 16));
			sprites[5] = new Sprite(new Sprite(sprites[0], 0, -32), sprites[0], new Sprite(sprites[0], 0, 32));
			sprites[6] = new Sprite(new Sprite(sprites[0], 0, -48), new Sprite(sprites[0], 0, -16), new Sprite(sprites[0], 0, 16), new Sprite(sprites[0], 0, 48));
			
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(6, 0, 0, 31, 31); // LevelData.ColorWhite
			sprites[7] = new Sprite(bitmap, -16, -16);
			
			sprites[8] = new Sprite();
			
			properties[0] = new PropertySpec("Formation", typeof(int), "Extended",
				"The formation of the blocks.", null, new Dictionary<string, int>
				{
					{ "Single Block", 0 },
					{ "Two Blocks (Horizontal)", 1 },
					{ "Three Blocks (Horizontal)", 2 },
					{ "Four Blocks (Horizontal)", 3 },
					{ "Two Blocks (Vertical)", 4 },
					{ "Three Blocks (Vertical)", 5 },
					{ "Four Blocks (Vertical)", 6 },
					{ "Invisible Block", 7 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return properties[0].Enumeration.GetKey(subtype);
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype > 6) ? 8 : subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue > 6) ? 8 : obj.PropertyValue];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue > 6) ? sprites[7] : null;
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			return (obj.PropertyValue > 6) ? new Rectangle(obj.X - 16, obj.Y - 16, 32, 32) : Rectangle.Empty;
		}
	}
}