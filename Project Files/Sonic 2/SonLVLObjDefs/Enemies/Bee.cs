using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Bee : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '1')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("EHZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(78, 1, 48, 18), -24, -8);
				sprites[1] = new Sprite(sheet.GetSection(19, 50, 6, 5), 5, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(78, 1, 48, 18), -24, -8);
				sprites[1] = new Sprite(sheet.GetSection(137, 331, 6, 5), 5, -8);
			}

			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"The direction the Bee will be facing initially.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 254) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return SubtypeImage(0); }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 2; i++)
			{
				Sprite sprite = new Sprite(sprites[i]);
				sprite.Flip((subtype & 1) == 1, false);
				sprs.Add(sprite);
			}
			return new Sprite(sprs.ToArray());
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}