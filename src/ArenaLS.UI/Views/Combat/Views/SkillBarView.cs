using System;

using ArenaLS.Model;
using ArenaLS.Utilities;
using SkiaSharp;

namespace ArenaLS.UI.Views.Combat.Views
{
	class SkillBarView : View
	{
		TilesetLoader Loader;

		public SkillBarView (Point position, Size size) : base (position, size)
		{
			Loader = new TilesetLoader ("data/tf_icon_32.png", 32);
		}

		const int MaxNumberOfSkills = 15;
		const int Padding = 2;
		const int CellSize = 32;
		const bool ShowHotkey = true;

		readonly string [] CellLabels = new string [MaxNumberOfSkills] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=", "[", "]", "\\" };

		readonly SKPaint CooldownPaint = new SKPaint () { Color = SKColors.Black.WithAlpha (220) };
		readonly SKPaint BlackPaint = new SKPaint () { Color = SKColors.Black.WithAlpha (220) };
		readonly SKPaint CellText = new SKPaint () { Color = SKColors.White, TextSize = 10, TextAlign = SKTextAlign.Center };
		readonly SKPaint CellTextDark = new SKPaint () { Color = SKColors.White.WithAlpha (50), TextSize = 10, TextAlign = SKTextAlign.Center };
		readonly SKPaint CellBorder = new SKPaint () { Color = SKColors.White, StrokeWidth = 2, IsStroke = true };

		SKRect RectForSkill (int i)
		{
			int left = Padding + ((Padding + CellSize) * i);
			int top = Padding;
			int right = left + CellSize + Padding;
			int bottom = top + CellSize + Padding;

			return new SKRect (left, top, right, bottom);
		}

		int IDForSkill (Skill skill)
		{
			switch (skill.Name)
			{
				case "Heal":
					return 1;
				case "Fire":
					return 33;
				case "Lightning":
					return 36;
				case "Poison":
					return 42;
				case "Shield":
					return 148;
				default:
					return 0;
			}
		}

		public override SKSurface Draw (GameState currentState, long frame)
		{
			Clear ();

			for (int i = 0; i < Math.Min (currentState.PlayerCharacter.Skills.Count, MaxNumberOfSkills); ++i)
			{
				Skill skill = currentState.PlayerCharacter.Skills [i];
				SKRect rect = RectForSkill (i);

				Canvas.DrawRect (rect, CellBorder);

				SKRect bitmapRect = new SKRect (rect.Left + Padding, rect.Top + Padding, rect.Right - Padding, rect.Bottom - Padding);
				Canvas.DrawBitmap (Loader.Tileset, Loader.GetRect (IDForSkill (skill)), bitmapRect, Styles.AntialiasPaint);

				if (skill.UnderCooldown)
					DrawCooldownOverlay (skill, bitmapRect);

				if (ShowHotkey)
					DrawHotkeyOverlay (i, rect, skill.UnderCooldown);
			}

			return Surface;
		}

		private void DrawCooldownOverlay (Skill skill, SKRect bitmapRect)
		{
			float percentageLeft = (float)skill.CurrentCooldown / (float)skill.SkillCooldown;
			float newHeight = bitmapRect.Height * percentageLeft;
			float remainingHeight = bitmapRect.Height - newHeight;
			SKPoint location = bitmapRect.Location;
			location.Offset (0, remainingHeight);
			SKRect cooldownRect = SKRect.Create (location, new SKSize (bitmapRect.Width, bitmapRect.Height * percentageLeft));
			Canvas.DrawRect (cooldownRect, CooldownPaint);
		}

		private void DrawHotkeyOverlay (int i, SKRect rect, bool skillDisabled)
		{
			float textLeft = rect.Left + (CellSize / 2);
			float textTop = CellSize + Padding + 3;
			Canvas.DrawRect (new SKRect (textLeft - 3, textTop - 9, textLeft + 5, textTop + 2), BlackPaint);
			Canvas.DrawText (CellLabels [i], textLeft, textTop, skillDisabled ? CellTextDark : CellText);
		}

		public override HitTestResults HitTest (SKPointI point)
		{
			if (!ScreenRect.Contains (point))
				return null;

			for (int i = 0; i < MaxNumberOfSkills; ++i)
			{
				SKRect buttonRect = RectForSkill (i);
				buttonRect.Offset (Position.X, Position.Y);
				if (buttonRect.Contains (point))
					return new HitTestResults (this, i);
			}
			return null;
		}
	}
}
