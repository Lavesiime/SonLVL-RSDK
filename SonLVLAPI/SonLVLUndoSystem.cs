﻿using System.Collections.Generic;
using System.IO;
using System;

namespace SonicRetro.SonLVL.API
{
	public class SonLVLUndoSystem : UndoSystem
	{
		protected override void ApplyState(byte[] state)
		{
			using (var ms = new MemoryStream(state))
			{
				ms.ReadDeflateBlock(ds =>
				{
					for (var i = 0; i < LevelData.NewPalette.Length; i++)
						LevelData.NewPalette[i] = System.Drawing.Color.FromArgb(ds.ReadByte(), ds.ReadByte(), ds.ReadByte());
				});
				ms.ReadDeflateBlock(ds =>
				{
					for (var i = 0; i < LevelData.NewTiles.Length; i++)
						ds.Read(LevelData.NewTiles[i].Bits, 0, LevelData.NewTiles[i].Bits.Length);
				});
				ms.ReadDeflateBlock(ds =>
				{
					using (var reader = new RSDKv3_4.Reader(ds))
						LevelData.NewChunks.Read(reader);
				});
				ms.ReadDeflateBlock(ds =>
				{
					using (var reader = new RSDKv3_4.Reader(ds))
						LevelData.Collision.Read(reader);
				});
				ms.ReadDeflateBlock(ds =>
				{
					using (var reader = new RSDKv3_4.Reader(ds))
						LevelData.StageConfig.Read(reader);
				});
				ms.ReadDeflateBlock(ds =>
				{
					using (var reader = new RSDKv3_4.Reader(ds))
						LevelData.Scene.Read(reader);
				});

				// (only contains tilelayer data, no parallax data)
				ms.ReadDeflateBlock(ds =>
				{
					using (var reader = new RSDKv3_4.Reader(ds))
						LevelData.Background.Read(reader);
				});

				// Now, go ahead and read the BG scroll data
				ms.ReadDeflateBlock(ds =>
				{
					using (var reader = new RSDKv3_4.Reader(ds))
					{
						for (int i = 0; i < 8; i++)
						{
							var listCount = reader.ReadInt32();
							LevelData.BGScroll[i] = new List<ScrollData>(listCount);

							for (int j = 0; j < listCount; j++)
							{
								var scrollData = new ScrollData();
								scrollData.StartPos = reader.ReadUInt16();
								scrollData.Deform = reader.ReadBoolean();
								scrollData.ParallaxFactor = reader.ReadDecimal();
								scrollData.ScrollSpeed = reader.ReadDecimal();
								LevelData.BGScroll[i].Add(scrollData);
							}
						}
					}
				});

				foreach (var scn in LevelData.AdditionalScenes)
					ms.ReadDeflateBlock(ds =>
					{
						using (var reader = new RSDKv3_4.Reader(ds))
							scn.Scene.Read(reader);
					});
			}
		}

		protected override byte[] GetState()
		{
			using (var ms = new MemoryStream())
			{
				ms.WriteDeflateBlock(ds =>
				{
					for (var i = 0; i < LevelData.NewPalette.Length; i++)
					{
						ds.WriteByte(LevelData.NewPalette[i].R);
						ds.WriteByte(LevelData.NewPalette[i].G);
						ds.WriteByte(LevelData.NewPalette[i].B);
					}
				});
				ms.WriteDeflateBlock(ds =>
				{
					for (var i = 0; i < LevelData.NewTiles.Length; i++)
						ds.Write(LevelData.NewTiles[i].Bits, 0, LevelData.NewTiles[i].Bits.Length);
				});
				ms.WriteDeflateBlock(ds => LevelData.NewChunks.Write(ds));
				ms.WriteDeflateBlock(ds => LevelData.Collision.Write(ds));
				ms.WriteDeflateBlock(ds => LevelData.StageConfig.Write(ds));
				ms.WriteDeflateBlock(ds => LevelData.Scene.Write(ds));

				// First, write the BG tilelayer data
				ms.WriteDeflateBlock(ds => LevelData.Background.Write(ds));

				// Now, go ahead and write the BG scroll data
				// We don't want to save it as a standard RSDK BG file, since we want to preserve the decimal precision/keep entries unmerged
				// (Alternatively we could write all the data using RSDKv4.Backgrounds.ScrollInfo, but then we'd lose on decimal precision a little?)
				ms.WriteDeflateBlock(ds =>
				{
					using (var writer = new RSDKv3_4.Writer(ds))
					{
						for (int i = 0; i < 8; i++)
						{
							writer.Write(LevelData.BGScroll[i].Count);

							foreach (var scroll in LevelData.BGScroll[i])
							{
								writer.Write(scroll.StartPos);
								writer.Write(scroll.Deform);
								writer.Write(scroll.ParallaxFactor);
								writer.Write(scroll.ScrollSpeed);
							}
						}
					}
				});
				foreach (var scn in LevelData.AdditionalScenes)
					ms.WriteDeflateBlock(ds => scn.Scene.Write(ds));
				return ms.ToArray();
			}
		}
	}
}
