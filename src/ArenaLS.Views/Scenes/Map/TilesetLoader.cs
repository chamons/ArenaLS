using SkiaSharp;
using System;

namespace ArenaLS.Views.Scenes.Map
{
	class TilesetLoader
	{
		public SKBitmap Tileset { get; }
		readonly int TileSize;
		readonly int WidthInTiles;
		readonly int HeightInTiles;

		public TilesetLoader (string name, int tileSize)
		{
			Tileset = SKBitmap.Decode ($"maps/{name}.png");
			TileSize = tileSize;
			WidthInTiles = Tileset.Width / TileSize;
			HeightInTiles = Tileset.Height / TileSize;
		}

		public SKRect GetRect (int id)
		{
			int column = id % WidthInTiles;
			int row = (int)Math.Floor ((double)id / (double)WidthInTiles);

			return SKRect.Create (TileSize * column, TileSize * row, TileSize, TileSize);
		}
	}
}