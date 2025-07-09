using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class VOBlocks : R4.VOBlock
	{
		public override int amplitude { get { return 0x1800; } }
		
		public override Sprite GetFrame()
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					return new Sprite(LevelData.GetSpriteSheet("R4/Objects.gif").GetSection(163, 1, 32, 32), -16, -16);
				case 'B': // Past
					return new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 157, 32, 32), -16, -16);
				case 'C': // Good Future
					return new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 190, 32, 32), -16, -16);
				case 'D': // Bad Future
					return new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 223, 32, 32), -16, -16);
			}
		}
	}
	
	class VOBlocks2 : R4.VOBlock
	{
		public override int amplitude { get { return 0x2000; } }
		
		public override Sprite GetFrame()
		{
			Sprite[] blocks = new Sprite[2];
			Sprite[] frames = new Sprite[4];
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
					blocks[0] = new Sprite(sheet.GetSection(163, 1, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(196, 1, 32, 32));
					break;
				case 'B': // Past
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 157, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 157, 32, 32));
					break;
				case 'C': // Good Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 190, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 190, 32, 32));
					break;
				case 'D': // Bad Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 223, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 223, 32, 32));
					break;
			}
			
			frames[0] = new Sprite(blocks[0], -16, -64);
			frames[1] = new Sprite(blocks[1], -16, -32);
			frames[2] = new Sprite(blocks[0], -16,   0);
			frames[3] = new Sprite(blocks[1], -16,  32);
			
			return new Sprite(frames);
		}
	}
	
	class VOBlocks3 : R4.VOBlock
	{
		public override int amplitude { get { return 0x2800; } }
		
		public override Sprite GetFrame()
		{
			Sprite[] blocks = new Sprite[2];
			Sprite[] frames = new Sprite[12];
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
					blocks[0] = new Sprite(sheet.GetSection(163, 1, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(196, 1, 32, 32));
					break;
				case 'B': // Past
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 157, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 157, 32, 32));
					break;
				case 'C': // Good Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 190, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 190, 32, 32));
					break;
				case 'D': // Bad Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 223, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 223, 32, 32));
					break;
			}
			
			frames[0] = new Sprite(blocks[0], -32, -96);
			frames[1] = new Sprite(blocks[1], 0, -96);
			frames[2] = new Sprite(blocks[1], -32, -64);
			frames[3] = new Sprite(blocks[0], 0, -64);
			frames[4] = new Sprite(blocks[1], -32, -32);
			frames[5] = new Sprite(blocks[0], 0, -32);
			frames[6] = new Sprite(blocks[0], -32, 0);
			frames[7] = new Sprite(blocks[1], 0, 0);
			frames[8] = new Sprite(blocks[1], -32, 32);
			frames[9] = new Sprite(blocks[0], 0, 32);
			frames[10] = new Sprite(blocks[0], -32, 64);
			frames[11] = new Sprite(blocks[1], 0, 64);
			
			return new Sprite(frames);
		}
	}
	
	class VOBlocks4 : R4.VOBlock
	{
		public override int amplitude { get { return 0x2000; } }
		
		public override Sprite GetFrame()
		{
			Sprite[] blocks = new Sprite[2];
			Sprite[] frames = new Sprite[4];
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
					blocks[0] = new Sprite(sheet.GetSection(163, 1, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(196, 1, 32, 32));
					break;
				case 'B': // Past
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 157, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 157, 32, 32));
					break;
				case 'C': // Good Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 190, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 190, 32, 32));
					break;
				case 'D': // Bad Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 223, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 223, 32, 32));
					break;
			}
			
			frames[0] = new Sprite(blocks[0], -16, -64);
			frames[1] = new Sprite(blocks[0], -16, -32);
			frames[2] = new Sprite(blocks[1], -16,   0);
			frames[3] = new Sprite(LevelData.GetSpriteSheet("Global/Items3.gif").GetSection(50, 100, 32, 32), -16, 32);
			
			return new Sprite(frames);
		}
	}
	
	class VOBlocks5 : R4.VOBlock
	{
		public override int amplitude { get { return 0x4000; } }
		
		public override Sprite GetFrame()
		{
			Sprite block;
			Sprite[] frames = new Sprite[2];
			
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
			
			frames[0] = new Sprite(block, -16, -32);
			frames[1] = new Sprite(block, -16,   0);
			
			return new Sprite(frames);
		}
	}
	
	abstract class VOBlock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite debug;
		
		public abstract int amplitude { get; }
		public abstract Sprite GetFrame();
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			int length = (512 * amplitude * 2) >> 16;
			BitmapBits bitmap = new BitmapBits(2, length + 1);
			bitmap.DrawLine(6, 0, 0, 0, length);
			debug = new Sprite(bitmap, 0, -length / 2);
			
			// technically it should just be shown as a normal int to the user.. but this is cleaner imo
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which position this block should start from and which direction it should be moving.", null, new Dictionary<string, int>
				{
					{ "Middle (-> Bottom)", 0x00 },
					{ "Bottom Middle (-> Bottom)", 0x20 },
					{ "Bottom (-> Top)", 0x40 },
					{ "Bottom Middle (-> Top)", 0x60 },
					{ "Middle (-> Top)", 0x80 },
					{ "Top Middle (-> Top)", 0xa0 },
					{ "Top (-> Bottom)", 0xc0 },
					{ "Top Middle (-> Bottom)", 0xe0 }
				},
				(obj) => obj.PropertyValue & ~0x1f,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			/*
			// (Old version)
			properties[0] = new PropertySpec("Angle Offset", typeof(int), "Extended",
				"How offset this Block's angle should be from the global cycle. The inverse of 0 would be 128.", null,
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			*/
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x00, 0x20, 0x40, 0x60, 0x80, 0xa0, 0xc0, 0xe0}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return "Start From " + properties[0].Enumeration.GetKey(subtype);;
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
			double angle = (obj.PropertyValue / 128.0) * Math.PI;
			return new Sprite(sprite, 0, (int)(Math.Sin(angle) * 512 * amplitude) >> 16);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}