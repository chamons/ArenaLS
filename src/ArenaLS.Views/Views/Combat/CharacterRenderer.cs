using SkiaSharp;

namespace ArenaLS.Views.Views.Combat
{
	class CharacterRenderer
	{
		enum CharacterStyle { Normal, Double, ExtraLarge }
		const int BackgroundOffsetX = -5;
		const int BackgroundOffsetY = -15;
		static CharacterStyleInfo [] Styles = new CharacterStyleInfo [] {
			new CharacterStyleInfo (26, 36, 52, 72, 60, 30, 60 + BackgroundOffsetX, 30 + BackgroundOffsetY),
			new CharacterStyleInfo (52, 72, 52, 72, 60, 30, 60 + BackgroundOffsetX, 30 + BackgroundOffsetY),
			new CharacterStyleInfo (122, 114, 122, 114, 21, 140, 21 + BackgroundOffsetX, 140 + BackgroundOffsetY),
		};

		struct CharacterStyleInfo
		{
			public readonly int Width;
			public readonly int Height;
			public readonly int RenderWidth;
			public readonly int RenderHeight;
			public readonly int TextXOffset;
			public readonly int TextYOffset;
			public readonly int BackgroundXOffset;
			public readonly int BackgroundYOffset;

			public CharacterStyleInfo (int width, int height, int renderWidth, int renderHeight, int textXOffset, int textYOffset, int backgroundXOffset, int backgroundYOffset)
			{
				Width = width;
				Height = height;
				RenderWidth = renderWidth;
				RenderHeight = renderHeight;
				TextXOffset = textXOffset;
				TextYOffset = textYOffset;
				BackgroundXOffset = backgroundXOffset;
				BackgroundYOffset = backgroundYOffset;
			}
		}

		TilesetLoader CharacterLoader;
		TilesetLoader StatusIconLoader;
		int StartingID;
		CharacterStyleInfo StyleInfo;

		CharacterRenderer (TilesetLoader characterLoader, int startingID, CharacterStyleInfo styleInfo)
		{
			CharacterLoader = characterLoader;
			StatusIconLoader = new TilesetLoader ("data/tf_icon_32.png", 32);

			StartingID = startingID;
			StyleInfo = styleInfo;
		}

		static CharacterRenderer Create (string path, int startingID, CharacterStyleInfo style)
		{
			TilesetLoader loader = new TilesetLoader (path, style.Width, style.Height);
			return new CharacterRenderer (loader, startingID, style);
		}

		public static CharacterRenderer CreateNormalSized (string path, int startingID, bool doubleSize)
		{
			var styleInfo = doubleSize ? Styles [(int)CharacterStyle.Double] : Styles [(int)CharacterStyle.Normal];
			return Create (path, startingID, styleInfo);
		}

		public static CharacterRenderer CreateExtraLarge (string path, int startingID)
		{
			return Create (path, startingID, Styles [(int)CharacterStyle.ExtraLarge]);
		}

		readonly int [] FrameOffset = new int [] { 1, 0, 1, 2 };
		int CalculateAnimationOffset (long frame)
		{
			const int FramesBetweenAnimation = 6;
			return FrameOffset [(int)((frame / FramesBetweenAnimation) % 4)];		
		}

		readonly SKPaint TextPaint = new SKPaint () {
			TextSize = 14, Color = new SKColor (238, 238, 238),
			IsAntialias = true, IsAutohinted = true, StrokeWidth = 3
		};

		readonly SKPaint TextBackground = new SKPaint () { Color = SKColors.Gray.WithAlpha (160) };
		readonly SKPaint AntialiasPaint = new SKPaint () { IsAntialias = false };

		public void Render (SKCanvas canvas, int x, int y, long frame)
		{
			var tilesetRect = CharacterLoader.GetRect (StartingID + CalculateAnimationOffset (frame));
			canvas.DrawBitmap (CharacterLoader.Tileset, tilesetRect, SKRect.Create (x, y, StyleInfo.RenderWidth, StyleInfo.RenderHeight));
			canvas.DrawRect (SKRect.Create (x + StyleInfo.BackgroundXOffset, y + StyleInfo.BackgroundYOffset, 85, 60), TextBackground);
			canvas.DrawText ("NameName", new SKPoint (x + StyleInfo.TextXOffset, y + StyleInfo.TextYOffset), TextPaint);
			canvas.DrawText ("HP 999/888", new SKPoint (x + StyleInfo.TextXOffset, 18 + y + StyleInfo.TextYOffset), TextPaint);
			// TestData
			int iconOffset = 0;
			foreach (int i in new int [] { 33, 42, 148, 36 })
			{
				var bitmapRect = SKRect.Create (x + StyleInfo.TextXOffset + (iconOffset * 20), 24 + y + StyleInfo.TextYOffset, 16, 16);
				canvas.DrawBitmap (StatusIconLoader.Tileset, StatusIconLoader.GetRect (i), bitmapRect, AntialiasPaint);
				iconOffset++;
			}
		}
	}
}
