using ArenaLS.Utilities;
using ArenaLS.Views.Views.Combat;
using SkiaSharp;
using System.Collections.Generic;

namespace ArenaLS.Views.Views
{
	class CombatView : View
	{
		SKSurface Background;
		List<CharacterRenderer> CharacterRenderers = new List<CharacterRenderer> ();

		// TestData
		const int CharacterStartingY = 180;
		const int CharacterYDelta = 80;
		Point [] Offsets = new Point [] {
			new Point (525, CharacterStartingY), new Point (525, CharacterStartingY + (1 * CharacterYDelta)),
			new Point (525, CharacterStartingY + (2 * CharacterYDelta)),
			new Point (525, CharacterStartingY + (3 * CharacterYDelta)),
			new Point (525, CharacterStartingY + (4 * CharacterYDelta)), new Point (300, 275),
		};

		readonly Point SkillBarOffset = new Point (2, 680);
		readonly Size SkillBarSize = new Size (550, 40);

		SkillBarView SkillBar;

		public CombatView (Point position, Size size) : base (position, size)
		{
			// TestData
			CharacterRenderers.Add (CharacterRenderer.CreateNormalSized ("data/characters/chara6.png", 21, true));
			CharacterRenderers.Add (CharacterRenderer.CreateNormalSized ("data/characters/chara7.png", 15, true));
			CharacterRenderers.Add (CharacterRenderer.CreateNormalSized ("data/characters/chara2.png", 12, false));
			CharacterRenderers.Add (CharacterRenderer.CreateNormalSized ("data/characters/chara2.png", 69, false));
			CharacterRenderers.Add (CharacterRenderer.CreateNormalSized ("data/characters/chara2.png", 21, false));
			CharacterRenderers.Add (CharacterRenderer.CreateExtraLarge ("data/characters/$monster_bird1.png", 0));

			SkillBar = new SkillBarView (SkillBarOffset, SkillBarSize);
		}

		public void Load (string mapName)
		{
			var mapLoader = new MapLoader ($"data/maps/{mapName}.tmx");

			Background = BackgroundRenderer.Render (mapLoader, 2f);
		}

		public override SKSurface Draw (long frame)
		{
			// TestData - Hard coded
			const int OffsetX = -205;
			const int OffsetY = -325;

			base.Draw (frame);

			Background.Draw (Canvas, OffsetX, OffsetY, null);

			for (int i = 0; i < CharacterRenderers.Count; ++i)
				CharacterRenderers [i].Render (Canvas, Offsets [i].X, Offsets [i].Y, frame);

			Canvas.DrawSurface (SkillBar.Draw (frame), SkillBarOffset.X, SkillBarOffset.Y);

			return Surface;
		}

		public override HitTestResults HitTest (SKPointI point)
		{
			return null;
		}
	}
}
