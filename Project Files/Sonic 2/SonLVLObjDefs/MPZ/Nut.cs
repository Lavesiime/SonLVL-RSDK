using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class Nut : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(130, 156, 64, 24), -32, -12);
			
			properties[0] = new PropertySpec("Allow Drop", typeof(int), "Extended",
				"If the Nut should drop once it reaches a certain point.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 0x80 }
				},
				(obj) => (obj.PropertyValue & 0x80),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Ground Distance", typeof(int), "Extended",
				"How far down this Nut should be able to go. If Allow Drop is true, this is the height at which the Nut will drop.", null,
				(obj) => (obj.PropertyValue & 0x7F),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x7F) | (byte)(((int)value) & 0x7F)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return "";
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
			int height = (obj.PropertyValue & 0x7f) << 3;
			var bitmap = new BitmapBits(2, height + 1);
			bitmap.DrawLine(6, 0, 0, 0, height); // LevelData.ColorWhite
			return new Sprite(bitmap);
		}
	}
}