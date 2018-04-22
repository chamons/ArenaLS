using SkiaSharp;

using ArenaLS.Views.Scenes.Map;

namespace ArenaLS.Views.Scenes
{
	class CombatScene : IScene
	{
		GameController Controller;
		SKSurface Background;

		public CombatScene (GameController controller)
		{
			Controller = controller;
		}

		public void Load (string mapName)
		{
			var mapLoader = new MapLoader ($"maps/{mapName}.tmx");
			var tilesetLoader = new TilesetLoader (mapLoader.TilesetName, mapLoader.TileSize);

			Background = SKSurface.Create (new SKImageInfo (mapLoader.MapPixelWidth, mapLoader.MapPixelHeight));

			DrawBackgroundLayer (mapLoader, tilesetLoader, 0);
			DrawBackgroundLayer (mapLoader, tilesetLoader, 1);
		}

		void DrawBackgroundLayer (MapLoader mapLoader, TilesetLoader tilesetLoader, int index)
		{
			var terrainTiles = mapLoader.GetTiles (index);

			for (int x = 0; x < terrainTiles.GetLength (0); ++x)
			{
				for (int y = 0; y < terrainTiles.GetLength (1); ++y)
				{
					int id = terrainTiles [x, y] - 1;
					var tilesetRect = tilesetLoader.GetRect (id);
					var renderRect = tilesetLoader.GetRenderRect (x, y);

					Background.Canvas.DrawBitmap (tilesetLoader.Tileset, tilesetRect, renderRect);
				}
			}
		}

		public void HandlePaint (SKSurface surface)
		{
			Background.Draw (surface.Canvas, 0, 0, null);
		}

		public void Invalidate ()
		{
			Controller.Invalidate ();
		}

		public void OnPress (SKPointI point)
		{
		}

		public void OnDetailPress (SKPointI point)
		{
		}

		public void HandleKeyDown (string character)
		{
		}
	}
}
