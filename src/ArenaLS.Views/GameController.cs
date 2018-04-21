using System;
using System.Collections.Generic;
using System.Linq;
using ArenaLS.Platform;
using ArenaLS.Views;

namespace ArenaLS
{
	public class GameController
	{
		public IGameWindow GameWindow { get; }
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
		}

		void OnPress (object sender, ClickEventArgs e)
		{
		}

		void OnDetailPress (object sender, ClickEventArgs e)
		{
		}

		void OnPaint (object sender, PaintEventArgs e)
		{
			e.Surface.Canvas.DrawColor (new SkiaSharp.SKColor (255, 0, 0));
		}
	}
}