using SkiaSharp;

namespace ArenaLS.Views.Views
{
	static class Styles
	{
		public static SKPaint TextPaint = new SKPaint ()
		{
			TextSize = 14,
			Color = new SKColor (238, 238, 238),
			IsAntialias = true,
			IsAutohinted = true,
			StrokeWidth = 3
		};

		public static SKPaint SmallTextPaint = new SKPaint ()
		{
			TextSize = 9,
			Color = new SKColor (238, 238, 238),
			IsAntialias = true,
			IsAutohinted = true,
			StrokeWidth = 2
		};

		public static readonly SKPaint TextBackground = new SKPaint () { Color = SKColors.Gray.WithAlpha (160) };
		public static readonly SKPaint TextBackgroundDark = new SKPaint () { Color = SKColors.DarkGray.WithAlpha (160) };
		public static readonly SKPaint AntialiasPaint = new SKPaint () { IsAntialias = false };
	}
}
