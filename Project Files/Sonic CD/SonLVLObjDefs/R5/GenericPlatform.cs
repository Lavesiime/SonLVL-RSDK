using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class FPlatform : R5.Platform
	{
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			BitmapBits bitmap = new BitmapBits(1, 0x1C);
			bitmap.DrawLine(6, 0, 0x00, 0, 0x03);
			bitmap.DrawLine(6, 0, 0x08, 0, 0x0B);
			bitmap.DrawLine(6, 0, 0x10, 0, 0x13);
			bitmap.DrawLine(6, 0, 0x18, 0, 0x1B);
			debug = new Sprite(bitmap, 0, 12);
			
			base.Init(data);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
	
	// Used by Dip Platforms and Static Platforms
	class Platform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[6];

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("R5/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 51, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(34, 51, 64, 32), -32, -16);
					sprites[2] = new Sprite(sheet.GetSection(1, 84, 96, 32), -48, -16);
					sprites[3] = new Sprite(sheet.GetSection(65, 208, 32, 16), -16, -16);
					sprites[4] = new Sprite(sheet.GetSection(1, 208, 64, 16), -32, -16);
					sprites[5] = new Sprite(sheet.GetSection(1, 191, 96, 16), -48, -16);
					break;
				case 'B': // Present
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 170, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(34, 170, 64, 32), -32, -16);
					sprites[2] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[3] = new Sprite(sheet.GetSection(223, 148, 32, 16), -16, -16);
					sprites[4] = new Sprite(sheet.GetSection(159, 148, 64, 16), -32, -16);
					sprites[5] = new Sprite(sheet.GetSection(159, 131, 96, 16), -48, -16);
					break;
				case 'C': // Good Future
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 170, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(34, 170, 64, 32), -32, -16);
					sprites[2] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[3] = new Sprite(sheet.GetSection(223, 182, 32, 16), -16, -16);
					sprites[4] = new Sprite(sheet.GetSection(159, 182, 64, 16), -32, -16);
					sprites[5] = new Sprite(sheet.GetSection(159, 165, 96, 16), -48, -16);
					break;
				case 'D': // Bad Future
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 170, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(34, 170, 64, 32), -32, -16);
					sprites[2] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[3] = new Sprite(sheet.GetSection(223, 216, 32, 16), -16, -16);
					sprites[4] = new Sprite(sheet.GetSection(159, 216, 64, 16), -32, -16);
					sprites[5] = new Sprite(sheet.GetSection(159, 199, 96, 16), -48, -16);
					break;
			}
			
			// Setup for conveyor platform sprites
			for (int i = 3; i < 6; i++)
			{
				sprites[i] = new Sprite(sprites[i-3], sprites[i]);
			}
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How large this platform should be.", null, new Dictionary<string, int>
				{
					{ "Small", 0 },
					{ "Medium", 1 },
					{ "Large", 2 }
				},
				(obj) => obj.PropertyValue % 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue - (obj.PropertyValue % 3)) + (int)value));
			
			properties[1] = new PropertySpec("Modifier", typeof(int), "Extended",
				"Which effects this platform should have.", null, new Dictionary<string, int>
				{
					{ "Normal", 0 },
					{ "Conveyor", 3 }
				},
				(obj) => obj.PropertyValue - (obj.PropertyValue % 3),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue % 3) + (int)value));
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
				case 0:
					return "Small";
				case 1:
					return "Medium";
				case 2:
					return "Large";
				case 3:
					return "Small Conveyor";
				case 4:
					return "Medium Conveyor";
				case 5:
					return "Large Conveyor";
				default:
					return "Unknown";
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
			return sprites[(obj.PropertyValue < 6) ? obj.PropertyValue : 0];
		}
	}
}