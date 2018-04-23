using System;
using ArenaLS.Platform;
using ArenaLS.Views;
using ArenaLS.Views.Scenes;

namespace ArenaLS
{
	public class GameController
	{
		public IGameWindow GameWindow { get; }
		IScene CurrentScene;

		ILogger Log;

		public GameController (IGameWindow gameWindow)
		{
			GameWindow = gameWindow;
			GameWindow.OnPaint += OnPaint;
			GameWindow.OnKeyDown += OnKeyDown;
			GameWindow.OnQuit += OnQuit;

			GameWindow.OnPress += OnPress;
			GameWindow.OnDetailPress += OnDetailPress;
			GameWindow.OnRelease += OnRelease;
			GameWindow.OnDetailRelease += OnDetailRelease;
		}

		public void Startup (IFileStorage storage)
		{
			Dependencies.Register<ILogger> (typeof (Logger));
			Dependencies.RegisterInstance<IFileStorage> (storage);

			var combatScene = new CombatScene (this);
			// TestData
			combatScene.Load ("BeachMap");
			CurrentScene = combatScene;

			Log = Dependencies.Get <ILogger>();

			GameWindow.StartAnimationTimer ();
		}

		public void Invalidate ()
		{
			GameWindow.Invalidate ();
		}

		void OnQuit (object sender, EventArgs e)
		{
		}

		void OnKeyDown (object sender, KeyEventArgs e)
		{
			CurrentScene.HandleKeyDown (e.Character);
		}

		void OnPress (object sender, ClickEventArgs e)
		{
			CurrentScene.OnPress (e.Position);
		}

		void OnDetailPress (object sender, ClickEventArgs e)
		{
			CurrentScene.OnDetailPress (e.Position);
		}

		void OnRelease (object sender, ClickEventArgs e)
		{
			CurrentScene.OnRelease (e.Position);
		}

		void OnDetailRelease (object sender, ClickEventArgs e)
		{
			CurrentScene.OnDetailRelease (e.Position);
		}

		void OnPaint (object sender, PaintEventArgs e)
		{
			CurrentScene.HandlePaint (e.Surface, GameWindow.Frame);
		}
	}
}