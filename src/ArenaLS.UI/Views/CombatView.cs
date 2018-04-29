using System;
using ArenaLS.Model;
using ArenaLS.Utilities;
using SkiaSharp;
using ArenaLS.UI.Views.Combat.Utilities;
using ArenaLS.UI.Views.Combat.Views;
using ArenaLS.UI.Views.Combat.Renderers;
using ArenaLS.UI.Utilities;
using ArenaLS.UI.Scenes;

namespace ArenaLS.UI.Views
{
	class CombatView : View
	{
		CharacterRenderCache RenderCache = new CharacterRenderCache ();

		SKSurface Background;
		string MapName;

		readonly Point SkillBarOffset = new Point (2, 680);
		readonly Size SkillBarSize = new Size (550, 40);

		readonly Point LogOffset = new Point (40, 0);

		SkillBarView SkillBar;
		LogView LogView;
		TargettingView TargetView;

		public CombatView (Point position, Size size) : base (position, size)
		{
			SkillBar = new SkillBarView (SkillBarOffset, SkillBarSize);
			LogView = new LogView (LogOffset, new Size (size.Width - (LogOffset.X * 2), 45));
			TargetView = new TargettingView (position, size);

			// TestData - Log test
			LogView.Show ("Test Message", 90);
		}

		public void Load (string mapName)
		{
			MapName = mapName;

			var mapLoader = new MapLoader ($"data/maps/{MapName}.tmx");

			Background = BackgroundRenderer.Render (mapLoader, 2f);
		}

		public override SKSurface Draw (GameState currentState, long frame)
		{
			Clear ();

			DrawBackground ();

			foreach (Character c in currentState.AllCharacters)
			{
				Point renderPoint = CharacterRenderLocation.GetRenderPoint (c);
				RenderCache [c].Render (Canvas, c, renderPoint.X, renderPoint.Y, frame);
			}

			Canvas.DrawSurface (TargetView.Draw (currentState, frame), 0, 0);
			Canvas.DrawSurface (SkillBar.Draw (currentState, frame), SkillBarOffset.X, SkillBarOffset.Y);
			Canvas.DrawSurface (LogView.Draw (currentState, frame), LogOffset.X, LogOffset.Y);

			return Surface;
		}

		Point GetBackgroundOffset ()
		{
			switch (MapName)
			{
				case "BeachMap":
					return new Point (-205, -325);
				default:
					throw new NotImplementedException ();
			}
		}

		void DrawBackground ()
		{
			var backgroundOffset = GetBackgroundOffset ();

			Background.Draw (Canvas, backgroundOffset.X, backgroundOffset.Y, null);
		}

		public void HandleDirection (Direction direction) => TargetView.HandleDirection (direction);
		public void EnableTargetting (TargettingType type) => TargetView.EnableTargetting (type);
		public void DisableTargetting () => TargetView.DisableTargetting ();

		public int CurrentTarget => TargetView.CurrentTarget;

		public override HitTestResults HitTest (SKPointI point)
		{
			return null;
		}
	}
}
