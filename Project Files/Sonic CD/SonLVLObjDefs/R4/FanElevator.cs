using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class FanElevator : ObjectDefinition
	{
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R4/Objects3.gif");
			Sprite[] sprites = new Sprite[4];
			
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] <= 'B')
			{
				// Present/Past frames
				sprites[0] = new Sprite(sheet.GetSection(84, 46, 32, 48), -32, -32);
				sprites[1] = new Sprite(sheet.GetSection(110, 144, 32, 16), -32, 16);
			}
			else
			{
				// Good/bad Future frames
				sprites[0] = new Sprite(sheet.GetSection(84, 46, 32, 48), -32, -32);
				sprites[1] = new Sprite(sheet.GetSection(126, 228, 32, 16), -32, 16);
			}
			
			sprites[2] = new Sprite(sprites[0], true, false);
			sprites[3] = new Sprite(sprites[1], true, false);
			
			sprite = new Sprite(sprites);
			
			// a debug vis for this obj would probably be pretty cool, but this object's movement is so weird... what would it even be like? a line?
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