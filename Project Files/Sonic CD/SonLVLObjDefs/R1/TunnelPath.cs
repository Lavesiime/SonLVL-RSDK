using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class TunnelPath : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			// btw Object[+1] should be a Blank Object with a nonzero prop val if you want the hole in the wall to appear, not quite sure how to show that in the editor though
			
			// this sucks, it's not even relative like all the other tunnelpaths
			// (but yeah, just like every other tunnel path, all these values are taken from the object's giant switch case)
			int[] path = {
				0x13F0B100, 0xF06500,
				0x1400B100, 0xF06500,
				0x1410B100, 0xF06500,
				0x1420B100, 0xF06500,
				0x1430B100, 0xF06500,
				0x1440B100, 0xF06500,
				0x1450B100, 0xF74000,
				0x1460B100, 0xFE1B00,
				0x1470B100, 0x104F600,
				0x1478B100, 0x108F600,
				0x147F8C00, 0x118F600,
				0x14866700, 0x128F600,
				0x148D4200, 0x138F600,
				0x14904200, 0x140F600,
				0x14904200, 0x150F600,
				0x14904200, 0x160F600,
				0x14904200, 0x170F600,
				0x14904200, 0x180F600,
				0x14904200, 0x190F600,
				0x14904200, 0x1A0F600,
				0x14904200, 0x1B0F600,
				0x14904200, 0x1C0F600,
				0x14904200, 0x1D0F600,
				0x14904200, 0x1E0F600,
				0x14804200, 0x1E5C200,
				0x14704200, 0x1EA8E00,
				0x14604200, 0x1EF5A00,
				0x14504200, 0x1F42600,
				0x14404200, 0x1F8F200,
				0x14304200, 0x1F2F200,
				0x14204200, 0x1ECF200,
				0x14104200, 0x1E6F200,
				0x14004200, 0x1E0F200,
				0x13F84200, 0x1D0F200,
				0x13F04200, 0x1C0F200,
				0x13F04200, 0x1B0F200,
				0x13F04200, 0x1A0F200,
				0x13F04200, 0x190F200,
				0x13F04200, 0x180F200,
				0x14004200, 0x170F200,
				0x14104200, 0x16CF200,
				0x14204200, 0x168F200,
				0x14304200, 0x16CF200,
				0x14404200, 0x170F200,
				0x144BAF00, 0x180F200,
				0x14571C00, 0x190F200,
				0x14628900, 0x1A0F200,
				0x14688900, 0x1A8F200,
				0x14788900, 0x1AC8000,
				0x14888900, 0x1B00E00,
				0x14988900, 0x1B39C00,
				0x14A88900, 0x1B72A00,
				0x14B88900, 0x1BAB800,
				0x14C88900, 0x1BE4600,
				0x14D88900, 0x1C1D400,
				0x14E88900, 0x1C56200,
				0x14F88900, 0x1C8F000,
				0x15088900, 0x1CC7E00,
				0x15188900, 0x1D00C00,
				0x15288900, 0x1D39A00,
				0x15388900, 0x1D72800,
				0x15488900, 0x1DAB600,
				0x15588900, 0x1DE4400,
				0x15688900, 0x1E1D200,
				0x15788900, 0x1E56000,
				0x15888900, 0x1E8EE00,
				0x15988900, 0x1EC7C00,
				0x15A88900, 0x1F00A00,
				0x15B88900, 0x1F39800,
				0x15C88900, 0x1F72600,
				0x15D88900, 0x1FAB400,
				0x15F88900, 0x201D000,
				0x16088900, 0x2055E00,
				0x16188900, 0x208EC00,
				0x16288900, 0x20C7A00,
				0x16388900, 0x2100800,
				0x16488900, 0x2139600,
				0x16588900, 0x2172400,
				0x16608900, 0x2182400,
				0x16708900, 0x2162400,
				0x16808900, 0x2142400,
				0x16908900, 0x2122400,
				0x16A08900, 0x2102400,
				0x16B08900, 0x2042400,
				0x16C08900, 0x1F82400,
				0x16C5DE00, 0x1E82400,
				0x16CB3300, 0x1D82400,
				0x16D08800, 0x1C82400,
				0x16C88800, 0x1B82400,
				0x16C08800, 0x1A82400,
				0x16B08800, 0x1A42400,
				0x16A08800, 0x1A02400,
				0x16908800, 0x19C2400,
				0x16808800, 0x1982400,
				0x16708800, 0x19B5700,
				0x16608800, 0x19E8A00,
				0x16588800, 0x1A08A00,
				0x164EEF00, 0x1B08A00,
				0x16455600, 0x1C08A00,
				0x16405600, 0x1C88A00,
				0x1646BC00, 0x1D88A00,
				0x164D2200, 0x1E88A00,
				0x16502200, 0x1F08A00,
				0x16602200, 0x1F5DF00,
				0x16702200, 0x1FB3400,
				0x16802200, 0x2008900,
				0x16902200, 0x2008900,
				0x16A02200, 0x2008900,
				0x16B02200, 0x2008900,
				0x16C02200, 0x2008900,
				0x16D02200, 0x2108900,
				0x16D02200, 0x2208900,
				0x16D02200, 0x2308900,
				0x16D02200, 0x2408900,
				0x16D02200, 0x2508900,
				0x16D02200, 0x2608900,
				0x16D02200, 0x2708900,
				0x16D02200, 0x2808900,
				0x16D02200, 0x2888900,
				0x16CB9000, 0x2988900,
				0x16C6FE00, 0x2A88900,
				0x16C26C00, 0x2B88900,
				0x16C06C00, 0x2C08900,
				0x16B06C00, 0x2C68900,
				0x16A06C00, 0x2CC8900,
				0x16906C00, 0x2D28900,
				0x16806C00, 0x2D88900,
				0x16706C00, 0x2D08900,
				0x16606C00, 0x2C88900,
				0x16506C00, 0x2C08900,
				0x16506C00, 0x2B08900,
				0x16506C00, 0x2A08900,
				0x16606C00, 0x29B3400,
				0x16706C00, 0x295DF00,
				0x16806C00, 0x2908A00,
				0x16906C00, 0x2908A00,
				0x16A06C00, 0x2908A00,
				0x16B06C00, 0x2908A00,
				0x16C06C00, 0x2908A00,
				0x16D06C00, 0x2908A00,
				0x16E06C00, 0x2908A00,
				0x16F06C00, 0x2908A00,
				0x17006C00, 0x2908A00,
				0x17106C00, 0x296F000,
				0x17206C00, 0x29D5600,
				0x17286C00, 0x2A05600,
				0x17286C00, 0x2B05600,
				0x17286C00, 0x2C05600,
				0x17286C00, 0x2D05600,
				0x17286C00, 0x2E05600,
				0x17186C00, 0x2E6BC00,
				0x17086C00, 0x2ED2200,
				0x17006C00, 0x2F02200,
				0x16F06C00, 0x2E68800,
				0x16E09A00, 0x2E4F900
			};
			
			int xmin = 0x7fff;
			int ymin = 0x7fff;
			int xmax = -0x7fff;
			int ymax = -0x7fff;
			
			for (int i = 0; i < path.Length; i += 2)
			{
				xmin = Math.Min(xmin, path[i] >> 16);
				ymin = Math.Min(ymin, path[i+1] >> 16);
				xmax = Math.Max(xmax, path[i] >> 16);
				ymax = Math.Max(ymax, path[i+1] >> 16);
			}
			
			BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			
			for (int i = 2; i < path.Length; i += 2)
				bitmap.DrawLine(6, (path[i-2] >> 16) - xmin, (path[i-1] >> 16) - ymin, (path[i] >> 16) - xmin, (path[i+1] >> 16) - ymin);
			
			debug = new Sprite(bitmap, xmin, ymin);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override bool Debug
		{
			get { return true; }
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
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			// (the tunnel starts at 0x13F0B100, 0xF06500, let's draw a line to that point and join it with the proper debug path vis)
			int xmin = Math.Min((int)obj.X, 0x13F0B100 >> 16);
			int ymin = Math.Min((int)obj.Y, 0xF06500 >> 16);
			int xmax = Math.Max((int)obj.X, 0x13F0B100 >> 16);
			int ymax = Math.Max((int)obj.Y, 0xF06500 >> 16);
			
			BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			bitmap.DrawLine(6, obj.X - xmin, obj.Y - ymin, (0x13F0B100 >> 16) - xmin, (0xF06500 >> 16) - ymin); // LevelData.ColorWhite
			
			return new Sprite(new Sprite(debug, -obj.X, -obj.Y), new Sprite(bitmap, xmin - obj.X, ymin - obj.Y));
		}
	}
}