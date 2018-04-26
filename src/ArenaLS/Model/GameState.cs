using System.Collections.Generic;

namespace ArenaLS.Model
{
	public class GameState
	{
		public Character PlayerCharacter { get; private set; }
		public List<Character> Mercenaries { get; private set; }
		public List<Character> Enemies { get; private set; }

		public IEnumerable<Character> PlayerCharacters
		{
			get
			{
				yield return PlayerCharacter;
				foreach (var m in Mercenaries)
					yield return m;
			}
		}

		public IEnumerable<Character> Characters
		{
			get
			{
				foreach (var p in PlayerCharacters)
					yield return p;
				foreach (var e in Enemies)
					yield return e;
			}
		}

		internal GameState ()
		{
			CreateTestData ();
		}

		// TestData
		void CreateTestData ()
		{
			PlayerCharacter = new Character ();
			for (int i = 0; i < 4; ++i)
				Mercenaries.Add (new Character ());
			Enemies.Add (new Character ());
		}
	}
}
