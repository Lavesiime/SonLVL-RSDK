using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class ArrowLauncher : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[4];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(1, 35, 32, 16), -16, -8);
			sprites[1] = new Sprite(sprites[0], true, false);
			
			sprites[2] = new Sprite(sprites[0], new Sprite(sheet.GetSection(1, 69, 32, 7), -16 + 64, -4));
			sprites[3] = new Sprite(sprites[2], true, false);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Arrow Launcher is facing.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX) ? 1 : 0,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
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
			return sprites[(((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX) ? 3 : 2];
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			// Use the bounds of just the launcher, don't include the arrow
			Rectangle bounds = sprites[0].Bounds;
			bounds.Offset(obj.X, obj.Y);
			return bounds;
		}
	}
}