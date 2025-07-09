using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class HorizontalDoor : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite sprite;
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] <= 'B')
				sprite = new Sprite(LevelData.GetSpriteSheet("R4/Objects.gif").GetSection(18, 69, 128, 16), -64, -8); // past/present frame
			else
				sprite = new Sprite(LevelData.GetSpriteSheet("R4/Objects3.gif").GetSection(62, 211, 128, 16), -64, -8); // good/bad future frame
			
			BitmapBits bitmap = new BitmapBits(129, 17);
			bitmap.DrawRectangle(6, 0, 0, 127, 15); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -64 - 128, -8);
			debug[1] = new Sprite(bitmap, -64 + 128, -8);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way this Door should open.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (int)value));
			
			properties[1] = new PropertySpec("Close Height", typeof(int), "Extended",
				"Origins only. How far above the player should be for the door to close.", null, new Dictionary<string, int>
				{
					{ "48px", 0 },
					{ "20px", 2 } // the box is -400, -100, 400, -20, but the only relevant bit is the 20px bit (and even then it's actually a bit less, cause it's a hitbox check and not a normal ypos check)
				},
				(obj) => obj.PropertyValue & 2,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~2) | (int)value));
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
			switch (subtype & 3)
			{
				default:
				case 0: return "Open Left (Close @ 48px)";
				case 1: return "Open Right (Close @ 48px)";
				case 2: return "Open Left (Close @ 20px)";
				case 3: return "Open Right (Close @ 20px)";
			}
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
			return debug[obj.PropertyValue & 1];
		}
	}
}