using ArenaLS.Model;
using ArenaLS.Utilities;
using ArenaLS.Views.Views;
using SkiaSharp;

namespace ArenaLS.Views.Scenes
{
	class CombatScene : IScene
	{
		GameController Controller;
		CombatView CombatView;

		readonly Point CombatOffset = new Point (0, 0);
		readonly Size CombatSize = new Size (1000, 720);

		public CombatScene (GameController controller)
		{
			Controller = controller;
			CombatView = new CombatView (CombatOffset, CombatSize);
		}

		public void Load (string mapName)
		{
			CombatView.Load (mapName);
		}

		public void HandlePaint (SKSurface surface, GameState currentState, long frame)
		{
			surface.Canvas.Clear (SKColors.Black);

			surface.Canvas.DrawSurface (CombatView.Draw (currentState, frame), 0, 0);
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
