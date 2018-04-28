using ArenaLS.UI.Views.Combat.Renderers;
using ArenaLS.Utilities;
using SkiaSharp;

namespace ArenaLS.UI.Views.Combat.Views
{
	class TargettingView : View
	{
		TargettingRenderer TargetRender;

		public TargettingView (Point position, Size size) : base (position, size)
		{
			TargetRender = new TargettingRenderer ();
		}

		public override HitTestResults HitTest (SKPointI point)
		{
			return null;
		}
	}
}
