using System;
using ArenaLS.Model;
using SkiaSharp;

namespace ArenaLS.Views.Views.Combat
{
	class HUDRenderer
	{
		CharacterStyleInfo StyleInfo;
		TilesetLoader StatusIconLoader;

		public HUDRenderer (CharacterStyleInfo styleInfo)
		{
			StyleInfo = styleInfo;
			StatusIconLoader = new TilesetLoader ("data/tf_icon_32.png", 32);
		}

		public void DrawHUD (SKCanvas canvas, Character c, int x, int y)
		{
			const int BackgroundOffsetX = 5;
			const int BackgroundOffsetY = 15;
			const int HUDWidth = 85;
			const int HUDHeight = 60;
			const int LineHeight = 18;
			const int StatusIconHeightGap = 4;
			const int StatusIconWidthGap = 20;

			canvas.DrawRect (SKRect.Create (x + StyleInfo.TextXOffset - BackgroundOffsetX, y + StyleInfo.TextYOffset - BackgroundOffsetY, HUDWidth, HUDHeight), Styles.TextBackground);
			canvas.DrawText (c.Name, new SKPoint (x + StyleInfo.TextXOffset, y + StyleInfo.TextYOffset), Styles.TextPaint);
			canvas.DrawText ($"HP {c.Health.Current}/{c.Health.Maximum}", new SKPoint (x + StyleInfo.TextXOffset, LineHeight + y + StyleInfo.TextYOffset), Styles.TextPaint);

			// TestData - Status Icons
			int iconOffset = 0;
			foreach (int i in new int [] { 33, 42, 148, 36 })
			{
				var bitmapRect = SKRect.Create (x + StyleInfo.TextXOffset + (iconOffset * StatusIconWidthGap), LineHeight + StatusIconHeightGap + y + StyleInfo.TextYOffset, 16, 16);
				canvas.DrawBitmap (StatusIconLoader.Tileset, StatusIconLoader.GetRect (i), bitmapRect, Styles.AntialiasPaint);
				iconOffset++;
			}
		}

		readonly SKPaint CastBarOutlinePaint = new SKPaint () { StrokeWidth = 2, Color = new SKColor (238, 238, 238), IsStroke = true };
		readonly SKPaint CastBarInsidePaint = new SKPaint () { StrokeWidth = 2, Color = new SKColor (44, 82, 178) };

		public void DrawCastbar (SKCanvas canvas, int x, int y, long frame)
		{
			const int TinyTextBackgroundOffsetX = 1;
			const int TinyTextBackgroundOffsetY = 8;
			const int CastbarHeight = 8;
			const int CastbarLength = 60;
			const int CastbarTextOffsetX = -12;
			const int CastbarTextOffsetY = 5;

			// TestData
			int percentCast = (int)((frame * 2) % 100);
			var castBarOutlineRect = SKRect.Create (x + StyleInfo.CastXOffset, y + StyleInfo.CastYOffset, CastbarLength, CastbarHeight);
			canvas.DrawRect (castBarOutlineRect, CastBarOutlinePaint);
			int filledLength = (int)Math.Round (((CastbarLength - 2.0) * percentCast) / 100);
			var castBarFilledRect = SKRect.Create (x + StyleInfo.CastXOffset + 1, y + StyleInfo.CastYOffset + 1, filledLength, CastbarHeight - 2);
			canvas.DrawRect (castBarFilledRect, CastBarInsidePaint);

			var castBarTextBackgroundRect = SKRect.Create (x + StyleInfo.CastXOffset - TinyTextBackgroundOffsetX - CastbarTextOffsetX, y + StyleInfo.CastYOffset - CastbarTextOffsetY - 1 - TinyTextBackgroundOffsetY, 42, 12);
			canvas.DrawRect (castBarTextBackgroundRect, Styles.TextBackground);
			var castBarTextRect = new SKPoint (x + StyleInfo.CastXOffset - CastbarTextOffsetX, y + StyleInfo.CastYOffset - CastbarTextOffsetY);
			canvas.DrawText ("Fire Strike", castBarTextRect, Styles.SmallTextPaint);
		}
	}
}
