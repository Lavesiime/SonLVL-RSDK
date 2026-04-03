using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class TransportTube : ObjectDefinition
	{
		// (btw all the path drawing stuff is at the bottom of the file for simplicity, i know it breaks convention but this script was never gonna be pretty in the first place..)
		// If you modified the script's tables in your mod, you can just go ahead and paste your changes in down there
		
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[6];
		
		public override void Init(ObjectData data)
		{
			BitmapBits bitmap = LevelData.GetSpriteSheet("Global/Display.gif");
			sprites[0] = new Sprite(bitmap.GetSection(1, 143, 32, 32), -16, -16); // Main object - SCRIPT box
			sprites[5] = new Sprite(bitmap.GetSection(168, 18, 16, 16), -8, -8);  // Transporters - T icon
			
			// just a lil 16x16 box, to wrap the transporter T icon with
			bitmap = new BitmapBits(16, 16);
			bitmap.DrawRectangle(6, 0, 0, 15, 15);
			sprites[5] = new Sprite(sprites[5], new Sprite(bitmap, -8, -8));
			
			// Since all Transport Tube objects in set are stacked at the same position in the scene, let's go ahead and offset their icons so that they don't appear stacked in the editor
			// We'll keep the main object where it is, and then offset all the nodes to be on the right
			sprites[1] = new Sprite(sprites[5], 8 + 16, -8); // Top left
			sprites[2] = new Sprite(sprites[5], 8 + 32, -8); // Top right
			sprites[3] = new Sprite(sprites[5], 8 + 16,  8); // Bottom left
			sprites[4] = new Sprite(sprites[5], 8 + 32,  8); // Bottom right
			
			// Let's set it all up, stuck into a separate function for cleanliness's sake..
			InitDebugPaths();
			
			properties[0] = new PropertySpec("Entry Size", typeof(int), "Extended",
				"What size of entrance this tube has. A normal Entry object should be followed by 4 Transporters.", null, new Dictionary<string, int> // well really it's [playerCount], not necessarily 4, but that's what the base game does, so
				{
					{ "160 px", 0 },
					{ "256 px", 1 },
					{ "288 px", 2 },
					{ "Transporter", 0xff }
				},
				(obj) => (obj.PropertyValue == 0xff) ? 0xff : (obj.PropertyValue & 3),
				(obj, value) => {
						byte val = (byte)((int)value);
						if (val == 0xff) // If we're becoming a Transporter, then reset everything
							obj.PropertyValue = val;
						else {
							if (obj.PropertyValue == 0xff)
							{
								// We *were* a transporter but now we're not, so let's reset the "path" variable (since it wasn't actually path before)
								obj.PropertyValue = val;
							}
							else {
								// We weren't a transporter before and we still aren't now, so let's keep our path value
								obj.PropertyValue = (byte)((obj.PropertyValue & ~3) | val);
							}
						}
					}
				);
			
			// Not that flexible but that just comes down to how the object works, nothin we can do about it
			properties[1] = new PropertySpec("Path", typeof(int), "Extended",
				"Which path this Tube should follow. Only used by Entry tubes.", null,
				(obj) => ((obj.PropertyValue == 0xff) ? -1 : ((obj.PropertyValue >> 2) & 0x0f)),
				(obj, value) => {
						if (obj.PropertyValue != 0xff) // don't set it if we're a transporter
							obj.PropertyValue = (byte)((obj.PropertyValue & ~(0x0f << 2)) | ((int)value & 0x0f) << 2);
					}
				);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return null;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return (subtype == 0xff) ? sprites[5] : sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			if (obj.PropertyValue != 0xff) // we're a main object, let's return the normal sprite
				return sprites[0];
			
			// if we're a transporter, let's offset ourselves as needed so that we're not stacked
			for (int i = 1; i <= 4; i++)
			{
				int index = LevelData.Objects.IndexOf(obj) - i;
				if (index < 0) break;
				if ((LevelData.Objects[index].Type == obj.Type) && (LevelData.Objects[index].PropertyValue != 0xff))
					return sprites[i];
			}
			
			// Looks like we're just a stray Transporter with no parent..
			return sprites[5];
		}
		
		// And now.. here we are
		private void InitDebugPaths()
		{
			// First, let's draw the enterances
			BitmapBits bitmap;
			int[] sizes = {160, 256, 288};
			for (int i = 0; i < sizes.Length; i++)
			{
				// To begin, just draw the enterance hitbox
				bitmap = new BitmapBits(sizes[i], 128);
				bitmap.DrawRectangle(6, 0, 0, sizes[i]-1, 127);
				Sprite rect = new Sprite(bitmap);
				
				Point endpointA;
				Point endpointB;
				
				// Now, let's draw the paths
				// Each enterance size has three different types - path a, path b, and path a/b (combining the first two)
				dbg_enterance[i, 0] = new Sprite(rect, DrawPath(paths_enterance, i << 2, 191, out endpointA));
				dbg_enterance[i, 1] = new Sprite(rect, DrawPath(paths_enterance, (i << 2) + 1, 175, out endpointB));
				dbg_enterance[i, 2] = new Sprite(dbg_enterance[i, 0], dbg_enterance[i, 1]);
				
				dbg_enterance_endpoint[i, 0] = new Point[] { endpointA };
				dbg_enterance_endpoint[i, 1] = new Point[] { endpointB };
				dbg_enterance_endpoint[i, 2] = new Point[] { endpointA, endpointB };
				
				//LevelData.Log("dbg_enterance_endpoint[" + i + ", 0]: " + dbg_enterance_endpoint[i, 0][0].ToString());
				//LevelData.Log("dbg_enterance_endpoint[" + i + ", 1]: " + dbg_enterance_endpoint[i, 1][0].ToString());
				
				//LevelData.Log("dbg_enterance[" + i + ", 0].Bounds: " + dbg_enterance[i, 0].Bounds.ToString());
				//LevelData.Log("dbg_enterance[" + i + ", 1].Bounds: " + dbg_enterance[i, 1].Bounds.ToString());
				//LevelData.Log("dbg_enterance[" + i + ", 2].Bounds: " + dbg_enterance[i, 2].Bounds.ToString());
			}
			
			// Now, let's go through every path ID and draw 'em out
			for (int i = 0; i < 16; i++)
			{
				int index = i;
				int transportPath = paths[index];
				
				index <<= 2;
				
				Sprite t;
				if (transportPath == 2) // random, draw both paths
				{
					Point endpointA;
					Point endpointB;
					t = DrawPaths(paths_tube, -nodeFlags[index], 191, out endpointA, -nodeFlags[index + 1], 175, out endpointB);
					dbg_path_startpoint[i] = new Point[] { endpointA, endpointB };
					
					//LevelData.Log("dbg_path_startpoint[" + i + "][0]: " + dbg_path_startpoint[i][0].ToString());
					//LevelData.Log("dbg_path_startpoint[" + i + "][1]: " + dbg_path_startpoint[i][1].ToString());
				}
				else
				{
					Point endpoint;
					t = DrawPath(paths_tube, -nodeFlags[index + transportPath], (byte)((transportPath == 0) ? 191 : 175), out endpoint);
					dbg_path_startpoint[i] = new Point[] { endpoint };
					
					//LevelData.Log("dbg_path_startpoint[" + i + "]: " + dbg_path_startpoint[i][0].ToString());
				}
				
				dbg_path[i] = t;
				//LevelData.Log("dbg_path[" + i + "].Bounds: " + dbg_path[i].Bounds.ToString());
			}
		}
		
		private Sprite[,] dbg_enterance = new Sprite[3, 3]; // entrance, transportPath - stores the enterance sprite
		private Sprite[] dbg_path = new Sprite[16]; // stores the main path sprite
		
		// Originally I was planning to draw a line between the endpoint of the enterance and the start of the path, but.. this object is complex enough, we don't need to make it worse
		// This is *mostly* functional if you uncomment it, but.. i feel like it's more effort than it's worth, not like this object is gonna be used often anyways haha
		private Point[,][] dbg_enterance_endpoint = new Point[3, 3][]; // stores the point where it connects to the main path
		private Point[][] dbg_path_startpoint = new Point[16][]; // stores the point where it connects to the enterance
		
		// All these values are copied from the original script, so if you modified those (really it would've just been easier to use Tube Path, but it's up to you..)
		// then you can just paste them here
		
		// Each value is dedicated to a pathID, it tells which index in nodeFlags the path should use
		// 2 means both, while 0/1 mean to use either the first or second value, respectively
		private static int[] paths = {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 2, 0, 1, 2, 1};
		
		// Each row is dedicated to a pathID
		// If it's negative, that means it goes through the path backwards
		private static int[] nodeFlags = {
			  2,   1, 0, 0,
			 -1,   3, 0, 0,
			  4,  -2, 0, 0,
			 -3,  -4, 0, 0,
			 -5,  -5, 0, 0,
			  7,   6, 0, 0,
			 -7,  -6, 0, 0,
			  8,   9, 0, 0,
			 -8,  -9, 0, 0,
			 11,  10, 0, 0,
			 12,   0, 0, 0,
			-11, -10, 0, 0,
			-12,   0, 0, 0,
			  0,  13, 0, 0,
			-13,  14, 0, 0,
			  0, -14, 0, 0
		};
		
		// These ones are relative, to the position of the Transport Tube
		// Every other entry is a duplicate of a previous table, but in reverse order - we don't use those here (but they're here anyways)
		private static int[][] paths_enterance = {
			new int[] { // TransportTube_transportPath1_0 - Size 160px, Path A entrance
			0x900000, 0x100000,
			0x900000, 0x700000,
			0x400000, 0x700000,
			0x350000, 0x6F0000,
			0x280000, 0x6A0000,
			0x1E0000, 0x620000,
			0x150000, 0x580000,
			0x110000, 0x4A0000,
			0x100000, 0x400000,
			0x110000, 0x350000,
			0x150000, 0x270000,
			0x1E0000, 0x1E0000,
			0x280000, 0x150000,
			0x350000, 0x110000,
			0x400000, 0x100000,
			0x500000, 0x100000,
			0x5E0000, 0x120000,
			0x680000, 0x180000,
			0x6D0000, 0x240000,
			0x700000, 0x300000,
			0x6D0000, 0x3D0000,
			0x680000, 0x480000,
			0x5E0000, 0x4E0000,
			0x500000, 0x500000,
			0x300000, 0x500000,
			0x220000, 0x520000,
			0x170000, 0x5A0000,
			0x110000, 0x630000,
			0x100000, 0x700000},
			new int[] { // TransportTube_transportPath1_1 - Size 160px, Path B entrance
			0x900000, 0x100000,
			0x900000, 0x700000,
			0x400000, 0x700000,
			0x2E0000, 0x6E0000,
			0x1D0000, 0x620000,
			0x130000, 0x530000,
			0x100000, 0x400000,
			0x130000, 0x2D0000,
			0x1D0000, 0x1E0000,
			0x2E0000, 0x130000,
			0x400000, 0x100000,
			0x580000, 0x100000,
			0x640000, 0x140000,
			0x6C0000, 0x1A0000,
			0x700000, 0x280000,
			0x6C0000, 0x360000,
			0x640000, 0x3C0000,
			0x580000, 0x400000,
			0x4B0000, 0x3D0000,
			0x400000, 0x380000,
			0x360000, 0x320000,
			0x280000, 0x300000,
			0x100000, 0x300000},
			new int[] { // TransportTube_transportPath1_2 - Size 160px, Path A exit (same as TransportTube_transportPath1_0, just in reverse)
			0x100000, 0x700000,
			0x110000, 0x630000,
			0x170000, 0x5A0000,
			0x220000, 0x520000,
			0x300000, 0x500000,
			0x500000, 0x500000,
			0x5E0000, 0x4E0000,
			0x680000, 0x480000,
			0x6D0000, 0x3D0000,
			0x700000, 0x300000,
			0x6D0000, 0x240000,
			0x680000, 0x180000,
			0x5E0000, 0x120000,
			0x500000, 0x100000,
			0x400000, 0x100000,
			0x350000, 0x110000,
			0x280000, 0x150000,
			0x1E0000, 0x1E0000,
			0x150000, 0x270000,
			0x110000, 0x350000,
			0x100000, 0x400000,
			0x110000, 0x4A0000,
			0x150000, 0x580000,
			0x1E0000, 0x620000,
			0x280000, 0x6A0000,
			0x350000, 0x6F0000,
			0x400000, 0x700000,
			0x900000, 0x700000,
			0x900000, 0x100000},
			new int[] { // TransportTube_transportPath1_3 - Size 160px, Path B exit (same as TransportTube_transportPath1_1, just in reverse)
			0x100000, 0x300000,
			0x280000, 0x300000,
			0x360000, 0x320000,
			0x400000, 0x380000,
			0x4B0000, 0x3D0000,
			0x580000, 0x400000,
			0x640000, 0x3C0000,
			0x6C0000, 0x360000,
			0x700000, 0x280000,
			0x6C0000, 0x1A0000,
			0x640000, 0x140000,
			0x580000, 0x100000,
			0x400000, 0x100000,
			0x2E0000, 0x130000,
			0x1D0000, 0x1E0000,
			0x130000, 0x2D0000,
			0x100000, 0x400000,
			0x130000, 0x530000,
			0x1D0000, 0x620000,
			0x2E0000, 0x6E0000,
			0x400000, 0x700000,
			0x900000, 0x700000,
			0x900000, 0x100000},
			new int[] { // TransportTube_transportPath1_4 - Size 256px, Path A entrance
			0x100000, 0x100000,
			0x100000, 0x700000,
			0xC00000, 0x700000,
			0xCA0000, 0x6F0000,
			0xD40000, 0x6C0000,
			0xDB0000, 0x680000,
			0xE30000, 0x620000,
			0xE80000, 0x5A0000,
			0xED0000, 0x520000,
			0xEF0000, 0x480000,
			0xF00000, 0x400000,
			0xEF0000, 0x360000,
			0xED0000, 0x2E0000,
			0xE80000, 0x260000,
			0xE30000, 0x1E0000,
			0xDB0000, 0x170000,
			0xD40000, 0x140000,
			0xCA0000, 0x120000,
			0xC00000, 0x100000,
			0xB70000, 0x110000,
			0xAF0000, 0x120000,
			0xA60000, 0x170000,
			0x9E0000, 0x1E0000,
			0x970000, 0x260000,
			0x930000, 0x2E0000,
			0x910000, 0x360000,
			0x900000, 0x400000,
			0x900000, 0x700000},
			new int[] { // TransportTube_transportPath1_5 - Size 256px, Path B entrance
			0x100000, 0x100000,
			0x100000, 0x700000,
			0xC00000, 0x700000,
			0xD20000, 0x6E0000,
			0xE30000, 0x620000,
			0xED0000, 0x530000,
			0xF00000, 0x400000,
			0xED0000, 0x2D0000,
			0xE30000, 0x1E0000,
			0xD20000, 0x130000,
			0xC00000, 0x100000,
			0xA80000, 0x100000,
			0x9C0000, 0x140000,
			0x940000, 0x1A0000,
			0x900000, 0x280000,
			0x940000, 0x360000,
			0x9C0000, 0x3C0000,
			0xA80000, 0x400000,
			0xB50000, 0x3D0000,
			0xC00000, 0x380000,
			0xCA0000, 0x320000,
			0xD80000, 0x300000,
			0xF00000, 0x300000},
			new int[] { // TransportTube_transportPath1_6 - Size 256px, Path A exit (same as TransportTube_transportPath1_4, just in reverse)
			0x900000, 0x700000,
			0x900000, 0x400000,
			0x910000, 0x360000,
			0x930000, 0x2E0000,
			0x970000, 0x260000,
			0x9E0000, 0x1E0000,
			0xA60000, 0x170000,
			0xAF0000, 0x120000,
			0xB70000, 0x110000,
			0xC00000, 0x100000,
			0xCA0000, 0x120000,
			0xD40000, 0x140000,
			0xDB0000, 0x170000,
			0xE30000, 0x1E0000,
			0xE80000, 0x260000,
			0xED0000, 0x2E0000,
			0xEF0000, 0x360000,
			0xF00000, 0x400000,
			0xEF0000, 0x480000,
			0xED0000, 0x520000,
			0xE80000, 0x5A0000,
			0xE30000, 0x620000,
			0xDB0000, 0x680000,
			0xD40000, 0x6C0000,
			0xCA0000, 0x6F0000,
			0xC00000, 0x700000,
			0x100000, 0x700000,
			0x100000, 0x100000},
			new int[] { // TransportTube_transportPath1_7 - Size 256px, Path B exit (same as TransportTube_transportPath1_5, just in reverse)
			0xF00000, 0x300000,
			0xD80000, 0x300000,
			0xCA0000, 0x320000,
			0xC00000, 0x380000,
			0xB50000, 0x3D0000,
			0xA80000, 0x400000,
			0x9C0000, 0x3C0000,
			0x940000, 0x360000,
			0x900000, 0x280000,
			0x940000, 0x1A0000,
			0x9C0000, 0x140000,
			0xA80000, 0x100000,
			0xC00000, 0x100000,
			0xD20000, 0x130000,
			0xE30000, 0x1E0000,
			0xED0000, 0x2D0000,
			0xF00000, 0x400000,
			0xED0000, 0x530000,
			0xE30000, 0x620000,
			0xD20000, 0x6E0000,
			0xC00000, 0x700000,
			0x100000, 0x700000,
			0x100000, 0x100000},
			new int[] { // TransportTube_transportPath1_8 - Size 288px, Path A entrance
			0x1100000, 0x100000,
			0x1100000, 0x700000,
			0x400000, 0x700000,
			0x350000, 0x6F0000,
			0x280000, 0x6A0000,
			0x1E0000, 0x620000,
			0x150000, 0x580000,
			0x110000, 0x4A0000,
			0x100000, 0x400000,
			0x110000, 0x350000,
			0x150000, 0x270000,
			0x1E0000, 0x1E0000,
			0x280000, 0x150000,
			0x350000, 0x110000,
			0x400000, 0x100000,
			0x500000, 0x100000,
			0x5E0000, 0x120000,
			0x680000, 0x180000,
			0x6D0000, 0x240000,
			0x700000, 0x300000,
			0x6D0000, 0x3D0000,
			0x680000, 0x480000,
			0x5E0000, 0x4E0000,
			0x500000, 0x500000,
			0x300000, 0x500000,
			0x220000, 0x520000,
			0x170000, 0x5A0000,
			0x110000, 0x630000,
			0x100000, 0x700000},
			new int[] { // TransportTube_transportPath1_9 - Size 288px, Path A entrance
			0x1100000, 0x100000,
			0x1100000, 0x700000,
			0x400000, 0x700000,
			0x2E0000, 0x6E0000,
			0x1D0000, 0x620000,
			0x130000, 0x530000,
			0x100000, 0x400000,
			0x130000, 0x2D0000,
			0x1D0000, 0x1E0000,
			0x2E0000, 0x130000,
			0x400000, 0x100000,
			0x580000, 0x100000,
			0x640000, 0x140000,
			0x6C0000, 0x1A0000,
			0x700000, 0x280000,
			0x6C0000, 0x360000,
			0x640000, 0x3C0000,
			0x580000, 0x400000,
			0x4B0000, 0x3D0000,
			0x400000, 0x380000,
			0x360000, 0x320000,
			0x280000, 0x300000,
			0x100000, 0x300000},
			new int[] { // TransportTube_transportPath1_10 - Size 288px, Path B exit (same as TransportTube_transportPath1_8, just in reverse)
			0x100000, 0x700000,
			0x110000, 0x630000,
			0x170000, 0x5A0000,
			0x220000, 0x520000,
			0x300000, 0x500000,
			0x500000, 0x500000,
			0x5E0000, 0x4E0000,
			0x680000, 0x480000,
			0x6D0000, 0x3D0000,
			0x700000, 0x300000,
			0x6D0000, 0x240000,
			0x680000, 0x180000,
			0x5E0000, 0x120000,
			0x500000, 0x100000,
			0x400000, 0x100000,
			0x350000, 0x110000,
			0x280000, 0x150000,
			0x1E0000, 0x1E0000,
			0x150000, 0x270000,
			0x110000, 0x350000,
			0x100000, 0x400000,
			0x110000, 0x4A0000,
			0x150000, 0x580000,
			0x1E0000, 0x620000,
			0x280000, 0x6A0000,
			0x350000, 0x6F0000,
			0x400000, 0x700000,
			0x1100000, 0x700000,
			0x1100000, 0x100000},
			new int[] { // TransportTube_transportPath1_11 - Size 288px, Path B exit (same as TransportTube_transportPath1_9, just in reverse)
			0x100000, 0x300000,
			0x280000, 0x300000,
			0x360000, 0x320000,
			0x400000, 0x380000,
			0x4B0000, 0x3D0000,
			0x580000, 0x400000,
			0x640000, 0x3C0000,
			0x6C0000, 0x360000,
			0x700000, 0x280000,
			0x6C0000, 0x1A0000,
			0x640000, 0x140000,
			0x580000, 0x100000,
			0x400000, 0x100000,
			0x2E0000, 0x130000,
			0x1D0000, 0x1E0000,
			0x130000, 0x2D0000,
			0x100000, 0x400000,
			0x130000, 0x530000,
			0x1D0000, 0x620000,
			0x2E0000, 0x6E0000,
			0x400000, 0x700000,
			0x1100000, 0x700000,
			0x1100000, 0x100000}
		};
		
		// On the other hand, these ones are absolute, or "relative" to (0,0)
		private static int[][] paths_tube = {
			new int[] { // TransportTube_transportPath2_0 (unused)
			0x7900000, 0x3B00000,
			0x7100000, 0x3B00000,
			0x7100000, 0x6B00000,
			0xA900000, 0x6B00000,
			0xA900000, 0x6700000},
			new int[] { // TransportTube_transportPath2_0
			0x7900000, 0x3B00000,
			0x7100000, 0x3B00000,
			0x7100000, 0x6B00000,
			0xA900000, 0x6B00000,
			0xA900000, 0x6700000},
			new int[] { // TransportTube_transportPath2_1
			0x7900000, 0x3F00000,
			0x7900000, 0x4B00000,
			0xA000000, 0x4B00000,
			0xC100000, 0x4B00000,
			0xC100000, 0x3300000,
			0xD900000, 0x3300000,
			0xD900000, 0x1B00000,
			0xF100000, 0x1B00000,
			0xF100000, 0x2B00000,
			0xF900000, 0x2B00000},
			new int[] { // TransportTube_transportPath2_2
			0xAF00000, 0x6300000,
			0xE900000, 0x6300000,
			0xE900000, 0x6B00000,
			0xF900000, 0x6B00000,
			0xF900000, 0x6700000},
			new int[] { // TransportTube_transportPath2_3
			0xF900000, 0x2F00000,
			0xF900000, 0x4B00000,
			0xF100000, 0x4B00000,
			0xF100000, 0x6300000,
			0xF900000, 0x6300000},
			new int[] { // TransportTube_transportPath2_4
			0x14100000, 0x5300000,
			0x11900000, 0x5300000,
			0x11900000, 0x6B00000,
			0x14100000, 0x6B00000,
			0x14100000, 0x5700000},
			new int[] { // TransportTube_transportPath2_5
			0x1AF00000, 0x5300000,
			0x1B900000, 0x5300000,
			0x1B900000, 0x3300000,
			0x1E100000, 0x3300000},
			new int[] { // TransportTube_transportPath2_6
			0x1A900000, 0x5700000,
			0x1A900000, 0x5B00000,
			0x1C100000, 0x5B00000,
			0x1C100000, 0x4300000,
			0x1E100000, 0x4300000,
			0x1E100000, 0x3700000},
			new int[] { // TransportTube_transportPath2_7
			0x24900000, 0x3700000,
			0x24900000, 0x3D00000,
			0x23900000, 0x3D00000,
			0x23900000, 0x5D00000,
			0x25100000, 0x5D00000,
			0x25100000, 0x5700000},
			new int[] { // TransportTube_transportPath2_8
			0x24F00000, 0x3300000,
			0x25900000, 0x3300000,
			0x25900000, 0x5300000,
			0x25700000, 0x5300000},
			new int[] { // TransportTube_transportPath2_9
			0x3100000, 0x3300000,
			0x2900000, 0x3300000,
			0x2900000, 0x2300000,
			0x4900000, 0x2300000},
			new int[] { // TransportTube_transportPath2_10
			0x3100000, 0x3700000,
			0x3100000, 0x3B00000,
			0x4100000, 0x3B00000,
			0x4100000, 0x2B00000,
			0x4900000, 0x2B00000,
			0x4900000, 0x2700000},
			new int[] { // TransportTube_transportPath2_11
			0x4900000, 0x6F00000,
			0x4900000, 0x7300000,
			0x6900000, 0x7300000,
			0x8900000, 0x7300000,
			0x8900000, 0x6F00000},
			new int[] { // TransportTube_transportPath2_12
			0xBF00000, 0x3300000,
			0xD900000, 0x3300000,
			0xD900000, 0x2F00000},
			new int[] { // TransportTube_transportPath2_13
			0xD900000, 0x2B00000,
			0xC900000, 0x2B00000,
			0xC900000, 0xB00000,
			0xE800000, 0xB00000,
			0x11100000, 0xB00000,
			0x11100000, 0x2300000,
			0x10F00000, 0x2300000}
		};
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if (obj.PropertyValue == 0xff)
				return null;
			
			int index = ((obj.PropertyValue >> 2) & 0x0f);
			int transportPath = paths[index];
			
			// Top left point we need to include
			// (dbg_enterance[obj.PropertyValue & 3, transportPath].X/Y is 0 so we leave 'em out here but pretend we're adding them to obj.X/Y)
			int xmin = Math.Min(obj.X, dbg_path[index].X);
			int ymin = Math.Min(obj.Y, dbg_path[index].Y);
			
			// Bottom right point we need to include
			int xmax = Math.Max(obj.X + dbg_enterance[obj.PropertyValue & 3, transportPath].Width, dbg_path[index].Right);
			int ymax = Math.Max(obj.Y + dbg_enterance[obj.PropertyValue & 3, transportPath].Height, dbg_path[index].Bottom);
			
			//LevelData.Log("xmin: " + xmin + ", xmax: " + xmax + ", ymin: " + ymin + ", ymax: " + ymax);
			
			// And now that we have our ranges.. let's construct our giant bitmap, and draw the sprites onto it
			BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			bitmap.DrawSprite(dbg_enterance[obj.PropertyValue & 3, transportPath], obj.X - xmin, obj.Y - ymin);
			bitmap.DrawSprite(dbg_path[index], -xmin, -ymin);
			
			//LevelData.Log("  Drawing enterance at: " + (obj.X - xmin) + ", " + (obj.Y - ymin));
			//LevelData.Log("  Drawing path at: " + (-xmin) + ", " + (-ymin));
			
			// Uncomment if you really wanna.. but it's not like you're missing out on much--
			// This would just connect the relative enterance path to the hardcoded/fixed transport path.. but honestly it's just more work than its worth imo
			// It works like 99% of the time and only breaks for one specific combo but i think it's important to not overcomplicate stuff anyways
			/*
			if (transportPath == 2)
			{
				var a = dbg_enterance_endpoint[obj.PropertyValue & 3, transportPath][0];
				var b = dbg_path_startpoint[index][0];
				LevelData.Log("  Drawing line1 from (" + (obj.X + a.X) + ", " + (obj.Y + a.Y) + ") to (" + b.X + ", " + b.X + ")");
				bitmap.DrawLine(191, obj.X + a.X - xmin, obj.Y + a.Y - ymin, b.X - xmin, b.Y - ymin);
				
				a = dbg_enterance_endpoint[obj.PropertyValue & 3, transportPath][1];
				b = dbg_path_startpoint[index][1];
				LevelData.Log("  Drawing line2 from (" + (obj.X + a.X) + ", " + (obj.Y + a.Y) + ") to (" + b.X + ", " + b.X + ")");
				bitmap.DrawLine(171, obj.X + a.X - xmin, obj.Y + a.Y - ymin, b.X - xmin, b.Y - ymin);
			}
			else
			{
				var a = dbg_enterance_endpoint[obj.PropertyValue & 3, transportPath][0];
				var b = dbg_path_startpoint[index][0];
				LevelData.Log("  Drawing line from (" + (obj.X + a.X) + ", " + (obj.Y + a.Y) + ") to (" + b.X + ", " + b.X + ")");
				bitmap.DrawLine((byte)((transportPath == 0) ? 191 : 175), obj.X + a.X - xmin, obj.Y + a.Y - ymin, b.X - xmin, b.Y - ymin);
			}
			*/
			
			return new Sprite(bitmap, -(obj.X - xmin), -(obj.Y - ymin));
		}
		
		private Sprite DrawPath(int[][] paths, int index, byte colour, out Point endpoint)
		{
			int[] path = paths[Math.Abs(index)];
			
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
				bitmap.DrawLine(colour, (path[i-2] >> 16) - xmin, (path[i-1] >> 16) - ymin, (path[i] >> 16) - xmin, (path[i+1] >> 16) - ymin);
			
			if (index > 0)
				endpoint = new Point(path[path.Length - 2] >> 16, path[path.Length - 1] >> 16); // The final two entries
			else
				endpoint = new Point(path[0] >> 16, path[1] >> 16); // The first two entries
			
			return new Sprite(bitmap, xmin, ymin);
		}
		
		private Sprite DrawPaths(int[][] paths, int indexA, byte colourA, out Point endpointA, int indexB, byte colourB, out Point endpointB)
		{
			int[] pathA = paths[Math.Abs(indexA)];
			int[] pathB = paths[Math.Abs(indexB)];
			
			int xmin = 0x7fff;
			int ymin = 0x7fff;
			int xmax = -0x7fff;
			int ymax = -0x7fff;
			
			for (int i = 0; i < pathA.Length; i += 2)
			{
				xmin = Math.Min(xmin, pathA[i] >> 16);
				ymin = Math.Min(ymin, pathA[i+1] >> 16);
				xmax = Math.Max(xmax, pathA[i] >> 16);
				ymax = Math.Max(ymax, pathA[i+1] >> 16);
			}
			
			for (int i = 0; i < pathB.Length; i += 2)
			{
				xmin = Math.Min(xmin, pathB[i] >> 16);
				ymin = Math.Min(ymin, pathB[i+1] >> 16);
				xmax = Math.Max(xmax, pathB[i] >> 16);
				ymax = Math.Max(ymax, pathB[i+1] >> 16);
			}
			
			BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			
			for (int i = 2; i < pathA.Length; i += 2)
				bitmap.DrawLine(colourA, (pathA[i-2] >> 16) - xmin, (pathA[i-1] >> 16) - ymin, (pathA[i] >> 16) - xmin, (pathA[i+1] >> 16) - ymin);
			
			if (indexA > 0)
				endpointA = new Point(pathA[pathA.Length - 2] >> 16, pathA[pathA.Length - 1] >> 16); // The final two entries
			else
				endpointA = new Point(pathA[0] >> 16, pathA[1] >> 16); // The first two entries
			
			for (int i = 2; i < pathB.Length; i += 2)
				bitmap.DrawLine(colourB, (pathB[i-2] >> 16) - xmin, (pathB[i-1] >> 16) - ymin, (pathB[i] >> 16) - xmin, (pathB[i+1] >> 16) - ymin);
			
			if (indexB > 0)
				endpointB = new Point(pathB[pathB.Length - 2] >> 16, pathB[pathB.Length - 1] >> 16); // The final two entries
			else
				endpointB = new Point(pathB[0] >> 16, pathB[1] >> 16); // The first two entries
			
			return new Sprite(bitmap, xmin, ymin);
		}
	}
}