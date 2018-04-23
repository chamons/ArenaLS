using SkiaSharp;

namespace ArenaLS.Views.Views.Combat
{
	class CharacterRenderer
	{
		TilesetLoader Loader;
		int StartingID;
		int Width;
		int Height;

		CharacterRenderer (TilesetLoader loader, int startingID, int width, int height)
		{
			Loader = loader;
			StartingID = startingID;
			Width = width;
			Height = height;
		}

		public static CharacterRenderer CreateNormalSized (string path, int startingID, bool doubleSize)
		{
			const int CharacterWidth = 26;
			const int CharacterHeight = 36;

			int width = doubleSize ? CharacterWidth * 2 : CharacterWidth;
			int height = doubleSize ? CharacterHeight * 2 : CharacterHeight;

			TilesetLoader loader = new TilesetLoader (path, width, height);
			return new CharacterRenderer (loader, startingID, CharacterWidth * 2, CharacterHeight * 2);
		}

		public static CharacterRenderer CreateExtraLarge (string path, int startingID)
		{
			const int ExtreLargeMonsterWidth = 122;
			const int ExtreLargeMonsterHeight = 114;

			TilesetLoader loader = new TilesetLoader (path, ExtreLargeMonsterWidth, ExtreLargeMonsterHeight);
			return new CharacterRenderer (loader, startingID, ExtreLargeMonsterWidth, ExtreLargeMonsterHeight);
		}

		readonly int [] FrameOffset = new int [] { 1, 0, 1, 2 };
		int CalculateAnimationOffset (long frame)
		{
			const int FramesBetweenAnimation = 10;

			return FrameOffset [(int)((frame / FramesBetweenAnimation) % 4)];		
		}

		public void Render (SKCanvas canvas, int x, int y, long frame)
		{
			var tilesetRect = Loader.GetRect (StartingID + CalculateAnimationOffset (frame));
			canvas.DrawBitmap (Loader.Tileset, tilesetRect, SKRect.Create (x, y, Width, Height));
		}
	}
}
