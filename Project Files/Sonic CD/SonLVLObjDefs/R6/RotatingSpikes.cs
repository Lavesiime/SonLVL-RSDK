using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R6
{
	class RotatingSpikes : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[5];
		private Sprite[] debug = new Sprite[5];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[2];
			BitmapBits sheet = LevelData.GetSpriteSheet("R6/Objects.gif");
			frames[0] = new Sprite(sheet.GetSection(133, 199, 16, 16), -8, -8);
			frames[1] = new Sprite(sheet.GetSection(150, 199, 16, 16), -8, -8);
			
			int[] lengths = {5, 5, 5, 2, 6};
			
			// for the first three subtypes, length depends on time zone
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				// present (5) is the default value in the array
				
				case 'B': // Past
					lengths[0] = 4; break;
				case 'C': // Good Future
				case 'D': // Bad Future
					lengths[0] = 6; break;
			}
			
			lengths[2] = lengths[1] = lengths[0];
			
			for (int i = 0; i < lengths.Length; i++)
			{
				sprites[i] = frames[0];
				for (int j = 0; j < lengths[i]; j++)
					sprites[i] = new Sprite(sprites[i], new Sprite(frames[1], 0, -((j+1) * 16)));
				
				BitmapBits overlay = new BitmapBits(2 * (lengths[i] * 16) + 1, 2 * (lengths[i] * 16) + 1);
				overlay.DrawCircle(6, (lengths[i] * 16), (lengths[i] * 16), (lengths[i] * 16)); // LevelData.ColorWhite
				debug[i] = new Sprite(overlay, -(lengths[i] * 16), -(lengths[i] * 16));
			}
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How these Spikes should behave.", null, new Dictionary<string, int>
				{
					{ lengths[0] + " Spikes, Clockwise", 0 },
					{ lengths[1] + " Spikes, Counter-Clockwise", 1 },
					{ lengths[2] + " Spikes, Match obj[-1]'s rotation", 2 },
					{ lengths[3] + " Spikes, Counter-Clockwise", 3 },
					{ lengths[4] + " Spikes, Clockwise ", 4 }, // extra space at the end of the name in case it's a dupe of type 0 lol
					// 5 technically works as static i think, but.. let's not
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue =  (byte)((int)value));
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
			return sprites[(obj.PropertyValue < 5) ? obj.PropertyValue : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue < 5) ? debug[obj.PropertyValue] : null;
		}
	}
}