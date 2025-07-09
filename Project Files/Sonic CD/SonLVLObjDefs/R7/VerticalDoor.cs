using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class VerticalDoor : ObjectDefinition
	{
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("C")) // Using a different sprite for the Good Future
				sprite = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(117, 219, 32, 32), -16, 0);
			else
				sprite = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(1, 1, 32, 32), -16, 0);
			
			sprite = new Sprite(sprite, new Sprite(sprite, 0, -32));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
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
	}
}