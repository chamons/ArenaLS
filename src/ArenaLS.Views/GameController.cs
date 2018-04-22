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
			GameWindow.OnPress += OnPress;
			GameWindow.OnDetailPress += OnDetailPress;
			GameWindow.OnKeyDown += OnKeyDown;
			GameWindow.OnQuit += OnQuit;
		}

		public void Startup (IFileStorage storage)
		{
			Dependencies.Register<ILogger> (typeof (Logger));
			Dependencies.RegisterInstance<IFileStorage> (storage);

			var combatScene = new CombatScene (this);
			combatScene.Load ("BeachMap");
			CurrentScene = combatScene;

			Log = Dependencies.Get <ILogger>();
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

		void OnPaint (object sender, PaintEventArgs e)
		{
			CurrentScene.HandlePaint (e.Surface);
		}
	}
}