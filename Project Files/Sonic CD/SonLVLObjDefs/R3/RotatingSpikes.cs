using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R3
{
	class RotatingSpikes : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[2];
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[2];
			BitmapBits sheet = LevelData.GetSpriteSheet("R3/Objects.gif");
			frames[0] = new Sprite(sheet.GetSection(181, 143, 16, 16), -8, -8);
			frames[1] = new Sprite(sheet.GetSection(181, 160, 16, 16), -8, -8);
			
			// In the future ('C' and 'D'), the first length is 4 chains instead of 3
			int[] lengths = {(LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] > 'B') ? 4 : 3, 2};
			
			for (int i = 0; i < lengths.Length; i++)
			{
				sprites[i] = frames[0];
				for (int j = 0; j < lengths[i]; j++)
					sprites[i] = new Sprite(sprites[i], new Sprite(frames[1], 0, -((j+1) * 16)));
				
				BitmapBits bitmap = new BitmapBits(2 * (lengths[i] * 16) + 1, 2 * (lengths[i] * 16) + 1);
				bitmap.DrawCircle(6, (lengths[i] * 16), (lengths[i] * 16), (lengths[i] * 16)); // LevelData.ColorWhite
				debug[i] = new Sprite(bitmap, -(lengths[i] * 16), -(lengths[i] * 16));
			}
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction these spikes will rotate in.", null, new Dictionary<string, int>
				{
					{ "Clockwise", 0 },
					{ "Counter-Clockwise", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (int)value));
			
			properties[1] = new PropertySpec("Length", typeof(int), "Extended",
				"Which direction these spikes will rotate in.", null, new Dictionary<string, int> // is it worth specifying that it's time-period dependant?
				{
					{ lengths[0] + " Spikes", 0 },
					{ lengths[1] + " Spikes", 2 }
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
			return properties[1].Enumeration.GetKey(subtype & 2) + "Spikes, " + properties[0].Enumeration.GetKey(subtype & 1);
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype & 2) >> 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue & 2) >> 1];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(obj.PropertyValue & 2) >> 1];
		}
	}
}