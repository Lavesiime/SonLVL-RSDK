using SonicRetro.SonLVL.API;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SCDObjectDefinitions.R4
{
	class RotatingSpikes : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[3];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(163, 52, 16, 16), -8, -8); // post
			sprites[1] = new Sprite(sheet.GetSection(180, 52, 16, 16), -8, -8); // chain
			sprites[2] = new Sprite(sheet.GetSection(221, 53, 32, 32), -16, -16); // spike
			
			// btw the hitbox for this object is messed up - the hitbox is at the last *chain*, not the actual spike ball itself
			// so.. that's bad on its own, but if your prop val is 0/1, then it's even worse
			// - at prop val 0, the hitbox is going to be on the opposite side of the post from where the spikeball actually appears
			// - at prop val 1, the hitbox is at the post itself, never moving
			// - at prop vals above those.. it just stays on the last chain, as said before
			// so.. not the best situation, but not much we can do about it lol
			// we'll let the user go to prop val 1 at minimum, visually it's the same as 0 anyways, just with a *little* less jank hitbox..
			
			properties[0] = new PropertySpec("Length", typeof(int), "Extended",
				"How many chains the Spike Ball should hang off of.", null,
				(obj) => Math.Max(0, obj.PropertyValue - 1),
				(obj, value) => obj.PropertyValue = (byte)(Math.Max((int)value+1, 1)));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {5, 6, 7} ); } // it can be any value, but let's give a few starting ones
		}
		
		public override byte DefaultSubtype
		{
			get { return 6; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return Math.Max(0, subtype-1) + " chains";
		}

		public override Sprite Image
		{
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			List<Sprite> sprs = new List<Sprite>() { sprites[0] };
			
			int l = Math.Max(1, (int)obj.PropertyValue);
			for (int i = 1; i <= l; i++)
			{
				int frame = (i == l) ? 2 : 1;
				sprs.Add(new Sprite(sprites[frame], 0, i * 16));
			}
			
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int length = Math.Max(1, (int)obj.PropertyValue) * 16;
			
			BitmapBits overlay = new BitmapBits(2 * length + 1, 2 * length + 1);
			overlay.DrawCircle(6, length, length, length); // LevelData.ColorWhite
			return new Sprite(overlay, -length, -length);
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			Rectangle bounds = sprites[2].Bounds;
			bounds.Offset(obj.X, obj.Y + (Math.Max(1, (int)obj.PropertyValue) * 16));
			return bounds;
		}
	}
}