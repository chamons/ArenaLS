using ArenaLS.Utilities;
using ArenaLS.UI;
using SkiaSharp;
using System;
using System.Windows;
using Point = System.Windows.Point;

namespace ArenaLS.Windows
{
	public partial class MainWindow : Window, IGameWindow
	{
		GameController Controller;
		PaintEventArgs PaintArgs = new PaintEventArgs ();
		ClickEventArgs ClickArgs = new ClickEventArgs ();
		KeyEventArgs KeyArgs = new KeyEventArgs ();

		public event EventHandler<PaintEventArgs> OnPaint;
		public event EventHandler<ClickEventArgs> OnPress;
		public event EventHandler<ClickEventArgs> OnDetailPress;

		public event EventHandler<ClickEventArgs> OnRelease;
		public event EventHandler<ClickEventArgs> OnDetailRelease;

		public new event EventHandler<KeyEventArgs> OnKeyDown;

		public event EventHandler<EventArgs> OnQuit;
		System.Windows.Media.Matrix Transform;
		System.Windows.Threading.DispatcherTimer Timer;
		public long Frame { get; private set; } = 0;
		public float Scale { get; private set; }

		public MainWindow ()
		{
			InitializeComponent ();

			Loaded += OnLoaded;
			TextInput += OnPlatformTextEnter;
			KeyDown += OnPlatformKeyDown;
			Closed += OnPlatformClose;

		}

		public void StartAnimationTimer ()
		{
			Timer = new System.Windows.Threading.DispatcherTimer ();
			Timer.Tick += (o, e) =>
			{
				Frame++;
				Invalidate (); // This is a bit lazy				
			};
			Timer.Interval = new TimeSpan (0, 0, 0, 0, 33);
			Timer.Start ();
		}

		public void Invalidate ()
		{
			SkiaView.InvalidateVisual ();
		}

		void IGameWindow.Close ()
		{
			Application.Current.Shutdown ();
		}

		void OnLoaded (object sender, RoutedEventArgs e)
		{
			Transform = PresentationSource.FromVisual (this).CompositionTarget.TransformToDevice;
			 
			Controller = new GameController (this);
			Controller.Startup (new FileStorage ());
			SkiaView.InvalidateVisual ();

			Scale = GetUIScale () > 1 ? 1.33f : 1f;
		}

		float GetUIScale ()
		{
			PresentationSource source = PresentationSource.FromVisual (SkiaView);

			double dpiX = 96.0 * source.CompositionTarget.TransformToDevice.M11;
			return (float)(dpiX / 96.0);
		}

		void OnPlatformPaint (object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
		{
			PaintArgs.Surface = e.Surface;
			OnPaint?.Invoke (this, PaintArgs);
		}

		void OnPlatformMouseDown (object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			EventHandler<ClickEventArgs> currentEvent = e.ChangedButton == System.Windows.Input.MouseButton.Left ? OnPress : OnDetailPress;
			ClickArgs.Position = GetMousePosition (e);
			currentEvent?.Invoke (this, ClickArgs);
		}

		void OnPlatformMouseUp (object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			EventHandler<ClickEventArgs> currentEvent = e.ChangedButton == System.Windows.Input.MouseButton.Left ? OnRelease : OnDetailRelease;
			ClickArgs.Position = GetMousePosition (e);
			currentEvent?.Invoke (this, ClickArgs);
		}

		SKPointI GetMousePosition (System.Windows.Input.MouseButtonEventArgs e)
		{
			Point p = e.GetPosition (null);
			p = Transform.Transform (p);
			return new SKPointI ((int)p.X, (int)p.Y);
		}

		void OnPlatformKeyDown (object sender, System.Windows.Input.KeyEventArgs e)
		{
			string entry = e.Key.ToString ();
			if (entry.Length > 1)
			{
				KeyArgs.Character = e.Key.ToString ();
				OnKeyDown?.Invoke (this, KeyArgs);
			}
		}

		void OnPlatformTextEnter (object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			string entry = e.TextComposition.Text;
			if (entry.Length == 1 && char.IsLetter (entry [0]))
			{
				KeyArgs.Character = entry;
				OnKeyDown?.Invoke (this, KeyArgs);
			}
		}

		void OnPlatformClose (object sender, EventArgs e)
		{
			OnQuit?.Invoke (this, e);
		}
	}
}
