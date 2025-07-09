using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class GrabPole : ObjectDefinition
	{
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("D")) // We're using a different sprite in the Bad Future
				sprite = new Sprite(LevelData.GetSpriteSheet("R4/Objects3.gif").GetSection(51, 163, 8, 92), -4, -46);
			else
				sprite = new Sprite(LevelData.GetSpriteSheet("R4/Objects3.gif").GetSection(134, 46, 8, 92), -4, -46);
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