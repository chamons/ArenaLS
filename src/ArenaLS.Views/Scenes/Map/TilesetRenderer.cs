using SkiaSharp;
using System;

namespace ArenaLS.Views.Scenes.Map
{
	class TilesetRenderer
	{
		public const float Scale = 2f;

		public static SKSurface Render (MapLoader mapLoader)
		{
			var tilesetLoader = new TilesetLoader (mapLoader.TilesetName, mapLoader.TileSize);

			int backgroundHeight = (int)Math.Round (mapLoader.MapPixelHeight * Scale);
			int backgroundWidth = (int)Math.Round (mapLoader.MapPixelWidth * Scale);

			var background = SKSurface.Create (new SKImageInfo (backgroundHeight, backgroundWidth));

			DrawBackgroundLayer (background, mapLoader, tilesetLoader, 0);
			DrawBackgroundLayer (background, mapLoader, tilesetLoader, 1);

			return background;
		}

		static void DrawBackgroundLayer (SKSurface surface, MapLoader mapLoader, TilesetLoader tilesetLoader, int index)
		{
			var terrainTiles = mapLoader.GetTiles (index);

			for (int x = 0; x < terrainTiles.GetLength (0); ++x)
			{
				for (int y = 0; y < terrainTiles.GetLength (1); ++y)
				{
					int id = terrainTiles [x, y] - 1;
					var tilesetRect = tilesetLoader.GetRect (id);
					var renderRect = GetRenderRect (x, y, mapLoader.TileSize);

					surface.Canvas.DrawBitmap (tilesetLoader.Tileset, tilesetRect, renderRect);
				}
			}
		}

		static SKRect GetRenderRect (int x, int y, int tileSize)
		{
			float left = x * tileSize * Scale;
			float top = y * tileSize * Scale;
			float size = tileSize * Scale;

			return SKRect.Create (left, top, size, size);
		}
	}
}
