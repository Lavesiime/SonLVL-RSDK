using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Special
{
	class Sonic : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Special/Sonic.gif").GetSection(1, 246, 40, 48), -20, -48);
			
			// Prop val is just an open byte ranging from 0-255, controlling Sonic's starting direction in individual degrees
			// The debug vis reflects that, but for simplicity's sake, let's only list the 8 cardinal directions in the property

			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction Sonic should start the stage facing towards.", null, new Dictionary<string, int>
				{
					{ "Downwards", 0 },
					{ "Down-Right", 32 },
					{ "Right", 64 },
					{ "Up-Right", 96 },
					{ "Upwards", 128 },
					{ "Up-Left", 160 },
					{ "Left", 192 },
					{ "Down-Left", 224 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 32, 64, 96, 128, 160, 192, 224}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return "Facing " + properties[0].Enumeration.GetKey(subtype);
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
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			BitmapBits bitmap = new BitmapBits(96, 96);
			
			double angle = (Math.PI * obj.PropertyValue / 128) + (Math.PI / 2);
			int x = -Math.Min(Math.Max((int)(Math.Cos(angle) * 50), -48), 48);
			int y =  Math.Min(Math.Max((int)(Math.Sin(angle) * 50), -48), 48);
			
			bitmap.DrawArrow(32, 48, 48, x + 48, y + 48);
			
			// ..it ain't pretty, but it is what it is :]
			return new Sprite(
				new Sprite(bitmap, -48 -1, -68 -1),
				new Sprite(bitmap, -48 +0, -68 -1),
				new Sprite(bitmap, -48 +1, -68 -1),
				new Sprite(bitmap, -48 -1, -68 +0),
				new Sprite(bitmap, -48 +1, -68 +0),
				new Sprite(bitmap, -48 -1, -68 +1),
				new Sprite(bitmap, -48 +0, -68 +1),
				new Sprite(bitmap, -48 +1, -68 +1));
		}
	}
	
	public static class BitmapBitsExtensions
	{
		public static void DrawArrow(this BitmapBits bitmap, byte index, int x1, int y1, int x2, int y2)
		{
			bitmap.DrawLine(index, x1, y1, x2, y2);
			
			double angle = Math.Atan2(y1 - y2, x1 - x2);
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle + 0.40) * 10), y2 + (int)(Math.Sin(angle + 0.40) * 10));
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle - 0.40) * 10), y2 + (int)(Math.Sin(angle - 0.40) * 10));
		}
	}
}
