using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicRetro.SonLVL
{
	public class DrawTileUndoSystem : UndoSystem
	{
		// Stores a reference to the parent BitmapBits
		public BitmapBits bitmap;

		public DrawTileUndoSystem(BitmapBits bitmap) => this.bitmap = bitmap;

		// Using a MemoryStream with compression might be overdoing it.. but even if it might be overboard for 16x16 tiles,
		//  let's go ahead and keep it this way for chunks, since those are edited in this form too
		protected override void ApplyState(byte[] state)
		{
			using (var ms = new MemoryStream(state))
				ms.ReadDeflateBlock(ds => ds.Read(bitmap.Bits, 0, bitmap.Bits.Length));
		}

		protected override byte[] GetState()
		{
			using (var ms = new MemoryStream())
			{
				ms.WriteDeflateBlock(ds => ds.Write(bitmap.Bits, 0, bitmap.Bits.Length));
				return ms.ToArray();
			}
		}
	}
}
