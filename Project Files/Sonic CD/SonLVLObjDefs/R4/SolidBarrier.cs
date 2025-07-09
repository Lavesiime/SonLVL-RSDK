using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class SolidBarrier : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			Sprite block;
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects.gif").GetSection(163, 1, 32, 32));
					break;
				case 'B': // Past
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 157, 32, 32));
					break;
				case 'C': // Good Future
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 190, 32, 32));
					break;
				case 'D': // Bad Future
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 223, 32, 32));
					break;
			}
			
			List<Sprite> sprites = new List<Sprite>();
			
			for (int offy = -64; offy < 64; offy += 32)
			{
				for (int offx = -64; offx < 64; offx += 32) sprites.Add(new Sprite(block, offx, offy));
			}
			
			sprite = new Sprite(sprites.ToArray());
			
			BitmapBits bitmap = new BitmapBits(128, 128);
			bitmap.DrawRectangle(6, 0, 0, 127, 127); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -64, -64 + 64);
			
			properties[0] = new PropertySpec("Start", typeof(int), "Extended",
				"How this door should start.", null, new Dictionary<string, int> // this is kind of iffy... but it's cleaner than all the other more in-depth stuff i can think of
				{
					{ "Start Closed", 0 },
					{ "Start Open", 1 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Start Closed";
				case 1: return "Start Open";
				default: return "Unknown";
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
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue == 1) ? debug : null;
		}
	}
}