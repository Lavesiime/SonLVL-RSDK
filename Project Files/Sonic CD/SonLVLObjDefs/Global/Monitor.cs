using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace SCDObjectDefinitions.Global
{
	class Monitor : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private ReadOnlyCollection<byte> subtypes;
		private Sprite[] sprites = new Sprite[13];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items.gif");
			
			sprites[0] = new Sprite(sheet.GetSection(51, 67, 32, 32), -16, -16); // empty
			sprites[1] = new Sprite(sheet.GetSection(18, 1, 32, 32), -16, -16); // rings
			sprites[2] = new Sprite(sheet.GetSection(18, 34, 32, 32), -16, -16); // shield
			sprites[3] = new Sprite(sheet.GetSection(18, 67, 32, 32), -16, -16); // invincibility
			sprites[4] = new Sprite(sheet.GetSection(18, 100, 32, 32), -16, -16); // speed shoes
			sprites[5] = new Sprite(sheet.GetSection(18, 133, 32, 32), -16, -16); // sonic
			sprites[6] = new Sprite(sheet.GetSection(18, 166, 32, 32), -16, -16); // clock
			sprites[7] = new Sprite(sheet.GetSection(51, 100, 32, 32), -16, -16); // tails
			sprites[8] = new Sprite(sheet.GetSection(51, 133, 32, 32), -16, -16); // super
			// (9 and 10 are origins frames, let's hold off on those for now) 
			sprites[11] = new Sprite(sheet.GetSection(51, 1, 32, 32), -16, -16); // static 1
			sprites[12] = new Sprite(sheet.GetSection(51, 34, 32, 32), -16, -16); // static 2
			
			bool plus = false;
			
			try
			{
				// the pre-plus sheet isn't large enough to hold these, let's use that to see which datafile type we're on
				// (there are probably mods which expand the sheet too, but i can't really think of any better way to do this, sorry..)
				
				sprites[9] = new Sprite(sheet.GetSection(34, 256, 32, 32), -16, -16); // knux (origins)
				sprites[10] = new Sprite(sheet.GetSection(1, 256, 32, 32), -16, -16); // amy (origins)
				
				plus = true;
			}
			catch
			{
				// the sheet isn't large enough for the new frames, let's assume this is a pre-plus datafile
				
				sprites[9] = sprites[11];
				sprites[10] = sprites[12];
				
				plus = false;
			}
			
			if (plus)
			{
				// this part is kind of scuffed.. it sure would've been easier it plane was just the top 4 bits instead of whatever this is
				
				properties[0] = new PropertySpec("Contents", typeof(int), "Extended",
					"The Contents of this Monitor.", null, new Dictionary<string, int>
					{
						{ "Blank", 0 },
						{ "Rings", 1 },
						{ "Shield", 2 },
						{ "Invincibility", 3 },
						{ "Speed Shoes", 4 },
						{ "Sonic", 5 },
						{ "Clock", 6 },
						{ "Tails", 7 },
						{ "Super", 8 },
						{ "Knuckles", 9 },
						{ "Amy", 10 }
					},
					(obj) => (obj.PropertyValue % 11),
					(obj, value) => obj.PropertyValue = (byte)(obj.PropertyValue - (obj.PropertyValue % 11) + ((int)value)));
				
				properties[1] = new PropertySpec("Plane", typeof(int), "Extended",
					"Which Plane this Monitor should be on.", null, new Dictionary<string, int>
					{
						{ "High Plane", 0 },
						{ "Low Plane", 11 },
					},
					(obj) => (obj.PropertyValue > 11) ? 11 : 0,
					(obj, value) => obj.PropertyValue = (byte)(obj.PropertyValue - ((obj.PropertyValue > 11) ? 11 : 0) + ((int)value)));
				
				subtypes = new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 });
			}
			else
			{
				properties[0] = new PropertySpec("Contents", typeof(int), "Extended",
					"The Contents of this Monitor.", null, new Dictionary<string, int>
					{
						{ "Blank", 0 },
						{ "Rings", 1 },
						{ "Shield", 2 },
						{ "Invincibility", 3 },
						{ "Speed Shoes", 4 },
						{ "Sonic", 5 },
						{ "Clock", 6 },
						{ "Tails", 7 },
						{ "Super", 8 }
					},
					(obj) => (obj.PropertyValue % 9),
					(obj, value) => obj.PropertyValue = (byte)(obj.PropertyValue - (obj.PropertyValue % 9) + ((int)value)));
				
				properties[1] = new PropertySpec("Plane", typeof(int), "Extended",
					"Which Plane this Monitor should be on.", null, new Dictionary<string, int>
					{
						{ "High Plane", 0 },
						{ "Low Plane", 9 },
					},
					(obj) => (obj.PropertyValue > 9) ? 9 : 0,
					(obj, value) => obj.PropertyValue = (byte)(obj.PropertyValue - ((obj.PropertyValue > 9) ? 9 : 0) + ((int)value)));
				
				subtypes = new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 });
			}
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return subtypes; }
		}
		
		public override byte DefaultSubtype
		{
			get { return 1; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			string[] contents = new string[]{ "Blank", "Rings", "Shield", "Invincibility", "Speed Shoes", "Sonic", "Clock", "Tails", "Super", "Knuckles", "Amy" };
			string name = contents[subtype % (subtypes.Count / 2)];
			if (subtype > (subtypes.Count / 2)) name += " (Low Plane)";
			return name;
		}

		public override Sprite Image
		{
			get { return sprites[1]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype % (subtypes.Count / 2)];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue % (subtypes.Count / 2)];
		}
	}
}