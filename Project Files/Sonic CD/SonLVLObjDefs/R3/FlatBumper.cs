using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R3
{
	class FlatBumper : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				default:
				case 'A': // Present
				case 'B': // Past
					sprite = new Sprite(LevelData.GetSpriteSheet("R3/Objects.gif").GetSection(1, 75, 64, 32), -32, -16);
					break;
				case 'C': // Good Future
					sprite = new Sprite(LevelData.GetSpriteSheet("R3/Objects3.gif").GetSection(132, 67, 64, 32), -32, -16);
					break;
				case 'D': // Bad Future
					sprite = new Sprite(LevelData.GetSpriteSheet("R3/Objects3.gif").GetSection(132, 100, 64, 32), -32, -16);
					break;
			}
			
			BitmapBits bitmap = new BitmapBits(193, 2);
			bitmap.DrawLine(6, 0, 0, 192, 0);
			debug[0] = new Sprite(bitmap, -96, 0);
			
			bitmap = new BitmapBits(2, 193);
			bitmap.DrawLine(6, 0, 0, 0, 192);
			debug[1] = new Sprite(bitmap, 0, -96);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction this bumper should travel in.", null, new Dictionary<string, int>
				{
					{ "Horizontal", 0 },
					{ "Vertical", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
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
			return (subtype == 0) ? "Move Horizontally" : "Move Vertically";
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
			return debug[(obj.PropertyValue == 0) ? 0 : 1];
		}
	}
}