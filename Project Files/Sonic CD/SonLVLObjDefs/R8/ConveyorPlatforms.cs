using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R8
{
	class ConveyorPlatforms : ObjectDefinition
	{
		// throwing this here - m094 sticks in a bunch of random extra Conveyor Platforms around its conveyor chains but those don't really do anything
		// there's not much we can do about 'em either, so..
		
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[7];
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			sprites[6] = new Sprite(LevelData.GetSpriteSheet("R8/Objects.gif").GetSection(107, 98, 32, 16), -16, -8);
			
			BitmapBits bitmap = new BitmapBits(104 * 2 + 2, 64 * 2 + 2);
			bitmap.DrawLine(6, 40, 6, 200, 86);  // Top line
			bitmap.DrawLine(6, 165, 120, 7, 41); // Bottom line
			bitmap.DrawCircle(6, 104 + 80, 64 + 40, 24); // Right circle (was originally gonna crop it, but imo it kinda looks cooler when the full wheel is in view--)
			bitmap.DrawCircle(6, 104 - 80, 64 - 40, 24); // Left circle  (ditto)
			debug = new Sprite(bitmap, -104, -64);
			
			int[] points = {0, 90, 180, 268, 358};
			for (int i = 0; i < points.Length; i++)
			{
				Point p = calcPos(points[i]);
				sprites[i+1] = new Sprite(sprites[6], p.X, p.Y);
			}
			
			properties[0] = new PropertySpec("Mode", typeof(int), "Extended",
				"Master objects should be followed by 5 child platforms.", null, new Dictionary<string, int>
				{
					{ "Parent", 0 },
					{ "Child", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		// this could def be a local function.. but i don't think the compiler in SonLVL itself is updated enough to do that?
		// and for compatability with older builds of the editor, we're just gonna leave this func here
		// either way, this is adapted from the original code, it's just the ObjectMain movement cycle but in C#
		public Point calcPos(int t)
		{
			int sx = 0, sy = 0;
			if (t < 160)
			{
				sx = t - 64;
				sy = (t >> 1) - 58;
			}
			else if (t < 224)
			{
				double angle = (((t - 144) << 2)/512.0) * Math.PI;
				sx = ((int)(Math.Sin(angle) * 512 * 0xC00) >> 16) + 80;
				sy = ((int)(Math.Cos(angle) * 512 * -0xC00) >> 16) + 40;
			}
			else if (t < 384)
			{
				sx = 62 - (t - 224);
				sy = 57 - ((t - 224) >> 1);
			}
			else
			{
				double angle = (((t - 368) << 2)/512.0) * Math.PI;
				sx = ((int)(Math.Sin(angle) * 512 * -0xC00) >> 16) - 80;
				sy = ((int)(Math.Cos(angle) * 512 * 0xC00) >> 16) - 40;
			}
			
			return new Point(sx, sy);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
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
			return (subtype == 0) ? "Parent Object" : "Child Object";
		}

		public override Sprite Image
		{
			get { return sprites[6]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 0) ? 0 : 6];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int index = LevelData.Objects.IndexOf(obj);
			
			// It's kinda funky, this is is like a recursive way of updating the next platform (in case the first platform had its Mode changed, we need to update all the platforms that follow it)
			if (((index + 1) < LevelData.Objects.Count) && (LevelData.Objects[index + 1].Type == obj.Type) && (LevelData.Objects[index + 1].PropertyValue != 0))
				LevelData.Objects[index + 1].UpdateSprite();
			
			if (obj.PropertyValue == 0)
				return sprites[0];
			
			index--;
			int offset = 1;
			while (index > 0)
			{
				if ((LevelData.Objects[index].Type == obj.Type) && (LevelData.Objects[index].PropertyValue == 0))
					break;
				
				index--;
				offset++;
				
				if (offset == 6) // there's no proper master obj for us to follow, let's just display the normal sprite at our current pos
				{
					return sprites[6];
				}
			}
			
			ObjectEntry leader = LevelData.Objects[index];
			return new Sprite(sprites[offset], leader.X - obj.X, leader.Y - obj.Y);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue == 0) ? debug : null;
		}
	}
}