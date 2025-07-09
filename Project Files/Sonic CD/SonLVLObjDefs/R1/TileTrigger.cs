using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class TileTriggerH : R1.TileTriggerV
	{
		public override Sprite DrawSprite(byte length)
		{
			if (length == 0)
				return sprite;
			
			List<Sprite> sprites = new List<Sprite>(length);
			int sx = -(length * 8) + 8;
			for (int i = 0; i < length; i++)
				sprites.Add(new Sprite(sprite, sx + (i * 16), 0));
			
			return new Sprite(sprites.ToArray());
		}
	}
	
	class TileTriggerL : R1.TileTriggerV
	{
		public override bool enteranceProperty { get { return false; } }
		
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(156, 67, 16, 16), -8, -8);
		}
	}
	
	class TileTriggerV : ObjectDefinition
	{
		private PropertySpec[] properties;
		public Sprite sprite;
		private Sprite debug;
		
		public virtual bool enteranceProperty { get { return true; } }
		
		public virtual Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
		}
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			BitmapBits bitmap = new BitmapBits(256, 256);
			bitmap.DrawRectangle(6, 0, 0, 255, 255);
			debug = new Sprite(bitmap);
			
			if (enteranceProperty)
			{
				// We're either a Tile Trigger V or H, so let's show the 2nd "Exit" property
				properties = new PropertySpec[2];
				
				properties[0] = new PropertySpec("Size", typeof(int), "Extended",
					"How long the Tile Trigger will be.", null,
					(obj) => (int)obj.PropertyValue,
					(obj, value) => obj.PropertyValue = (byte)((int)value));
				
				properties[1] = new PropertySpec("Exit", typeof(bool), "Extended",
					"If this Tile Trigger is the exit of the loop. Note that this object needs to be followed by a Blank Object on the object list in order to function properly.", null,
					(obj) => {
							int index = LevelData.Objects.IndexOf(obj) + 1;
							if (index >= LevelData.Objects.Count)
								return false;
							
							ObjectEntry other = LevelData.Objects[index];
							return (other.Type == 0 && other.PropertyValue != 0); // Is it a Blank Object with a non-zero PropertyValue?
						},
					(obj, value) => {
							int index = LevelData.Objects.IndexOf(obj) + 1;
							if (index >= LevelData.Objects.Count)
								return;
							
							ObjectEntry other = LevelData.Objects[index];
							if (other.Type > 0) return; // Don't change it if it's not a Blank Object
							
							other.PropertyValue = (byte)((bool)value ? 1 : 0);
						}
					);
					
				// Not only should Object[+1] should be a Blank Object, but its prop val is also (sometimes) used by the blank object too.. now how do we even show that in the editor? i'm not too sure..
			}
			else
			{
				// We're a Tile Trigger L, so we only need to show the Size property
				properties = new PropertySpec[1];
				
				properties[0] = new PropertySpec("Size", typeof(int), "Extended",
					"How long the Tile Trigger will be.", null,
					(obj) => (int)obj.PropertyValue,
					(obj, value) => obj.PropertyValue = (byte)((int)value));
			}
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {4, 8, 12, 16}); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override byte DefaultSubtype
		{
			get { return 4; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + " Nodes";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return DrawSprite(subtype);
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return DrawSprite(obj.PropertyValue);
		}
		
		public virtual Sprite DrawSprite(byte length)
		{
			if (length == 0)
				return sprite;
			
			List<Sprite> sprites = new List<Sprite>(length);
			int sy = -(length * 8) + 8;
			for (int i = 0; i < length; i++)
				sprites.Add(new Sprite(sprite, 0, sy + (i * 16)));
			
			return new Sprite(sprites.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			// Let's draw a line to this object's helper, as well as box around the chunks that this object's gonna change
			
			int index = LevelData.Objects.IndexOf(obj) + 1;
			if (index >= LevelData.Objects.Count)
				return null;
			
			ObjectEntry other = LevelData.Objects[index];
			
			int xmin = Math.Min(obj.X, other.X);
			int ymin = Math.Min(obj.Y, other.Y);
			int xmax = Math.Max(obj.X, other.X);
			int ymax = Math.Max(obj.Y, other.Y);
			
			BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			bitmap.DrawLine(6, obj.X - xmin, obj.Y - ymin, other.X - xmin, other.Y - ymin);
			
			return new Sprite(new Sprite(debug, (other.X & ~0x7f) - obj.X, (other.Y & ~0x7f) - obj.Y), new Sprite(bitmap, xmin - obj.X, ymin - obj.Y));
		}
	}
}