using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.OOZ
{
	class Elevator : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[2];

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(127, 1, 128, 24), -64, -12);
			
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How far the Elevator will go.", null,
				(obj) => obj.PropertyValue & 127,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 128) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Start From", typeof(int), "Extended",
				"Where the Elevator will start from.", null, new Dictionary<string, int>
				{
					{ "Bottom", 0 },
					{ "Top", 128 }
				},
				(obj) => obj.PropertyValue & 128,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 127) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 24; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
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
			int dist = (obj.PropertyValue & 127) << 2;
			BitmapBits bitmap = new BitmapBits(2, (2 * dist) + 1);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0, 0, (2 * dist));
			return new Sprite(bitmap, 0, -dist);
		}
	}
}