using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class Bridge : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					sprite = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(35, 1, 16, 16), -8, -8);
					break;
				case 'B': // Past
					sprite = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(67, 174, 16, 16), -8, -8);
					break;
				case 'C': // Good Future
					sprite = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(83, 174, 16, 16), -8, -8);
					break;
				case 'D': // Bad Future
					sprite = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(83, 158, 16, 16), -8, -8);
					break;
			}
			
			properties[0] = new PropertySpec("Length", typeof(int), "Extended",
				"How long this Bridge will be.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {6, 8, 10, 12, 14, 16}); } // it can be any value, but why not give a few starting ones
		}
		
		public override byte DefaultSubtype
		{
			get { return 12; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + " Logs"; // honestly idk if you'd call QQZ's things "logs" but let's just keep it like this anyways
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
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
			if (obj.PropertyValue == 0)
				return sprite;
			
			int st = -((obj.PropertyValue * 16) / 2) + 8;
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < obj.PropertyValue; i++)
			{
				sprs.Add(new Sprite(sprite, st + (i * 16), 0));
			}
			return new Sprite(sprs.ToArray());
		}
	}
}