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
			Background = TilesetRenderer.Render (mapLoader);
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
