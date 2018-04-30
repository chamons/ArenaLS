using System.Collections.Generic;
using ArenaLS.Model;

namespace ArenaLS.Tests.Utilities
{
	internal static class TestFactory
	{
		public static Skill CreateTestSkill () => new Skill ("Test", 10, 10, (c, t) => {});
		public static Character CreateTestCharacter () => new Character ("Test", 0, new Health (100, 100));

		public static GameState CreateTestState ()
		{
			var player = new Character ("Player", 0, new Health (100, 100));

			List<Character> mercenaries = new List<Character> ();
			for (int i = 0; i < 4; ++i)
				mercenaries.Add (new Character ($"Character {i}", i + 1, new Health (100, 100)));

			List<Character> enemies = new List<Character> ();
			enemies.Add (new Character ("Enemy", 5, new Health (100, 100)));

			return GameState.Create (player, mercenaries, enemies, "TestMap");
		}
	}
}
