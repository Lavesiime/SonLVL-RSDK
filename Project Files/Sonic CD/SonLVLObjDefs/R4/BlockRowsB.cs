using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class BlockRowsB : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[6];
		private Sprite debug;
		
		public override void Init(ObjectData data)
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
			
			frames[0] = new Sprite(block, -32, -32);
			frames[1] = new Sprite(block,   0, -32);
			frames[2] = new Sprite(block, -32,   0);
			frames[3] = new Sprite(block,   0,   0);
			
			sprites[5] = new Sprite(frames);
			
			int[] offsets = {-96, -32, 32, 96, 32};
			for (int i = 0; i < 5; i++)
			{
				sprites[i] = new Sprite(sprites[5], offsets[i], 0);
			}
			
			BitmapBits bitmap = new BitmapBits(193, 2);
			bitmap.DrawLine(6, 0, 0, 192, 0);
			debug = new Sprite(bitmap, -96, 0);
			
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which position this block should start from and which direction it should be moving.", null, new Dictionary<string, int>
				{
					{ "Left (-> Right)", 0 },
					{ "Middle Left (-> Right)", 1 },
					{ "Middle Right (-> Right)", 2 },
					{ "Right (-> Left)", 3 },
					{ "Middle Right (-> Left)", 4 }
				},
				(obj) => (obj.PropertyValue < 5) ? (int)obj.PropertyValue : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return "Start From " + properties[0].Enumeration.GetKey((subtype < 5) ? subtype : 0);
		}

		public override Sprite Image
		{
			get { return sprites[5]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[5];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue < 5) ? obj.PropertyValue : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}