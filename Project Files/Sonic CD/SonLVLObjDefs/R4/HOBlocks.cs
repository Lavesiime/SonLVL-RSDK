using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class HOBlocks2 : R4.HOBlocks
	{
		public override Sprite GetFrame()
		{
			Sprite block;
			Sprite[] frames = new Sprite[3];
			
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
			
			frames[0] = new Sprite(block, -48, -16);
			frames[1] = new Sprite(block, -16, -16);
			frames[2] = new Sprite(block,  16, -16);
			
			return new Sprite(frames);
		}
	}
	
	class HOBlocks : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite debug;
		
		public virtual Sprite GetFrame()
		{
			Sprite block;
			Sprite[] frames = new Sprite[4];
			
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
			
			frames[0] = new Sprite(block, -64, -16);
			frames[1] = new Sprite(block, -32, -16);
			frames[2] = new Sprite(block,   0, -16);
			frames[3] = new Sprite(block,  32, -16);
			
			return new Sprite(frames);
		}
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			// this line's kinda lame tbh but it's better than nothing
			BitmapBits bitmap = new BitmapBits(65, 2);
			bitmap.DrawLine(6, 0, 0, 64, 0);
			debug = new Sprite(bitmap, -32, 0);
			
			// technically it should just be shown as a normal int to the user.. but this is cleaner imo
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which position this block should start from and which direction it should be moving towards.", null, new Dictionary<string, int>
				{
					{ "Middle (-> Right)", 0xc0 },
					{ "Middle Right (-> Right)", 0xe0 },
					{ "Right (-> Left)", 0x00 },
					{ "Middle Right (-> Left)", 0x20 },
					{ "Middle (-> Left)", 0x40 },
					{ "Middle Left (-> Left)", 0x60 },
					{ "Left (-> Right)", 0x80 },
					{ "Middle Left (-> Right)", 0xa0 }
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
			get { return new ReadOnlyCollection<byte>(new byte[] {0xc0, 0xe0, 0x00, 0x20, 0x40, 0x60, 0x80, 0xa0}); }
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
			return new Sprite(sprite, (int)(Math.Cos(angle) * 32), 0);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}