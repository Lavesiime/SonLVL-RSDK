using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class Hotaru : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[10];
		private Sprite[] debug = new Sprite[4];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R7/Objects.gif");
			
			// Flying sprites, we're gonna use these sprites in the loop
			Sprite[] frames = {
				new Sprite(sheet.GetSection(182, 121, 40, 24), -20, -12),  // Good
				new Sprite(sheet.GetSection(182, 146, 40, 24), -20, -12)}; // Bad
			
			// For each pair:
			// 	Y radius, X radius
			int[] distances = {
				60, 35,
				68, 50,
				56, 70,
				18, 96
			};
			
			BitmapBits bitmap;
			for (int i = 0; i < distances.Length; i += 2)
			{
				sprites[i] = new Sprite(new Sprite(frames[0], distances[i + 1], distances[i]), new Sprite(frames[0], -distances[i + 1], -distances[i], true, false));
				sprites[i + 1] = new Sprite(new Sprite(frames[1], distances[i + 1], distances[i]), new Sprite(frames[1], -distances[i + 1], -distances[i], true, false));
				
				bitmap = new BitmapBits(distances[i + 1] * 2 + 1, distances[i] * 2 + 1);
				bitmap.DrawLine(6, 0, 0, distances[i + 1] * 2, 0);
				bitmap.DrawLine(6, 0, distances[i] * 2, distances[i + 1] * 2, distances[i] * 2);
				debug[i / 2] = new Sprite(bitmap, -distances[i + 1], -distances[i]);
			}
			
			// Icon, good Hotaru facing the camera
			sprites[8] = new Sprite(sheet.GetSection(231, 119, 24, 40), -21, -20);
			
			// Used for the subtype image, a bad Hotaru facing the camera
			sprites[9] = new Sprite(sheet.GetSection(231, 201, 24, 40), -21, -20);
			
			properties[0] = new PropertySpec("Condition", typeof(int), "Extended",
				"What condition these Hotaru should be in. Only good Hotaru can shoot light beams.", null, new Dictionary<string, int>
				{
					{ "Good", 0 },
					{ "Bad", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (int)value));
			
			// tbh still kind of dislike naming it like this, but i can't think of anything better, so..
			properties[1] = new PropertySpec("Radius", typeof(int), "Extended",
				"How far these Hotaru should be apart from each other, vertically.", null, new Dictionary<string, int>
				{
					{ "18px", 12 },
					{ "56px", 8 },
					{ "60px", 0 },
					{ "68px", 4 }
				},
				(obj) => obj.PropertyValue & ~3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 3) | (int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 4, 5, 8, 9, 12, 13}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (((subtype & 1) == 0) ? "Good" : "Bad") + " Condition (" + properties[1].Enumeration.GetKey(subtype & ~3) + " Radius)";
		}

		public override Sprite Image
		{
			get { return sprites[8]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[8 + (subtype & 1)];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue >> 1) | (obj.PropertyValue & 1)];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue >> 2];
		}
	}
}