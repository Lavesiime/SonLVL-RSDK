using SonicRetro.SonLVL.API;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SCDObjectDefinitions.R4
{
	class RotatingSpikes2 : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[3];
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
			sprites[2] = new Sprite(sheet.GetSection(221, 53, 32, 32), -16, -16); // spike
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 4; i++)
			{
				double angle = ((i * 16) / 256.0) * Math.PI;
				sprs.Add(new Sprite(sprites[2], (int)(Math.Cos(angle) * 64), (int)(Math.Sin(angle) * 64)));
			}
			
			sprites[0] = new Sprite(sprs);
			
			// was originally going to do this for sprites[1], but it turns out all the spikes are at a slightly different interval too
//			sprs.Reverse();
			
			sprs = new List<Sprite>();
			for (int i = 0; i < 4; i++)
			{
				double angle = ((64 - (i * 16)) / 256.0) * Math.PI;
				sprs.Add(new Sprite(sprites[2], (int)(Math.Cos(angle) * 64), (int)(Math.Sin(angle) * 64)));
			}
			sprites[1] = new Sprite(sprs);
			
			int length = 64;
			BitmapBits bitmap = new BitmapBits(2 * length + 1, 2 * length + 1);
			bitmap.DrawCircle(6, length, length, length);
			debug = new Sprite(bitmap, -length, -length);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction these Spikes should rotate.", null, new Dictionary<string, int>
				{
					{ "Clockwise", 2 },
					{ "Counter-Clockwise", 0xfe }
				},
				(obj) => (obj.PropertyValue < 0x80) ? 2 : 0xfe, // (see below note on why we chose these values)
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {2, 0xfe} ); } // well really it only matters if it's greater or less than 128, but these values are what the game uses
		}
		
		public override byte DefaultSubtype
		{
			get { return 2; } // this doesn't really matter lol
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype < 0x80) ? "Spin Clockwise" : "Spin Counter-Clockwise";
		}

		public override Sprite Image
		{
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[2];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue < 0x80) ? 0 : 1];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}