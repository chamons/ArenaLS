using SkiaSharp;

using System.Collections.Generic;

namespace ArenaLS.Views.Scenes.Combat
{
	class CombatScene : IScene
	{
		GameController Controller;
		SKSurface Background;
		List<CharacterRenderer> CharacterRenderers = new List<CharacterRenderer> ();
		SKPointI [] Offsets = new SKPointI [] {
			new SKPointI (525, 175), new SKPointI (525, 250), new SKPointI (525, 325),
			new SKPointI (525, 400), new SKPointI (525, 475), new SKPointI (300, 275),
		};
		public CombatScene (GameController controller)
		{
			Controller = controller;
		}

		public void Load (string mapName)
		{
			var mapLoader = new MapLoader ($"data/maps/{mapName}.tmx");
			Background = BackgroundRenderer.Render (mapLoader, 2f);
			CharacterRenderers.Add (CharacterRenderer.CreateNormalSized ("data/characters/chara6.png", 21, true));
			CharacterRenderers.Add (CharacterRenderer.CreateNormalSized ("data/characters/chara7.png", 15, true));
			CharacterRenderers.Add (CharacterRenderer.CreateNormalSized ("data/characters/chara2.png", 12, false));
			CharacterRenderers.Add (CharacterRenderer.CreateNormalSized ("data/characters/chara2.png", 69, false));
			CharacterRenderers.Add (CharacterRenderer.CreateNormalSized ("data/characters/chara2.png", 21, false));
			CharacterRenderers.Add (CharacterRenderer.CreateExtraLarge ("data/characters/$monster_bird1.png", 0));
		}

		int offsetX = -205;
		int offsetY = -325;

		public void HandlePaint (SKSurface surface, long frame)
		{
			Background.Draw (surface.Canvas, offsetX, offsetY, null);

			for (int i = 0; i < CharacterRenderers.Count; ++i)
				CharacterRenderers[i].Render (surface.Canvas, Offsets[i].X, Offsets [i].Y, frame);
		}

		public void Invalidate ()
		{
			Controller.Invalidate ();
		}

		public void OnPress (SKPointI point)
		{
		}

		public void OnDetailPress (SKPointI point)
		{
		}

		public void OnRelease (SKPointI point)
		{
		}

		public void OnDetailRelease (SKPointI point)
		{
		}

		public void HandleKeyDown (string character)
		{
		}
	}
}
