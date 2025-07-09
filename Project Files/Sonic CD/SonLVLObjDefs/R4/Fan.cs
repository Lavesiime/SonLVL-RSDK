using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class Fan : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[6];
		
		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				case 'B': // Past
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(163, 35, 32, 16), -16, -8);
					sprites[1] = new Sprite(sheet.GetSection(229, 1, 24, 16), -12, -8);
					sprites[2] = new Sprite(sheet.GetSection(239, 136, 16, 32), -8, -16);
					sprites[3] = new Sprite(sheet.GetSection(222, 169, 16, 24), -8, -12);
					sprites[4] = new Sprite(sheet.GetSection(239, 136, 16, 32), -8, -16);
					sprites[5] = new Sprite(sheet.GetSection(222, 169, 16, 24), -8, -12);
					break;
				case 'C': // Good Future
					sheet = LevelData.GetSpriteSheet("R4/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(51, 67, 32, 16), -16, -8);
					sprites[1] = new Sprite(sheet.GetSection(51, 84, 32, 16), -16, -8);
					sprites[2] = new Sprite(sheet.GetSection(50, 101, 16, 32), -8, -16);
					sprites[3] = new Sprite(sheet.GetSection(67, 101, 16, 32), -8, -16);
					sprites[4] = new Sprite(sheet.GetSection(50, 101, 16, 32), -8, -16);
					sprites[5] = new Sprite(sheet.GetSection(67, 101, 16, 32), -8, -16);
					break;
				case 'D': // Bad Future
					sheet = LevelData.GetSpriteSheet("R4/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(62, 228, 32, 16), -16, -8);
					sprites[1] = new Sprite(sheet.GetSection(94, 228, 32, 16), -16, -8);
					sprites[2] = new Sprite(sheet.GetSection(76, 144, 16, 32), -8, -16);
					sprites[3] = new Sprite(sheet.GetSection(93, 144, 16, 32), -8, -16);
					sprites[4] = new Sprite(sheet.GetSection(76, 144, 16, 32), -8, -16);
					sprites[5] = new Sprite(sheet.GetSection(93, 144, 16, 32), -8, -16);
					break;
			}
			
			sprites[4].Flip(true, false);
			sprites[5].Flip(true, false);
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Fan should behave.", null, new Dictionary<string, int>
				{
					{ "Button Triggered", 0 },
					{ "Always On", 1 }
				},
				(obj) => (obj.PropertyValue & 1),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (int)value));
			
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction this Fan should face.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Right", 2 },
					{ "Left", 4 },
				},
				(obj) => (obj.PropertyValue & ~1),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 1) | (int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Upwards (Button Enabled)";
				case 1: return "Upwards (Always On)";
				case 2: return "Right (Button Enabled)";
				case 3: return "Right (Always On)";
				case 4: return "Left (Button Enabled)";
				case 5: return "Left (Always On)";
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return (obj.PropertyValue < 6) ? sprites[obj.PropertyValue] : new Sprite(LevelData.UnknownSprite);
		}
	}
}