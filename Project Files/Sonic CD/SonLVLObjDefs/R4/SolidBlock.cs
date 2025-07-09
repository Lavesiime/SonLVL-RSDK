using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class SolidBlock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[6];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet;
			Sprite[] frames = new Sprite[12];
			int sprx1 = 0, sprx2 = 0, spry = 0;
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
					sprx1 = 163;
					sprx2 = 196;
					spry = 1;
					break;
				case 'B': // Past
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					sprx1 = 1;
					sprx2 = 34;
					spry = 157;
					break;
				case 'C': // Good Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					sprx1 = 1;
					sprx2 = 34;
					spry = 190;
					break;
				case 'D': // Bad Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					sprx1 = 1;
					sprx2 = 34;
					spry = 223;
					break;
			}
			
			frames[0] = new Sprite(sheet.GetSection(sprx1, spry, 32, 32), -16, -16);
			frames[1] = new Sprite(sheet.GetSection(sprx2, spry, 32, 32), -16, -16);
			frames[2] = new Sprite(sheet.GetSection(sprx1, spry, 32, 32), -32, -16);
			frames[3] = new Sprite(sheet.GetSection(sprx2, spry, 32, 32), 0, -16);
			frames[4] = new Sprite(sheet.GetSection(sprx1, spry, 32, 32), -16, -32);
			frames[5] = new Sprite(sheet.GetSection(sprx2, spry, 32, 32), -16, 0);
			frames[6] = new Sprite(sheet.GetSection(sprx1, spry, 32, 32), -32, -32);
			frames[7] = new Sprite(sheet.GetSection(sprx2, spry, 32, 32), 0, -32);
			frames[8] = new Sprite(sheet.GetSection(sprx2, spry, 32, 32), -32, 0);
			frames[9] = new Sprite(sheet.GetSection(sprx1, spry, 32, 32), 0, 0);
			frames[10] = new Sprite(sheet.GetSection(sprx1, spry, 32, 32), -64, -16);
			frames[11] = new Sprite(sheet.GetSection(sprx2, spry, 32, 32), 32, -16);
			
			sprites[0] = new Sprite(frames[0]);
			sprites[1] = new Sprite(frames[1]);
			sprites[2] = new Sprite(frames[2], frames[3]);
			sprites[3] = new Sprite(frames[4], frames[5]);
			sprites[4] = new Sprite(frames[6], frames[7], frames[8], frames[9]);
			sprites[5] = new Sprite(frames[2], frames[10], frames[11], frames[3]);
			
			properties[0] = new PropertySpec("Formation", typeof(int), "Extended",
				"How this set of Blocks should be arranged.", null, new Dictionary<string, int>
				{
					{ "Single Block (Frame A)", 0 },
					{ "Single Block (Frame B)", 1 },
					{ "Two Blocks (Horizontal)", 2 },
					{ "Two Blocks (Vertical)", 3 },
					{ "Four Blocks (Cube)", 4 },
					{ "Four Blocks (Horizontal)", 5 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
				case 0: return "Single Block (Frame A)";
				case 1: return "Single Block (Frame B)";
				case 2: return "Two Blocks (Horizontal)";
				case 3: return "Two Blocks (Vertical)";
				case 4: return "Four Blocks (Cube)";
				case 5: return "Four Blocks (Horizontal)";
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