using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class VPlatformSmall : R5.VPlatform
	{
		public override Sprite[] GetFrames()
		{
			Sprite[] sprites = new Sprite[2];
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("R5/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 51, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(65, 208, 32, 16), -16, -16);
					break;
				case 'B': // Past
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 170, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(223, 148, 32, 16), -16, -16);
					break;
				case 'C': // Good Future
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 170, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(223, 182, 32, 16), -16, -16);
					break;
				case 'D': // Bad Future
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 170, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(223, 216, 32, 16), -16, -16);
					break;
			}
			
			return sprites;
		}
	}
	
	class VPlatformMedium : R5.VPlatform
	{
		public override bool boundsProperty { get { return true; } }
		
		public override Sprite[] GetFrames()
		{
			Sprite[] sprites = new Sprite[2];
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("R5/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(34, 51, 64, 32), -32, -16);
					sprites[1] = new Sprite(sheet.GetSection(1, 208, 64, 16), -32, -16);
					break;
				case 'B': // Past
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(34, 170, 64, 32), -32, -16);
					sprites[1] = new Sprite(sheet.GetSection(159, 148, 64, 16), -32, -16);
					break;
				case 'C': // Good Future
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(34, 170, 64, 32), -32, -16);
					sprites[1] = new Sprite(sheet.GetSection(159, 182, 64, 16), -32, -16);
					break;
				case 'D': // Bad Future
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(34, 170, 64, 32), -32, -16);
					sprites[1] = new Sprite(sheet.GetSection(159, 216, 64, 16), -32, -16);
					break;
			}
			
			return sprites;
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 2, 3, 5, 6, 8, 9, 11}); }
		}
	}
	
	class VPlatformLarge : R5.VPlatform
	{
		public override Sprite[] GetFrames()
		{
			Sprite[] sprites = new Sprite[2];
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("R5/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 84, 96, 32), -48, -16);
					sprites[1] = new Sprite(sheet.GetSection(1, 191, 96, 16), -48, -16);
					break;
				case 'B': // Past
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[1] = new Sprite(sheet.GetSection(159, 131, 96, 16), -48, -16);
					break;
				case 'C': // Good Future
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[1] = new Sprite(sheet.GetSection(159, 165, 96, 16), -48, -16);
					break;
				case 'D': // Bad Future
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[1] = new Sprite(sheet.GetSection(159, 199, 96, 16), -48, -16);
					break;
			}
			
			return sprites;
		}
	}
	
	abstract class VPlatform : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite[] sprites = new Sprite[2];
		private Sprite[] debug = new Sprite[2];
		
		public virtual bool boundsProperty { get { return false; } } // used by Medium platforms
		public abstract Sprite[] GetFrames();
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = GetFrames();
			
			sprites[0] = frames[0];
			sprites[1] = new Sprite(frames[0], frames[1]);
			
			BitmapBits bitmap = new BitmapBits(2, 97);
			bitmap.DrawLine(6, 0, 0, 0, 96);
			debug[0] = new Sprite(bitmap);
			debug[1] = new Sprite(debug[0], false, true);
			
			properties = new PropertySpec[boundsProperty ? 3 : 2];
			
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which side this platform should start from.", null, new Dictionary<string, int>
				{
					{ "Top", 0 },
					{ "Bottom", 3 }
				},
				(obj) => (obj.PropertyValue / 3) * 3 - ((obj.PropertyValue > 5) ? 6 : 0),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue % 3) + ((obj.PropertyValue > 5) ? 6 : 0) + (int)value));
			
			properties[1] = new PropertySpec("Modifier", typeof(int), "Extended",
				"Which effects this platform should have.", null, new Dictionary<string, int>
				{
					{ "Normal", 0 },
//					{ "Carry Springs", 1 },
					{ "Conveyor", 2 }
				},
				(obj) => obj.PropertyValue % 3,
				(obj, value) => obj.PropertyValue = (byte)(((obj.PropertyValue / 3) * 3) + (int)value));
			
			if (boundsProperty)
			{
				properties[2] = new PropertySpec("Priority", typeof(int), "Extended",
					"Which priority this platform should have.", null, new Dictionary<string, int>
					{
						{ "Normal", 0 },
						{ "XBounds", 6 }
					},
					(obj) => (obj.PropertyValue > 5) ? 6 : 0,
					(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue % 6) + (int)value));
			}
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 2, 3, 5}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			string name;
			switch ((boundsProperty && subtype > 5) ? (subtype - 6) : subtype)
			{
				case 0:
					name = "Start From Top";
					break;
				case 2:
					name = "Start From Top (Conveyor)";
					break;
				case 3:
					name = "Start From Bottom";
					break;
				case 5:
					name = "Start From Bottom (Conveyor)";
					break;
				case 1: // 1 and 4 are normally spring carrying platforms, but v plats just dummy them out
				case 4:
				default:
					name = "Unknown";
					break;
			}
			
			if (boundsProperty && subtype > 5)
				name += " (XBounds)";
			
			return name;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			if (boundsProperty && subtype > 5)
				subtype -= 6;
			
			return sprites[(subtype == 2 || subtype == 5) ? 1 : 0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			switch ((boundsProperty && obj.PropertyValue > 5) ? (obj.PropertyValue - 6) : obj.PropertyValue)
			{
				case 0:
				case 1:
				case 2: return debug[0];
				case 3:
				case 4:
				case 5: return debug[1];
				default: return null;
			}
		}
	}
}