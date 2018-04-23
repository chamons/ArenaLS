using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaLS.Views.Scenes.Map
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

		const int CharacterAnimationFrames = 3;
		const int CharacterWidth = 26;
		const int CharacterHeight = 36;
		public static CharacterRenderer CreateNormalSized (string path, int startingID, bool doubleSize)
		{
			int width = doubleSize ? CharacterWidth * 2 : CharacterWidth;
			int height = doubleSize ? CharacterHeight * 2 : CharacterHeight;

			TilesetLoader loader = new TilesetLoader (path, width, height);
			return new CharacterRenderer (loader, startingID, CharacterWidth * 2, CharacterHeight * 2);
		}

		const int ExtreLargeMonsterWidth = 122;
		const int ExtreLargeMonsterHeight = 114;
		public static CharacterRenderer CreateExtraLarge (string path, int startingID)
		{
			TilesetLoader loader = new TilesetLoader (path, ExtreLargeMonsterWidth, ExtreLargeMonsterHeight);
			return new CharacterRenderer (loader, startingID, ExtreLargeMonsterWidth, ExtreLargeMonsterHeight);
		}

		const int FramesBetweenAnimation = 10;
		readonly int [] FrameOffset = new int [] { 1, 0, 1, 2 };

		int CalculateAnimationOffset (long frame)
		{
			return FrameOffset [(int)((frame / FramesBetweenAnimation) % 4)];		
		}

		public void Render (SKCanvas canvas, int x, int y, long frame)
		{
			var tilesetRect = Loader.GetRect (StartingID + CalculateAnimationOffset (frame));
			canvas.DrawBitmap (Loader.Tileset, tilesetRect, SKRect.Create (x, y, Width, Height));
		}
	}
}
