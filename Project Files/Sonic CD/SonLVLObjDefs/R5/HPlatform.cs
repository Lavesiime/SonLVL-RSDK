using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class HPlatformSmall : R5.HPlatform
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
	
	class HPlatformMedium1 : R5.HPlatformMedium2
	{
		// Same tihng as H Platform 2, just with a different distance
		public override int distance { get { return 64; } }
	}
	
	class HPlatformMedium2 : R5.HPlatform
	{
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
	}
	
	class HPlatformLarge : R5.HPlatform
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
	
	abstract class HPlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[2];
		private Sprite[] debug = new Sprite[2];
		
		public virtual int distance { get { return 96; } }
		public abstract Sprite[] GetFrames();
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = GetFrames();
			
			sprites[0] = frames[0];
			sprites[1] = new Sprite(frames[0], frames[1]);
			
			BitmapBits bitmap = new BitmapBits(distance + 1, 2);
			bitmap.DrawLine(6, 0, 0, distance, 0);
			debug[0] = new Sprite(bitmap);
			debug[1] = new Sprite(debug[0], true, false);
			
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which side this platform should start from.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 3 }
				},
				(obj) => (obj.PropertyValue / 3) * 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue % 3) + (int)value));
			
			properties[1] = new PropertySpec("Modifier", typeof(int), "Extended",
				"Which effects this platform should have.", null, new Dictionary<string, int>
				{
					{ "Normal", 0 },
					{ "Carry Springs", 1 },
					{ "Conveyor", 2 }
				},
				(obj) => obj.PropertyValue % 3,
				(obj, value) => obj.PropertyValue = (byte)(((obj.PropertyValue / 3) * 3) + (int)value));
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
				case 0: return "Start From Left";
				case 1: return "Start From Left (W/ Spring)";
				case 2: return "Start From Left (Conveyor)";
				case 3: return "Start From Right";
				case 4: return "Start From Right (W/ Spring)";
				case 5: return "Start From Right (Conveyor)";
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 2 || subtype == 5) ? 1 : 0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 2 || obj.PropertyValue == 5) ? 1 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			switch (obj.PropertyValue)
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