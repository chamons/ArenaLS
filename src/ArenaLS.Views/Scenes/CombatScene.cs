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
			Background = TilesetRenderer.Render (mapLoader, 2f);
		}

		int offsetX = -205;
		int offsetY = -325;

		public void HandlePaint (SKSurface surface)
		{
			Background.Draw (surface.Canvas, offsetX, offsetY, null);
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

		public void OnRelease (SKPointI point)
		{
		}

		public void OnDetailRelease (SKPointI point)
		{
		}

		public void HandleKeyDown (string character)
		{
		}
	}
}
