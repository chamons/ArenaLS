using SkiaSharp;
namespace ArenaLS.Views.Scenes
{
	internal interface IScene
	{
		void HandlePaint (SKSurface surface);

		void OnPress (SKPointI point);
		void OnDetailPress (SKPointI point);
		void HandleKeyDown (string character);

		void Invalidate ();
	}
}
