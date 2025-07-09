using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class RotatingLog : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[10];
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					sprites[9] = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(35, 1, 16, 16), -8, -8);
					break;
				case 'B': // Past
					sprites[9] = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(67, 174, 16, 16), -8, -8);
					break;
				case 'C': // Good Future
					sprites[9] = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(83, 174, 16, 16), -8, -8);
					break;
				case 'D': // Bad Future
					sprites[9] = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(83, 158, 16, 16), -8, -8);
					break;
			}
			
			BitmapBits bitmap = new BitmapBits(193, 193);
			bitmap.DrawCircle(6, 96, 96, 96);
			debug = new Sprite(bitmap, -96, -96);
			
			int[] angles = {504, 8, 120, 136, 248, 264, 376, 392, 0};
			for (int i = 0; i < angles.Length; i++)
			{
				double angle = angles[i] / 256.0 * Math.PI;
				sprites[i] = new Sprite(sprites[9], (int)(Math.Cos(angle) * 96), (int)(Math.Sin(angle) * 96));
			}
			
			properties[0] = new PropertySpec("Leader", typeof(bool), "Extended",
				"If this Log is the lead of the set. Leaders should be followed by 7 normal Logs.", null,
				(obj) => (obj.PropertyValue == 1),
				(obj, value) => obj.PropertyValue = (byte)((bool)value ? 1 : 0));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype == 1) ? "Leader" : "Follower";
		}

		public override Sprite Image
		{
			get { return sprites[9]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[9];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int index = LevelData.Objects.IndexOf(obj);
			
			// It's kinda funky, this is is like a recursive way of updating the next log (in case the first log had its Leader value changed, so we need to update all the logs that follow it)
			if (((index + 1) < LevelData.Objects.Count) && (LevelData.Objects[index + 1].Type == obj.Type) && (LevelData.Objects[index + 1].PropertyValue == 0))
				LevelData.Objects[index + 1].UpdateSprite();
			
			int offset = 0;
			while (index > 0)
			{
				if ((LevelData.Objects[index].Type == obj.Type) && (LevelData.Objects[index].PropertyValue == 1))
					break;
				
				index--;
				offset++;
				
				if (offset == 8)
				{
					return sprites[8];
				}
			}
			
			return sprites[offset];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}