using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;

namespace SonicRetro.SonLVL.API
{
	[Serializable]
	/// <summary>
	/// Represents the pixel data of an 8bpp Bitmap.
	/// </summary>
	public class BitmapBits32
	{
		public int[] Bits { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public Size Size { get { return new Size(Width, Height); } }
		public PixelFormat OriginalFormat { get; private set; }
		private readonly Color[] palette = new Color[256];
		private readonly Color[] waterPalette = new Color[256];
		public Color[] Palette => palette;
		public Color[] WaterPalette => waterPalette;
		public int WaterHeight { get; set; } = int.MaxValue;

		public int GetPixelIndex(int x, int y) => (y * Width) + x;

		public Color this[int x, int y]
		{
			get => Color.FromArgb(Bits[GetPixelIndex(x, y)]);
			set => Bits[GetPixelIndex(x, y)] = value.ToArgb();
		}

		public void SafeSetPixel(Color color, int x, int y)
		{
			if (x >= 0 && x < Width && y >= 0 && y < Height)
				this[x, y] = color;
		}

		public void BlendPixel(Color color, int x, int y)
		{
			switch (color.A)
			{
				case 255:
					SafeSetPixel(color, x, y);
					return;
				case 0:
					return;
			}
			if (x >= 0 && x < Width && y >= 0 && y < Height)
			{
				Color dstcol = this[x, y];
				this[x, y] = color.AlphaBlend(dstcol);
			}
		}

		public BitmapBits32(int width, int height)
		{
			Width = width;
			Height = height;
			Bits = new int[width * height];
			OriginalFormat = PixelFormat.Format32bppArgb;
		}

		public BitmapBits32(Size size)
			: this(size.Width, size.Height) { }

		public BitmapBits32(Bitmap bmp) => LoadBitmap(bmp);

		public BitmapBits32(string filename)
		{
			using (Bitmap bmp = new Bitmap(filename))
				LoadBitmap(bmp);
		}

		private void LoadBitmap(Bitmap bmp)
		{
			using (Bitmap b32 = bmp.To32bpp())
			{
				BitmapData bmpd = b32.LockBits(new Rectangle(0, 0, b32.Width, b32.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				Width = bmpd.Width;
				Height = bmpd.Height;
				Bits = new int[bmpd.Width * bmpd.Height];
				Marshal.Copy(bmpd.Scan0, Bits, 0, Width * Height);
				b32.UnlockBits(bmpd);
			}
			OriginalFormat = bmp.PixelFormat;
		}

		public BitmapBits32(BitmapBits32 source)
		{
			Width = source.Width;
			Height = source.Height;
			Bits = new int[source.Bits.Length];
			Array.Copy(source.Bits, Bits, Bits.Length);
			OriginalFormat = PixelFormat.Format32bppArgb;
		}

		public Bitmap ToBitmap()
		{
			if (Size.IsEmpty) return new Bitmap(1, 1, PixelFormat.Format32bppArgb);
			Bitmap newbmp = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
			ToBitmapInternal(newbmp);
			return newbmp;
		}

		public void ToBitmap(Bitmap destination)
		{
			if (destination.Width != Width)
				throw new ArgumentException("Width of destination bitmap must be equal to current bitmap.");
			if (destination.Height != Height)
				throw new ArgumentException("Height of destination bitmap must be equal to current bitmap.");
			if (destination.PixelFormat != PixelFormat.Format32bppArgb)
				throw new ArgumentException("Destination bitmap's pixel format must be 32bpp.");
			ToBitmapInternal(destination);
		}

		private void ToBitmapInternal(Bitmap destination)
		{
			BitmapData newbmpd = destination.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			Marshal.Copy(Bits, 0, newbmpd.Scan0, Bits.Length);
			destination.UnlockBits(newbmpd);
		}

		public void DrawBitmap(BitmapBits32 source, int x, int y)
		{
			int srcl = 0;
			if (x < 0)
				srcl = -x;
			int srct = 0;
			if (y < 0)
				srct = -y;
			int srcr = source.Width;
			if (srcr > Width - x)
				srcr = Width - x;
			int srcb = source.Height;
			if (srcb > Height - y)
				srcb = Height - y;
			for (int c = srct; c < srcb; c++)
				for (int r = srcl; r < srcr; r++)
					BlendPixel(source[r, c], x + r, y + c);
		}

		public void DrawBitmap(BitmapBits32 source, Point location) => DrawBitmap(source, location.X, location.Y);

		public void DrawBitmap(BitmapBits source, int x, int y)
		{
			int srcl = Math.Max(0, -x);
			int srct = Math.Max(0, -y);
			int srcr = Math.Min(source.Width, Width - x);
			int srcb = Math.Min(source.Height, Height - y);

			// Nothing to draw, nope
			if (srcl >= srcr || srct >= srcb)
				return;

			Color[] pal = palette;

			// The this[] indexer isn't inherently slow, but calling it for every pixel in both images' area adds up very quickly
			// So, instead, let's access Bits directly, and keep track of the indexes ourselves instead of recalcuating them for every pixel
			for (int srcY = srct, destY = y + srct; srcY < srcb; srcY++, destY++)
			{
				if (destY + y >= WaterHeight)
					pal = waterPalette;

				// Calculate base indexes for the row
				int srcBase = srcY * source.Width;
				int dstBase = destY * Width;

				// Now, loop through every column within the row
				for (int srcX = srcl, destX = x + srcl; srcX < srcr; srcX++, destX++)
				{
					var pixel = source.Bits[srcBase + srcX];
					if (pixel != 0)
						Bits[dstBase + destX] = pal[pixel].ToArgb();
				}
			}
		}

		public void DrawBitmap(BitmapBits source, Point location) => DrawBitmap(source, location.X, location.Y);

		public void DrawSprite(Sprite sprite, int x, int y)
		{
			foreach (PixelStrip strip in sprite.Strips)
				DrawStrip(strip, x, y);
		}

		public void DrawSprite(Sprite sprite, Point location) => DrawSprite(sprite, location.X, location.Y);

		public void DrawSprite(Sprite sprite) => DrawSprite(sprite, 0, 0);

		public void DrawSpriteLow(Sprite sprite, int x, int y)
		{
			foreach (PixelStrip strip in sprite.Strips.Where(a => a.Priority == false))
				DrawStrip(strip, x, y);
		}

		public void DrawSpriteLow(Sprite sprite, Point location) => DrawSpriteLow(sprite, location.X, location.Y);

		public void DrawSpriteLow(Sprite sprite) => DrawSpriteLow(sprite, 0, 0);

		public void DrawSpriteHigh(Sprite sprite, int x, int y)
		{
			foreach (PixelStrip strip in sprite.Strips.Where(a => a.Priority == true))
				DrawStrip(strip, x, y);
		}

		public void DrawSpriteHigh(Sprite sprite, Point location) => DrawSpriteHigh(sprite, location.X, location.Y);

		public void DrawSpriteHigh(Sprite sprite) => DrawSpriteHigh(sprite, 0, 0);

		private void DrawStrip(PixelStrip strip, int x, int y)
		{
			int sty = strip.Y + y;
			if (sty < 0 || sty >= Height) return;
			int stx = strip.X + x;
			int srcl = Math.Max(0, -stx);
			int srcr = Math.Min(strip.Width, Width - stx);
			Color[] pal = sty >= WaterHeight ? waterPalette : palette;
			int dst = (sty * Width) + stx;
			for (int r = srcl; r < srcr; r++)
				Bits[dst + r] = pal[strip.Pixels[r]].ToArgb();
		}

		public void ClearSpriteLow(Sprite sprite, int x, int y)
		{
			foreach (PixelStrip strip in sprite.Strips.Where(a => a.Priority == false))
				ClearStrip(strip, x, y);
		}

		private void ClearStrip(PixelStrip strip, int x, int y)
		{
			int sty = strip.Y + y;
			if (sty < 0 || sty >= Height) return;
			int stx = strip.X + x;
			int srcl = 0;
			if (stx < 0)
				srcl = -stx;
			int srcr = strip.Width;
			if (srcr > Width - stx)
				srcr = Width - stx;
			if (srcr > srcl)
				Array.Clear(Bits, GetPixelIndex(stx + srcl, sty), srcr - srcl);
		}

		public void ReplaceColor(Color old, Color @new)
		{
			int oi = old.ToArgb();
			int ni = @new.ToArgb();
			for (int i = 0; i < Bits.Length; i++)
				if (Bits[i] == oi)
					Bits[i] = ni;
		}

		public void Flip(bool XFlip, bool YFlip)
		{
			if (!XFlip & !YFlip)
				return;
			if (XFlip)
			{
				for (int y = 0; y < Height; y++)
				{
					int addr = y * Width;
					Array.Reverse(Bits, addr, Width);
				}
			}
			if (YFlip)
			{
				int[] tmppix = new int[Bits.Length];
				for (int y = 0; y < Height; y++)
				{
					int srcaddr = y * Width;
					int dstaddr = (Height - y - 1) * Width;
					Array.Copy(Bits, srcaddr, tmppix, dstaddr, Width);
				}
				Bits = tmppix;
			}
		}

		public void Flip(RSDKv3_4.Tiles128x128.Block.Tile.Directions dir)
		{
			switch (dir)
			{
				case RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX:
					Flip(true, false);
					break;
				case RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY:
					Flip(false, true);
					break;
				case RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipXY:
					Flip(true, true);
					break;
			}
		}

		public void Clear()
		{
			Array.Clear(Bits, 0, Bits.Length);
		}

		public void Clear(Color color)
		{
			Bits.FastFill(color.ToArgb());
		}

		public void Rotate(int R)
		{
			int[] tmppix = new int[Bits.Length];
			switch (R & 3)
			{
				case 1:
					for (int y = 0; y < Height; y++)
					{
						int srcaddr = y * Width;
						int dstaddr = (Width * (Width - 1)) + y;
						for (int x = 0; x < Width; x++)
						{
							tmppix[dstaddr] = Bits[srcaddr + x];
							dstaddr -= Width;
						}
					}
					Bits = tmppix;
					int h = Height;
					Height = Width;
					Width = h;
					break;
				case 2:
					Flip(true, true);
					break;
				case 3:
					for (int y = 0; y < Height; y++)
					{
						int srcaddr = y * Width;
						int dstaddr = Height - 1 - y;
						for (int x = 0; x < Width; x++)
						{
							tmppix[dstaddr] = Bits[srcaddr + x];
							dstaddr += Width;
						}
					}
					Bits = tmppix;
					h = Height;
					Height = Width;
					Width = h;
					break;
			}
		}

		public BitmapBits32 Scale(int factor)
		{
			if (factor < 1)
				throw new ArgumentOutOfRangeException("factor", "Scaling factor must be 1 or greater.");
			if (factor == 1)
				return new BitmapBits32(this);
			BitmapBits32 res = new BitmapBits32(Width * factor, Height * factor);
			int srcaddr = 0;
			int dstaddr = 0;
			for (int y = 0; y < Height; y++)
			{
				int linestart = dstaddr;
				for (int x = 0; x < Width; x++)
				{
					res.Bits.FastFill(Bits[srcaddr++], dstaddr, factor);
					dstaddr += factor;
				}
				for (int i = 1; i < factor; i++)
				{
					Array.Copy(res.Bits, linestart, res.Bits, dstaddr, res.Width);
					dstaddr += res.Width;
				}
			}
			return res;
		}

		public void DrawLine(Color color, int x1, int y1, int x2, int y2)
		{
			if (color.A == 255)
			{
				if (y1 == y2)
				{
					if (y1 >= Height || y1 < 0)
						return;
					if (x1 > x2)
					{
						int tmp = x1;
						x1 = x2;
						x2 = tmp;
					}
					if (x1 >= Width || x2 < 0)
						return;
					x1 = Math.Max(x1, 0);
					x2 = Math.Min(x2, Width - 1);
					Bits.FastFill(color.ToArgb(), GetPixelIndex(x1, y1), x2 - x1 + 1);
					return;
				}
				if (x1 == x2)
				{
					if (x1 >= Width || x1 < 0)
						return;
					if (y1 > y2)
					{
						int tmp = y1;
						y1 = y2;
						y2 = tmp;
					}
					if (y1 >= Height || y2 < 0)
						return;
					y1 = Math.Max(y1, 0);
					y2 = Math.Min(y2, Height - 1);
					int end = GetPixelIndex(x1, y2);
					for (int i = GetPixelIndex(x1, y1); i <= end; i += Width)
						Bits[i] = color.ToArgb();
					return;
				}
				bool steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);
				if (steep)
				{
					int tmp = x1;
					x1 = y1;
					y1 = tmp;
					tmp = x2;
					x2 = y2;
					y2 = tmp;
				}
				if (x1 > x2)
				{
					int tmp = x1;
					x1 = x2;
					x2 = tmp;
					tmp = y1;
					y1 = y2;
					y2 = tmp;
				}
				int deltax = x2 - x1;
				int deltay = Math.Abs(y2 - y1);
				double error = 0;
				double deltaerr = deltay / (double)deltax;
				int ystep;
				int y = y1;
				if (y1 < y2)
					ystep = 1;
				else
					ystep = -1;
				for (int x = x1; x <= x2; x++)
				{
					if (steep)
						SafeSetPixel(color, y, x);
					else
						SafeSetPixel(color, x, y);
					error += deltaerr;
					if (error >= 0.5)
					{
						y += ystep;
						error -= 1.0;
					}
				}
			}
			else if (color.A != 0)
			{
				bool steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);
				if (steep)
				{
					int tmp = x1;
					x1 = y1;
					y1 = tmp;
					tmp = x2;
					x2 = y2;
					y2 = tmp;
				}
				if (x1 > x2)
				{
					int tmp = x1;
					x1 = x2;
					x2 = tmp;
					tmp = y1;
					y1 = y2;
					y2 = tmp;
				}
				int deltax = x2 - x1;
				int deltay = Math.Abs(y2 - y1);
				double error = 0;
				double deltaerr = deltay / (double)deltax;
				int ystep;
				int y = y1;
				if (y1 < y2)
					ystep = 1;
				else
					ystep = -1;
				for (int x = x1; x <= x2; x++)
				{
					if (steep)
						BlendPixel(color, y, x);
					else
						BlendPixel(color, x, y);
					error += deltaerr;
					if (error >= 0.5)
					{
						y += ystep;
						error -= 1.0;
					}
				}
			}
		}

		public void DrawLine(Color color, Point p1, Point p2) => DrawLine(color, p1.X, p1.Y, p2.X, p2.Y);

		public void DrawRectangle(Color color, int x, int y, int width, int height)
		{
			DrawLine(color, x, y, x + width, y);
			DrawLine(color, x, y, x, y + height);
			DrawLine(color, x + width, y, x + width, y + height);
			DrawLine(color, x, y + height, x + width, y + height);
		}

		public void DrawRectangle(Color color, Rectangle rect) => DrawRectangle(color, rect.X, rect.Y, rect.Width, rect.Height);

		public void FillRectangle(Color color, int x, int y, int width, int height)
		{
			if (x > Width) return;
			if (y > Height) return;
			if (x + width <= 0) return;
			if (y + height <= 0) return;
			int srcl = Math.Max(x, 0);
			int srct = Math.Max(y, 0);
			int srcr = Math.Min(x + width, Width);
			int srcb = Math.Min(y + height, Height);
			int start = srct * Width + srcl;
			if (color.A == 255)
			{
				if (srcl == 0 && srcr == Width)
					Bits.FastFill(color.ToArgb(), start, srcb * Width - start);
				else
				{
					int length = srcr - srcl;
					for (int cy = srct; cy < srcb; cy++)
					{
						Bits.FastFill(color.ToArgb(), start, length);
						start += Width;
					}
				}
			}
			else if (color.A != 0)
			{
				int length = srcr - srcl;
				for (int cy = srct; cy < srcb; cy++)
				{
					for (int cx = 0; cx < length; cx++)
						Bits[start + cx] = color.AlphaBlend(Color.FromArgb(Bits[start + cx])).ToArgb();
					start += Width;
				}
			}
		}

		public void FillRectangle(Color color, Point loc, Size size) => FillRectangle(color, loc.X, loc.Y, size.Width, size.Height);

		public void FillRectangle(Color color, Rectangle rect) => FillRectangle(color, rect.X, rect.Y, rect.Width, rect.Height);

		public void DrawCircle(Color color, int x, int y, int radius)
		{
			int cx = -radius, cy = 0, err = 2 - 2 * radius; /* II. Quadrant */
			do
			{
				BlendPixel(color, x - cx, y + cy); /*   I. Quadrant */
				BlendPixel(color, x - cy, y - cx); /*  II. Quadrant */
				BlendPixel(color, x + cx, y - cy); /* III. Quadrant */
				BlendPixel(color, x + cy, y + cx); /*  IV. Quadrant */
				radius = err;
				if (radius <= cy) err += ++cy * 2 + 1;           /* e_xy+e_y < 0 */
				if (radius > cx || err > cy) err += ++cx * 2 + 1; /* e_xy+e_x > 0 or no 2nd y-step */
			} while (cx < 0);
		}

		public void DrawCircle(Color color, Point loc, int radius) => DrawCircle(color, loc.X, loc.Y, radius);

		public void DrawEllipse(Color color, int x1, int y1, int x2, int y2)
		{
			int a = Math.Abs(x2 - x1), b = Math.Abs(y2 - y1), b1 = b & 1; /* values of diameter */
			long dx = 4 * (1 - a) * b * b, dy = 4 * (b1 + 1) * a * a; /* error increment */
			long err = dx + dy + b1 * a * a, e2; /* error of 1.step */

			if (x1 > x2) { x1 = x2; x2 += a; } /* if called with swapped points */
			if (y1 > y2) y1 = y2; /* .. exchange them */
			y1 += (b + 1) / 2; y2 = y1 - b1;   /* starting pixel */
			a *= 8 * a; b1 = 8 * b * b;

			do
			{
				BlendPixel(color, x2, y1); /*   I. Quadrant */
				BlendPixel(color, x1, y1); /*  II. Quadrant */
				BlendPixel(color, x1, y2); /* III. Quadrant */
				BlendPixel(color, x2, y2); /*  IV. Quadrant */
				e2 = 2 * err;
				if (e2 <= dy) { y1++; y2--; err += dy += a; }  /* y step */
				if (e2 >= dx || 2 * err > dy) { x1++; x2--; err += dx += b1; } /* x step */
			} while (x1 <= x2);

			while (y1 - y2 < b)
			{  /* too early stop of flat ellipses a=1 */
				BlendPixel(color, x1 - 1, y1); /* -> finish tip of ellipse */
				BlendPixel(color, x2 + 1, y1++);
				BlendPixel(color, x1 - 1, y2);
				BlendPixel(color, x2 + 1, y2--);
			}
		}

		public void DrawEllipse(Color color, Point p1, Point p2) => DrawEllipse(color, p1.X, p1.Y, p2.X, p2.Y);

		public void DrawEllipse(Color color, Rectangle rect) => DrawEllipse(color, rect.Left, rect.Top, rect.Right, rect.Bottom);

		public override bool Equals(object obj)
		{
			if (base.Equals(obj)) return true;
			BitmapBits32 other = obj as BitmapBits32;
			if (other == null) return false;
			if (Width != other.Width | Height != other.Height) return false;
			return Bits.FastArrayEqual(other.Bits);
		}

		public void DrawBezier(Color color, int x1, int y1, int x2, int y2, int x3, int y3)
		{
			int sx = x3 - x2, sy = y3 - y2;
			long xx = x1 - x2, yy = y1 - y2, xy;         /* relative values for checks */
			double dx, dy, err, cur = xx * sy - yy * sx;                    /* curvature */

			if (xx * sx <= 0 && yy * sy <= 0) throw new ArgumentOutOfRangeException();  /* sign of gradient must not change */

			if (sx * (long)sx + sy * (long)sy > xx * xx + yy * yy)
			{ /* begin with longer part */
				x3 = x1; x1 = sx + x2; y3 = y1; y1 = sy + y2; cur = -cur;  /* swap P0 P2 */
			}
			if (cur != 0)
			{                                    /* no straight line */
				xx += sx; xx *= sx = x1 < x3 ? 1 : -1;           /* x step direction */
				yy += sy; yy *= sy = y1 < y3 ? 1 : -1;           /* y step direction */
				xy = 2 * xx * yy; xx *= xx; yy *= yy;          /* differences 2nd degree */
				if (cur * sx * sy < 0)
				{                           /* negated curvature? */
					xx = -xx; yy = -yy; xy = -xy; cur = -cur;
				}
				dx = 4.0 * sy * cur * (x2 - x1) + xx - xy;             /* differences 1st degree */
				dy = 4.0 * sx * cur * (y1 - y2) + yy - xy;
				xx += xx; yy += yy; err = dx + dy + xy;                /* error 1st step */
				do
				{
					BlendPixel(color, x1, y1);                                     /* plot curve */
					if (x1 == x3 && y1 == y3) return;  /* last pixel -> curve finished */
					y2 = (2 * err < dx) ? 1 : 0;                  /* save value for test of y step */
					if (2 * err > dy) { x1 += sx; dx -= xy; err += dy += yy; } /* x step */
					if (y2 != 0) { y1 += sy; dy -= xy; err += dx += xx; } /* y step */
				} while (dy < dx);           /* gradient negates -> algorithm fails */
			}
			DrawLine(color, x1, y1, x3, y3);                  /* plot remaining part to end */
		}

		public void DrawBezier(Color color, Point p1, Point p2, Point p3) => DrawBezier(color, p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y);

		public void DrawGraphX(Color color, int xmin, int xmax, int yoff, Func<int, int> func)
		{
			Point? last = null;
			for (int x = xmin; x <= xmax; x++)
			{
				Point cur = new Point(x, func(x) + yoff);
				if (last.HasValue)
					DrawLine(color, last.Value, cur);
				BlendPixel(color, x, cur.Y);
				last = cur;
			}
		}

		public void DrawGraphY(Color color, int ymin, int ymax, int xoff, Func<int, int> func)
		{
			Point? last = null;
			for (int y = ymin; y <= ymax; y++)
			{
				Point cur = new Point(func(y) + xoff, y);
				if (last.HasValue)
					DrawLine(color, last.Value, cur);
				BlendPixel(color, cur.X, y);
				last = cur;
			}
		}

		public override int GetHashCode() => base.GetHashCode();

		public BitmapBits32 GetSection(int x, int y, int width, int height)
		{
			BitmapBits32 result = new BitmapBits32(width, height);
			for (int v = 0; v < height; v++)
				Array.Copy(Bits, GetPixelIndex(x, y + v), result.Bits, v * width, width);
			return result;
		}

		public BitmapBits32 GetSection(Rectangle rect) => GetSection(rect.X, rect.Y, rect.Width, rect.Height);

		/// <summary>
		/// Scrolls the image horizontally to the left by <paramref name="amount"/> pixels.
		/// </summary>
		/// <param name="amount">The number of pixels to scroll by. Positive is left, negative is right.</param>
		public void ScrollHorizontal(int amount)
		{
			int[] newBits = new int[Bits.Length];
			amount %= Width;
			if (amount < 0)
				amount += Width;
			if (amount == 0) return;
			int copy1src = amount;
			int copy1dst = 0;
			int copy1len = Width - amount;
			int copy2src = 0;
			int copy2dst = Width - amount;
			int copy2len = amount;
			for (int y = 0; y < Height; y++)
			{
				Array.Copy(Bits, copy1src, newBits, copy1dst, copy1len);
				Array.Copy(Bits, copy2src, newBits, copy2dst, copy2len);
				copy1src += Width;
				copy1dst += Width;
				copy2src += Width;
				copy2dst += Width;
			}
			Bits = newBits;
		}

		/// <summary>
		/// Scrolls each row in the image horizontally to the left by <paramref name="amounts"/> pixels.
		/// </summary>
		/// <param name="amounts">The number of pixels to scroll each row by. Positive is left, negative is right.</param>
		public void ScrollHorizontal(params int[] amounts)
		{
			int[] newBits = new int[Bits.Length];
			for (int i = 0; i < amounts.Length; i++)
			{
				amounts[i] %= Width;
				if (amounts[i] < 0)
					amounts[i] += Width;
			}
			int rowStart = 0;
			for (int y = 0; y < Height; y++)
			{
				int amount = amounts[y % amounts.Length];
				Array.Copy(Bits, rowStart + amount, newBits, rowStart, Width - amount);
				if (amount != 0)
					Array.Copy(Bits, rowStart, newBits, rowStart + Width - amount, amount);
				rowStart += Width;
			}
			Bits = newBits;
		}

		/// <summary>
		/// Scrolls the image vertically upwards by <paramref name="amount"/> pixels.
		/// </summary>
		/// <param name="amount">The number of pixels to scroll by. Positive is up, negative is down.</param>
		public void ScrollVertical(int amount)
		{
			int[] newBits = new int[Bits.Length];
			amount %= Height;
			if (amount < 0)
				amount += Height;
			if (amount == 0) return;
			int src = amount * Width;
			int dst = 0;
			for (int y = 0; y < Height; y++)
			{
				Array.Copy(Bits, src, newBits, dst, Width);
				src = (src + Width) % Bits.Length;
				dst += Width;
			}
			Bits = newBits;
		}

		/// <summary>
		/// Scrolls each column in the image vertically upwards by <paramref name="amounts"/> pixels.
		/// </summary>
		/// <param name="amounts">The number of pixels to scroll each column by. Positive is up, negative is down.</param>
		public void ScrollVertical(params int[] amounts)
		{
			int[] newBits = new int[Bits.Length];
			for (int i = 0; i < amounts.Length; i++)
			{
				amounts[i] %= Height;
				if (amounts[i] < 0)
					amounts[i] += Height;
			}
			for (int x = 0; x < Width; x++)
			{
				int amount = amounts[x % amounts.Length];
				int src = GetPixelIndex(x, amount);
				int dst = x;
				for (int y = 0; y < Height; y++)
				{
					newBits[dst] = Bits[src];
					src = (src + Width) % Bits.Length;
					dst += Width;
				}
			}
			Bits = newBits;
		}

		public void ScrollHV(BitmapBits32 destination, int dstY, int srcY, params int[] srcX)
		{
			if (dstY < 0 || dstY >= destination.Height) return;
			for (int i = 0; i < srcX.Length; i++)
			{
				srcX[i] %= Width;
				if (srcX[i] < 0)
					srcX[i] += Width;
			}
			srcY %= Height;
			if (srcY < 0)
				srcY += Height;
			int rowSrc = GetPixelIndex(0, srcY);
			int rowDst = destination.GetPixelIndex(0, dstY);
			for (int y = 0; y < destination.Height - dstY; y++)
			{
				int amount = srcX[(srcY + y) % srcX.Length];
				Array.Copy(Bits, rowSrc + amount, destination.Bits, rowDst, Math.Min(Width - amount, destination.Width));
				if (amount != 0 && Width - amount < destination.Width)
					Array.Copy(Bits, rowSrc, destination.Bits, rowDst + Width - amount, Math.Min(amount, destination.Width - (Width - amount)));
				rowSrc = (rowSrc + Width) % Bits.Length;
				rowDst += destination.Width;
			}
		}

		public void ScrollVH(BitmapBits32 destination, int dstX, int srcX, params int[] srcY)
		{
			if (dstX < 0 || dstX >= destination.Width)
				return;

			for (int i = 0; i < srcY.Length; i++)
			{
				srcY[i] %= Height;
				if (srcY[i] < 0)
					srcY[i] += Height;
			}

			srcX %= Width;
			if (srcX < 0)
				srcX += Width;

			int copyWidth = destination.Width - dstX;
			for (int dx = 0; dx < copyWidth; dx++)
			{
				int srcBase = srcX + dx;
				if (srcBase >= Width)
					srcBase -= Width;

				int destBase = dx + dstX;
				int sy = srcY[(srcX + dx) % srcY.Length];
				for (int y = 0; y < destination.Height; y++)
				{
					if (sy >= Height) sy -= Height;
					destination.Bits[destBase + y * destination.Width] = Bits[srcBase + (sy * Width)];
					sy++;
				}
			}
		}

		public void FloodFill(Color color, int x, int y) => FloodFill(color, new Point(x, y));

		public void FloodFill(Color color, Point location)
		{
			Color oldind = this[location.X, location.Y];
			if (oldind == color) return;
			Queue<Point> pts = new Queue<Point>(Bits.Length / 2);
			pts.Enqueue(location);
			this[location.X, location.Y] = color;
			while (pts.Count > 0)
			{
				Point pt = pts.Dequeue();
				if (pt.X > 0 && this[pt.X - 1, pt.Y] == oldind)
				{
					this[pt.X - 1, pt.Y] = color;
					pts.Enqueue(new Point(pt.X - 1, pt.Y));
				}
				if (pt.X < Width - 1 && this[pt.X + 1, pt.Y] == oldind)
				{
					this[pt.X + 1, pt.Y] = color;
					pts.Enqueue(new Point(pt.X + 1, pt.Y));
				}
				if (pt.Y > 0 && this[pt.X, pt.Y - 1] == oldind)
				{
					this[pt.X, pt.Y - 1] = color;
					pts.Enqueue(new Point(pt.X, pt.Y - 1));
				}
				if (pt.Y < Height - 1 && this[pt.X, pt.Y + 1] == oldind)
				{
					this[pt.X, pt.Y + 1] = color;
					pts.Enqueue(new Point(pt.X, pt.Y + 1));
				}
			}
		}

	}
}
