using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7 // used by both R7 and R8, just ignore subtype
{
	class InvisibleBarrier : R6.InvisibleBarrier
	{
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return null; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
		}
	}
}

namespace SCDObjectDefinitions.R6
{
	class InvisibleBarrier : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			BitmapBits bitmap = new BitmapBits(32, 30);
			bitmap.DrawRectangle(24, 0, 0, 31, 29); // pink
			sprite = new Sprite(sprite, new Sprite(bitmap, -16, -16));
			
			properties[0] = new PropertySpec("Push Direction", typeof(int), "Extended",
				"Which direction this object should push the player out.", null, new Dictionary<string, int>
				{
					{ "None", 0 },
					{ "Upwards", 1 },
					{ "Downwards", 2 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2}); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				default: return "Normal";
				case 1: return "Push Out Upwards";
				case 2: return "Push Out Downwards";
			}
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
	}
}