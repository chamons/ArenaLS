using SkiaSharp;
using System;

namespace ArenaLS.Views.Scenes.Map
{
	class TilesetLoader
	{
		public SKBitmap Tileset { get; }
		readonly int TileSize;
		readonly int WidthInTiles;

		public TilesetLoader (string name, int tileSize)
		{
			Tileset = SKBitmap.Decode ($"maps/{name}.png");
			TileSize = tileSize;
			WidthInTiles = Tileset.Width / TileSize;
		}

		public SKRect GetRect (int id)
		{
			int column = id % WidthInTiles;
			int row = (int)Math.Floor ((double)id / (double)WidthInTiles);

			return SKRect.Create (TileSize * column, TileSize * row, TileSize, TileSize);
		}

		public SKRect GetRenderRect (int x, int y)
		{
			float left = x * TileSize;
			float top = y * TileSize;

			return SKRect.Create (left, top, TileSize, TileSize);
		}
	}
}