using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.OOZ
{
	class Elevator : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private Sprite[] debug = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[3];

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("OOZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(127, 1, 128, 24), -64, -12);
			sprites[1] = new Sprite(sheet.GetSection(141, 26, 64, 24), -32, -12);
			
			BitmapBits overlay = new BitmapBits(128, 24);
			overlay.DrawRectangle(6, 0, 0, 127, 23); // LevelData.ColorWhite
			debug[0] = new Sprite(overlay, -64, -12);
			
			overlay = new BitmapBits(64, 24);
			overlay.DrawRectangle(6, 0, 0, 63, 23); // LevelData.ColorWhite
			debug[1] = new Sprite(overlay, -32, -12);

			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How far, in pixels, the Elevator will go.", null,
				(obj) => (obj.PropertyValue & 0x7f) << 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x7f) | ((int)value >> 3)));
			
			properties[1] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which direction the Elevator will start from.", null, new Dictionary<string, int>
				{
					{ "Bottom", 0 },
					{ "Top", 0x80 }
				},
				(obj) => obj.PropertyValue & 0x80,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | (int)value));
			
			properties[2] = new PropertySpec("Size", typeof(int), "Extended",
				"How large this Elevator is.", null, new Dictionary<string, int>
				{
					{ "Large", 0 },
					{ "Small", 1 }
				},
				(obj) => ((V4ObjectEntry)obj).Frame == 0 ? 0 : 1,
				(obj, value) => ((V4ObjectEntry)obj).Frame = (byte)value);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x18, 0x98}); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x18; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype < 0x80) ? "Start From Bottom" : "Start From Top";
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
			int dist = (obj.PropertyValue & 0x7f) << 2;
			if ((obj.PropertyValue & 0x80) == 0)
				dist *= -1;
			
			return new Sprite(sprites[((V4ObjectEntry)obj).Frame == 0 ? 0 : 1], 0, -dist + 4);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int dist = (obj.PropertyValue & 0x7f) << 2;
			BitmapBits bitmap = new BitmapBits(2, (2 * dist) + 1);
			bitmap.DrawLine(6, 0, 0, 0, (2 * dist)); // LevelData.ColorWhite
			
			if ((obj.PropertyValue & 0x80) == 0)
				dist *= -1;
			
			return new Sprite(new Sprite(bitmap, 0, -Math.Abs(dist) + 4), new Sprite(debug[((V4ObjectEntry)obj).Frame == 0 ? 0 : 1], 0, dist + 4));
		}
	}
}