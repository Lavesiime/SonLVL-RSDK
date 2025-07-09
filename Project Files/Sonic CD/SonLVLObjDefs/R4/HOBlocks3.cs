using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class HOBlocks3 : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[4];
		private Sprite[] debug = new Sprite[4];
		
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
			
			sprites[0] = new Sprite(block, -16, -16);
			sprites[1] = new Sprite(new Sprite(block, -32, -16), new Sprite(block, 0, -16));
			sprites[2] = new Sprite(new Sprite(block, -48, -16), new Sprite(block, -16, -16), new Sprite(block, 16, -16));
			sprites[3] = new Sprite(new Sprite(block, -64, -16), new Sprite(block, -32, -16), new Sprite(block, 0, -16), new Sprite(block, 32, -16));
			
			// (these lines are kind of useless tbh, you can hardly even see them..)
			int[] amplitudes = {0x800, 0x1000, 0x1800, 0x2000};
			for (int i = 0; i < amplitudes.Length; i++)
			{
				int length = (512 * amplitudes[i] * 2) >> 16;
				BitmapBits bitmap = new BitmapBits(length + 1, 2);
				bitmap.DrawLine(6, 0, 0, length, 0);
				debug[i] = new Sprite(bitmap, -length / 2, 0);
				
				sprites[i].Offset(-length / 2, 0);
			}
			
			// formation/range are the same thing, so..
			properties[0] = new PropertySpec("Formation", typeof(int), "Extended",
				"Which formation these blocks should be in. Affects movement range as well.", null, new Dictionary<string, int>
				{
					{ "Single Block", 0 },
					{ "Two Blocks", 1 },
					{ "Three Blocks", 2 },
					{ "Four Blocks", 3 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return properties[0].Enumeration.GetKey(subtype);
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
			return sprites[(obj.PropertyValue < 4) ? obj.PropertyValue : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue < 4) ? debug[obj.PropertyValue] : null;
		}
	}
}