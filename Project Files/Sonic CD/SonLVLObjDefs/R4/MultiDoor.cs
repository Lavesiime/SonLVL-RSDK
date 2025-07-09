using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class MultiDoor : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			Sprite[] blocks = new Sprite[2];
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A': // Present
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
					blocks[0] = new Sprite(sheet.GetSection(163, 1, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(196, 1, 32, 32));
					break;
				case 'B': // Past
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 157, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 157, 32, 32));
					break;
				case 'C': // Good Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 190, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 190, 32, 32));
					break;
				case 'D': // Bad Future
					sheet = LevelData.GetSpriteSheet("R4/Objects2.gif");
					blocks[0] = new Sprite(sheet.GetSection(1, 223, 32, 32));
					blocks[1] = new Sprite(sheet.GetSection(34, 223, 32, 32));
					break;
			}
			
			Sprite[] frames = {
				new Sprite(blocks[0], -16, -64),
				new Sprite(blocks[0], -16, -32),
				new Sprite(blocks[0], -16, 0),
				new Sprite(blocks[0], -16, 32),
				new Sprite(blocks[1], -16, -64),
				new Sprite(blocks[1], -16, -32),
				new Sprite(blocks[1], -16, 0),
				new Sprite(blocks[1], -16, 32)};
			
			Sprite[] door = {
				new Sprite(frames[0], -48, 0),
				new Sprite(frames[1], -48, 0),
				new Sprite(frames[2], -48, 0),
				new Sprite(frames[3], -48, 0),
				new Sprite(frames[4], -16, 0),
				new Sprite(frames[5], -16, 0),
				new Sprite(frames[6], -16, 0),
				new Sprite(frames[7], -16, 0),
				new Sprite(frames[0],  16, 0),
				new Sprite(frames[1],  16, 0),
				new Sprite(frames[2],  16, 0),
				new Sprite(frames[3],  16, 0),
				new Sprite(frames[4],  48, 0),
				new Sprite(frames[5],  48, 0),
				new Sprite(frames[6],  48, 0),
				new Sprite(frames[7],  48, 0)};
			
			sprite = new Sprite(door);
			
			BitmapBits bitmap = new BitmapBits(32, 128);
			bitmap.DrawRectangle(6, 0, 0, 31, 127); // LevelData.ColorWhite
			Sprite[] dbg = {
				new Sprite(bitmap, -16 - 48, -64 - 128),
				new Sprite(bitmap, -16 - 16, -64 + 128),
				new Sprite(bitmap, -16 + 16, -64 - 128),
				new Sprite(bitmap, -16 + 48, -64 + 128)};
			debug = new Sprite(dbg);
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
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}