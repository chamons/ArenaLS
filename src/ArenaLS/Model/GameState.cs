using System.Collections.Generic;

namespace ArenaLS.Model
{
	public class GameState
	{
		public Character PlayerCharacter { get; private set; }
		public List<Character> Mercenaries { get; private set; } = new List<Character> ();
		public List<Character> Enemies { get; private set; } = new List<Character> ();

		public string CurrentMap { get; private set; }

		public IEnumerable<Character> PlayerCharacters
		{
			get
			{
				yield return PlayerCharacter;
				foreach (var m in Mercenaries)
					yield return m;
			}
		}

		public IEnumerable<Character> AllCharacters
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
			PlayerCharacter = new Character ("Player", 0);
			foreach (var skill in new Skill [] { new Skill ("Heal"), new Skill ("Shield"), new Skill ("Fire"), new Skill ("Lightning"), new Skill ("Poison") })
				PlayerCharacter.AddSkill (skill);

			for (int i = 0; i < 4; ++i)
				Mercenaries.Add (new Character ($"Character {i}", i + 1));
			Enemies.Add (new Character ("Enemy", 5));

			CurrentMap = "BeachMap";
		}
	}
}
