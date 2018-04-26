using ArenaLS.Model;
using ArenaLS.Utilities;
using ArenaLS.Views.Utilities;
using SkiaSharp;

namespace ArenaLS.Views.Views.Combat
{
	class LogView : View
	{
		TilesetLoader Loader;
		string Message;
		int UntilFrame = int.MinValue;

		public LogView (Point position, Size size) : base (position, size)
		{
			Loader = new TilesetLoader ("data/tf_icon_32.png", 32);
		}

		public void Show (string message, int untilFrame)
		{
			Message = message;
			UntilFrame = untilFrame;
		}

		public void Show (string message)
		{
			Message = message;
			UntilFrame = int.MaxValue;
		}

		public void Dismiss ()
		{
			UntilFrame = int.MinValue;
		}

		bool IsEnabled (long frame) => frame < UntilFrame;

		public override SKSurface Draw (GameState currentState, long frame)
		{
			if (IsEnabled (frame))
			{
				base.Draw (currentState, frame);

				var logFrameRect = SKRect.Create (SKPoint.Empty, Size.AsSKSize ());
				Canvas.DrawRoundRect (logFrameRect, 15, 20, Styles.LogBorder);
				logFrameRect.Inflate (-2, -2);
				Canvas.DrawRoundRect (logFrameRect, 15, 20, Styles.LogFill);
				Canvas.DrawText (Message, new SKPoint (Size.Width / 2, 6 + Size.Height / 2), Styles.LargeTextPaintCentered);

				return Surface;
			}
			else
			{
				Surface.Canvas.Clear ();
				return Surface;
			}
		}

		public override HitTestResults HitTest (SKPointI point) => null;
	}
}
